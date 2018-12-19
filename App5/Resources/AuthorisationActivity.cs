﻿using System;
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
    [Activity(Label = "AuthorisationActivity")]
    public class AuthorisationActivity : Activity
    {
        EditText login, password;
        Button enter, registration;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AuthLayout);
            // Create your application here
            Title = "Вход";

            login = FindViewById<EditText>(Resource.Id.editText1);
            password = FindViewById<EditText>(Resource.Id.editText2);

            enter = FindViewById<Button>(Resource.Id.button1);
            enter.Click += OnEnter;

            registration = FindViewById<Button>(Resource.Id.buttonReg);
            registration.Click += OnReg;

            is_entered = Intent.GetBooleanExtra("is_entered", false);
        }

        public override void OnBackPressed()
        {
            //base.OnBackPressed();
            Intent intent = new Intent(this, typeof(MenuActivity));
            intent.PutExtra("is_entered", is_entered);
            SetResult(Result.Canceled, intent);
            Finish();
        }

        bool is_entered;

        
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

        
        private void OnEnter(object sender, EventArgs e)
        {
         
        }

        private void OnReg(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ActivityReg));
            intent.PutExtra("login", login.Text);
            StartActivityForResult(intent, 8);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if(requestCode == 8)
                if (data != null)
                {
                    login.Text = data.GetStringExtra("login");
                    password.Text = data.GetStringExtra("password");
                }
        }

        async private void SendPostAndHandleAnswerEnter(string login, string password, bool is_entered)
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

        }

       
    }
}