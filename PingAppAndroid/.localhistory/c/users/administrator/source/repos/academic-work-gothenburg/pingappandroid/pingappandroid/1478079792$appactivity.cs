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
using static PingAppAndroid.Services.PingService;
using PingAppAndroid.Services;

namespace PingAppAndroid
{
    [Activity(Label = "Ping", Icon = "@drawable/icon")]
    public class AppActivity : Activity
    {
        public bool isBound = false;
        public PingServiceBinder binder;
        PingServiceConnection pingServiceConnection;
        PingReceiver pingReceiver;
        Intent pingServiceIntent;
       

        static ISharedPreferences mPrefs = Application.Context.GetSharedPreferences("gcmToken", FileCreationMode.Private);
        ISharedPreferencesEditor mEditor = mPrefs.Edit();

        static ISharedPreferences mPrefsUserInfo = Application.Context.GetSharedPreferences("userInfo", FileCreationMode.Private);
        ISharedPreferencesEditor mEditorUserInfo = mPrefsUserInfo.Edit();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            pingServiceIntent = new Intent("com.xamarin.PingService");
            pingReceiver = new PingReceiver();

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
                        string username = mPrefsUserInfo.GetString("username", "");
                        pubSub.Subscribe(token, "/topics/" + username, null);
                        Log.Debug("Success", "Subscribed to topic: " + username);
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

        void SchedulePingUpdates()
        {
            if (!IsAlarmSet())
            {
                var alarm = (AlarmManager)GetSystemService(Context.AlarmService);

                var pendingServiceIntent = PendingIntent.GetService(this, 0, pingServiceIntent, PendingIntentFlags.CancelCurrent);
                alarm.SetRepeating(AlarmType.Rtc, 0, AlarmManager.IntervalHalfHour, pendingServiceIntent);
            }
        }

        bool IsAlarmSet()
        {
            return PendingIntent.GetBroadcast(this, 0, pingServiceIntent, PendingIntentFlags.NoCreate) != null;
        }

        public void GetPings()
        {
            if (isBound)
            {
                DataManager.mPingList = binder.GetPingService().GetPings();
                //RunOnUiThread(() => {
                //var stocks = binder.GetStockService().GetStocks();

                //ListAdapter = new ArrayAdapter<PingNotification>(
                //        this,
                //        Resource.Layout.PingItemView,
                //        stocks
                //    );
                //});
            }
        }
    }
}