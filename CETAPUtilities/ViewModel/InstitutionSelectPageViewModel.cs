using System;
using CETAPUtilities.Model;
using GalaSoft.MvvmLight;

namespace CETAPUtilities.ViewModel
	{
	/// <summary>
	/// This class contains properties that a View can data bind to.
	/// <para>
	/// See http://www.galasoft.ch/mvvm
	/// </para>
	/// </summary>
	public class InstitutionSelectPageViewModel :InstitutionMatchPageViewModelBase
		{
		private ICompositeService _service;
		/// <summary>
		/// Initializes a new instance of the InstitutionSelectPageViewModel class.
		/// </summary>
		public InstitutionSelectPageViewModel(InstitutionMatch institutionMatch,ICompositeService CompositeService)
			:base(institutionMatch)
			{
				_service = CompositeService;
			}

		public override string DisplayName
			{
			get
				{
				throw new NotImplementedException();
				}
			}

		internal override bool IsValid()
			{
			throw new NotImplementedException();
			}
		}
	}