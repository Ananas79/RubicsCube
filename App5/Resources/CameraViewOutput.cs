using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


namespace App5
{
    public class CameraViewOutput : ImageView
    {
        public event EventHandler<Side> SideGot;
        public int CubeSize { get; set; }
        Paint p = new Paint()
        {
            Color = Color.Black,
            StrokeWidth = 3
        };

        /*
        float[][] lines;
        int size = 600;
        Point uprightcorner;
        int check = 0;
        public Activity a { get; set; }
        
        
        */
        public CameraViewOutput(Context context) : base(context)
        {
            Visibility = ViewStates.Visible;
            Activated = true;
            Background = new Android.Graphics.Drawables.ColorDrawable(Color.Aqua);
            
        }
        
        public void SaveCurSide()
        {
            state[curSide.Color] = curSide.Colors.ToArray();
        }

        public bool IsSideFilled(int side)
        {
            return !state[side].Contains(-1); 
        }
         
        public bool IsAllSidesFilled
        {
            get
            {
                return state.Where(x => { return x.Contains(-1); }).Count() == 0;
            }
        }

        int[][] state = new int[][]
        {
            new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1 },
            new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1 },
            new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1 },
            new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1 },
            new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1 },
            new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1 }
        };

        public int[][] GetState()
        {
            int[][] state = new int[][]
            {
                new int[9],
                new int[9],
                new int[9],
                new int[9],
                new int[9],
                new int[9]
            };
            for (int i = 0; i < this.state.Length; i++)
            {
                for (int j = 0; j < this.state[i].Length; j++)
                {
                    state[i][j] = this.state[i][j];
                }
            }
            return state;
        }

        int colorPixel;
        int[] bufRGB = { 0, 0, 0 };
        int bufColor;
        float[] bufHSV = { 0, 0, 0 };
        int xc, yc;
        float koef = 5;
        float scaledCubeSize;
        float width, height;
        const int ORANGE = 0, GREEN = 1, YELLOW = 2, BLUE = 3, RED = 4, WHITE = 5;
        private Color _getColorByIndex(int i)
        {
            switch (i)
            {
                case WHITE: return Color.White;
                case RED: return Color.Red;
                case GREEN: return Color.Green;
                case YELLOW: return Color.Yellow;
                case BLUE: return Color.Blue;
                case ORANGE: return Color.Orange;
            }
            return Color.Black;
        }
        //Color[] colors = { Color.Blue, Color.Yellow, Color.Orange, Color.Green, Color.White, Color.Red, Color.Black};


        private Side curSide = new Side();
        
        public void DrawBitmap(Bitmap bm)
        {
           
            scaledCubeSize = CubeSize / koef;
            width = bm.Width / koef;
            height = bm.Height / koef;
            Matrix matrix = new Matrix();
            matrix.PostRotate(90);
            Bitmap bm2 = bm.Copy(Bitmap.Config.Argb8888, true);
            Bitmap scaled = Bitmap.CreateScaledBitmap(bm2, (int)width, (int)height, true);
            Bitmap rotated = Bitmap.CreateBitmap(scaled, 0, 0, (int)width, (int)height, matrix, true);
            //bm = Bitmap.CreateBitmap(bm, 0, 0, bm.Width, bm.Height, matrix, true);
            Canvas canvas = new Canvas(rotated);
            
            for (int k = 0; k < 9; k++)
            {
                yc =(int) scaledCubeSize / 3 * ((k / 3) - 1) + (rotated.Height) / 2;
                xc = (int)scaledCubeSize / 3 * ((k % 3) - 1) + (rotated.Width) / 2;

                bufColor = rotated.GetPixel(xc, yc);
                bufRGB[0] = Color.GetRedComponent(bufColor);
                bufRGB[1] = Color.GetGreenComponent(bufColor);
                bufRGB[2] = Color.GetBlueComponent(bufColor);
                Color.RGBToHSV(bufRGB[0], bufRGB[1], bufRGB[2], bufHSV);
                //if (k == 0)
               //     ((Activity)Context).Title = $"{bufHSV[0],-3:f2}|{bufHSV[1],-3:f2}|{bufHSV[2],-3:f2}     {bufRGB[0],3}|{bufRGB[1],3}|{bufRGB[2],3}";
                if (bufHSV[1] < 0.5 && bufRGB.Max() - bufRGB.Min() < 38 )
                    colorPixel = WHITE;
                else if (bufRGB[2] > 1.3 * bufRGB[0] && bufRGB[2] > 1.3 * bufRGB[1])
                    colorPixel = BLUE;
                else if (bufRGB[1] > 2 * bufRGB[0])
                    colorPixel = GREEN;
                else if (bufHSV[0] > 160 || bufHSV[0] < 15)
                    colorPixel = RED;
                else if (bufHSV[0] < 40 && bufRGB[0] - bufRGB[1] > 50)
                    colorPixel = ORANGE;
                else if (bufHSV[0] < 70)
                    colorPixel = YELLOW;
                else
                    colorPixel = -1;
                canvas.DrawRect(
                    (rotated.Width - scaledCubeSize) / 2 + scaledCubeSize / 3 * (k % 3),
                    (rotated.Height - scaledCubeSize) / 2 + scaledCubeSize / 3 * (k / 3),
                    (rotated.Width - scaledCubeSize) / 2 + scaledCubeSize / 3 * (k % 3) + scaledCubeSize/3,
                    (rotated.Height - scaledCubeSize) / 2 + scaledCubeSize / 3 * (k / 3) + scaledCubeSize/3,
                    new Paint() { Color = _getColorByIndex(colorPixel) });
                curSide.Colors[k] = colorPixel;
            }
            SideGot?.Invoke(this, curSide);
            SetImageBitmap(rotated);
        }
    }

}