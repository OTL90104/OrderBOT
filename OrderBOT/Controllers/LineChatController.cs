using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrderBOT.Controllers
{
    public class LineChatController : ApiController
    {
       
        [HttpPost]
        public IHttpActionResult POST()
        {
            string ChannelAccessToken = "azk9zrJd7AMC2Hs5bwyEOxK+8bi2DD3kaJWIHvnO7AS2vyUpW/VYaaqyZJfPF5bm9hVwOSEBCHLTEwGwrccIf0i9UOoIdA2iOT3IJZrq/1eLfC5jBKHibdqJbFhpqhpvCADPRY9gV3Oyj3HKnVq5ZAdB04t89/1O/w1cDnyilFU=";

            try
            {
                //取得 http Post RawData(should be JSON)
                string postData = Request.Content.ReadAsStringAsync().Result;
                //剖析JSON
                var ReceivedMessage = isRock.LineBot.Utility.Parsing(postData);
                //回覆訊息
                string Message;
                Message = "你說了:" + ReceivedMessage.events[0].message.text;
                //回覆用戶
                isRock.LineBot.Utility.ReplyMessage(ReceivedMessage.events[0].replyToken, Message, ChannelAccessToken);
                //回覆API OK
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }

    }
}
