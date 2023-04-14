using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace UWPYourNoteLibrary.Util
{
    public class ChangeAccentColor
    {
        public enum ColorType
        {
            Default,
            Lavendar,
            Forest,
            Nighttime
        };

        public enum Themes
        {
            Light,
            Dark,
            System
        };
        static ResourceDictionary Light = null;
        static ResourceDictionary Dark = null;
        public static void AccentColorHelper2(string theme, ColorType accentColor)
        {
                ResourceDictionary Theme = null;
            if (theme == "L")
                Theme = Light;
            else
                Theme = Dark;
            Color primaryButtonBgColor = (Color)Theme["PrimaryButtonBgColor" + accentColor.ToString()];
            Color primaryButtonFgColor = (Color)Theme["PrimaryButtonFgColor" + accentColor.ToString()];
            Color secondaryButtonBgColor = (Color)Theme["SecondaryButtonBgColor" + accentColor.ToString()];
            Color secondaryButtonFgColor = (Color)Theme["SecondaryButtonFgColor" + accentColor.ToString()];
            Color primaryButtonBgColorOnHover = (Color)Theme["PrimaryButtonBgColorOnHover" + accentColor.ToString()];
            Color secondaryButtonBgColorOnHover = (Color)Theme["SecondaryButtonBgColorOnHover" + accentColor.ToString()];
            Color primaryButtonBgColorOnClick = (Color)Theme["PrimaryButtonBgColorOnClick" + accentColor.ToString()];
            Color secondaryButtonBgColorOnClick = (Color)Theme["SecondaryButtonBgColorOnClick" + accentColor.ToString()];

            Color textBoxBgColor = (Color)Theme["TextBoxBgColor" + accentColor.ToString()];
            Color textBoxFgColor = (Color)Theme["TextBoxFgColor" + accentColor.ToString()];
            Color textBoxBorderColor = (Color)Theme["TextBoxBorderColor" + accentColor.ToString()];
            Color textBoxBorderColorOnHover = (Color)Theme["TextBoxBorderColorOnHover" + accentColor.ToString()];
            Color textBoxBorderColorOnClick = (Color)Theme["TextBoxBorderColorOnClick" + accentColor.ToString()];
            Color textBoxClearButtonFgColorOnClick = (Color)Theme["TextBoxClearButtonFgColorOnClick" + accentColor.ToString()];
            Color textBoxClearButtonFgColor = (Color)Theme["TextBoxClearButtonFgColor" + accentColor.ToString()];
            Color textBoxPlaceHolderTextFgColor = (Color)Theme["TextBoxPlaceHolderTextFgColor" + accentColor.ToString()];
            Color textBoxTextFgColor = (Color)Theme["TextBoxTextFgColor" + accentColor.ToString()];


            (Application.Current.Resources["PrimaryButtonBgColorBrush" + theme] as SolidColorBrush).Color = primaryButtonBgColor;
            (Application.Current.Resources["PrimaryButtonFgColorBrush" + theme] as SolidColorBrush).Color = primaryButtonFgColor;
            (Application.Current.Resources["SecondaryButtonBgColorBrush" + theme] as SolidColorBrush).Color = secondaryButtonBgColor;
            (Application.Current.Resources["SecondaryButtonFgColorBrush" + theme] as SolidColorBrush).Color = secondaryButtonFgColor;
            (Application.Current.Resources["PrimaryButtonBgColorOnHoverBrush" + theme] as SolidColorBrush).Color = primaryButtonBgColorOnHover;
            (Application.Current.Resources["SecondaryButtonBgColorOnHoverBrush" + theme] as SolidColorBrush).Color = secondaryButtonBgColorOnHover;
            (Application.Current.Resources["PrimaryButtonBgColorOnClickBrush" + theme] as SolidColorBrush).Color = primaryButtonBgColorOnClick;
            (Application.Current.Resources["SecondaryButtonBgColorOnClickBrush" + theme] as SolidColorBrush).Color = secondaryButtonBgColorOnClick;


            (Application.Current.Resources["TextBoxBgColorBrush" + theme] as SolidColorBrush).Color = textBoxBgColor;
            (Application.Current.Resources["TextBoxFgColorBrush" + theme] as SolidColorBrush).Color = textBoxFgColor;
            (Application.Current.Resources["TextBoxBorderColorBrush" + theme] as SolidColorBrush).Color = textBoxBorderColor;
            (Application.Current.Resources["TextBoxBorderColorOnHoverBrush" + theme] as SolidColorBrush).Color = textBoxBorderColorOnHover;
            (Application.Current.Resources["TextBoxBorderColorOnClickBrush" + theme] as SolidColorBrush).Color = textBoxBorderColorOnClick;
            (Application.Current.Resources["TextBoxClearButtonFgColorOnClickBrush" + theme] as SolidColorBrush).Color = textBoxClearButtonFgColorOnClick;
            (Application.Current.Resources["TextBoxClearButtonFgColorBrush" + theme] as SolidColorBrush).Color = textBoxClearButtonFgColor;
            (Application.Current.Resources["TextBoxPlaceHolderTextFgColorBrush" + theme] as SolidColorBrush).Color = textBoxPlaceHolderTextFgColor;
            (Application.Current.Resources["TextBoxTextFgColorBrush" + theme] as SolidColorBrush).Color = textBoxTextFgColor;
        }

        public static void AccentColorHelper1(ColorType accent)
        {
            AccentColorHelper2("L", accent);
            AccentColorHelper2("D", accent);
            

        }
        public static void GetResourceNames()
        {
            var value = Application.Current.Resources.ThemeDictionaries.Values.FirstOrDefault();
            Console.WriteLine(value);

            foreach (var x in Application.Current.Resources.ThemeDictionaries)
            {
                if (x.Key.Equals("Dark"))
                {
                    Dark = x.Value as ResourceDictionary;
                }
                if (x.Key.Equals("Light"))
                {
                    Light = x.Value as ResourceDictionary;
                }

                if (Dark != null && Light != null)
                    break;
            }
        }

        public static void ChangeTheme(FrameworkElement currentTheme, Themes themeToChange)
        {
            if (Light == null && Dark == null)
            {
                GetResourceNames();
            }
            switch (themeToChange)
            {
                case Themes.System:
                    {
                        currentTheme.RequestedTheme = ElementTheme.Default;
                        break;
                    }
                case Themes.Dark:
                    {
                        currentTheme.RequestedTheme = ElementTheme.Dark; 
                        break;
          

                    }
                case Themes.Light:
                    {
                        currentTheme.RequestedTheme = ElementTheme.Light;
                        break;
                    }

            }
            SaveAppSettings.SaveThemePreferences(themeToChange);
         

        }
        
         public static void AccentDefault()
        {
            AccentColorHelper1(ColorType.Default);

        }
        public static void AccentLavendar()
        {

            AccentColorHelper1(ColorType.Lavendar);

        }
        public static void AccentNightTime()
        {

            AccentColorHelper1(ColorType.Nighttime);


        }
        public static void AccentForest()
        {

            AccentColorHelper1(ColorType.Forest);


        }
        public static void ChangeAccent(ColorType accentToChange)
        {
            if(Light == null && Dark == null) 
            {
                GetResourceNames();
            }
            switch(accentToChange)
            {
                case ColorType.Lavendar: AccentLavendar(); break;
                case ColorType.Forest: AccentForest(); break;
                case ColorType.Nighttime: AccentNightTime();break;
                default: AccentDefault();break;
            }

            SaveAppSettings.SaveAccentPreferences(accentToChange);
        }

        //public static void ChangeThemeNewView(ElementTheme currentTheme, Themes themeToChange)
        //{
        //}

    }
}
