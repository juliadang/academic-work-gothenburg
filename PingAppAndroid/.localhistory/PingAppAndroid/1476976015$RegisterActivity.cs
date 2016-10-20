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
            Button submit = FindViewById<Button>(Resource.Id.buttonSubmit);
        
            submit.Click += async (sender, e) =>
            {

                CreateUserBindingModel user = new CreateUserBindingModel
                {
                    Username = username.Text,
                    Password = password.Text,
                    ConfirmPassword = confirmpassword.Text,
                    Email = email.Text
                };

                HttpClient client = new HttpClient();

                var uri = new Uri("http://pinggothenburg.azurewebsites.net/api/accounts/create");
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    new AlertDialog.Builder(this).SetMessage("Registration completed").Show();
                }
                else 
                {
                    new AlertDialog.Builder(this).SetMessage("Registration failed").Show();
                }

                username.Text = "";
                password.Text = "";
                confirmpassword.Text = "";
                email.Text = "";
            };
        }
    }
}