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
using Android.Graphics.Drawables;
using Android.Text;
using PingAppAndroid.Models;

namespace PingAppAndroid
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Register);

            EditText username = FindViewById<EditText>(Resource.Id.username);
            EditText password = FindViewById<EditText>(Resource.Id.password);
            EditText email = FindViewById<EditText>(Resource.Id.email);
            Button submit = FindViewById<Button>(Resource.Id.submit);

            if (username.Text.Length == 0)
                username.SetError(new Java.Lang.String("Username Required"), GetDrawable(Resource.Drawable.Icon));

            submit.Click += async (sender, e) =>
            {
                User user = new User
                {
                    UserName = username.Text,
                    Password = password.Text,
                    Email = email.Text
                };
                string result = await DataManager.insertUpdateData(user);
                username.Text = "";
                password.Text = "";
                email.Text = "";
                new AlertDialog.Builder(this).SetMessage(result).Show();
            };


            //username.TextChanged += (sender, e) =>
            //{
            //    Console.WriteLine("Event fired");
            //    if (username.Text.Length == 0)
            //        username.SetError(new Java.Lang.String("Username Required"), GetDrawable(Resource.Drawable.Icon));
            //};

            //username.addTextChangedListener(this);
            //txt2.addTextChangedListener(this);
            //txt3.addTextChangedListener(this);

        }


    }
}