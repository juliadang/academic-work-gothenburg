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

namespace PingAppAndroid
{
    [Activity(Label = "Ping", Icon = "@drawable/icon")]
    public class AppActivity : Activity
    {
        List<Friend> mFriends = new List<Friend> {
            new Friend { UserName = "Bo" },
            new Friend { UserName = "Li" },
            new Friend { UserName = "An" }
        };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.Notification);

            Button notification = FindViewById<Button>(Resource.Id.NotificationsBtn);
            Button profile = FindViewById<Button>(Resource.Id.ProfileBtn);
            Button friends = FindViewById<Button>(Resource.Id.FriendListBtn);

            

            notification.Click += notification_click;
            profile.Click += profile_click;
            friends.Click += friends_click;
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
            ListView friendlist = FindViewById<ListView>(Resource.Id.friendList);
            FriendListAdapter adapter = new FriendListAdapter(this, mFriends.ToArray());

            friendlist.Adapter = adapter;




            SearchView1.TextChanged
        }
    }
}