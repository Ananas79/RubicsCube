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

namespace App5
{
    [Activity(Label = "ActivityAlgo")]
    public class ActivityAlgo : Activity
    {
        ImageButton[] b = new ImageButton[10];
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Title = "Формулы";
            SetContentView(Resource.Layout.AlgoLayout);
            b[0] = FindViewById<ImageButton>(Resource.Id.imageButtonAlgo1);
            b[1] = FindViewById<ImageButton>(Resource.Id.imageButtonAlgo2);
            b[2] = FindViewById<ImageButton>(Resource.Id.imageButtonAlgo3);
            b[3] = FindViewById<ImageButton>(Resource.Id.imageButtonAlgo4);
            b[4] = FindViewById<ImageButton>(Resource.Id.imageButtonAlgo5);
            b[5] = FindViewById<ImageButton>(Resource.Id.imageButtonAlgo6);
            b[6] = FindViewById<ImageButton>(Resource.Id.imageButtonAlgo7);
            b[7] = FindViewById<ImageButton>(Resource.Id.imageButtonAlgo8);
            b[8] = FindViewById<ImageButton>(Resource.Id.imageButtonAlgo9);
            b[9] = FindViewById<ImageButton>(Resource.Id.imageButtonAlgo10);

            foreach(ImageButton button in b)
            {
                button.Click += button_Click;
            }
            // Create your application here
        }

