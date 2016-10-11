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

//[assembly: Dependency (typeof (SQLite_Android))]
//// ...
//public class SQLite_Android : SQLite3
//{
//    public SQLite_Android() { }
//    public SQLite.SQLiteConnection GetConnection()
//    {
//        var sqliteFilename = "TodoSQLite.db3";
//        string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
//        var path = Path.Combine(documentsPath, sqliteFilename);
//        // Create the connection
//        var conn = new SQLite.SQLiteConnection(path);
//        // Return the database connection
//        return conn;
//    }
//}
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

            //Alert-check for database created
            //new AlertDialog.Builder(this).SetMessage(dm.createDatabase()).Show();

            Button Login = FindViewById<Button>(Resource.Id.button1);
            Login.Click += redirectToApp;

            Button Register = FindViewById<Button>(Resource.Id.register);
            Register.Click += registerUser;
        }

        private void redirectToApp(object sender, EventArgs e)
        {
            Intent index = new Intent(this, typeof(AppActivity));
            StartActivity(index);

            //SetContentView(Resource.Layout.Index);
        }

        private void registerUser(object sender, EventArgs e)
        {
            Intent register = new Intent(this, typeof(RegisterActivity));
            StartActivity(register);
        }
    }
}
