using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Composition;

namespace UWPYourNoteLibrary.Util
{
    public class SaveAppSettings
    {
        public static void SavePreferences(string theme)
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            // Save a setting locally on the device
            localSettings.Values["ApplicationTheme"] = theme;

        }
        public static string LoadPreferences()
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            // load a setting that is local to the device
            String localValue = localSettings.Values["ApplicationTheme"] as string;
            return localValue;

        }
    }
}
