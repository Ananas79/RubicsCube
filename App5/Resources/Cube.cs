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
    [Activity(Label = "Cube")]
    public class Cube : Activity
    {
        const int ORANGE = 0, GREEN = 1, YELLOW = 2, BLUE = 3, RED = 4, WHITE = 5, BROWN = 6;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Title = "Сборка";
  
            //cb.SetState(Intent.GetIntArrayExtra("state"));


           
        }

        public override void OnBackPressed()
        {
            Finish();
        }
    }
}