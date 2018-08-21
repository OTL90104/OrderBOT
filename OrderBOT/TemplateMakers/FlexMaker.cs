using Newtonsoft.Json;
using OrderBot.Models;
using OrderBot.RecoginzeManager;
using OrderBot.SQLObject;
using OrderBot.Utility;
using PushChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static OrderBot.Models.WeekFlex;
using static PushChecker.Models.FlexReportToShop;

namespace OrderBot.TemplateMakers
{
    public class FlexMaker
    {
        internal static string MakeShopItem(string UserID, int QID, int OID, string shopID)
        {
            List<ShopInfo> shopInfos = new List<ShopInfo>();
            List<ShopItem> shopItems = new List<ShopItem>(); ;
            ShopInfo shopInfo = new ShopInfo();
            ShopItem shopItem;
            shopInfo.ShopID = shopID;
            switch (QID)
            {
                case 201:
                    shopInfos = shopInfo.MyShopSelectShopNameByShopid();
                    shopItem = new ShopItem(shopID);
                    shopItems = shopItem.SelectByMyShopID();
                    break;
                case 202:
                    shopInfos = shopInfo.BossShopSelectShopNameByShopid();
                    shopItem = new ShopItem(shopID);
                    shopItems = shopItem.SelectByBossShopID();
                    break;
                case 211:
                    shopInfos = shopInfo.MyShopSelectShopNameByShopid();
                    shopItem = new ShopItem(shopID);
                    shopItems = shopItem.SelectByMyShopID();
                    break;
                case 212:
                    shopInfos = shopInfo.BossShopSelectShopNameByShopid();
                    shopItem = new ShopItem(shopID);
                    shopItems = shopItem.SelectByBossShopID();
                    break;
                case 231:
                    shopInfos = shopInfo.MyShopSelectShopNameByShopid();
                    shopItem = new ShopItem(shopID);
                    shopItems = shopItem.SelectByMyShopID();
                    break;
                case 232:
                    shopInfos = shopInfo.BossShopSelectShopNameByShopid();
                    shopItem = new ShopItem(shopID);
                    shopItems = shopItem.SelectByBossShopID();
                    break;
                case 241:
                    shopInfos = shopInfo.ClubShopSelectShopNameByShopid();
                    shopItem = new ShopItem(shopID);
                    shopItems = shopItem.SelectByClubShopID();
                    break;
                case 242:
                    shopInfos = shopInfo.BossShopSelectShopNameByShopid();
                    shopItem = new ShopItem(shopID);
                    shopItems = shopItem.SelectByBossShopID();
                    break;
                case 251:
                    shopInfos = shopInfo.ClubShopSelectShopNameByShopid();
                    shopItem = new ShopItem(shopID);
                    shopItems = shopItem.SelectByClubShopID();
                    break;
                case 252:
                    shopInfos = shopInfo.BossShopSelectShopNameByShopid();
                    shopItem = new ShopItem(shopID);
                    shopItems = shopItem.SelectByBossShopID();
                    break;

                case 261:
                    shopInfos = shopInfo.ClubShopSelectShopNameByShopid();
                    shopItem = new ShopItem(shopID);
                    shopItems = shopItem.SelectByClubShopID();
                    break;
                case 262:
                    shopInfos = shopInfo.BossShopSelectShopNameByShopid();
                    shopItem = new ShopItem(shopID);
                    shopItems = shopItem.SelectByBossShopID();
                    break;
                default:
                    break;
            }




            FlexBublbe flexBublbe = new FlexBublbe();
            flexBublbe.contents.contents[0].body.contents[1].text = shopInfos[0].ShopName;


            flexBublbe.contents.contents[0].body.contents[3].contents = new List<FlexBublbe.Content>();
            FlexBublbe.Content content = new FlexBublbe.Content();



            foreach (ShopItem shopItemm in shopItems)
            {
                content = new FlexBublbe.Content();
                content.type = "box";
                content.layout = "horizontal";
                //flexBublbe.contents.contents[0].body.contents[3].contents.Add(content);
                ShopItemText shopItemText = new ShopItemText();
                shopItemText.type = "text";
                shopItemText.text = shopItemm.shopItem;
                shopItemText.size = "md";
                shopItemText.color = "#111111";
                content.contents.Add(shopItemText);
                shopItemText = new ShopItemText();
                shopItemText.type = "text";
                shopItemText.text =   shopItemm.ShopItemPrice.ToString("0.##")+ "  TWD";
                shopItemText.size = "md";
                shopItemText.color = "#111111";
                shopItemText.align = "end";
                content.contents.Add(shopItemText);
                flexBublbe.contents.contents[0].body.contents[3].contents.Add(content);

            }

            flexBublbe.contents.contents[0].body.contents.Add(new PartitionLine());

            //底下Btn
            FooterBtn footerBtn = new FooterBtn();
            NextHelper nextHelper = new NextHelper(QID, OID);
            QuestionDetail questionDetail = nextHelper.GetNext();
            footerBtn.contents[0].action.data = $"{DateTime.Now},{questionDetail.QID},{questionDetail.OID},{shopID}";
            PreviousHelper previousHelper = new PreviousHelper(QID, OID);
            questionDetail = previousHelper.GetPrevious();
            footerBtn.contents[1].action.data = $"{DateTime.Now},{questionDetail.QID},{questionDetail.OID},{shopID}";
            flexBublbe.contents.contents[0].body.contents.Add(footerBtn);



            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var json = JsonConvert.SerializeObject(flexBublbe, settings);
            //  string json = JsonConvert.SerializeObject(flexBublbe);

            return "[" + json + "]";
        }

