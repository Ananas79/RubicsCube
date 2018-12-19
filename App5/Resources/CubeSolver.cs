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
    public class CubeSolver
    {

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


        public void DoFormula(string formula)
        {
            string[] moves = formula.Split(' ');

            foreach (string move in moves)
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
                        break;
                }
            }
        }

        int[][] state = new int[][]
        {
            new int[9],
            new int[9],
            new int[9],
            new int[9],
            new int[9],
            new int[9]
        };

        static readonly int[][] solveState = new int[][]{
            new int[] {ORANGE, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE, ORANGE},
            new int[] {GREEN, GREEN, GREEN, GREEN, GREEN, GREEN, GREEN, GREEN, GREEN},
            new int[] {YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW, YELLOW},
            new int[] {BLUE, BLUE, BLUE, BLUE, BLUE, BLUE, BLUE, BLUE, BLUE},
            new int[] {RED, RED, RED, RED, RED, RED, RED, RED, RED},
            new int[] {WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE, WHITE}
        };

        public CubeSolver(int[][] state)
        {
            for (int i = 0; i < state.Length; i++)
            {
                for (int j = 0; j < state[i].Length; j++)
                {
                    this.state[i][j] = state[i][j];
                }
            }
        }

        public CubeSolver()
        {
            for (int i = 0; i < solveState.Length; i++)
            {
                for (int j = 0; j < solveState[i].Length; j++)
                {
                    state[i][j] = solveState[i][j];
                }
            }
        }

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

        public List<string> GetSolution()
        {
            List<string> res = new List<string>();
            Solve1(res);
            Solve2(res);
            Solve3(res);
            Solve4(res);
            Solve5(res);
            Solve6(res);
            Solve7(res);
            return res;
        }
        

        const int ORANGE = 0, GREEN = 1, YELLOW = 2, BLUE = 3, RED = 4, WHITE = 5;
        private int Mask(params int[] m)
        {
            int res = 0;
            foreach(int i in m)
            {
                switch (i)
                {
                    case BLUE:   res |=  1; break;
                    case YELLOW: res |=  2; break;
                    case ORANGE: res |=  4; break;
                    case WHITE:  res |=  8; break;
                    case GREEN:  res |= 16; break;
                    case RED:    res |= 32; break;
                }
            }
           
            return res;
        }
        struct BiPlace
        {
            public int S1 { get; set; }
            public int S2 { get; set; }

            public int I1 { get; set; }
            public int I2 { get; set; }

            public BiPlace(int s1, int i1, int s2, int i2)
            {
                S1 = s1;
                S2 = s2;
                I1 = i1;
                I2 = i2;
            }
        }
        private object GetByMask(int mask)
        {
            switch (mask)
            {
                case 33: return new BiPlace(BLUE, 5, RED, 3); 
                case  9: return new BiPlace(BLUE, 7, WHITE, 7); 
                case  3: return new BiPlace(BLUE, 1, YELLOW, 1); 
                case  5: return new BiPlace(BLUE, 3, ORANGE, 5); 

                case 34: return new BiPlace(YELLOW, 3, RED, 1); 
                case 18: return new BiPlace(YELLOW, 7, GREEN, 1); 
                case  6: return new BiPlace(YELLOW, 5, ORANGE, 1); 

                case 48: return new BiPlace(RED, 5, GREEN, 3); 
                case 40: return new BiPlace(RED, 7, WHITE, 3); 
                case 12: return new BiPlace(ORANGE, 7, WHITE, 5); 
                case 20: return new BiPlace(ORANGE, 3, GREEN, 5); 

                case 24: return new BiPlace(GREEN, 7, WHITE, 1);

                case 35: return new TriPlace(BLUE, 2, RED, 0, YELLOW, 0);
                case  7: return new TriPlace(BLUE, 0, YELLOW, 2, ORANGE, 2);
                case 13: return new TriPlace(BLUE, 6, ORANGE, 8, WHITE, 8);
                case 41: return new TriPlace(BLUE, 8, RED, 6, WHITE, 6);

                case 50: return new TriPlace(GREEN, 0, RED, 2, YELLOW, 6);
                case 22: return new TriPlace(GREEN, 2, ORANGE, 0, YELLOW, 8);
                case 56: return new TriPlace(GREEN, 6, RED, 8, WHITE, 0);
                case 28: return new TriPlace(GREEN, 8, ORANGE, 6, WHITE, 2);
            }
            return null;
        }
        struct Pair
        {
            public int a, b;
            public int this[int i]
            {
                get { if (i == 0) return a; else return b; }
            }
            public Pair(int x, int y)
            {
                a = x;
                b = y;
            }
        }
        private BiPlace Find2(int c1, int c2)
        {
            List<Pair> bis = new List<Pair>()
            {
                new Pair(BLUE, YELLOW),
                new Pair(BLUE, RED),
                new Pair(BLUE, WHITE),
                new Pair(BLUE, ORANGE),

                new Pair(YELLOW, ORANGE),
                new Pair(YELLOW, RED),
                new Pair(YELLOW, GREEN),

                new Pair(RED, GREEN),
                new Pair(RED, WHITE),

                new Pair(ORANGE, GREEN),
                new Pair(ORANGE, WHITE),

                new Pair(GREEN, WHITE)
            };
            foreach(Pair p in bis)
            {
                BiPlace b = (BiPlace)GetByMask(Mask(p.a, p.b));
                int col1 = state[b.S1][b.I1];
                int col2 = state[b.S2][b.I2];
                if (col1 == c1 && col2 == c2)
			        return new BiPlace(b.S1, b.I1, b.S2, b.I2);
                else if (col1 == c2 && col2 == c1)
                    return new BiPlace(b.S2, b.I2, b.S1, b.I1);
            }
            return new BiPlace(-1, -1,- 1,- 1);
        }

        struct TriPlace
        {
            public int S1 { get; set; }
            public int S2 { get; set; }
            public int S3 { get; set; }

            public int I1 { get; set; }
            public int I2 { get; set; }
            public int I3 { get; set; }

            public TriPlace(int s1, int i1, int s2, int i2, int s3, int i3)
            {
                S1 = s1;
                S2 = s2;
                S3 = s3;
                I1 = i1;
                I2 = i2;
                I3 = i3;
            }
        }
       
        struct Triple
        {
            public int a, b, c;
            public int this[int i]
            {
                get { if (i == 0) return a; else if (i == 1) return b; else return c; }
            }
            public Triple(int x, int y, int z)
            {
                a = x;
                b = y;
                c = z;
            }
        }
        private TriPlace Find3(int c1, int c2, int c3)
        {
            List<Triple> tris = new List<Triple>()
            {
                new Triple(BLUE, YELLOW, RED),
                new Triple(BLUE, WHITE, RED),
                new Triple(BLUE, WHITE, ORANGE),
                new Triple(BLUE, ORANGE, YELLOW),

                new Triple(GREEN, YELLOW, RED),
                new Triple(GREEN, WHITE, RED),
                new Triple(GREEN, WHITE, ORANGE),
                new Triple(GREEN, ORANGE, YELLOW)
            };
            foreach (Triple p in tris)
            {
                TriPlace b = (TriPlace)GetByMask(Mask(p.a, p.b, p.c));
                int col1 = state[b.S1][b.I1];
                int col2 = state[b.S2][b.I2];
                int col3 = state[b.S3][b.I3];
              
                if (Mask(col1, col2, col3) == Mask(c1, c2, c3))
                    return b;
            }
            return new TriPlace(-1, -1, -1, -1, -1, -1);
        }

        private void Solve1(List<string> res)
        {
            Dictionary<int, string> solv1up = new Dictionary<int, string> {
                { 40, "L2 Uz"},
                { 9, "B2 U2"},
                { 12, "R2 U"},
                { 24, "F2"},
                { 33, "L Uz Lz"},
                { 5, "Rz U R"},
                { 48, "Lz Uz L"},
                { 20, "R U Rz"},
                { 34, "Uz"},
                { 18, ""},
                { 6, "U"},
                { 3, "U2"}
            };

            Dictionary<int, string> solv1down = new Dictionary<int, string> {
                { 32, "U L2"},
                { 40, "Fz L F"},
                { 1, "U2 B2"},
                { 9, "U Lz B L"},
                { 4, "Uz R2"},
                { 12, "F Rz Fz"},
                { 16, "F2"},
                { 24, "Uz Rz F R"}
            };

            var temp = new List<Pair>() {
                new Pair(WHITE, GREEN),
                new Pair(WHITE, RED),
                new Pair(WHITE, BLUE),
                new Pair(WHITE, ORANGE)
            };
            foreach (Pair p in temp)
            {
                BiPlace b = Find2(p.a, p.b);
                if (state[b.S1][b.I1] == b.S1 && state[b.S2][b.I2] == b.S2)
                    continue;
                res.Add(solv1up[Mask(b.S1, b.S2)]);
                DoFormula(solv1up[Mask(b.S1, b.S2)]);
                b = Find2(p.a, p.b);
                int key = Mask(p.b, state[GREEN][1]);
                res.Add(solv1down[key]);
                DoFormula(solv1down[key]);
            }
        }

        private void Solve2(List<string> res)
        {
            Dictionary<int, string> solv2place = new Dictionary<int, string>{
               { 28, "Fz Uz F U"},
               { 56, "Lz Uz L"},
               { 50, "Uz"},
               { 13, "Rz U2 R Uz"},
               { 41, "L U2 Lz"},
               { 22, ""},
               { 35, "U2"},
               {  7, "U"},
            };

            Dictionary<int, string> solv2orient = new Dictionary<int, string> {
                { GREEN , ""},
                { YELLOW , "Uz Rz Fz Lz F R Fz L F"},
                { ORANGE , "U F R B Rz Fz R Bz Rz"}
            };

            Dictionary<int, string> solv2fin = new Dictionary<int, string> {
                { 28 , "Fz Uz F"},
                { 56 , "U Lz Uz L"},
                { 13 , "Uz Rz Uz R"},
                { 41 , "L U2 Lz"}
            };

            var temp = new List<Triple>() {
                new Triple(BLUE, WHITE, RED),
                new Triple(BLUE, WHITE, ORANGE),
                new Triple(GREEN, WHITE, RED),
                new Triple(GREEN, WHITE, ORANGE)
            };
            string formulaBuffer;
            foreach (Triple p in temp)
            {
                TriPlace b = Find3(p.a, p.b, p.c);
                if (state[b.S1][b.I1] == b.S1 && state[b.S2][b.I2] == b.S2)
                    continue;

                formulaBuffer = solv2place[Mask(b.S1, b.S2, b.S3)];
                res.Add(formulaBuffer);
                DoFormula(formulaBuffer);

                if (state[YELLOW][8] == WHITE)
                    formulaBuffer = solv2orient[YELLOW];
                else if (state[ORANGE][0] == WHITE)
                    formulaBuffer = solv2orient[ORANGE];
                else
                    formulaBuffer = solv2orient[GREEN];
                res.Add(formulaBuffer);
                DoFormula(formulaBuffer);

                formulaBuffer = solv2fin[Mask(p.a, p.b, p.c)];
                res.Add(formulaBuffer);
                DoFormula(formulaBuffer);

            }
        }

        private void Solve3(List<string> res)
        {
            Dictionary<int, string> solv3up = new Dictionary<int, string>{
                { 33, "Uz Bz U B U L Uz Lz"},
                {  5, "U B Uz Bz Uz Rz U R"},
                { 48, "Uz Lz U L U F Uz Fz"},
                { 20, "U R Uz Rz Uz Fz U F" }
            };

            Dictionary<string, string> solv3down = new Dictionary<string, string> {
               { $"{BLUE}{RED}|{RED}", "Uz Bz U B U L Uz Lz"},
               { $"{BLUE}{RED}|{BLUE}", "U L Uz Lz Uz Bz U B"},
               { $"{BLUE}{ORANGE}|{ORANGE}", "U B Uz Bz Uz Rz U R"},
               { $"{BLUE}{ORANGE}|{BLUE}", "Uz Rz U R U B Uz Bz"},
               { $"{GREEN}{RED}|{GREEN}", "Uz Lz U L U F Uz Fz"},
               { $"{GREEN}{RED}|{RED}", "U F Uz Fz Uz Lz U L"},
               { $"{GREEN}{ORANGE}|{GREEN}", "U R Uz Rz Uz Fz U F"},
               { $"{GREEN}{ORANGE}|{ORANGE}", "Uz Fz U F U R Uz Rz" }
            };

            var temp = new List<Pair>() {
                new Pair(BLUE, RED),
                new Pair(BLUE, ORANGE),
                new Pair(GREEN, RED),
                new Pair(GREEN, ORANGE)
            };
            string formulaBuffer;
            string key;
            foreach (Pair p in temp)
            {
                BiPlace b = Find2(p.a, p.b);
                if (state[b.S1][b.I1] == b.S1 && state[b.S2][b.I2] == b.S2)
                    continue;

                if (b.S1 != YELLOW && b.S2 != YELLOW)
                {
                    formulaBuffer = solv3up[Mask(b.S1, b.S2)];
                    res.Add(formulaBuffer);
                    DoFormula(formulaBuffer);
                }

                b = Find2(p.a, p.b);
                while(p.a != b.S1 && p.b != b.S2)
                {
                    formulaBuffer = "U";
                    res.Add(formulaBuffer);
                    DoFormula(formulaBuffer);
                    b = Find2(p.a, p.b);
                }

                if (p.a == b.S1)
                    key = $"{p.a}{p.b}|{p.a}";
                else
                    key = $"{p.a}{p.b}|{p.b}";

                formulaBuffer = solv3down[key];
                res.Add(formulaBuffer);
                DoFormula(formulaBuffer);
            }
        }

        private void Solve4(List<string> res)
        {
            string frontCornerRot = "L F U Fz Uz Lz Rz Fz Uz F U R";
            string formulaBuffer;
            while (state[YELLOW][1] != YELLOW ||
                   state[YELLOW][3] != YELLOW ||
                   state[YELLOW][7] != YELLOW ||
                   state[YELLOW][5] != YELLOW)
            {
                if (state[YELLOW][7] != YELLOW)
                {
                    formulaBuffer = frontCornerRot;
                    res.Add(formulaBuffer);
                    DoFormula(formulaBuffer);
                }
                res.Add("Uz");
                DoFormula("Uz");
            }
        }

        private void Solve5(List<string> res)
        {
            string eyes = "R2 D Rz U2 R Dz Rz U2 Rz";
            string ears = "Fz Lz Bz L F Lz B L2 F R Fz Lz F Rz Fz";
            string formulaBuffer;
            while (state[YELLOW][0] != YELLOW || 
                   state[YELLOW][2] != YELLOW || 
                   state[YELLOW][6] != YELLOW || 
                   state[YELLOW][8] != YELLOW)
            {
                if (state[GREEN][2] == YELLOW)
                {
                    formulaBuffer = eyes;
                    res.Add(formulaBuffer);
                    DoFormula(formulaBuffer);
                }
                else if (state[ORANGE][0] == YELLOW)
                {
                    formulaBuffer = ears;
                    res.Add(formulaBuffer);
                    DoFormula(formulaBuffer);
                }
                res.Add("Uz");
                DoFormula("Uz");
            }
        }

        private void Solve6(List<string> res)
        {
            while(state[GREEN][1] != GREEN)
            {
                res.Add("Uz");
                DoFormula("Uz");
            }

            Dictionary<string, string> solv6pll = new Dictionary<string, string> {
               { $"{RED}{BLUE}{ORANGE}", ""},
               { $"{RED}{ORANGE}{BLUE}", "Rz U Rz Uz Bz Rz B2 Uz Bz U Bz R B R"},
               { $"{BLUE}{RED}{ORANGE}", "F R Uz Rz Uz R U Rz Fz R U Rz Uz Rz F R Fz"},
               { $"{BLUE}{ORANGE}{RED}", "Rz U Rz Uz Rz Uz Rz U R U R2"},
               { $"{ORANGE}{BLUE}{RED}", "R U Rz Uz Rz F R2 Uz Rz Uz R U Rz Fz"},
               { $"{ORANGE}{RED}{BLUE}", "R2 Uz Rz Uz R U R U R Uz R" }
            };
            string formulaBuffer = solv6pll[$"{state[RED][1]}{state[BLUE][1]}{state[ORANGE][1]}"];
            res.Add(formulaBuffer);
            DoFormula(formulaBuffer);
        }

        private void Solve7(List<string> res)
        {
            Dictionary<int, string> solv7index = new Dictionary<int, string> {
                { 48 , "1" },
                { 20 , "2" },
                {  5 , "3" },
                { 33 , "4" }
            };

            string loc2 = 
                $"{solv7index[Mask(state[RED][2],   state[GREEN][0])]}" +
                $"{solv7index[Mask(state[GREEN][2], state[ORANGE][0])]}";

            string po = "R U Rz Fz L F Rz Fz Lz F R2 Uz Rz",
                   protiv = "L Fz L B2 Lz F L B2 L2";

            Dictionary<string, string> solv7final = new Dictionary<string, string> {
                {"12" , ""},
                {"13" , "Uz " + protiv + " U"},
                {"14" , "Uz " + po + " U"},
                {"21" , "L Uz R D2 Rz U Lz R Uz L D2 Lz U Rz"},
                {"23" , "U2 " + protiv + " U2"},
                {"24" , "U " + protiv + " Uz"},
                {"31" , "U2 " + po + " U2"},
                {"32" , protiv},
                {"34" , "U2 L R U2 Lz Rz Fz Bz U2 F B"},
                {"41" , "U " + po + " Uz"},
                {"42" , po},
                {"43" , "U L Uz R D2 Rz U Lz R Uz L D2 Lz U Rz Uz" }
            };

            string formulaBuffer = solv7final[loc2];
            res.Add(formulaBuffer);
            DoFormula(formulaBuffer);
        }

        public static string Backward(string forward)
        {
            return String.Join(" ",
                (forward + ' ')
                .Replace("z", "_")
                .Replace("R ", "Rz ")
                .Replace("L ", "Lz ")
                .Replace("F ", "Fz ")
                .Replace("B ", "Bz ")
                .Replace("U ", "Uz ")
                .Replace("D ", "Dz ")
                .Replace("_ ", " ")
                .Split(' ')
                .Reverse());
        }

        public static int[] StateToArray(int[][] state)
        {
            return state[0]
                 .Concat(state[1])
                 .Concat(state[2])
                 .Concat(state[3])
                 .Concat(state[4])
                 .Concat(state[5])
                 .ToArray()
                ;
        }
    }
}