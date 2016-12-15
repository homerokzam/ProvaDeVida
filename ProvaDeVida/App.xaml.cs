using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Prism.Unity;

using ProvaDeVida.Views;

namespace ProvaDeVida
{
  public partial class App : PrismApplication
  {
    protected override void OnInitialized()
    {
      InitializeComponent();
      NavigationService.NavigateAsync("/NavigationPage/Main");
    }

    protected override void RegisterTypes()
    {
      RegisterNavigation();
      RegisterServices();
    }

    void RegisterNavigation()
    {
      Container.RegisterTypeForNavigation<NavigationPage>();
      Container.RegisterTypeForNavigation<Main>();
    }

    void RegisterServices()
    {
    }
  }
}