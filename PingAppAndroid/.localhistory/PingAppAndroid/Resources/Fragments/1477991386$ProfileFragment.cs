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

namespace PingAppAndroid.Resources.Fragments
{
    public class ProfileFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Profile, container, false);

            Button signOut = view.FindViewById<Button>(Resource.Id.buttonSignOut);

            signOut.Click += SignOutUser;

            //Lägg till funktioner för knapparna Settings/Log out m.m.
            //var sampleTextView = view.FindViewById<TextView>(Resource.Id.sampleTextView);
            //sampleTextView.Text = "sample fragment text";

            return view;
        }

        private void SignOutUser(object sender, EventArgs e)
        {
           
        }
    }
}