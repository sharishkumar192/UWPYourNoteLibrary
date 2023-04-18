﻿using System;
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
                StorageFolder localFolder1 = ApplicationData.Current.LocalFolder;
                SQLiteConnection sasdas = new SQLiteConnection("Data Source=" + localFolder1.Path + "\\database.db;foreign keys=true; Version = 3;  New = True; Compress = True; ");
                ;
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
            try
            {
                SQLiteConnection sqlite_conn = SQLiteConnection;
                sqlite_conn.Open();
                return sqlite_conn;
            }
            catch (Exception e)
            {
               
            }
            return null;
               }
    }
}
