using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.IO;
using Android.Provider;
using Android.Hardware;
using Android.Views;
using Android.Graphics;
using System;
using Android.Util;
using Android.Opengl;

namespace App5
{

    [Activity(Label = "App5")]
    public class MainActivity : Activity
    {
        CameraPreview preview;
        CameraViewElement mask;
        CameraViewOutput photoOutput;
        Android.Hardware.Camera mCamera;
        AbsoluteLayout mFrame;
        Context mContext;
        Button b;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            RequestWindowFeature(WindowFeatures.NoTitle);
            Window.SetFlags(WindowManagerFlags.Fullscreen,
                WindowManagerFlags.Fullscreen);
            SetContentView(Resource.Layout.Main);
            
            mFrame = FindViewById<AbsoluteLayout>(Resource.Id.layout); //4 
            mContext = this;

            mCamera = Android.Hardware.Camera.Open(0); //1
            if (mCamera == null)
            { //2
                Toast.MakeText(this, "Opening camera failed", ToastLength.Long).Show();
                return;
            }
            mask = new CameraViewElement(this)
            {
                Clickable = true
            };
            mask.Click += OnMaskClick;

            photoOutput = new CameraViewOutput(this)
            {
                CubeSize = mask.Size 
            };

            mask.SetOutput(photoOutput);

            preview = new CameraPreview(this, mCamera) {
                a = this,
                output = photoOutput,
                Clickable = true
            };

            mask.LayoutParameters = new AbsoluteLayout.LayoutParams(
                    ViewGroup.LayoutParams.WrapContent,
                    ViewGroup.LayoutParams.WrapContent,
                    0, 
                    0
            );


            photoOutput.LayoutParameters = new AbsoluteLayout.LayoutParams(
                    300,
                    533,
                    0,
                    0
            ); ;
            mFrame = FindViewById<AbsoluteLayout>(Resource.Id.layout); //4 

            mFrame.AddView(preview);
            mFrame.AddView(mask);
            mFrame.AddView(photoOutput);
            

            /*
            CubeAnimation cb = new CubeAnimation(this);
            cb.LayoutParameters = new AbsoluteLayout.LayoutParams(
                ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.MatchParent,
                0, 0
            );
            mFrame.AddView(cb);
            */

            /*
            mFrame.AddView(b = new Button(this) {
                LayoutParameters = new AbsoluteLayout.LayoutParams(
                    ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.WrapContent,
                    0, 0
                    ),
                Text = "Pick me"
            });
            b.Click += OnClick;
            */
        }

        public void OnMaskClick(object sender, EventArgs args)
        {
            photoOutput.SaveCurSide();
            mask.OnClick();
            if (photoOutput.IsAllSidesFilled)
            {
                Intent intent = new Intent(this, typeof(Cube));
                var temp = CubeSolver.StateToArray(photoOutput.GetState());
                intent.PutExtra("type", "photo");
                intent.PutExtra("state", temp);
                StartActivityForResult(intent, 3);
                Finish();
            }
            //mCamera.TakePicture(null, null, null, )
        }
    }

}

