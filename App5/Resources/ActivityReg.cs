using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using Java.Net;
using System.Net;

namespace App5
{
    [Activity(Label = "ActivityReg")]
    public class ActivityReg : Activity
    {
        EditText login, password, repassword;
        Button registration;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegLayout);
            // Create your application here
            Title = "Регистрация";

            login = FindViewById<EditText>(Resource.Id.editTextLoginReg);
            password = FindViewById<EditText>(Resource.Id.editTextPassReg);
            repassword = FindViewById<EditText>(Resource.Id.editTextRePassReg);

            registration = FindViewById<Button>(Resource.Id.buttonRegReg);
            registration.Click += OnReg;
        }

        public override void OnBackPressed()
        {
            //base.OnBackPressed();
            Intent intent = new Intent(this, typeof(AuthorisationActivity));
            intent.PutExtra("login", login.Text);
            SetResult(Result.Canceled, intent);
            Finish();
        }


        private void OnReg(object sender, EventArgs e)
        {

        }

        async private void SendPostAndHandleAnswerReg(string login, string password)
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                { "username" , login },
                { "password" , password }
            };
         
        }


    }
}