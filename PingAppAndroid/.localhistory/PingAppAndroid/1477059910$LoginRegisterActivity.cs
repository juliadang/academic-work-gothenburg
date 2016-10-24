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

            Button SignalR = FindViewById<Button>(Resource.Id.buttonSignalR);
            SignalR.Click += testSignalR;

        }

        private async void testSignalR(object sender, EventArgs e)
        {
            var HubCon = new HubConnection("http://pinggothenburg.azurewebsites.net");
            var PingProxy = HubCon.CreateHubProxy("PingHub");

            PingProxy.On<string>("Hey", (val) =>
            {
                RunOnUiThread(() =>
               {
                   //new AlertDialog.Builder(this).SetMessage("funkar!!").Show();
                   //Intent index = new Intent(this, typeof(AppActivity));
                   //StartActivity(index);
                   //Finish();

                   //var intent = new Intent(this, typeof(LoginRegisterActivity));
                   //intent.AddFlags(ActivityFlags.ClearTop);
                   //var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

                   //Notification.Builder builder = new Notification.Builder(this)
                   //    .SetSmallIcon(Resource.Drawable.Icon)
                   //    .SetContentTitle("Ping")
                   //    .SetContentText("Vårat första ping");

                   //Notification notification = builder.Build();

                   //var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);

                   //notificationManager.Notify(0, notification);
               });

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
