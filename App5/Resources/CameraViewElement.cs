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
    public class CameraViewElement : ImageView
    {
        float[][] lines;
        public int Size {
            get;
        }
        Point uprightcorner;
        int check = 0;
        private CameraViewOutput output;

        Paint p = new Paint()
        {
            Color = Color.White,
            StrokeWidth = 3
        };

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
        public bool[] saved = new bool[6] { false, false, false, false, false, false };
        public int[] ulrd = new int[4] { -1, -1, -1, -1 };
       
        public CameraViewElement(Context context) : base(context)
        {
            Visibility = ViewStates.Visible;
            Activated = true;
            Background = new Android.Graphics.Drawables.ColorDrawable(Color.Transparent);
            Rect r = new Rect();
            GetWindowVisibleDisplayFrame(r);
            Size = (int)(r.Width() * 0.555);
            bm = Bitmap.CreateBitmap(r.Width(), r.Height(), Bitmap.Config.Argb8888);
            SetFitsSystemWindows(true);

        }
        float precubemargin = 0.05f, pilesize = 0.33f;
        Bitmap bm;
        public void SetOutput(CameraViewOutput o)
        {
            output = o;
            output.SideGot += Redraw;
        }

        public void OnClick()
        {
            for (int i = 0; i < 6; i++)
            {
                saved[i] = output.IsSideFilled(i);
            }
        }

        private void FillColors(int centerColor)
        {
            switch (centerColor)
            {
                case WHITE:
                    {
                        ulrd = new int[]{ GREEN, RED, ORANGE, BLUE };
                        break;
                    }
                case RED:
                    {
                        ulrd = new int[] { YELLOW, BLUE, GREEN, WHITE };
                        break;
                    }
                case GREEN:
                    {
                        ulrd = new int[] { YELLOW, RED, ORANGE, WHITE };
                        break;
                    }
                case BLUE:
                    {
                        ulrd = new int[] { YELLOW, ORANGE, RED, WHITE };
                        break;
                    }
                case ORANGE:
                    {
                        ulrd = new int[] { YELLOW, GREEN, BLUE, WHITE };
                        break;
                    }
                case YELLOW:
                    {
                        ulrd = new int[] { BLUE, RED, YELLOW, GREEN };
                        break;
                    }
            }
        }
        public void Redraw(object sender, Side e)
        {
            Canvas canvas = new Canvas(bm);
            
            FillColors(e.Color);
            Draw(canvas);
        }
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            
            canvas.DrawColor(Color.Transparent);
            uprightcorner = new Point(Width / 2 - Size / 2, Height / 2 - Size / 2);
            lines = new float[][] {
                new float[] { uprightcorner.X + 0 * Size, uprightcorner.Y + 0 * Size, uprightcorner.X + 1 * Size, uprightcorner.Y + 0 * Size },
                new float[] { uprightcorner.X + 0 * Size, uprightcorner.Y + pilesize * Size, uprightcorner.X + 1 * Size, uprightcorner.Y + pilesize * Size },
                new float[] { uprightcorner.X + 0 * Size, uprightcorner.Y + pilesize * 2 * Size, uprightcorner.X + 1 * Size, uprightcorner.Y + pilesize * 2 * Size },
                new float[] { uprightcorner.X + 0 * Size, uprightcorner.Y + 1 * Size, uprightcorner.X + 1 * Size, uprightcorner.Y + 1 * Size },
                new float[] { uprightcorner.X + 0 * Size, uprightcorner.Y + 0 * Size, uprightcorner.X + 0 * Size, uprightcorner.Y + 1 * Size },
                new float[] { uprightcorner.X + pilesize * Size, uprightcorner.Y + 0 * Size, uprightcorner.X + pilesize * Size, uprightcorner.Y + 1 * Size },
                new float[] { uprightcorner.X + pilesize * 2 * Size, uprightcorner.Y + 0 * Size, uprightcorner.X + pilesize * 2 * Size, uprightcorner.Y + 1 * Size },
                new float[] { uprightcorner.X + 1 * Size, uprightcorner.Y + 0 * Size, uprightcorner.X + 1 * Size, uprightcorner.Y + 1 * Size }
            };
            if (canvas != null && Width * Height != 0)
            {
                foreach (float[] line in lines)
                {
                    canvas.DrawLine(line[0], line[1], line[2], line[3], p);
                }

                for (int i = 0; i < saved.Length; i++)
                {
                    if (saved[i])
                    {
                        canvas.DrawCircle(
                            Width * (i + 4) / 10 + Width / 70,
                            Height / 20 + Width / 70,
                            Width / 35,
                            new Paint() { Color = _getColorByIndex(i) }
                            );
                    }
                    else
                    {
                        canvas.DrawCircle(
                            Width * (i + 4) / 10 + Width / 70,
                            Height / 20 + Width / 70,
                            Width / 70,
                            new Paint() { Color = _getColorByIndex(i) }
                            );
                    }


                }
                if (ulrd[0] != -1)
                {
                    canvas.DrawRoundRect(
                        uprightcorner.X + pilesize * Size,
                        uprightcorner.Y - (pilesize + precubemargin) * Size,
                        uprightcorner.X + pilesize * 2 * Size,
                        uprightcorner.Y - precubemargin * Size,
                        20, 20,
                        new Paint() { Color = _getColorByIndex(ulrd[0]) }

                        );
                    canvas.DrawRoundRect(
                        uprightcorner.X - (pilesize + precubemargin) * Size,
                        uprightcorner.Y + pilesize * Size,
                        uprightcorner.X - precubemargin * Size,
                        uprightcorner.Y + pilesize * 2 * Size,
                        20, 20,
                        new Paint() { Color = _getColorByIndex(ulrd[1]) }

                        );

                    canvas.DrawRoundRect(
                        uprightcorner.X + pilesize * Size,
                        uprightcorner.Y + (1 + precubemargin) * Size,
                        uprightcorner.X + pilesize * 2 * Size,
                        uprightcorner.Y + (1 + precubemargin + pilesize) * Size,
                        20, 20,
                        new Paint() { Color = _getColorByIndex(ulrd[3]) }

                        );

                    canvas.DrawRoundRect(
                        uprightcorner.X + (1 + precubemargin) * Size,
                        uprightcorner.Y + pilesize * Size,
                        uprightcorner.X + (1 + precubemargin + pilesize) * Size,
                        uprightcorner.Y + pilesize * 2 * Size,
                        20, 20,
                        new Paint() { Color = _getColorByIndex(ulrd[2]) }

                        );
                }
            }
            else
            {
            }
            SetImageBitmap(bm);
        }
    }

}