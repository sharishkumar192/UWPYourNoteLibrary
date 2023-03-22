using Microsoft.Data.Sqlite;
using System;
using System.Data.SQLite;
using UWPYourNoteLibrary.Data;
using UWPYourNoteLibrary.Data.Handler;
using UWPYourNoteLibrary.Data.Handler.Adapter;

namespace UWPYourNoteLibrary.Models 
{ 
    public class DBUpdation
    {

        // ----------------------------------------SIGN UP PAGE DB UPDATION----------------------------------------

        //Creates new currentUser 
        //public static void InsertNewUser(Models.User user) //Needed
        //{
        //    SQLiteConnection conn  = new  SQLiteConnection() ;
        //    try
        //    {


        //           string query = $"INSERT INTO {UserUtilities.userTableName}(NAME, USERID, PASSWORD) VALUES ( @name, @userId, @password);";


        //        SQLiteCommand command = new SQLiteCommand(query, conn);
        //        SQLiteParameter[] parameters = new SQLiteParameter[3];
        //        //parameters[0] = new SQLiteParameter("@userTableName", UserUtilities.userTableName);
        //        parameters[0] = new SQLiteParameter("@name", user.name);
        //        parameters[1] = new SQLiteParameter("@userId", user.userId);
        //        parameters[2] = new SQLiteParameter("@password", user.password);
        //        command.Parameters.Add(parameters[0]);
        //        command.Parameters.Add(parameters[1]);
        //        command.Parameters.Add(parameters[2]);
        //        // command.Parameters.Add(parameters[3]);
        //        command.ExecuteNonQuery();
        //        conn.Close();





        //    }
        //   catch(Exception e) { Logger.WriteLog(e.Message);  }
        //    finally
        //    {
        //        conn.Close();

        //    }

        //    //sqlite_cmd.CommandText = $"INSERT INTO {UserUtilities.userTableName}(UserId, Password,Name) VALUES ('{currentUser.Userid}' , ' " + { currentUser.Password} + "','" + currentUser.Name + "');";

        //}


        // ----------------------------------------ACCOUNT PAGE DB UPDATION----------------------------------------

        //Creates new note 



        // ----------------------------------------NOTE DISPLAY PAGE DB UPDATION----------------------------------------

        //Creates a new entry for the shared note
      
        //Updation of the Note
      
        //Updation of the Note Title
       

        public static void UpdateNoteCount(string notesTableName, long searchCount, long noteId)
        {
            string query = $"UPDATE {notesTableName} SET  SEARCHCOUNT = @count  WHERE NOTEID = @noteId  ;";
            SQLiteConnection conn  = new  SQLiteConnection() ;
            try
            {

                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter[] parameters = new SQLiteParameter[2];
                parameters[0] = new SQLiteParameter("@count", searchCount);
                parameters[1] = new SQLiteParameter("@noteId", noteId);


                command.Parameters.Add(parameters[0]);
                command.Parameters.Add(parameters[1]);
                command.ExecuteNonQuery();
                conn.Close();
            }
           catch(Exception e) { Logger.WriteLog(e.Message);  }
            finally
            {
                conn.Close();

            }


        }

        //Updation of the Note Content
       


        

        public static void UpdateNoteColor(string tableName, long noteId, long noteColor, string modifiedDay)
        {
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
                conn.Close();

            }
            catch (Exception e) { Logger.WriteLog(e.Message); }
            finally
            {
                conn.Close();

            }


        }








    }
}
