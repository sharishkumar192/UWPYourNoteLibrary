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
    public class RecentLogInUseCaseResponse
    {
       public ObservableCollection<Models.User> List { get; set; }  

    }
    public class RecentLogInUseCaseRequest
    {
        public string UserId { get; set; }
        public string Password { get; set; }
     
    }
    public class RecentLogInUseCase : UseCaseBase<RecentLogInUseCaseResponse>
    {
        public ICallback<RecentLogInUseCaseResponse> PresenterCallBack;
        public RecentLogInUseCaseRequest Request { get; set; }
        public RecentLogInUseCaseDataManager DataManager { get; set; }
        public RecentLogInUseCase(RecentLogInUseCaseRequest request, ICallback<RecentLogInUseCaseResponse> callback)
        {
            DataManager = RecentLogInUseCaseDataManager.Singleton;
            Request = request;
            PresenterCallBack = callback;
        }

        public interface IRecentLogInUseCaseDataManager<RecentLogInUseCaseResponse>
        {
            void RecentLogInUsers(string userId, string password, ICallback<RecentLogInUseCaseResponse> useCaseCallBack);
        }


        public override void Action()
        {
            DataManager.RecentLogInUsers(Request.UserId, Request.Password, new RecentLogInUseCaseCallBack(this));
        }

        private class RecentLogInUseCaseCallBack : ICallback<RecentLogInUseCaseResponse>
        {
            private RecentLogInUseCase UseCase;
            public RecentLogInUseCaseCallBack(RecentLogInUseCase useCase)
            {
                UseCase = useCase;
            }

            public void onFailure(RecentLogInUseCaseResponse response)
            {
                UseCase?.PresenterCallBack?.onFailure(response);

            }

            public void onSuccess(RecentLogInUseCaseResponse response)
            {
                UseCase?.PresenterCallBack?.onSuccess(response);
            }
        }
    }
}
