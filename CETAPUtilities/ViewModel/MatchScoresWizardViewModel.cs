using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CETAPUtilities.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace CETAPUtilities.ViewModel
	{
	/// <summary>
	/// This class contains properties that a View can data bind to.
	/// <para>
	/// See http://www.galasoft.ch/mvvm
	/// </para>
	/// </summary>
	public class MatchScoresWizardViewModel :ViewModelBase
		{

        public event EventHandler RequestClose;

        public RelayCommand CancelCommand { get; private set; }
		public RelayCommand MoveNextCommand { get; private set; }
		public RelayCommand MovePreviousCommand { get; private set; }
        //ReadOnlyCollection<InstitutionMatchPageViewModelBase> _pages;
        //InstitutionMatchPageViewModelBase _currentPage;

        /// <summary>
            /// The <see cref="Pages" /> property's name.
            /// </summary>
        public const string PagesPropertyName = "Pages";

        private ReadOnlyCollection<InstitutionMatchPageViewModelBase> _pages;

        /// <summary>
        /// Sets and gets the Pages property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ReadOnlyCollection<InstitutionMatchPageViewModelBase> Pages
            {
            get
                {
                if (_pages == null)
                    this.CreatePages();

                return _pages;
                }

            set
                {
                if (_pages == value)
                    {
                    return;
                    }

                _pages = value;
                RaisePropertyChanged(PagesPropertyName);
                }
            }

        /// <summary>
        /// The <see cref="InstitutionMatch" /> property's name.
        /// </summary>
        public const string InstitutionMatchPropertyName = "InstitutionMatch";

        private InstitutionMatch _institutionMatch;
        private ICompositeService _service;

        /// <summary>
        /// Sets and gets the InstitutionMatch property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public InstitutionMatch InstitutionMatch
            {
            get
                {
                return _institutionMatch;
                }

            set
                {
                if (_institutionMatch == value)
                    {
                    return;
                    }

                _institutionMatch = value;
                RaisePropertyChanged(InstitutionMatchPropertyName);
                }
            }

        /// <summary>
            /// The <see cref="CurrentPage" /> property's name.
            /// </summary>
        public const string CurrentPagePropertyName = "CurrentPage";

        private InstitutionMatchPageViewModelBase _currentPage;

        int CurrentPageIndex
            {
                get
                {
                if (this.CurrentPage == null)
                    {
                    Debug.Fail("Why is the current page null?");
                    return -1;
                    }
                return this.Pages.IndexOf(this.CurrentPage);

                }
            }
        /// <summary>
        /// Sets and gets the CurrentPage property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public InstitutionMatchPageViewModelBase CurrentPage
            {
            get
                {
                return _currentPage;
                }

            set
                {
                if (_currentPage == value)
                    {
                    return;
                    }

                _currentPage = value;
                RaisePropertyChanged(CurrentPagePropertyName);
                }
            }
        
        
        
        /// <summary>
		/// Initializes a new instance of the MatchScoresWizardViewModel class.
		/// </summary>
		public MatchScoresWizardViewModel()
			{
			InitializeModels();
			RegisterCommands();
			}

		private void InitializeModels()
			{
                _institutionMatch = new InstitutionMatch();
                this.CurrentPage = this.Pages[0];
			}
		private void RegisterCommands()
			{
				CancelCommand = new RelayCommand(() => CancelProcess());
				MoveNextCommand = new RelayCommand(() => NextPage());
				MovePreviousCommand = new RelayCommand(() => PreviousPage(), () => CanMoveToPreviousPage());
			}

         void CreatePages()
            {
            var SelectInstVM = new InstitutionSelectPageViewModel(this.InstitutionMatch, this._service);
            var pages = new List<InstitutionMatchPageViewModelBase>();


            pages.Add(new WelcomePageViewModel());
            }
			private void CancelProcess()
			{
                InstitutionMatch = null;
                this.OnRequestClose();
			}
			private void NextPage()
			{

			}
        private bool CanMoveToPreviousPage()
            {
            if (this.CanMoveToPreviousPage)
                this.CurrentPage = this.Pages[this.CurrentPageIndex - 1];
            }
			private void PreviousPage()
			{

			}

            void OnRequestClose()
            {
                EventHandler handler = this.RequestClose();
                if (handler != null)
                    handler(this, EventArgs.Empty);
            }
		}
	}