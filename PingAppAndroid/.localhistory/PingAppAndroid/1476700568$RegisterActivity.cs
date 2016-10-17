using System;

using Android.App;
using Android.OS;
using Android.Widget;
using System.Net;
using System.Net.Http;
using SimpleWebApi.Models;
using Org.Apache.Http.Client.Methods;
using Newtonsoft.Json;
using System.Text;

namespace PingAppAndroid
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Register);

            EditText username = FindViewById<EditText>(Resource.Id.username);
            EditText password = FindViewById<EditText>(Resource.Id.password);
            EditText confirmpassword = FindViewById<EditText>(Resource.Id.confirmpassword);
            EditText email = FindViewById<EditText>(Resource.Id.email);
            Button submit = FindViewById<Button>(Resource.Id.submit);

            // String[] permissions = { Manifest.Permission.WriteExternalStorage };
            //    RequestPermissions(permissions, WriteRequestCode);

            if (username.Text.Length == 0)
                username.SetError(new Java.Lang.String("Username Required"), GetDrawable(Resource.Drawable.Icon));

            submit.Click += (sender, e) =>
            {

                CreateUserBindingModel user = new CreateUserBindingModel
                {
                    Username = username.Text,
                    Password = password.Text,
                    ConfirmPassword = confirmpassword.Text,
                    Email = email.Text
                };

                HttpClient client = new HttpClient();
                HttpPost postClient = new HttpPost("http://192.168.2.119:11014/api/account/create");


                var uri = new Uri("http://192.168.2.119:11014/api/account/create");
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await client.PostAsync(uri, content);
                }
  ...

  if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"             TodoItem successfully saved.");

                }
  ...
}


        //Uri uri = new Uri("http://192.168.2.118:11014/api/account");
        //NameValueCollection parameters = new NameValueCollection();

            //var response = await client.GetAsync(uri)

            //parameters.Add("name", username.Text);
            //parameters.Add("password", password.Text);
            ////parameters.Add("E-mail", email.Text);

            //client.UploadValuesCompleted += client_UploadValuesCompleted;
            ////client.UploadValuesAsync(uri, parameters);
            //client.

            //username.Text = "";
            //password.Text = "";
            //email.Text = "";
            //new AlertDialog.Builder(this).SetMessage(uri.ToString()).Show();
        };
    }

    void client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
    {
        //Activity.RunOnUiThread(() =>
        //{
        //    var id = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(e.Result)); //Get the data echo backed from PHP

        //if (OnCreateContact != null)
        //{
        //    //Broadcast event
        //    OnCreateContact.Invoke(this, new CreateContactEventArgs(txtName.Text, txtNumber.Text));
        //}

        //mProgressBar.Visibility = ViewStates.Invisible;
        //this.Dismiss();
        //});

    }
}
public class CreateContactEventArgs : EventArgs
{
    public int ID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public bool IsCreated { get; set; }

    public CreateContactEventArgs(int id, string username, string password, string email, bool isCreated)
    {
        ID = id;
        Username = username;
        Password = password;
        Email = email;
        IsCreated = isCreated;
    }
}
}