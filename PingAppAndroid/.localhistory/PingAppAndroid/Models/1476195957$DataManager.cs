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
        string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public string createDatabase(string path)
        {

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
        public async System.Threading.Tasks.Task<string> insertUpdateData(User data, string path)
        {
            try
            {
                var db = new SQLiteAsyncConnection(path);
                if (await db.InsertAsync(data) != 0)
                    await db.UpdateAsync(data);
                return "Single data file inserted or updated";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }
    }
}