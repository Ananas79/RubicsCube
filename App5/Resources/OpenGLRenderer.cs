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
using Javax.Microedition.Khronos.Egl;
using Javax.Microedition.Khronos.Opengles;
using static Android.Opengl.GLSurfaceView;
using static Android.Opengl.GLES20;
using static Android.Opengl.GLU;

namespace App5
{
    public class OpenGLRenderer : Java.Lang.Object, IRenderer
    {
        public double alpha, beta;

        public OpenGLRenderer()
        {
            GlEnable(GlDepthTest);
        }

        public void OnDrawFrame(IGL10 gl)
        {
            GlClear(GlColorBufferBit|GlDepthBufferBit);
        }
        

        public void OnSurfaceChanged(IGL10 gl, int width, int height)
        {
            GlViewport(0,0,width,height);
        }

        public void OnSurfaceCreated(IGL10 gl, EGLConfig config)
        {
            GlClearColor(0f,1f,1f,1f);
        }
    }
}