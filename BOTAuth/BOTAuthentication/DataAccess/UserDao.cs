using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using BOTAuthentication.Enums;
using BOTAuthentication.Models;

namespace BOTAuthentication.DataAccess
{
    public class UserDao : BaseDao
    {
        public UserDao(SQLiteConnection connection) : base(connection)
        {

        }

        #region Single object load

        internal List<UserProfile> LoadByUserData(string FirstName = "", string LastName = "", string Username = "", int userId = 0)
        {
            string query = "";
            if (userId > 0)
            {
                query += @"select * from UserProfile where UserId =" + userId + " ORDER BY Id LIMIT 1";
            }
            else
                query = @" select * from UserProfile where FirstName like '%" + FirstName + "%' or " +
                                "LastName like '%" + LastName + "%' or Username like '%" + Username + "%'  ORDER BY Id LIMIT 1";
            this.command.CommandText = query;
            return Fill();
        }

        #endregion

        #region Operation

        public bool Insert(UserProfile obj)
        {
            try
            {
                string queryStr = string.Format(@"INSERT INTO  UserProfile
                                               (
                                                [UserId]    
                                                ,[ContactNo] 
                                                , FirstName
                                                , LastName
                                                , UserName
                                                , UserStatus
                                                , CreationDate
                                               )
                                            VALUES({0}, '{1}', '{2}', '{3}', '{4}', {5}, '{6}')
                                          ",
                                            obj.UserId,
                                            AddSlashes(obj.ContactNo),
                                            AddSlashes(obj.FirstName),
                                            AddSlashes(obj.LastName),
                                            AddSlashes(obj.UserName),
                                            (int)UserStatusEnum.WaitingForApproval,
                                            AddSlashes(obj.CreationDate)
                                           );
                int numOfRows = ExecuteOperationQuery(queryStr);
                return numOfRows > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        internal bool Update(UserProfile userProfile)
        {
            try
            {
                string queryStr = string.Format(@"UPDATE UserProfile SET [ContactNo] ='{0}', FirstName='{1}',
                                                LastName='{2}' WHERE UserId = {3}",

                                               userProfile.ContactNo,
                                               userProfile.FirstName,
                                               userProfile.LastName,
                                               userProfile.UserId);

                int numOfRows = ExecuteOperationQuery(queryStr);
                return numOfRows > 0;
            }
            catch (Exception e) { return false; }
        }

        internal bool Delete(List<int> list)
        {
            try
            {
                string queryStr = string.Format(@"DELETE FROM UserProfile 
                                           WHERE Id in ({0})
                                          ", String.Join(",", list.ToArray())
                                              );
                ExecuteOperationQuery(queryStr);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion

        #region Others

        private List<UserProfile> Fill()
        {
            var resList = new List<UserProfile>();
            try
            {
                //now execute query
                this.reader = this.command.ExecuteReader();
                if (this.reader.HasRows)
                {
                    while (this.reader.Read())
                    {
                        int i = 0;
                        var row = new UserProfile();
                        row.Id = reader.GetInt32(i++);
                        row.UserId = reader.GetInt32(i++);
                        row.ContactNo = reader.GetString(i++);
                        row.FirstName = reader.GetString(i++);
                        row.LastName = reader.GetString(i++);
                        row.UserName = reader.GetString(i++);
                        row.UserStatus = reader.GetInt32(i++);
                        row.CreationDate = reader.GetString(i++);
                        resList.Add(row);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //close the reader
                if (this.reader != null && this.reader.IsClosed == false)
                    this.reader.Close();
            }

            //return value
            if (resList == null || resList.Count < 1)
                return null;
            return resList;
        }

        #endregion

    }
}