        internal static string MyOrderMakeWeekFlex(string UserID, int QIDnow, int OIDnow, string shopID)
        {

            WeekFlex weekFlex = new WeekFlex();
            weekFlex.contents.contents[0].body.contents[1].text = "選擇固定星期 (可複選)";

            for (int i = 0; i < 7; i++)
            {
                weekFlex.contents.contents[0].body.contents[3].contents[i].action.data = $"{DateTime.Now},42,21,{i + 1}";
            }
            weekFlex.contents.contents[0].body.contents[3].contents[7].action.data = $"{DateTime.Now},42,3,";

            PeriodOrderTmp periodOrderTmp = new PeriodOrderTmp(UserID);//此處開始檢查按鈕有沒有被按下
            periodOrderTmp.SelectAllByUserID();

            string selectColor = "#4b607c";//在此處更改已選擇按鈕顏色

            if (periodOrderTmp.Monday == "Y")
            {
                weekFlex.contents.contents[0].body.contents[3].contents[0].color = selectColor;
            }

            if (periodOrderTmp.Tuesday == "Y")
            {
                weekFlex.contents.contents[0].body.contents[3].contents[1].color = selectColor;
            }
            if (periodOrderTmp.Wednesday == "Y")
            {
                weekFlex.contents.contents[0].body.contents[3].contents[2].color = selectColor;
            }
            if (periodOrderTmp.Thursday == "Y")
            {
                weekFlex.contents.contents[0].body.contents[3].contents[3].color = selectColor;
            }
            if (periodOrderTmp.Friday == "Y")
            {
                weekFlex.contents.contents[0].body.contents[3].contents[4].color = selectColor;
            }
            if (periodOrderTmp.Saturday == "Y")
            {
                weekFlex.contents.contents[0].body.contents[3].contents[5].color = selectColor;
            }
            if (periodOrderTmp.Sunday == "Y")
            {
                weekFlex.contents.contents[0].body.contents[3].contents[6].color = selectColor;
            }



            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var json = JsonConvert.SerializeObject(weekFlex, settings);

            return "[" + json + "]";
        }

