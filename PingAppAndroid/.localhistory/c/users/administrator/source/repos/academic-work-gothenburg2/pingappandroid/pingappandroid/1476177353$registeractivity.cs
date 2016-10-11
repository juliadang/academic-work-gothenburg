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

            if (username.Text.Length == 0)
                username.SetError(new Java.Lang.String("Hello"), );

            //username.addTextChangedListener(this);
            //txt2.addTextChangedListener(this);
            //txt3.addTextChangedListener(this);

        }


    }
}