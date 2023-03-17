using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using System.Data.SQLite;
using Windows.UI.Xaml.Media.Animation;
using UWPYourNoteLibrary.Data.Handler;
using UWPYourNoteLibrary.Models;
namespace UWPYourNoteLibrary.Models
{
    public class DBCreation
    {
        public static string userTableName = "UserTable",
            notesTableName = "NotesTable",
            sharedTableName = "ShareTable";
        public static void UserTableCreation()
        {
            CreateUserTable();
        }

        public static void NotesTableCreation()
        {
            CreateNotesTable();
        }

        // Creates an object of SQLiteConnection 

      


        //Creates The User Table 
        public static void CreateUserTable()
        {
            string query =
            $"CREATE TABLE IF NOT EXISTS {DBCreation.userTableName } (NAME VARCHAR(10000)," +
            $" USERID VARCHAR(10000) PRIMARY KEY," +
            $" PASSWORD VARCHAR(10000)," +
            $" LOGINCOUNT INTEGER DEFAULT 0 )";
            SQLiteConnection conn  = new  SQLiteConnection() ;
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
           catch(Exception e) { Logger.WriteLog(e.Message);  }

            finally
            {
                conn.Close();

            }

        }

        //Creates The Notes Table 

        public static void CreateNotesTable()
        {
            string query = $"CREATE TABLE IF NOT EXISTS {DBCreation.notesTableName}" +
      $"(USERID VARCHAR(10000)," +
      $"NOTEID INTEGER PRIMARY KEY AUTOINCREMENT," +
      $"TITLE VARCHAR(10000)," +
      $"CONTENT TEXT, " +
      $"NOTECOLOR INTEGER DEFAULT 0 ,  " +
      $"SEARCHCOUNT INTEGER DEFAULT 0  ,  " +
      $"CREATIONDAY VARCHAR(27)  ,  " +
      $"MODIFIEDDAY VARCHAR(27)  ,  " +
      $"FOREIGN KEY(USERID) REFERENCES  { DBCreation.userTableName} (USERID) ON DELETE CASCADE)" ;
            SQLiteConnection conn = SQLiteAdapter.OpenConnection();
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                command.ExecuteNonQuery();
                conn.Close();


            }
           catch(Exception e) { Logger.WriteLog(e.Message);  }

            finally
            {
                conn.Close();

            }

        }

        //Creates The Shared Table 

        public static void SharedNotesTableCreation()
        {
            string query = $"CREATE TABLE IF NOT EXISTS {DBCreation.sharedTableName}" +
     $"(SHAREDUSERID VARCHAR(10000) ,  " +
     $"SHAREDNOTEID INTEGER ," +
     $"PRIMARY KEY (SHAREDUSERID, SHAREDNOTEID)" +
     $" FOREIGN KEY(SHAREDUSERID) REFERENCES { DBCreation.userTableName} (USERID) ON DELETE CASCADE" +
       $" FOREIGN KEY(SHAREDNOTEID) REFERENCES {DBCreation.notesTableName} (NOTEID) ON DELETE CASCADE)";
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
    }
}

