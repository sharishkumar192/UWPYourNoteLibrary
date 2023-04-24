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
        //          SQLiteAdapter.CloseConnection(conn);





        //    }
        //   catch(Exception e) { Logger.WriteLog(e.Message);  }
        //    finally
        //    {
        //          SQLiteAdapter.CloseConnection(conn);

        //    }

        //    //sqlite_cmd.CommandText = $"INSERT INTO {UserUtilities.userTableName}(UserId, Password,Name) VALUES ('{currentUser.Userid}' , ' " + { currentUser.Password} + "','" + currentUser.Name + "');";

        //}


        // ----------------------------------------ACCOUNT PAGE DB UPDATION----------------------------------------

        //Creates new note 



        // ----------------------------------------NOTE DISPLAY PAGE DB UPDATION----------------------------------------

        //Creates a new entry for the shared note
      
        //Updation of the Note
      
        //Updation of the Note Title
       

        

        //Updation of the Note Content
       


        










    }
}
