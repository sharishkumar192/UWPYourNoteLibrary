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

namespace UWPYourNoteLibrary.Data.Managers
{
    public class GetNotesDataManager : IGetNotesUseCaseDataManager
    {
        public IUserDBHandler DBHandler { get; set; }
        private static GetNotesDataManager dataManager;
        public static GetNotesDataManager DataManager
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
        public void GetNotes(string userId, string type, bool IsSort, ICallback<GetNotesUseCaseResponse> callback)
        {
            DBHandler = UserDBHandler.Handler;
            ObservableCollection<Note> list = null;
            if (type.CompareTo("PersonalNotes") == 0)
                list = DBHandler.GetPersonalNotes(DBCreation.notesTableName, userId);
            else if (type.CompareTo("SharedNotes") == 0)
                list = DBHandler.GetSharedNotes(DBCreation.notesTableName, DBCreation.sharedTableName, userId);
            else
                list = DBHandler.GetAllNotes(DBCreation.notesTableName, DBCreation.sharedTableName, userId);
            GetNotesUseCaseResponse response = new GetNotesUseCaseResponse();
            response.List = list;
            response.IsSort = IsSort;

            if (list == null)
                callback?.onSuccess(response);
            else
                callback?.onFailure(response);

        }
        public ObservableCollection<Note> GetNotes(string userId)
        {
            DBHandler = UserDBHandler.Handler;
            ObservableCollection<Note> list = DBHandler.GetAllNotes(DBCreation.notesTableName, DBCreation.sharedTableName, userId);
            return list;

        }




    }
}
