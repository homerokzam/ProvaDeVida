using System;

using MonoTouch.Fabric.DigitsKit;
using UIKit;
using Xamarin.Forms;

using ProvaDeVida.Dependencies;

[assembly: Dependency(typeof(ProvaDeVida.iOS.Dependencies.NativePages))]
namespace ProvaDeVida.iOS.Dependencies
{
  public class NativePages : INativePages
  {
    public void ShowDigits()
    {
      UIViewController root = UIApplication.SharedApplication.KeyWindow.RootViewController;
      var view = new SignInViewController(root.Handle);
      root.PresentViewController(view, true, null);
    }
  }
}