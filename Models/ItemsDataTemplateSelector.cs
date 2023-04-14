using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using UWPYourNoteLibrary.Models;
using System.Collections.ObjectModel;
using System.Reflection;

namespace UWPYourNoteLibrary.Models {

    public class ItemsDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate AllItems { get; set; }
        public DataTemplate LastItems { get; set; }

        public DataTemplate NoItems { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            DataTemplate _returnTemplate = new DataTemplate();
            var itemsControl = ItemsControl.ItemsControlFromItemContainer(container);


            Type tp = item.GetType();

            if (tp.Equals(typeof(User)))
            {
                var contents = itemsControl.ItemsSource as ObservableCollection<User>;
                int count = contents.Count;
                var i = contents.IndexOf((User)item);

                if (contents.Count == 1)
                    _returnTemplate = LastItems;
                else
                {
                    if (i == count - 1)
                        _returnTemplate = LastItems;
                    else
                        _returnTemplate = AllItems;
                }

            }
            else
            {
                var contents = itemsControl.ItemsSource as ObservableCollection<Models.Note>;
                int count = contents.Count;
                var i = contents.IndexOf((Note)item);
                if (contents.Count == 0 || contents == null)
                    _returnTemplate = NoItems;
                if (contents.Count == 1)
                    _returnTemplate = LastItems;
                else
                {
                    if (i == count - 1)
                        _returnTemplate = LastItems;
                    else
                        _returnTemplate = AllItems;
                }
            }





            return _returnTemplate;
        }
    }
}
