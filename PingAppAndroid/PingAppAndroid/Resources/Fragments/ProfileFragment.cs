using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace PingAppAndroid.Resources.Fragments
{
    public class ProfileFragment : Fragment
    {
        static ISharedPreferences mPrefsUserInfo = Application.Context.GetSharedPreferences("userInfo", FileCreationMode.Private);
        static ISharedPreferences mPrefs = Application.Context.GetSharedPreferences("token", FileCreationMode.Private);
        static ISharedPreferencesEditor mEditorUserInfo = mPrefsUserInfo.Edit();
        static ISharedPreferencesEditor mEditor = mPrefs.Edit();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Profile, container, false);

            Button signOut = view.FindViewById<Button>(Resource.Id.buttonSignOut);

            signOut.Click += SignOutUser;

            //L�gg till funktioner f�r knapparna Settings m.m.
            //var sampleTextView = view.FindViewById<TextView>(Resource.Id.sampleTextView);
            //sampleTextView.Text = "sample fragment text";

            return view;
        }

        private void SignOutUser(object sender, EventArgs e)
        {
            mEditor.Clear();
            mEditor.Apply();
            mEditorUserInfo.Clear();
            mEditorUserInfo.Apply();

            Intent intent = new Intent(Application.Context, typeof(LoginRegisterActivity));
            this.StartActivity(intent);
            Activity.Finish();
        }
    }
}