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

namespace PingAppAndroid.Models
{
    //Todo: När vi hämtar ut vänner, hämta istället ut en Friend istället för Username
    class Friend
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //Todo: Vilka images?
        public string ImageSource { get; set; }
    }
}