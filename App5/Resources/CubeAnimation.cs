using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Hardware;
//using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace App5
{

    public class CubeAnimationView : ImageView
    {
        

        public CubeAnimationView(Context context) : base(context)
        {
            
            Visibility = ViewStates.Visible;
            Activated = true;
            Background = new Android.Graphics.Drawables.ColorDrawable(Color.Black);
            Touch += RedrawCube;

            Rect r = new Rect();
            GetWindowVisibleDisplayFrame(r);

            bm = Bitmap.CreateBitmap(r.Width(), r.Height(), Bitmap.Config.Argb8888);
            
        }

        Thread threadAnimation;

        static Bitmap bm;


        class Point3
        {
            public float x, y, z;

            public Point3()
            {
                x = y = z = 0;
            }

            public double X
            {
                get
                {
                    return -x * System.Math.Sin(-AlphaAngle) + y * System.Math.Cos(AlphaAngle) + bm.Width / 2;
                }
            }

            public double Y
            {
                get
                {
                    return bm.Height / 2 - (x * System.Math.Sin(BetaAngle) * System.Math.Cos(AlphaAngle) + y * System.Math.Sin(-AlphaAngle) * System.Math.Sin(BetaAngle) + z * System.Math.Cos(BetaAngle));
                }
            }
        }


        public int Size = 100;
        const int ORANGE = 0, GREEN = 1, YELLOW = 2, BLUE = 3, RED = 4, WHITE = 5, BROWN = 6;
        public int[][] state = new int[][]{
            new int[] {BLUE, YELLOW, GREEN, ORANGE, ORANGE, BLUE, WHITE, GREEN, GREEN},
            new int[] {WHITE, RED,RED, BLUE, GREEN, WHITE, RED, ORANGE, GREEN},
            new int[] {BLUE, YELLOW, YELLOW, RED, YELLOW, ORANGE, ORANGE, WHITE,WHITE},
            new int[] {ORANGE, RED, ORANGE, YELLOW, BLUE, BLUE,RED, WHITE, YELLOW},
            new int[] {WHITE, BLUE, GREEN, WHITE, RED, ORANGE, BLUE, RED, YELLOW},
            new int[] {BLUE, GREEN, RED, GREEN, WHITE, YELLOW, ORANGE, GREEN, YELLOW}
        };

        static readonly int[][] solveState = new int[][]{
            new int[] {ORANGE, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE},
            new int[] {GREEN, GREEN, GREEN, GREEN, GREEN, GREEN, GREEN, GREEN, GREEN},
            new int[] {YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW},
            new int[] {BLUE, BLUE, BLUE, BLUE, BLUE, BLUE, BLUE, BLUE, BLUE},
            new int[] {RED, RED, RED, RED, RED, RED, RED, RED, RED},
            new int[] {WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE}
        };

        public void SetState(int[] colors)
        {
            state[0] = colors.Take(9).ToArray(); 
            state[1] = colors.Skip(9).Take(9).ToArray();
            state[2] = colors.Skip(18).Take(9).ToArray();
            state[3] = colors.Skip(27).Take(9).ToArray(); 
            state[4] = colors.Skip(36).Take(9).ToArray();
            state[5] = colors.Skip(45).Take(9).ToArray();
        }

        public void SetState(int[][] colors)
        {
            for (int i = 0; i < state.Length; i++)
            {
                for (int j = 0; j < state[i].Length; j++)
                {
                    state[i][j] = colors[i][j];
                }
            }
        }

        public void SetDefaultState()
        {
            for (int i = 0; i < solveState.Length; i++)
            {
                for (int j = 0; j < solveState[i].Length; j++)
                {
                    state[i][j] = solveState[i][j];
                }
            }
        }

        readonly List<List<List<int>>> parts = new List<List<List<int>>>{

            new List<List<int>>{
                new List<int>{ 3,  7, 23, 19},
                new List<int>{ 7, 11, 27, 23},
                new List<int>{11, 15, 31, 27},
                new List<int>{19, 23, 39, 35},
                new List<int>{23, 27, 43, 39},
                new List<int>{27, 31, 47, 43},
                new List<int>{35, 39, 55, 51},
                new List<int>{39, 43, 59, 55},
                new List<int>{43, 47, 63, 59}

            },
            new List<List<int>>{

                new List<int>{17,  1,  0, 16},
                new List<int>{18,  2,  1, 17},
                new List<int>{19,  3,  2, 18},
                new List<int>{33, 17, 16, 32},
                new List<int>{34, 18, 17, 33},
                new List<int>{35, 19, 18, 34},
                new List<int>{49, 33, 32, 48},
                new List<int>{50, 34, 33, 49},
                new List<int>{51, 35, 34, 50},
            },
            new List<List<int>>{

                new List<int>{ 9, 13, 12,  8},
                new List<int>{10, 14, 13,  9},
                new List<int>{11, 15, 14, 10},
                new List<int>{ 5,  9,  8,  4},
                new List<int>{ 6, 10,  9,  5},
                new List<int>{ 7, 11, 10,  6},
                new List<int>{ 1,  5,  4,  0},
                new List<int>{ 2,  6,  5,  1},
                new List<int>{ 3,  7,  6,  2}
            },
            new List<List<int>>{

                new List<int>{15, 31, 30, 14},
                new List<int>{14, 30, 29, 13},
                new List<int>{13, 29, 28, 12},
                new List<int>{31, 47, 46, 30},
                new List<int>{30, 46, 45, 29},
                new List<int>{29, 45, 44, 28},
                new List<int>{47, 63, 62, 46},
                new List<int>{46, 62, 61, 45},
                new List<int>{45, 61, 60, 44}
            },
            new List<List<int>>{

                new List<int>{ 8, 12, 28, 24},
                new List<int>{ 4,  8, 24, 20},
                new List<int>{ 0,  4, 20, 16},
                new List<int>{24, 28, 44, 40},
                new List<int>{20, 24, 40, 36},
                new List<int>{16, 20, 36, 32},
                new List<int>{40, 44, 60, 56},
                new List<int>{36, 40, 56, 52},
                new List<int>{32, 36, 52, 48}
            },
            new List<List<int>>{

                new List<int>{48, 52, 53, 49},
                new List<int>{49, 53, 54, 50},
                new List<int>{50, 54, 55, 51},
                new List<int>{52, 56, 57, 53},
                new List<int>{53, 57, 58, 54},
                new List<int>{54, 58, 59, 55},
                new List<int>{56, 60, 61, 57},
                new List<int>{57, 61, 62, 58},
                new List<int>{58, 62, 63, 59}
            }
        };


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
                case BROWN: return Color.BurlyWood;
            }
            return Color.Black;
        }

        private Point3 Camera
        {
            get
            {
                return new Point3()
                {
                    x = -(float)(System.Math.Cos(AlphaAngle) * System.Math.Cos(BetaAngle)) * 1000 * Size,
                    y = (float)(System.Math.Sin(AlphaAngle) * System.Math.Cos(BetaAngle)) * 1000 * Size,
                    z = (float)(System.Math.Sin(BetaAngle)) * 1000 * Size
                };
            }
        }

        private Point3 _getPointByIndex(int i)
        {
            return new Point3
            { 
                x = Size * (-3 + 2 * ((i % 16) / 4)),
                y = Size * (-3 + 2 * (i % 4)),
                z = Size * (3 - 2 * (i / 16))
            };
        }

        private bool _swapColors(string[] arguments)
        {
            int temp, buf = 0;
            for (var i = 0; i < arguments.Length; i++)
            {
                temp = state[arguments[i][0] - '0'][arguments[i][1] - '0'];
                state[arguments[i][0] - '0'][arguments[i][1] - '0'] = buf;
                buf = temp;
            }
            return true;
        }

        private void B_colorChange()
        {
            Bz_colorChange();
            Bz_colorChange();
            Bz_colorChange();
        }
        private void Bz_colorChange()
        {
            this._swapColors(new string[] { "02", "58", "46", "20", "02" });
            this._swapColors(new string[] { "05", "57", "43", "21", "05" });
            this._swapColors(new string[] { "08", "56", "40", "22", "08" });
            this._swapColors(new string[] { "30", "36", "38", "32", "30" });
            this._swapColors(new string[] { "31", "33", "37", "35", "31" });
        }
        private void B2_colorChange()
        {
            Bz_colorChange();
            Bz_colorChange();
        }
        private void L_colorChange()
        {
            Lz_colorChange();
            Lz_colorChange();
            Lz_colorChange();
        }
        private void Lz_colorChange()
        {
            this._swapColors(new string[] { "16", "26", "32", "56", "16" });
            this._swapColors(new string[] { "13", "23", "35", "53", "13" });
            this._swapColors(new string[] { "10", "20", "38", "50", "10" });
            this._swapColors(new string[] { "40", "46", "48", "42", "40" });
            this._swapColors(new string[] { "41", "43", "47", "45", "41" });
        }
        private void L2_colorChange()
        {
            Lz_colorChange();
            Lz_colorChange();
        }
        private void U_colorChange()
        {
            Uz_colorChange();
            Uz_colorChange();
            Uz_colorChange();
        }
        private void Uz_colorChange()
        {
            this._swapColors(new string[] { "12", "02", "32", "42", "12" });
            this._swapColors(new string[] { "11", "01", "31", "41", "11" });
            this._swapColors(new string[] { "10", "00", "30", "40", "10" });
            this._swapColors(new string[] { "20", "26", "28", "22", "20" });
            this._swapColors(new string[] { "21", "23", "27", "25", "21" });
        }
        private void U2_colorChange()
        {
            Uz_colorChange();
            Uz_colorChange();
        }
        private void R_colorChange()
        {
            Rz_colorChange();
            Rz_colorChange();
            Rz_colorChange();

        }
        private void Rz_colorChange()
        {
            this._swapColors(new string[] { "12", "52", "36", "22", "12" });
            this._swapColors(new string[] { "15", "55", "33", "25", "15" });
            this._swapColors(new string[] { "18", "58", "30", "28", "18" });
            this._swapColors(new string[] { "08", "02", "00", "06", "08" });
            this._swapColors(new string[] { "07", "05", "01", "03", "07" });
        }
        private void R2_colorChange()
        {
            Rz_colorChange();
            Rz_colorChange();

        }
        private void F_colorChange()
        {
            Fz_colorChange();
            Fz_colorChange();
            Fz_colorChange();
        }
        private void Fz_colorChange()
        {
            this._swapColors(new string[] { "00", "26", "48", "52", "00" });
            this._swapColors(new string[] { "03", "27", "45", "51", "03" });
            this._swapColors(new string[] { "06", "28", "42", "50", "06" });
            this._swapColors(new string[] { "10", "16", "18", "12", "10" });
            this._swapColors(new string[] { "11", "13", "17", "15", "11" });

        }
        private void F2_colorChange()
        {
            Fz_colorChange();
            Fz_colorChange();
        }
        private void D_colorChange()
        {
            Dz_colorChange();
            Dz_colorChange();
            Dz_colorChange();
        }
        private void Dz_colorChange()
        {
            this._swapColors(new string[] { "46", "36", "06", "16", "46" });
            this._swapColors(new string[] { "47", "37", "07", "17", "47" });
            this._swapColors(new string[] { "48", "38", "08", "18", "48" });
            this._swapColors(new string[] { "58", "52", "50", "56", "58" });
            this._swapColors(new string[] { "57", "55", "51", "53", "57" });
        }
        private void D2_colorChange()
        {
            Dz_colorChange();
            Dz_colorChange();
        }

        private void FillPolygon(Canvas canvas, Point3[] points, Color color)
        {
            if (points.Length == 0) return;
            Android.Graphics.Path p = new Android.Graphics.Path();
            Point3 p3 = points[0];
            p.MoveTo((float)p3.X, (float)p3.Y);
            for (int i = 1; i < points.Length; i++)
            {
                p3 = points[i];
                p.LineTo((float)p3.X, (float)p3.Y);
            }
            Paint pnt = new Paint() { Color = color };
            canvas.DrawPath(p, pnt);
            pnt.SetStyle(Paint.Style.Stroke);
            pnt.Color = Color.Black;
            pnt.StrokeWidth = 2;
            pnt.StrokeMiter = 1.5f;

            canvas.DrawPath(p, pnt);

        }

        class PartsComparer : IEqualityComparer<KeyValuePair<List<int>, int>>
        {
            public bool Equals(KeyValuePair<List<int>, int> x, KeyValuePair<List<int>, int> y)
            {
                return x.Key.Intersect(y.Key).Count() == x.Key.Count;
            }

            public int GetHashCode(KeyValuePair<List<int>, int> obj)
            {
                return obj.Key.GetHashCode();
            }

        }


        private void DrawSide(Canvas canvas, int side)
        {
            for (int i = 0; i < state[side].Length; i++)
            {
                try
                {
                    if (!partsToRot.Contains(new KeyValuePair<List<int>, int>(parts[side][i], -1), new PartsComparer()))
                        FillPolygon(canvas, parts[side][i].Select(x => { return _getPointByIndex(x); }).ToArray(), _getColorByIndex(state[side][i]));

                }
                catch
                {

                }
            }
        }
        int check = 0;

        private void DrawCubeWithoutRotating(Canvas canvas)
        {
            if ((AlphaAngle < System.Math.PI) == (BetaAngle < System.Math.PI / 2 || BetaAngle > System.Math.PI / 2 * 3))
            {
                this.DrawSide(canvas, ORANGE);
            }
            else
            {
                this.DrawSide(canvas, RED);
            }

            if ((AlphaAngle > System.Math.PI / 2 && AlphaAngle < System.Math.PI * 3 / 2) != (BetaAngle < System.Math.PI / 2 || BetaAngle > System.Math.PI / 2 * 3))
            {
                this.DrawSide(canvas, GREEN);
            }
            else
            {
                this.DrawSide(canvas, BLUE);
            }

            if (BetaAngle > System.Math.PI)
            {
                this.DrawSide(canvas, WHITE);
            }
            else
            {
                this.DrawSide(canvas, YELLOW);
            }
        }

        private Point3 _getNormalVectorSide(int side)
        {
            switch (side)
            {
                case BLUE:
                    return new Point3() { x = -1, y = 0, z = 0 };
                case GREEN:
                    return new Point3() { x = 1, y = 0, z = 0 };
                case YELLOW:
                    return new Point3() { x = 0, y = 0, z = -1 };
                case WHITE:
                    return new Point3() { x = 0, y = 0, z = 1 };
                case ORANGE:
                    return new Point3() { x = 0, y = -1, z = 0 };
                case RED:
                    return new Point3() { x = 0, y = 1, z = 0 };
                default:
                    return null;
            }
        }

        private bool IsSideBeyondCube(int side)
        {
            Point3 camera = Camera;
            Point3 normal = _getNormalVectorSide(side);

            return camera.x * normal.x + camera.y * normal.y + camera.z * normal.z > 0;
        }

        private Point3[] _getBlackSide(int side, float angle)
        {
            Point3[] blackSide = new Point3[4];

            Point3 normal = _getNormalVectorSide(side);

            if (normal.x != 0)
            {
                blackSide[0] = new Point3() { x = normal.x * (-Size), y = 3 * Size, z = 3 * Size };
                blackSide[1] = new Point3() { x = normal.x * (-Size), y = 3 * Size, z = -3 * Size };
                blackSide[2] = new Point3() { x = normal.x * (-Size), y = -3 * Size, z = -3 * Size };
                blackSide[3] = new Point3() { x = normal.x * (-Size), y = -3 * Size, z = 3 * Size };
            }
            else if (normal.y != 0)
            {
                blackSide[0] = new Point3() { x = 3 * Size, y = normal.y * (-Size), z = 3 * Size };
                blackSide[1] = new Point3() { x = 3 * Size, y = normal.y * (-Size), z = -3 * Size };
                blackSide[2] = new Point3() { x = -3 * Size, y = normal.y * (-Size), z = -3 * Size };
                blackSide[3] = new Point3() { x = -3 * Size, y = normal.y * (-Size), z = 3 * Size };
            }
            else
            {
                blackSide[0] = new Point3() { x = 3 * Size, y = 3 * Size, z = normal.z * (-Size) };
                blackSide[1] = new Point3() { x = 3 * Size, y = -3 * Size, z = normal.z * (-Size) };
                blackSide[2] = new Point3() { x = -3 * Size, y = -3 * Size, z = normal.z * (-Size) };
                blackSide[3] = new Point3() { x = -3 * Size, y = 3 * Size, z = normal.z * (-Size) };
            }

            if (IsSideBeyondCube(side))
            {
                return blackSide.Select(x => _rotatePoint(x, side, angle)).ToArray();
            }
            return blackSide;
        }
        const int tsize = 100;
        Paint textPaint = new Paint() { TextSize = tsize, Color = Color.GreenYellow, FakeBoldText = true , TextAlign = Paint.Align.Center };
        protected override void OnDraw(Canvas canvas)
        {
            try
            {
                base.OnDraw(canvas);
            }
            catch (Java.Lang.Exception)
            {
                return;
            }
            canvas.DrawColor(Color.Black);
            if (IsSideBeyondCube(RotatingSide))
            {
                RotateSide(canvas, RotatingSide, rotatorAngle);
                DrawCubeWithoutRotating(canvas);
            }
            else
            {
                DrawCubeWithoutRotating(canvas);
                RotateSide(canvas, RotatingSide, rotatorAngle);
            }
            if(curFormula.Length > 15)
            {
                canvas.DrawText(
                    (new string(curFormula.Take(curFormula.Length / 2).ToArray())).Replace('z', '\''), 
                    Width / 2, 
                    Height / 8,
                    textPaint);
                canvas.DrawText(
                    (new string(curFormula.Skip(curFormula.Length / 2).ToArray())).Replace('z', '\''),
                    Width / 2 ,
                    Height / 8 + tsize + 5,
                    textPaint);
            }
            else
            {
                canvas.DrawText(
                    curFormula.Replace('z', '\''),
                    Width / 2 ,
                    Height / 8 + tsize/2,
                    textPaint);
            }

            if (onPause)
            {
                Android.Graphics.Path p = new Android.Graphics.Path();
                p.MoveTo(Width / 20, Height * 17 / 20);
                p.LineTo(Width / 8, Height * 18 / 20);
                p.LineTo(Width / 20, Height * 19 / 20);
                canvas.DrawPath(p, new Paint() { Color = Color.Aquamarine, StrokeWidth = 10 });
            }
            else
            {
                canvas.DrawLine(Width / 20, Height * 17 / 20, Width / 20, Height * 19 / 20, new Paint() { Color = Color.Aquamarine, StrokeWidth = 10 });
                canvas.DrawLine(Width / 8, Height * 17 / 20, Width / 8, Height * 19 / 20, new Paint() { Color = Color.Aquamarine, StrokeWidth = 10 });
            }
            canvas.DrawText(textSpeed, Width * 7/8, Height * 18/20, textPaint);

            //canvas.DrawRect(Width * 9/11, Height * 5 / 11, Width, Height * 7 / 11, new Paint() { Color = Color.AliceBlue });

            /*
            camera = new Point3()
            {
                x = (float)(System.Math.Cos(AlphaAngle) * System.Math.Cos(BetaAngle)) * 10 * Size,
                y = (float)(-System.Math.Sin(AlphaAngle) * System.Math.Cos(BetaAngle)) * 10 * Size,
                z = (float)(-System.Math.Sin(BetaAngle)) * 10 * Size
            };

            canvas.DrawCircle((float)camera.X, (float)camera.Y, 10, new Paint() { Color = Color.White });
            */
            /*
            canvas.DrawCircle((float)TestPoints[0].X, (float)TestPoints[0].Y, 10, new Paint() { Color = Color.Red });
            canvas.DrawCircle((float)TestPoints[1].X, (float)TestPoints[1].Y, 10, new Paint() { Color = Color.Green });
            canvas.DrawCircle((float)TestPoints[2].X, (float)TestPoints[2].Y, 10, new Paint() { Color = Color.Blue });
            */
            try
            {
                SetImageBitmap(bm);
            }
            catch { }
        }
        string textSpeed = "x2";
        private Point3[] TestPoints = new Point3[3] {
            new Point3() { x = 300, y = 0, z = 0 },
            new Point3() { x = 0, y = 300, z = 0 },
            new Point3() { x = 0, y = 0, z = 300 }
        };
        private float rotatorAngle = 0;
        private float neededAngle = 0;

        private static float alpha = (float)System.Math.PI / 4, beta = (float)System.Math.PI / 4;

        public static float AlphaAngle
        {
            get => alpha;
            set
            {
                if (value > 2 * System.Math.PI)
                    alpha = value - 2 * (float)System.Math.PI;
                else if (value < 0)
                    alpha = value + 2 * (float)System.Math.PI;
                else
                    alpha = value;
            }
        }
        public static float BetaAngle
        {
            get => beta;
            set
            {
                if (value > 2 * System.Math.PI)
                    beta = value % (2 * (float)System.Math.PI);
                else if (value < 0)
                    beta = value + 2 * (float)System.Math.PI;
                else
                    beta = value;
            }
        }

        float lastX = 0, lastY = 0;
        float aa = AlphaAngle,
              ba = BetaAngle;
        string[] formulas;
        string curFormula = "";
        private void ThreadAnimation()
        {
            Canvas canvas = new Canvas(bm);
            foreach (string formula in formulas)
            {
                curFormula = formula;
                string[] moves = formula.Split(' ');
                MoveInit(moves[0]);
                for (int i = 0; i < moves.Length;)
                {
                    while (onPause)
                        Thread.Sleep(5); ;
                    if (neededAngle > 0)
                        rotatorAngle += angleStep;
                    else
                        rotatorAngle -= angleStep;
                    Draw(canvas);
                    if (System.Math.Abs(rotatorAngle)  + 0.1 >= System.Math.Abs(neededAngle))
                    {
                        MakeMove(moves[i]);
                        i++;
                        if (i != moves.Length)
                            MoveInit(moves[i]);
                    }
                    Thread.Sleep(timeDelay);
                }
            }
            partsToRot.Clear();
        }
        private int timeDelay = 100;
        private bool animationReadyStart = true;
        private bool onPause = true;
        private void RedrawCube(object sender, TouchEventArgs a)
        {

            Canvas canvas = new Canvas(bm);
            switch (a.Event.Action)
            {
                case MotionEventActions.Down:
                    lastX = a.Event.RawX;
                    lastY = a.Event.RawY;
                    aa = AlphaAngle;
                    ba = BetaAngle;
                    
                    
                    if(lastX < Width * 2/11 && lastY >Height * 17 / 20)
                    {
                        if (animationReadyStart)
                        {
                            Start();
                        }
                        onPause = !onPause;
                    }
                    else if(lastX > Width * 9 / 11  && lastY > Height * 17 / 20)
                    {
                        switch (timeDelay)
                        {
                            case 200: timeDelay = 100; textSpeed = "x2";  break;
                            case 100: timeDelay = 5; textSpeed = "x3"; break;
                            case 5: timeDelay = 200; textSpeed = "x1"; break;
                        }
                    }
                    
                   
                    break;
                case MotionEventActions.Move:
                    int dx = -(int)(a.Event.RawX - lastX);
                    int dy = (int)(a.Event.RawY - lastY);
                    AlphaAngle = aa + (float)System.Math.PI * dx / 2000;
                    BetaAngle = ba + (float)System.Math.PI * dy / 2000;
                    Draw(canvas);
                    break;
            }
        }

        public void SolveCurrentState()
        {
            CubeSolver cs = new CubeSolver(state);
            formulas = cs.GetSolution().ToArray();
        }

        private List<KeyValuePair<List<int>, int>> partsToRot = new List<KeyValuePair<List<int>, int>>();
        private int RotatingSide = 0;
        private void StartRotating(int side, float angle)
        {
            rotatorAngle  = 0;
            neededAngle = angle;
            RotatingSide = side;
            List<int> pointsRotatingSide = parts[side][0]
              .Union(parts[side][2])
              .Union(parts[side][6])
              .Union(parts[side][8])
              .ToList();

            partsToRot = new List<KeyValuePair<List<int>, int>>();
            for (int i = 0; i < parts.Count; i++)
            {
                for (int j = 0; j < parts[i].Count; j++)
                {
                    if (parts[i][j].Intersect(pointsRotatingSide).Count() != 0)
                        partsToRot.Add(new KeyValuePair<List<int>, int>(parts[i][j], state[i][j]));
                }
            }
        }

        private Point3 _rotatePointX(Point3 point, float angle)
        {
            return new Point3()
            {
                x = point.x,
                y = point.y * (float)System.Math.Cos(angle) - point.z * (float)System.Math.Sin(angle),
                z = point.y * (float)System.Math.Sin(angle) + point.z * (float)System.Math.Cos(angle)
            };
        }

        private Point3 _rotatePointY(Point3 point, float angle)
        {
            return new Point3()
            {
                x = point.x * (float)System.Math.Cos(angle) + point.z * (float)System.Math.Sin(angle),
                y = point.y,
                z = -point.x * (float)System.Math.Sin(angle) + point.z * (float)System.Math.Cos(angle)
            };
        }

        private Point3 _rotatePointZ(Point3 point, float angle)
        {
            return new Point3()
            {
                x = point.x * (float)System.Math.Cos(angle) - point.y * (float)System.Math.Sin(angle),
                y = point.x * (float)System.Math.Sin(angle) + point.y * (float)System.Math.Cos(angle),
                z = point.z
            };
        }

        private Point3 _rotatePoint(Point3 point, int side, float angle)
        {
            switch (side)
            {
                case ORANGE:
                    return _rotatePointY(point, -angle);
                case BLUE:
                    return _rotatePointX(point, angle);
                case GREEN:
                    return _rotatePointX(point, -angle);
                case RED:
                    return _rotatePointY(point, angle);
                case YELLOW:
                    return _rotatePointZ(point, -angle);
                case WHITE:
                    return _rotatePointZ(point, angle);
            }
            return point;
        }

        private void RotateSide(Canvas canvas, int side, float degrees)
        {
            if (partsToRot.Count == 0)
                return;
            Point3 pileNorm = new Point3();
            Point3[] wa = new Point3[3];
            float pileD;
            Point3 camera = Camera;
            IEnumerable<KeyValuePair<List<int>, int>> en = partsToRot.Where(pile =>
            {
                wa[0] = _rotatePoint(_getPointByIndex(pile.Key[0]), side, degrees);
                wa[1] = _rotatePoint(_getPointByIndex(pile.Key[1]), side, degrees);
                wa[2] = _rotatePoint(_getPointByIndex(pile.Key[2]), side, degrees);
                pileNorm.x = wa[0].y * wa[1].z + wa[2].y * wa[0].z + wa[1].y * wa[2].z - (wa[2].y * wa[1].z + wa[0].y * wa[2].z + wa[1].y * wa[0].z);
                pileNorm.y = -(wa[0].x * wa[1].z + wa[2].x * wa[0].z + wa[1].x * wa[2].z - (wa[2].x * wa[1].z + wa[0].x * wa[2].z + wa[1].x * wa[0].z));
                pileNorm.z = wa[0].x * wa[1].y + wa[2].x * wa[0].y + wa[1].x * wa[2].y - (wa[2].x * wa[1].y + wa[0].x * wa[2].y + wa[1].x * wa[0].y);
                pileD = -(
                    wa[0].x * wa[1].y * wa[2].z +
                    wa[2].x * wa[0].y * wa[1].z +
                    wa[1].x * wa[2].y * wa[0].z -
                    wa[2].x * wa[1].y * wa[0].z -
                    wa[1].x * wa[0].y * wa[2].z -
                    wa[0].x * wa[2].y * wa[1].z);

                return (((camera.x) * (pileNorm.x) + (camera.y) * (pileNorm.y) + (camera.z) * (pileNorm.z) + pileD) * (pileD) < 0);
            });

            try
            {
                FillPolygon(canvas, _getBlackSide(RotatingSide, degrees), Color.Brown);
                for (int i = 0; i < en.Count(); i++)
                {
                    FillPolygon(canvas, en.ElementAt(i).Key.Select(x => _rotatePoint(_getPointByIndex(x), side, degrees)).ToArray(), _getColorByIndex(en.ElementAt(i).Value));
                }

            }
            catch
            {

            }

        }

        float piHalf = (float)System.Math.PI / 2;
        float pi = (float)System.Math.PI;
        private float angleStep = (float)System.Math.PI/16;

        public void Start()
        {
            animationReadyStart = false;
            threadAnimation = new Thread(ThreadAnimation);
            threadAnimation.Start();
        }
        

        private void MoveInit(string move)
        {
                switch (move)
                {
                    case "R":
                        StartRotating(ORANGE, -piHalf);
                        break;
                    case "Rz":
                        StartRotating(ORANGE, piHalf);
                        break;
                    case "R2":
                        StartRotating(ORANGE, pi);
                        break;
                    case "L":
                        StartRotating(RED, -piHalf);
                        break;
                    case "Lz":
                        StartRotating(RED, piHalf);
                        break;
                    case "L2":
                        StartRotating(RED, pi);
                        break;
                    case "F":
                        StartRotating(GREEN, piHalf);
                        break;
                    case "Fz":
                        StartRotating(GREEN, -piHalf);
                        break;
                    case "F2":
                        StartRotating(GREEN, pi);
                        break;
                    case "B":
                        StartRotating(BLUE, piHalf);
                        break;
                    case "Bz":
                        StartRotating(BLUE, -piHalf);
                        break;
                    case "B2":
                        StartRotating(BLUE, pi);
                        break;
                    case "D":
                        StartRotating(WHITE, -piHalf);
                        break;
                    case "Dz":
                        StartRotating(WHITE, piHalf);
                        break;
                    case "D2":
                        StartRotating(WHITE, pi);
                        break;
                    case "U":
                        StartRotating(YELLOW, -piHalf);
                        break;
                    case "Uz":
                        StartRotating(YELLOW, piHalf);
                        break;
                    case "U2":
                        StartRotating(YELLOW, pi);
                        break;
                    default:
                        return;
                }

        }

        public void DoFormulas(string[] formulas)
        {
            this.formulas = formulas;
            Start();
        }

        private void MakeMove(string move)
        {
            switch (move)
            {
                case "R":
                    R_colorChange();
                    break;
                case "Rz":
                    Rz_colorChange();
                    break;
                case "R2":
                    R2_colorChange();
                    break;
                case "L":
                    L_colorChange();
                    break;
                case "Lz":
                    Lz_colorChange();
                    break;
                case "L2":
                    L2_colorChange();
                    break;
                case "F":
                    F_colorChange();
                    break;
                case "Fz":
                    Fz_colorChange();
                    break;
                case "F2":
                    F2_colorChange();
                    break;
                case "B":
                    B_colorChange();
                    break;
                case "Bz":
                    Bz_colorChange();
                    break;
                case "B2":
                    B2_colorChange();
                    break;
                case "D":
                    D_colorChange();
                    break;
                case "Dz":
                    Dz_colorChange();
                    break;
                case "D2":
                    D2_colorChange();
                    break;
                case "U":
                    U_colorChange();
                    break;
                case "Uz":
                    Uz_colorChange();
                    break;
                case "U2":
                    U2_colorChange();
                    break;
                default:
                    return;
            }

        }
    }
}