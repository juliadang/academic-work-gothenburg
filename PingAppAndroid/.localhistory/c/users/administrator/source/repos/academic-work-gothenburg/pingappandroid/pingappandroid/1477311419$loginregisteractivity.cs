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
using Microsoft.AspNet.SignalR.Client;
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;
using Android.Support.V4.App;
using Android.Gms.Common;
using Android.Util;

namespace PingAppAndroid
{
    [Activity(Label = "Ping", MainLauncher = true, Icon = "@drawable/icon")]
    public class LoginRegisterActivity : Activity
    {
        private static readonly int ButtonClickNotificationId = 1000;
        int count = 0;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

        

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.LoginRegister);

            msgText = FindViewById<TextView>(Resource.Id.msgText);

            IsPlayServicesAvailable();

            Button Login = FindViewById<Button>(Resource.Id.buttonLogIn);
            Login.Click += login;

            Button Register = FindViewById<Button>(Resource.Id.buttonRegister);
            Register.Click += registerUser;

            Button SignalR = FindViewById<Button>(Resource.Id.buttonSignalR);
            SignalR.Click += testSignalR;

        }

        private async void testSignalR(object sender, EventArgs e)
        {

            #region SignalR
            var HubCon = new HubConnection("http://pinggothenburg.azurewebsites.net");
            var PingProxy = HubCon.CreateHubProxy("PingHub");

            PingProxy.On<string>("Hey", (val) =>
            {
                RunOnUiThread(() =>
               {
               });

                NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
                    .SetContentTitle("Ping")
                    .SetContentText("Vårat första ping")
                    .SetSmallIcon(Resource.Drawable.Icon);

                Notification notification = builder.Build();

                var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);

                notificationManager.Notify(0, notification);

            });
            try
            {
                await HubCon.Start();
                await PingProxy.Invoke("Hello", "Hej, det funkar");
            }
            catch (Exception)
            {
                new AlertDialog.Builder(this).SetMessage("Connection failed").Show();
            }
            #endregion

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
