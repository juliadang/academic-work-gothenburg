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
    static public class DataManager
    {
        //#region SQLite
        //static string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        //public static string createDatabase()
        //{

        //    try
        //    {
        //        var connection = new SQLiteAsyncConnection(path);

        //        connection.CreateTableAsync<User>();
        //        return "Database created";

        //    }

        //    catch (SQLiteException ex)
        //    {
        //        return ex.Message;
        //    }
        //}
        //public static async System.Threading.Tasks.Task<string> insertUpdateData(User data)
        //{
        //    try
        //    {
        //        var db = new SQLiteAsyncConnection(path);
        //        if (await db.InsertAsync(data) != 0)
        //            await db.UpdateAsync(data);
        //        return "Single data file inserted or updated";
        //    }
        //    catch (SQLiteException ex)
        //    {
        //        return ex.Message;
        //    }
        //}

        //private static int staticfindNumberRecords()
        //{
        //    try
        //    {
        //        var db = new SQLiteAsyncConnection(path);
        //        // this counts all records in the database, it can be slow depending on the size of the database
        //        var count = Convert.ToInt32(db.ExecuteScalarAsync<int>("SELECT Count(*) FROM User"));

        //        // for a non-parameterless query
        //        // var count = db.ExecuteScalar<int>("SELECT Count(*) FROM Person WHERE FirstName="Amy");

        //        return count;
        //    }
        //    catch (SQLiteException)
        //    {
        //        return -1;
        //    }
        //} 
        //#endregion


    }
}