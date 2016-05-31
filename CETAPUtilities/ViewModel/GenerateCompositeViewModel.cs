using GalaSoft.MvvmLight;
using CETAPUtilities.Model;
using GalaSoft.MvvmLight.CommandWpf;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using log4net;
using CETAPUtilities.Database;
using System;
using System.Collections.Generic;
using CETAPUtilities.BDO;
using System.Collections.ObjectModel;

namespace CETAPUtilities.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class GenerateCompositeViewModel : ViewModelBase
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public RelayCommand GenerateCompositeCommand { get; private set; }
        public RelayCommand GenerateLogisticsCompositeCommand { get; private set; }
        //public RelayCommand ProcessScoresCommand { get; private set; }


        /// <summary>
            /// The <see cref="Composite" /> property's name.
            /// </summary>
        public const string CompositePropertyName = "Composite";

        private ObservableCollection<CompositBDO> _composite;

        /// <summary>
        /// Sets and gets the Composite property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<CompositBDO> Composite
        {
            get
            {
                return _composite;
            }

            set
            {
                if (_composite == value)
                {
                    return;
                }

                _composite = value;
                RaisePropertyChanged(CompositePropertyName);
            }
        }
        /// <summary>
            /// The <see cref="Periods" /> property's name.
            /// </summary>
        public const string PeriodsPropertyName = "Periods";

        private List<IntakeYearsBDO> _periods;

        /// <summary>
        /// Sets and gets the Periods property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<IntakeYearsBDO> Periods
        {
            get
            {
                return _periods;
            }

            set
            {
                if (_periods == value)
                {
                    return;
                }

                _periods = value;
                RaisePropertyChanged(PeriodsPropertyName);
            }
        }

        /// <summary>
            /// The <see cref="SelectedPeriod" /> property's name.
            /// </summary>
        public const string SelectedPeriodPropertyName = "SelectedPeriod";

        private IntakeYearsBDO _selectedPeriod;

        /// <summary>
        /// Sets and gets the SelectedPeriod property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public IntakeYearsBDO SelectedPeriod
        {     
            get
            {
                return _selectedPeriod;
            }

            set
            {
                if (_selectedPeriod == value)
                {
                    return;
                }

                _selectedPeriod = value;
                GetComposite();
                RaisePropertyChanged(SelectedPeriodPropertyName);
            }
        }
        
        /// <summary>
            /// The <see cref="DataVisible" /> property's name.
            /// </summary>
        public const string DataVisiblePropertyName = "DataVisible";

        private bool _DataVisible;

        /// <summary>
        /// Sets and gets the DataVisible property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool DataVisible
        {
            get
            {
                return _DataVisible;
            }

            set
            {
                if (_DataVisible == value)
                {
                    return;
                }

                _DataVisible = value;
                GenerateCompositeCommand.RaiseCanExecuteChanged();
                GenerateLogisticsCompositeCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged(DataVisiblePropertyName);
            }
        }
        /// <summary>
            /// The <see cref="IsLoaded" /> property's name.
            /// </summary>
        public const string IsLoadedPropertyName = "IsLoaded";

        private bool _isloaded = false;

        /// <summary>
        /// Sets and gets the IsLoaded property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsLoaded
        {
            get
            {
                return _isloaded;
            }

            set
            {
                if (_isloaded == value)
                {
                    return;
                }

                _isloaded = value;
                RaisePropertyChanged(IsLoadedPropertyName);
               
            }
        }    
            /// <summary>
            /// The <see cref="Folder" /> property's name.
            /// </summary>
        public const string FolderPropertyName = "Folder";

        private string _myfolder;

        /// <summary>
        /// Sets and gets the Folder property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Folder
        {
            get
            {
                return _myfolder;
            }

            set
            {
                if (_myfolder == value)
                {
                    return;
                }

                _myfolder = value;
                RaisePropertyChanged(FolderPropertyName);
            }
        }
        private IDataService _service;
        private ICompositeService _cservice;
        /// <summary>
        /// Initializes a new instance of the GenerateCompositeViewModel class.
        /// </summary>
        public GenerateCompositeViewModel(IDataService Service, ICompositeService CService)
        {
            _service = Service;
            _cservice = CService;
            InitializeModels();
            RegisterCommands();
        }

        private async void InitializeModels()
        {
            Periods = _service.GetAllIntakeYears();
          
        }
      
        private async Task RefreshAsync()
        {
            IsLoaded = true;
           
           // DataVisible = await _service.GetAllNBTScoresAsync(_selectedPeriod);
            if (DataVisible)  IsLoaded=false;
            
          
        }

        private void RegisterCommands()
        {
            GenerateCompositeCommand = new RelayCommand(() => GenCompositeAsync(), () => canRun());
            GenerateLogisticsCompositeCommand = new RelayCommand(() => LogisticsCompAsync(), () => canRun());
        }

        private bool canRun()
        {
            return DataVisible;
        }

        private async void GenCompositeAsync()
        {
            Completed = false;
            DataVisible = false;
            selectFolder();

            Completed =  await _service.SaveLargeExcelAsync(Folder, Composite, _selectedPeriod);
            DataVisible = true;

        }

        private bool Completed; // check to see if generation is complete

        private async void LogisticsCompAsync()
        {
            Completed = false;
            DataVisible = false;
            selectFolder();
            Completed = await _service.SaveLargeExcel2Async(Folder, Composite, _selectedPeriod);
            DataVisible = true;
        }
        private void selectFolder()
        {

          
            var dialog = new FolderBrowserDialog();
            dialog.SelectedPath = ApplicationSettings.Default.CompositeFolder;
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Folder = dialog.SelectedPath;
                //To Add to MRU list     
                //_mruManager.Add(path);
                //mruList.Add(path);
                //Folder = path;
                ApplicationSettings.Default.CompositeFolder = Folder;
                ApplicationSettings.Default.Save();
            }
        }

        private async void GetComposite()
        {
            DataVisible = false;
            IsLoaded = true;
            DateTime myDate = Convert.ToDateTime("2016/04/16");
           
            Composite = new ObservableCollection<CompositBDO>();
            if (_selectedPeriod.Year == 2017)
            {
                var temp = await _cservice.GetAllNBTScoresAsync(_selectedPeriod);
                var temp1 = temp.Where(x => ( x.VenueCode != 40101) && (x.DOT != myDate)).Select(x => x).ToList();
                Composite = new ObservableCollection<CompositBDO>(temp1);
            }
            else if (_selectedPeriod.Year == 2016)
            {
                Composite = await _cservice.GetAllNBTScoresAsync(_selectedPeriod);
                var MyPer = new IntakeYearsBDO();
                MyPer = _selectedPeriod;
                MyPer.yearStart = myDate;
                MyPer.yearEnd = myDate;
                var temp = await _cservice.GetAllNBTScoresAsync(MyPer);
               
                foreach(var item in temp)
                {
                    Composite.Add(item);
                }
            }
            else
            {
                Composite = await _cservice.GetAllNBTScoresAsync(_selectedPeriod);
            }
           
            DataVisible = true;
            IsLoaded = false;
        }
    }
}