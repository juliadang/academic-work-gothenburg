using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using Android.Content.PM;
using Java.Security;
using System.Runtime.CompilerServices;
using PingAppAndroid.Models;
using System.Net.Http;
using Android.Gms.Common;
using Android.Util;

namespace PingAppAndroid
{
    [Activity(Label = "Ping", MainLauncher = false, Icon = "@drawable/icon" )]
    public class LoginRegisterActivity : Activity
    {
        private static readonly int ButtonClickNotificationId = 1000;
        int count = 0;
        TextView labelLogin;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.LoginRegister);

            labelLogin = FindViewById<TextView>(Resource.Id.labelLogin);

            if (IsPlayServicesAvailable())
            {
                var intent = new Intent(this, typeof(RegistrationIntentService));
                StartService(intent);
            }

            Button Login = FindViewById<Button>(Resource.Id.buttonLogIn);
            Login.Click += login;

            Button Register = FindViewById<Button>(Resource.Id.buttonRegister);
            Register.Click += registerUser;
        }

        private bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    labelLogin.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    new AlertDialog.Builder(this).SetMessage("Sorry, this device is not supported.").Show();
                    Finish();
                }
                return false;
            }
            else
            {
                return true;
            }
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
                Finish();
            }
            else
                new AlertDialog.Builder(this).SetMessage("Login failed").Show();

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
