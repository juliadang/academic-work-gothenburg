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
using PingAppAndroid.Adapters;
using PingAppAndroid.Models;

namespace PingAppAndroid.Resources.Fragments
{
    public class NotificationsFragment : Fragment
    {
        ListView mPingList;
        PingListAdapter mPingAdapter;
        Spinner mFilter;

        List<Ping> mPings = new List<Ping>
        {
            new Ping { Time = new DateTime(2016, 10, 4, 13, 30, 0), Sender = new Friend { UserName = "Bo", FirstName = "Bo", LastName = "Johansson" }, Type = 1},
            new Ping { Time = new DateTime(2016, 10, 3, 14, 30, 0), Sender = new Friend { UserName = "Li", FirstName = "Li", LastName = "Andersson" }, Type = 3},
            new Ping { Time = new DateTime(2015, 10, 4, 15, 19, 0), Sender = new Friend { UserName = "An", FirstName = "An", LastName = "Svensson" }, Type = 2}
        };

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Notification, container, false);
            //var sampleTextView = view.FindViewById<TextView>(Resource.Id.sampleTextView);
            //sampleTextView.Text = "sample fragment text";

            //Todo: Find resource by ID
            //mFilter.ItemSelected += mFilter_selectedItem;

            //mPingAdapter = new PingListAdapter(this, mPings);
            //mPingList.Adapter = mPingAdapter;
            return view;
        }

        private void mFilter_selectedItem(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            string choosenIcon = mFilter.SelectedItem.ToString();
            if (choosenIcon.ToLower() == "filter")
            {
                //mPingAdapter = new PingListAdapter(this, mPings);
                //mPingList.Adapter = mPingAdapter;
            }

            else
            {
                List<Ping> filteredpings = mPings.Where(p => p.Type.ToString().Contains(choosenIcon, StringComparison.OrdinalIgnoreCase)).ToList();
                //mPingAdapter = new PingListAdapter(this, filteredpings);
                //mPingList.Adapter = mPingAdapter;
                Console.WriteLine(choosenIcon);
            }
        }
    }
}