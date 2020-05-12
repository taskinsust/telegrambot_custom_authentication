using Newtonsoft.Json;
using BOTAuthentication.Configurations;
using BOTAuthentication.Helper;
using BOTAuthentication.Models;
using BOTAuthentication.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOTAuthentication
{
    public class TelegramAuthentication : ITelegramAuthentication
    {
        private readonly UserService _userService;
        public TelegramAuthentication()
        {
            _userService = new UserService();
        }

        public bool IsAuthentic { get; set; }

        public string CheckUserCredential(bool isBot = false, string firstName = "", string lastName = "",
            string userName = "", int userId = 0, string contactNo = "")
        {
            try
            {
                if (isBot)
                    return Serializer.Serialize<ResponseResult>(new ResponseResult(false, ErrorMessages.DO_NOT_SUPPORT_BOT_QUERY));
                ResponseResult result = _userService.CheckAuthentication(firstName, lastName, userName, userId, contactNo);
                IsAuthentic = result.IsSuccess;
                return Serializer.Serialize(result);
            }
            catch (Exception e)
            {
                return Serializer.Serialize(new ResponseResult(false, ErrorMessages.ERROR_MESAGE));
            }
        }

        public void LimitCheck(bool isBot, string FirstName = "", string LastName = "", string Username = "")
        {
            throw new NotImplementedException();
        }

        public string UpdateUserCredential(string firstName, string lastName, string username, int userId, string phoneNumber)
        {
            try
            {
                ResponseResult result = _userService.UpdateUserCredential(firstName, lastName, username, userId, phoneNumber);
                return Serializer.Serialize(result);
            }
            catch (Exception e)
            {
                return Serializer.Serialize(new ResponseResult(false, ErrorMessages.ERROR_MESAGE));
            }
        }
    }
}
