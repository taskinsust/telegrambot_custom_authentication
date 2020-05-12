using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace BOTAuthentication.Helper
{
    public static class DbHelper
    {
        public const string DatabaseName = "telegramuser.db3";
        internal const string DbPassword = "T@sK1n";

        public static bool CreateDb()
        {
            try
            {
                //string source = "G:\\Taskin" + "\\" + DatabaseName; ;
                var source = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "\\DB" + "\\" + DatabaseName;
                if (!File.Exists(source))
                {
                    //string FilePath = source;// Path.Combine(source, DatabaseName);
                    SQLiteConnection.CreateFile(source);
                    //File.Create(FilePath);

                    using (var sqliteConnection = new SQLiteConnection("Data Source=" + source + ";Version=3;"))
                    {
                        //sqliteConnection.SetPassword(DbPassword);
                        sqliteConnection.Open();
                        var command = new SQLiteCommand();
                        command.Connection = sqliteConnection;

                        string userSql = @"CREATE TABLE [UserProfile] 
                                                    (
                                                         [Id] INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT
                                                        ,[UserId] INTEGER  NOT NULL   
                                                        ,[ContactNo] VARCHAR(50)
                                                        ,[FirstName]  VARCHAR(50) 
                                                        ,[LastName]  VARCHAR(50) 
                                                        ,[UserName]  VARCHAR(50) 
                                                        ,[UserStatus]  INTEGER  NOT NULL
                                                        ,[CreationDate] VARCHAR(50) 
                                                    )";
                        command.CommandText = userSql;
                        command.ExecuteNonQuery();

                        sqliteConnection.Close();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static SQLiteConnection OpenDbConnection()
        {
            var dbConnection = "Data Source=" + Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "\\DB" + "\\" + DatabaseName + ";Version=3;";
            try
            {
                var sql_con = new SQLiteConnection(dbConnection);
                sql_con.Open();
                return sql_con;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
