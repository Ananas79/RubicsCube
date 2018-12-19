using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App5
{
    [Activity(Label = "ActivityShare")]
    public class ActivityShare : Activity
    {
        EditText reciever;
        Button button;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ShareLayout);
            // Create your application here

            reciever = FindViewById<EditText>(Resource.Id.editText3);
            button = FindViewById<Button>(Resource.Id.button3);
            button.Click += OnClick;

            is_entered = Intent.GetBooleanExtra("is_entered", false);
            moves = Intent.GetStringArrayListExtra("moves");
        }

        private void OnClick(object sender, EventArgs e)
        {
            //SendPostAndHandleAnswer();
        }

        bool is_entered;
        IList<string> moves;

        async private void SendPostAndHandleAnswer(string login, string password, bool is_entered)
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                { "username" , login },
                { "password" , password },
                { "is_entered", is_entered.ToString() }
            };
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://bayan79.pythonanywhere.com/login");
            request.Method = HttpMethod.Post;

            HttpContent content = new FormUrlEncodedContent(data);
            request.Content = content;


            HttpResponseMessage response = await client.PostAsync(request.RequestUri, content);
            string answer = await response.Content.ReadAsStringAsync();

            Title = answer;

            switch (answer)
            {
                case "Already entered":
                    break;
                case "Enter allowed":
                    is_entered = true;
                    break;
                case "Wrong password":
                    break;
                case "No user":
                    break;
            }
        }
    }
}