using BOTAuthentication.Configurations;
using BOTAuthentication.DataAccess;
using BOTAuthentication.Enums;
using BOTAuthentication.Helper;
using BOTAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOTAuthentication.Services
{
    public class UserService
    {
        private readonly UserDao _userDao;
        public UserService()
        {
            DbHelper.CreateDb();
            var sqliteConnection = DbHelper.OpenDbConnection();
            _userDao = new UserDao(sqliteConnection);
        }

        internal ResponseResult CheckAuthentication(string firstName, string lastName, string userName, int userId, string contactNo)
        {
            try
            {
                var ss = (int)UserStatusEnum.WaitingForApproval;
                List<UserProfile> userList = _userDao.LoadByUserData(firstName, lastName, userName, userId);
                if (userList == null || userList.Count <= 0)
                {
                    bool success = _userDao.Insert(new UserProfile() { FirstName = firstName, LastName = lastName, UserName = userName, CreationDate = DateTime.Now.ToString(), UserId = userId, ContactNo = contactNo });
                    if (success)
                        return new ResponseResult(false, ErrorMessages.FIRST_TIME_USER_REQUEST);
                }
                else if (userList.Where(x => x.UserStatus == (int)UserStatusEnum.WaitingForApproval).ToList().Count > 0)
                {
                    return new ResponseResult(false, ErrorMessages.WAITING_FOR_APPROVAL);
                }
                else if (userList.Where(x => x.UserStatus == (int)UserStatusEnum.Block).ToList().Count > 0)
                {
                    return new ResponseResult(false, ErrorMessages.ACCOUNT_BLOCK);
                }

                return new ResponseResult(true, "");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal ResponseResult UpdateUserCredential(string firstName, string lastName, string username, int userId, string phoneNumber)
        {
            try
            {
                bool success = _userDao.Update(new UserProfile() { FirstName = firstName, LastName = lastName, UserName = username, CreationDate = DateTime.Now.ToString(), UserId = userId, ContactNo = phoneNumber });
                if (success)
                    return new ResponseResult(true, ErrorMessages.THANK_YOU);
                return new ResponseResult(false, ErrorMessages.ERROR_MESAGE);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
