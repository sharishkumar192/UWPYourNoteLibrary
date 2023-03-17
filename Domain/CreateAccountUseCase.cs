using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UWPYourNoteLibrary.Data;
using static UWPYourNoteLibrary.Domain.CreateAccountUseCase;

namespace UWPYourNoteLibrary.Domain
{
    public interface ICreateAccountDataManager
    {
        void AccountCreation(string name, string email, string password, ICallback<CreateAccountUseCaseResponse> presenterCallback);
    }
    public interface IUseCaseCallBack<T> : ICallback<T>
    {
        void OnSuccess(bool isCreated);
        void OnFailure(bool isCreated);
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

        public CreateAccountUseCaseRequest(CreateAccountUseCaseRequest old)
        {
            Name = old.Name;
            Email = old.Email;
            Password = old.Password;
        }
    }

   
    public sealed class CreateAccountUseCaseResponse
    {

    }

    public class CreateAccountUseCase : UseCaseBase<CreateAccountUseCaseResponse>
    {


        public CreateAccountDataManager AccountCreationDataManager;
        public CreateAccountUseCaseRequest Request { get; set; }

        public IPresenterCallback SignUpPresenterCallback { get; private set; }
        public CreateAccountUseCase(CreateAccountUseCaseRequest uCAccountCreationRequest, IPresenterCallback callback)
        {
            AccountCreationDataManager = CreateAccountDataManager.DataManager;
            Request = new CreateAccountUseCaseRequest(uCAccountCreationRequest); 
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

            public void onFailure()
            {
                accountCreation.SignUpPresenterCallback.onFailure();
            }

            public void onSuccess()
            {
                accountCreation.SignUpPresenterCallback.onSuccess();
            }
        }


    }


}
