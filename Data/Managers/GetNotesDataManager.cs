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
    public class GetNotesDataManager : IGetNotesUseCaseDataManager
    {
        public INoteDBHandler DBHandler { get; set; }
        private static GetNotesDataManager dataManager;
        public static GetNotesDataManager Singleton
        {
            get
            {
                if (dataManager == null)
                {
                    dataManager = new GetNotesDataManager();
                }
                return dataManager;
            }
        }
        public void GetNotes(string userId, TypeOfNote type, bool IsSort, ICallback<GetNotesUseCaseResponse> callback)
        {
            DBHandler = NoteDBHandler.Singleton;
            ObservableCollection<Note> list = null;
            switch(type)
            {
                case TypeOfNote.PersonalNotes: list = DBHandler.GetPersonalNotes(NotesUtilities.notesTableName, userId); break;
                case TypeOfNote.SharedNotes:   list = DBHandler.GetSharedNotes(NotesUtilities.notesTableName, NotesUtilities.sharedTableName, userId); break;
                case TypeOfNote.AllNotes : list = DBHandler.GetAllNotes(NotesUtilities.notesTableName, NotesUtilities.sharedTableName, userId); break;
                default: list = null; break;
            }
            GetNotesUseCaseResponse response = new GetNotesUseCaseResponse();
            response.List = list;
            response.IsSort = IsSort;

            if (list == null)
                callback?.onSuccess(response);
            else
                callback?.onFailure(response);

        }
        public ObservableCollection<Note> GetRecentNotes(string userId)
        {
            DBHandler = NoteDBHandler.Singleton;
            ObservableCollection<Note> list = DBHandler.GetAllRecentNotes(NotesUtilities.notesTableName, NotesUtilities.sharedTableName, userId);
            return list;

        }




    }
}
