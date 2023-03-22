using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UWPYourNoteLibrary.Domain.UseCase.RecentLogInUseCase;
using UWPYourNoteLibrary.Domain.UseCase;
using UWPYourNoteLibrary.Domain.Contract;
using System.ComponentModel.DataAnnotations;
using UWPYourNoteLibrary.Data.Handler.Contract;
using UWPYourNoteLibrary.Data.Handler;
using System.Collections.ObjectModel;
using UWPYourNoteLibrary.Models;
using UWPYourNoteLibrary.Util;

namespace UWPYourNoteLibrary.Data.Managers
{
    public class RecentLogInUseCaseDataManager : IRecentLogInUseCaseDataManager<RecentLogInUseCaseResponse>
    {
        public IUserDBHandler userDBHandler { get; set; }
        private static RecentLogInUseCaseDataManager login;

        public static RecentLogInUseCaseDataManager Singleton
        {
            get
            {
                if (login == null)
                {
                    login = new RecentLogInUseCaseDataManager();
                }
                return login;
            }
        }
        public void RecentLogInUsers(string userId, string password, ICallback<RecentLogInUseCaseResponse> useCaseCallBack)
        {
            userDBHandler = UserDBHandler.Singleton;
            ObservableCollection<Models.User> recentList = userDBHandler.RecentLoggedInUsers(UserUtilities.userTableName);
            RecentLogInUseCaseResponse response = new RecentLogInUseCaseResponse();
            response.List = recentList;
            if(recentList!=null)
                useCaseCallBack?.onSuccess(response);
            else
                useCaseCallBack?.onFailure(response);


        }
    }
}
