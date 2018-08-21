using OrderBot.SQLObject;
using OrderBot.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderBot.TemplateMakers
{
    public class SQLCommunicator
    {
        // 參數postbackData是藏在button裡，經由postback傳進來的
        // 參加社團和退出社團需要clubID，建立社團需要ClubName
        internal static string CommunicateClubInfo(string userId, int QID, int OID, string postbackData, string channelAccessToken)
        {
            UserStatus userStatus;
            ClubInfo clubInfo;
            switch (QID)
            {
                case 91:
                    switch (OID)
                    {
                        case 3:
                            try
                            {
                                clubInfo = new ClubInfo(userId, postbackData, ""); // 這裡的postbackData是clubID
                                clubInfo.InsertClubInfoToSQL();
                                OrderInfo orderInfo = new OrderInfo(postbackData); //ClubID
                                List<OrderInfo> list = orderInfo.CheckClubOrderByUserID();
                                string CheckMessage = "成功加入社團";
                                foreach (OrderInfo item in list)
                                {
                                    if (item.OrderStatus == "available")
                                    {
                                        CheckMessage = "成功加入社團~~\n此社團有訂單正在推播中~~\n可以直接點選上面選項直接訂購喔~~";
                                     //   isRock.LineBot.Utility.PushMessage(userId, CheckMessage, channelAccessToken);
                                        OrderTemp orderTemp = new OrderTemp(userId);
                                        orderTemp.OrderPartitionID = item.OrderPartitionID;
                                        orderTemp.UpdateOrderPartitionIDByUserID();
                                        string JasonMessage = FlexMaker.MakeSearchingItem(QID, OID, userId);
                                        isRock.LineBot.Utility.PushMessagesWithJSON(userId, JasonMessage, channelAccessToken);
                                        userStatus = new UserStatus(userId, 0, 0);
                                        userStatus.UpdateByUserID();
                                    }
                                    //    return CheckMessage;
                                }

                                userStatus = new UserStatus(userId, 0, 0);
                                userStatus.UpdateByUserID();
                                return CheckMessage;
                            }
                            catch (Exception e)
                            {
                                return "加入社團失敗，或是已加入社團";
                            }

                        default:
                            break;
                    }
                    break;

                case 92:
                    switch (OID)
                    {
                        case 3:
                            clubInfo = new ClubInfo(userId, postbackData); // 這裡的postbackData是clubName
                            clubInfo.InsertClubInfoToSQL();
                            userStatus = new UserStatus(userId, 0, 0);
                            userStatus.UpdateByUserID();
                            isRock.LineBot.Utility.PushMessage(userId, clubInfo.ClubID, channelAccessToken);
                            return "你的社團已經建立嘍~~\n上方是你的社團參加碼~\n將此參加碼分享給要參加此社團的人~";
                        default:
                            break;
                    }
                    break;

                case 102:
                    switch (OID)
                    {
                        case 2:
                            clubInfo = new ClubInfo(userId, postbackData, "");
                            clubInfo.DeleteClubInfoToSQL();
                            userStatus = new UserStatus(userId, 0, 0);
                            userStatus.UpdateByUserID();
                            return MessageMaker.MakeLeaveClubMessage(userId, QID, OID, postbackData);

                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }
            return "資料庫溝通失敗";
        }

        internal static bool CommunicateOrderTmp(string userId, int QID, int OID, string seletedDateTimeStatus, string selecteddateTime, string channelAccessToken)
        {
            OrderTemp orderTmp = new OrderTemp(userId);
            DateTime dateTime = Convert.ToDateTime(selecteddateTime);
            bool BothTimeHasFilledIn = false;
            string message = "";
            if (DateTimeChecker.DateTimeCheckIsEarlierThanNow(dateTime))
            {
                message = "不要選擇過時的時間哦~";
            }
            else
            {
                switch (seletedDateTimeStatus)
                {
                    case "start":

                        //先判斷有沒有比暫存結束時間晚
                        if (!DateTimeChecker.CompareIsLaterThanEndTime(dateTime, orderTmp))
                        {
                            orderTmp.StartTime = dateTime;
                            orderTmp.UpdateStartTime();
                            BothTimeHasFilledIn = orderTmp.CheckBothTimeHasFilledIn(orderTmp);
                            if (!BothTimeHasFilledIn)
                            {
                                message = $"訂單發出時間: {dateTime.ToString("yyyy-MM-dd HH:mm")}";
                            }
                            else
                            {
                                isRock.LineBot.Utility.PushMessage(userId, $"訂單發出時間: {dateTime.ToString("yyyy-MM-dd HH:mm")}", channelAccessToken);
                                message = "選擇時間完畢!";
                            }
                        }
                        else
                        {
                            message = "開始時間無法比結束時間晚哦~";
                        }
                        break;

                    case "end":
                        //先判斷有沒有比暫存結束時間晚
                        if (!DateTimeChecker.CompareIsEarlierThanStartTime(dateTime, orderTmp))
                        {
                            orderTmp.EndTime = dateTime;
                            orderTmp.UpdateEndTime();
                            BothTimeHasFilledIn = orderTmp.CheckBothTimeHasFilledIn(orderTmp);
                            if (!BothTimeHasFilledIn)
                            {
                                message = $"訂單結束時間: {dateTime.ToString("yyyy-MM-dd HH:mm")}";
                            }
                            else
                            {
                                isRock.LineBot.Utility.PushMessage(userId, $"訂單結束時間: {dateTime.ToString("yyyy-MM-dd HH:mm")}", channelAccessToken);
                                message = "選擇時間完畢!";
                            }
                        }
                        else
                        {
                            message = "開始時間無法比結束時間晚哦~";
                        }
                        break;
                    default:
                        break;
                }
            }
            isRock.LineBot.Utility.PushMessage(userId, message, channelAccessToken);
            return BothTimeHasFilledIn;
        }

        internal static string CommunicateOrder(string userId, int QID, int OID, string OrderName, string channelAccessToken)
        {
            OrderTemp orderTmp = new OrderTemp(userId);
            orderTmp.SelectByUserID();//這裡主要從OrderTmp抓取起始時間跟結束時間,ShopID
            OrderInfo orderinfo = new OrderInfo();//準備輸入SQL的資料
            switch (QID)//根據QID不同初始化clubOrderAndMyOrder
            {
                case 201:
                    orderinfo = new OrderInfo(userId, OrderName);
                    break;
                case 231://231只修改shopID
                    orderinfo = new OrderInfo(userId);
                    orderTmp.SelectByUserID();
                    orderTmp.ShopID = OrderName;//在此處傳進來OrderName是ShopID
                    break;
                case 241:
                    orderTmp.ClubIDSelectByUserID();//撈取ClubID
                    orderinfo = new OrderInfo(orderTmp.ClubID, OrderName);
                    break;
                case 261:
                    orderTmp.ClubIDSelectByUserID();//撈取ClubID
                    orderinfo = new OrderInfo(orderTmp.ClubID);
                    orderTmp.SelectByUserID();
                    orderTmp.ShopID = OrderName;//在此處傳進來OrderName是ShopID
                    break;
                default:
                    break;
            }
            orderinfo.StartTime = orderTmp.StartTime;
            orderinfo.EndTime = orderTmp.EndTime;
            orderinfo.ShopID = orderTmp.ShopID;
            if (QID == 201 | QID == 241)
            {
                orderinfo.OrderType = "Once";
            }
            else
            {
                orderinfo.OrderType = "Period";
            }
            orderinfo.OrderStatus = "wait";

            int result = 0;
            switch (QID)
            {
                case 201:
                    orderinfo.OrderPartitionID = "MOO" + orderinfo.OrderID;
                    result = orderinfo.InserMyOrderTableToSQL();
                    break;
                case 231://修改MyOrder訂單店家
                    orderinfo.OrderPartitionID = orderTmp.OrderPartitionID;
                    result = orderinfo.UpdateShopIDByOrderPartitionIDToMyOrder();
                    if (result>0)
                    {
                        return "修改完成";
                    }
                    else
                    {
                        return "修改失敗";
                    }
                    break;
                case 241:
                    orderinfo.OrderPartitionID = "COO" + orderinfo.OrderID;
                    result = orderinfo.InserClubOrderTableToSQL();
                    if (result > 0)
                    {
                        UserStatus userStatus = new UserStatus(userId);
                        userStatus.InitializeUserStatusByUserID();
                        orderTmp.UpdateInitialOrderTemp();
                        orderTmp = new OrderTemp(userId);

                        return "社團訂單建立成功，所有社團成員皆能收到此訂單通知，無須再讓成員加入訂單喔~";
                    }
                    break;
                case 261:
                    orderinfo.OrderPartitionID = orderTmp.OrderPartitionID;
                    result = orderinfo.UpdateShopIDByOrderPartitionIDToClubOrder();
                    if (result > 0)
                    {
                        return "修改完成";
                    }
                    else
                    {
                        return "修改失敗";
                    }
                    break;
                default:
                    break;
            }
            if (result > 0)
            {
                UserStatus userStatus = new UserStatus(userId);
                userStatus.InitializeUserStatusByUserID();
                orderTmp = new OrderTemp(userId);
                orderinfo.InsertOrdeUserTable();
                orderTmp.UpdateInitialOrderTemp();

                isRock.LineBot.Utility.PushMessage(userId, orderinfo.OrderID, channelAccessToken);
                //   string orderCreateMessage = "上面是你的訂單參加碼，可以將此參加碼分享給其他人，讓其他人加入你的訂單喔~";
                //    isRock.LineBot.Utility.PushMessage(userId, orderCreateMessage, channelAccessToken);
                return "訂單建立成功喔~~\n上面是你的訂單參加碼，可以將此參加碼分享給其他人，讓其他人加入你的訂單喔~";

            }
            else
            {
                return "訂單建立失敗";
            }
        }

        internal static string CommunicateUserInfo(string userId, int QID, int OID, string postbackData, string channelAccessToken)
        {
            UserStatus userStatus;
            OrderInfo orderInfo;
            int result;
            switch (QID)
            {
                case 71:
                    switch (OID)
                    {
                        case 3:
                            // 先拿到UserStatus裡面的TempData，上一步存的是OrderID
                            userStatus = new UserStatus(userId);
                            userStatus.SelectByUserID();

                            // 把資料存進OrderUserTable裡
                            orderInfo = new OrderInfo(userId);
                            orderInfo.OrderID = userStatus.TempData;
                            result = orderInfo.InsertOrdeUserTable();

                            if (result > 0)
                            {

                                List<OrderInfo> list = orderInfo.SelectMyOrderByOrderID();
                                if (list[0].OrderStatus == "available")
                                {
                                    string CheckMessage = "成功參加訂單~~\n此訂單正在推播中~~\n可以直接點選上面選項直接訂購喔~~";
                                    //   isRock.LineBot.Utility.PushMessage(userId, CheckMessage, channelAccessToken);
                                    OrderTemp orderTemp = new OrderTemp(userId);
                                    orderTemp.OrderPartitionID = list[0].OrderPartitionID;
                                    orderTemp.UpdateOrderPartitionIDByUserID();
                                    string JasonMessage = FlexMaker.MakeSearchingItem(QID, OID, userId);
                                    isRock.LineBot.Utility.PushMessagesWithJSON(userId, JasonMessage, channelAccessToken);
                                    return CheckMessage;

                                }
                                else
                                {
                                    return "成功參加訂單";
                                }
                            }
                            else
                            {
                                return "參加訂單失敗";
                            }

                        default:
                            break;
                    }
                    break;

                case 82:
                    switch (OID)
                    {
                        case 2:
                            OrderTemp orderTemp = new OrderTemp(userId);
                            orderTemp.SelectByUserID();

                            orderInfo = new OrderInfo(userId);
                            orderInfo.OrderPartitionID = orderTemp.OrderPartitionID;
                            orderInfo.SelectMyOrderTableByOrderPartitionID();

                            switch (postbackData)
                            {
                                case "MyOrderPartition": // 刪除我的週期性訂單的其中一筆
                                    result = orderInfo.DeleteMyOrderPartitionByUserIDandOrderPartitionID();
                                    if (result > 0)
                                    {
                                        return "成功刪除訂單";
                                    }
                                    else
                                    {
                                        return "刪除訂單失敗";
                                    }

                                case "MyOrder": // 刪除完整我的週期性訂單
                                    result = orderInfo.DeleteMyOrderByUserIDandOrderID();
                                    if (result > 0)
                                    {
                                        return "成功刪除訂單";
                                    }
                                    else
                                    {
                                        return "刪除訂單失敗";
                                    }

                                case "MyOnceOrder": // 刪除一次性我的訂單
                                    result = orderInfo.DeleteMyOrderByUserIDandOrderID();
                                    if (result > 0)
                                    {
                                        return "成功刪除訂單";
                                    }
                                    else
                                    {
                                        return "刪除訂單失敗";
                                    }
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
            return "資料庫溝通失敗";
        }




        internal static string CommunicateMyShop(string UserID, int QID, int OID, string channelAccessToken)
        {
            // 取得ShopTemp裡的資料：ShopName, ShopPhone, ShopAddress
            ShopTemp shopTemp = new ShopTemp(UserID);
            shopTemp.GetMyShopTempInfoFromSQL();

            // 先用時間和UserID製作ShopID，因為是MyShop所以前面加上MS
            shopTemp.ShopID = $"MS{DateTime.Now.ToString("yyyyMMddHHmmssfff")}{UserID}";

            // 取得ShopItemTemp裡的資料：ShopItem, ShopItemPrice
            List<ShopTemp> shopItemList = shopTemp.GetShopItemTempFromSQL();

            // 把資料存進各個資料表
            int result = 0;
            result = shopTemp.InsertAllMyShopData(shopItemList);
            if (result > 0)
            {
                shopTemp.InitializeShopTempByUserID();
                shopTemp.DeleteShopItemTempByUserID();

                UserStatus userStatus = new UserStatus(UserID);
                userStatus.InitializeUserStatusByUserID();
                return "商店建立成功";
            }
            else
            {
                return "商店建立失敗";
            }
        }

        internal static string CommunicateClubShop(string UserID, int QID, int OID, string channelAccessToken)
        {
            // 取得ShopTemp裡的資料：ShopName, ShopPhone, ShopAddress, ClubID
            ShopTemp shopTemp = new ShopTemp(UserID);
            shopTemp.GetClubShopTempInfoFromSQL();

            // 先用時間和UserID製作ShopID，因為是ClubShop所以前面加上CS
            shopTemp.ShopID = $"CS{DateTime.Now.ToString("yyyyMMddHHmmssfff")}{shopTemp.ClubID}";

            // 取得ShopItemTemp裡的資料：ShopItem, ShopItemPrice
            List<ShopTemp> shopItemList = shopTemp.GetShopItemTempFromSQL();

            // 把資料存進各個資料表
            int result = 0;
            result = shopTemp.InsertAllClubShopData(shopItemList);
            if (result > 0)
            {
                shopTemp.InitializeShopTempByUserID();
                shopTemp.DeleteShopItemTempByUserID();

                UserStatus userStatus = new UserStatus(UserID);
                userStatus.InitializeUserStatusByUserID();
                return "商店建立成功";
            }
            else
            {
                return "商店建立失敗";
            }
        }

        internal static string CommunicateShopNameUpdate(string UserID, int QID, int OID, string channelAccessToken)
        {
            // 先拿到存在ShopTemp裡面的ShopName
            ShopTemp shopTemp = new ShopTemp(UserID);
            shopTemp.SelectByUserID();

            // 拿到存在UserStatus裡的TempData，目前裡面是前一步輸入的ShopID
            UserStatus userStatus = new UserStatus(UserID);
            userStatus.SelectByUserID();

            ShopInfo shopInfo = new ShopInfo();
            int result = 0;

            // 分成MyShop跟ClubShop兩種update方法
            switch (QID)
            {
                case 191:
                    shopInfo.ShopName = shopTemp.ShopName;
                    result = shopInfo.UpdateMyShopNameByShopID(shopTemp.ShopID);
                    break;

                case 291:
                    shopInfo.ShopName = shopTemp.ShopName;
                    result = shopInfo.UpdateClubShopNameByShopID(userStatus.TempData);
                    break;

                default:
                    break;
            }

            if (result > 0)
            {
                userStatus.InitializeUserStatusByUserID();
                shopTemp.InitializeShopTempByUserID();
                return "商店名稱修改成功";
            }
            else
            {
                return "商店名稱修改失敗";
            }
        }

        internal static string CommunicateShopPhoneUpdate(string UserID, int QID, int OID, string channelAccessToken)
        {
            // 先拿到存在ShopTemp裡面的ShopPhone
            ShopTemp shopTemp = new ShopTemp(UserID);
            shopTemp.SelectByUserID();

            // 拿到存在UserStatus裡的TempData，目前裡面是前一步輸入的ShopID
            UserStatus userStatus = new UserStatus(UserID);
            userStatus.SelectByUserID();

            ShopInfo shopInfo = new ShopInfo();
            int result = 0;

            // 分成MyShop跟ClubShop兩種update方法
            switch (QID)
            {
                case 192:
                    shopInfo.ShopPhone = shopTemp.ShopPhone;
                    result = shopInfo.UpdateMyShopPhoneByShopID(shopTemp.ShopID);
                    break;

                case 292:
                    shopInfo.ShopPhone = shopTemp.ShopPhone;
                    result = shopInfo.UpdateClubShopPhoneByShopID(userStatus.TempData);
                    break;

                default:
                    break;
            }

            if (result > 0)
            {
                userStatus.InitializeUserStatusByUserID();
                shopTemp.InitializeShopTempByUserID();
                return "商店電話修改成功";
            }
            else
            {
                return "商店電話修改失敗";
            }
        }

        internal static string CommunicateShopAddressUpdate(string UserID, int QID, int OID, string channelAccessToken)
        {
            // 先拿到存在ShopTemp裡面的ShopPhone
            ShopTemp shopTemp = new ShopTemp(UserID);
            shopTemp.SelectByUserID();

            // 拿到存在UserStatus裡的TempData，目前裡面是前一步輸入的ShopID
            UserStatus userStatus = new UserStatus(UserID);
            userStatus.SelectByUserID();

            ShopInfo shopInfo = new ShopInfo();
            int result = 0;

            // 分成MyShop跟ClubShop兩種update方法
            switch (QID)
            {
                case 193:
                    shopInfo.ShopAddress = shopTemp.ShopAddress;
                    result = shopInfo.UpdateMyShopAddressByShopID(shopTemp.ShopID);
                    break;

                case 293:
                    shopInfo.ShopAddress = shopTemp.ShopAddress;
                    result = shopInfo.UpdateClubShopAddressByShopID(userStatus.TempData);
                    break;

                default:
                    break;
            }

            if (result > 0)
            {
                userStatus.InitializeUserStatusByUserID();
                shopTemp.InitializeShopTempByUserID();
                return "商店地址修改成功";
            }
            else
            {
                return "商店地址修改失敗";
            }
        }


        internal static string CommunicateDeleteShop(string UserID, int QID, int OID, string channelAccessToken)
        {
            // 先從ShopTemp取得ClubID和ShopID
            ShopTemp shopTemp = new ShopTemp(UserID);
            shopTemp.SelectByUserID();

            ShopInfo shopInfo;
            UserStatus userStatus = new UserStatus(UserID);
            int result = 0;

            // 分成MyShop跟ClubShop兩種delete方法
            switch (QID)
            {
                case 182:
                    shopInfo = new ShopInfo(UserID);
                    result = shopInfo.DeleteMyShopByUserIDandShopID(shopTemp.ShopID);
                    break;

                case 282:
                    shopInfo = new ShopInfo(shopTemp.ClubID);
                    result = shopInfo.DeleteClubShopByClubIDandShopID(shopTemp.ShopID);
                    break;
                default:
                    break;
            }
            if (result > 0)
            {
                userStatus.InitializeUserStatusByUserID();
                shopTemp.InitializeShopTempByUserID();
                return "商店刪除成功";
            }
            else
            {
                return "商店刪除失敗";
            }
        }



        internal static string MyPeriodOrderCommunicate(string userId, int QID, int OID, string OrderName, string channelAccessToken)
        {
            PeriodOrderTmp periodOrderTmp = new PeriodOrderTmp(userId);
            periodOrderTmp.SelectAllByUserID();
            OrderInfo orderinfo = new OrderInfo();//準備輸入SQL
            switch (QID)//根據QID不同初始化clubOrderAndMyOrder
            {
                case 211:
                    orderinfo = new OrderInfo(userId, OrderName);
                    break;

                default:
                    break;
            }


            //切割週期時間
            DateTime st1 = periodOrderTmp.StartDate.Add(periodOrderTmp.StartTime);//第一次開單時間點
            DateTime et1 = periodOrderTmp.EndDate.Add(periodOrderTmp.StartTime);//最後一次開單時間點
            DateTime st2 = periodOrderTmp.StartDate.Add(periodOrderTmp.EndTime);//第一次收單時間點
            DateTime et2 = periodOrderTmp.EndDate.Add(periodOrderTmp.EndTime);//最後一次收單時間點

            if (DateTimeChecker.DateTimeCheckIsEarlierThanNow(st1))//週期單開始時間點若是比現在時間點早則往後一天
            {
                st1 = st1.AddDays(1);
                st2 = st2.AddDays(1);
            }


            List<string> weekList = new List<string>();
            if (periodOrderTmp.Monday == "Y")
            {
                weekList.Add("Monday");
            }
            if (periodOrderTmp.Tuesday == "Y")
            {
                weekList.Add("Tuesday");
            }
            if (periodOrderTmp.Wednesday == "Y")
            {
                weekList.Add("Wednesday");
            }
            if (periodOrderTmp.Thursday == "Y")
            {
                weekList.Add("Thursday");
            }
            if (periodOrderTmp.Friday == "Y")
            {
                weekList.Add("Friday");
            }
            if (periodOrderTmp.Saturday == "Y")
            {
                weekList.Add("Saturday");
            }
            if (periodOrderTmp.Sunday == "Y")
            {
                weekList.Add("Sunday");
            }
            List<DateTime> dateTimes = PeriodSplitter.Cut(st1, et1, weekList);//得到每次的開單時間點
            List<DateTime> dateTimes2 = PeriodSplitter.Cut(st2, et2, weekList);//得到每次的收單時間點
                                                                               //
            orderinfo.OrderType = "Period";
            orderinfo.OrderStatus = "wait";

            //開始撈取使用者選擇的複數商店
            ShopListTemp shopListTemp = new ShopListTemp(userId);
            List<ShopListTemp> shopListTemps = shopListTemp.SelectByUserID();//選取的商店清單
            List<ShopListTemp> randomshopListTemps = new List<ShopListTemp>();//準備一個要裝進符合選取天數的清單

            //將randomshopListTemps打亂
            while (randomshopListTemps.Count <= dateTimes.Count)
            {
                for (int i = 0; i < shopListTemps.Count; i++)
                {
                    Swap(shopListTemps);
                }
                for (int i = 0; i < shopListTemps.Count; i++)
                {
                    randomshopListTemps.Add(shopListTemps[i]);
                }
            }
            //

            int result = 0;
            int count = 0; //用來檢查迴圈有沒有跑完
            string OrderID;
            switch (QID)
            {
                case 211:
                    OrderID = orderinfo.OrderID;
                    for (int i = 0; i < dateTimes.Count; i++)
                    {
                        orderinfo.StartTime = dateTimes[i];
                        orderinfo.EndTime = dateTimes2[i];
                        orderinfo.ShopID = randomshopListTemps[i].ShopID;
                        orderinfo.OrderPartitionID = "MOP" + OrderID + $"-{ dateTimes[i].ToString("yyyyMMdd")}";
                        result = orderinfo.InserMyOrderTableToSQL();
                        if (result <= 0)
                        {
                            break;
                        }
                        count++;
                    }

                    break;

                default:
                    break;
            }
            if (count == dateTimes.Count)
            {
                UserStatus userStatus = new UserStatus(userId);
                userStatus.InitializeUserStatusByUserID();
                periodOrderTmp = new PeriodOrderTmp(userId);
                periodOrderTmp.UpdateInitialPeriodOrderTmp();
                orderinfo.InsertOrdeUserTable();
                shopListTemp.DeleteByUserID();

                isRock.LineBot.Utility.PushMessage(userId, orderinfo.OrderID, channelAccessToken);
                // string orderCreateMessage = "上面是你的訂單參加碼，可以將此參加碼分享給其他人，讓其他人加入你的訂單喔~";
                // isRock.LineBot.Utility.PushMessage(userId, orderCreateMessage, channelAccessToken);

                return "訂單建立成功喔~~\n上面是你的訂單參加碼，可以將此參加碼分享給其他人，讓其他人加入你的訂單喔~";
            }
            else
            {
                return "訂單建立失敗";
            }
        }


        internal static string ClubPeriodOrderCommunicate(string userId, int QID, int OID, string OrderName, string channelAccessToken)
        {
            PeriodOrderTmp periodOrderTmp = new PeriodOrderTmp(userId);
            periodOrderTmp.SelectAllByUserID();
            OrderInfo orderinfo = new OrderInfo();//準備輸入SQL
            switch (QID)//根據QID不同初始化clubOrderAndMyOrder
            {
                case 251:
                    periodOrderTmp.ClubIDSelectByUserID();//撈取ClubID
                    orderinfo = new OrderInfo(periodOrderTmp.ClubID, OrderName);
                    break;

                default:
                    break;
            }

            //切割週期時間
            DateTime st1 = periodOrderTmp.StartDate.Add(periodOrderTmp.StartTime);//第一次開單時間點
            DateTime et1 = periodOrderTmp.EndDate.Add(periodOrderTmp.StartTime);//最後一次開單時間點
            DateTime st2 = periodOrderTmp.StartDate.Add(periodOrderTmp.EndTime);//第一次收單時間點
            DateTime et2 = periodOrderTmp.EndDate.Add(periodOrderTmp.EndTime);//最後一次收單時間點
            List<string> weekList = new List<string>();
            if (periodOrderTmp.Monday == "Y")
            {
                weekList.Add("Monday");
            }
            if (periodOrderTmp.Tuesday == "Y")
            {
                weekList.Add("Tuesday");
            }
            if (periodOrderTmp.Wednesday == "Y")
            {
                weekList.Add("Wednesday");
            }
            if (periodOrderTmp.Thursday == "Y")
            {
                weekList.Add("Thursday");
            }
            if (periodOrderTmp.Friday == "Y")
            {
                weekList.Add("Friday");
            }
            if (periodOrderTmp.Saturday == "Y")
            {
                weekList.Add("Saturday");
            }
            if (periodOrderTmp.Sunday == "Y")
            {
                weekList.Add("Sunday");
            }
            List<DateTime> dateTimes = PeriodSplitter.Cut(st1, et1, weekList);//得到每次的開單時間點
            List<DateTime> dateTimes2 = PeriodSplitter.Cut(st2, et2, weekList);//得到每次的收單時間點

            orderinfo.OrderType = "Period";
            orderinfo.OrderStatus = "wait";


            //開始撈取使用者選擇的複數商店
            ShopListTemp shopListTemp = new ShopListTemp(userId);
            List<ShopListTemp> shopListTemps = shopListTemp.SelectByUserID();//選取的商店清單
            List<ShopListTemp> randomshopListTemps = new List<ShopListTemp>();//準備一個要裝進符合選取天數的清單

            //將randomshopListTemps打亂
            while (randomshopListTemps.Count <= dateTimes.Count)
            {
                for (int i = 0; i < shopListTemps.Count; i++)
                {
                    Swap(shopListTemps);
                }
                for (int i = 0; i < shopListTemps.Count; i++)
                {
                    randomshopListTemps.Add(shopListTemps[i]);
                }
            }

            int result = 0;
            int count = 0; //用來檢查迴圈有沒有跑完
            string clubOrderAndMyOrderID;
            switch (QID)
            {
                case 251:
                    clubOrderAndMyOrderID = orderinfo.OrderID;
                    for (int i = 0; i < dateTimes.Count; i++)
                    {
                        orderinfo.StartTime = dateTimes[i];
                        orderinfo.EndTime = dateTimes2[i];
                        orderinfo.ShopID = randomshopListTemps[i].ShopID;
                        orderinfo.OrderPartitionID = "COP" + clubOrderAndMyOrderID + $"-{ dateTimes[i].ToString("yyyyMMdd")}";
                        result = orderinfo.InserClubOrderTableToSQL();
                        if (result <= 0)
                        {
                            break;
                        }
                        count++;
                    }

                    break;
                default:
                    break;
            }
            if (count == dateTimes.Count)
            {
                UserStatus userStatus = new UserStatus(userId);
                userStatus.InitializeUserStatusByUserID();
                periodOrderTmp = new PeriodOrderTmp(userId);
                periodOrderTmp.UpdateInitialPeriodOrderTmp();
                shopListTemp.DeleteByUserID();

                return "訂單建立成功";
            }
            else
            {
                return "訂單建立失敗";
            }
        }


        private static void Swap(List<ShopListTemp> shopListTemps)
        {
            Random random = new Random();
            for (int i = 0; i < shopListTemps.Count; i++)
            {
                int change = random.Next(i, shopListTemps.Count);
                ShopListTemp tmp = new ShopListTemp();
                tmp = shopListTemps[i];
                shopListTemps[i] = shopListTemps[change];
                shopListTemps[change] = tmp;
            }
        }


        internal static bool CommunicatePeriodOrderTmpForDate(string userId, int QID, int OID, string seletedDateTimeStatus, string selecteddateTime, string channelAccessToken)
        {
            PeriodOrderTmp periodOrderTmp = new PeriodOrderTmp(userId);
            DateTime dateTime = Convert.ToDateTime(selecteddateTime);
            bool BothTimeHasFilledIn = false;
            string message = "";
            if (DateTimeChecker.DateTimeCheckIsEarlierThanNowForPeriod(dateTime))
            {
                message = "不要選擇過時的時間哦~";
            }

            else
            {
                switch (seletedDateTimeStatus)
                {
                    case "start":

                        //先判斷有沒有比暫存結束時間晚
                        if (!DateTimeChecker.DateTimeCompareIsLaterThanEndTime(dateTime, periodOrderTmp))
                        {
                            periodOrderTmp.StartDate = dateTime;
                            periodOrderTmp.UpdateStartDate();
                            BothTimeHasFilledIn = periodOrderTmp.CheckBothDateHasFilledIn(periodOrderTmp);
                            if (!BothTimeHasFilledIn)
                            {
                                message = $"週期開始日期: {dateTime.ToShortDateString()}";
                            }
                            else
                            {
                                isRock.LineBot.Utility.PushMessage(userId, $"週期開始日期: {dateTime.ToShortDateString()}", channelAccessToken);
                                message = "選擇時間完畢!";
                            }
                        }
                        else
                        {
                            message = "開始時間無法比結束時間晚~";
                        }
                        break;

                    case "end":
                        //先判斷有沒有比暫存結束時間晚
                        if (!DateTimeChecker.DateTimeCompareIsEarlierThanStartTime(dateTime, periodOrderTmp))
                        {
                            periodOrderTmp.EndDate = dateTime;
                            periodOrderTmp.UpdateEndDate();
                            BothTimeHasFilledIn = periodOrderTmp.CheckBothDateHasFilledIn(periodOrderTmp);
                            if (!BothTimeHasFilledIn)
                            {
                                message = $"週期結束日期: {dateTime.ToShortDateString()}";
                            }
                            else
                            {
                                isRock.LineBot.Utility.PushMessage(userId, $"週期結束日期: {dateTime.ToShortDateString()}", channelAccessToken);
                                message = "選擇時間完畢!";
                            }
                        }
                        else
                        {
                            message = "開始時間無法比結束時間晚哦~";
                        }
                        break;
                    default:
                        break;
                }
            }
            isRock.LineBot.Utility.PushMessage(userId, message, channelAccessToken);
            return BothTimeHasFilledIn;
        }

        internal static string CheckPeriodOrderTmpWeek(string userId, int QID, int OID, string selectWeekofDay)
        {
            PeriodOrderTmp periodOrderTmp = new PeriodOrderTmp(userId);
            periodOrderTmp.WeekofDaySelectByUserID();
            switch (selectWeekofDay)
            {
                case "1":
                    switch (periodOrderTmp.Monday)
                    {
                        case "Y":
                            periodOrderTmp.UpdateWeekofDayToNo(selectWeekofDay);
                            return "取消選擇了星期一";
                        case "N":
                            periodOrderTmp.UpdateWeekofDayToYes(selectWeekofDay);
                            return "選擇了星期一";

                        default:
                            break;
                    }
                    break;
                case "2":
                    switch (periodOrderTmp.Tuesday)
                    {
                        case "Y":
                            periodOrderTmp.UpdateWeekofDayToNo(selectWeekofDay);
                            return "取消選擇了星期二";
                        case "N":
                            periodOrderTmp.UpdateWeekofDayToYes(selectWeekofDay);
                            return "選擇了星期二";
                        default:
                            break;
                    }
                    break;
                case "3":
                    switch (periodOrderTmp.Wednesday)
                    {
                        case "Y":
                            periodOrderTmp.UpdateWeekofDayToNo(selectWeekofDay);
                            return "取消選擇了星期三";
                        case "N":
                            periodOrderTmp.UpdateWeekofDayToYes(selectWeekofDay);
                            return "選擇了星期三";
                        default:

                            break;
                    }
                    break;
                case "4":
                    switch (periodOrderTmp.Thursday)
                    {
                        case "Y":
                            periodOrderTmp.UpdateWeekofDayToNo(selectWeekofDay);
                            return "取消選擇了星期四";

                        case "N":
                            periodOrderTmp.UpdateWeekofDayToYes(selectWeekofDay);
                            return "選擇了星期四";

                        default:
                            break;
                    }
                    break;
                case "5":
                    switch (periodOrderTmp.Friday)
                    {
                        case "Y":
                            periodOrderTmp.UpdateWeekofDayToNo(selectWeekofDay);
                            return "取消選擇了星期五";

                        case "N":
                            periodOrderTmp.UpdateWeekofDayToYes(selectWeekofDay);
                            return "選擇了星期五";
                        default:
                            break;
                    }
                    break;
                case "6":
                    switch (periodOrderTmp.Saturday)
                    {
                        case "Y":
                            periodOrderTmp.UpdateWeekofDayToNo(selectWeekofDay);
                            return "取消選擇了星期六";

                        case "N":
                            periodOrderTmp.UpdateWeekofDayToYes(selectWeekofDay);
                            return "選擇了星期六";

                        default:
                            break;
                    }
                    break;
                case "7":
                    switch (periodOrderTmp.Sunday)
                    {
                        case "Y":
                            periodOrderTmp.UpdateWeekofDayToNo(selectWeekofDay);
                            return "取消選擇了星期日";

                        case "N":
                            periodOrderTmp.UpdateWeekofDayToYes(selectWeekofDay);
                            return "選擇了星期日";

                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            return "系統錯誤";
        }

        internal static bool CommunicatePeriodOrderTmpTime(string userId, int QID, int OID, string seletedDateTimeStatus, string selecteddateTime, string channelAccessToken)
        {
            PeriodOrderTmp periodOrderTmp = new PeriodOrderTmp(userId);
            TimeSpan dateTime = TimeSpan.Parse(selecteddateTime);
            bool BothTimeHasFilledIn = false;
            string message = "";

            switch (seletedDateTimeStatus)
            {
                case "start":

                    //先判斷有沒有比暫存結束時間晚
                    if (!DateTimeChecker.TimeCompareIsLaterThanEndTime(dateTime, periodOrderTmp))
                    {
                        periodOrderTmp.StartTime = dateTime;
                        periodOrderTmp.UpdateStartTime();
                        BothTimeHasFilledIn = periodOrderTmp.CheckBothTimeHasFilledIn(periodOrderTmp);
                        if (!BothTimeHasFilledIn)
                        {
                            message = $"每次開單時間: {dateTime.ToString(@"hh\:mm")}";
                        }
                        else
                        {
                            isRock.LineBot.Utility.PushMessage(userId, $"每次開單時間: {dateTime.ToString(@"hh\:mm")}", channelAccessToken);
                            message = "選擇時間完畢!";
                        }
                    }
                    else
                    {
                        message = "開始時間無法比結束時間晚~";
                    }
                    break;

                case "end":
                    //先判斷有沒有比暫存結束時間晚
                    if (!DateTimeChecker.TimeCompareIsEarlierThanStartTime(dateTime, periodOrderTmp))
                    {
                        periodOrderTmp.EndTime = dateTime;
                        periodOrderTmp.UpdateEndTime();
                        BothTimeHasFilledIn = periodOrderTmp.CheckBothTimeHasFilledIn(periodOrderTmp);
                        if (!BothTimeHasFilledIn)
                        {
                            message = $"每次結單時間: {dateTime.ToString(@"hh\:mm")}";
                        }
                        else
                        {
                            isRock.LineBot.Utility.PushMessage(userId, $"每次結單時間: {dateTime.ToString(@"hh\:mm")}", channelAccessToken);
                            message = "選擇時間完畢!";
                        }
                    }
                    else
                    {
                        message = "開單時間無法比結單時間晚哦~";
                    }
                    break;
                default:
                    break;
            }
            isRock.LineBot.Utility.PushMessage(userId, message, channelAccessToken);
            return BothTimeHasFilledIn;
        }

        internal static string UpdateOrder(int QID, int OID, string userId)
        {
            int result = 0;
            OrderTemp orderTemp;
            OrderInfo orderInfo;
            switch (QID)
            {
                case 61:
                    orderTemp = new OrderTemp(userId);
                    orderTemp.SelectByUserID();
                    orderInfo = new OrderInfo(userId);
                    orderInfo.OrderPartitionID = orderTemp.OrderPartitionID;
                    orderInfo.SelectMyOrderTableByOrderPartitionID();
                    orderInfo.StartTime = orderTemp.StartTime;
                    orderInfo.EndTime = orderTemp.EndTime;
                    switch (orderInfo.OrderStatus)
                    {
                        case "wait":
                            orderInfo.OrderStatus = "wait";
                            result = orderInfo.UpdateTimeByOrderPartitionIDToMyOrder();

                            break;
                        case "available":
                            orderInfo.OrderStatus = "wait";
                            result = orderInfo.UpdateTimeByOrderPartitionIDToMyOrder();

                            break;
                        case "done":
                            return "此訂單已經結單，無法再改時間囉~";
                            break;
                        default:
                            break;
                    }
                  //  result = orderInfo.UpdateTimeByOrderPartitionIDToMyOrder();
                    break;
                case 161:
                    orderTemp = new OrderTemp(userId);
                    orderTemp.ClubIDSelectByUserID();
                    orderTemp.SelectByUserID();

                    orderInfo = new OrderInfo(orderTemp.ClubID);
                    orderInfo.OrderPartitionID = orderTemp.OrderPartitionID;
                    orderInfo.SelectClubOrderTableByOrderPartitionID();

                    orderInfo.StartTime = orderTemp.StartTime;
                    orderInfo.EndTime = orderTemp.EndTime;
                    switch (orderInfo.OrderStatus)
                    {
                        case "wait":
                            orderInfo.OrderStatus = "wait";
                            result = orderInfo.UpdateTimeByOrderPartitionIDTOClubOrder();

                            break;
                        case "available":
                            orderInfo.OrderStatus = "wait";
                            result = orderInfo.UpdateTimeByOrderPartitionIDTOClubOrder();

                            break;
                        case "done":
                            return "此訂單已經結單，無法再改時間囉~";
                            break;
                        default:
                            break;
                    }

            //        result = orderInfo.UpdateTimeByOrderPartitionIDTOClubOrder();
                    break;
                default:
                    break;
            }
            if (result > 0)
            {
                orderTemp = new OrderTemp(userId);
                orderTemp.UpdateInitialOrderTemp();
                return "修改成功";
            }
            else
            {
                return "修改失敗";

            }
        }

        internal static string DeletOrder(int QID, int OID, string OrderID, string userId)
        {
            int result = 0;
            OrderInfo orderInfo;
            switch (QID)
            {
                case 52:
                    orderInfo = new OrderInfo();
                    orderInfo.OrderID = OrderID;
                    result = orderInfo.DeleteMyOrderByOrderID();
                    break;
                case 152:
                    orderInfo = new OrderInfo();
                    orderInfo.OrderID = OrderID;
                    result = orderInfo.DeleteClubOrderByOrderID();
                    break;
                default:
                    break;
            }

            if (result > 0)
            {
                return "已經刪除訂單";
            }
            else
            {
                return "刪除訂單失敗";
            }
        }

        internal static string DeletBuyerInfoItem(int QIDnow, int OIDnow, int BuyerInfoID, string userId)
        {
            BuyerInfo buyerInfo = new BuyerInfo();
            buyerInfo.ID = BuyerInfoID;
            int result = buyerInfo.DeletByBuyerInfoID();

            if (result>0)
            {
                return "刪除成功";
            }
            else
            {
                return "刪除失敗";

            }
        }
    }
}