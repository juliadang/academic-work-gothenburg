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
        List<PingNotification> mPings;

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

            mPings = DataManager.GetPings();
            mPingList = view.FindViewById<ListView>(Resource.Id.RecentPings);
            mPingAdapter = new PingListAdapter(Activity, mPings);
            mPingList.Adapter = mPingAdapter;
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
                List<Models.PingNotification> filteredpings = mPings.Where(p => p.Type.ToString().Contains(choosenIcon, StringComparison.OrdinalIgnoreCase)).ToList();
                //mPingAdapter = new PingListAdapter(this, filteredpings);
                //mPingList.Adapter = mPingAdapter;
                Console.WriteLine(choosenIcon);
            }
        }
    }
}