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
using Newtonsoft.Json;

namespace PingAppAndroid.Resources.Fragments
{
    public class FriendsFragment : Fragment
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
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Friends, container, false);

            mSearch = view.FindViewById<EditText>(Resource.Id.searchbarFriend);
            buttonAddFriend = view.FindViewById<Button>(Resource.Id.buttonAddFriend);

            mSearch.TextChanged += mSearch_TextChanged;
            buttonAddFriend.Click += buttonAddFriend_AddFriend;

            List<string> friendlist = JsonConvert<User>();
            mFriendList = view.FindViewById<ListView>(Resource.Id.friendList);
            mFriendAdapter = new FriendListAdapter(Activity, (DataManager.GetAllFriends()));
            mFriendList.Adapter = mFriendAdapter;

            return view;
        }

        private async void buttonAddFriend_AddFriend(object sender, EventArgs e)
        {
            string username2 = View.FindViewById<EditText>(Resource.Id.searchbarFriend).Text;

            string response;
            response = await DataManager.AddFriend(username2);

            var context = this.Activity;
            new AlertDialog.Builder(context).SetMessage(response).Show();

        }

        private void mSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Friend> searchedFriends = mFriends.Where(f => f.UserName.Contains(mSearch.Text, StringComparison.OrdinalIgnoreCase)
                                                            || f.FirstName.Contains(mSearch.Text, StringComparison.OrdinalIgnoreCase)
                                                            || f.LastName.Contains(mSearch.Text, StringComparison.OrdinalIgnoreCase)).ToList();

            mFriendAdapter = new FriendListAdapter(Activity, searchedFriends);
            mFriendList.Adapter = mFriendAdapter;
        }
    }
}