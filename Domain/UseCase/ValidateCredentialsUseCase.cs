using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPYourNoteLibrary.Domain.Contract;
using UWPYourNoteLibrary.Models;
using UWPYourNoteLibrary.Data.Managers;
using UWPYourNoteLibrary.Data.Handler;

namespace UWPYourNoteLibrary.Domain.UseCase
{
    public class ValidateCredentialsUseCaseResponse
    {
        public User user { get; set; }
    }
    public class ValidateCredentialsUseCaseRequest
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public ValidateCredentialsUseCaseRequest(string userId, string password)
        {
            UserId = userId;
            Password = password;
        }

    }
    public class ValidateCredentialsUseCase : UseCaseBase<ValidateCredentialsUseCaseResponse>
    {
        public ICallback<ValidateCredentialsUseCaseResponse> PresenterCallBack;
        public ValidateCredentialsUseCaseRequest Request { get; set; }
        public ValidateCredentialsDataManager DataManager { get; set; }
        public ValidateCredentialsUseCase(ValidateCredentialsUseCaseRequest request, ICallback<ValidateCredentialsUseCaseResponse> callback)
        {
            DataManager = ValidateCredentialsDataManager.Singleton;
            Request = request;
            PresenterCallBack = callback;
        }

        public interface IValidateCredentialsDataManager<ValidateCredentialsUseCaseResponse>
        {
            void ValidateCredentials(string userId, string password, ICallback<ValidateCredentialsUseCaseResponse> useCaseCallBack);
        }


        public override void Action()
        {
            DataManager.ValidateCredentials(Request.UserId, Request.Password, new ValidateCredentialsUseCaseCallBack(this));
        }

        private class ValidateCredentialsUseCaseCallBack : ICallback<ValidateCredentialsUseCaseResponse>
        {
            private ValidateCredentialsUseCase UseCase;
            public ValidateCredentialsUseCaseCallBack(ValidateCredentialsUseCase useCase)
            {
                UseCase = useCase;
            }

            public void onFailure(ValidateCredentialsUseCaseResponse result)
            {
              UseCase?.PresenterCallBack?.onFailure(result);

            }

            public void onSuccess(ValidateCredentialsUseCaseResponse result)
            {
               UseCase?.PresenterCallBack?.onSuccess(result);
            }
        }
    }
}