        internal static string ClubMakeWeekFlex(string UserID, int QIDnow, int OIDnow, string shopID)
        {
            WeekFlex weekFlex = new WeekFlex();
            weekFlex.contents.contents[0].body.contents[1].text = "選擇固定星期 (可複選)";

            for (int i = 0; i < 7; i++)
            {
                weekFlex.contents.contents[0].body.contents[3].contents[i].action.data = $"{DateTime.Now},142,21,{i + 1}";
            }
            weekFlex.contents.contents[0].body.contents[3].contents[7].action.data = $"{DateTime.Now},142,3,";


            PeriodOrderTmp periodOrderTmp = new PeriodOrderTmp(UserID);//此處開始檢查按鈕有沒有被按下
            periodOrderTmp.SelectAllByUserID();

            string selectColor = "#4b607c";//在此處更改已選擇按鈕顏色

            if (periodOrderTmp.Monday == "Y")
            {
                weekFlex.contents.contents[0].body.contents[3].contents[0].color = selectColor;
            }

            if (periodOrderTmp.Tuesday == "Y")
            {
                weekFlex.contents.contents[0].body.contents[3].contents[1].color = selectColor;
            }
            if (periodOrderTmp.Wednesday == "Y")
            {
                weekFlex.contents.contents[0].body.contents[3].contents[2].color = selectColor;
            }
            if (periodOrderTmp.Thursday == "Y")
            {
                weekFlex.contents.contents[0].body.contents[3].contents[3].color = selectColor;
            }
            if (periodOrderTmp.Friday == "Y")
            {
                weekFlex.contents.contents[0].body.contents[3].contents[4].color = selectColor;
            }
            if (periodOrderTmp.Saturday == "Y")
            {
                weekFlex.contents.contents[0].body.contents[3].contents[5].color = selectColor;
            }
            if (periodOrderTmp.Sunday == "Y")
            {
                weekFlex.contents.contents[0].body.contents[3].contents[6].color = selectColor;
            }




            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var json = JsonConvert.SerializeObject(weekFlex, settings);

            return "[" + json + "]";
        }



