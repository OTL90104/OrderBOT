using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using isRock.LineBot;
using OrderBot.SQLObject;
using OrderBot.TemplateMakers;
using OrderBot.Utility;

namespace OrderBot.EventManager
{
    public class MessageManager
    {
        enum FUN
        {
            TEXT,
            STICKER,
            IMAGE
        }

        internal void Process(Event item, ReceievedMessage receivedMessage, string channelAccessToken, Bot bot)
        {
            UserStatus userStatus = new UserStatus(item.source.userId);
            userStatus.SelectByUserID();
            string message = "Hi!";
            string imageURL;
            int millisecondsTimeout = 1000; // 1 sec

            NEXTMESSAGETYPE NextMessageType = RecognizeMessageType(userStatus);

            switch (NextMessageType)
            {
                case NEXTMESSAGETYPE.BUTTON:
                    break;
                case NEXTMESSAGETYPE.CAROUSEL:
                    break;
                case NEXTMESSAGETYPE.CONFIRM:
                    MakeConfirmByUserStatus(item, receivedMessage, channelAccessToken, bot, userStatus);
                    break;
                case NEXTMESSAGETYPE.MESSAGE:
                    MakeMessageByUserStatus(item, receivedMessage, channelAccessToken, bot, userStatus);
                    break;
                case NEXTMESSAGETYPE.ERROR:
                    break;
                default:
                    break;
            }

            if (item.message.text.ToLower().Trim() == "menu")
            {
                //List<string> list = new List<string>();
                //list.Add("Monday");
                //List<DateTime> tmp = PeriodSplitter.Cut(DateTime.Now, DateTime.Now.AddDays(30), list);

                string displayName = bot.GetUserInfo(item.source.userId).displayName;
                ImagemapMessage imagemapMessage = ImagemapMaker.MakeMenu(item);

                //發送
                bot.PushMessage(item.source.userId, imagemapMessage);
                if (item.source.groupId != null)
                {
                    bot.PushMessage(item.source.groupId, $@"HI!!{displayName},我已經私密囉~\n請使用手機至個人訊息觀看~");
                }
            }
            else if (item.message.text == "test")
            {

            }
            else if (item.message.text == "我要進入訂單模式!!!")
            {
                ButtonsTemplate buttonsTemplate = ButtonMaker.Make(2, 0, "");
                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, buttonsTemplate, channelAccessToken);
            }
            else if (item.message.text == "我要進入個人模式!!!")
            {
                ButtonsTemplate buttonsTemplate = ButtonMaker.Make(7, 0, "");
                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, buttonsTemplate, channelAccessToken);
            }
            else if (item.message.text == "我要進入社團模式!!!")
            {
                ButtonsTemplate buttonsTemplate = ButtonMaker.Make(9, 0, "");
                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, buttonsTemplate, channelAccessToken);
            }
            else if (item.message.text == "我要進入商店模式!!!")
            {
                ButtonsTemplate buttonsTemplate = ButtonMaker.Make(11, 0, "");
                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, buttonsTemplate, channelAccessToken);
            }
            else if (item.message.text == "Hey~OrderBot!")
            {
                Random random = new Random();
                int FunType = random.Next(0, 3);
                int FunMessage = random.Next(1, 11);
                LineUserInfo UserInfo = isRock.LineBot.Utility.GetUserInfo(item.source.userId, channelAccessToken);


                switch ((FUN)FunType)
                {

                    case FUN.TEXT:
                        switch (FunMessage)
                        {
                            case 1:
                                message = $"Hi!!!\n{UserInfo.displayName}，你好!!";
                                break;
                            case 2:
                                message = $"Hi~{UserInfo.displayName}，今天天氣真好呢!";
                                break;
                            case 3:
                                isRock.LineBot.Utility.PushMessage(item.source.userId, "自毀程式啟動......", channelAccessToken);
                                System.Threading.Thread.Sleep(millisecondsTimeout);
                                isRock.LineBot.Utility.PushMessage(item.source.userId, "3...", channelAccessToken);
                                System.Threading.Thread.Sleep(millisecondsTimeout);
                                isRock.LineBot.Utility.PushMessage(item.source.userId, "2...", channelAccessToken);
                                System.Threading.Thread.Sleep(millisecondsTimeout);
                                isRock.LineBot.Utility.PushMessage(item.source.userId, "1...", channelAccessToken);
                                System.Threading.Thread.Sleep(millisecondsTimeout);
                                System.Threading.Thread.Sleep(millisecondsTimeout);
                                message = "騙你的(・3・)";
                                break;
                            case 4:
                                isRock.LineBot.Utility.PushMessage(item.source.userId, "匯款成功!", channelAccessToken);
                                System.Threading.Thread.Sleep(millisecondsTimeout);
                                System.Threading.Thread.Sleep(millisecondsTimeout);
                                message = "假的。";
                                break;
                            case 5:
                                isRock.LineBot.Utility.PushMessage(item.source.userId, "川普跌倒變甚麼?!", channelAccessToken);
                                System.Threading.Thread.Sleep(millisecondsTimeout);
                                message = "三普";
                                break;
                            case 6:
                                imageURL = "https://www.artsticket.com.tw//Images/Products/39088.gif";
                                isRock.LineBot.Utility.ReplyImageMessage(item.replyToken, imageURL, imageURL, channelAccessToken);
                                message = "要一起去聽音樂會嗎??\n https://www.artsticket.com.tw/CKSCC2005/Product/Product00/ProductsDetailsPage.aspx?ProductID=hsobWfDDQ3QsVce6Tl6OHg";
                                break;
                            case 7:
                                isRock.LineBot.Utility.PushMessage(item.source.userId, "我根本沒把你放在眼裡", channelAccessToken);
                                System.Threading.Thread.Sleep(millisecondsTimeout);
                                message = "因為我把你放在心裡";
                                break;
                            case 8:
                                message = "你應該喜歡一個讓你笑的人，比如我";
                                break;
                            case 9:
                                message = "有一天丁丁走在路上，他的肚子被揍了一拳就變成ㄎㄎ了";
                                break;
                            case 10:
                                message = "火柴突然覺得頭癢，抓著抓著就著火了";
                                break;
                            default:
                                break;
                        }
                        isRock.LineBot.Utility.PushMessage(item.source.userId, message, channelAccessToken);
                        break;
                    case FUN.STICKER:
                        isRock.LineBot.Utility.ReplyStickerMessage(item.replyToken, 1, FunMessage, channelAccessToken);
                        break;
                    case FUN.IMAGE:

                        switch (FunMessage)
                        {
                            case 1:

                                imageURL = UserInfo.pictureUrl;
                                isRock.LineBot.Utility.PushImageMessage(item.source.userId, imageURL, imageURL, channelAccessToken);
                                break;
                            case 2:
                                imageURL = "https://scontent.ftpe1-1.fna.fbcdn.net/v/t1.0-9/36449945_1073857182766226_5028801027327918080_n.jpg?_nc_cat=0&oh=5d918c0bbb9f0c75371d4a2a5a7b971a&oe=5BCB5AF8";
                                isRock.LineBot.Utility.ReplyImageMessage(item.replyToken, imageURL, imageURL, channelAccessToken);
                                break;
                            case 3:
                                imageURL = "https://i.imgur.com/u4N3wpJ.jpg";
                                isRock.LineBot.Utility.ReplyImageMessage(item.replyToken, imageURL, imageURL, channelAccessToken);
                                break;
                            case 4:
                                var ImageCarouselTemplate = new isRock.LineBot.ImageCarouselTemplate();
                                {
                                    var ImageCarouselColumn1 = new isRock.LineBot.ImageCarouselColumn
                                    {
                                        imageUrl = new Uri("https://i.imgur.com/iPm2zIr.jpg"),
                                        action = new isRock.LineBot.MessageAction() { label = "老婆", text = "Hi!老婆" }
                                    };
                                    var ImageCarouselColumn2 = new isRock.LineBot.ImageCarouselColumn
                                    {
                                        imageUrl = new Uri("https://i.imgur.com/327g7wZ.jpg"),
                                        action = new isRock.LineBot.MessageAction() { label = "老婆", text = "Hi!老婆" }
                                    };
                                    var ImageCarouselColumn3 = new isRock.LineBot.ImageCarouselColumn
                                    {
                                        imageUrl = new Uri("https://i.imgur.com/9AWUtHf.jpg"),
                                        action = new isRock.LineBot.MessageAction() { label = "老婆", text = "Hi!老婆" }
                                    };
                                    var ImageCarouselColumn4 = new isRock.LineBot.ImageCarouselColumn
                                    {
                                        imageUrl = new Uri("https://i.imgur.com/O5RvsPIl.jpg"),
                                        action = new isRock.LineBot.MessageAction() { label = "老婆", text = "Hi!老婆" }
                                    };
                                    var ImageCarouselColumn5 = new isRock.LineBot.ImageCarouselColumn
                                    {
                                        imageUrl = new Uri("https://i.imgur.com/Mc5OmPL.jpg"),
                                        action = new isRock.LineBot.MessageAction() { label = "老婆", text = "Hi!老婆" }
                                    };
                                    var ImageCarouselColumn6 = new isRock.LineBot.ImageCarouselColumn
                                    {
                                        imageUrl = new Uri("https://i.imgur.com/sDQqSA8l.jpg"),
                                        action = new isRock.LineBot.MessageAction() { label = "老婆", text = "Hi!老婆" }
                                    };
                                    var ImageCarouselColumn7 = new isRock.LineBot.ImageCarouselColumn
                                    {
                                        imageUrl = new Uri("https://i.imgur.com/pWyyUfm.jpg"),
                                        action = new isRock.LineBot.MessageAction() { label = "老婆", text = "Hi!老婆" }
                                    };
                                    var ImageCarouselColumn8 = new isRock.LineBot.ImageCarouselColumn
                                    {
                                        imageUrl = new Uri("https://i.imgur.com/jLwCqZBl.jpg"),
                                        action = new isRock.LineBot.MessageAction() { label = "老婆", text = "Hi!老婆" }
                                    };
                                    var ImageCarouselColumn9 = new isRock.LineBot.ImageCarouselColumn
                                    {
                                        imageUrl = new Uri("https://i.imgur.com/34B5fssl.jpg"),
                                        action = new isRock.LineBot.MessageAction() { label = "老婆", text = "Hi!老婆" }
                                    };

                                    ImageCarouselTemplate.columns.Add(ImageCarouselColumn1);
                                    ImageCarouselTemplate.columns.Add(ImageCarouselColumn2);
                                    ImageCarouselTemplate.columns.Add(ImageCarouselColumn3);
                                    ImageCarouselTemplate.columns.Add(ImageCarouselColumn4);
                                    ImageCarouselTemplate.columns.Add(ImageCarouselColumn5);
                                    ImageCarouselTemplate.columns.Add(ImageCarouselColumn6);
                                    ImageCarouselTemplate.columns.Add(ImageCarouselColumn7);
                                    ImageCarouselTemplate.columns.Add(ImageCarouselColumn8);
                                    ImageCarouselTemplate.columns.Add(ImageCarouselColumn9);
                                }
                                bot.PushMessage(item.source.userId, ImageCarouselTemplate);
                                break;

                            case 5:
                                imageURL = "https://imgur.dcard.tw/74v1kPo.jpg";
                                isRock.LineBot.Utility.ReplyImageMessage(item.replyToken, imageURL, imageURL, channelAccessToken);
                                break;

                            case 6:
                                imageURL = "https://media3.giphy.com/media/L3nWlmgyqCeU8/giphy.gif";
                                isRock.LineBot.Utility.ReplyImageMessage(item.replyToken, imageURL, imageURL, channelAccessToken);
                                break;

                            case 7:
                                imageURL = "https://stickershop.line-scdn.net/stickershop/v1/product/1312/LINEStorePC/main@2x.png;compress=true";
                                isRock.LineBot.Utility.ReplyImageMessage(item.replyToken, imageURL, imageURL, channelAccessToken);
                                break;

                            case 8:
                                imageURL = "https://aishow.co/images/aHR0cHM6Ly9wMC5zc2wucWhpbWdzNC5jb20vdDAxMzA1YmNjZjFmZGZjZThjMC5qcGc";
                                isRock.LineBot.Utility.ReplyImageMessage(item.replyToken, imageURL, imageURL, channelAccessToken);
                                break;

                            case 9:
                                imageURL = "https://cdn2.theweek.co.uk/sites/theweek/files/styles/16x8_465/public/2018/03/lionel_messi_release_clause_barcelona_transfer_news_getty_images_927115406.jpg?itok=7uhp6i3Y";
                                isRock.LineBot.Utility.ReplyImageMessage(item.replyToken, imageURL, imageURL, channelAccessToken);
                                break;

                            case 10:
                                imageURL = "";
                                isRock.LineBot.Utility.ReplyImageMessage(item.replyToken, imageURL, imageURL, channelAccessToken);
                                break;

                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            else if (item.message.text.ToLower().Trim() == "qrcode")
            {
                imageURL = "https://i220.photobucket.com/albums/dd130/jung_04/OrderBot_QR.png";
                isRock.LineBot.Utility.PushImageMessage(item.source.userId, imageURL, imageURL, channelAccessToken);
            }
            else if (item.message.text == "Google表單有哪些小缺點")
            {
                isRock.LineBot.Utility.PushMessage(item.source.userId, "Google表單不能設定開單收單時間", channelAccessToken);
                System.Threading.Thread.Sleep(millisecondsTimeout);
                isRock.LineBot.Utility.PushMessage(item.source.userId, "Google表單不會在收單時傳訊息通知", channelAccessToken);
                System.Threading.Thread.Sleep(millisecondsTimeout);
                isRock.LineBot.Utility.PushMessage(item.source.userId, "Google表單不方便更改選項", channelAccessToken);
                System.Threading.Thread.Sleep(millisecondsTimeout / 2);
                isRock.LineBot.Utility.PushMessage(item.source.userId, "Google表單不能幫你解決不知道要吃甚麼的窘境", channelAccessToken);
                System.Threading.Thread.Sleep(millisecondsTimeout / 2);
                isRock.LineBot.Utility.PushMessage(item.source.userId, "Google表單沒有可愛的機器人", channelAccessToken);
                System.Threading.Thread.Sleep(millisecondsTimeout / 4);
                isRock.LineBot.Utility.PushMessage(item.source.userId, "Google表單不能跟你聊天", channelAccessToken);
                System.Threading.Thread.Sleep(millisecondsTimeout / 4);
                isRock.LineBot.Utility.PushMessage(item.source.userId, "Google表單沒有溫度", channelAccessToken);
                System.Threading.Thread.Sleep(millisecondsTimeout / 4);
                isRock.LineBot.Utility.PushMessage(item.source.userId, "Google表單沒有一片真誠的心", channelAccessToken);
                System.Threading.Thread.Sleep(millisecondsTimeout * 10);

                imageURL = "https://i220.photobucket.com/albums/dd130/jung_04/OrderBot_QR.png";
                isRock.LineBot.Utility.PushImageMessage(item.source.userId, imageURL, imageURL, channelAccessToken);
                System.Threading.Thread.Sleep(millisecondsTimeout / 2);
                isRock.LineBot.Utility.PushMessage(item.source.userId, "我建議你先讓大家加我好友，其他的都好說", channelAccessToken);
            }
            else if (item.message.text == "來看看統計數據")
            {
                isRock.LineBot.Utility.PushMessage(item.source.userId, "https://uk.businessinsider.com/the-messaging-app-report-2015-11", channelAccessToken);
                System.Threading.Thread.Sleep(millisecondsTimeout / 2);
                imageURL = "https://i220.photobucket.com/albums/dd130/jung_04/bii%20chat%20apps%20vs%20social%20networks.png";
                isRock.LineBot.Utility.PushImageMessage(item.source.userId, imageURL, imageURL, channelAccessToken);
                isRock.LineBot.Utility.PushMessage(item.source.userId, "英國研究不一定都是騙人的", channelAccessToken);
                System.Threading.Thread.Sleep(millisecondsTimeout);
                isRock.LineBot.Utility.PushMessage(item.source.userId, "應該吧XD", channelAccessToken);
            }
            else if (item.message.text == "你說是不是")
            {
                isRock.LineBot.Utility.PushMessage(item.source.userId, "是，您說的真對!", channelAccessToken);
                System.Threading.Thread.Sleep(millisecondsTimeout / 2);
                isRock.LineBot.Utility.PushMessage(item.source.userId, "那就…歡迎Spoka幫我們介紹一下聊天機器人(鼓掌)", channelAccessToken);
            }
            else if (item.message.text == "來一張全球分佈圖")
            {
                isRock.LineBot.Utility.PushMessage(item.source.userId, "https://www.similarweb.com/blog/messaging-apps", channelAccessToken);
                System.Threading.Thread.Sleep(millisecondsTimeout / 2);
                imageURL = "https://i220.photobucket.com/albums/dd130/jung_04/Line%20worldwide.png";
                isRock.LineBot.Utility.PushImageMessage(item.source.userId, imageURL, imageURL, channelAccessToken);
                isRock.LineBot.Utility.PushMessage(item.source.userId, "這次不是英國研究了", channelAccessToken);
                isRock.LineBot.Utility.PushStickerMessage(item.source.userId, 1, 106, channelAccessToken);
            }
            else if (item.message.text == "以上幾點")
            {
                isRock.LineBot.Utility.PushMessage(item.source.userId, "我能看懂你在說甚麼也是蠻厲害的...", channelAccessToken);
                System.Threading.Thread.Sleep(millisecondsTimeout / 2);
                isRock.LineBot.Utility.PushMessage(item.source.userId, "1. 用Google表單訂東西的小缺點\n2. 通訊軟體的使用率超過社群軟體\n3. 聊天機器人是一個新時代的突破口\n4. Line在台灣的超高使用率", channelAccessToken);
            }
            else if (item.message.text == "四大功能")
            {
                ImagemapMessage imagemapMessage = ImagemapMaker.MakeDemo(item);
                bot.PushMessage(item.source.userId, imagemapMessage);
                isRock.LineBot.Utility.PushMessage(item.source.userId, "按上面的區塊就可以查看功能嘍~", channelAccessToken);
            }
            else if (item.message.text == "Demo選單")
            {
                ImagemapMessage imagemapMessage = ImagemapMaker.MakeDemo(item);
                bot.PushMessage(item.source.userId, imagemapMessage);
            }
            else if (item.message.text == "訂單模式介紹")
            {
                ImagemapMessage imagemapMessage = ImagemapMaker.MakeDemoOrder(item);
                bot.PushMessage(item.source.userId, imagemapMessage);
            }
            else if (item.message.text == "個人模式介紹")
            {
                ImagemapMessage imagemapMessage = ImagemapMaker.MakeDemoUser(item);
                bot.PushMessage(item.source.userId, imagemapMessage);
            }
            else if (item.message.text == "社團模式介紹")
            {
                ImagemapMessage imagemapMessage = ImagemapMaker.MakeDemoClub(item);
                bot.PushMessage(item.source.userId, imagemapMessage);
            }
            else if (item.message.text == "商店模式介紹")
            {
                ImagemapMessage imagemapMessage = ImagemapMaker.MakeDemoShop(item);
                bot.PushMessage(item.source.userId, imagemapMessage);
            }
            else if (item.message.text == "給我幾個情境")
            {
                isRock.LineBot.Utility.PushMessage(item.source.userId, "公司訂便當", channelAccessToken);
                isRock.LineBot.Utility.PushMessage(item.source.userId, "揪團訂飲料", channelAccessToken);
                isRock.LineBot.Utility.PushMessage(item.source.userId, "小農賣水果", channelAccessToken);
                isRock.LineBot.Utility.PushMessage(item.source.userId, "網紅直播業配", channelAccessToken);
            }
            else if (item.message.text == "給我背景")
            {
                imageURL = "https://i220.photobucket.com/albums/dd130/jung_04/wall_paper.png";
                isRock.LineBot.Utility.PushImageMessage(item.source.userId, imageURL, imageURL, channelAccessToken);
                isRock.LineBot.Utility.PushMessage(item.source.userId, "來了!!", channelAccessToken);
            }
            else if (item.message.text == "小農賣水果")
            {

                imageURL = "https://scontent.ftpe1-2.fna.fbcdn.net/v/t1.0-9/38439991_101185220822825_2671768991562727424_n.jpg?_nc_cat=0&oh=d8e44c0d64d2e06085f580e137e3751a&oe=5BC684CA";
                isRock.LineBot.Utility.PushImageMessage(item.source.userId, imageURL, imageURL, channelAccessToken);
                isRock.LineBot.Utility.PushMessage(item.source.userId, "網室無毒栽培~美濃瓜~\n完全不噴農藥~\n完全不噴農藥~\n完全不噴農藥~\n\n一箱10斤 $850元\n數量不多~要買要快唷～\n蘇先生\n0910-488-263\n05-7834163", channelAccessToken);
                isRock.LineBot.Utility.PushMessage(item.source.userId, "FB連結：https://www.facebook.com/profile.php?id=100027940411905", channelAccessToken);
            }
            else if (item.message.text == "解決了什麼問題")
            {
                isRock.LineBot.Utility.PushMessage(item.source.userId, "其實不瞞你說...我擅長解決提出問題的人", channelAccessToken);
                System.Threading.Thread.Sleep(millisecondsTimeout);
                isRock.LineBot.Utility.PushMessage(item.source.userId, "阿不是啦XDD", channelAccessToken);
                System.Threading.Thread.Sleep(millisecondsTimeout);
                isRock.LineBot.Utility.PushMessage(item.source.userId, @"1. 可以設定時間自動通知\n\n2. 週期性訂單可以讓店家隨機，不用思考今天要訂哪一家\n\n3. 通訊軟體內建的購物功能不能訂便當和訂飲料\n\n4. 全部功能皆在通訊軟體上操作", channelAccessToken);
                System.Threading.Thread.Sleep(millisecondsTimeout);
                isRock.LineBot.Utility.PushMessage(item.source.userId, "這樣如何~", channelAccessToken);
            }
            else if (item.message.text == "未來可以發展的方向")
            {
                isRock.LineBot.Utility.PushMessage(item.source.userId, @"1. 串聯更多Line提供的功能，例如：Line pay、GPS輸入地址\n2. 每日抽一次優惠券、團購享折扣\n3. 合作店家廣告投放\n4. 官方網站\n5. 跨平台服務", channelAccessToken);
            }
            else if (item.message.text == "機器人說拜拜")
            {
                isRock.LineBot.Utility.PushMessage(item.source.userId, "bye~bye~", channelAccessToken);
                isRock.LineBot.Utility.PushMessage(item.source.userId, "謝謝大家~~~", channelAccessToken);
                isRock.LineBot.Utility.PushMessage(item.source.userId, "有空多回來玩~~~", channelAccessToken);
            }

        }

        private void MakeMessageByUserStatus(Event item, ReceievedMessage receivedMessage, string channelAccessToken, Bot bot, UserStatus userStatus)
        {
            string replaymessage;
            ShopTemp shopTemp = new ShopTemp(item.source.userId);

            switch (userStatus.QID)
            {
                //case 121:
                //    switch (userStatus.OID)
                //    {
                //        case 3:
                //            BuyerTemp buyerTemp = new BuyerTemp(item.source.userId);
                //            int amount = 0;
                //            bool isNumber = int.TryParse(item.message.text, out amount);
                //            if (isNumber)
                //            {
                //                buyerTemp.Amount = amount;
                //                buyerTemp.UpdateAmountByBuyerID();

                //                replaymessage = MessageMaker.make(item.source.userId, userStatus.QID, userStatus.OID, item.message.text);
                //                isRock.LineBot.Utility.ReplyMessage(item.replyToken, replaymessage, channelAccessToken);
                //            }
                //            else
                //            {
                //                replaymessage = "請輸入數字喔~~";
                //                isRock.LineBot.Utility.ReplyMessage(item.replyToken, replaymessage, channelAccessToken);
                //            }
                //            break;

                //        default:
                //            break;
                //    }
                //    break;

                case 171:
                    switch (userStatus.OID)
                    {
                        case 2:
                            if (item.message.text.Length > 20)
                            {
                                replaymessage = "商店名稱不能超過20個字喔~";
                                bot.PushMessage(item.source.userId, replaymessage);
                            }
                            else
                            {
                                shopTemp.UpdateShopName(item.message.text);
                                replaymessage = MessageMaker.make(item.source.userId, userStatus.QID, userStatus.OID, item.message.text);
                                isRock.LineBot.Utility.ReplyMessage(item.replyToken, replaymessage, channelAccessToken);
                            }
                            break;

                        case 3:
                            shopTemp.UpdateShopPhone(item.message.text);
                            replaymessage = MessageMaker.make(item.source.userId, userStatus.QID, userStatus.OID, item.message.text);
                            isRock.LineBot.Utility.ReplyMessage(item.replyToken, replaymessage, channelAccessToken);
                            break;

                        case 4:
                            shopTemp.UpdateShopAddress(item.message.text);
                            replaymessage = MessageMaker.make(item.source.userId, userStatus.QID, userStatus.OID, item.message.text);
                            isRock.LineBot.Utility.ReplyMessage(item.replyToken, replaymessage, channelAccessToken);
                            break;

                        case 5:
                            shopTemp.InsertShopItem(item.message.text);
                            replaymessage = MessageMaker.make(item.source.userId, userStatus.QID, userStatus.OID, item.message.text);
                            isRock.LineBot.Utility.ReplyMessage(item.replyToken, replaymessage, channelAccessToken);
                            break;

                        default:
                            break;
                    }
                    break;

                case 194:
                    switch (userStatus.OID)
                    {
                        case 3:
                            shopTemp.InsertShopItem(item.message.text);
                            replaymessage = MessageMaker.make(item.source.userId, userStatus.QID, userStatus.OID, item.message.text);
                            isRock.LineBot.Utility.ReplyMessage(item.replyToken, replaymessage, channelAccessToken);
                            break;
                        default:
                            break;
                    }
                    break;

                case 271:
                    switch (userStatus.OID)
                    {
                        case 2:
                            if (item.message.text.Length > 20)
                            {
                                replaymessage = "商店名稱不能超過20個字喔~";
                                bot.PushMessage(item.source.userId, replaymessage);
                            }
                            else
                            {
                                shopTemp.UpdateShopName(item.message.text);
                                replaymessage = MessageMaker.make(item.source.userId, userStatus.QID, userStatus.OID, item.message.text);
                                isRock.LineBot.Utility.ReplyMessage(item.replyToken, replaymessage, channelAccessToken);
                            }
                            break;

                        case 3:
                            shopTemp.UpdateShopPhone(item.message.text);
                            replaymessage = MessageMaker.make(item.source.userId, userStatus.QID, userStatus.OID, item.message.text);
                            isRock.LineBot.Utility.ReplyMessage(item.replyToken, replaymessage, channelAccessToken);
                            break;

                        case 4:
                            shopTemp.UpdateShopAddress(item.message.text);
                            replaymessage = MessageMaker.make(item.source.userId, userStatus.QID, userStatus.OID, item.message.text);
                            isRock.LineBot.Utility.ReplyMessage(item.replyToken, replaymessage, channelAccessToken);
                            break;

                        case 5:
                            shopTemp.InsertShopItem(item.message.text);
                            replaymessage = MessageMaker.make(item.source.userId, userStatus.QID, userStatus.OID, item.message.text);
                            isRock.LineBot.Utility.ReplyMessage(item.replyToken, replaymessage, channelAccessToken);
                            break;

                        default:
                            break;
                    }
                    break;

                case 294:
                    switch (userStatus.OID)
                    {
                        case 3:
                            shopTemp.InsertShopItem(item.message.text);
                            replaymessage = MessageMaker.make(item.source.userId, userStatus.QID, userStatus.OID, item.message.text);
                            isRock.LineBot.Utility.ReplyMessage(item.replyToken, replaymessage, channelAccessToken);
                            break;
                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }
        }

        private void MakeConfirmByUserStatus(Event item, ReceievedMessage receivedMessage, string channelAccessToken, Bot bot, UserStatus userStatus)
        {
            string message = "";
            ConfirmTemplate confirmTemplate;
            BuyerTemp buyerTemp;
            switch (userStatus.QID)
            {
                case 71: // 確認加入訂單，還沒改好
                    switch (userStatus.OID)
                    {
                        case 2:
                            // 先檢查他輸入的OrderID有沒有在OrderUserTable
                            OrderInfo orderInfo = new OrderInfo(item.source.userId);
                            int result = orderInfo.CheckOrderUserTable(item.message.text);

                            if (result > 0)
                            {
                                // 找到OrderName
                                orderInfo.OrderID = item.message.text;
                                orderInfo.SelectMyOrderNameByOrderID();

                                // 把OrderID先暫存在UserStatus裡的TempData
                                userStatus.TempData = item.message.text;
                                userStatus.UpdateTempDataByUserID();

                                message = $"你確定要參加 {orderInfo.OrderName} 嗎?";
                                confirmTemplate = ConfirmMaker.MakeMessageConfirmBtn(message, userStatus.QID, userStatus.OID, item.source.userId);
                                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, channelAccessToken);
                            }
                            else
                            {
                                message = "輸入參加碼錯誤喔!!";
                                bot.PushMessage(item.source.userId, message);
                            }
                            break;

                        default:
                            break;
                    }
                    break;

                case 81: // 確認訂單
                    switch (userStatus.OID)
                    {
                        case 3:
                        //string clubID = item.message.text;
                        //ClubInfo clubInfo = new ClubInfo(item.source.userId, clubID, "");
                        //List<ClubInfo> clubInfos = clubInfo.SelectByClubid();
                        //message = $"你確定要參加{clubInfos[0].ClubName} 嗎?";
                        //confirmTemplate = ConfirmMaker.MakeCreateClubConfirmBtn(message, userStatus.QID, userStatus.OID, clubID, clubInfos[0].ClubName);

                        ////這邊改用Push
                        //isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, channelAccessToken);
                        //break;

                        default:
                            break;
                    }
                    break;

                case 91:
                    switch (userStatus.OID)
                    {
                        case 2:
                            string clubID = item.message.text;
                            ClubInfo clubInfo = new ClubInfo(item.source.userId, clubID, "");
                            List<ClubInfo> clubInfos = clubInfo.SelectByClubid();

                            if (clubInfos.Count == 0)
                            {
                                string errormessage = "參加失敗，請確認參加碼輸入無誤喔~";
                                isRock.LineBot.Utility.PushMessage(item.source.userId, errormessage, channelAccessToken);

                            }
                            else
                            {

                                message = $"你確定要參加{clubInfos[0].ClubName} 嗎?";
                                confirmTemplate = ConfirmMaker.MakeCreateClubConfirmBtn(message, userStatus.QID, userStatus.OID, clubID, clubInfos[0].ClubName);

                                //這邊改用Push
                                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, channelAccessToken);
                            }
                            break;

                        default:
                            break;
                    }
                    break;

                case 92:
                    switch (userStatus.OID)
                    {
                        case 2:
                            if (item.message.text.Length > 20)
                            {
                                message = $"社團名稱不能超過20個字喔!!!";
                                isRock.LineBot.Utility.PushMessage(item.source.userId, message, channelAccessToken);
                            }
                            else
                            {
                                message = $"你的社團名字確定要訂為 {item.message.text} 嗎?";
                                confirmTemplate = ConfirmMaker.MakeCreateClubConfirmBtn(message, userStatus.QID, userStatus.OID, "", item.message.text);

                                //這邊改用Push
                                bot.PushMessage(item.source.userId, confirmTemplate);
                                // isRock.LineBot.Utility.ReplyTemplateMessage(receivedMessage.events[0].replyToken, confirmTemplate, channelAccessToken);
                            }
                            break;
                    }
                    break;

                case 121:
                    switch (userStatus.OID)
                    {
                        case 3:
                            buyerTemp = new BuyerTemp(item.source.userId);
                            int amount = 0;
                            bool isNumber = int.TryParse(item.message.text, out amount);
                            if (isNumber)
                            {
                                buyerTemp.Amount = amount;
                                buyerTemp.UpdateAmountByBuyerID();
                           //     buyerTemp.In();//

                                confirmTemplate = ConfirmMaker.MakeNoteConfirmBtn(userStatus.QID, userStatus.OID);
                                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, channelAccessToken);
                            }
                            else
                            {
                                message = "請輸入數字喔~~";
                                isRock.LineBot.Utility.ReplyMessage(item.replyToken, message, channelAccessToken);
                            }
                            break;

                        case 4:
                            // 把item.message.text存進BuyerInfoTemp
                            // 先取得OrderPartitionID
                            OrderTemp orderTemp = new OrderTemp(item.source.userId);
                            orderTemp.SelectByUserID();
                            buyerTemp = new BuyerTemp(item.source.userId);
                            buyerTemp.OrderPartitionID = orderTemp.OrderPartitionID;
                            buyerTemp.Note = item.message.text;
                            buyerTemp.UpdateNoteByBuyerID();

                            // 從BuyerInfoTemp拿出所有資料
                            buyerTemp.SelectByBuyerID();

                            message = $"請確認訂購{buyerTemp.Item}，" +
                                $"單價：{buyerTemp.Price}，" +
                                $"數量：{buyerTemp.Amount}，" +
                                $"總價：{buyerTemp.Amount * buyerTemp.Price}，" +
                                $"註記：{buyerTemp.Note}";
                            confirmTemplate = ConfirmMaker.MakeMessageConfirmBtn(message, userStatus.QID, userStatus.OID, item.source.userId);

                            bot.PushMessage(item.source.userId, confirmTemplate);
                            break;

                        default:
                            break;
                    }
                    break;

                case 171:
                    switch (userStatus.OID)
                    {
                        case 6:
                            ShopTemp shopTemp = new ShopTemp(item.source.userId);

                            double number = 0;
                            bool isNumber = double.TryParse(item.message.text, out number);
                            if (isNumber)
                            {
                                shopTemp.InsertShopItemPrice(item.message.text);
                                message = $"你要繼續輸入品項嗎?";
                                confirmTemplate = ConfirmMaker.MakeContinueConfirmBtn(message, userStatus.QID, userStatus.OID);

                                bot.PushMessage(item.source.userId, confirmTemplate);
                            }
                            else
                            {
                                // 防止輸入不是數字
                                message = $"請輸入數字喔!!!!!";
                                bot.PushMessage(item.source.userId, message);
                            }
                            break;

                        default:
                            break;
                    }
                    break;

                case 191:
                    switch (userStatus.OID)
                    {
                        case 2:
                            if (item.message.text.Length > 20)
                            {
                                message = "商店名稱不能超過20個字喔~";
                                bot.PushMessage(item.source.userId, message);
                            }
                            else
                            {
                                ShopTemp shopTemp = new ShopTemp(item.source.userId);
                                shopTemp.UpdateShopName(item.message.text);

                                message = $"你確定要將商店名稱改為{item.message.text}嗎?";
                                confirmTemplate = ConfirmMaker.MakeMessageConfirmBtn(message, userStatus.QID, userStatus.OID, item.source.userId);
                                bot.PushMessage(item.source.userId, confirmTemplate);
                            }
                            break;

                        default:
                            break;
                    }
                    break;

                case 192:
                    switch (userStatus.OID)
                    {
                        case 2:
                            ShopTemp shopTemp = new ShopTemp(item.source.userId);
                            shopTemp.UpdateShopPhone(item.message.text);

                            message = $"你確定要將商店電話改為{item.message.text}嗎?";
                            confirmTemplate = ConfirmMaker.MakeMessageConfirmBtn(message, userStatus.QID, userStatus.OID, item.source.userId);

                            bot.PushMessage(item.source.userId, confirmTemplate);
                            break;

                        default:
                            break;
                    }
                    break;

                case 193:
                    switch (userStatus.OID)
                    {
                        case 2:
                            ShopTemp shopTemp = new ShopTemp(item.source.userId);
                            shopTemp.UpdateShopAddress(item.message.text);

                            message = $"你確定要將商店地址改為{item.message.text}嗎?";
                            confirmTemplate = ConfirmMaker.MakeMessageConfirmBtn(message, userStatus.QID, userStatus.OID, item.source.userId);

                            bot.PushMessage(item.source.userId, confirmTemplate);
                            break;

                        default:
                            break;
                    }
                    break;

                case 194:
                    switch (userStatus.OID)
                    {
                        case 4:
                            ShopTemp shopTemp = new ShopTemp(item.source.userId);
                            // shopTemp.UpdateShopItemPrice(userStatus.TempData, item.message.text);
                            shopTemp.InsertShopItemPrice(item.message.text);

                            message = $"你確定要將品項改為{shopTemp.ShopItem}　{item.message.text}TWD嗎?";
                            confirmTemplate = ConfirmMaker.MakeMessageConfirmBtn(message, userStatus.QID, userStatus.OID, item.source.userId);

                            bot.PushMessage(item.source.userId, confirmTemplate);
                            break;

                        default:
                            break;
                    }
                    break;

                case 201:
                    if (item.message.text.Length > 20)
                    {
                        message = $"訂單名稱不能超過20個字喔!!!";
                        isRock.LineBot.Utility.PushMessage(item.source.userId, message, channelAccessToken);
                    }
                    else
                    {
                        message = $"訂單名字確定要訂為{item.message.text}嗎?";
                        confirmTemplate = ConfirmMaker.MakeCreateClubConfirmBtn(message, userStatus.QID, userStatus.OID, "", item.message.text);

                        //這邊改用Push
                        bot.PushMessage(item.source.userId, confirmTemplate);
                        // isRock.LineBot.Utility.ReplyTemplateMessage(receivedMessage.events[0].replyToken, confirmTemplate, channelAccessToken);
                    }
                    break;

                case 211:
                    if (item.message.text.Length > 20)
                    {
                        message = $"訂單名稱不能超過20個字喔!!!";
                        isRock.LineBot.Utility.PushMessage(item.source.userId, message, channelAccessToken);
                    }
                    else
                    {
                        message = $"訂單名字確定要訂為{item.message.text}嗎?";
                        confirmTemplate = ConfirmMaker.MakeCreateClubConfirmBtn(message, userStatus.QID, userStatus.OID, "", item.message.text);

                        //這邊改用Push
                        bot.PushMessage(item.source.userId, confirmTemplate);
                        // isRock.LineBot.Utility.ReplyTemplateMessage(receivedMessage.events[0].replyToken, confirmTemplate, channelAccessToken);
                    }
                    break;


                case 241:
                    if (item.message.text.Length > 20)
                    {
                        message = $"訂單名稱不能超過20個字喔!!!";
                        isRock.LineBot.Utility.PushMessage(item.source.userId, message, channelAccessToken);
                    }
                    else
                    {
                        message = $"訂單名字確定要訂為{item.message.text}嗎?";
                        confirmTemplate = ConfirmMaker.MakeCreateClubConfirmBtn(message, userStatus.QID, userStatus.OID, "", item.message.text);

                        //這邊改用Push
                        bot.PushMessage(item.source.userId, confirmTemplate);
                        // isRock.LineBot.Utility.ReplyTemplateMessage(receivedMessage.events[0].replyToken, confirmTemplate, channelAccessToken);
                    }
                    break;



                case 251:
                    if (item.message.text.Length > 20)
                    {
                        message = $"訂單名稱不能超過20個字喔!!!";
                        isRock.LineBot.Utility.PushMessage(item.source.userId, message, channelAccessToken);
                    }
                    else
                    {
                        message = $"訂單名字確定要訂為{item.message.text}嗎?";
                        confirmTemplate = ConfirmMaker.MakeCreateClubConfirmBtn(message, userStatus.QID, userStatus.OID, "", item.message.text);

                        //這邊改用Push
                        bot.PushMessage(item.source.userId, confirmTemplate);
                        // isRock.LineBot.Utility.ReplyTemplateMessage(receivedMessage.events[0].replyToken, confirmTemplate, channelAccessToken);
                    }
                    break;

                case 271:
                    switch (userStatus.OID)
                    {
                        case 6:
                            ShopTemp shopTemp = new ShopTemp(item.source.userId);

                            double number = 0;
                            bool isNumber = double.TryParse(item.message.text, out number);
                            if (isNumber)
                            {
                                shopTemp.InsertShopItemPrice(item.message.text);
                                message = $"你要繼續輸入品項嗎?";
                                confirmTemplate = ConfirmMaker.MakeContinueConfirmBtn(message, userStatus.QID, userStatus.OID);

                                bot.PushMessage(item.source.userId, confirmTemplate);
                            }
                            else
                            {
                                // 防止輸入不是數字
                                message = $"請輸入數字喔!!!!!";
                                bot.PushMessage(item.source.userId, message);
                            }
                            break;

                        default:
                            break;
                    }
                    break;

                case 291:
                    switch (userStatus.OID)
                    {
                        case 2:
                            if (item.message.text.Length > 20)
                            {
                                message = "商店名稱不能超過20個字喔~";
                                bot.PushMessage(item.source.userId, message);
                            }
                            else
                            {
                                ShopTemp shopTemp = new ShopTemp(item.source.userId);
                                shopTemp.UpdateShopName(item.message.text);

                                message = $"你確定要將商店名稱改為{item.message.text}嗎?";
                                confirmTemplate = ConfirmMaker.MakeMessageConfirmBtn(message, userStatus.QID, userStatus.OID, item.source.userId);
                                bot.PushMessage(item.source.userId, confirmTemplate);
                            }
                            break;

                        default:
                            break;
                    }
                    break;

                case 292:
                    switch (userStatus.OID)
                    {
                        case 2:
                            ShopTemp shopTemp = new ShopTemp(item.source.userId);
                            shopTemp.UpdateShopPhone(item.message.text);

                            message = $"你確定要將商店電話改為{item.message.text}嗎?";
                            confirmTemplate = ConfirmMaker.MakeMessageConfirmBtn(message, userStatus.QID, userStatus.OID, item.source.userId);

                            bot.PushMessage(item.source.userId, confirmTemplate);
                            break;

                        default:
                            break;
                    }
                    break;

                case 293:
                    switch (userStatus.OID)
                    {
                        case 2:
                            ShopTemp shopTemp = new ShopTemp(item.source.userId);
                            shopTemp.UpdateShopAddress(item.message.text);

                            message = $"你確定要將商店地址改為{item.message.text}嗎?";
                            confirmTemplate = ConfirmMaker.MakeMessageConfirmBtn(message, userStatus.QID, userStatus.OID, item.source.userId);

                            bot.PushMessage(item.source.userId, confirmTemplate);
                            break;

                        default:
                            break;
                    }
                    break;

                case 294:
                    switch (userStatus.OID)
                    {
                        case 4:
                            ShopTemp shopTemp = new ShopTemp(item.source.userId);
                            // shopTemp.UpdateShopItemPrice(userStatus.TempData, item.message.text);
                            shopTemp.InsertShopItemPrice(item.message.text);

                            message = $"你確定要將品項改為{shopTemp.ShopItem}　{item.message.text}TWD嗎?";
                            confirmTemplate = ConfirmMaker.MakeMessageConfirmBtn(message, userStatus.QID, userStatus.OID, item.source.userId);

                            bot.PushMessage(item.source.userId, confirmTemplate);
                            break;

                        default:
                            break;
                    }
                    break;

                case 9999:
                    switch (userStatus.OID)
                    {
                        case 3:
                            buyerTemp = new BuyerTemp(item.source.userId);
                            int amount = 0;
                            bool isNumber = int.TryParse(item.message.text, out amount);
                            if (isNumber)
                            {
                                buyerTemp.Amount = amount;
                                buyerTemp.UpdateAmountByBuyerID();

                                confirmTemplate = ConfirmMaker.MakeNoteConfirmBtn(userStatus.QID, userStatus.OID);
                                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, channelAccessToken);
                            }
                            else
                            {
                                message = "請輸入數字喔~~";
                                isRock.LineBot.Utility.ReplyMessage(item.replyToken, message, channelAccessToken);
                            }
                            break;

                        case 4:
                            // 把item.message.text存進BuyerInfoTemp
                            // 先取得OrderPartitionID
                            OrderTemp orderTemp = new OrderTemp(item.source.userId);
                            orderTemp.SelectByUserID();
                            buyerTemp = new BuyerTemp(item.source.userId);
                            buyerTemp.OrderPartitionID = orderTemp.OrderPartitionID;
                            buyerTemp.Note = item.message.text;
                            buyerTemp.UpdateNoteByBuyerID();

                            // 從BuyerInfoTemp拿出所有資料
                            buyerTemp.SelectByBuyerID();

                            message = $"請確認訂購{buyerTemp.Item}，" +
                                $"單價：{buyerTemp.Price}，" +
                                $"數量：{buyerTemp.Amount}，" +
                                $"總價：{buyerTemp.Amount * buyerTemp.Price}，" +
                                $"註記：{buyerTemp.Note}";
                            confirmTemplate = ConfirmMaker.MakeMessageConfirmBtn(message, userStatus.QID, userStatus.OID, item.source.userId);

                            bot.PushMessage(item.source.userId, confirmTemplate);
                            break;

                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }
        }

        private NEXTMESSAGETYPE RecognizeMessageType(UserStatus userStatus)
        {
            switch (userStatus.QID)
            {
                case 71:
                    switch (userStatus.OID)
                    {
                        case 2:
                            return NEXTMESSAGETYPE.CONFIRM;

                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case 91:
                    switch (userStatus.OID)
                    {
                        case 2:
                            return NEXTMESSAGETYPE.CONFIRM;

                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case 92:
                    switch (userStatus.OID)
                    {
                        case 2:
                            return NEXTMESSAGETYPE.CONFIRM;

                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case 121:
                    switch (userStatus.OID)
                    {
                        case 3:
                            return NEXTMESSAGETYPE.CONFIRM;
                        case 4:
                            return NEXTMESSAGETYPE.CONFIRM;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case 171:
                    switch (userStatus.OID)
                    {
                        case 2:
                            return NEXTMESSAGETYPE.MESSAGE;
                        case 3:
                            return NEXTMESSAGETYPE.MESSAGE;
                        case 4:
                            return NEXTMESSAGETYPE.MESSAGE;
                        case 5:
                            return NEXTMESSAGETYPE.MESSAGE;
                        case 6:
                            return NEXTMESSAGETYPE.CONFIRM;

                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case 191:
                    switch (userStatus.OID)
                    {
                        case 2:
                            return NEXTMESSAGETYPE.CONFIRM;

                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case 201:
                    switch (userStatus.OID)
                    {
                        case 4:
                            return NEXTMESSAGETYPE.CONFIRM;

                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case 192:
                    switch (userStatus.OID)
                    {
                        case 2:
                            return NEXTMESSAGETYPE.CONFIRM;

                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;


                case 211:
                    switch (userStatus.OID)
                    {
                        case 5:
                            return NEXTMESSAGETYPE.CONFIRM;

                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case 193:
                    switch (userStatus.OID)
                    {
                        case 2:
                            return NEXTMESSAGETYPE.CONFIRM;

                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case 194:
                    switch (userStatus.OID)
                    {
                        case 3:
                            return NEXTMESSAGETYPE.MESSAGE;
                        case 4:
                            return NEXTMESSAGETYPE.CONFIRM;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case 241:
                    switch (userStatus.OID)
                    {
                        case 4:
                            return NEXTMESSAGETYPE.CONFIRM;

                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case 251:
                    switch (userStatus.OID)
                    {
                        case 5:
                            return NEXTMESSAGETYPE.CONFIRM;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case 271:
                    switch (userStatus.OID)
                    {
                        case 2:
                            return NEXTMESSAGETYPE.MESSAGE;
                        case 3:
                            return NEXTMESSAGETYPE.MESSAGE;
                        case 4:
                            return NEXTMESSAGETYPE.MESSAGE;
                        case 5:
                            return NEXTMESSAGETYPE.MESSAGE;
                        case 6:
                            return NEXTMESSAGETYPE.CONFIRM;

                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case 291:
                    switch (userStatus.OID)
                    {
                        case 2:
                            return NEXTMESSAGETYPE.CONFIRM;

                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case 292:
                    switch (userStatus.OID)
                    {
                        case 2:
                            return NEXTMESSAGETYPE.CONFIRM;

                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case 293:
                    switch (userStatus.OID)
                    {
                        case 2:
                            return NEXTMESSAGETYPE.CONFIRM;

                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case 294:
                    switch (userStatus.OID)
                    {
                        case 3:
                            return NEXTMESSAGETYPE.MESSAGE;
                        case 4:
                            return NEXTMESSAGETYPE.CONFIRM;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case 9999:
                    switch (userStatus.OID)
                    {
                        case 3:
                            return NEXTMESSAGETYPE.CONFIRM;
                        case 4:
                            return NEXTMESSAGETYPE.CONFIRM;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                default:
                    break;
            }
            return NEXTMESSAGETYPE.ERROR;
        }
    }
}