using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Foundation;
using MonoTouch.Fabric;
using MonoTouch.Fabric.Crashlytics;
using MonoTouch.Fabric.DigitsKit;
using UIKit;

namespace ProvaDeVida.iOS
{
  [Register("AppDelegate")]
  public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
  {
    public override bool FinishedLaunching(UIApplication app, NSDictionary options)
    {
      global::Xamarin.Forms.Forms.Init();

      // Code for starting up the Xamarin Test Cloud Agent
#if ENABLE_TEST_CLOUD
      Xamarin.Calabash.Start();
#endif

      Initialize();

      LoadApplication(new App());

      return base.FinishedLaunching(app, options);
    }

    private void Initialize()
    {
      Setup.EnableCrashReporting(() =>
                {
                  var crashlytics = Crashlytics.SharedInstance;
                  crashlytics.DebugMode = true;
                  Crashlytics.StartWithAPIKey("9900a9ffe0a9f7df7aea242d2aa3bee673dfbe57");

                  Fabric.With(new NSObject[] { crashlytics });

                  AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
                  {
                    Setup.CaptureManagedInfo(e.ExceptionObject);
                    Setup.CaptureStackFrames(e.ExceptionObject);
                    Setup.ThrowExceptionAsNative(e.ExceptionObject);
                  };

                  TaskScheduler.UnobservedTaskException += (sender, e) =>
                  {
                    Setup.CaptureManagedInfo(e.Exception);
                    Setup.CaptureStackFrames(e.Exception);
                    Setup.ThrowExceptionAsNative(e.Exception);
                  };
                }, Path.GetFileNameWithoutExtension(typeof(AppDelegate).Module.Name));

      var digitsKit = Digits.SharedInstance;
      digitsKit.StartWithConsumerKey("WWez3qdQTeRpznFvqlIJzaUbr", "XPitCbheI9h9UOYydv9TiJiehz5u0GxZ2RaAwvxMDbFNOUajwg");

      Fabric.With(new NSObject[] { digitsKit });
    }
  }
}