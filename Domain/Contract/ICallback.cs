using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPYourNoteLibrary.Domain.Contract
{
    public interface ICallback<T>
    { 
        void onSuccess(T result);
        void onFailure(T result);
    }
}
