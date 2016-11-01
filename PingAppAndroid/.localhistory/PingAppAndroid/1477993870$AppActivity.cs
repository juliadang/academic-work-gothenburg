using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PingAppAndroid.Models;
using PingAppAndroid.Adapters;
using Android.Text;
using PingAppAndroid.Resources.Fragments;
using ActionBar = Android.App.ActionBar;
using Android.Gms.Common;
using Android.Gms.Gcm;
using Android.Gms.Gcm.Iid;
using Android.Util;
using Android.Nfc;
using Java.IO;

namespace PingAppAndroid
{
    [Activity(Label = "Ping", Icon = "@drawable/icon")]
    public class AppActivity : Activity
    {
        static ISharedPreferences mPrefs = Application.Context.GetSharedPreferences("gcmToken", FileCreationMode.Private);
        ISharedPreferencesEditor mEditor = mPrefs.Edit();

        static ISharedPreferences mPrefsUserInfo = Application.Context.GetSharedPreferences("userInfo", FileCreationMode.Private);
        ISharedPreferencesEditor mEditorUserInfo = mPrefsUserInfo.Edit();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FrameLayout);

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            AddTab("Notifications", new NotificationsFragment());
            AddTab("Profile", new ProfileFragment());
            AddTab("Friends", new FriendsFragment());

            if (IsPlayServicesAvailable())
            {
                var intent = new Intent(this, typeof(RegistrationIntentService));
                StartService(intent);
            }

            //Use this because you can't subscribe from the main thread
            ThreadPool.QueueUserWorkItem(o =>
            {
                var pubSub = GcmPubSub.GetInstance(Application.Context);
                InstanceID instanceID = InstanceID.GetInstance(Application.Context);
                String token = null;
                try
                {
                    token = instanceID.GetToken(
                      "884131269913", GoogleCloudMessaging.InstanceIdScope, null); //Todo: Hide token...

                    if (token != null)
                    {
                        pubSub.Subscribe(token, "/topics/" + "Oliver", null); //Todo: Ta in username istället för hårdkodad sträng, spara ner username och password
                        Log.Debug("Success", "Subscribed to topic: " + "Oliver");
                    }
                    else
                    {
                        Log.Debug("Error", "error: gcm registration id is null");
                    }
                }
                catch (IOException e)
                {

                }

            });

            //SavedInstanceState gör så att man kan se alla tabbar
            if (savedInstanceState != null) ActionBar.SelectTab(ActionBar.GetTabAt(savedInstanceState.GetInt("tab")));
        }

        void AddTab(string tabText, Fragment view)
        {
            var tab = this.ActionBar.NewTab();
            tab.SetText(tabText);

            // must set event handler before adding tab

            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                var fragment = this.FragmentManager.FindFragmentById(Resource.Id.frameLayout);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);
                e.FragmentTransaction.Add(Resource.Id.frameLayout, view);
            };
            tab.TabUnselected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                e.FragmentTransaction.Remove(view);
            };

            ActionBar.AddTab(tab);
        }
        private bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode)) { }
                //labelLogin.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
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

    }
}