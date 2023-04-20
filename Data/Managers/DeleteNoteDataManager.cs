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
using UWPYourNoteLibrary.Notification;

namespace UWPYourNoteLibrary.Data.Managers
{
    public class DeleteNoteDataManager : IDeleteNoteUseCaseDataManager
    {
        public INoteDBHandler DBHandler { get; set; }
        private static DeleteNoteDataManager dataManager;
        public static DeleteNoteDataManager Singleton
        {
            get
            {
                if (dataManager == null)
                {
                    dataManager = new DeleteNoteDataManager();
                }
                return dataManager;
            }
        }
        public void DeleteNote(long noteId, ICallback<DeleteNoteUseCaseResponse> callback)
        {
            DBHandler = NoteDBHandler.Singleton;

            bool result = DBHandler.DeleteNote(NotesUtilities.notesTableName, noteId);
            DeleteNoteUseCaseResponse response = new DeleteNoteUseCaseResponse();
            response.Result = result;
            NotificationManager.NotifyDeleteNoteSucceeded();
            if (result)
                callback?.onSuccess(response);
            else
                callback?.onFailure(response);

        }

    }




}
