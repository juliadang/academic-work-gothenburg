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
using ActionBar = Android.App.ActionBar;

namespace PingAppAndroid
{
    [Activity(Label = "Ping", Icon = "@drawable/icon")]
    public class AppActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FrameLayout);

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            AddTab("Notifications", new NotificationsFragment());
            AddTab("Profile", new ProfileFragment());
            AddTab("Friends", new FriendsFragment());

            //SavedInstanceState gör så att man kan se alla tabbar
            if (savedInstanceState != null) ActionBar.SelectTab(ActionBar.GetTabAt(savedInstanceState.GetInt("tab")));
        }

        void AddTab(string tabText, Fragment view)
        {
            var tab = this.ActionBar.NewTab();
            tab.SetText(tabText);
         
            // must set event handler before adding tab

            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                var fragment = this.FragmentManager.FindFragmentById(Resource.Id.frameLayout);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);
                e.FragmentTransaction.Add(Resource.Id.frameLayout, view);
            };
            tab.TabUnselected += delegate (object sender, ActionBar.TabEventArgs e) {
                e.FragmentTransaction.Remove(view);
            };

            ActionBar.AddTab(tab);
        }
    }
}