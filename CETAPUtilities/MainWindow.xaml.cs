using FirstFloor.ModernUI.Windows.Controls;
using CETAPUtilities.ViewModel;

namespace CETAPUtilities
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            InitializeComponent();
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
