using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPYourNoteLibrary.DI
{
    internal class NotesDIServiceProvider : IServiceProvider
    {
        private class NotesDIServiceProviderSingleton
        {
            // Explicit static constructor
            static NotesDIServiceProviderSingleton() { }

            //Marked as internal as it will be accessed from the enclosing class. It doesn't raise any problem, as the class itself is private.
            internal static readonly NotesDIServiceProvider Instance = new NotesDIServiceProvider();
        }
        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        private NotesDIServiceProvider()
        {
            IServiceCollection servicesCollection = new ServiceCollection();

        }
        public static NotesDIServiceProvider Instance { get { return NotesDIServiceProviderSingleton.Instance; } }
    }
}
