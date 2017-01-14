using Android.App;
using Android.Widget;
using Android.OS;

namespace SkypathVR
{
    [Activity(Label = "SkypathVR", MainLauncher = true, Icon = "@drawable/temp_logo")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
        }
    }
}

