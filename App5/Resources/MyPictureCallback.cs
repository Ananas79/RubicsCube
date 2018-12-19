using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using static Android.Hardware.Camera;

namespace App5.Resources
{
    class MyPictureCallback : Java.Lang.Object, IPictureCallback
    {
        public void OnPictureTaken(byte[] data, Camera camera)
        {

        }
    }
}