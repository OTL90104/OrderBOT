using isRock.LineBot;
using OrderBot.EventManager;
using OrderBot.Utility;
using System;
using System.Collections.Generic;

namespace OrderBot
{
    internal class MessageHelper
    {
        private Bot bot;
        private string ChannelAccessToken;


        public MessageHelper(Bot bot, string ChannelAccessToken)
        {
            this.bot = bot;
            this.ChannelAccessToken = ChannelAccessToken;
        }

        public void MessageProcess(Event item, ReceievedMessage receivedMessage)
        {

            try
            {

                switch (item.type)
                {
                    case "postback":

                        PostbackManager postbackManager = new PostbackManager();
                        postbackManager.Process(item, receivedMessage, ChannelAccessToken);
                        break;

                    case "join":

                        break;

                    case "message":

                        MessageManager messageManager = new MessageManager();
                        messageManager.Process(item, receivedMessage, ChannelAccessToken, bot);

                        break;


                    case "follow":
                        FollowManager followManager = new FollowManager();
                        followManager.process(item, receivedMessage, ChannelAccessToken, bot);
                        break;
                    default:
                        break;

                }

            }
            catch (Exception ex)
            {

            }

        }


    }
}