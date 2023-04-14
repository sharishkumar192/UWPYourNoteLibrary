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
using UWPYourNoteLibrary.Util;
namespace UWPYourNoteLibrary.Data.Managers
{
    public class GetSuggestedAndRecentNotesDataManager : IGetSuggestedAndRecentNotesUseCaseDataManager
    {
        public INoteDBHandler DBHandler { get; set; }
        private static GetSuggestedAndRecentNotesDataManager dataManager;
        public static GetSuggestedAndRecentNotesDataManager Singleton
        {
            get
            {
                if (dataManager == null)
                {
                    dataManager = new GetSuggestedAndRecentNotesDataManager();
                }
                return dataManager;
            }
        }
        public void GetSuggestedAndRecentNotes(string userId, string text, ICallback<GetSuggestedAndRecentNotesUseCaseResponse> callback)
        {
            DBHandler = NoteDBHandler.Singleton;
            ObservableCollection<Note> list = null;
            if(text.Length >=3)
            {
                list = DBHandler.GetSuggestedNotes(NotesUtilities.notesTableName, userId, text);
            }
            else
            {
                IGetNotesUseCaseDataManager dataManager = GetNotesDataManager.Singleton;
                list = dataManager.GetRecentNotes(userId);
            }
           
            GetSuggestedAndRecentNotesUseCaseResponse response = new GetSuggestedAndRecentNotesUseCaseResponse();
            response.List = list;


            if (list == null)
                callback?.onFailure(response);
            else
                callback?.onSuccess(response);

        }


    }
}
