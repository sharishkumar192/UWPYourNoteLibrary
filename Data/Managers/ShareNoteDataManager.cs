using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPYourNoteLibrary.Data.Handler;
using UWPYourNoteLibrary.Data.Handler.Contract;
using UWPYourNoteLibrary.Domain.Contract;
using UWPYourNoteLibrary.Domain;
using UWPYourNoteLibrary.Domain.UseCase;
using UWPYourNoteLibrary.Models;
using System.Collections.ObjectModel;
using UWPYourNoteLibrary.Util;
using static UWPYourNoteLibrary.Util.NotesUtilities;

namespace UWPYourNoteLibrary.Data.Managers
{
    public class ShareNoteDataManager : IShareNoteUseCaseDataManager
    {
        public INoteDBHandler DBHandler { get; set; }
        private static ShareNoteDataManager dataManager;
        public static ShareNoteDataManager Singleton
        {
            get
            {
                if (dataManager == null)
                {
                    dataManager = new ShareNoteDataManager();
                }
                return dataManager;
            }
        }
        public void ShareNote(long noteId, string sharedUserId, ICallback<ShareNoteUseCaseResponse> callback)
        {
            DBHandler = NoteDBHandler.Singleton;

            bool result = DBHandler.InsertSharedNote(NotesUtilities.sharedTableName, sharedUserId, noteId);
            ShareNoteUseCaseResponse response = new ShareNoteUseCaseResponse();
            response.Result = result;
            if (result)
                callback?.onSuccess(response);
            else
                callback?.onFailure(response);


      
        }

    }




}
