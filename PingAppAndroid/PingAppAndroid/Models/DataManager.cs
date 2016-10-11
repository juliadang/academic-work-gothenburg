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
using SQLite;

namespace PingAppAndroid.Models
{
    public class DataManager
    {
        public string createDatabase()
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            try
            {
                var connection = new SQLiteAsyncConnection(path);

                connection.CreateTableAsync<User>();
                return "Database created";
                            
            }

            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }
    }
}