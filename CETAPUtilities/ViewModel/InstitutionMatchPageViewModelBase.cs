using CETAPUtilities.Model;
using GalaSoft.MvvmLight;
using System.ComponentModel;

namespace CETAPUtilities.ViewModel
	{
	/// <summary>
	/// This class contains properties that a View can data bind to.
	/// <para>
	/// See http://www.galasoft.ch/mvvm
	/// </para>
	/// </summary>
	public abstract class InstitutionMatchPageViewModelBase :ViewModelBase
		{
		readonly InstitutionMatch _institutionMatch;
		

		/// <summary>
			/// The <see cref="IsCurrentPage" /> property's name.
			/// </summary>
		public const string IsCurrentPagePropertyName = "IsCurrentPage";

		private bool _isCurrentPage;

		/// <summary>
		/// Sets and gets the IsCurrentPage property.
		/// Changes to that property's value raise the PropertyChanged event. 
		/// </summary>
		public bool IsCurrentPage
			{
			get
				{
				return _isCurrentPage;
				}

			set
				{
				if (_isCurrentPage == value)
					{
					return;
					}

				_isCurrentPage = value;
				RaisePropertyChanged(IsCurrentPagePropertyName);
				}
			}
		/// <summary>
		/// Initializes a new instance of the InstitutionMatchPageViewModelBase class.
		/// </summary>
		protected InstitutionMatchPageViewModelBase(InstitutionMatch institutionMatch)
			{
				_institutionMatch = institutionMatch;
			}

		public InstitutionMatch InstitutionMatch
			{
				get
				{
					return _institutionMatch;
				}
			}
		public abstract string DisplayName { get; }
		internal abstract bool IsValid();
		
		}
	}