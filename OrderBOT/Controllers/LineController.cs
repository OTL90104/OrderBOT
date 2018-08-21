using OrderBot.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrderBot
{
    public class LineController : ApiController
    {
        [HttpPost]
        public IHttpActionResult POST()
        {

           
            //OrderBot好友已滿
           // string ChannelAccessToken = "QH6zLQR76ZyP+uRpvS5ugYBN+nfF8FzNmJRO9nsrcGlMqLpzVC0bkB/v6O4O1xHGAiVz33lARDoW6IfDS8jolqvvKXaZhj9CZTnnKwFdbe14K2lWs+EoKaJZ1ak/266S1K3voR9TQXRwIEN1eCQHRwdB04t89/1O/w1cDnyilFU=";
            string ChannelAccessToken = "rIJEwJQXXG6kQpQiyyDBjkeYQMUG81WhxsXK62gtg7nQl94gwBOq88izfx6LFyi5lWTMlGy1Ark0P+r918l0a9LeY7yvDY0yUirH6dMAOa3bI1s7hyjoD9s4B3k5mSx9euLG7qd+7UfR6J5h+9x8+wdB04t89/1O/w1cDnyilFU=";
                 
                       
            
            //建立Bot instance
            isRock.LineBot.Bot bot =
                new isRock.LineBot.Bot(ChannelAccessToken);  //傳入Channel access token
            MessageHelper messageHelper = new MessageHelper(bot, ChannelAccessToken);
            try
            {

                //取得 http Post RawData(should be JSON)
                string postData = Request.Content.ReadAsStringAsync().Result;
                //剖析JSON
                var ReceivedMessage = isRock.LineBot.Utility.Parsing(postData);

                foreach (var item in ReceivedMessage.events)
                {
                    //分多執行緒處理多個Event
                    Task.Factory.StartNew(() => messageHelper.MessageProcess(item, ReceivedMessage));
                   // messageHelper.MessageProcess(item, ReceivedMessage);
                }
                // var item = ReceivedMessage.events.FirstOrDefault();
                // messageHelper.MessageProcess(item, ReceivedMessage);


                return Ok();

            }
            catch (Exception ex)
            {
                return Ok();
            }

        }
    }
}