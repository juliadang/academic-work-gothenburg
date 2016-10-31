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
using PingAppAndroid.Models;

namespace PingAppAndroid.Adapters
{
    class PingListAdapter : BaseAdapter<Models.PingNotification>
    {
        public List<Models.Notification> Pings;
        private Context Context;
        public PingListAdapter(Activity context, List<Models.Notification> pings) : base() {
            Context = context;
            Pings = pings;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override Notification this[int position]
        {
            get { return Pings[position]; }
        }
        public override int Count
        {
            get { return Pings.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = LayoutInflater.From(Context).Inflate(Resource.Layout.PingRowView, null, false);

            TextView senderName = view.FindViewById<TextView>(Resource.Id.PingUserName);
            senderName.Text = Pings[position].Sender.UserName;

            TextView date = view.FindViewById<TextView>(Resource.Id.PingTime);
            if (DateTime.Now.Month - Pings[position].Time.Month > 0)
            {
                date.Text = "pingade dig för " + (DateTime.Now.Month - Pings[position].Time.Month) + " månader sen";
            }
            else if (DateTime.Now.Day - Pings[position].Time.Day > 0)
            {
                date.Text = "pingade dig för " + (DateTime.Now.Day - Pings[position].Time.Day) + " dagar sen";
            }
            else if (DateTime.Now.Hour - Pings[position].Time.Hour > 0)
            {
                date.Text = "pingade dig för " + (DateTime.Now.Hour - Pings[position].Time.Hour) + " timmar sen";
            }
            else if (DateTime.Now.Minute - Pings[position].Time.Minute >= 0)
            {
                date.Text = "pingade dig för " + (DateTime.Now.Minute - Pings[position].Time.Minute) + " minuter sen";
            }

            TextView pingType = view.FindViewById<TextView>(Resource.Id.PingIcon);
            pingType.Text = Pings[position].Type.ToString();
            return view;
        }
    }
}