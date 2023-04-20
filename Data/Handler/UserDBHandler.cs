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
using UWPYourNoteLibrary.Util;
using System.Diagnostics;

namespace UWPYourNoteLibrary.Data.Handler
{
    public class UserDBHandler : IUserDBHandler
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


        public static void CreateUserTable()
        {
            string query =
            $"CREATE TABLE IF NOT EXISTS {UserUtilities.userTableName} (NAME VARCHAR(10000)," +
            $" USERID VARCHAR(10000) PRIMARY KEY," +
            $" PASSWORD VARCHAR(10000)," +
            $" LOGINCOUNT INTEGER DEFAULT 0 )";
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
            Debug.WriteLine(conn);
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

        }
}
