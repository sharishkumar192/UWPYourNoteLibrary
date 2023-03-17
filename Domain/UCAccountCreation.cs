using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UWPYourNoteLibrary.Data;
using static UWPYourNoteLibrary.Domain.UCAccountCreation;

namespace UWPYourNoteLibrary.Domain
{
    public interface IAccountCreationDataManager
    {
        void AccountCreation(string name, string email, string password, ICallback<UCAccountCreationResponse> presenterCallback);
    }
    public interface IUseCaseCallBack<T> : ICallback<T>
    {
        void OnSuccess(bool isCreated);
        void OnFailure(bool isCreated);
    }



    public sealed class UCAccountCreationRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UCAccountCreationRequest(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public UCAccountCreationRequest(UCAccountCreationRequest old)
        {
            Name = old.Name;
            Email = old.Email;
            Password = old.Password;
        }
    }

   
    public sealed class UCAccountCreationResponse
    {

    }

    public class UCAccountCreation : UCBase<UCAccountCreationResponse>
    {


        public AccountCreationDataManager AccountCreationDataManager;
        public UCAccountCreationRequest Request { get; set; }

        public IPresenterCallback VMPresenterCallback { get; private set; }
        public UCAccountCreation(UCAccountCreationRequest uCAccountCreationRequest, IPresenterCallback callback)
        {
            AccountCreationDataManager = AccountCreationDataManager.DataManager;
            Request = new UCAccountCreationRequest(uCAccountCreationRequest); 
            VMPresenterCallback = callback; 
        }

 

        public override void Action()
        {
            AccountCreationDataManager.AccountCreation(Request.Name, Request.Email, Request.Password, new UseCaseCallBack(this));
        }


        private class UseCaseCallBack : ICallback<UCAccountCreationResponse>
        {
            
            private UCAccountCreation accountCreation;
            public UseCaseCallBack(UCAccountCreation uCAccountCreation)
            {
                accountCreation = uCAccountCreation;
            }

            public void onFailure()
            {
                accountCreation.VMPresenterCallback.onFailure();
            }

          

            public void onSuccess()
            {
                accountCreation.VMPresenterCallback.onSuccess();
            }
        }


    }


}
