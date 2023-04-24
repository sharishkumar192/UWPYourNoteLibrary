using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPYourNoteLibrary.Data.Managers;
using UWPYourNoteLibrary.Domain.Contract;

namespace UWPYourNoteLibrary.Domain.UseCase
{
    public class ValidUsersToShareUseCaseResponse
    {
        public ObservableCollection<UWPYourNoteLibrary.Models.User> Result { get; set; }
    }

    public class ValidUsersToShareUseCaseRequest
    {
        public string UserId { get; set; }  
        public long NoteId { get; set; }

    }
    public class ValidUsersToShareUseCase : UseCaseBase<ValidUsersToShareUseCaseResponse>
    {
        public ValidUsersToShareUseCaseRequest Request { get; set; }
        public ICallback<ValidUsersToShareUseCaseResponse> PresenterCallBack { get; set; }

        public interface IValidUsersToShareDataManager<ValidUsersToShareUseCaseResponse>
        {
            void ValidUsersToShare(string userId, long displayNoteId, ICallback<ValidUsersToShareUseCaseResponse> useCaseCallBack);
        }
        public override void Action()
        {
            DataManager.ValidUsersToShare(Request.UserId, Request.NoteId, new ValidUsersToShareUseCaseCallBack(this));
        }

        public IValidUsersToShareDataManager<ValidUsersToShareUseCaseResponse> DataManager;
        public ValidUsersToShareUseCase(ValidUsersToShareUseCaseRequest request, ICallback<ValidUsersToShareUseCaseResponse> callback)
        {
            DataManager = ValidUsersToShareDataManager.Singleton;
            PresenterCallBack = callback;
            Request = request;

        }

        private class ValidUsersToShareUseCaseCallBack : ICallback<ValidUsersToShareUseCaseResponse>
        {
            ValidUsersToShareUseCase UseCase;
            public ValidUsersToShareUseCaseCallBack(ValidUsersToShareUseCase useCase)
            {
                UseCase = useCase;
            }

            public void onFailure(ValidUsersToShareUseCaseResponse response)
            {
                UseCase?.PresenterCallBack?.onFailure(response);
            }

            public void onSuccess(ValidUsersToShareUseCaseResponse response)
            {
                UseCase?.PresenterCallBack?.onSuccess(response);
            }
        }
    }
}
