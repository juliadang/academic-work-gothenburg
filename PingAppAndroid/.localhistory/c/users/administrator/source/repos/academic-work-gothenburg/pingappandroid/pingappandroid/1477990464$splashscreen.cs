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
using PingAppAndroid.Models;

namespace PingAppAndroid
{
    //Set MainLauncher = true makes this Activity Shown First on Running this Application
    //Theme property set the Custom Theme for this Activity
    //No History= true removes the Activity from BackStack when user navigates away from the Activity
    [Activity(Label = "Ping", MainLauncher = true, Theme = "@style/Theme.Splash", NoHistory = true, Icon = "@drawable/icon")]
    public class SplashScreen : Activity
    {
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);



            ISharedPreferences prefs = Application.Context.GetSharedPreferences("userInfo", FileCreationMode.Private);
            string userName = prefs.GetString("username", System.String.Empty);
            string password = prefs.GetString("password", String.Empty);

            if (userName == String.Empty || password == String.Empty)
            {
                Intent intent = new Intent(this, typeof(LoginRegisterActivity));
                this.StartActivity(intent);
            }
            else
            {
                bool succeeded;
                succeeded = await DataManager.SignInAsync(userName, password);

                if (succeeded)
                {
                    DataManager.GetAllFriendsAsync();
                    DataManager.GetPingsAsync();
                    Intent index = new Intent(this, typeof(AppActivity));
                    StartActivity(index);
                    Finish();
                }
                else
                {
                    new AlertDialog.Builder(this).SetMessage("Login failed").Show();
                    Intent intent = new Intent(this, typeof(LoginRegisterActivity));
                    this.StartActivity(intent);
                }

            }

            //Display Splash Screen for 4 Sec
            Thread.Sleep(4000);
            //Start Activity1 Activity
            StartActivity(typeof(LoginRegisterActivity));
        }
    }
}