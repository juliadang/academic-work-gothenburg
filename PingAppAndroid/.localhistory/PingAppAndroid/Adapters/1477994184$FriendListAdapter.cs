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
    class FriendListAdapter : BaseAdapter<string>
    {
        //Todo: Return list of friends (pga av profilbilder)
        public List<string> Friends;
        private Context Context;
        public FriendListAdapter(Activity context, List<string> friends) : base()
        {
            Context = context;
            Friends = friends;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override string this[int position]
        {
            get { return Friends[position]; }
        }
        public override int Count
        {
            get { return Friends.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = LayoutInflater.From(Context).Inflate(Resource.Layout.FriendRowView, null, false);

            TextView txtName = view.FindViewById<TextView>(Resource.Id.TxtUserName);
            txtName.Text = Friends[position];
           // ImageView profileImg = view.FindViewById<ImageView>(Resource.Id.profileImg);
           // profileImg.SetBackgroundResource(Resource.Drawable.cheese_5);
            //Todo: L�gg till bild?
            //Todo: Anpassa search med knapp ikoner f�r l�gg till/block/etc.
            return view;
        }
    }
}