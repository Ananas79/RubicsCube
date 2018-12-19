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
    public class Side
    {
        int[] colors;

        public int[] Colors { get => colors; set => colors = value; }
        public int Color { get => colors[4]; set => colors[4] = value; }

        public int this[int index]
        {
            get => Colors[index];
            set => Colors[index] = value;
        }

        public Side(int[] colors)
        {
            this.colors = colors;
        }
        public Side()
        {
            this.colors = new int[] { -1, -1, -1, -1, 0, -1, -1, -1, -1 };
        }

        public void Fill(int[] colors)
        {
            this.colors = colors;
        }
    }
}