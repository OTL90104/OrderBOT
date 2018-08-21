using isRock.LineBot;
using OrderBot.SQLObject;
using OrderBot.TemplateMakers;
using OrderBot.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderBot.EventManager
{
    enum NEXTMESSAGETYPE
    {
        MENU,
        BUTTON,
        CAROUSEL,
        CONFIRM,
        MESSAGE,
        FLEX,
        DATETIMEBUTTON,
        ORDERSQLCOMMUNICATE,
        USERINFOSQLCOMMUNICATE,
        CLUBINFOSQLCOMMUNICATE,
        SHOPINFOSQLCOMMUNICATE,
        ERROR
    }
    public class PostbackManager
    {
        public void Process(Event item, ReceievedMessage receivedMessage, string ChannelAccessToken)
        {
            string replaymessage;
            string Message = "";

            string[] SplitedData = item.postback.data.Split(',');
            bool isTimeAtRange = DateTimeChecker.TimeCheck(SplitedData[0]);

            // 先判斷button的有效期限
            if (isTimeAtRange)
            {
                //利用Recognize()辨別該回復哪種訊息
                NEXTMESSAGETYPE NextMessageType = RecognizeMessageType(item, SplitedData);
                OrderTemp orderTemp;
                OrderInfo orderInfo;
                ShopTemp shopTemp;
                UserStatus userStatus;
                BuyerTemp buyerTemp;
                PeriodOrderTmp periodOrderTmp;
                ConfirmTemplate confirmTemplate;
                ShopInfo shopInfo;
                BuyerInfo buyerInfo;
                ShopListTemp shopListTemp;
                ImagemapMessage MenuBtnTemplate;
                switch (NextMessageType)
                {
                    case NEXTMESSAGETYPE.MENU:
                        MenuBtnTemplate = ImagemapMaker.MakeMenu(item);
                        isRock.LineBot.Utility.PushImageMapMessage(item.source.userId, MenuBtnTemplate, ChannelAccessToken);
                        break;

                    case NEXTMESSAGETYPE.BUTTON:

                        // 做button前先偷做的事情
                        switch (SplitedData[1])
                        {
                            case "4":
                                orderTemp = new OrderTemp(item.source.userId);//初始化OrderTmp
                                orderTemp.UpdateInitialOrderTemp();
                                periodOrderTmp = new PeriodOrderTmp(item.source.userId);//初始化PeriodOrderTmp
                                periodOrderTmp.ClubUpdateInitialOrderTmp();
                                break;

                            case "6":
                                orderTemp = new OrderTemp(item.source.userId);
                                orderTemp.OrderPartitionID = SplitedData[3];
                                orderTemp.UpdateOrderPartitionIDByUserID();
                                break;

                            case "8":
                                orderTemp = new OrderTemp(item.source.userId);
                                orderTemp.OrderPartitionID = SplitedData[3];
                                orderTemp.UpdateOrderPartitionIDByUserID();
                                break;

                            case "12":
                                
                                // 先到orderTemp拿到OrderPartitionID
                                orderTemp = new OrderTemp(item.source.userId);
                                orderTemp.SelectByUserID();
                                // 把UserID和OrderPartitionID先放到buyerTemp去
                                buyerTemp = new BuyerTemp(item.source.userId);
                                buyerTemp.OrderPartitionID = orderTemp.OrderPartitionID;
                                buyerTemp.InsertBuyerItemInfoTemp();

                                // 把指定的修改品項ID(在SplitedData[4]裡面)存在UserStatus裡的TempData
                                userStatus = new UserStatus(item.source.userId);
                                userStatus.TempData = SplitedData[4];
                                userStatus.UpdateTempDataByUserID();
                                break;

                            case "13":
                                orderTemp = new OrderTemp(item.source.userId);
                                orderTemp.ClubID = SplitedData[3];
                                orderTemp.UpdateClubIDByUserID();
                                break;

                            case "14":
                                orderTemp = new OrderTemp(item.source.userId);//初始化OrderTmp
                                orderTemp.ClubUpdateInitialOrderTmp();
                                periodOrderTmp = new PeriodOrderTmp(item.source.userId);//初始化PeriodOrderTmp
                                periodOrderTmp.ClubUpdateInitialOrderTmp();
                                break;

                            case "16":
                                orderTemp = new OrderTemp(item.source.userId);
                                orderTemp.OrderPartitionID = SplitedData[3];
                                orderTemp.UpdateOrderPartitionIDByUserID();
                                break;

                            case "18":
                                shopTemp = new ShopTemp(item.source.userId);
                                shopTemp.ShopID = SplitedData[3];
                                shopTemp.UpdateShopIDByUserID();
                                break;

                            case "19":
                                shopTemp = new ShopTemp(item.source.userId);
                                shopTemp.DeleteShopItemTempByUserID();
                                userStatus = new UserStatus(item.source.userId);
                                userStatus.InitializeUserStatusByUserID();
                                break;

                            case "27":
                                shopTemp = new ShopTemp(item.source.userId);
                                shopTemp.ClubID = SplitedData[3];
                                shopTemp.UpdateClubIDByUserID();
                                break;

                            case "28":
                                shopTemp = new ShopTemp(item.source.userId);
                                shopTemp.ShopID = SplitedData[3];
                                shopTemp.UpdateShopIDByUserID();
                                break;

                            case "29":
                                shopTemp = new ShopTemp(item.source.userId);
                                shopTemp.DeleteShopItemTempByUserID();
                                userStatus = new UserStatus(item.source.userId);
                                userStatus.InitializeUserStatusByUserID();
                                break;

                            default:
                                break;
                        }

                        ButtonsTemplate buttonsTemplate = new ButtonsTemplate(); 
                        if (SplitedData[1] == "12")
                        {
                            buttonsTemplate = ButtonMaker.Make(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[4]);

                        }
                        else
                        {
                            buttonsTemplate = ButtonMaker.Make(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3]);
                        }


                        isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, buttonsTemplate, ChannelAccessToken);
                        break;

                    case NEXTMESSAGETYPE.CAROUSEL:
                        List<CarouselTemplate> carouselTemplatesList = new List<CarouselTemplate>();
                        Message = "目前查無資料喔~~";
                        switch (SplitedData[1])
                        {
                            case "5":
                                carouselTemplatesList = CarouselMaker.MakeSearchingMyOrder(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                break;

                            case "22":
                                carouselTemplatesList = CarouselMaker.MakeSearchingClub(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                break;

                            case "32":
                                carouselTemplatesList = CarouselMaker.MakeSearchingMyOrder(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                break;

                            case "51":
                                carouselTemplatesList = CarouselMaker.MakeSearchingPartitionOrder(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], item.source.userId);
                                break;

                            case "72":
                                switch (SplitedData[2])
                                {
                                    case "1":
                                        carouselTemplatesList = CarouselMaker.MakeSearchingUserOrder(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                        break;
                                    case "2":
                                        // splitedData[3]是OrderID，要先存下來，存在UserStatus下的TempData
                                        userStatus = new UserStatus(item.source.userId);
                                        userStatus.TempData = SplitedData[3];
                                        userStatus.UpdateTempDataByUserID();

                                        carouselTemplatesList = CarouselMaker.MakeSearchingUserPartitionOrder(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], item.source.userId);
                                        break;
                                    default:
                                        break;
                                }
                                break;

                            case "73":
                                switch (SplitedData[2])
                                {
                                    case "1":
                                        carouselTemplatesList = CarouselMaker.MakeSearchingUserOrder(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                        break;
                                    default:
                                        break;
                                }
                                break;

                            case "81":
                                switch (SplitedData[2])
                                {
                                    case "1":
                                        orderTemp = new OrderTemp(item.source.userId);
                                        orderTemp.SelectByUserID();
                                        string splittedOrderPartitionID = orderTemp.OrderPartitionID.Substring(0, 2);
                                        orderInfo = new OrderInfo();
                                        orderInfo.OrderPartitionID = orderTemp.OrderPartitionID;
                                        if (splittedOrderPartitionID == "MO")
                                        {
                                            orderInfo.SelectMyOrderTableByOrderPartitionID();
                                        }
                                        else if (splittedOrderPartitionID == "CO")
                                        {
                                            orderInfo.SelectClubOrderTableByOrderPartitionID();
                                        }

                                        if (orderInfo.OrderStatus == "available")
                                        {
                                            carouselTemplatesList = CarouselMaker.MakeSearchingBuyerItem(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                        }
                                        else
                                        {
                                            Message = "訂單不在可修改的時間範圍內喔~";
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                break;

                            case "93":
                                carouselTemplatesList = CarouselMaker.MakeSearchingClub(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                break;

                            case "112":
                                carouselTemplatesList = CarouselMaker.MakeSearchingClub(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                break;

                            case "121":
                                carouselTemplatesList = CarouselMaker.MakeSearchingItem(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId, SplitedData[3]);
                                break;

                            case "132":
                                carouselTemplatesList = CarouselMaker.MakeSearchingMyOrder(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                break;

                            case "151":
                                carouselTemplatesList = CarouselMaker.MakeSearchingPartitionOrder(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], item.source.userId);
                                break;

                            case "172":
                                carouselTemplatesList = CarouselMaker.MakeSearchingMyShop(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                break;

                            case "194":
                                carouselTemplatesList = CarouselMaker.MakeSearchingMyShopItem(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                break;

                            case "201":
                                switch (SplitedData[2])
                                {
                                    case "1":
                                        carouselTemplatesList = CarouselMaker.MakeSearchingMyShop(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                        break;
                                    default:
                                        break;
                                }
                                break;

                            case "202":
                                switch (SplitedData[2])
                                {
                                    case "1":
                                        carouselTemplatesList = CarouselMaker.MakeSearchingBossShop(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]));
                                        break;

                                    default:
                                        break;
                                }
                                break;

                            case "211":
                                switch (SplitedData[2])
                                {
                                    case "1":
                                        carouselTemplatesList = CarouselMaker.MakeSearchingMyShop(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                        break;
                                    default:
                                        break;
                                }
                                break;

                            case "212":
                                switch (SplitedData[2])
                                {
                                    case "1":
                                        carouselTemplatesList = CarouselMaker.MakeSearchingBossShop(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]));
                                        break;

                                    default:
                                        break;
                                }
                                break;


                            case "231":
                                switch (SplitedData[2])
                                {
                                    case "1":
                                        carouselTemplatesList = CarouselMaker.MakeSearchingMyShop(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "232":
                                switch (SplitedData[2])
                                {
                                    case "1":
                                        carouselTemplatesList = CarouselMaker.MakeSearchingBossShop(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]));
                                        break;
                                    default:
                                        break;
                                }
                                break;

                            case "241":
                                switch (SplitedData[2])
                                {
                                    case "1":
                                        carouselTemplatesList = CarouselMaker.MakeSearchingClubShopForClubOrder(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                        break;
                                    default:
                                        break;
                                }
                                break;

                            case "242":
                                switch (SplitedData[2])
                                {
                                    case "1":
                                        carouselTemplatesList = CarouselMaker.MakeSearchingBossShop(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]));
                                        break;

                                    default:
                                        break;
                                }
                                break;

                            case "251":
                                switch (SplitedData[2])
                                {
                                    case "1":
                                        shopTemp = new ShopTemp(item.source.userId);
                                        shopTemp.SelectByUserID();
                                        periodOrderTmp = new PeriodOrderTmp(item.source.userId);
                                        periodOrderTmp.SelectAllByUserID();

                                        carouselTemplatesList = CarouselMaker.MakeSearchingClubShop(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId, periodOrderTmp.ClubID);

                                        break;
                                    default:
                                        break;
                                }
                                break;

                            case "252":
                                switch (SplitedData[2])
                                {
                                    case "1":
                                        carouselTemplatesList = CarouselMaker.MakeSearchingBossShop(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]));
                                        break;

                                    default:
                                        break;
                                }
                                break;

                            case "261":
                                switch (SplitedData[2])
                                {
                                    case "1":
                                        orderTemp = new OrderTemp(item.source.userId);
                                        orderTemp.ClubIDSelectByUserID();
                                        carouselTemplatesList = CarouselMaker.MakeSearchingClubShop(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId, orderTemp.ClubID);
                                        break;
                                    default:
                                        break;
                                }
                                break;

                            case "262":
                                switch (SplitedData[2])
                                {
                                    case "1":

                                        carouselTemplatesList = CarouselMaker.MakeSearchingBossShop(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]));
                                        break;
                                    default:
                                        break;
                                }
                                break;

                            case "272":
                                shopTemp = new ShopTemp(item.source.userId);
                                shopTemp.SelectByUserID();
                                carouselTemplatesList = CarouselMaker.MakeSearchingClubShop(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId, shopTemp.ClubID);
                                break;

                            case "294":
                                carouselTemplatesList = CarouselMaker.MakeSearchingClubShopItem(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                break;

                            case "9999":
                                //無法使用 改用Flex
                              //  carouselTemplatesList = CarouselMaker.MakeSearchingItem(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                break;

                            default:
                                break;
                        }

                        if (carouselTemplatesList.Count > 0)
                        {
                            foreach (CarouselTemplate carouselTemplate in carouselTemplatesList)
                            {
                                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, carouselTemplate, ChannelAccessToken);
                            }
                        }
                        else
                        {
                            isRock.LineBot.Utility.PushMessage(item.source.userId, Message, ChannelAccessToken);
                        }
                        break;

                    case NEXTMESSAGETYPE.CONFIRM:

                        string message = "有問題";

                        switch (SplitedData[1])
                        {

                            case "52"://刪除MyOrder定單
                                confirmTemplate = ConfirmMaker.MakeCreateOrderConfirmBtn(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], item.source.userId, ChannelAccessToken);
                                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                break;

                            case "82":
                                // 從OrderTemp裡面拿到PartitionOrderID
                                orderTemp = new OrderTemp(item.source.userId);
                                orderTemp.SelectByUserID();
                                // 用OrderPartitionID去MyOrderTable找到StartTime
                                orderInfo = new OrderInfo();
                                orderInfo.OrderPartitionID = orderTemp.OrderPartitionID;

                                // 先看看MyOrderTable裡面有沒有這個OrderPartitionID，接著判斷是一次性或週期性
                                orderInfo.SelectMyOrderTableByOrderPartitionID();

                                if (orderInfo.OrderType == "Period")
                                {
                                    buttonsTemplate = ButtonMaker.MakeDeleteMyPeriodOrderConfirmBtn(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), orderInfo);
                                    isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, buttonsTemplate, ChannelAccessToken);
                                }
                                else if (orderInfo.OrderType == "Once")
                                {
                                    message = $"你確定要退出{orderInfo.OrderName}的訂單嗎?";
                                    confirmTemplate = ConfirmMaker.MakeDeleteMyOnceOrderConfirmBtn(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                    isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                }
                                else // ClubOrder不可刪除，除非退出社團
                                {
                                    message = "社團訂單沒辦法刪除喔~除非退出社團";
                                    isRock.LineBot.Utility.PushMessage(item.source.userId, message, ChannelAccessToken);

                                    // 回復7-2 Button
                                    buttonsTemplate = ButtonMaker.Make(7, 2, "");
                                    isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, buttonsTemplate, ChannelAccessToken);
                                }
                                break;

                            case "102":
                                confirmTemplate = ConfirmMaker.MakeCreateClubConfirmBtn(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], "");
                                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                break;

                            case "121":
                                switch (SplitedData[2])
                                {
                                    case "2":
                                        // 把postback data裡的SplitedData[3]是OrderPartitionID存進OrderTemp，製作再訂購的Carousel要用
                                        orderTemp = new OrderTemp(item.source.userId);
                                        orderTemp.OrderPartitionID = SplitedData[3];
                                        orderTemp.UpdateOrderPartitionIDByUserID();

                                        // 同時也把OrderPartitionID和UserID存進BuyerInfoTemp
                                        buyerTemp = new BuyerTemp(item.source.userId);
                                        buyerTemp.OrderPartitionID = SplitedData[3];
                                        buyerTemp.Item = SplitedData[4];
                                        buyerTemp.Price = double.Parse(SplitedData[5]);
                                        buyerTemp.InsertBuyerItemInfoTemp();

                                        confirmTemplate = ConfirmMaker.MakeAmountConfirmBtn(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]));
                                        isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                        break;

                                    case "3":
                                        confirmTemplate = ConfirmMaker.MakeNoteConfirmBtn(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]));
                                        isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                        break;

                                    case "4":
                                        // 把item.message.text存進BuyerInfoTemp
                                        // 先取得OrderPartitionID
                                        orderTemp = new OrderTemp(item.source.userId);
                                        orderTemp.SelectByUserID();
                                        buyerTemp = new BuyerTemp(item.source.userId);
                                        buyerTemp.OrderPartitionID = orderTemp.OrderPartitionID;
                                        buyerTemp.UpdateNoteByBuyerID();

                                        // 從BuyerInfoTemp拿出所有資料
                                        buyerTemp.SelectByBuyerID();

                                        if (buyerTemp.Note == "no")
                                        {
                                            message = $"請確認訂購{buyerTemp.Item}，" +
                                                      $"單價：{buyerTemp.Price}，" +
                                                      $"數量：{buyerTemp.Amount}，" +
                                                      $"總價：{buyerTemp.Amount * buyerTemp.Price}";
                                        }
                                        else
                                        {
                                            message = $"請確認訂購{buyerTemp.Item}，" +
                                                      $"單價：{buyerTemp.Price}，" +
                                                      $"數量：{buyerTemp.Amount}，" +
                                                      $"總價：{buyerTemp.Amount * buyerTemp.Price}，" +
                                                      $"註記：{buyerTemp.Note}";
                                        }

                                        confirmTemplate = ConfirmMaker.MakeMessageConfirmBtn(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                        isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                        break;

                                    case "5":
                                        // 已確認修改訂購，把BuyerInfoTempTable裡的東西放進BuyerInfoTable，因為是修改所以要把之前的訂購覆蓋掉
                                        // 先從UserStatus裡找到TempData，裡面放的是之前選的品項
                                        userStatus = new UserStatus(item.source.userId);
                                        userStatus.SelectByUserID();
                                        string OldBuyerInfoID = userStatus.TempData;

                                        // 拿到這次要修改的所有資料 
                                        buyerTemp = new BuyerTemp(item.source.userId);
                                        buyerTemp.SelectByBuyerID();
                                        // 給Note格式
                                        if (buyerTemp.Note != "no")
                                        {
                                            buyerTemp.Note = $"{buyerTemp.Note}(數量：{buyerTemp.Amount})";
                                        }

                                        // 以OrderPartitionID、UserID、ItemOld修改訂購的內容
                                        buyerTemp.UpdateAllTempToBuyerInfoTableByOldBuyerInfoID(OldBuyerInfoID);

                                        confirmTemplate = ConfirmMaker.MakeContinueConfirmBtn(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]));
                                        isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);

                                        break;
                                    default:
                                        break;
                                }
                                break;

                            case "122":
                                confirmTemplate = ConfirmMaker.MakeDeletOrderItem(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], item.source.userId, ChannelAccessToken);
                                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);

                                break;

                            case "152"://刪除ClubOrder定單
                                confirmTemplate = ConfirmMaker.MakeCreateOrderConfirmBtn(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], item.source.userId, ChannelAccessToken);
                                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                break;


                            case "171":
                                confirmTemplate = ConfirmMaker.MakeMessageConfirmBtn(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                break;

                            case "182":
                                // 先從ShopTemp取得ShopID
                                shopTemp = new ShopTemp(item.source.userId);
                                shopTemp.SelectByUserID();

                                //以ShopID找到ShopName
                                shopInfo = new ShopInfo();
                                shopInfo.ShopID = shopTemp.ShopID;
                                shopInfo.SelectMyShopNameByShopID();

                                message = $"你確定要刪除 {shopInfo.ShopName} 嗎?";
                                confirmTemplate = ConfirmMaker.MakeMessageConfirmBtn(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                break;

                            case "194":

                                // 確認修改一筆品項後，就直接把那筆存進主表
                                // 拿到修改之前的品項名稱和ShopID
                                shopTemp = new ShopTemp(item.source.userId);
                                shopTemp.SelectByUserID();

                                // 拿到ShopItemTemp裡暫存的新的ShopItem和ShopItemPrice，雖然回傳List但只會有一項
                                List<ShopTemp> MyshopItemList = shopTemp.GetShopItemTempFromSQL();

                                // 把temp裡的東西存進主表裡
                                shopTemp.UpdateMyShopItem(MyshopItemList[0]);

                                // 把ShopItemTempTable裡的東西清掉
                                shopTemp.DeleteShopItemTempByUserID();

                                message = "你要繼續修改品項嗎?";
                                confirmTemplate = ConfirmMaker.MakeContinueConfirmBtn(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]));
                                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                break;

                            case "201":

                                confirmTemplate = ConfirmMaker.MakeCreateOrderConfirmBtn(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], item.source.userId, ChannelAccessToken);
                                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                break;

                            case "211":
                                shopListTemp = new ShopListTemp(item.source.userId);
                                shopListTemp.ShopID = SplitedData[3];
                                shopListTemp.InsertShopIDByUserID();
                                confirmTemplate = ConfirmMaker.MakeContinueConfirmBtn(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]));
                                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                break;


                            case "231":

                                confirmTemplate = ConfirmMaker.MakeModifyOrderConfirmBtn(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], item.source.userId);
                                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                break;

                            case "232":

                                confirmTemplate = ConfirmMaker.MakeModifyOrderConfirmBtn(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], item.source.userId);
                                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                break;
                            case "241":

                                confirmTemplate = ConfirmMaker.MakeCreateOrderConfirmBtn(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], item.source.userId, ChannelAccessToken);
                                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                break;
                            case "251":
                                shopListTemp = new ShopListTemp(item.source.userId);
                                shopListTemp.ShopID = SplitedData[3];
                                shopListTemp.InsertShopIDByUserID();
                                confirmTemplate = ConfirmMaker.MakeContinueConfirmBtn(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]));
                                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                break;

                            case "261":

                                confirmTemplate = ConfirmMaker.MakeModifyOrderConfirmBtn(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], item.source.userId);
                                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                break;

                            case "262":

                                confirmTemplate = ConfirmMaker.MakeModifyOrderConfirmBtn(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], item.source.userId);
                                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                break;


                            case "271":
                                confirmTemplate = ConfirmMaker.MakeMessageConfirmBtn(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                break;

                            case "282":
                                // 先從ShopTemp取得ShopID
                                shopTemp = new ShopTemp(item.source.userId);
                                shopTemp.SelectByUserID();

                                //以ShopID找到ShopName
                                shopInfo = new ShopInfo();
                                shopInfo.ShopID = shopTemp.ShopID;
                                shopInfo.SelectClubShopNameByShopID();

                                message = $"你確定要刪除 {shopInfo.ShopName} 嗎?";
                                confirmTemplate = ConfirmMaker.MakeMessageConfirmBtn(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                break;

                            case "294":

                                // 確認修改一筆品項後，就直接把那筆存進主表
                                // 拿到修改之前的品項名稱和ShopID
                                shopTemp = new ShopTemp(item.source.userId);
                                shopTemp.SelectByUserID();

                                // 拿到ShopItemTemp裡暫存的新的ShopItem，雖然回傳List但只會有一項
                                List<ShopTemp> ClubshopItemList = shopTemp.GetShopItemTempFromSQL();

                                // 把temp裡的東西存進主表裡
                                shopTemp.UpdateClubShopItem(ClubshopItemList[0]);

                                // 把ShopItemTempTable裡的東西清掉
                                shopTemp.InitializeShopTempByUserID();

                                message = "你要繼續修改品項嗎?";
                                confirmTemplate = ConfirmMaker.MakeContinueConfirmBtn(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]));
                                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                break;

                            case "9999":

                                switch (SplitedData[2])
                                {
                                    case "2":
                                        // 把postback data裡的SplitedData[3]是OrderPartitionID存進OrderTemp，製作再訂購的Carousel要用
                                        orderTemp = new OrderTemp(item.source.userId);
                                        orderTemp.OrderPartitionID = SplitedData[3];
                                        orderTemp.UpdateOrderPartitionIDByUserID();

                                        // 同時也把OrderPartitionID和UserID存進BuyerInfoTemp
                                        buyerTemp = new BuyerTemp(item.source.userId);
                                        buyerTemp.OrderPartitionID = SplitedData[3];
                                        buyerTemp.Item = SplitedData[4];
                                        buyerTemp.Price = double.Parse(SplitedData[5]);
                                        buyerTemp.InsertBuyerItemInfoTemp();

                                        confirmTemplate = ConfirmMaker.MakeAmountConfirmBtn(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]));
                                        isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                        break;

                                    case "3":
                                        confirmTemplate = ConfirmMaker.MakeNoteConfirmBtn(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]));
                                        isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                        break;

                                    case "4":
                                        // 把item.message.text存進BuyerInfoTemp
                                        // 先取得OrderPartitionID
                                        orderTemp = new OrderTemp(item.source.userId);
                                        orderTemp.SelectByUserID();
                                        buyerTemp = new BuyerTemp(item.source.userId);
                                        buyerTemp.OrderPartitionID = orderTemp.OrderPartitionID;
                                        buyerTemp.UpdateNoteByBuyerID();

                                        // 從BuyerInfoTemp拿出所有資料
                                        buyerTemp.SelectByBuyerID();

                                        if (buyerTemp.Note == "no")
                                        {
                                            message = $"請確認訂購{buyerTemp.Item}，" +
                                                      $"單價：{buyerTemp.Price}，" +
                                                      $"數量：{buyerTemp.Amount}，" +
                                                      $"總價：{buyerTemp.Amount * buyerTemp.Price}";
                                        }
                                        else
                                        {
                                            message = $"請確認訂購{buyerTemp.Item}，" +
                                                      $"單價：{buyerTemp.Price}，" +
                                                      $"數量：{buyerTemp.Amount}，" +
                                                      $"總價：{buyerTemp.Amount * buyerTemp.Price}，" +
                                                      $"註記：{buyerTemp.Note}";
                                        }

                                        confirmTemplate = ConfirmMaker.MakeMessageConfirmBtn(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                        isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                        break;

                                    case "5":
                                        // 已確認訂購，把BuyerInfoTempTable裡的東西放進BuyerInfoTable

                                        // 1.從BuyerInfoTempTable取得新的訂單資訊
                                        buyerTemp = new BuyerTemp(item.source.userId);
                                        buyerTemp.SelectByBuyerID();


                                        // 2. 檢查訂單是不是收單了
                                        string SplittedOrderPartitionID;
                                        orderInfo = new OrderInfo();
                                        orderInfo.OrderPartitionID = buyerTemp.OrderPartitionID;
                                        SplittedOrderPartitionID = buyerTemp.OrderPartitionID.Substring(0, 2);
                                        switch (SplittedOrderPartitionID)
                                        {
                                            case "MO": // MyOrder
                                                orderInfo.SelectMyOrderTableByOrderPartitionID();
                                                break;

                                            case "CO": // ClubOrder
                                                orderInfo.SelectClubOrderTableByOrderPartitionID();
                                                break;

                                            default:
                                                break;
                                        }


                                        if (orderInfo.OrderStatus == "available")//若是可以訂購狀態才寫入
                                        {
                                            // 3.先檢查BuyerInfoTable裡，這個買家有沒有買過同一件品項，如果有的話數量要加上去，Note也要更新
                                            buyerInfo = new BuyerInfo(item.source.userId);
                                            buyerInfo.OrderPartitionID = buyerTemp.OrderPartitionID;
                                            buyerInfo.Item = buyerTemp.Item;
                                            int result = buyerInfo.SelectByBuyerIDandOrderPartitionIDandItem();
                                            if (result > 0)
                                            {
                                                // Note拼字串
                                                if (buyerInfo.Note == "no")
                                                {
                                                    if (buyerTemp.Note == "no")
                                                    {
                                                        buyerTemp.Note = "no";
                                                    }
                                                    else
                                                    {
                                                        buyerTemp.Note = $"{buyerTemp.Note}(數量：{buyerTemp.Amount})";
                                                    }
                                                }
                                                else
                                                {
                                                    if (buyerTemp.Note == "no")
                                                    {
                                                        buyerTemp.Note = buyerInfo.Note;
                                                    }
                                                    else
                                                    {
                                                        buyerTemp.Note = $"{buyerInfo.Note}{buyerTemp.Note}(數量：{buyerTemp.Amount})";
                                                    }
                                                }

                                                // Amount相加
                                                buyerTemp.Amount += buyerInfo.Amount;

                                                buyerTemp.UpdateAllTempToBuyerInfoTableByOldBuyerInfoID(buyerInfo.Item);
                                            }
                                            else if (result == 0)
                                            {
                                                if (buyerTemp.Note == "no")
                                                {
                                                    buyerTemp.Note = "no";
                                                }
                                                else
                                                {
                                                    buyerTemp.Note = $"{buyerTemp.Note}(數量：{buyerTemp.Amount})";
                                                }

                                                buyerTemp.InsertAllTempToBuyerInfoTable();
                                            }

                                            // 把BuyerInfoTempTable清掉
                                            buyerTemp.DeleteByBuyerID();

                                            message = "你要繼續訂購嗎??";
                                            confirmTemplate = ConfirmMaker.MakeContinueConfirmBtn(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]));
                                            isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);
                                        }
                                        else
                                        {
                                            buyerTemp.DeleteByBuyerID();
                                            message = "訂單已經收單囉囉~\n無法再訂購~~";
                                            isRock.LineBot.Utility.PushMessage(item.source.userId, message, ChannelAccessToken);
                                        }

                                        break;
                                    default:
                                        break;
                                }
                                break;

                            default:
                                break;
                        }
                        break;

                    case NEXTMESSAGETYPE.MESSAGE:
                        List<string> Buyers;
                        string OrderPartitionIDSplitted;
                        switch (SplitedData[1])
                        {
                            case "201":
                                orderTemp = new OrderTemp(item.source.userId);
                                orderTemp.ShopID = SplitedData[3];
                                orderTemp.UpdateShopID();
                                break;
                            case "241":
                                orderTemp = new OrderTemp(item.source.userId);
                                orderTemp.ShopID = SplitedData[3];
                                orderTemp.UpdateShopID();
                                break;
                            case "211":
                                //orderTemp = new OrderTemp(item.source.userId);
                                //orderTemp.ShopID = SplitedData[3];
                                //orderTemp.UpdateShopID();
                                break;
                            case "251":
                                //orderTemp = new OrderTemp(item.source.userId);
                                //orderTemp.ShopID = SplitedData[3];
                                //orderTemp.UpdateShopID();
                                break;
                            case "9998":
                                switch (SplitedData[3])
                                {
                                    case "accept":
                                        orderInfo = new OrderInfo();
                                        orderInfo.OrderPartitionID = SplitedData[4];
                                        OrderPartitionIDSplitted = orderInfo.OrderPartitionID.Substring(0, 2);

                                        switch (OrderPartitionIDSplitted)
                                        {
                                            case "MO": // MyOrder
                                                orderInfo.SelectMyOrderTableByOrderPartitionID();
                                                break;

                                            case "CO": // ClubOrder
                                                orderInfo.SelectClubOrderTableByOrderPartitionID();
                                                break;

                                            default:
                                                break;
                                        }

                                        buyerInfo = new BuyerInfo();
                                        buyerInfo.OrderPartitionID = orderInfo.OrderPartitionID;
                                        Buyers = buyerInfo.SelectAllBuyerTotalPartitionID();
                                        foreach (string user in Buyers)
                                        {
                                            isRock.LineBot.Utility.PushMessage(user, $"訂單成立，店家已接受訂單:\n{orderInfo.OrderName}!", ChannelAccessToken);
                                        }

                                        break;
                                    case "refuse":
                                        orderInfo = new OrderInfo();
                                        orderInfo.OrderPartitionID = SplitedData[4];
                                        OrderPartitionIDSplitted = orderInfo.OrderPartitionID.Substring(0, 2);

                                        switch (OrderPartitionIDSplitted)
                                        {
                                            case "MO": // MyOrder
                                                orderInfo.SelectMyOrderTableByOrderPartitionID();
                                                break;

                                            case "CO": // ClubOrder
                                                orderInfo.SelectClubOrderTableByOrderPartitionID();
                                                break;

                                            default:
                                                break;
                                        }

                                        buyerInfo = new BuyerInfo();
                                        buyerInfo.OrderPartitionID = orderInfo.OrderPartitionID;
                                        Buyers = buyerInfo.SelectAllBuyerTotalPartitionID();
                                        foreach (string user in Buyers)
                                        {
                                            isRock.LineBot.Utility.PushMessage(user, $"訂單失效，店家拒絕訂單:\n{orderInfo.OrderName}!", ChannelAccessToken);
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                        replaymessage = MessageMaker.make(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3]);
                        isRock.LineBot.Utility.PushMessage(item.source.userId, replaymessage, ChannelAccessToken);
                        // isRock.LineBot.Utility.ReplyMessage(item.replyToken, replaymessage, ChannelAccessToken);
                        break;

                    case NEXTMESSAGETYPE.FLEX:
                        string JasonMessage;
                        switch (SplitedData[1])
                        {
                            case "9998":
                                string BossID = "default";
                                JasonMessage = FlexMaker.MakeToShopBossFlex(SplitedData[3], SplitedData[4], item.source.userId, out BossID);
                                isRock.LineBot.Utility.PushMessagesWithJSON(BossID, JasonMessage, ChannelAccessToken);
                                break;

                            case "9999":
                                JasonMessage = FlexMaker.MakeSearchingItem(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                isRock.LineBot.Utility.PushMessagesWithJSON(item.source.userId, JasonMessage, ChannelAccessToken);

                                break;
                            default:
                                JasonMessage = FlexMaker.MakeShopItem(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3]);
                                isRock.LineBot.Utility.PushMessagesWithJSON(item.source.userId, JasonMessage, ChannelAccessToken);

                                break;
                        }

                        break;

                    case NEXTMESSAGETYPE.DATETIMEBUTTON:
                        ButtonsTemplate DateTimebtnTemplate;
                        switch (SplitedData[1])
                        {
                            case "42":
                                switch (SplitedData[2])
                                {
                                    case "1":
                                        DateTimebtnTemplate = ButtonMaker.DateBtnMake(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3]);
                                        isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, DateTimebtnTemplate, ChannelAccessToken);
                                        break;

                                    case "3":
                                        DateTimebtnTemplate = ButtonMaker.TimeBtnMake(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3]);
                                        isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, DateTimebtnTemplate, ChannelAccessToken);
                                        break;
                                }

                                break;

                            case "142":
                                switch (SplitedData[2])
                                {
                                    case "1":
                                        periodOrderTmp = new PeriodOrderTmp(item.source.userId);
                                        periodOrderTmp.ClubID = SplitedData[3];
                                        periodOrderTmp.UpdateClubIDByUserID();
                                        DateTimebtnTemplate = ButtonMaker.DateBtnMake(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3]);
                                        isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, DateTimebtnTemplate, ChannelAccessToken);
                                        break;

                                    case "3":
                                        DateTimebtnTemplate = ButtonMaker.TimeBtnMake(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3]);
                                        isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, DateTimebtnTemplate, ChannelAccessToken);
                                        break;
                                }
                                break;

                            case "141":
                                switch (SplitedData[2])
                                {
                                    case "1":
                                        DateTimebtnTemplate = ButtonMaker.DateTimeBtnMake(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3]);
                                        isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, DateTimebtnTemplate, ChannelAccessToken);
                                        break;
                                }
                                break;

                            default:
                                DateTimebtnTemplate = ButtonMaker.DateTimeBtnMake(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3]);
                                isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, DateTimebtnTemplate, ChannelAccessToken);
                                break;
                        }
                        break;

                    case NEXTMESSAGETYPE.CLUBINFOSQLCOMMUNICATE:
                        replaymessage = SQLCommunicator.CommunicateClubInfo(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], ChannelAccessToken);
                        isRock.LineBot.Utility.ReplyMessage(item.replyToken, replaymessage, ChannelAccessToken);
                        //參加完社團吐Menu
                        //   MenuBtnTemplate = ButtonMaker.MakeMenu(item);
                        //    isRock.LineBot.Utility.PushImageMapMessage(item.source.userId, MenuBtnTemplate, ChannelAccessToken);
                        break;

                    case NEXTMESSAGETYPE.USERINFOSQLCOMMUNICATE:

                        switch (SplitedData[1])
                        {
                            case "71":
                                switch (SplitedData[2])
                                {
                                    case "3":

                                        replaymessage = SQLCommunicator.CommunicateUserInfo(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], ChannelAccessToken);
                                        isRock.LineBot.Utility.ReplyMessage(item.replyToken, replaymessage, ChannelAccessToken);
                                        //參加成功後吐Menu
                                        //MenuBtnTemplate = ButtonMaker.MakeMenu(item);
                                        //isRock.LineBot.Utility.PushImageMapMessage(item.source.userId, MenuBtnTemplate, ChannelAccessToken);
                                        break;

                                    default:
                                        break;
                                }
                                break;

                            case "82":
                                switch (SplitedData[2])
                                {
                                    case "2":
                                        replaymessage = SQLCommunicator.CommunicateUserInfo(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], ChannelAccessToken);
                                        isRock.LineBot.Utility.ReplyMessage(item.replyToken, replaymessage, ChannelAccessToken);

                                        MenuBtnTemplate = ImagemapMaker.MakeMenu(item);
                                        isRock.LineBot.Utility.PushImageMapMessage(item.source.userId, MenuBtnTemplate, ChannelAccessToken);
                                        break;

                                    default:
                                        break;
                                }
                                break;




                            default:
                                break;
                        }
                        break;


                    case NEXTMESSAGETYPE.ORDERSQLCOMMUNICATE:
                        //與SQL溝通，並且判斷使用者start/end時間都選了
                        bool BothTimeHasFilledIn = false;
                        switch (SplitedData[1])
                        {
                            case "20":
                                if (SplitedData[3] != "default")//若是上一步則不用近來
                                {
                                    BothTimeHasFilledIn = SQLCommunicator.CommunicateOrderTmp(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], item.postback.Params.datetime, ChannelAccessToken);
                                }
                                else
                                {
                                    BothTimeHasFilledIn = true;
                                }

                                //如果填滿就呼叫下一個動作
                                if (BothTimeHasFilledIn)
                                {
                                    ButtonsTemplate buttonsTemplateForOrder = ButtonMaker.Make(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3]);
                                    isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, buttonsTemplateForOrder, ChannelAccessToken);
                                }
                                break;
                            case "21":
                                if (SplitedData[3] != "default")//若是上一步則不用近來
                                {
                                    BothTimeHasFilledIn = SQLCommunicator.CommunicatePeriodOrderTmpTime(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], item.postback.Params.time, ChannelAccessToken);
                                }
                                else
                                {
                                    BothTimeHasFilledIn = true;
                                }
                                //如果填滿就呼叫下一個動作
                                if (BothTimeHasFilledIn)
                                {
                                    ButtonsTemplate buttonsTemplateForOrder = ButtonMaker.Make(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3]);
                                    isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, buttonsTemplateForOrder, ChannelAccessToken);
                                }
                                break;
                            case "24":

                                if (SplitedData[3] != "default")//若是上一步則不用近來
                                {
                                    BothTimeHasFilledIn = SQLCommunicator.CommunicateOrderTmp(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], item.postback.Params.datetime, ChannelAccessToken);
                                }
                                else
                                {
                                    BothTimeHasFilledIn = true;
                                }


                                //如果填滿就呼叫下一個動作
                                if (BothTimeHasFilledIn)
                                {
                                    ButtonsTemplate buttonsTemplateForOrder = ButtonMaker.Make(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3]);
                                    isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, buttonsTemplateForOrder, ChannelAccessToken);
                                }
                                break;

                            case "25":
                                if (SplitedData[3] != "default")//若是上一步則不用近來
                                {
                                    BothTimeHasFilledIn = SQLCommunicator.CommunicatePeriodOrderTmpTime(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], item.postback.Params.time, ChannelAccessToken);
                                }
                                else
                                {
                                    BothTimeHasFilledIn = true;
                                }
                                //如果填滿就呼叫下一個動作
                                if (BothTimeHasFilledIn)
                                {
                                    ButtonsTemplate buttonsTemplateForOrder = ButtonMaker.Make(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3]);
                                    isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, buttonsTemplateForOrder, ChannelAccessToken);
                                }
                                break;

                            case "42":
                                string JMessage;
                                switch (SplitedData[2])
                                {
                                    case "2":
                                        BothTimeHasFilledIn = SQLCommunicator.CommunicatePeriodOrderTmpForDate(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], item.postback.Params.date, ChannelAccessToken);

                                        //如果填滿就呼叫下一個動作
                                        if (BothTimeHasFilledIn)
                                        {
                                            JMessage = FlexMaker.MyOrderMakeWeekFlex(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3]);
                                            isRock.LineBot.Utility.PushMessagesWithJSON(item.source.userId, JMessage, ChannelAccessToken);
                                        }
                                        break;

                                    case "21":
                                        string replymessage = SQLCommunicator.CheckPeriodOrderTmpWeek(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3]);
                                        // isRock.LineBot.Utility.PushMessage(item.source.userId, replymessage, ChannelAccessToken);
                                        JMessage = FlexMaker.MyOrderMakeWeekFlex(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3]);
                                        isRock.LineBot.Utility.PushMessagesWithJSON(item.source.userId, JMessage, ChannelAccessToken);
                                        break;

                                    default:
                                        BothTimeHasFilledIn = SQLCommunicator.CommunicatePeriodOrderTmpForDate(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], item.postback.Params.datetime, ChannelAccessToken);

                                        //如果填滿就呼叫下一個動作
                                        if (BothTimeHasFilledIn)
                                        {
                                            JMessage = FlexMaker.MyOrderMakeWeekFlex(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3]);
                                            isRock.LineBot.Utility.PushMessagesWithJSON(item.source.userId, JMessage, ChannelAccessToken);
                                        }
                                        break;
                                }
                                break;

                            case "52":
                                message = SQLCommunicator.DeletOrder(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], item.source.userId);
                                isRock.LineBot.Utility.PushMessage(item.source.userId, message, ChannelAccessToken);
                                break;

                            case "61":
                                switch (SplitedData[2])
                                {

                                    case "2":
                                        BothTimeHasFilledIn = SQLCommunicator.CommunicateOrderTmp(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], item.postback.Params.datetime, ChannelAccessToken);

                                        //如果填滿就呼叫下一個動作
                                        if (BothTimeHasFilledIn)
                                        {
                                            message = "Nodata";
                                            confirmTemplate = ConfirmMaker.MakeMessageConfirmBtn(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                            isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);

                                            //ButtonsTemplate buttonsTemplateForOrder = ButtonMaker.Make(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3]);
                                            //isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, buttonsTemplateForOrder, ChannelAccessToken);
                                        }
                                        break;

                                    case "3":
                                        message = SQLCommunicator.UpdateOrder(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                        isRock.LineBot.Utility.PushMessage(item.source.userId, message, ChannelAccessToken);
                                        break;

                                    default:
                                        break;
                                }
                                break;

                            case "122":
                                message = SQLCommunicator.DeletBuyerInfoItem(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), int.Parse(SplitedData[3]), item.source.userId);
                                isRock.LineBot.Utility.PushMessage(item.source.userId, message, ChannelAccessToken);

                                break;


                            case "142":
                                switch (SplitedData[2])
                                {
                                    case "2":
                                        BothTimeHasFilledIn = SQLCommunicator.CommunicatePeriodOrderTmpForDate(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], item.postback.Params.date, ChannelAccessToken);

                                        //如果填滿就呼叫下一個動作
                                        if (BothTimeHasFilledIn)
                                        {
                                            JMessage = FlexMaker.ClubMakeWeekFlex(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3]);
                                            isRock.LineBot.Utility.PushMessagesWithJSON(item.source.userId, JMessage, ChannelAccessToken);
                                        }
                                        break;

                                    case "21":
                                        string replymessage = SQLCommunicator.CheckPeriodOrderTmpWeek(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3]);
                                        // isRock.LineBot.Utility.PushMessage(item.source.userId, replymessage, ChannelAccessToken);
                                        JMessage = FlexMaker.ClubMakeWeekFlex(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3]);
                                        isRock.LineBot.Utility.PushMessagesWithJSON(item.source.userId, JMessage, ChannelAccessToken);
                                        break;

                                    default:
                                        BothTimeHasFilledIn = SQLCommunicator.CommunicatePeriodOrderTmpForDate(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], item.postback.Params.datetime, ChannelAccessToken);

                                        //如果填滿就呼叫下一個動作
                                        if (BothTimeHasFilledIn)
                                        {
                                            JMessage = FlexMaker.MyOrderMakeWeekFlex(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3]);
                                            isRock.LineBot.Utility.PushMessagesWithJSON(item.source.userId, JMessage, ChannelAccessToken);
                                        }
                                        break;
                                }
                                break;

                            case "152":
                                message = SQLCommunicator.DeletOrder(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], item.source.userId);
                                isRock.LineBot.Utility.PushMessage(item.source.userId, message, ChannelAccessToken);
                                break;

                            case "161":
                                switch (SplitedData[2])
                                {
                                    case "2":

                                        BothTimeHasFilledIn = SQLCommunicator.CommunicateOrderTmp(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], item.postback.Params.datetime, ChannelAccessToken);

                                        //如果填滿就呼叫下一個動作
                                        if (BothTimeHasFilledIn)
                                        {
                                            message = "Nodata";
                                            confirmTemplate = ConfirmMaker.MakeMessageConfirmBtn(message, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                            isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, confirmTemplate, ChannelAccessToken);

                                            //ButtonsTemplate buttonsTemplateForOrder = ButtonMaker.Make(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3]);
                                            //isRock.LineBot.Utility.PushTemplateMessage(item.source.userId, buttonsTemplateForOrder, ChannelAccessToken);
                                        }
                                        break;

                                    case "3":
                                        message = SQLCommunicator.UpdateOrder(int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), item.source.userId);
                                        isRock.LineBot.Utility.PushMessage(item.source.userId, message, ChannelAccessToken);
                                        break;
                                    default:
                                        break;
                                }
                                break;


                            case "201":
                                message = SQLCommunicator.CommunicateOrder(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], ChannelAccessToken);
                                isRock.LineBot.Utility.PushMessage(item.source.userId, message, ChannelAccessToken);
                                break;

                            case "211":
                                message = SQLCommunicator.MyPeriodOrderCommunicate(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], ChannelAccessToken);
                                isRock.LineBot.Utility.PushMessage(item.source.userId, message, ChannelAccessToken);
                                break;

                            case "231":
                                message = SQLCommunicator.CommunicateOrder(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], ChannelAccessToken);
                                isRock.LineBot.Utility.PushMessage(item.source.userId, message, ChannelAccessToken);
                                break;

                            case "241":
                                message = SQLCommunicator.CommunicateOrder(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], ChannelAccessToken);
                                isRock.LineBot.Utility.PushMessage(item.source.userId, message, ChannelAccessToken);
                                break;

                            case "251":
                                message = SQLCommunicator.ClubPeriodOrderCommunicate(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], ChannelAccessToken);
                                isRock.LineBot.Utility.PushMessage(item.source.userId, message, ChannelAccessToken);
                                break;

                            case "261":
                                message = SQLCommunicator.CommunicateOrder(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), SplitedData[3], ChannelAccessToken);
                                isRock.LineBot.Utility.PushMessage(item.source.userId, message, ChannelAccessToken);
                                break;

                            default:
                                break;
                        }
                        break;

                    case NEXTMESSAGETYPE.SHOPINFOSQLCOMMUNICATE:
                        switch (SplitedData[1])
                        {
                            case "171":
                                switch (SplitedData[2])
                                {
                                    case "8":
                                        message = SQLCommunicator.CommunicateMyShop(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), ChannelAccessToken);
                                        isRock.LineBot.Utility.PushMessage(item.source.userId, message, ChannelAccessToken);

                                        MenuBtnTemplate = ImagemapMaker.MakeMenu(item);
                                        isRock.LineBot.Utility.PushImageMapMessage(item.source.userId, MenuBtnTemplate, ChannelAccessToken);
                                        break;

                                    default:
                                        break;
                                }
                                break;

                            case "182":
                                switch (SplitedData[2])
                                {
                                    case "2":
                                        message = SQLCommunicator.CommunicateDeleteShop(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), ChannelAccessToken);
                                        isRock.LineBot.Utility.PushMessage(item.source.userId, message, ChannelAccessToken);

                                        MenuBtnTemplate = ImagemapMaker.MakeMenu(item);
                                        isRock.LineBot.Utility.PushImageMapMessage(item.source.userId, MenuBtnTemplate, ChannelAccessToken);
                                        break;

                                    default:
                                        break;
                                }
                                break;

                            case "191":
                                switch (SplitedData[2])
                                {
                                    case "3":
                                        message = SQLCommunicator.CommunicateShopNameUpdate(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), ChannelAccessToken);
                                        isRock.LineBot.Utility.PushMessage(item.source.userId, message, ChannelAccessToken);

                                        MenuBtnTemplate = ImagemapMaker.MakeMenu(item);
                                        isRock.LineBot.Utility.PushImageMapMessage(item.source.userId, MenuBtnTemplate, ChannelAccessToken);
                                        break;

                                    default:
                                        break;
                                }
                                break;

                            case "192":
                                switch (SplitedData[2])
                                {
                                    case "3":
                                        message = SQLCommunicator.CommunicateShopPhoneUpdate(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), ChannelAccessToken);
                                        isRock.LineBot.Utility.PushMessage(item.source.userId, message, ChannelAccessToken);

                                        MenuBtnTemplate = ImagemapMaker.MakeMenu(item);
                                        isRock.LineBot.Utility.PushImageMapMessage(item.source.userId, MenuBtnTemplate, ChannelAccessToken);
                                        break;

                                    default:
                                        break;
                                }
                                break;

                            case "193":
                                switch (SplitedData[2])
                                {
                                    case "3":
                                        message = SQLCommunicator.CommunicateShopAddressUpdate(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), ChannelAccessToken);
                                        isRock.LineBot.Utility.PushMessage(item.source.userId, message, ChannelAccessToken);

                                        MenuBtnTemplate = ImagemapMaker.MakeMenu(item);
                                        isRock.LineBot.Utility.PushImageMapMessage(item.source.userId, MenuBtnTemplate, ChannelAccessToken);
                                        break;

                                    default:
                                        break;
                                }
                                break;

                            case "271":
                                switch (SplitedData[2])
                                {
                                    case "8":
                                        message = SQLCommunicator.CommunicateClubShop(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), ChannelAccessToken);
                                        isRock.LineBot.Utility.PushMessage(item.source.userId, message, ChannelAccessToken);

                                        MenuBtnTemplate = ImagemapMaker.MakeMenu(item);
                                        isRock.LineBot.Utility.PushImageMapMessage(item.source.userId, MenuBtnTemplate, ChannelAccessToken);
                                        break;

                                    default:
                                        break;
                                }
                                break;

                            case "282":
                                switch (SplitedData[2])
                                {
                                    case "2":
                                        message = SQLCommunicator.CommunicateDeleteShop(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), ChannelAccessToken);
                                        isRock.LineBot.Utility.PushMessage(item.source.userId, message, ChannelAccessToken);

                                        MenuBtnTemplate = ImagemapMaker.MakeMenu(item);
                                        isRock.LineBot.Utility.PushImageMapMessage(item.source.userId, MenuBtnTemplate, ChannelAccessToken);
                                        break;

                                    default:
                                        break;
                                }
                                break;

                            case "291":
                                switch (SplitedData[2])
                                {
                                    case "3":
                                        message = SQLCommunicator.CommunicateShopNameUpdate(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), ChannelAccessToken);
                                        isRock.LineBot.Utility.PushMessage(item.source.userId, message, ChannelAccessToken);

                                        MenuBtnTemplate = ImagemapMaker.MakeMenu(item);
                                        isRock.LineBot.Utility.PushImageMapMessage(item.source.userId, MenuBtnTemplate, ChannelAccessToken);
                                        break;

                                    default:
                                        break;
                                }
                                break;

                            case "292":
                                switch (SplitedData[2])
                                {
                                    case "3":
                                        message = SQLCommunicator.CommunicateShopPhoneUpdate(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), ChannelAccessToken);
                                        isRock.LineBot.Utility.PushMessage(item.source.userId, message, ChannelAccessToken);

                                        MenuBtnTemplate = ImagemapMaker.MakeMenu(item);
                                        isRock.LineBot.Utility.PushImageMapMessage(item.source.userId, MenuBtnTemplate, ChannelAccessToken);
                                        break;

                                    default:
                                        break;
                                }
                                break;

                            case "293":
                                switch (SplitedData[2])
                                {
                                    case "3":
                                        message = SQLCommunicator.CommunicateShopAddressUpdate(item.source.userId, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), ChannelAccessToken);
                                        isRock.LineBot.Utility.PushMessage(item.source.userId, message, ChannelAccessToken);

                                        MenuBtnTemplate = ImagemapMaker.MakeMenu(item);
                                        isRock.LineBot.Utility.PushImageMapMessage(item.source.userId, MenuBtnTemplate, ChannelAccessToken);
                                        break;

                                    default:
                                        break;
                                }
                                break;

                            default:
                                break;
                        }
                        break;

                    default:
                        break;
                }
                //回覆用戶
            }
            else
            {
                Message = $"該動作有效時間已過，請再重新選擇";
                //回覆用戶
                isRock.LineBot.Utility.ReplyMessage(item.replyToken, Message, ChannelAccessToken);
            }
        }



        /// <summary>
        /// 用來辨別回復格式，回傳常數
        /// </summary>
        /// <param name="spiledData"></param>
        /// <returns></returns>
        private NEXTMESSAGETYPE RecognizeMessageType(Event item, string[] spiledData)
        {
            string QID = spiledData[1];
            string OID = spiledData[2];

            switch (QID)
            {
                case "1":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.MENU;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.BUTTON;

                case "2":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.BUTTON;
                        case "1":
                            return NEXTMESSAGETYPE.BUTTON;
                        case "2":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "3":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.BUTTON;
                        case "1":
                            return NEXTMESSAGETYPE.BUTTON;
                        case "2":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        case "3":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "4":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.BUTTON;

                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.DATETIMEBUTTON;

                case "5":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.BUTTON;
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "6":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.BUTTON;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "7":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.BUTTON;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "8":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.BUTTON;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "9":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.BUTTON;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "10":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.BUTTON;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "11":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.BUTTON;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "12":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.BUTTON;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "13":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.BUTTON;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "14":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.BUTTON;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;


                case "15":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.BUTTON;
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "16":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.BUTTON;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "17":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.BUTTON;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "18":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.BUTTON;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "19":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.BUTTON;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "20":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.ORDERSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "21":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.ORDERSQLCOMMUNICATE;
                        case "99":
                            return NEXTMESSAGETYPE.BUTTON;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "22":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "23":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.BUTTON;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "24":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.ORDERSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "25":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.ORDERSQLCOMMUNICATE;
                        case "99":
                            return NEXTMESSAGETYPE.BUTTON;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "26":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.BUTTON;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;


                case "27":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.BUTTON;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "28":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.BUTTON;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "29":
                    switch (OID)
                    {
                        case "0":
                            return NEXTMESSAGETYPE.BUTTON;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "32":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "41":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.DATETIMEBUTTON;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "42":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.DATETIMEBUTTON;
                        case "2":
                            return NEXTMESSAGETYPE.ORDERSQLCOMMUNICATE;
                        case "3":
                            return NEXTMESSAGETYPE.DATETIMEBUTTON;
                        case "21":
                            return NEXTMESSAGETYPE.ORDERSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "51":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;

                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "52":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "2":
                            return NEXTMESSAGETYPE.ORDERSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "53":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.MESSAGE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "61":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.DATETIMEBUTTON;
                        case "2":
                            return NEXTMESSAGETYPE.ORDERSQLCOMMUNICATE;
                        case "3":
                            return NEXTMESSAGETYPE.ORDERSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "71":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.MESSAGE;
                        case "2":
                            // 在MessageManager那邊
                            break;
                        case "3":
                            return NEXTMESSAGETYPE.USERINFOSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "72":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        case "2":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "73":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        case "2":
                            return NEXTMESSAGETYPE.MESSAGE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "81":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;

                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "82":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "2":
                            return NEXTMESSAGETYPE.USERINFOSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "91":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.MESSAGE;
                        case "2":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "3":
                            return NEXTMESSAGETYPE.CLUBINFOSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "92":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.MESSAGE;
                        case "2":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "3":
                            return NEXTMESSAGETYPE.CLUBINFOSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "93":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "101":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.MESSAGE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "102":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "2":
                            return NEXTMESSAGETYPE.CLUBINFOSQLCOMMUNICATE;
                        case "3":
                            return NEXTMESSAGETYPE.MENU;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "112":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "121":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        case "2":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "21":
                            return NEXTMESSAGETYPE.MESSAGE;
                        case "3":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "31":
                            return NEXTMESSAGETYPE.MESSAGE;
                        case "4":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "5":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "6":
                            return NEXTMESSAGETYPE.MESSAGE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;


                case "122": // 使用者訂購流程
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "2":
                            return NEXTMESSAGETYPE.ORDERSQLCOMMUNICATE;

                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "132":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "141":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.DATETIMEBUTTON;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "142":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.DATETIMEBUTTON;
                        case "2":
                            return NEXTMESSAGETYPE.ORDERSQLCOMMUNICATE;
                        case "3":
                            return NEXTMESSAGETYPE.DATETIMEBUTTON;
                        case "21":
                            return NEXTMESSAGETYPE.ORDERSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "151":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "152":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "2":
                            return NEXTMESSAGETYPE.ORDERSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "153":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.MESSAGE;

                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "161":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.DATETIMEBUTTON;
                        case "2":
                            return NEXTMESSAGETYPE.ORDERSQLCOMMUNICATE;
                        case "3":
                            return NEXTMESSAGETYPE.ORDERSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "171":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.MESSAGE;
                        case "4":
                            return NEXTMESSAGETYPE.MESSAGE;
                        case "7":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "8":
                            return NEXTMESSAGETYPE.SHOPINFOSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "172":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "182":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "2":
                            return NEXTMESSAGETYPE.SHOPINFOSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "191":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.MESSAGE;
                        case "2":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "3":
                            return NEXTMESSAGETYPE.SHOPINFOSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "192":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.MESSAGE;
                        case "2":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "3":
                            return NEXTMESSAGETYPE.SHOPINFOSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "193":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.MESSAGE;
                        case "2":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "3":
                            return NEXTMESSAGETYPE.SHOPINFOSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "194":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        case "2":
                            return NEXTMESSAGETYPE.MESSAGE;
                        case "3":
                            return NEXTMESSAGETYPE.MESSAGE;

                        case "5":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "6":
                            return NEXTMESSAGETYPE.MESSAGE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "201":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        case "2":
                            return NEXTMESSAGETYPE.FLEX;
                        case "3":
                            return NEXTMESSAGETYPE.MESSAGE;
                        case "4":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "5":
                            return NEXTMESSAGETYPE.ORDERSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "202":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        case "2":
                            return NEXTMESSAGETYPE.FLEX;
                        case "3":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "4":
                            return NEXTMESSAGETYPE.ORDERSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "211":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        case "2":
                            return NEXTMESSAGETYPE.FLEX;
                        case "3":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "4":
                            return NEXTMESSAGETYPE.MESSAGE;
                        case "6":
                            return NEXTMESSAGETYPE.ORDERSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "212":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        case "2":
                            return NEXTMESSAGETYPE.FLEX;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "231":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        case "2":
                            return NEXTMESSAGETYPE.FLEX;
                        case "3":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "4":
                            return NEXTMESSAGETYPE.ORDERSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "232":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        case "2":
                            return NEXTMESSAGETYPE.FLEX;
                        case "3":
                            return NEXTMESSAGETYPE.CONFIRM;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "241":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        case "2":
                            return NEXTMESSAGETYPE.FLEX;
                        case "3":
                            return NEXTMESSAGETYPE.MESSAGE;
                        case "4":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "5":
                            return NEXTMESSAGETYPE.ORDERSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "242":

                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        case "2":
                            return NEXTMESSAGETYPE.FLEX;
                        case "3":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "4":
                            return NEXTMESSAGETYPE.ORDERSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "251":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        case "2":
                            return NEXTMESSAGETYPE.FLEX;
                        case "3":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "4":
                            return NEXTMESSAGETYPE.MESSAGE;
                        case "6":
                            return NEXTMESSAGETYPE.ORDERSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;


                case "252":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        case "2":
                            return NEXTMESSAGETYPE.FLEX;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "261":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        case "2":
                            return NEXTMESSAGETYPE.FLEX;
                        case "3":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "4":
                            return NEXTMESSAGETYPE.ORDERSQLCOMMUNICATE;
                        default:
                            break;

                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "262":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        case "2":
                            return NEXTMESSAGETYPE.FLEX;
                        case "3":
                            return NEXTMESSAGETYPE.CONFIRM;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "271":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.MESSAGE;
                        case "4":
                            return NEXTMESSAGETYPE.MESSAGE;
                        case "7":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "8":
                            return NEXTMESSAGETYPE.SHOPINFOSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "272":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "282":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "2":
                            return NEXTMESSAGETYPE.SHOPINFOSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "291":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.MESSAGE;
                        case "2":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "3":
                            return NEXTMESSAGETYPE.SHOPINFOSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "292":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.MESSAGE;
                        case "2":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "3":
                            return NEXTMESSAGETYPE.SHOPINFOSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "293":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.MESSAGE;
                        case "2":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "3":
                            return NEXTMESSAGETYPE.SHOPINFOSQLCOMMUNICATE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                case "294":
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.CAROUSEL;
                        case "2":
                            return NEXTMESSAGETYPE.MESSAGE;
                        case "3":
                            return NEXTMESSAGETYPE.MESSAGE;

                        case "5":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "6":
                            return NEXTMESSAGETYPE.MESSAGE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;
                case "9998": // 使用者訂購流程
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.FLEX;
                        case "2":
                            return NEXTMESSAGETYPE.MESSAGE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;
                case "9999": // 使用者訂購流程
                    switch (OID)
                    {
                        case "1":
                            return NEXTMESSAGETYPE.FLEX;
                        case "2":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "21":
                            return NEXTMESSAGETYPE.MESSAGE;
                        case "3":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "31":
                            return NEXTMESSAGETYPE.MESSAGE;
                        case "4":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "5":
                            return NEXTMESSAGETYPE.CONFIRM;
                        case "6":
                            return NEXTMESSAGETYPE.MESSAGE;
                        default:
                            break;
                    }
                    return NEXTMESSAGETYPE.ERROR;

                default:
                    return NEXTMESSAGETYPE.ERROR;
            }
        }
    }
}