using GalaSoft.MvvmLight;
using FirstFloor.ModernUI.Presentation;
using CETAPUtilities.Model;
using CETAPUtilities.BDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CETAPUtilities.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// The <see cref="TitleLink" /> property's name.
        /// </summary>
        public const string TitleLinkPropertyName = "TitleLink";

        private LinkCollection _mytitleLinks;

        /// <summary>
        /// Sets and gets the TitleLinks property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public LinkCollection TitleLink
        {
            get
            {
                return _mytitleLinks;
            }

            set
            {
                if (_mytitleLinks == value)
                {
                    return;
                }

                _mytitleLinks = value;
                RaisePropertyChanged(TitleLinkPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="MenuLinkGroups" /> property's name.
        /// </summary>
        public const string MenuLinkGroupsPropertyName = "MenuLinkGroups";

        private LinkGroupCollection _mymenulinks;

        /// <summary>
        /// Sets and gets the MenuLinkGroups property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public LinkGroupCollection MenuLinkGroups
        {
            get
            {
                return _mymenulinks;
            }

            set
            {
                if (_mymenulinks == value)
                {
                    return;
                }

                _mymenulinks = value;
                RaisePropertyChanged(MenuLinkGroupsPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="Links" /> property's name.
        /// </summary>
        public const string LinksPropertyName = "Links";

        private LinkCollection _mylinks;

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
        /// The <see cref="User" /> property's name.
        /// </summary>
        public const string UserPropertyName = "User";

        private UserBDO _myuser;

        /// <summary>
        /// Sets and gets the User property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public UserBDO User
        {
            get
            {
                return _myuser;
            }

            set
            {
                if (_myuser == value)
                {
                    return;
                }

                _myuser = value;
                RaisePropertyChanged(UserPropertyName);
            }
        }


        private IDataService _service;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService Service)
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
                _service = Service;
                string person = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                User = new UserBDO();
                if (person == "WF\\01376340")
                {
                    User.Name = "Admin";
                    //User.Areas = "012345-1234-1234-1234-1234-1234-1234";
                }
                else
                {
                    List<UserBDO> persons = new List<UserBDO>();
                    persons = _service.GetAllUsers();
                    User = persons.Where(m => m.StaffID == person.Substring(3)).Select(v => v).FirstOrDefault();

                }
                InitializeModels();
            }
        }
        private void InitializeModels()
        {
            this.MenuLinkGroups = new LinkGroupCollection();
            this.TitleLink = new LinkCollection();
            this.CreateTitles();
            this.CreateMenu();
        }
        private void CreateTitles()
        {
            this.TitleLink.Clear();
            string user1 = "Logged in as: " + User.ToString();
            var link = new Link { DisplayName = user1, Source = new Uri("/View/introduction/IntroView.xaml", UriKind.Relative) };
            TitleLink.Add(link);
            var link1 = new Link { DisplayName = "settings", Source = new Uri("/View/SettingsView.xaml", UriKind.Relative) };
            TitleLink.Add(link1);
        }
        private void CreateMenu()
        {
            this.MenuLinkGroups.Clear();
            var linkGroupAll = new LinkGroup { DisplayName = "Welcome" };
            var link1 = new Link { DisplayName = "Introduction", Source = new Uri("/View/introduction/IntroView.xaml", UriKind.Relative) };

            linkGroupAll.Links.Add(link1);
            var link2 = new Link { DisplayName = "Instructions", Source = new Uri("/View/introduction/InstructView.xaml", UriKind.Relative) };
            // var link3 = new Link { DisplayName = "Map", Source = new Uri("/View/writers/VenueMapView.xaml", UriKind.Relative) };
            linkGroupAll.Links.Add(link2);
            // linkGroupAll.Links.Add(link3);

            this.MenuLinkGroups.Add(linkGroupAll);

            //map all required modules and links
            int numberOfModules = 2; // change number of modules as required
            Link Linkx = null;

            for (int i = 0; i < numberOfModules; i++)
            {
                switch (i)
                {
                    case 0: // The Group
                        var First = new LinkGroup { DisplayName = "Utilities" };
                        string[] module = new string[] { "aaa", "b", "c", "d" };
                        foreach (var m in module)
                        {
                            switch (m)
                            {
                                case "aaa":
                                    Linkx = new Link { DisplayName = "Composite", Source = new Uri("/View/CompositeView.xaml", UriKind.Relative) };
                                    First.Links.Add(Linkx);
                                    break;
                                case "b":
                                    Linkx = new Link { DisplayName = "Subdomain", Source = new Uri("/View/SubdomainView.xaml", UriKind.Relative) };
                                    First.Links.Add(Linkx);
                                    break;
                                case "c":
                                    Linkx = new Link { DisplayName = "Extras", Source = new Uri("/View/OtherView.xaml", UriKind.Relative) };
                                    First.Links.Add(Linkx);
                                    break;

                            }
                        }
                        this.MenuLinkGroups.Add(First);
                        break;

                    //case 1: // The Group
                    //    var Second = new LinkGroup { DisplayName = "Subdomains" };
                    //    string[] module1 = new string[] { "aaa", "b", "c", "d" };
                    //    foreach (var m in module1)
                    //    {
                    //        switch (m)
                    //        {
                    //            case "aaa":
                    //                Linkx = new Link { DisplayName = "Clean Directory", Source = new Uri("/View/DirectoryView.xaml", UriKind.Relative) };
                    //                Second.Links.Add(Linkx);
                    //                break;
                    //            case "b":
                    //                Linkx = new Link { DisplayName = "Load Data", Source = new Uri("/View/SubdomainView.xaml", UriKind.Relative) };
                    //                Second.Links.Add(Linkx);
                    //                break;
                          

                    //        }
                    //    }
                    //    this.MenuLinkGroups.Add(Second);
                    //    break;

                  
                }
            }
        }

    }
}