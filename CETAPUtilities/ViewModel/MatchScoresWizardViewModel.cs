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

		public RelayCommand CancelCommand { get; private set; }
		public RelayCommand MoveNextCommand { get; private set; }
		public RelayCommand MovePreviousCommand { get; private set; }
		

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

			}
		private void RegisterCommands()
			{
				CancelCommand = new RelayCommand(() => CancelProcess());
				MoveNextCommand = new RelayCommand(() => NextPage());
				MovePreviousCommand = new RelayCommand(() => PreviousPage());
			}
			private void CancelProcess()
			{

			}
			private void NextPage()
			{

			}
			private void PreviousPage()
			{

			}
		}
	}