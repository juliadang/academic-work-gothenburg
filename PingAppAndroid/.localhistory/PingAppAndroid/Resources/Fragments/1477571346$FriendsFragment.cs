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
        Button mButtonAddFriend;
        ListView mFriendList;
        List<string> friendlist = DataManager.GetAllFriends();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Friends, container, false);

            mSearch = view.FindViewById<EditText>(Resource.Id.searchbarFriend);
            mButtonAddFriend = view.FindViewById<Button>(Resource.Id.buttonAddFriend);

            mSearch.TextChanged += mSearch_TextChanged;
            mButtonAddFriend.Click += buttonAddFriend_AddFriend;

            mFriendList = view.FindViewById<ListView>(Resource.Id.friendList);
            mFriendAdapter = new FriendListAdapter(Activity, friendlist);
            mFriendList.Adapter = mFriendAdapter;

            mFriendList.ItemClick += MFriendList_ItemClick;
            return view;
        }

        private void MFriendList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var sendToFriend = friendlist[e.Position].username;
        }

        private async void buttonAddFriend_AddFriend(object sender, EventArgs e)
        {
            string username2 = View.FindViewById<EditText>(Resource.Id.searchbarFriend).Text;

            string response;
            response = await DataManager.AddFriendAsync(username2);

            var context = this.Activity;
            new AlertDialog.Builder(context).SetMessage(response).Show();
        }

        private void mSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Todo: Fixa searchfunktion
            //List<Friend> searchedFriends = mFriends.Where(f => f.UserName.Contains(mSearch.Text, StringComparison.OrdinalIgnoreCase)
            //                                                || f.FirstName.Contains(mSearch.Text, StringComparison.OrdinalIgnoreCase)
            //                                                || f.LastName.Contains(mSearch.Text, StringComparison.OrdinalIgnoreCase)).ToList();

            //mFriendAdapter = new FriendListAdapter(Activity, searchedFriends);
            //mFriendList.Adapter = mFriendAdapter;
        }
    }
}