/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:CETAPUtilities"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using CETAPUtilities.ViewModel;
using CETAPUtilities.Model;

namespace CETAPUtilities.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
                SimpleIoc.Default.Register<IDataService, DesignDataService>();
            }
            else
            {
                // Create run time view services and models
                SimpleIoc.Default.Register<IDataService, DataService>();
                SimpleIoc.Default.Register<ICompositeService, CompositeService>();
            }

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<SettingsAppearanceViewModel>();
            SimpleIoc.Default.Register<DirectoryViewModel>();
            SimpleIoc.Default.Register<SubDomainsViewModel>();
            SimpleIoc.Default.Register<GenerateCompositeViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
          "CA1822:MarkMembersAsStatic",
          Justification = "This non-static member is needed for data binding purposes.")]
        public SettingsViewModel Settings
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
          "CA1822:MarkMembersAsStatic",
          Justification = "This non-static member is needed for data binding purposes.")]
        public SettingsAppearanceViewModel SettingsAppearance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsAppearanceViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
         "CA1822:MarkMembersAsStatic",
         Justification = "This non-static member is needed for data binding purposes.")]
        public DirectoryViewModel Directory
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DirectoryViewModel>();
            }
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
         "CA1822:MarkMembersAsStatic",
         Justification = "This non-static member is needed for data binding purposes.")]
        public SubDomainsViewModel subdomains
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SubDomainsViewModel>();
            }
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
         "CA1822:MarkMembersAsStatic",
         Justification = "This non-static member is needed for data binding purposes.")]
        public GenerateCompositeViewModel Composite
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GenerateCompositeViewModel>();
            }
        }


        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        // "CA1822:MarkMembersAsStatic",
        // Justification = "This non-static member is needed for data binding purposes.")]
        //public SettingsAppearanceViewModel SettingsAppearance
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<SettingsAppearanceViewModel>();
        //    }
        //}


        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}