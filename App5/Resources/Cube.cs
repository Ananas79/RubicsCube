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
        CubeAnimationView cb;
        LinearLayout mLayout;
        const int ORANGE = 0, GREEN = 1, YELLOW = 2, BLUE = 3, RED = 4, WHITE = 5, BROWN = 6;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Title = "Сборка";
            SetContentView(Resource.Layout.CubeActivityLayout);
            mLayout = FindViewById<LinearLayout>(Resource.Id.cubeLayout);
            cb = new CubeAnimationView(this);

            string type = Intent.GetStringExtra("type");
            switch (type)
            {
                case "photo":
                    cb.SetState(Intent.GetIntArrayExtra("state"));
                    cb.SolveCurrentState();
                    break;
                case "server":
                    var cs = new CubeSolver();
                    
                    string f = CubeSolver.Backward(CubeSolver.Unzip(Intent.GetStringExtra("server")));
                    cs.DoFormula(f);
                    cb.SetState(cs.GetState());
                    cb.SolveCurrentState();
                    break;
                case "algo":
                    
                    cb.SetState(Intent.GetIntArrayExtra("state"));
                    cb.DoFormulas(new string[1] { Intent.GetStringExtra("formula") });
                    break;
            }

            //cb.SetState(Intent.GetIntArrayExtra("state"));

            cb.LayoutParameters = new ViewGroup.LayoutParams
            (
                    ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.MatchParent
            );
            mLayout.AddView(cb);

           
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            
            this.MenuInflater.Inflate(Resource.Menu.menu1, menu);
            if (menu != null && Intent.GetStringExtra("type") == "algo")
                menu.SetGroupVisible(Resource.Id.main_menu_group, false);
            return base.OnCreateOptionsMenu(menu);
        }
        

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.share:
                    SendPostFormula(CubeSolver.Zip(String.Join(" ", new CubeSolver(cb.state).GetSolution())));
                    return true;
            }
            return base.OnContextItemSelected(item);
        }

        async private void SendPostFormula(string data_value)
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                { "data" , data_value }
            };
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://bayan79.pythonanywhere.com/share");
            request.Method = HttpMethod.Post;

            HttpContent content = new FormUrlEncodedContent(data);
            request.Content = content;

            
            HttpResponseMessage response = await client.PostAsync(request.RequestUri, content);
            
            string answer = await response.Content.ReadAsStringAsync();
            
        }

        public override void OnBackPressed()
        {
            Finish();
        }
    }
}