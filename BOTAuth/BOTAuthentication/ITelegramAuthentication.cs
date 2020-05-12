using System;
using System.Collections.Generic;
using System.Text;

namespace BOTAuthentication
{
    public interface ITelegramAuthentication
    {
        bool IsAuthentic { get; set; }
        string CheckUserCredential(bool isBot, string FirstName = "", string LastName = "", string Username = "", int userId = 0, string contactNo = "");
        void LimitCheck(bool isBot, string FirstName = "", string LastName = "", string Username = "");
        string UpdateUserCredential(string firstName = "", string lastName = "", string username = "", int userId = 0, string phoneNumber = "");
    }
}
