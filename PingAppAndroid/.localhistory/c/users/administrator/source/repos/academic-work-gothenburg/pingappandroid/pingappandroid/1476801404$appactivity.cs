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
     
        List<Ping> mPings = new List<Ping>
        {
            new Ping { Time = new DateTime(2016, 10, 4, 13, 30, 0), Sender = new Friend { UserName = "Bo", FirstName = "Bo", LastName = "Johansson" }, Type = 1},
            new Ping { Time = new DateTime(2016, 10, 3, 14, 30, 0), Sender = new Friend { UserName = "Li", FirstName = "Li", LastName = "Andersson" }, Type = 3},
            new Ping { Time = new DateTime(2015, 10, 4, 15, 19, 0), Sender = new Friend { UserName = "An", FirstName = "An", LastName = "Svensson" }, Type = 2}
        };

  
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.FrameLayout);

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            AddTab("Notifications", new Notifications());
            AddTab("Profile", new Profile());
            AddTab("Friends", new Friends());

            if (savedInstanceState != null) this.ActionBar.SelectTab(this.ActionBar.GetTabAt(savedInstanceState.GetInt("tab")));

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

      
    }
}