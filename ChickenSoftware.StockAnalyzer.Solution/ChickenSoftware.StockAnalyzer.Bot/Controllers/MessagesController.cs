using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Utilities;
using Newtonsoft.Json;

namespace ChickenSoftware.StockAnalyzer.Bot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<Message> Post([FromBody]Message message)
        {
            if (message.Type == "Message")
            {
                var stockProvider = new StockProvider();
                var ticker = message.Text;
                var currentPrice = stockProvider.GetMostRecentPrice(ticker);
                var replyMessage = String.Empty;
                var nextDate = DateTime.Now.AddDays(1);
                var predictedPrice = stockProvider.PredictStockPrice(ticker, nextDate);
                if (currentPrice == -1 || predictedPrice == -1)
                {
                    replyMessage = "Could not be found!";
                }
                else
                {
                    replyMessage = "The current price is " + currentPrice + " and the predicted price is " + predictedPrice;
                }
                return message.CreateReplyMessage(replyMessage);


            }
            else
            {
                return HandleSystemMessage(message);
            }
        }

        private Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Ping";
                return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == "BotAddedToConversation")
            {
            }
            else if (message.Type == "BotRemovedFromConversation")
            {
            }
            else if (message.Type == "UserAddedToConversation")
            {
            }
            else if (message.Type == "UserRemovedFromConversation")
            {
            }
            else if (message.Type == "EndOfConversation")
            {
            }

            return null;
        }
    }
}