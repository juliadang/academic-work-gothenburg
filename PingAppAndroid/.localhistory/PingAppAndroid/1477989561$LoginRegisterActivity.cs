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
    [Activity(Label = "Ping", MainLauncher = false, Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo.Light.NoActionBar" )]
    public class LoginRegisterActivity : Activity
    {
        TextView labelLogin;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.LoginRegister);

            labelLogin = FindViewById<TextView>(Resource.Id.labelLogin);

            Button Login = FindViewById<Button>(Resource.Id.buttonLogIn);
            Login.Click += login;

            Button Register = FindViewById<Button>(Resource.Id.buttonRegister);
            Register.Click += registerUser;
        }

        private async void login(object sender, EventArgs e)
        {
            ISharedPreferences mPrefs = Application.Context.GetSharedPreferences("userInfo", FileCreationMode.Private);
            ISharedPreferencesEditor mEditor = mPrefs.Edit();

            EditText userName = FindViewById<EditText>(Resource.Id.userNameMain);
            EditText password = FindViewById<EditText>(Resource.Id.passwordMain);
            CheckBox rememberMe = FindViewById<CheckBox>(Resource.Id.checkBoxRememberMe);

            if (rememberMe.Checked)
            {
                mEditor.PutString("password", password.Text);

            }
            bool succeeded;
            succeeded = await DataManager.SignInAsync(userName.Text, password.Text);

            if (succeeded)
            {
                DataManager.GetAllFriendsAsync();
                DataManager.GetPingsAsync();
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
