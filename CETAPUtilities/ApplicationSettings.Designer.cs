﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CETAPUtilities
{


    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class ApplicationSettings : global::System.Configuration.ApplicationSettingsBase
    {

        private static ApplicationSettings defaultInstance = ((ApplicationSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new ApplicationSettings())));

        public static ApplicationSettings Default
        {
            get
            {
                return defaultInstance;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("#FF0000FF")]
        public global::System.Windows.Media.Color SelectedAccentColor
        {
            get
            {
                return ((global::System.Windows.Media.Color)(this["SelectedAccentColor"]));
            }
            set
            {
                this["SelectedAccentColor"] = value;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("/FirstFloor.ModernUI;component/Assets/ModernUI.Dark.xaml")]
        public global::System.Uri SelectedThemeSource
        {
            get
            {
                return ((global::System.Uri)(this["SelectedThemeSource"]));
            }
            set
            {
                this["SelectedThemeSource"] = value;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("dark")]
        public string SelectedThemeDisplayName
        {
            get
            {
                return ((string)(this["SelectedThemeDisplayName"]));
            }
            set
            {
                this["SelectedThemeDisplayName"] = value;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Large")]
        public string SelectedFontSize
        {
            get
            {
                return ((string)(this["SelectedFontSize"]));
            }
            set
            {
                this["SelectedFontSize"] = value;
            }
        }
    }
}
