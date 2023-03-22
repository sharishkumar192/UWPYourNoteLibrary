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

namespace UWPYourNoteLibrary.Data.Handler
{
    internal class UserDBHandler : IUserDBHandler
    {

        private SQLiteConnection conn = null;
        private static UserDBHandler _userDBHandler = null;

        public static UserDBHandler Singleton
        {

            get
            {
                if (_userDBHandler == null)
                {
                    _userDBHandler = new UserDBHandler();

                }
                return _userDBHandler;

            }

        }
        public bool InsertNewUser(string tableName, string username, string email, string password)
        {
            bool result = true;
            conn = SQLiteAdapter.OpenConnection();
            try
            {


                string query = $"INSERT INTO {tableName}(NAME, USERID, PASSWORD) VALUES ( @name, @userId, @password);";


                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter[] parameters = new SQLiteParameter[3];

                parameters[0] = new SQLiteParameter("@name", username);
                parameters[1] = new SQLiteParameter("@userId", email);
                parameters[2] = new SQLiteParameter("@password", password);

                command.Parameters.Add(parameters[0]);
                command.Parameters.Add(parameters[1]);
                command.Parameters.Add(parameters[2]);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog(e.Message);
                result = false;
            }
            finally
            {
                conn.Close();

            }
            return result;
        }

        public bool CheckIfExistingEmail(string tableName, string userId)
        {
            bool result = false;
            conn = SQLiteAdapter.OpenConnection();
            try
            {


                string query = $"SELECT USERID FROM {tableName} WHERE USERID = @userId;";


                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter parameters = new SQLiteParameter("@userId", userId);
                
                command.Parameters.Add(parameters);

                using (SQLiteDataReader sqlite_datareader = command.ExecuteReader())
                {
                    while (sqlite_datareader.Read())
                    {
                        string userIdCheck = sqlite_datareader.GetString(0);
                        if(userIdCheck == userId)
                            result = true;

                    }

                    sqlite_datareader.Close();
                }
            }
            catch (Exception e)
            {
                Logger.WriteLog(e.Message);
                result = false;
            }
            finally
            {
                conn.Close();

            }
            return result;
        }

        public Models.User ValidateUser(string tableName, string loggedUserId, string loggedPassword)
        {
            conn = SQLiteAdapter.OpenConnection();
            Models.User userDetails = null;
            string query1 = $"SELECT * FROM {tableName} WHERE USERID = @userId  AND PASSWORD = @password ; ";

            string query2 = $"UPDATE  {tableName}  SET LOGINCOUNT = LOGINCOUNT+1  WHERE USERID = @userId ; ";
        
            try
            {

                SQLiteCommand command = new SQLiteCommand(query1, conn);
                SQLiteParameter[] parameters = new SQLiteParameter[2];
                parameters[0] = new SQLiteParameter("@userId", loggedUserId);
                parameters[1] = new SQLiteParameter("@password", loggedPassword);

                command.Parameters.Add(parameters[0]);
                command.Parameters.Add(parameters[1]);
                using (SQLiteDataReader sqlite_datareader = command.ExecuteReader())
                {
                    while (sqlite_datareader.Read())
                    {
                        string name = sqlite_datareader.GetString(0);
                        string userId = sqlite_datareader.GetString(1);
                        string password = sqlite_datareader.GetString(2);
                        long loginCount = (long)sqlite_datareader.GetValue(3);
                        userDetails = new Models.User(name, userId, password, loginCount);
                   
                    }


                    sqlite_datareader.Close();
                }

                command.CommandText = query2;
                command.Parameters.Remove(parameters[1]);
                command.ExecuteNonQuery();
         



            }
            catch (Exception e) { Logger.WriteLog(e.Message); }
            finally
            {
                conn.Close();

            }

            return userDetails;


        }

        public  ObservableCollection<Models.User> RecentLoggedInUsers(string userTableName)// Needed
        {

            string query = $"SELECT * FROM {userTableName} WHERE LOGINCOUNT >= @count ORDER BY LOGINCOUNT DESC; ";
            ObservableCollection<Models.User> users = null;
            SQLiteConnection conn = SQLiteAdapter.OpenConnection();
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter parameters = new SQLiteParameter("@count", DBFetch._logincount);
                command.Parameters.Add(parameters);
                using (SQLiteDataReader sqlite_datareader = command.ExecuteReader())
                {
                    while (sqlite_datareader.Read())
                    {
                        if (users == null)
                            users = new ObservableCollection<Models.User>();
                        Models.User user = new Models.User(sqlite_datareader.GetString(0), sqlite_datareader.GetString(1), sqlite_datareader.GetString(2), (long)sqlite_datareader.GetValue(3));
                        users.Add(user);
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

            return users;
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

        public  ObservableCollection<Models.Note> GetSuggestedNotes(string tableName, string userId, string title)
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
                conn.Close();

            }
            return notes;

        }
    }
}
