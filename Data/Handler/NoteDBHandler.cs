using System;
using UWPYourNoteLibrary.Data.Handler.Contract;
using UWPYourNoteLibrary.Models;
using System.Data.SQLite;
using UWPYourNoteLibrary.Data.Handler.Adapter;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Windows.UI.StartScreen;
using System.Linq;
using System.Diagnostics;
using UWPYourNoteLibrary.Util;
namespace UWPYourNoteLibrary.Data.Handler
{
    public class NoteDBHandler : INoteDBHandler
    {
        private SQLiteConnection conn = null;
        private static NoteDBHandler _NoteDBHandler = null;

        public static NoteDBHandler Singleton
        {

            get
            {
                if (_NoteDBHandler == null)
                {
                    _NoteDBHandler = new NoteDBHandler();

                }
                return _NoteDBHandler;

            }

        }
        public static void CreateNotesTable()
        {
            string query = $"CREATE TABLE IF NOT EXISTS {NotesUtilities.notesTableName}" +
      $"(USERID VARCHAR(10000)," +
      $"NOTEID INTEGER PRIMARY KEY AUTOINCREMENT," +
      $"TITLE VARCHAR(10000)," +
      $"CONTENT TEXT, " +
      $"NOTECOLOR INTEGER DEFAULT 0 ,  " +
      $"SEARCHCOUNT INTEGER DEFAULT 0  ,  " +
      $"CREATIONDAY VARCHAR(27)  ,  " +
      $"MODIFIEDDAY VARCHAR(27)  ,  " +
      $"FOREIGN KEY(USERID) REFERENCES  {UserUtilities.userTableName} (USERID) ON DELETE CASCADE)";
            SQLiteConnection conn = SQLiteAdapter.OpenConnection();
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                command.ExecuteNonQuery();
                conn.Close();


            }
            catch (Exception e) { Logger.WriteLog(e.Message); }

            finally
            {
                conn.Close();

            }

        }

        //Creates The Shared Table 

