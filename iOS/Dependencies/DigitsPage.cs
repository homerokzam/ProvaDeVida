using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using Foundation;
using MonoTouch.Fabric.DigitsKit;
using UIKit;

using ProvaDeVida.Views;

[assembly: ExportRenderer(typeof(DigitsPage), typeof(ProvaDeVida.iOS.Dependencies.DigitsPageRenderer))]
namespace ProvaDeVida.iOS.Dependencies
{
  public class DigitsPageRenderer : PageRenderer
  {
    protected override void OnElementChanged(VisualElementChangedEventArgs e)
    {
      base.OnElementChanged(e);

      var page = e.NewElement as DigitsPage;
      var hostViewController = ViewController;
      var viewController = new UIViewController();

      var authButton = DGTAuthenticateButton.ButtonWithAuthenticationCompletion((DGTSession session, NSError error) =>
          {
            if (session != null && !string.IsNullOrEmpty(session.UserID))
            {
              // TODO: associate the session userID with your user model
              var msg = string.Format("Phone number: {0}", session.PhoneNumber);
              var alert = new UIAlertView("You are logged in!", msg, null, "OK", null);
              alert.Show();
            }
            else if (error != null)
              Console.WriteLine(string.Format("Authentication error: {0}", error.LocalizedDescription));
          });

      authButton.Center = View.Center;
      viewController.View.AddSubview(authButton);

      hostViewController.AddChildViewController(viewController);
      hostViewController.View.Add(viewController.View);

      viewController.DidMoveToParentViewController(hostViewController);
    }

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      //var view = new SignInViewController(this.Handle);
      //View.AddSubviews(view);
    }
  }
}