        private void button_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < b.Length; i++)
            {
                if(sender == b[i])
                {
                    Intent intent = new Intent(this, typeof(Cube));
                    intent.PutExtra("type", "algo");
                    intent.PutExtra("state", CubeSolver.StateToArray(states[i]));
                    intent.PutExtra("formula", formulas[i]);
                    StartActivityForResult(intent, 11);
                    break;
                }
            }
        }

        public override void OnBackPressed()
        {
            Finish();
        }

        const int ORANGE = 0, GREEN = 1, YELLOW = 2, BLUE = 3, RED = 4, WHITE = 5, BROWN = 6;

        int[][][] states = new int[][][] {
             new int[][]{
                new int[] { BROWN, BROWN,  BROWN, BROWN, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE},
                new int[] { BROWN, GREEN, BROWN, GREEN, GREEN, BROWN, GREEN, GREEN, GREEN},
                new int[] { BROWN, BROWN, BROWN, BROWN, BROWN, BROWN, BROWN, ORANGE, BROWN },
                new int[] { BROWN, BROWN, BROWN, BLUE, BLUE, BLUE, BLUE, BLUE, BLUE},
                new int[] { BROWN, BROWN, BROWN, RED, RED, RED, RED, RED, RED},
                new int[] {WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE}
            },
             new int[][]{
                new int[] { BROWN, ORANGE,  BROWN, BROWN, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE},
                new int[] { BROWN, BROWN, BROWN, GREEN, GREEN, BROWN, GREEN, GREEN, GREEN},
                new int[] { BROWN, BROWN, BROWN, BROWN, BROWN, GREEN, BROWN, BROWN, BROWN },
                new int[] { BROWN, BROWN, BROWN, BLUE, BLUE, BLUE, BLUE, BLUE, BLUE},
                new int[] { BROWN, BROWN, BROWN, RED, RED, RED, RED, RED, RED},
                new int[] {WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE}
            },
             new int[][]{
                new int[] { BROWN, BROWN,  BROWN, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE},
                new int[] { BROWN, YELLOW, BROWN, GREEN, GREEN, GREEN, GREEN, GREEN, GREEN},
                new int[] { BROWN, YELLOW, BROWN, BROWN, YELLOW, YELLOW, BROWN, BROWN, BROWN },
                new int[] { BROWN, BROWN, BROWN, BLUE, BLUE, BLUE, BLUE, BLUE, BLUE},
                new int[] { BROWN, YELLOW, BROWN, RED, RED, RED, RED, RED, RED},
                new int[] {WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE}
            },
             new int[][]{
                new int[] { BROWN, YELLOW,  BROWN, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE},
                new int[] { BROWN, BROWN, BROWN, GREEN, GREEN, GREEN, GREEN, GREEN, GREEN},
                new int[] { BROWN, YELLOW, BROWN, BROWN, YELLOW, BROWN, BROWN, YELLOW, BROWN },
                new int[] { BROWN, BROWN, BROWN, BLUE, BLUE, BLUE, BLUE, BLUE, BLUE},
                new int[] { BROWN, YELLOW, BROWN, RED, RED, RED, RED, RED, RED},
                new int[] {WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE}
            },
             new int[][]{
                new int[] { YELLOW, BROWN,  BROWN, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE},
                new int[] { BROWN, BROWN, BROWN, GREEN, GREEN, GREEN, GREEN, GREEN, GREEN},
                new int[] { BROWN, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, BROWN, YELLOW, BROWN },
                new int[] { BROWN, BROWN, BROWN, BLUE, BLUE, BLUE, BLUE, BLUE, BLUE},
                new int[] { BROWN, BROWN, BROWN, RED, RED, RED, RED, RED, RED},
                new int[] {WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE}
            },
             new int[][]{
                new int[] { BROWN, BROWN,  BROWN, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE},
                new int[] { BROWN, BROWN, YELLOW, GREEN, GREEN, GREEN, GREEN, GREEN, GREEN},
                new int[] { BROWN, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, BROWN, YELLOW, BROWN },
                new int[] { BROWN, BROWN, BROWN, BLUE, BLUE, BLUE, BLUE, BLUE, BLUE},
                new int[] { BROWN, BROWN, BROWN, RED, RED, RED, RED, RED, RED},
                new int[] {WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE}
            },
             new int[][]{
                new int[] { ORANGE, GREEN, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE},
                new int[] { GREEN, RED, GREEN, GREEN, GREEN, GREEN, GREEN, GREEN, GREEN},
                new int[] { YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW },
                new int[] { BLUE, BLUE, BLUE, BLUE, BLUE, BLUE, BLUE, BLUE, BLUE},
                new int[] { RED, ORANGE, RED, RED, RED, RED, RED, RED, RED},
                new int[] { WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE}
            },
             new int[][]{
                new int[] { ORANGE, RED, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE},
                new int[] { GREEN, ORANGE, GREEN, GREEN, GREEN, GREEN, GREEN, GREEN, GREEN},
                new int[] { YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW },
                new int[] { BLUE, BLUE, BLUE, BLUE, BLUE, BLUE, BLUE, BLUE, BLUE},
                new int[] { RED, GREEN, RED, RED, RED, RED, RED, RED, RED},
                new int[] { WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE}
            },
             new int[][]{
                new int[] { ORANGE, ORANGE, RED, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE},
                new int[] { RED, GREEN, GREEN, GREEN, GREEN, GREEN, GREEN, GREEN, GREEN},
                new int[] { YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW },
                new int[] { GREEN, BLUE, ORANGE, BLUE, BLUE, BLUE, BLUE, BLUE, BLUE},
                new int[] { BLUE, RED, BLUE, RED, RED, RED, RED, RED, RED},
                new int[] { WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE}
            },
             new int[][]{
                 new int[] { ORANGE, ORANGE, BLUE, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE},
                new int[] { BLUE, GREEN, GREEN, GREEN, GREEN, GREEN, GREEN, GREEN, GREEN},
                new int[] { YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW },
                new int[] { RED, BLUE, RED, BLUE, BLUE, BLUE, BLUE, BLUE, BLUE},
                new int[] { GREEN, RED, ORANGE, RED, RED, RED, RED, RED, RED},
                new int[] { WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE}
            }
        };
    
        string[] formulas = new string[]
        {
            "U R Uz Rz Uz Fz U F",
            "Uz Fz U F U R Uz Rz",
            "L U F Uz Fz Lz",
            "L F U Fz Uz Lz",
            "Fz Lz Bz L F Lz B L2 F R Fz Lz F Rz Fz",
            "R2 D Rz U2 R Dz Rz U2 Rz",
            "R2 U R U Rz Uz Rz Uz Rz U Rz",
            "R Uz R U R U R Uz Rz Uz R2",
            "R U Rz Fz L F Rz Fz Lz F R2 Uz Rz",
            "L Fz L B2 Lz F L B2 L2"
        };
    }
}