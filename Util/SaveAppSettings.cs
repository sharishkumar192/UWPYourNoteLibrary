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
       
        public static void SaveThemePreferences(ChangeAccentColor.Themes theme)
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["ApplicationTheme"] = theme.ToString();

        }
        public static void SaveAccentPreferences(ChangeAccentColor.ColorType accentColor)
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["ApplicationAccentColor"] = accentColor.ToString();
        }
        public static ChangeAccentColor.Themes LoadThemePreferences()
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            var value = localSettings.Values["ApplicationTheme"];
           if(value==null)
                    return ChangeAccentColor.Themes.System;
            ChangeAccentColor.Themes theme = ChangeAccentColor.Themes.Light;
            if (value.Equals("Dark"))
            {
                theme = ChangeAccentColor.Themes.Dark;
            }
            else if (value.Equals("System"))
            {
                theme = ChangeAccentColor.Themes.System;

            }
            return theme;

        }

        public static ChangeAccentColor.ColorType LoadAccentColorPreferences()
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            var value = localSettings.Values["ApplicationAccentColor"];
            ChangeAccentColor.ColorType color;
            Console.WriteLine(value);   
            if (value == null || value.Equals("Default")== true)
                color = ChangeAccentColor.ColorType.Default;
            else if (value.Equals("Lavendar")==true)
                color = ChangeAccentColor.ColorType.Lavendar;
            else if (value.Equals("Forest")== true)
                color = ChangeAccentColor.ColorType.Forest;
            else
                color = ChangeAccentColor.ColorType.Nighttime;
            
               
            return color;

        }


        public static bool CheckDBIntialization()
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            var value = localSettings.Values["DBTableCreation"];
            if (value != null)
                return true;
            localSettings.Values["DBTableCreation"] = true;
            return false;
        }
    }
}
