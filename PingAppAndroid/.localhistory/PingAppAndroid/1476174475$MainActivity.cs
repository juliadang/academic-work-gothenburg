using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using Android.Content.PM;
using Java.Security;

namespace PingAppAndroid
{
    [Activity(Label = "Ping", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
           
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //PackageInfo info = this.PackageManager.GetPackageInfo("PingAppAndroid", PackageInfoFlags.Signatures);

            //foreach (Android.Content.PM.Signature signature in info.Signatures)
            //{
            //    MessageDigest md = MessageDigest.GetInstance("SHA");
            //    md.Update(signature.ToByteArray());

            //    string keyhash = Convert.ToBase64String(md.Digest());
            //    Console.WriteLine("KeyHash", keyhash);
            //}

            Button Login = FindViewById<Button>(Resource.Id.button1);
            Login.Click += redirectToApp;
        }

        private void redirectToApp(object sender, EventArgs e)
        {
            Intent index = new Intent(this, typeof(AppActivity));
            StartActivity(index);

            //SetContentView(Resource.Layout.Index);
        }
    }
}

