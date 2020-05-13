using System;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;
//using OPUS.TelegramBotClient.Enums;
//using OPUS.TelegramBotClient.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.InlineQueryResults;

using BOTService.Model;
using BOTAuthentication;
using Newtonsoft.Json;
using BOTAuthentication.Configurations;

namespace OPUS.BOTService
{
    public static class Program
    {
        private static Telegram.Bot.TelegramBotClient Bot;
        private static ITelegramAuthentication BOTAuthentication;

        public static async Task Main()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        Bot = new Telegram.Bot.TelegramBotClient("1044782323:AAG5KZu_7Igg9jKztbq9ib3MlKYpMjkmINE");
                        BOTAuthentication = new TelegramAuthentication();

                        Bot.OnMessage += BotOnMessageReceived;
                        Bot.OnMessageEdited += BotOnMessageReceived;
                        Bot.OnReceiveError += BotOnReceiveError;

                        Bot.StartReceiving(Array.Empty<UpdateType>());

                        Console.ReadLine();

                    }
                    catch (Exception e)
                    {
                        Thread.Sleep(1000);
                    }
                }
            }
            catch (Exception e)
            {
                Thread.Sleep(2000);
            }
        }

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

        static async Task RequestContact(Message message)
        {
            var RequestReplyKeyboard = new ReplyKeyboardMarkup(new[]
            {
                  KeyboardButton.WithRequestContact("Contact"),
                });
            await Bot.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Who are you?",
                replyMarkup: RequestReplyKeyboard
            );
        }

        static async Task SendMessage(Message message, string response)
        {
            await Bot.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: response,
                    replyMarkup: new ReplyKeyboardRemove()
                );
        }

        private static void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            Console.WriteLine("Received error: {0} — {1}",
                receiveErrorEventArgs.ApiRequestException.ErrorCode,
                receiveErrorEventArgs.ApiRequestException.Message
            );
        }
    }
}