﻿using Android.App;
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
    public class LoginRegisterActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.LoginRegister);

            Button Login = FindViewById<Button>(Resource.Id.buttonLogIn);
            Login.Click += login;

            Button Register = FindViewById<Button>(Resource.Id.buttonRegister);
            Register.Click += registerUser;
        }

        private async void login(object sender, EventArgs e)
        {
            EditText userName = FindViewById<EditText>(Resource.Id.userNameMain);
            EditText password = FindViewById<EditText>(Resource.Id.passwordMain);

            bool succeeded;
            succeeded = await DataManager.SignInAsync(userName.Text, password.Text);

            if (succeeded)
            {
                DataManager.GetAllFriendsAsync();
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