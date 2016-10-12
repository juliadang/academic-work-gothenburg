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

                User user = new User
                {
                    UserName = username.Text,
                    Password = password.Text,
                    Email = email.Text
                };

                WebClient client = new WebClient();
                Uri uri = new Uri("http://192.168.2.8/CreateContact.php");
                NameValueCollection parameters = new NameValueCollection();

                parameters.Add("Username", username.Text);
                parameters.Add("Password", password.Text);

                client.UploadValuesCompleted += client_UploadValuesCompleted;
                //client.UploadValuesAsync(uri, parameters);


                username.Text = "";
                password.Text = "";
                email.Text = "";
                new AlertDialog.Builder(this).SetMessage(result).Show();
            };
        }

        private void client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            
        }

        void mButtonCreateContact_Click(object sender, EventArgs e)
        {
            mProgressBar.Visibility = ViewStates.Visible;

            
        }
    }
}