        internal static string MakeSearchingItem(int QIDnow, int OIDnow, string UserID)
        {
            // 從OrderTemp裡面拿到OrderPartitionID
            OrderTemp orderTemp = new OrderTemp(UserID);
            orderTemp.SelectByUserID();

            // 用OrderPartitionID去找到ShopID，再用ShopID找到ShopItem
            OrderInfo orderInfo = new OrderInfo(UserID);
            orderInfo.OrderPartitionID = orderTemp.OrderPartitionID;



            string OrderPartitionIDSplitted = orderTemp.OrderPartitionID.Substring(0, 2);

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





            //NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            //QuestionDetail questionDetailNext = nextHelper.GetNext();

            //PreviousHelper previousHelper = new PreviousHelper(QIDnow, OIDnow);
            //QuestionDetail questionDetailPrevious = previousHelper.GetPrevious();

            //CancelHelper cancelHelper = new CancelHelper(QIDnow, OIDnow);
            //QuestionDetail questionDetailCancel = cancelHelper.GetCancel();




            ShopItem shopItem = new ShopItem(orderInfo.ShopID);
            List<ShopItem> myshopItems = shopItem.SelectByMyShopID();
            List<ShopItem> bossshopItems = shopItem.SelectByBossShopID();
            List<ShopItem> clubshopItems = shopItem.SelectByClubShopID();

            ShopInfo shopInfo = new ShopInfo();
            shopInfo.ShopID = orderInfo.ShopID;
            string OrderShopName;
            ShopImage shopImage = new ShopImage(orderInfo.ShopID);
            List<string> BossShopimagesUris = shopImage.SelectImageByShopID();//抓出該商店的圖片
            ShuffleImage(BossShopimagesUris);//隨機洗亂
            /////
            string DefaultUri = "https://i220.photobucket.com/albums/dd130/jung_04/GJ.gif";//Flex圖片超連結
            int ButtonControl = 4;//設定Button數量
            /////
            int ButtonCount = 0;//控制Button數量
            FlexPushOrderMenu flexPushOrderMenu = new FlexPushOrderMenu();

            if (myshopItems.Count > 0)
            {
                shopInfo.SelectMyShopNameByShopID();
                OrderShopName = shopInfo.ShopName;
                int BubbleCount = 0;
                flexPushOrderMenu.contents.contents.Add(new FlexPushOrderMenu.Bubble(DefaultUri));
                flexPushOrderMenu.contents.contents[BubbleCount].body.contents.Add(new FlexPushOrderMenu.OrderName(orderInfo.OrderName));
                flexPushOrderMenu.contents.contents[BubbleCount].body.contents.Add(new FlexPushOrderMenu.DateAndShopName( "結單時間："+orderInfo.StartTime.ToString("yyyy-MM-dd HH:mm"))  );
                flexPushOrderMenu.contents.contents[BubbleCount].body.contents.Add(new FlexPushOrderMenu.DateAndShopName(OrderShopName));
                flexPushOrderMenu.contents.contents[BubbleCount].body.contents.Add(new FlexPushOrderMenu.Content2() { type = "separator", margin = "xxl" });//分隔線
                foreach (ShopItem item in myshopItems)
                {
                    if (ButtonCount >= ButtonControl)
                    {
                        ButtonCount = 0;
                        BubbleCount++;
                        flexPushOrderMenu.contents.contents.Add(new FlexPushOrderMenu.Bubble(DefaultUri));

                        flexPushOrderMenu.contents.contents[BubbleCount].body.contents.Add(new FlexPushOrderMenu.OrderName(orderInfo.OrderName));
                        flexPushOrderMenu.contents.contents[BubbleCount].body.contents.Add(new FlexPushOrderMenu.DateAndShopName("結單時間：" + orderInfo.EndTime.ToString("yyyy-MM-dd HH:mm")));
                        flexPushOrderMenu.contents.contents[BubbleCount].body.contents.Add(new FlexPushOrderMenu.DateAndShopName(OrderShopName));
                        flexPushOrderMenu.contents.contents[BubbleCount].body.contents.Add(new FlexPushOrderMenu.Content2() { type = "separator", margin = "xxl" });//分隔線
                    }
                    flexPushOrderMenu.contents.contents[BubbleCount].footer.contents.Add(new FlexPushOrderMenu.OrderButton(item.shopItem, item.ShopItemPrice));
                    flexPushOrderMenu.contents.contents[BubbleCount].footer.contents[ButtonCount].action.data = DateTime.Now.Add((orderInfo.EndTime - orderInfo.StartTime).Add(new TimeSpan(0, -5, 0)))//主程式檢查按鈕有效時間為5分鐘
                                        + "," + 9999
                                        + "," + 2
                                        + "," + orderInfo.OrderPartitionID
                                        + "," + item.shopItem
                                        + "," + item.ShopItemPrice;
                    ButtonCount++;
                }

            }



            if (bossshopItems.Count > 0)
            {
                if (bossshopItems.Count >= BossShopimagesUris.Count * ButtonControl)
                {
                    int NeedAddcount = bossshopItems.Count - BossShopimagesUris.Count;
                    for (int i = 0; i <= NeedAddcount; i++)
                    {
                        BossShopimagesUris.Add(BossShopimagesUris[i]);
                    }
                }
                shopInfo.SelectBossShopNameByShopID();
                OrderShopName = shopInfo.ShopName;
                int BubbleCount = 0;
                flexPushOrderMenu.contents.contents.Add(new FlexPushOrderMenu.Bubble(BossShopimagesUris[0]));
                flexPushOrderMenu.contents.contents[BubbleCount].body.contents.Add(new FlexPushOrderMenu.OrderName(orderInfo.OrderName));
                flexPushOrderMenu.contents.contents[BubbleCount].body.contents.Add(new FlexPushOrderMenu.DateAndShopName("結單時間：" + orderInfo.EndTime.ToString("yyyy-MM-dd HH:mm")));
                flexPushOrderMenu.contents.contents[BubbleCount].body.contents.Add(new FlexPushOrderMenu.DateAndShopName(OrderShopName));
                flexPushOrderMenu.contents.contents[BubbleCount].body.contents.Add(new FlexPushOrderMenu.Content2() { type = "separator", margin = "xxl" });//分隔線
                foreach (ShopItem item in bossshopItems)
                {
                    if (ButtonCount >= ButtonControl)
                    {
                        ButtonCount = 0;
                        BubbleCount++;
                        flexPushOrderMenu.contents.contents.Add(new FlexPushOrderMenu.Bubble(BossShopimagesUris[BubbleCount]));

                        flexPushOrderMenu.contents.contents[BubbleCount].body.contents.Add(new FlexPushOrderMenu.OrderName(orderInfo.OrderName));
                        flexPushOrderMenu.contents.contents[BubbleCount].body.contents.Add(new FlexPushOrderMenu.DateAndShopName("結單時間：" + orderInfo.EndTime.ToString("yyyy-MM-dd HH:mm")));
                        flexPushOrderMenu.contents.contents[BubbleCount].body.contents.Add(new FlexPushOrderMenu.DateAndShopName(OrderShopName));
                        flexPushOrderMenu.contents.contents[BubbleCount].body.contents.Add(new FlexPushOrderMenu.Content2() { type = "separator", margin = "xxl" });//分隔線
                    }
                    flexPushOrderMenu.contents.contents[BubbleCount].footer.contents.Add(new FlexPushOrderMenu.OrderButton(item.shopItem, item.ShopItemPrice));
                    flexPushOrderMenu.contents.contents[BubbleCount].footer.contents[ButtonCount].action.data = DateTime.Now.Add((orderInfo.EndTime - orderInfo.StartTime).Add(new TimeSpan(0, -5, 0)))//主程式檢查按鈕有效時間為5分鐘
                                        + "," + 9999
                                        + "," + 2
                                        + "," + orderInfo.OrderPartitionID
                                        + "," + item.shopItem
                                        + "," + item.ShopItemPrice;
                    ButtonCount++;
                }


            }



            if (clubshopItems.Count > 0)
            {
                shopInfo.SelectClubShopNameByShopID();
                OrderShopName = shopInfo.ShopName;
                int BubbleCount = 0;
                flexPushOrderMenu.contents.contents.Add(new FlexPushOrderMenu.Bubble(DefaultUri));
                flexPushOrderMenu.contents.contents[BubbleCount].body.contents.Add(new FlexPushOrderMenu.OrderName(orderInfo.OrderName));
                flexPushOrderMenu.contents.contents[BubbleCount].body.contents.Add(new FlexPushOrderMenu.DateAndShopName("結單時間：" + orderInfo.EndTime.ToString("yyyy-MM-dd HH:mm")));
                flexPushOrderMenu.contents.contents[BubbleCount].body.contents.Add(new FlexPushOrderMenu.DateAndShopName(OrderShopName));
                flexPushOrderMenu.contents.contents[BubbleCount].body.contents.Add(new FlexPushOrderMenu.Content2() { type = "separator", margin = "xxl" });//分隔線
                foreach (ShopItem item in clubshopItems)
                {
                    if (ButtonCount >= ButtonControl)
                    {
                        ButtonCount = 0;
                        BubbleCount++;
                        flexPushOrderMenu.contents.contents.Add(new FlexPushOrderMenu.Bubble(DefaultUri));

                        flexPushOrderMenu.contents.contents[BubbleCount].body.contents.Add(new FlexPushOrderMenu.OrderName(orderInfo.OrderName));
                        flexPushOrderMenu.contents.contents[BubbleCount].body.contents.Add(new FlexPushOrderMenu.DateAndShopName("結單時間：" + orderInfo.EndTime.ToString("yyyy-MM-dd HH:mm")));
                        flexPushOrderMenu.contents.contents[BubbleCount].body.contents.Add(new FlexPushOrderMenu.DateAndShopName(OrderShopName));
                        flexPushOrderMenu.contents.contents[BubbleCount].body.contents.Add(new FlexPushOrderMenu.Content2() { type = "separator", margin = "xxl" });//分隔線
                    }
                    flexPushOrderMenu.contents.contents[BubbleCount].footer.contents.Add(new FlexPushOrderMenu.OrderButton(item.shopItem, item.ShopItemPrice));
                    flexPushOrderMenu.contents.contents[BubbleCount].footer.contents[ButtonCount].action.data = DateTime.Now.Add((orderInfo.EndTime - orderInfo.StartTime).Add(new TimeSpan(0, -5, 0)))//主程式檢查按鈕有效時間為5分鐘
                                        + "," + 9999
                                        + "," + 2
                                        + "," + orderInfo.OrderPartitionID
                                        + "," + item.shopItem
                                        + "," + item.ShopItemPrice;
                    ButtonCount++;
                }



            }


            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var json = JsonConvert.SerializeObject(flexPushOrderMenu, settings);
            return "[" + json + "]";


        }

