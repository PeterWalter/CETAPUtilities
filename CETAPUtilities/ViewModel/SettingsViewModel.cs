using GalaSoft.MvvmLight;
using FirstFloor.ModernUI.Presentation;
using System;

namespace CETAPUtilities.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class SettingsViewModel : ViewModelBase
    {
        /// <summary>
        /// The <see cref="Links" /> property's name.
        /// </summary>
        public const string LinksPropertyName = "Links";

        private LinkCollection _mylinks = new LinkCollection();

        /// <summary>
        /// Sets and gets the Links property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public LinkCollection Links
        {
            get
            {
                return _mylinks;
            }

            set
            {
                if (_mylinks == value)
                {
                    return;
                }


                _mylinks = value;
                RaisePropertyChanged(LinksPropertyName);
            }
        }

        /// <summary>
        /// Initializes a new instance of the SettingsViewModel class.
        /// </summary>
        public SettingsViewModel()
        {
            InitializeModels();
        }

        private void InitializeModels()
        {
            // setup the data services


            var Link1 = new Link { DisplayName = "Settings", Source = new Uri("View/SettingsAppearanceView.xaml", UriKind.Relative) };
            _mylinks.Add(Link1);


        }
    }
}