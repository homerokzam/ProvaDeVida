﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using Acr.UserDialogs;
using Prism.Commands;
using Prism.Common;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;

using ProvaDeVida.Models;
using ProvaDeVida.Dependencies;

namespace ProvaDeVida.ViewModels
{
  public class MainViewModel : BindableBase, INavigationAware, IPageAware
  {
    public MainViewModel (INavigationService navigationService, IPageDialogService pageDialogService)
    {
      _navigationService = navigationService;
      _pageDialogService = pageDialogService;

      Debug.WriteLine ("MailViewModel");
      OnFindSimilarFaceCommand = new DelegateCommand(async () => FindSimilarFaceCommandAsync());

      Employees = new ObservableCollection<Employee>
      {
        new Employee { Name = "Nat Friedman", Title = "CEO", PhotoUrl = "http://static4.businessinsider.com/image/559d359decad04574c42a3c4-480/xamarin-nat-friedman.jpg" },
        new Employee { Name = "Miguel de Icaza", Title = "CTO", PhotoUrl = "http://images.techhive.com/images/idge/imported/article/nww/2011/03/031111-deicaza-100272676-orig.jpg" },
        new Employee { Name = "Joseph Hill", Title = "VP of Developer Relations", PhotoUrl = "https://www.gravatar.com/avatar/f763ec6935726b7f7715808828e52223.jpg?s=256" },
        new Employee { Name = "James Montemagno", Title = "Developer Evangelist", PhotoUrl = "http://www.gravatar.com/avatar/7d1f32b86a6076963e7beab73dddf7ca?s=256" },
        new Employee { Name = "Pierce Boggan", Title = "Software Engineer", PhotoUrl = "https://avatars3.githubusercontent.com/u/1091304?v=3&s=460" },
      };
    }

    private INavigationService _navigationService;
    private IPageDialogService _pageDialogService;

    private bool _isBusy;

    public bool IsBusy
    {
      get { return _isBusy; }
      set { SetProperty(ref _isBusy, value); }
    }

    public ObservableCollection<Employee> Employees { get; set; }

    public DelegateCommand OnFindSimilarFaceCommand { get; private set; }

    public Page Page { get; set; }

    public void OnNavigatedFrom (NavigationParameters parameters)
    {
    }

    public void OnNavigatedTo (NavigationParameters parameters)
    {
    }

    private async Task FindSimilarFaceCommandAsync()
    {
      //Xamarin.Forms.DependencyService.Get<INativePages>().ShowDigits();
      Page.Navigation.PushModalAsync(new Views.DigitsPage());
    }

    private async Task FindSimilarFaceCommandAsync1()
    {
      if (IsBusy)
        return;

      IsBusy = true;

      try
      {
        MediaFile photo;

        await CrossMedia.Current.Initialize();

        // Take photo
        if (CrossMedia.Current.IsCameraAvailable)
        {
          photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
          {
            Directory = "Employee Directory",
            Name = "photo.jpg"
          });
        }
        else
        {
          photo = await CrossMedia.Current.PickPhotoAsync();
        }

        // Upload to cognitive services
        using (var stream = photo.GetStream())
        {
          //var faceServiceClient = new FaceServiceClient("22e49721a20e457880a32138afc9e027");

          //// Step 4 - Upload our photo and see who it is!
          //var faces = await faceServiceClient.DetectAsync(stream);
          //var faceIds = faces.Select(face => face.FaceId).ToArray();

          //var results = await faceServiceClient.IdentifyAsync(personGroupId, faceIds);
          //var result = results[0].Candidates[0].PersonId;

          //var person = await faceServiceClient.GetPersonAsync(personGroupId, result);
          var person = new Employee { Name = "Homero" };

          UserDialogs.Instance.ShowSuccess($"Person identified is {person.Name}.");
        }
      }
      catch (Exception ex)
      {
        UserDialogs.Instance.ShowError(ex.Message);
      }
      finally
      {
        IsBusy = false;
      }
    }

    //async Task RegisterEmployees()
    //{
    //  var faceServiceClient = new FaceServiceClient("22e49721a20e457880a32138afc9e027");

    //  // Step 1 - Create Face List
    //  personGroupId = Guid.NewGuid().ToString();
    //  await faceServiceClient.CreatePersonGroupAsync(personGroupId, "Xamarin Employees");

    //  // Step 2 - Add people to face list
    //  foreach (var employee in Employees)
    //  {
    //    var p = await faceServiceClient.CreatePersonAsync(personGroupId, employee.Name);
    //    await faceServiceClient.AddPersonFaceAsync(personGroupId, p.PersonId, employee.PhotoUrl);
    //  }

    //  // Step 3 - Train face group
    //  await faceServiceClient.TrainPersonGroupAsync(personGroupId);
    //}
  }
}