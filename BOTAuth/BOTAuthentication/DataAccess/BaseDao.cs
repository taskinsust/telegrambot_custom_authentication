using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace BOTAuthentication.DataAccess
{
    public class BaseDao
    {
        protected SQLiteCommand command;
        protected SQLiteDataReader reader;
        protected SQLiteConnection sqLiteConnection;

        public BaseDao(SQLiteConnection connection)
        {
            this.sqLiteConnection = connection;
            command = sqLiteConnection.CreateCommand();
        }

        //Remove unusal slash from string
        protected String AddSlashes(String strData)
        {
            if (String.IsNullOrEmpty(strData))
                return "";
            //adding slashes for database
            strData = strData.Replace("\\", "\\\\");    //add a slash before   slash    character
            strData = strData.Replace("'", "\\'");      //add a slash before   single   quote character
            strData = strData.Replace("\"", "\\\"");    //add a slash before   double   quote character
            return strData;
        }

        //Execute query
        public string ExecuteQuery(string sql)
        {
            try
            {
                command.CommandText = sql;
                object value = command.ExecuteScalar();
                if (value != null)
                {
                    return value.ToString();
                }
                return "";
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //Execute query
        public int ExecuteOperationQuery(string sql)
        {
            try
            {
                command.CommandText = sql;
                int rowsUpdated = command.ExecuteNonQuery();
                return rowsUpdated;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
