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
using System.Net.Http;

namespace PingAppAndroid
{
    [Activity(Label = "Ping", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //Alert-check for database created
            //new AlertDialog.Builder(this).SetMessage(DataManager.createDatabase()).Show();


            Button Login = FindViewById<Button>(Resource.Id.button1);
            Login.Click += login;

            Button Register = FindViewById<Button>(Resource.Id.register);
            Register.Click += registerUser;
        }

        private async void login(object sender, EventArgs e)
        {
            EditText userName = FindViewById<EditText>(Resource.Id.userNameMain);
            EditText password = FindViewById<EditText>(Resource.Id.passwordMain);

            bool succeeded;
            succeeded = await DataManager.SignIn(userName.Text, password.Text);

            if (succeeded)
            {
                Intent index = new Intent(this, typeof(AppActivity));
                StartActivity(index);
                this.Finish();
            }
            else
            {
                new AlertDialog.Builder(this).SetMessage("Login failed").Show();
            }

            userName.Text = "";
            password.Text = "";
        }

        private void registerUser(object sender, EventArgs e)
        {
            Intent register = new Intent(this, typeof(RegisterActivity));
            StartActivity(register);
        }
    }
}
