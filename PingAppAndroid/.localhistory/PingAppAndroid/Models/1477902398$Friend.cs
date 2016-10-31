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
    //Todo: N�r vi h�mtar ut v�nner, h�mta ist�llet ut en Friend ist�llet f�r Username
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