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
    public class UpdateNoteDataManager : IUpdateNoteUseCaseDataManager
    {
        public INoteDBHandler DBHandler { get; set; }
        private static UpdateNoteDataManager dataManager;
        public static UpdateNoteDataManager Singleton
        {
            get
            {
                if (dataManager == null)
                {
                    dataManager = new UpdateNoteDataManager();
                }
                return dataManager;
            }
        }
        public void UpdateNote(Note noteToUpdate, bool isTitleChanged, bool isContentChanged, ICallback<UpdateNoteUseCaseResponse> callback)
        {
            DBHandler = new NoteDBHandler() ;

            string tableName = NotesUtilities.notesTableName;
            bool result;
            if(isTitleChanged && isContentChanged)
            {
                result= DBHandler.UpdateNote(tableName, noteToUpdate);
            }
            else if(isTitleChanged)
            {
                result = DBHandler.UpdateNoteTitle(tableName, noteToUpdate);
            }
            else
                result = DBHandler.UpdateNoteContent(tableName, noteToUpdate);

            UpdateNoteUseCaseResponse response = new UpdateNoteUseCaseResponse();
            response.Result = result;

            NotificationManager.NotifyUpdateNoteSucceeded(noteToUpdate);

            if (result)
                callback?.onSuccess(response);
            else
                callback?.onFailure(response);

        }
        
        }




    }
