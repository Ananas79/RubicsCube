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

        const int WRONG_CHAR = 1, OK = 0, CAPITAL_E = 2, NUM_E = 3, SPECIAL_E = 4;
        private int IsPasswordOk(string password)
        {
            foreach (char c in password)
            {
                if (!"qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890".Contains(c))
                {
                    return WRONG_CHAR;
                }
            }
            if(!password.Any(x => "QWERTYUIOPASDFGHJKLZXCVBNM".Contains(x)))
            {
                return CAPITAL_E;
            }
            if (!password.Any(x => "0123456789".Contains(x)))
            {
                return NUM_E;
            }
            if (!password.Any(x => "!@#$%^&*()+-*/".Contains(x)))
            {
                return SPECIAL_E;
            }
            return OK;
        }

        private int IsLoginOk(string login)
        {
            foreach (char c in login)
            {
                if (!"qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890".Contains(c))
                {
                    return WRONG_CHAR;
                }
            }
            return OK;
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