# telegrambot_custom_authenticatoin
This is a custom C# written telegram bot API's assistance, which does restrict other people to access this BOT without admin approval.

In Order to communnicate with Telegram, there are several Telegram CLient which directly call these api. you can write your own client if want.
Here I have used the best known .NET Client for Telegram Bot API

https://github.com/TelegramBots/Telegram.Bot

BotAuthentication manages a small sqlite db in it's backend, based on user status it will allow user to query or response with a static message.  

 public enum UserStatusEnum
                    {
                        Approve = 1,
                        WaitingForApproval,
                        Block,
                        NoPermission
                    }
               
HOW TO CALL https://github.com/taskinsust/telegrambot_custom_authentication

ITelegramAuthentication BOTAuthentication = new TelegramAuthentication();

        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            if (message == null && (message.Type != MessageType.Text || message.Type != MessageType.Contact))
                return;

            if (message.Contact != null)
            {
                //update contact information based on UserId
                var updateResult = BOTAuthentication.UpdateUserCredential(message.From.FirstName, message.From.LastName, message.From.Username, message.Contact.UserId, message.Contact.PhoneNumber);
                var responseResult = JsonConvert.DeserializeObject<ResponseResult>(updateResult);
                await SendMessage(message, responseResult.Message);
                return;
            }

            var result = BOTAuthentication.CheckUserCredential(message.From.IsBot, message.From.FirstName, message.From.LastName, message.From.Username, message.From.Id);
            var response = JsonConvert.DeserializeObject<ResponseResult>(result);

            if (!response.IsSuccess)
            {
                await SendMessage(message, response.Message);
                if (response.Message.Equals(ErrorMessages.FIRST_TIME_USER_REQUEST))
                    await RequestContact(message);
                return;
            }
        }
