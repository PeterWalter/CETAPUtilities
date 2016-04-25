using GalaSoft.MvvmLight;
using FirstFloor.ModernUI.Presentation;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System;
using System.ComponentModel;
using System.Linq;

namespace CETAPUtilities.ViewModel
{    /// <summary>
     /// This class contains properties that a View can data bind to.
     /// <para>
     /// See http://www.galasoft.ch/mvvm
     /// </para>
     /// </summary>
    public class SettingsAppearanceViewModel : ViewModelBase
    {
        private bool _colorLoadedYet;

        /// <summary>
        /// The <see cref="SelectedPalette" /> property's name.
        /// </summary>
        public const string SelectedPalettePropertyName = "SelectedPalette";

        private string _mySelectedPalette = "windows phone";

        /// <summary>
        /// Sets and gets the SelectedPalette property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SelectedPalette
        {
            get
            {
                return _mySelectedPalette;
            }

            set
            {
                if (_mySelectedPalette == value)
                {
                    return;
                }

                _mySelectedPalette = value;
                RaisePropertyChanged(AccentColorsPropertyName);
                this.SelectedAccentColor = this.AccentColors.FirstOrDefault();
                RaisePropertyChanged(SelectedPalettePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Palettes" /> property's name.
        /// </summary>
        public const string PalettesPropertyName = "Palettes";

        private ObservableCollection<string> _mypalettes = new ObservableCollection<string>
        {
            "metro","windows phone"
        };

        /// <summary>
        /// Sets and gets the Palettes property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<string> Palettes
        {
            get
            {
                return _mypalettes;
            }

            set
            {
                if (_mypalettes == value)
                {
                    return;
                }

                _mypalettes = value;

                RaisePropertyChanged(PalettesPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="SelectedMetroAccentColor" /> property's name.
        /// </summary>
        public const string SelectedMetroAccentColorPropertyName = "SelectedMetroAccentColor";

        private Color _myMetroColor;

        /// <summary>
        /// Sets and gets the SelectedMetroAccentColor property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Color SelectedMetroAccentColor
        {
            get
            {
                return _myMetroColor;
            }

            set
            {
                if (_myMetroColor == value)
                {
                    return;
                }

                _myMetroColor = value;
                RaisePropertyChanged(SelectedMetroAccentColorPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="AccentColors" /> property's name.
        /// </summary>
        public const string AccentColorsPropertyName = "AccentColors";

        private ObservableCollection<Color> _accentColors;

        /// <summary>
        /// Sets and gets the AccentColors property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<Color> AccentColors
        {
            get
            {
                return this.SelectedPalette == "metro" ? this.metroAccentColors : this.wpAccentColors;
            }

            set
            {
                if (_accentColors == value)
                {
                    return;
                }

                _accentColors = value;
                RaisePropertyChanged(AccentColorsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="metroAccentColors" /> property's name.
        /// </summary>
        public const string metroAccentColorsPropertyName = "metroAccentColors";

        private ObservableCollection<Color> _metroColor = new ObservableCollection<Color>
        {
            Color.FromRgb(0x33, 0x99, 0xff),   // blue
			Color.FromRgb(0x00, 0xab, 0xa9),   // teal
			Color.FromRgb(0x33, 0x99, 0x33),   // green
			Color.FromRgb(0x8c, 0xbf, 0x26),   // lime
			Color.FromRgb(0xf0, 0x96, 0x09),   // orange
			Color.FromRgb(0xff, 0x45, 0x00),   // orange red
			Color.FromRgb(0xe5, 0x14, 0x00),   // red
			Color.FromRgb(0xff, 0x00, 0x97),   // magenta
			Color.FromRgb(0xa2, 0x00, 0xff),   // purple   
		};

        /// <summary>
        /// Sets and gets the metroAccentColors property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<Color> metroAccentColors
        {
            get
            {
                return _metroColor;
            }

            set
            {
                if (_metroColor == value)
                {
                    return;
                }

                _metroColor = value;
                RaisePropertyChanged(metroAccentColorsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="wpAccentColors" /> property's name.
        /// </summary>
        public const string wpaccentColorsPropertyName = "wpAccentColors";

        // 20 accent colors for windows phone 8
        private ObservableCollection<Color> _myProperty = new ObservableCollection<Color> {
            Color.FromRgb(0xa4, 0xc4, 0x00),	// lime
			Color.FromRgb(0x60, 0xa9, 0x17),	// green
			Color.FromRgb(0x00, 0x8a, 0x00),	// emerald
			Color.FromRgb(0x00, 0xab, 0xa9),	// teal
			Color.FromRgb(0x1b, 0xa1, 0xe2),	// cyan
			Color.FromRgb(0x00, 0x50, 0xef),	// cobalt
			Color.FromRgb(0x6a, 0x00, 0xff),	// indigo
			Color.FromRgb(0xaa, 0x00, 0xff),	// violet
			Color.FromRgb(0xf4, 0x72, 0xd0),	// pink
			Color.FromRgb(0xd8, 0x00, 0x73),	// magenta
			Color.FromRgb(0xa2, 0x00, 0x25),	// crimson
			Color.FromRgb(0xe5, 0x14, 0x00),	// red
			Color.FromRgb(0xfa, 0x68, 0x00),	// orange
			Color.FromRgb(0xf0, 0xa3, 0x0a),	// amber
			Color.FromRgb(0xe3, 0xc8, 0x00),	// yellow
			Color.FromRgb(0x82, 0x5a, 0x2c),	// brown
			Color.FromRgb(0x6d, 0x87, 0x64),	// olive
			Color.FromRgb(0x64, 0x76, 0x87),	// steel
			Color.FromRgb(0x76, 0x60, 0x8a),	// mauve
			Color.FromRgb(0x87, 0x79, 0x4e),	// taupe
		};

        /// <summary>
        /// Sets and gets the accentColors property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<Color> wpAccentColors
        {
            get
            {
                return _myProperty;
            }

            set
            {
                if (_myProperty == value)
                {
                    return;
                }


                _myProperty = value;
                RaisePropertyChanged(wpaccentColorsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedAccentColor" /> property's name.
        /// </summary>
        public const string SelectedAccentColorPropertyName = "SelectedAccentColor";

        private Color _selectedAccentColor;

        /// <summary>
        /// Sets and gets the SelectedAccentColor property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Color SelectedAccentColor
        {
            get
            {
                return _selectedAccentColor;
            }

            set
            {
                if (_selectedAccentColor == value)
                {
                    return;
                }


                _selectedAccentColor = value;
                AppearanceManager.Current.AccentColor = value;
                RaisePropertyChanged(SelectedAccentColorPropertyName);

            }
        }
        /// <summary>
        /// The <see cref="Themes" /> property's name.
        /// </summary>
        public const string ThemesPropertyName = "Themes";

        private LinkCollection themes = new LinkCollection();

        /// <summary>
        /// Sets and gets the Themes property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public LinkCollection Themes
        {
            get
            {
                return themes;
            }

            set
            {
                if (themes == value)
                {
                    return;
                }


                themes = value;
                RaisePropertyChanged(ThemesPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="SelectedTheme" /> property's name.
        /// </summary>
        public const string SelectedThemePropertyName = "SelectedTheme";

        private Link _selectedTheme = null;

        /// <summary>
        /// Sets and gets the SelectedTheme property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Link SelectedTheme
        {
            get
            {
                return _selectedTheme;
            }

            set
            {
                if (_selectedTheme == value)
                {
                    return;
                }


                _selectedTheme = value;
                RaisePropertyChanged(SelectedThemePropertyName);
                // and update the actual theme
                AppearanceManager.Current.ThemeSource = value.Source;
            }
        }


        // private string selectedFontSize;
        /// <summary>
        /// The <see cref="SelectedFontSize" /> property's name.
        /// </summary>
        public const string SelectedFontSizePropertyName = "SelectedFontSize";

        private string selectedFontSize = string.Empty;

        /// <summary>
        /// Sets and gets the SelectedFontSize property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string SelectedFontSize
        {
            get
            {
                return selectedFontSize;
            }

            set
            {
                if (selectedFontSize == value)
                {
                    return;
                }


                selectedFontSize = value;
                //	RaisePropertyChanged(() => SelectedFontSize);
                RaisePropertyChanged(SelectedFontSizePropertyName);
                AppearanceManager.Current.FontSize = value == FontLarge ? FontSize.Large : FontSize.Small;
                if (_colorLoadedYet)
                {
                    ApplicationSettings.Default.SelectedFontSize = this.selectedFontSize;
                    ApplicationSettings.Default.Save();
                }
            }
        }

        private const string FontSmall = "small";
        private const string FontLarge = "large";

        /// <summary>
        /// Initializes a new instance of the SettingsAppearanceViewModel class.
        /// </summary>
        public SettingsAppearanceViewModel()
        {
            // add the default themes
            themes.Add(new Link { DisplayName = "dark", Source = AppearanceManager.DarkThemeSource });
            themes.Add(new Link { DisplayName = "light", Source = AppearanceManager.LightThemeSource });

            //additional themes
            //themes.Add(new Link { DisplayName = "snowflake", Source = new Uri("/CETAPUtilities;component/Assets/ModernUI.Snowflakes.xaml", UriKind.Relative) });
            //themes.Add(new Link { DisplayName = "bing image", Source = new Uri("/CETAPUtilities;component/Assets/ModernUI.BingImage.xaml", UriKind.Relative) });
            //themes.Add(new Link { DisplayName = "cetap", Source = new Uri("/CETAPUtilities;component/Assets/ModernUI.Cetap.xaml", UriKind.Relative) });
            //themes.Add(new Link { DisplayName = "rose", Source = new Uri("/CETAPUtilities;component/Assets/ModernUI.Rose.xaml", UriKind.Relative) });
            //themes.Add(new Link { DisplayName = "bubbles", Source = new Uri("/CETAPUtilities;component/Assets/ModernUI.Bubbles.xaml", UriKind.Relative) });
            //themes.Add(new Link { DisplayName = "water drop", Source = new Uri("/CETAPUtilities;component/Assets/ModernUI.WaterDrop.xaml", UriKind.Relative) });
            themes.Add(new Link { DisplayName = "Theme1", Source = new Uri("/CETAPUtilities;component/Assets/ModernUI.Theme1.xaml", UriKind.Relative) });
            // themes.Add(new Link { DisplayName = "snowflake", Source = AppearanceManager.LightThemeSource });




            SelectedFontSize = AppearanceManager.Current.FontSize == FontSize.Large ? FontLarge : FontSmall;
            SyncThemeAndColor();

            AppearanceManager.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;
        }
        private void OnAppearanceManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ThemeSource" || e.PropertyName == "AccentColor")
            {
                SyncThemeAndColor();
            }
        }
        private void SyncThemeAndColor()
        {
            // synchronizes the selected viewmodel theme with the actual theme used by the appearance manager.
            SelectedTheme = themes.FirstOrDefault(l => l.Source.Equals(AppearanceManager.Current.ThemeSource));

            // and make sure accent color is up-to-date
            SelectedAccentColor = AppearanceManager.Current.AccentColor;
            if (this._colorLoadedYet)
            {
                ApplicationSettings.Default.SelectedThemeDisplayName = this.SelectedTheme.DisplayName;
                ApplicationSettings.Default.SelectedThemeSource = this.SelectedTheme.Source;
                ApplicationSettings.Default.SelectedAccentColor = this.SelectedAccentColor;
                ApplicationSettings.Default.SelectedFontSize = this.SelectedFontSize;
                ApplicationSettings.Default.Save();
            }
        }
        public string[] FontSizes
        {
            get { return new string[] { FontSmall, FontLarge }; }
        }

        public void SetThemeAndColor(string themeSourceDisplayName, Uri themeSourceUri, Color accentColor, string fontSize)
        {
            this.SelectedTheme = new Link { DisplayName = themeSourceDisplayName, Source = themeSourceUri };
            this.SelectedAccentColor = accentColor;
            this.SelectedFontSize = fontSize;
            this._colorLoadedYet = true;
        }
    }
}