using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPYourNoteLibrary.Data;
using UWPYourNoteLibrary.Data.Managers;
using UWPYourNoteLibrary.Domain.Contract;
using static UWPYourNoteLibrary.Domain.CheckIfUserExistsUseCase;

namespace UWPYourNoteLibrary.Domain
{
    public class CheckIfUserExistsUseCaseResponse
    {
        public bool IsExists {get; set; }
    }

    public class CheckIfUserExistsUseCaseRequest
    {
        public string Email { get; set; }

    }
    public class CheckIfUserExistsUseCase : UseCaseBase<CheckIfUserExistsUseCaseResponse>
    {
        public CheckIfUserExistsUseCaseRequest Request { get; set; }
        public ICallback<CheckIfUserExistsUseCaseResponse> PresenterCallBack { get; set; }

        public interface ICheckIfUserExistsEmailDataManager<CheckIfUserExistsUseCaseResponse>
        {
            void CheckIfExistingEmail(string email, ICallback<CheckIfUserExistsUseCaseResponse> useCaseCallBack);
        }
        public override void Action()
        {
            DataManager.CheckIfExistingEmail(Request.Email , new CheckIfExistingEmailUseCaseCallBack(this));
        }

        public ICheckIfUserExistsEmailDataManager<CheckIfUserExistsUseCaseResponse> DataManager;
        public CheckIfUserExistsUseCase(CheckIfUserExistsUseCaseRequest request, ICallback<CheckIfUserExistsUseCaseResponse> callback)
        {
            DataManager = CheckIfUserExistsEmailDataManager.Singleton;
           PresenterCallBack = callback;
           Request = request;

        }

        private class CheckIfExistingEmailUseCaseCallBack : ICallback<CheckIfUserExistsUseCaseResponse>
        {
            CheckIfUserExistsUseCase UseCase;
            public CheckIfExistingEmailUseCaseCallBack(CheckIfUserExistsUseCase useCase)
            {
               UseCase = useCase;
            }   

            public void onFailure(CheckIfUserExistsUseCaseResponse result)
            {
                UseCase?.PresenterCallBack?.onFailure(result);
            }

            public void onSuccess(CheckIfUserExistsUseCaseResponse result)
            {
                UseCase?.PresenterCallBack?.onSuccess(result);   
            }
        }
    }
}
