using FirstFloor.ModernUI.Windows.Controls;
using CETAPUtilities.ViewModel;
using CETAPUtilities.Helper;


namespace CETAPUtilities
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();
        
        public MainWindow()
        {
            
            InitializeComponent();
            //log.Warn("Application started");
            SettingsAppearanceViewModel settings = new SettingsAppearanceViewModel();

            settings.SetThemeAndColor(ApplicationSettings.Default.SelectedThemeDisplayName,
                ApplicationSettings.Default.SelectedThemeSource,
                ApplicationSettings.Default.SelectedAccentColor,
                ApplicationSettings.Default.SelectedFontSize);

            ApplicationSettings.Default.Save();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }
    }
}
