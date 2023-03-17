using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPYourNoteLibrary.Data.Handler
{
    internal interface INoteDBHandler
    {
        bool InsertNewUser(string tableName, string username, string email, string password);
    }
}
