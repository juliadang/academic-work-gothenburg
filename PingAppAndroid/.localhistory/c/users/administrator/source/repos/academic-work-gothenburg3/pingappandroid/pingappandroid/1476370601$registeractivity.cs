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
using Android;
using System.Net;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.Net.Http;

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

           // String[] permissions = { Manifest.Permission.WriteExternalStorage };
        //    RequestPermissions(permissions, WriteRequestCode);

            if (username.Text.Length == 0)
                username.SetError(new Java.Lang.String("Username Required"), GetDrawable(Resource.Drawable.Icon));

            submit.Click += (sender, e) =>
            {
                string url = "http://pinggothenburg.azurewebsites.net/api/account?name=" + username.Text + "&password=" + password.Text + "&email=" + email.Text;
                username.Text = "";
                password.Text = "";
                email.Text = "";

            };
        }

        void client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            //Activity.RunOnUiThread(() =>
            //{
            //    var id = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(e.Result)); //Get the data echo backed from PHP

                //if (OnCreateContact != null)
                //{
                //    //Broadcast event
                //    OnCreateContact.Invoke(this, new CreateContactEventArgs(txtName.Text, txtNumber.Text));
                //}

                //mProgressBar.Visibility = ViewStates.Invisible;
                //this.Dismiss();
            //});

        }
    }
    public class CreateContactEventArgs : EventArgs
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsCreated { get; set; }

        public CreateContactEventArgs(int id, string username, string password, string email, bool isCreated)
        {
            ID = id;
            Username = username;
            Password = password;
            Email = email;
            IsCreated = isCreated;
        }
    }
}