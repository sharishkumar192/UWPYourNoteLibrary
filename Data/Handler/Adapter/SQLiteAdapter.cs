using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Windows.Storage;

namespace UWPYourNoteLibrary.Data.Handler.Adapter
{
    public class SQLiteAdapter
    {
        private static SQLiteConnection _sqliteConnection = null;
        public static SQLiteConnection SQLiteConnection
        {

            get
            {
                if (_sqliteConnection == null)
                {
                    StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                    _sqliteConnection = new SQLiteConnection("Data Source=" + localFolder.Path + "\\database.db;foreign keys=true; Version = 3;  New = True; Compress = True; ");

                }
                return _sqliteConnection;

            }

        }
        public static SQLiteConnection OpenConnection()
        {
            SQLiteConnection sqlite_conn = SQLiteConnection;
            sqlite_conn.Open();
            return sqlite_conn;
        }
    }
}
