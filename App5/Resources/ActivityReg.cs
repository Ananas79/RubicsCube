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

        const int MAX_LENGTH = 2, WRONG_CHAR = 1, OK = 0;
        private int IsOk(string login)
        {
            if (login.Length < 6 || login.Length > 20)
                return MAX_LENGTH;
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

            switch (IsOk(password.Text))
            {
                case MAX_LENGTH:
                    Title = "Длина пароля : от 6 до 20";
                    return;
                case WRONG_CHAR:
                    Title = "Символы пароля: a-z, A-Z, 0-9";
                    return;
            }
            switch (IsOk(login.Text))
            {
                case MAX_LENGTH:
                    Title = "Длина логина : от 6 до 20";
                    break;
                case WRONG_CHAR:
                    Title = "Символы логина: a-z, A-Z, 0-9";
                    break;
                case OK:
                    if (password.Text == repassword.Text)
                    {
                        Title = "Регистрация";
                        SendPostAndHandleAnswerReg(login.Text, password.Text);
                    }
                    else
                        Title = "Пароли не совпадают";
                    break;
            }
        }
        async private void SendPostAndHandleAnswerReg(string login, string password)
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                { "username" , login },
                { "password" , password }
            };
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://bayan79.pythonanywhere.com/reg");
            request.Method = HttpMethod.Post;

            HttpContent content = new FormUrlEncodedContent(data);
            request.Content = content;


            HttpResponseMessage response = await client.PostAsync(request.RequestUri, content);
            string answer = await response.Content.ReadAsStringAsync();

            while (answer == "") ;

            switch (answer)
            {
                case "Exists":
                    Title = $"{login} уже зарегистрирован";
                    break;
                case "Reged":
                    Title = $"Новый пользователь: {login}!";
                    break;
            }
        }


    }
}