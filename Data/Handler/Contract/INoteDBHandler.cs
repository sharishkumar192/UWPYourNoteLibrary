using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPYourNoteLibrary.Models;
using System.Data.SQLite;
using Windows.System.RemoteSystems;
using Windows.Storage;
using UWPYourNoteLibrary.Data.Handler.Contract;
using UWPYourNoteLibrary.Data.Handler.Adapter;
using System.Collections.ObjectModel;
using System.Net.Http;
using Windows.System;

namespace UWPYourNoteLibrary.Data.Handler.Contract
{
    public interface INoteDBHandler
    {
        long CreateNewNote(string tableName, Note newNote);
        bool UpdateNoteContent(string tableName, Note noteToUpdate);
        bool UpdateNote(string tableName, Note noteToUpdate);
        bool UpdateNoteTitle(string tableName, Note noteToUpdate);

        ObservableCollection<Note> GetSuggestedNotes(string tableName, string userId, string title);
        ObservableCollection<Note> GetPersonalNotes(string notesTableName, string userId);
        ObservableCollection<Note> GetSharedNotes(string notesTableName, string sharedTableName, string userId);
        ObservableCollection<Note> GetAllNotes(string notesTableName, string sharedTableName, string userId);

        ObservableCollection<Note> GetAllRecentNotes(string notesTableName, string sharedTableName, string userId);
        bool InsertSharedNote(string tableName, string sharedUserId, long noteId);

        bool DeleteNote(string notesTableName, long noteId);



    }
}
