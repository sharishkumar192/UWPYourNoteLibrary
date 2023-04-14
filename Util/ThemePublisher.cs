using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPYourNoteLibrary.Util
{
    public class ThemePublisher
    {
        public delegate void ThemeChangedEventHandler(ChangeAccentColor.Themes newData);
        private event ThemeChangedEventHandler themeUpdated;

        private static ThemePublisher _tp;
        public static ThemePublisher Tp
        {
            get 
            {
                if( _tp == null )   
                    _tp = new ThemePublisher();
                return _tp;
            }
        }

        public void UpdateTheme(ChangeAccentColor.Themes themeChanged)
        {
            themeUpdated?.Invoke(themeChanged);
        }

        public event ThemeChangedEventHandler ThemeUpdated
        {
            add { themeUpdated += value; }
            remove { themeUpdated -= value; }
        }


        //public event EventHandler<ThemePublisher> ThemeChanged;

        //public void ThemeUpdate(ChangeAccentColor.Themes themeToUpdate)
        //{
        //    OnThemeUpdate(themeToUpdate);
        //}

        //protected virtual void OnThemeUpdate(ChangeAccentColor.Themes themeToUpdate)
        //{
        //    if (ThemeChanged != null) 
        //    {
        //        ThemeChanged(this, themeToUpdate);
        //    }
        //}

    }
}
