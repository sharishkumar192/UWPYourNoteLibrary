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


        public CreateAccountDataManager DataManager;
        public CreateAccountUseCaseRequest Request { get; set; }

        public ICallback<CreateAccountUseCaseResponse> PresenterCallBack { get; set; }
        public CreateAccountUseCase(CreateAccountUseCaseRequest uCAccountCreationRequest, ICallback<CreateAccountUseCaseResponse> callback)
        {
            DataManager = CreateAccountDataManager.Singleton;
            Request = uCAccountCreationRequest;
            PresenterCallBack = callback;
        }



        public override void Action()
        {
            DataManager.AccountCreation(Request.Name, Request.Email, Request.Password, new UseCaseCallBack(this));
        }


        private class UseCaseCallBack : ICallback<CreateAccountUseCaseResponse>
        {

            private CreateAccountUseCase UseCase;
            public UseCaseCallBack(CreateAccountUseCase useCase)
            {
                UseCase = useCase;
            }


            public void onFailure(CreateAccountUseCaseResponse response)
            {
                UseCase?.PresenterCallBack?.onFailure(response);
            }

            public void onSuccess(CreateAccountUseCaseResponse response)
            {
                UseCase?.PresenterCallBack?.onSuccess(response);
            }
        }


    }


}
