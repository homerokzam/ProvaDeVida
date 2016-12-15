using System;
using System.Collections.Generic;

using Prism.Common;
using Xamarin.Forms;

namespace ProvaDeVida.Views
{
  public partial class Main : ContentPage
  {
    public Main()
    {
      this.BindingContextChanged += (sender, args) =>
      {
        var pageAware = this.BindingContext as IPageAware;
        if (pageAware != null)
        {
          pageAware.Page = this;
        }
      };
      
      InitializeComponent();
    }
  }
}