        public static void SharedNotesTableCreation()
        {
            string query = $"CREATE TABLE IF NOT EXISTS {NotesUtilities.sharedTableName}" +
     $"(SHAREDUSERID VARCHAR(10000) ,  " +
     $"SHAREDNOTEID INTEGER ," +
     $"PRIMARY KEY (SHAREDUSERID, SHAREDNOTEID)" +
     $" FOREIGN KEY(SHAREDUSERID) REFERENCES {UserUtilities.userTableName} (USERID) ON DELETE CASCADE" +
       $" FOREIGN KEY(SHAREDNOTEID) REFERENCES {NotesUtilities.notesTableName} (NOTEID) ON DELETE CASCADE)";
            SQLiteConnection conn = SQLiteAdapter.OpenConnection();
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();

            }

        }
    

    public long CreateNewNote(string tableName, Note newNote)
        {
            conn = SQLiteAdapter.OpenConnection();
            long noteId = -1;
            string query1 = $"INSERT INTO {tableName}  (USERID, TITLE, CONTENT, NOTECOLOR, CREATIONDAY, MODIFIEDDAY) VALUES (@userId, @title, @content, @noteColor, @creationDay, @modifiedDay);";
            string query2 = $"SELECT seq FROM sqlite_sequence where name =  '{tableName}' ; ";
            try
            {


                SQLiteCommand command = new SQLiteCommand(query1, conn);
                SQLiteParameter[] parameters = new SQLiteParameter[6];
                parameters[0] = new SQLiteParameter("@userId", newNote.userId);
                parameters[1] = new SQLiteParameter("@title", newNote.title);
                parameters[2] = new SQLiteParameter("@content", newNote.content);
                parameters[3] = new SQLiteParameter("@noteColor", newNote.noteColor);
                parameters[4] = new SQLiteParameter("@creationDay", newNote.creationDay);
                parameters[5] = new SQLiteParameter("@modifiedDay", newNote.modifiedDay);
                command.Parameters.Add(parameters[0]);
                command.Parameters.Add(parameters[1]);
                command.Parameters.Add(parameters[2]);
                command.Parameters.Add(parameters[3]);
                command.Parameters.Add(parameters[4]);
                command.Parameters.Add(parameters[5]);

                command.ExecuteNonQuery();

                command.Parameters.Remove(parameters[0]);
                command.Parameters.Remove(parameters[1]);
                command.Parameters.Remove(parameters[2]);
                command.Parameters.Remove(parameters[3]);
                command.Parameters.Remove(parameters[4]);
                command.Parameters.Remove(parameters[5]);
                command.CommandText = query2;

                noteId = (long)command.ExecuteScalar();
            }
            catch (Exception e) { Logger.WriteLog(e.Message); }
            finally
            {
                conn.Close();

            }
            return noteId;
        }

        public bool UpdateNoteContent(string notesTableName, Note noteToUpdate)// Needed
        {
            bool result = true;
            string query = $"UPDATE {notesTableName} SET  CONTENT= @content, MODIFIEDDAY = @modifiedDay WHERE NOTEID = @noteId  ;";
            conn = SQLiteAdapter.OpenConnection();
            try
            {

                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter[] parameters = new SQLiteParameter[3];
                parameters[0] = new SQLiteParameter("@content", noteToUpdate.content);
                parameters[1] = new SQLiteParameter("@modifiedDay", noteToUpdate.modifiedDay);
                parameters[2] = new SQLiteParameter("@noteId", noteToUpdate.noteId);


                command.Parameters.Add(parameters[0]);
                command.Parameters.Add(parameters[1]);
                command.Parameters.Add(parameters[2]);
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                result = false;
                Logger.WriteLog(e.Message);
            }
            finally
            {
                conn.Close();

            }
            return result;
        }

        public bool UpdateNote(string notesTableName, Note noteToUpdate)// Needed
        {
            bool result = true;
            string query = $"UPDATE {notesTableName} SET TITLE = @title, CONTENT= @content, MODIFIEDDAY = @modifiedDay WHERE NOTEID = @noteId  ;";
            conn = SQLiteAdapter.OpenConnection();
            try
            {

                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter[] parameters = new SQLiteParameter[4];
                parameters[0] = new SQLiteParameter("@title", noteToUpdate.title);
                parameters[1] = new SQLiteParameter("@content", noteToUpdate.content);
                parameters[2] = new SQLiteParameter("@modifiedDay", noteToUpdate.modifiedDay);
                parameters[3] = new SQLiteParameter("@noteId", noteToUpdate.noteId);


                command.Parameters.Add(parameters[0]);
                command.Parameters.Add(parameters[1]);
                command.Parameters.Add(parameters[2]);
                command.Parameters.Add(parameters[3]);
                command.ExecuteNonQuery();
                conn.Close();




            }
            catch (Exception e)
            {
                result = false;
                Logger.WriteLog(e.Message);
            }
            finally
            {
                conn.Close();

            }
            return result;
        }

        public bool UpdateNoteTitle(string notesTableName, Note noteToUpdate)// Needed
        {
            bool result = true;
            string query = $"UPDATE {notesTableName} SET TITLE= @title, MODIFIEDDAY = @modifiedDay WHERE NOTEID = @noteId  ;";
            conn = SQLiteAdapter.OpenConnection();
            try
            {

                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter[] parameters = new SQLiteParameter[3];
                parameters[0] = new SQLiteParameter("@title", noteToUpdate.title);
                parameters[1] = new SQLiteParameter("@modifiedDay", noteToUpdate.modifiedDay);
                parameters[2] = new SQLiteParameter("@noteId", noteToUpdate.noteId);


                command.Parameters.Add(parameters[0]);
                command.Parameters.Add(parameters[1]);
                command.Parameters.Add(parameters[2]);
                command.ExecuteNonQuery();
                conn.Close();




            }
            catch (Exception e)
            {
                result = false;
                Logger.WriteLog(e.Message);
            }
            finally
            {
                conn.Close();

            }
            return result;

        }

        public ObservableCollection<Note> GetPersonalNotes(string tableName, string userId)
        {
            ObservableCollection<Models.Note> notes = null;
            string query = $"SELECT * FROM {tableName} WHERE USERID = @userId   ";
            SQLiteConnection conn = SQLiteAdapter.OpenConnection();
            try
            {

                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter parameters = new SQLiteParameter("@userId", userId);
                command.Parameters.Add(parameters);
                using (SQLiteDataReader SQLite_datareader = command.ExecuteReader())
                {
                    while (SQLite_datareader.Read())
                    {
                        if (notes == null)
                            notes = new ObservableCollection<Models.Note>();
                        Note note = new Note(0, "", "", 0, 0, "");

                        note.noteId = (long)SQLite_datareader.GetValue(1);
                        note.title = SQLite_datareader.GetString(2);
                        note.content = SQLite_datareader.GetString(3);
                        note.noteColor = (long)SQLite_datareader.GetValue(4);
                        note.searchCount = (long)SQLite_datareader.GetValue(5);
                        note.modifiedDay = SQLite_datareader.GetString(7);
                        notes.Add(note);

                    }

                    SQLite_datareader.Close();
                }
            }

            catch (Exception e) { Logger.WriteLog(e.Message); }
            finally
            {
                conn.Close();

            }
            return notes;
        }

        public ObservableCollection<Note> GetSharedNotes(string notesTableName, string sharedTableName, string userId)
        {
            ObservableCollection<Models.Note> sharedNotes = null;
            string query = $"SELECT * FROM {notesTableName} , {sharedTableName}  WHERE NOTEID = SHAREDNOTEID AND SHAREDUSERID = @userId  ORDER BY SEARCHCOUNT DESC ; ";
            SQLiteConnection conn = SQLiteAdapter.OpenConnection();
            try
            {


                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter parameter = new SQLiteParameter("@userId", userId);
                command.Parameters.Add(parameter);

                using (SQLiteDataReader sqlite_datareader = command.ExecuteReader())
                {
                    while (sqlite_datareader.Read())
                    {
                        if (sharedNotes == null)
                            sharedNotes = new ObservableCollection<Models.Note>();
                        Note note = new Note(0, "", "", 0, 0, "");
                        note.noteId = (long)sqlite_datareader.GetValue(1);
                        note.title = sqlite_datareader.GetString(2);
                        note.content = sqlite_datareader.GetString(3);
                        note.noteColor = (long)sqlite_datareader.GetValue(4);
                        note.searchCount = (long)sqlite_datareader.GetValue(5);
                        note.modifiedDay = sqlite_datareader.GetString(7);

                        sharedNotes.Add(note);
                    }
                    sqlite_datareader.Close();


                }

            }
            catch (Exception e) { Logger.WriteLog(e.Message); }
            finally
            {
                conn.Close();

            }

            return sharedNotes;
        }

        public ObservableCollection<Note> GetAllNotes(string notesTableName, string sharedTableName, string userId)
        {


            var allNotes = new ObservableCollection<UWPYourNoteLibrary.Models.Note>();

            var pnotes = GetPersonalNotes(notesTableName, userId);
            var snotes = GetSharedNotes(notesTableName, sharedTableName, userId);

            if (pnotes != null)
                foreach (Note notes in pnotes)
                {
                    allNotes.Add(notes);
                }
            if (snotes != null)
                foreach (Note notes in snotes)
                {
                    allNotes.Add(notes);
                }
            return allNotes;
        }

        public ObservableCollection<Note> GetAllRecentNotes(string notesTableName, string sharedTableName, string userId)
        {

            ObservableCollection<Note> allRecentNotes = null;
            ObservableCollection<Note> tempList = GetAllNotes(notesTableName, sharedTableName, userId);
            tempList.OrderByDescending(note => note.searchCount);

            if (tempList != null)
            {
                foreach (Note note in tempList)
                {
                    if (note.searchCount > 0)
                    {
                        if (allRecentNotes == null)
                            allRecentNotes = new ObservableCollection<UWPYourNoteLibrary.Models.Note>();
                        allRecentNotes.Add(note);
                        if (allRecentNotes.Count == 5)
                            break;
                    }
                }
            }
            return allRecentNotes;
        }

        public ObservableCollection<Models.Note> GetSuggestedNotes(string tableName, string userId, string title)
        {
            ObservableCollection<Models.Note> notes = null;
            string query = $"SELECT * FROM {tableName} where USERID = @userId AND TITLE like @title ; ";
            string ntitle = "%" + title + "%";
            SQLiteConnection conn = SQLiteAdapter.OpenConnection();
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter[] parameters = new SQLiteParameter[2];
                parameters[0] = new SQLiteParameter("@userId", userId);
                parameters[1] = new SQLiteParameter("@title", ntitle);

                command.Parameters.Add(parameters[0]);
                command.Parameters.Add(parameters[1]);
                using (SQLiteDataReader sqlite_datareader = command.ExecuteReader())
                {
                    while (sqlite_datareader.Read())
                    {
                        if (notes == null)
                            notes = new ObservableCollection<Models.Note>();
                        Note note = new Note(0, "", "", 0, 0, "")
                        {
                            noteId = (long)sqlite_datareader.GetValue(1),
                            title = sqlite_datareader.GetString(2),
                            content = sqlite_datareader.GetString(3),
                            noteColor = (long)sqlite_datareader.GetValue(4),
                            searchCount = (long)sqlite_datareader.GetValue(5),
                            modifiedDay = sqlite_datareader.GetString(7)
                        };
                        notes.Add(note);

                    }
                }

                conn.Close();
            }
            catch (Exception e) { Logger.WriteLog(e.Message); }
            finally
            {
                conn?.Close();

            }
            return notes;

        }

        public bool InsertSharedNote(string sharedTableName, string sharedUserId, long noteId)// Needed
        {
            bool result = true;
            string query = $"INSERT INTO {sharedTableName} VALUES (@SHAREDUSERID, @NOTEID);";
            conn = SQLiteAdapter.OpenConnection();
            try
            {


                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter[] parameters = new SQLiteParameter[2];
                parameters[0] = new SQLiteParameter("@sharedUserId", sharedUserId);
                parameters[1] = new SQLiteParameter("@noteId", noteId);
                command.Parameters.Add(parameters[0]);
                command.Parameters.Add(parameters[1]);
                command.ExecuteNonQuery();
                conn.Close();






            }
            catch (Exception e)
            {
                result = false;
                Logger.WriteLog(e.Message);
            }
            finally
            {
                conn.Close();

            }
            return result;
        }

        //Delete the Note
        public bool DeleteNote(string notesTableName, long noteId)// Needed 
        {
            bool result = true;

            string query = $"DELETE FROM {notesTableName} WHERE NOTEID  = @noteId ; ";

            conn = SQLiteAdapter.OpenConnection();

            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter parameters = new SQLiteParameter("@noteId", noteId);
                command.Parameters.Add(parameters);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                result = false;
                Logger.WriteLog(e.Message);
            }
            finally
            {
                conn.Close();

            }
            return result;


        }


        public  bool UpdateNoteCount(string notesTableName, long searchCount, long noteId)
        {
            bool result = false;
            string query = $"UPDATE {notesTableName} SET  SEARCHCOUNT = @count  WHERE NOTEID = @noteId  ;";
            SQLiteConnection conn = new SQLiteConnection();
            try
            {

                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter[] parameters = new SQLiteParameter[2];
                parameters[0] = new SQLiteParameter("@count", searchCount);
                parameters[1] = new SQLiteParameter("@noteId", noteId);


                command.Parameters.Add(parameters[0]);
                command.Parameters.Add(parameters[1]);
                command.ExecuteNonQuery();
                result = true; 
            }
            catch (Exception e) { Logger.WriteLog(e.Message); }
            finally
            {
                conn.Close();

            }
            return result;

        }



        public bool UpdateNoteColor(string tableName, long noteId, long noteColor, string modifiedDay)
        {
            bool result = false;
            string query = $"UPDATE  {tableName} SET NOTECOLOR = @noteColor, MODIFIEDDAY = @modifiedDay  WHERE NOTEID = @noteId ; ";
            SQLiteConnection conn = SQLiteAdapter.OpenConnection();
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter[] parameters = new SQLiteParameter[3];
                parameters[0] = new SQLiteParameter("@noteColor", noteColor);
                parameters[1] = new SQLiteParameter("@modifiedDay", modifiedDay);
                parameters[2] = new SQLiteParameter("@noteId", noteId);

                command.Parameters.Add(parameters[0]);
                command.Parameters.Add(parameters[1]);
                command.Parameters.Add(parameters[2]);
                command.ExecuteNonQuery();
                 result = true;
            }
            catch (Exception e) { Logger.WriteLog(e.Message); }
            finally
            {
                conn.Close();

            }

            return result;
        }


        public bool CanShareNote(string tableName, string userId, long noteId)
        {
            bool isOwner = false;
            string query = $"SELECT * FROM {tableName} WHERE USERID = @userId AND NOTEID = @noteId ; ";
            SQLiteConnection conn = SQLiteAdapter.OpenConnection();
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter[] parameters = new SQLiteParameter[2];
                parameters[0] = new SQLiteParameter("@userId", userId);
                parameters[1] = new SQLiteParameter("@noteId", noteId);

                command.Parameters.Add(parameters[0]);
                command.Parameters.Add(parameters[1]);
                using (SQLiteDataReader sqlite_datareader = command.ExecuteReader())
                {
                    while (sqlite_datareader.Read())
                    {
                        isOwner = true;
                        break;

                    }

                    sqlite_datareader.Close();
                }
                conn.Close();

            }
            catch (Exception e) { Logger.WriteLog(e.Message); }
            finally
            {
                conn.Close();

            }

            return isOwner;

        }

        public Dictionary<string, bool> AlreadySharedUsers(string tableName, long noteId)
        {
            Dictionary<string, bool> sharedUserIds = null;

            string query = $"SELECT SHAREDUSERID FROM {tableName} WHERE SHAREDNOTEID = @noteId ; ";
            SQLiteConnection conn = SQLiteAdapter.OpenConnection();
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter parameter = new SQLiteParameter("@noteId", noteId);

                command.Parameters.Add(parameter);
                using (SQLiteDataReader sqlite_datareader = command.ExecuteReader())
                {
                    while (sqlite_datareader.Read())
                    {
                        if (sharedUserIds == null)
                            sharedUserIds = new Dictionary<string, bool>();
                        sharedUserIds.Add(sqlite_datareader.GetString(0), true);
                    }

                    sqlite_datareader.Close();
                }
                conn.Close();
            }
            catch (Exception e) { Logger.WriteLog(e.Message); }
            finally
            {
                conn.Close();

            }

            return sharedUserIds;

        }


        public ObservableCollection<UWPYourNoteLibrary.Models.User> ValidUsersToShare(string userTableName, string sharedTableName, string notesTableName, string userId, long noteId)// Needed
        {
            Dictionary<string, bool> sharedUserIds = AlreadySharedUsers(sharedTableName, noteId);
            ObservableCollection<UWPYourNoteLibrary.Models.User> userToShare = new ObservableCollection<Models.User>(); ;
            string query = $"SELECT * FROM {userTableName} WHERE USERID != @userId ; ";
            SQLiteConnection conn = SQLiteAdapter.OpenConnection();
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter parameter = new SQLiteParameter("@userId", userId);

                command.Parameters.Add(parameter);

                using (SQLiteDataReader sqlite_datareader = command.ExecuteReader())
                {
                    while (sqlite_datareader.Read())
                    {
                        string name = sqlite_datareader.GetString(0);
                        string validUserId = sqlite_datareader.GetString(1);

                        if ((sharedUserIds != null && !sharedUserIds.ContainsKey(validUserId)) || sharedUserIds == null)
                        {
                            Models.User user = new Models.User(name, validUserId);
                            userToShare.Add(user);
                        }
                    }

                    sqlite_datareader.Close();
                }
                conn.Close();
            }
            catch (Exception e) { Logger.WriteLog(e.Message); }
            finally
            {
                conn.Close();

            }



            return userToShare;
        }

    }




}
