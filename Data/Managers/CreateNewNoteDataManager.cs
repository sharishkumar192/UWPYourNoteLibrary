using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPYourNoteLibrary.Data.Handler.Contract;
using UWPYourNoteLibrary.Data.Handler;
using UWPYourNoteLibrary.Domain.Contract;
using UWPYourNoteLibrary.Domain.UseCase;
using UWPYourNoteLibrary.Models;

namespace UWPYourNoteLibrary.Data.Managers
{
    public class CreateNewNoteDataManager : ICreateNewNoteUseCaseDataManager
    {
        public INoteDBHandler DBHandler { get; set; }
        private static CreateNewNoteDataManager dataManager;
        public static CreateNewNoteDataManager Singleton
        {
            get
            {
                if (dataManager == null)
                {
                    dataManager = new CreateNewNoteDataManager();
                }
                return dataManager;
            }
        }
        public void CreateNewNote(Note newNote, ICallback<CreateNewNoteUseCaseResponse> callback)
        {
            DBHandler = NoteDBHandler.Singleton;
            long noteId = DBHandler.CreateNewNote(DBCreation.notesTableName, newNote);

            CreateNewNoteUseCaseResponse response = new CreateNewNoteUseCaseResponse();
            response.NoteId = noteId;

            if (noteId != -1)
                callback?.onSuccess(response);
            else
                callback?.onFailure(response);

        }


    }
}
