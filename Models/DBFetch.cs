using System;
using System.Data.SQLite;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection;
using Windows.Security.Cryptography.Core;
using Windows.System;
using Windows.UI.Xaml.Controls.Primitives;
using UWPYourNoteLibrary.Data.Handler.Adapter;

namespace UWPYourNoteLibrary.Models
{
    public class DBFetch
    {
        public  readonly static int _logincount = 2;

        // ----------------------------------------SIGN UP PAGE DB FETCHES----------------------------------------

        //Checks the email Id which the currentUser enters already exists or not 
        public static bool CheckValidEmail(string userTableName, string userId)// Needed
        {
        
            bool check = false;
            string query = $"SELECT * FROM  {userTableName}  WHERE USERID = @userId ";
            SQLiteConnection conn  = SQLiteAdapter.OpenConnection();
            try
            {

                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter parameters = new SQLiteParameter("@userId", userId);
                command.Parameters.Add(parameters);
                using (SQLiteDataReader sqlite_datareader = command.ExecuteReader())
                {
                    while (sqlite_datareader.Read())
                    {
                        check = true;

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
            return check;

        }



        //  ----------------------------------------LOGIN IN PAGE DB FETCHES----------------------------------------

        //Gets the Suggestion list
        //public static ObservableCollection<Models.Note> SuggestList(string tableName, string searchText)
        //{

        //    string query = $"SELECT * FROM {tableName} WHERE TITLE LIKE  " + "'%" + "@searchText" + "%'" + " ;";
        //    ObservableCollection<Models.Note> suggested = null;
        //    SQLiteConnection conn  = SQLiteAdapter.OpenConnection();
        //    try
        //    {
        //        SQLiteCommand command = new SQLiteCommand(query, conn);
        //        SQLiteParameter parameters = new SQLiteParameter("@searchText", searchText);
        //        command.Parameters.Add(parameters);
        //        using (SQLiteDataReader sqlite_datareader = command.ExecuteReader())
        //        {
        //            while (sqlite_datareader.Read())
        //            {
        //                if (suggested == null)
        //                    suggested = new ObservableCollection<Models.Note>();
        //                Note note = new Note(0, "", "", 0, 0, "");

        //                note.noteId = (long)sqlite_datareader.GetValue(1);
        //                note.title = sqlite_datareader.GetString(2);
        //                note.content = sqlite_datareader.GetString(3);
        //                note.noteColor = (long)sqlite_datareader.GetValue(4);
        //                note.searchCount = (long)sqlite_datareader.GetValue(5);
        //                note.modifiedDay = sqlite_datareader.GetString(7);
        //                suggested.Add(note);
        //            }

        //            sqlite_datareader.Close();
        //        }
        //        conn.Close();

        //    }

        //    catch (Exception e) { Logger.WriteLog(e.Message); }
        //    finally
        //    {
        //        conn.Close();

        //    }

        //    return suggested;

        //}

        // It prints all the data of the currentUser 
        public static ObservableCollection<Models.User> FrequentLoggedUsers(string userTableName)// Needed
        {
           
            string query = $"SELECT * FROM {userTableName} WHERE LOGINCOUNT >= @count ORDER BY LOGINCOUNT DESC; ";
            ObservableCollection<Models.User> users = null;
            SQLiteConnection conn  = SQLiteAdapter.OpenConnection();
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter parameters = new SQLiteParameter("@count", _logincount);
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



        // Checks if the currentUser's id and password is present in the table or not, updates the login count of the user if the user exists 
       


        
        //  ----------------------------------------HOME PAGE DB FETCHES----------------------------------------  

        //Get Suggested Notes
       


        //Get Personal Notes

        public static ObservableCollection<Models.Note> GetPersonalNotes(string noteTableName, string userId)// Needed
        {
            ObservableCollection<Models.Note> notes = null;


            string query = $"SELECT * FROM {noteTableName} WHERE USERID = @userId   ";
            SQLiteConnection conn  = SQLiteAdapter.OpenConnection() ;
            try
            {

                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter parameters = new SQLiteParameter("@userId", userId);
                command.Parameters.Add(parameters);
                using (SQLiteDataReader sqlite_datareader = command.ExecuteReader())
                {
                    while (sqlite_datareader.Read())
                    {
                        if (notes == null)
                            notes = new ObservableCollection<Models.Note>();
                        Note note = new Note(0, "", "", 0, 0, "");

                        note.noteId = (long)sqlite_datareader.GetValue(1);
                        note.title = sqlite_datareader.GetString(2);
                        note.content = sqlite_datareader.GetString(3);
                        note.noteColor = (long)sqlite_datareader.GetValue(4);
                        note.searchCount = (long)sqlite_datareader.GetValue(5);
                        note.modifiedDay = sqlite_datareader.GetString(7);
                        notes.Add(note);

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
            return notes;
        }

        //Get Shared Notes
        public static ObservableCollection<Models.Note> GetSharedNotes(string notesTableName, string sharedTableName, string userId)// Needed
        {
            ObservableCollection<Models.Note> sharedNotes = null;
            string query = $"SELECT * FROM {notesTableName} , {sharedTableName}  WHERE NOTEID = SHAREDNOTEID AND SHAREDUSERID = @userId  ORDER BY SEARCHCOUNT DESC ; ";
            SQLiteConnection conn  = SQLiteAdapter.OpenConnection();
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
                conn.Close();

            }
            catch (Exception e) { Logger.WriteLog(e.Message); }
            finally
            {
                conn.Close();

            }

            return sharedNotes;
        }



        // ----------------------------------------NOTE DISPLAY PAGE DB FETCH----------------------------------------

        public static Dictionary<string, bool> AlreadySharedUsers(string tableName, long noteId)
        {
            Dictionary<string, bool> sharedUserIds = null;

            string query = $"SELECT SHAREDUSERID FROM {tableName} WHERE SHAREDNOTEID = @noteId ; ";
            SQLiteConnection conn  = SQLiteAdapter.OpenConnection();
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
        //It prints all the available suggested to whom we can share the note 
        public static ObservableCollection<UWPYourNoteLibrary.Models.User> ValidUsersToShare(string userTableName, string sharedTableName, string notesTableName, string userId, long noteId)// Needed
        {
            Dictionary<string, bool> sharedUserIds = AlreadySharedUsers(sharedTableName, noteId);
            ObservableCollection<UWPYourNoteLibrary.Models.User> userToShare = new ObservableCollection<Models.User>(); ;
            string query = $"SELECT * FROM {userTableName} WHERE USERID != @userId ; ";
            SQLiteConnection conn  = SQLiteAdapter.OpenConnection();
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


        public static bool CanShareNote(string tableName, string userId, long noteId)
        {
            bool isOwner = false;
            string query = $"SELECT * FROM { tableName } WHERE USERID = @userId AND NOTEID = @noteId ; ";
            SQLiteConnection conn  = SQLiteAdapter.OpenConnection();
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





    }
}

