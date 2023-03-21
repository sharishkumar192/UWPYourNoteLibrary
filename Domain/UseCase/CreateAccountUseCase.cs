using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UWPYourNoteLibrary.Domain.Contract;
using UWPYourNoteLibrary.Data;
using UWPYourNoteLibrary.Data.Managers;

namespace UWPYourNoteLibrary.Domain
{
    public interface ICreateAccountDataManager
    {
        void AccountCreation(string name, string email, string password, ICallback<CreateAccountUseCaseResponse> presenterCallback);
    }

    public sealed class CreateAccountUseCaseRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public CreateAccountUseCaseRequest(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        //public CreateAccountUseCaseRequest(CreateAccountUseCaseRequest old)
        //{
        //    Name = old.Name;
        //    Email = old.Email;
        //    Password = old.Password;
        //}
    }

   
    public sealed class CreateAccountUseCaseResponse
    {
        bool result;
        public CreateAccountUseCaseResponse(bool result)
        {
            this.result = result;
        }
    }

    public class CreateAccountUseCase : UseCaseBase<CreateAccountUseCaseResponse>
    {


        public CreateAccountDataManager AccountCreationDataManager;
        public CreateAccountUseCaseRequest Request { get; set; }

        public ICallback<CreateAccountUseCaseResponse> SignUpPresenterCallback { get; private set; }
        public CreateAccountUseCase(CreateAccountUseCaseRequest uCAccountCreationRequest, ICallback<CreateAccountUseCaseResponse> callback)
        {
            AccountCreationDataManager = CreateAccountDataManager.DataManager;
            Request = uCAccountCreationRequest; 
            SignUpPresenterCallback = callback; 
        }

 

        public override void Action()
        {
            AccountCreationDataManager.AccountCreation(Request.Name, Request.Email, Request.Password, new UseCaseCallBack(this));
        }


        private class UseCaseCallBack : ICallback<CreateAccountUseCaseResponse>
        {
            
            private CreateAccountUseCase accountCreation;
            public UseCaseCallBack(CreateAccountUseCase uCAccountCreation)
            {
                accountCreation = uCAccountCreation;
            }

          
            public void onFailure(CreateAccountUseCaseResponse result)
            {
                accountCreation?.SignUpPresenterCallback?.onFailure(result);
            }

            public void onSuccess(CreateAccountUseCaseResponse result)
            {
                accountCreation?.SignUpPresenterCallback?.onSuccess(result);
            }
        }


    }


}
