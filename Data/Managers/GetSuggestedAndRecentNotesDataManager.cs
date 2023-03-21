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
    public class GetSuggestedAndRecentNotesDataManager : IGetSuggestedAndRecentNotesUseCaseDataManager
    {
        public IUserDBHandler DBHandler { get; set; }
        private static GetSuggestedAndRecentNotesDataManager dataManager;
        public static GetSuggestedAndRecentNotesDataManager DataManager
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
            DBHandler = UserDBHandler.Handler;
            ObservableCollection<Note> list = null;
            if(text.Length >=3)
            {
                list = DBHandler.GetSuggestedNotes(DBCreation.notesTableName, userId, text);
            }
            else
            {
                
                IGetNotesUseCaseDataManager dataManager = GetNotesDataManager.DataManager;
                ObservableCollection<Note> tempList = dataManager.GetNotes(userId);
                tempList.OrderByDescending(note => note.searchCount);
                if (tempList != null)
                {
                    foreach (Note note in tempList)
                    {
                        if (note.searchCount > 0)
                        {
                            if (list == null)
                                list = new ObservableCollection<UWPYourNoteLibrary.Models.Note>();
                            list.Add(note);
                            if (list.Count == 5)
                                break;
                        }
                    }
                }
            }
           
            GetSuggestedAndRecentNotesUseCaseResponse response = new GetSuggestedAndRecentNotesUseCaseResponse();
            response.List = list;


            if (list == null)
                callback?.onSuccess(response);
            else
                callback?.onFailure(response);

        }


    }
}