        private static void ShuffleImage(List<string> bossShopimagesUris)
        {
            Random random = new Random();
            for (int i = 0; i < bossShopimagesUris.Count; i++)
            {
                string tmp ="";
                int Change = random.Next(i, bossShopimagesUris.Count);
                tmp = bossShopimagesUris[i] ;
                bossShopimagesUris[i] = bossShopimagesUris[Change];
                bossShopimagesUris[Change] = tmp;

            }

        }

        private static List<ShopItem> GetShopItemList(string shopID)
        {
            ShopItem shopItem = new ShopItem();
            shopItem.ShopID = shopID;
            List<ShopItem> shopItemList = new List<ShopItem>();

            string ShopIDSplitted = shopItem.ShopID.Substring(0, 2);
            switch (ShopIDSplitted.ToUpper())
            {
                case "MS":
                    shopItemList = shopItem.SelectByMyShopID();
                    break;
                case "CS":
                    shopItemList = shopItem.SelectByClubShopID();
                    break;
                case "BS":
                    shopItemList = shopItem.SelectByBossShopID();
                    break;
                default:
                    break;
            }
            return shopItemList;
        }

        internal static string MakeToShopBossFlex(string OrderPartitionID, string ShopID, string userId , out string BossID)
        {
            ShopInfo shopInfo = new ShopInfo();
            shopInfo.ShopID = ShopID;
            shopInfo.SelectBossIDByShopID();//找出店家負責人shopInfo.ClubIDorUserIDorBossID
            BossID = shopInfo.ClubIDorUserIDorBossID;

            OrderInfo orderInfo = new OrderInfo();
            orderInfo.OrderPartitionID = OrderPartitionID;
            string OrderPartitionIDSplitted = orderInfo.OrderPartitionID.Substring(0, 2);

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
            UserStatus userStatus = new UserStatus(userId);
            userStatus.SelectDisplayNameByUserID();//傳送訂單者名字
            FlexReportToShop flexReportToShop = new FlexReportToShop(userStatus.UserDisplayName, orderInfo.OrderPartitionID);
            flexReportToShop.contents.body.contents.Add(new ShopNameAndDate() { text = $"{orderInfo.EndTime.ToString("yyyy-MM-dd HH:mm")}" });

            flexReportToShop.contents.body.contents.Add(new FlexReportToShop.Content() { type = "separator", margin = "xxl" });//分隔線


            ////
            BuyerInfo buyerInfo = new BuyerInfo();
            buyerInfo.OrderPartitionID = orderInfo.OrderPartitionID;
            List<string> Buyers = buyerInfo.SelectAllBuyerTotalPartitionID();
            double totalprice = 0;//總價

        //    List<BuyerInfo> list = new List<BuyerInfo>(); ;//所有訂餐者
            foreach (string Buyer in Buyers)//針對每個訂餐者做事
            {
                userStatus = new UserStatus(Buyer);
                BuyerInfo buyer = new BuyerInfo();
                buyer.OrderPartitionID = orderInfo.OrderPartitionID;
                buyer.BuyerID = Buyer;
                List<BuyerInfo> list = buyer.SelectItemAndAmontByBuyerID();//選出訂餐者的訂餐細節
                double subtotalprice = 0;//該訂餐者總價
                foreach (BuyerInfo BuyerInfo in list)//處理訂餐細節
                {
                    subtotalprice += (BuyerInfo.Price * BuyerInfo.Amount);
                }
                totalprice += subtotalprice;//計算總價

            }


            List<BuyerInfo> infos = buyerInfo.SelectBuyerSUMItemAndAmountByOrderPartitionID();//直接用SQL選出每個品項總數
            List<TotalItem> totalItems = new List<TotalItem>();
            foreach (BuyerInfo buyerinfo in infos)
            {
                totalItems.Add(new TotalItem() { Item = buyerinfo.Item, Amount = buyerinfo.Amount });

            }
            flexReportToShop.AddTotal(totalItems, totalprice, Buyers, orderInfo.OrderPartitionID);
            ////

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var json = JsonConvert.SerializeObject(flexReportToShop, settings);
            json = "[" + json + "]";
            return json;

        }
    }
}