using Android.App;
using Android.Widget;
using Android.OS;
using MjpegProcessor;
using System;
using System.IO;
using Android.Graphics.Drawables;
using Android.Graphics;

namespace SkypathVR
{
    [Activity(Label = "Skypath VR", MainLauncher = true, Icon = "@drawable/temp_logo")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            ImageView output = FindViewById<ImageView>(Resource.Id.OutputImage);
            
            var decoder = new MjpegDecoder();
            decoder.ParseStream(new System.Uri("http://webcam5.mmto.arizona.edu/mjpg/video.mjpg"));
            Bitmap inter;
            decoder.FrameReady += delegate (object sender, FrameReadyEventArgs FB)
            {
                //Stream inter = new MemoryStream(FB.FrameBuffer);
                inter = BitmapFactory.DecodeByteArray(FB.FrameBuffer, 0, FB.FrameBuffer.Length);
                //output.SetImageDrawable(Drawable.CreateFromStream(inter, "itemp"));
                output.SetImageBitmap(inter);
                
            };
        }
    }
}

