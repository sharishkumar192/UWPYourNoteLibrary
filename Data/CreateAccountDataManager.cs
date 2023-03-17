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
    public class CreateAccountDataManager : ICreateAccountDataManager
    {
        private NoteDBHandler noteDBHandler;
        
        private static CreateAccountDataManager dataManager = null;
        public static CreateAccountDataManager DataManager
        {

            get
            {
                if (dataManager == null)
                {
                    dataManager = new CreateAccountDataManager();

                }
                return dataManager;

            }

        }

        public void AccountCreation(string name, string email, string password, ICallback<CreateAccountUseCaseResponse> callback) 
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
