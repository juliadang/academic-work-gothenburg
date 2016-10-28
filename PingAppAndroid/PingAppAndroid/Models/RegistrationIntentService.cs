using System;
using Android.App;
using Android.Content;
using Android.Util;
using Android.Gms.Gcm.Iid;
using Android.Gms.Gcm;

namespace PingAppAndroid.Models
{
    [Service(Exported = false)]
    class RegistrationIntentService : IntentService
    {
        static ISharedPreferences mPrefs = Application.Context.GetSharedPreferences("gcmToken", FileCreationMode.Private);
        static ISharedPreferencesEditor mEditor = mPrefs.Edit();
        static object locker = new object();

        public RegistrationIntentService() : base("RegistrationIntentService") { }

        protected override void OnHandleIntent(Intent intent)
        {
            try
            {
                Log.Info("RegistrationIntentService", "Calling InstanceID.GetToken");
                lock (locker)
                {
                    var instanceID = InstanceID.GetInstance(this);
                    var token = instanceID.GetToken(
                        "884131269913", GoogleCloudMessaging.InstanceIdScope, null);

                    Log.Info("RegistrationIntentService", "GCM Registration Token: " + token);
                    SendRegistrationToAppServer(token);

                    mEditor.PutString("gcmToken", token);
                    mEditor.Apply();
                    Subscribe("global");
                }
            }
            catch (Exception e)
            {
                Log.Debug("RegistrationIntentService", "Failed to get a registration token");
                return;
            }
        }

        void SendRegistrationToAppServer(string token)
        {
            // Add custom implementation here as needed.
        }

        void Subscribe(string topic)
        {
            var pubSub = GcmPubSub.GetInstance(Application.Context);
            pubSub.Subscribe(mPrefs.GetString("gcmToken", ""), "/topics/" + topic, null);
        }
    }
}