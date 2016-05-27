using GalaSoft.MvvmLight;
using CETAPUtilities.Model;
using GalaSoft.MvvmLight.CommandWpf;

namespace CETAPUtilities.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class DirectoryViewModel : ViewModelBase
    {
        public RelayCommand SelectFolderCommand { get; private set; }
        public RelayCommand ReadFilesCommand { get; private set; }
        public RelayCommand ProcessScoresCommand { get; private set; }

        private IDataService _service;
        /// <summary>
        /// Initializes a new instance of the DirectoryViewModel1 class.
        /// </summary>
        public DirectoryViewModel(IDataService Service)
        {
            _service = Service;
            InitializeModels();
            RegisterCommands();
        }

        private void InitializeModels()
        {

        }

        private void RegisterCommands()
        {

        }
    }
}