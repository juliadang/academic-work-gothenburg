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
using static Android.Views.View;

namespace PingAppAndroid.Models
{
    class SetOnFocusChangeListener : IOnTouchListener
    {
        public IntPtr Handle
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            throw new NotImplementedException();
        }
        public void onFocusChange(EditText textfield)
        {
            String myText = textfield.Text;
            if (myText.Length <= 0)
            {
                Console.WriteLine("verkar funka");
            }

        }
    }
}