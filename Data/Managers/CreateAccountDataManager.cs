using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPYourNoteLibrary.Data.Handler;
using UWPYourNoteLibrary.Domain;
using UWPYourNoteLibrary.Models;
using UWPYourNoteLibrary.Domain.Contract;
namespace UWPYourNoteLibrary.Data.Managers
{
    public class CreateAccountDataManager : ICreateAccountDataManager
    {
        private UserDBHandler noteDBHandler;
        
        private static CreateAccountDataManager dataManager = null;
        public static CreateAccountDataManager Singleton
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
            noteDBHandler = UserDBHandler.Singleton;
            bool result = noteDBHandler.InsertNewUser(DBCreation.userTableName, name, email, password);
            CreateAccountUseCaseResponse response = new CreateAccountUseCaseResponse(result);
            if(result)
            callback?.onSuccess(response);
            else
            callback.onFailure(response);


        }

   
    }
}
