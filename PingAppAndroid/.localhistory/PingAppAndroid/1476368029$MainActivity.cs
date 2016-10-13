using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using Android.Content.PM;
using Java.Security;
using SQLite;
using System.Runtime.CompilerServices;
using PingAppAndroid.Models;

namespace PingAppAndroid
{
    [Activity(Label = "Ping", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        const string url = "http://pinggothenburg.azurewebsites.net";
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //Alert-check for database created
            new AlertDialog.Builder(this).SetMessage(DataManager.createDatabase()).Show();

            Button Login = FindViewById<Button>(Resource.Id.button1);
            Login.Click += redirectToApp;

            Button Register = FindViewById<Button>(Resource.Id.register);
            Register.Click += registerUser;
        }

        private void redirectToApp(object sender, EventArgs e)
        {
            Intent index = new Intent(this, typeof(AppActivity));
            StartActivity(index);

            //SetContentView(Resource.Layout.Index);
        }

        private void registerUser(object sender, EventArgs e)
        {
            Intent register = new Intent(this, typeof(RegisterActivity));
            StartActivity(register);
        }
    }
}
