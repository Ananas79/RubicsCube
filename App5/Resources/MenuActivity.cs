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
using Android.Bluetooth;
using System.Net.Http;

namespace App5
{
    [Activity(Label = "App6", MainLauncher = true)]
    class MenuActivity : Activity
    {
        ImageButton cam, login, load, algo;
        LinearLayout layout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Title = "Меню";
            SetContentView(Resource.Layout.MenuLayout);

            layout = FindViewById<LinearLayout>(Resource.Id.menuLayout);
            layout.SetGravity(GravityFlags.Center);

            cam = FindViewById<ImageButton>(Resource.Id.imageButton1);
            cam.Click += OnButtonClickPhoto;

            login = FindViewById<ImageButton>(Resource.Id.imageButton2);
            login.Click += OnButtonClickLogin;

            load = FindViewById<ImageButton>(Resource.Id.imageButton3);
            load.Click += OnButtonClickLoad;

            algo = FindViewById<ImageButton>(Resource.Id.imageButton4);
            algo.Click += OnAlgo;
            

            //b = FindViewById<Button>(Resource.Id.button1);
            //b.Click += OnButtonClickPhoto;
        }

        private void OnAlgo(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ActivityAlgo));
            /*Intent intent = new Intent(this, typeof(Cube));
            intent.PutExtra("type", "photo");*/
            StartActivity(intent);
        }

        private void OnButtonClickPhoto(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            /*Intent intent = new Intent(this, typeof(Cube));
            intent.PutExtra("type", "photo");*/
            StartActivityForResult(intent, 3);
        }

        bool is_entered = false;

        private void OnButtonClickLogin(object sender, EventArgs e)
        {
            //Intent intent = new Intent(this, typeof(MainActivity));
            Intent intent = new Intent(this, typeof(AuthorisationActivity));
            intent.PutExtra("is_entered", is_entered);
            StartActivityForResult(intent, 4);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if(requestCode == 4)
            {
                is_entered = data.GetBooleanExtra("is_entered", false);
            }
        }

        private void OnButtonClickLoad(object sender, EventArgs e)
        {
            GetFormulaAndShow();
           
        }
    }
}