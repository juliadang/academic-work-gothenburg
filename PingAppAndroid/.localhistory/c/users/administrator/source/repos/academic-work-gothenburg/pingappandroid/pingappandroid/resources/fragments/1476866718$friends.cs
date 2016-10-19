using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using PingAppAndroid.Models;
using PingAppAndroid.Adapters;
using Android.Text;
using System.Net.Http;

namespace PingAppAndroid.Resources.Fragments
{
    //Todo: När man klickar på friends kraschar det
    //FindViewById...
    public class Friends : Fragment
    {
        EditText mSearch;
        FriendListAdapter mFriendAdapter;
        Button buttonAddFriend;
        ListView mFriendList;
        List<Friend> mFriends = new List<Friend> {
            new Friend { UserName = "Bo", FirstName = "Bo", LastName = "Johansson" },
            new Friend { UserName = "Li", FirstName = "Li", LastName = "Andersson" },
            new Friend { UserName = "An", FirstName = "An", LastName = "Svensson" }
        };

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Friends, container, false);
            //var sampleTextView = view.FindViewById<TextView>(Resource.Id.sampleTextView);
            //sampleTextView.Text = "sample fragment text";
            mFriendList = View.FindViewById<ListView>(Resource.Id.friendList);
            //   mFriendAdapter = new FriendListAdapter(this, mFriends);

            //mFriendList.Adapter = mFriendAdapter;

            mSearch = View.FindViewById<EditText>(Resource.Id.searchbarFriend);

            mSearch.TextChanged += mSearch_TextChanged;

            buttonAddFriend = View.FindViewById<Button>(Resource.Id.buttonAddFriend);

            buttonAddFriend.Click += buttonAddFriend_AddFriend;


            return view;
        }

        private async void buttonAddFriend_AddFriend(object sender, EventArgs e)
        {
            string username2 = View.FindViewById<EditText>(Resource.Id.searchbarFriend).Text;

            bool succeeded;
            succeeded = await DataManager.AddFriend(username2);

            if (succeeded)
            {
                AlertDialog.Builder("Friend added");
            }
        }

        private void mSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Friend> searchedFriends = mFriends.Where(f => f.UserName.Contains(mSearch.Text, StringComparison.OrdinalIgnoreCase)
                                                            || f.FirstName.Contains(mSearch.Text, StringComparison.OrdinalIgnoreCase)
                                                            || f.LastName.Contains(mSearch.Text, StringComparison.OrdinalIgnoreCase)).ToList();

            // mFriendAdapter = new FriendListAdapter(this, searchedFriends);
            // mFriendList.Adapter = mFriendAdapter;
        }
    }
}