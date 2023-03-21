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
        public ICallback<RecentLogInUseCaseResponse> RecentLogInCallBack;
        public RecentLogInUseCaseRequest Request { get; set; }
        public RecentLogInUseCaseDataManager dataManager { get; set; }
        public RecentLogInUseCase(RecentLogInUseCaseRequest request, ICallback<RecentLogInUseCaseResponse> callback)
        {
            dataManager = RecentLogInUseCaseDataManager.DataManager;
            Request = request;
            RecentLogInCallBack = callback;
        }

        public interface IRecentLogInUseCaseDataManager<RecentLogInUseCaseResponse>
        {
            void RecentLogInUsers(string userId, string password, ICallback<RecentLogInUseCaseResponse> useCaseCallBack);
        }


        public override void Action()
        {
            dataManager.RecentLogInUsers(Request.UserId, Request.Password, new RecentLogInUseCaseCallBack(this));
        }

        private class RecentLogInUseCaseCallBack : ICallback<RecentLogInUseCaseResponse>
        {
            private RecentLogInUseCase _useCase;
            public RecentLogInUseCaseCallBack(RecentLogInUseCase useCase)
            {
                _useCase = useCase;
            }

            public void onFailure(RecentLogInUseCaseResponse result)
            {
                _useCase?.RecentLogInCallBack?.onFailure(result);

            }

            public void onSuccess(RecentLogInUseCaseResponse result)
            {
                _useCase?.RecentLogInCallBack?.onSuccess(result);
            }
        }
    }
}
