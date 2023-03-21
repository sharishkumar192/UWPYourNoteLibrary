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
        public ICallback<CheckIfUserExistsUseCaseResponse> IsExistingUserCallback { get; set; }

        public interface ICheckIfUserExistsEmailDataManager<CheckIfUserExistsUseCaseResponse>
        {
            void CheckIfExistingEmail(string email, ICallback<CheckIfUserExistsUseCaseResponse> useCaseCallBack);
        }
        public override void Action()
        {
            checkIfExistingEmailDataManager.CheckIfExistingEmail(Request.Email , new CheckIfExistingEmailUseCaseCallBack(this));
        }

        public ICheckIfUserExistsEmailDataManager<CheckIfUserExistsUseCaseResponse> checkIfExistingEmailDataManager;
        public CheckIfUserExistsUseCase(CheckIfUserExistsUseCaseRequest request, ICallback<CheckIfUserExistsUseCaseResponse> callback)
        {
            checkIfExistingEmailDataManager = CheckIfUserExistsEmailDataManager.DataManager;
           IsExistingUserCallback = callback;
           Request = request;

        }

        private class CheckIfExistingEmailUseCaseCallBack : ICallback<CheckIfUserExistsUseCaseResponse>
        {
            CheckIfUserExistsUseCase checkIfExistingEmail;
            public CheckIfExistingEmailUseCaseCallBack(CheckIfUserExistsUseCase checkIfExistingEmail)
            {
                this.checkIfExistingEmail = checkIfExistingEmail;
            }   

            public void onFailure(CheckIfUserExistsUseCaseResponse result)
            {
                checkIfExistingEmail?.IsExistingUserCallback?.onFailure(result);
            }

            public void onSuccess(CheckIfUserExistsUseCaseResponse result)
            {
                checkIfExistingEmail?.IsExistingUserCallback?.onSuccess(result);   
            }
        }
    }
}
