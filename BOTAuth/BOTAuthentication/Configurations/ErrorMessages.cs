using System;
using System.Collections.Generic;
using System.Text;

namespace BOTAuthentication.Configurations
{
    public static class ErrorMessages
    {
        public static string DO_NOT_SUPPORT_BOT_QUERY = "we do not process any BOT request!";
        public static string WAITING_FOR_APPROVAL = "thanks for query with opus! you need admin permission to chat with me!";
        public static string FIRST_TIME_USER_REQUEST = "your request has been sent to admin! he will let you inform!";
        public static string THANK_YOU = "thanks so much for your cooperation!";
        public static string ERROR_MESAGE = "currently processing other requests! please try later.";

        public static string ACCOUNT_BLOCK = "your account is temporary block! please contact admin.";
    }
}
