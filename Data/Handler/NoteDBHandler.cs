using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPYourNoteLibrary.Models;
using System.Data.SQLite;
using Windows.System.RemoteSystems;
using Windows.Storage;

namespace UWPYourNoteLibrary.Data.Handler
{
    internal class NoteDBHandler : INoteDBHandler
    {
        private SQLiteConnection conn = null;
        private static NoteDBHandler _noteDBHandler = null;
        
        public static NoteDBHandler NDBHandler
        {

            get
            {
                if (_noteDBHandler == null)
                {
                    _noteDBHandler = new NoteDBHandler();

                }
                return _noteDBHandler;

            }

        }
        public bool InsertNewUser(string tableName, string username, string email, string password)
        {
            bool result = true;
            conn = DBAdapter.OpenConnection();
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
                conn.Close();
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
    }
}
