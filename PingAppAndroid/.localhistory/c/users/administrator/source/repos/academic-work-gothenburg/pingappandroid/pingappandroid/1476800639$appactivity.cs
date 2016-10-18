using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PingAppAndroid.Models;
using PingAppAndroid.Adapters;
using Android.Text;
using PingAppAndroid.Resources.Fragments;

namespace PingAppAndroid
{
    [Activity(Label = "Ping", Icon = "@drawable/icon")]
    public class AppActivity : Activity
    {
        List<Friend> mFriends = new List<Friend> {
            new Friend { UserName = "Bo", FirstName = "Bo", LastName = "Johansson" },
            new Friend { UserName = "Li", FirstName = "Li", LastName = "Andersson" },
            new Friend { UserName = "An", FirstName = "An", LastName = "Svensson" }
        };
        List<Ping> mPings = new List<Ping>
        {
            new Ping { Time = new DateTime(2016, 10, 4, 13, 30, 0), Sender = new Friend { UserName = "Bo", FirstName = "Bo", LastName = "Johansson" }, Type = 1},
            new Ping { Time = new DateTime(2016, 10, 3, 14, 30, 0), Sender = new Friend { UserName = "Li", FirstName = "Li", LastName = "Andersson" }, Type = 3},
            new Ping { Time = new DateTime(2015, 10, 4, 15, 19, 0), Sender = new Friend { UserName = "An", FirstName = "An", LastName = "Svensson" }, Type = 2}
        };

        EditText mSearch;
        FriendListAdapter mFriendAdapter;
        ListView mFriendList;
        ListView mPingList;
        PingListAdapter mPingAdapter;
        Spinner mFilter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.FrameLayout);

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            AddTab("Tab 1", new Profile());
            //AddTab("Tab 2", Resource.Drawable.ic_tab_white, new SampleTabFragment2());

            //if (bundle != null) this.ActionBar.SelectTab(this.ActionBar.GetTabAt(bundle.GetInt("tab")));

            //Button notification = FindViewById<Button>(Resource.Id.NotificationsBtn);
            //Button profile = FindViewById<Button>(Resource.Id.ProfileBtn);
            //Button friends = FindViewById<Button>(Resource.Id.FriendListBtn);
            //mPingList = FindViewById<ListView>(Resource.Id.RecentPings);
            //mFilter = FindViewById<Spinner>(Resource.Id.spinner1);

            //profile.Click += profile_click;
            //friends.Click += friends_click;
            //mFilter.ItemSelected += mFilter_selectedItem;

            //mPingAdapter = new PingListAdapter(this, mPings);
            //mPingList.Adapter = mPingAdapter;
        }

        void AddTab(string tabText, Fragment view)
        {
            var tab = this.ActionBar.NewTab();
            tab.SetText(tabText);
         
            // must set event handler before adding tab

            tab.TabSelected += delegate (object sender, Android.App.ActionBar.TabEventArgs e)
            {
                var fragment = this.FragmentManager.FindFragmentById(Resource.Id.frameLayout);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);
                e.FragmentTransaction.Add(Resource.Id.frameLayout, view);
            };
            tab.TabUnselected += delegate (object sender, Android.App.ActionBar.TabEventArgs e) {
                e.FragmentTransaction.Remove(view);
            };

            this.ActionBar.AddTab(tab);
        }

        private void mFilter_selectedItem(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            string choosenIcon = mFilter.SelectedItem.ToString();
            if (choosenIcon.ToLower() == "filter")
            {
                mPingAdapter = new PingListAdapter(this, mPings);
                mPingList.Adapter = mPingAdapter;
            }

            else
            {
                List<Ping> filteredpings = mPings.Where(p => p.Type.ToString().Contains(choosenIcon, StringComparison.OrdinalIgnoreCase)).ToList();
                mPingAdapter = new PingListAdapter(this, filteredpings);
                mPingList.Adapter = mPingAdapter;
                Console.WriteLine(choosenIcon);
            }
        }

        private void notification_click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.Notification);

        }
        private void profile_click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.Profile);
        }
        private void friends_click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.Friends);
            //ArrayAdapter<Friend> adapter = new ArrayAdapter<Friend>(this, Android.Resource.Layout.SimpleListItem1, mFriends);
            mFriendList = FindViewById<ListView>(Resource.Id.friendList);
            mFriendAdapter = new FriendListAdapter(this, mFriends);

            mFriendList.Adapter = mFriendAdapter;

            mSearch = FindViewById<EditText>(Resource.Id.searchbarFriend);

            mSearch.TextChanged += mSearch_TextChanged;

        }

        private void mSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Friend> searchedFriends = mFriends.Where(f => f.UserName.Contains(mSearch.Text, StringComparison.OrdinalIgnoreCase)
                                                            || f.FirstName.Contains(mSearch.Text, StringComparison.OrdinalIgnoreCase)
                                                            || f.LastName.Contains(mSearch.Text, StringComparison.OrdinalIgnoreCase)).ToList();

            mFriendAdapter = new FriendListAdapter(this, searchedFriends);
            mFriendList.Adapter = mFriendAdapter;
        }
    }
}