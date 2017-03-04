using Android.App;
using Android.Widget;
using Android.OS;
using MjpegProcessor;
using System;
using Android.Graphics;
using Media;
using Media.Rtsp;

namespace SkypathVR
{
    [Activity(Label = "Skypath VR", MainLauncher = true, Icon = "@drawable/temp_logo")]
    public class MainActivity : Activity
    {
        private ImageView output;
        private Bitmap decoded;
        private RtspClient client;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            output = FindViewById<ImageView>(Resource.Id.OutputImage);

            //rtsp://mpv.cdn3.bigCDN.com:554/bigCDN/definst/mp4:bigbuckbunnyiphone_400.mp4
            client = new RtspClient(new Uri("rtsp://mpv.cdn3.bigCDN.com:554/bigCDN/definst/mp4:bigbuckbunnyiphone_400.mp4"));
            //client.Connect();
            client.Connect();
            client.Play();
            client.OnResponse += Client_OnResponse;

            /*
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
            */
        }

        protected override void OnDestroy()
        {
            client.StopPlaying();
            client.Disconnect();

            base.OnDestroy();
        }

        private async void Client_OnResponse(RtspClient sender, RtspMessage request, RtspMessage response)
        {
            byte[] frame = response.ToBytes();
            decoded = await BitmapFactory.DecodeByteArrayAsync(frame, 0, frame.Length);
            output.SetImageBitmap(decoded);
        }
    }
}

