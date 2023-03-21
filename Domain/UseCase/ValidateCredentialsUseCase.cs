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
        public ICallback<ValidateCredentialsUseCaseResponse> ValidateCredentialsCallBack;
        public ValidateCredentialsUseCaseRequest Request { get; set; }
        public ValidateCredentialsDataManager dataManager { get; set; }
        public ValidateCredentialsUseCase(ValidateCredentialsUseCaseRequest request, ICallback<ValidateCredentialsUseCaseResponse> callback)
        {
            dataManager = ValidateCredentialsDataManager.DataManager;
            Request = request;
            ValidateCredentialsCallBack = callback;
        }

        public interface IValidateCredentialsDataManager<ValidateCredentialsUseCaseResponse>
        {
            void ValidateCredentials(string userId, string password, ICallback<ValidateCredentialsUseCaseResponse> useCaseCallBack);
        }


        public override void Action()
        {
            dataManager.ValidateCredentials(Request.UserId, Request.Password, new ValidateCredentialsUseCaseCallBack(this));
        }

        private class ValidateCredentialsUseCaseCallBack : ICallback<ValidateCredentialsUseCaseResponse>
        {
            private ValidateCredentialsUseCase _useCase;
            public ValidateCredentialsUseCaseCallBack(ValidateCredentialsUseCase useCase)
            {
                _useCase = useCase;
            }

            public void onFailure(ValidateCredentialsUseCaseResponse result)
            {
              _useCase?.ValidateCredentialsCallBack?.onFailure(result);

            }

            public void onSuccess(ValidateCredentialsUseCaseResponse result)
            {
               _useCase?.ValidateCredentialsCallBack?.onSuccess(result);
            }
        }
    }
}
