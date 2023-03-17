using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPYourNoteLibrary.Data.Handler;
using UWPYourNoteLibrary.Domain;
using UWPYourNoteLibrary.Models;

namespace UWPYourNoteLibrary.Data
{
    public class AccountCreationDataManager : IAccountCreationDataManager
    {
        private NoteDBHandler noteDBHandler;
        
        private static AccountCreationDataManager dataManager = null;
        public static AccountCreationDataManager DataManager
        {

            get
            {
                if (dataManager == null)
                {
                    dataManager = new AccountCreationDataManager();

                }
                return dataManager;

            }

        }

        public void AccountCreation(string name, string email, string password, ICallback<UCAccountCreationResponse> callback) 
        {
            noteDBHandler = NoteDBHandler.NDBHandler;
            bool result = noteDBHandler.InsertNewUser(DBCreation.userTableName, name, email, password);

            if(result)
            callback?.onSuccess();
            else
            callback.onFailure();


        }

   
    }
}
