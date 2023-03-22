using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPYourNoteLibrary.Models;
namespace UWPYourNoteLibrary.Data.Handler.Contract
{
    public interface IUserDBHandler
    {
        bool InsertNewUser(string tableName, string username, string email, string password);
        bool CheckIfExistingEmail(string tableName, string userId);
        Models.User ValidateUser(string tableName, string userId, string password);
         ObservableCollection<Models.User> RecentLoggedInUsers(string tableName);

    }
}
