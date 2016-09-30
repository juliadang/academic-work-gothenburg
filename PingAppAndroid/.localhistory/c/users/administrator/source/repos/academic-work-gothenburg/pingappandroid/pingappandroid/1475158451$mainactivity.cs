using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace PingAppAndroid
{
    [Activity(Label = "PingAppAndroid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Button Login = FindViewById<Button>(Resource.Id.button1);
            Login.Click += redirectToIndex;
        }

        private void redirectToIndex(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.Index);
        }
    }
}

