using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPYourNoteLibrary.Domain
{
    public interface ICallback<T>
    { 
        void onSuccess();
        void onFailure();
    }
}
