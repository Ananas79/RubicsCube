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
using Android.Hardware;
using Java.IO;
using Android.Util;
using Android.Graphics;
using System.IO;

namespace App5
{
    public class CameraPreview : SurfaceView, ISurfaceHolderCallback
    {
        Android.Hardware.Camera camera;
        Context context;
        public Activity a;
        public CameraViewOutput output;

        public CameraPreview(Context context, Android.Hardware.Camera camera) : base(context)
        {
            this.camera = camera;
            this.context = context;

            //Surface holder callback is set so theat SurfaceChanged, Created, destroy... 
            //Could be called from here.
            Holder.AddCallback(this);
            
            
        }

        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Android.Graphics.Format format, int width, int height)
        {
            
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            try
            {
                camera.SetPreviewDisplay(holder);
                camera.SetDisplayOrientation(90);
                camera.SetPreviewCallback(new MyPreviewCallback()
                {
                    v = output
                });
                camera.StartPreview();
            }
            catch (Java.IO.IOException e)
            {
                throw e;
            }
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            camera.StopPreview();
        }
    }

    class MyPreviewCallback : Java.Lang.Object, Android.Hardware.Camera.IPreviewCallback
    {
        public CameraViewOutput v;
        public void OnPreviewFrame(byte[] data, Android.Hardware.Camera camera)
        {
            byte[] jpegData = ConvertYuvToJpeg(data, camera);
            v.DrawBitmap(bytesToBitmap(jpegData));
        }

        private byte[] ConvertYuvToJpeg(byte[] yuvData, Android.Hardware.Camera camera)
        {
            var cameraParameters = camera.GetParameters();
            var width =  cameraParameters.PreviewSize.Width;
            var height = cameraParameters.PreviewSize.Height;
            var yuv = new YuvImage(yuvData, cameraParameters.PreviewFormat, width, height, null);
            var ms = new MemoryStream();
            var quality = 80;   // adjust this as needed
            yuv.CompressToJpeg(new Rect(0, 0, width, height), quality, ms);
            var jpegData = ms.ToArray();

            return jpegData;
        }

        public static Bitmap bytesToBitmap(byte[] imageBytes)
        {
            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length, new BitmapFactory.Options() { InMutable = true });

            return bitmap;
        }
    }
}