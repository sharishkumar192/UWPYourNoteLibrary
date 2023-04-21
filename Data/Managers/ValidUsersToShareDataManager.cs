using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UWPYourNoteLibrary.Domain.UseCase.ValidUsersToShareUseCase;
using UWPYourNoteLibrary.Data.Handler.Contract;
using UWPYourNoteLibrary.Data.Handler;
using UWPYourNoteLibrary.Domain.Contract;
using UWPYourNoteLibrary.Domain.UseCase;
using UWPYourNoteLibrary.Util;
using System.Collections.ObjectModel;

namespace UWPYourNoteLibrary.Data.Managers
{
    internal class ValidUsersToShareDataManager : IValidUsersToShareDataManager<ValidUsersToShareUseCaseResponse>
    {
        public INoteDBHandler noteDBHandler { get; set; }
        private static ValidUsersToShareDataManager checkIf;
        public static ValidUsersToShareDataManager Singleton
        {
            get
            {
                if (checkIf == null)
                {
                    checkIf = new ValidUsersToShareDataManager();
                }
                return checkIf;
            }
        }
        public void ValidUsersToShare(string userId, long displayNoteId, ICallback<ValidUsersToShareUseCaseResponse> useCaseCallBack)
        {
            noteDBHandler = NoteDBHandler.Singleton;
            ObservableCollection<UWPYourNoteLibrary.Models.User> result = noteDBHandler.ValidUsersToShare(UserUtilities.userTableName, NotesUtilities.sharedTableName, NotesUtilities.notesTableName, userId, displayNoteId);

            ValidUsersToShareUseCaseResponse ValidUsersToShareResponse = new ValidUsersToShareUseCaseResponse();
            ValidUsersToShareResponse.Result = result;
            if (result != null && result.Count!=0)
                useCaseCallBack?.onSuccess(ValidUsersToShareResponse);
            else
                useCaseCallBack?.onFailure(ValidUsersToShareResponse);

        }

    }
}
