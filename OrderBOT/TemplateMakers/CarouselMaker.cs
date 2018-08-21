using isRock.LineBot;
using OrderBot.RecoginzeManager;
using OrderBot.SQLObject;
using OrderBot.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderBot.TemplateMakers
{
    public class CarouselMaker
    {
        internal static List<CarouselTemplate> MakeSearchingClub(int QIDnow, int OIDnow, string UserID)
        {
            string carouselImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/scg.png";
            string carouselFCImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/funcg.png";


            switch (QIDnow)
            {
                case 22:
                    carouselImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/scb.png";
                    carouselFCImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/funcb.png";
                    break;

                case 112:
                    carouselImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/scr.png";
                    carouselFCImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/funcr.png";
                    break;
                default:
                    break;
            }
            // 給UserID即可選出UserID底下所有的社團資訊
            ClubInfo clubInfo = new ClubInfo(UserID);
            List<ClubInfo> clubInfoList = clubInfo.SelectByUserid();

            // 這個方法即將回傳一個CarouselTemplate的List
            List<CarouselTemplate> carouselTemplatesList = new List<CarouselTemplate>();

            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            PreviousHelper previousHelper = new PreviousHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailPrevious = previousHelper.GetPrevious();

            CancelHelper cancelHelper = new CancelHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailCancel = cancelHelper.GetCancel();

            //int NextBtnQID = Recongnize(QIDnow, OIDnow);

            // OIDnow = 3;//調整因上一步導致OID換成0
            int buttonNumber = 0; // 已經製造出的button數，最後應該跟UserID下的社團數量一樣多
            int buttonIndex = 0;

            int carouselNumber = 1; // 共要發出的Carousel訊息數量

            // 總社團數量+3個功能button，再除以30來決定總共需要發出幾則Carousel訊息
            // 如果button數量大於30則carouselNumber無條件進位(等於是需要多發出一個訊息)
            double doubleTemp = (Convert.ToDouble(clubInfoList.Count) + 3) / 30;
            if (doubleTemp > 1.0)
            {
                carouselNumber = Convert.ToInt32(Math.Ceiling(doubleTemp));
            }

            // 開始製造carousel
            for (int i = 0; i < carouselNumber; i++)
            {
                CarouselTemplate carouselTemplate = new CarouselTemplate();

                int buttonTemplateTemp;
                if ((clubInfoList.Count - buttonIndex) / 3 < 1)  // 如果button數目少於三的話，還是給出一片buttonTemplate
                {
                    buttonTemplateTemp = 1;
                }
                else
                {
                    double temp = (Convert.ToDouble(clubInfoList.Count) - buttonIndex) / 3;
                    buttonTemplateTemp = Convert.ToInt32(Math.Ceiling(temp));
                    if (buttonTemplateTemp > 10)
                    {
                        buttonTemplateTemp = 10;
                    }
                }

                // 每片buttonTemplate的內容配置
                if (clubInfoList.Count == buttonIndex)
                {
                    carouselTemplatesList.Add(carouselTemplate);
                }
                else
                {
                    for (int j = 0; j < buttonTemplateTemp; j++)
                    {
                        var actions = new List<TemplateActionBase>();
                        string labeltemp;
                        string datatemp;

                        {// 第一個button
                            actions.Add(new PostbackAction()
                            {
                                label = clubInfoList[buttonIndex].ClubName,
                                data = DateTime.Now.ToString()
                                        + "," + questionDetailNext.QID
                                        + "," + questionDetailNext.OID
                                        + "," + clubInfoList[buttonIndex].ClubID
                            });
                            buttonIndex++;
                            buttonNumber++;
                        }

                        {// 第二個button
                            if (clubInfoList.Count <= buttonNumber)
                            {
                                labeltemp = "   ";
                                datatemp = "   ";
                            }
                            else
                            {
                                labeltemp = clubInfoList[buttonIndex].ClubName;
                                datatemp = DateTime.Now.ToString() + "," + questionDetailNext.QID + "," + questionDetailNext.OID + "," + clubInfoList[buttonIndex].ClubID;
                                buttonIndex++;
                            }
                            actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                            buttonNumber++;
                        }

                        { // 第三個button
                            if (clubInfoList.Count <= buttonNumber)
                            {
                                labeltemp = "   ";
                                datatemp = "   ";
                            }
                            else
                            {
                                labeltemp = clubInfoList[buttonIndex].ClubName;
                                datatemp = DateTime.Now.ToString() + "," + questionDetailNext.QID + "," + questionDetailNext.OID + "," + clubInfoList[buttonIndex].ClubID;
                                buttonIndex++;
                            }
                            actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                            buttonNumber++;
                        }

                        var Column = new Column
                        {
                            // title = "選擇社團",
                            text = "選擇社團",
                            thumbnailImageUrl = new Uri(carouselImageUri),
                            actions = actions
                        };
                        carouselTemplate.columns.Add(Column);
                    }
                    carouselTemplatesList.Add(carouselTemplate);
                }
            }

            var actionsFunction = new List<TemplateActionBase>();
            actionsFunction.Add(new PostbackAction() { label = "上一步", data = DateTime.Now.ToString() + $",{questionDetailPrevious.QID},{questionDetailPrevious.OID},default" });
            actionsFunction.Add(new PostbackAction() { label = "取消", data = DateTime.Now.ToString() + $",{questionDetailCancel.QID},{questionDetailCancel.OID},default" });
            actionsFunction.Add(new PostbackAction() { label = "   ", data = DateTime.Now.ToString() + ",,," });

            var ColumnFunction = new Column
            {
                // title = "這是功能片",
                text = "選擇功能",
                thumbnailImageUrl = new Uri(carouselFCImageUri),
                actions = actionsFunction
            };
            carouselTemplatesList[carouselNumber - 1].columns.Add(ColumnFunction);

            return carouselTemplatesList;
        }

        internal static List<CarouselTemplate> MakeSearchingPartitionOrder(int QIDnow, int OIDnow, string OrderID, string UserID)
        {
            // 給UserID即可選出UserID底下所有的訂單資訊
            OrderInfo orderinfo = new OrderInfo(UserID);
            orderinfo.OrderID = OrderID;
            List<OrderInfo> orderinfoList = new List<OrderInfo>();

            switch (QIDnow)
            {
                case 51:
                    orderinfoList = orderinfo.SelectMyOrderByOrderID();
                    break;

                case 151:
                    orderinfoList = orderinfo.SelectClubOrderByOrderID();
                    break;

                default:
                    break;
            }

            // 這個方法即將回傳一個CarouselTemplate的List
            List<CarouselTemplate> carouselTemplatesList = new List<CarouselTemplate>();

            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            PreviousHelper previousHelper = new PreviousHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailPrevious = previousHelper.GetPrevious();

            CancelHelper cancelHelper = new CancelHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailCancel = cancelHelper.GetCancel();

            int buttonNumber = 0; // 已經製造出的button數，最後應該跟UserID下的社團數量一樣多
            int buttonIndex = 0;

            int carouselNumber = 1; // 共要發出的Carousel訊息數量

            // 總社團數量+3個功能button，再除以30來決定總共需要發出幾則Carousel訊息
            // 如果button數量大於30則carouselNumber無條件進位(等於是需要多發出一個訊息)
            double doubleTemp = (Convert.ToDouble(orderinfoList.Count) + 3) / 30;
            if (doubleTemp > 1.0)
            {
                carouselNumber = Convert.ToInt32(Math.Ceiling(doubleTemp));
            }

            // 開始製造carousel
            for (int i = 0; i < carouselNumber; i++)
            {
                CarouselTemplate carouselTemplate = new CarouselTemplate();

                int buttonTemplateTemp;
                if ((orderinfoList.Count - buttonIndex) / 3 < 1)  // 如果button數目少於三的話，還是給出一片buttonTemplate
                {
                    buttonTemplateTemp = 1;
                }
                else
                {
                    double temp = (Convert.ToDouble(orderinfoList.Count) - buttonIndex) / 3;
                    buttonTemplateTemp = Convert.ToInt32(Math.Ceiling(temp));
                    if (buttonTemplateTemp > 10)
                    {
                        buttonTemplateTemp = 10;
                    }
                }

                // 每片buttonTemplate的內容配置
                if (orderinfoList.Count == buttonIndex)
                {
                    carouselTemplatesList.Add(carouselTemplate);
                }
                else
                {
                    for (int j = 0; j < buttonTemplateTemp; j++)
                    {
                        var actions = new List<TemplateActionBase>();
                        string labeltemp;
                        string datatemp;
                        string shopName = "無資料";
                        ShopInfo shopInfo = new ShopInfo();
                        shopInfo.ShopID = orderinfoList[buttonIndex].ShopID;
                        switch (QIDnow)
                        {
                            case 51:
                                shopInfo.SelectMyShopNameByShopID();
                                if (shopInfo.ShopName == null)
                                {
                                    shopInfo.SelectBossShopNameByShopID();
                                    //   orderinfo.
                                }
                                shopName = shopInfo.ShopName;
                                break;

                            case 151:
                                shopInfo.SelectClubShopNameByShopID();
                                if (shopInfo.ShopName == null)
                                {
                                    shopInfo.SelectBossShopNameByShopID();
                                    //   orderinfo.
                                }
                                shopName = shopInfo.ShopName;
                                break;

                            default:
                                break;
                        }
                        //  orderInfo.ShopID = orderinfoList[buttonIndex].ShopID;
                        //     orderInfo.se

                        {// 第一個button
                            actions.Add(new PostbackAction()
                            {
                                // label = $"{orderinfoList[buttonIndex].StartTime.ToShortDateString()}-{orderinfoList[buttonIndex].OrderName}",
                                label = $"{orderinfoList[buttonIndex].StartTime.ToShortDateString()}-{shopName}",
                                data = DateTime.Now.ToString()
                                        + "," + questionDetailNext.QID
                                        + "," + questionDetailNext.OID
                                        + "," + orderinfoList[buttonIndex].OrderPartitionID
                            });
                            buttonIndex++;
                            buttonNumber++;
                        }

                        {// 第二個button
                            if (orderinfoList.Count <= buttonNumber)
                            {
                                labeltemp = "   ";
                                datatemp = "   ";
                            }
                            else
                            {
                                //  labeltemp = $"{orderinfoList[buttonIndex].StartTime.ToShortDateString()}-{orderinfoList[buttonIndex].OrderName}";
                                labeltemp = $"{orderinfoList[buttonIndex].StartTime.ToShortDateString()}-{shopName}";
                                datatemp = DateTime.Now.ToString()
                                    + "," + questionDetailNext.QID
                                    + "," + questionDetailNext.OID
                                    + "," + orderinfoList[buttonIndex].OrderPartitionID;
                                buttonIndex++;
                            }
                            actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                            buttonNumber++;
                        }

                        { // 第三個button
                            if (orderinfoList.Count <= buttonNumber)
                            {
                                labeltemp = "   ";
                                datatemp = "   ";
                            }
                            else
                            {
                                // labeltemp = $"{orderinfoList[buttonIndex].StartTime.ToShortDateString()}-{orderinfoList[buttonIndex].OrderName}";
                                labeltemp = $"{orderinfoList[buttonIndex].StartTime.ToShortDateString()}-{shopName}";
                                datatemp = DateTime.Now.ToString()
                                    + "," + questionDetailNext.QID
                                    + "," + questionDetailNext.OID
                                    + "," + orderinfoList[buttonIndex].OrderPartitionID;
                                buttonIndex++;
                            }
                            actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                            buttonNumber++;
                        }

                        var Column = new Column
                        {
                            // title = "選擇訂單",
                            text = "選擇個別訂單",
                            thumbnailImageUrl = new Uri("https://i220.photobucket.com/albums/dd130/jung_04/sopb.png"),
                            actions = actions
                        };
                        carouselTemplate.columns.Add(Column);
                    }
                    carouselTemplatesList.Add(carouselTemplate);
                }
            }

            var actionsFunction = new List<TemplateActionBase>();
            actionsFunction.Add(new PostbackAction() { label = "上一步", data = DateTime.Now.ToString() + $",{questionDetailPrevious.QID},{questionDetailPrevious.OID},default" });
            actionsFunction.Add(new PostbackAction() { label = "取消", data = DateTime.Now.ToString() + $",{questionDetailCancel.QID},{questionDetailCancel.OID},default" });
            actionsFunction.Add(new PostbackAction() { label = "   ", data = DateTime.Now.ToString() + ",,," });

            var ColumnFunction = new Column
            {
                //  title = "這是功能片",
                text = "選擇功能",
                thumbnailImageUrl = new Uri("https://i220.photobucket.com/albums/dd130/jung_04/funcb.png"),
                actions = actionsFunction
            };
            carouselTemplatesList[carouselNumber - 1].columns.Add(ColumnFunction);

            return carouselTemplatesList;
        }

        internal static List<CarouselTemplate> MakeSearchingMyOrder(int QIDnow, int OIDnow, string UserID)
        {
            OrderInfo orderinfo;
            List<OrderInfo> orderinfoList = new List<OrderInfo>();
            switch (QIDnow)
            {
                case 32://MyOrder
                    orderinfo = new OrderInfo(UserID);
                    orderinfoList = orderinfo.SelectMyOrderByUserID();
                    break;
                case 132://CluberOrder
                    OrderTemp orderTemp = new OrderTemp(UserID);
                    orderTemp.ClubIDSelectByUserID();
                    orderinfo = new OrderInfo(orderTemp.ClubID);
                    orderinfoList = orderinfo.SelectClubOrderByUserID();

                    break;
                default:
                    break;
            }
            // 給UserID即可選出UserID底下所有的訂單資訊


            // 這個方法即將回傳一個CarouselTemplate的List
            List<CarouselTemplate> carouselTemplatesList = new List<CarouselTemplate>();

            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            PreviousHelper previousHelper = new PreviousHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailPrevious = previousHelper.GetPrevious();

            CancelHelper cancelHelper = new CancelHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailCancel = cancelHelper.GetCancel();

            int buttonNumber = 0; // 已經製造出的button數，最後應該跟UserID下的社團數量一樣多
            int buttonIndex = 0;

            int carouselNumber = 1; // 共要發出的Carousel訊息數量

            // 總社團數量+3個功能button，再除以30來決定總共需要發出幾則Carousel訊息
            // 如果button數量大於30則carouselNumber無條件進位(等於是需要多發出一個訊息)
            double doubleTemp = (Convert.ToDouble(orderinfoList.Count) + 3) / 30;
            if (doubleTemp > 1.0)
            {
                carouselNumber = Convert.ToInt32(Math.Ceiling(doubleTemp));
            }

            // 開始製造carousel
            for (int i = 0; i < carouselNumber; i++)
            {
                CarouselTemplate carouselTemplate = new CarouselTemplate();

                int buttonTemplateTemp;
                if ((orderinfoList.Count - buttonIndex) / 3 < 1)  // 如果button數目少於三的話，還是給出一片buttonTemplate
                {
                    buttonTemplateTemp = 1;
                }
                else
                {
                    double temp = (Convert.ToDouble(orderinfoList.Count) - buttonIndex) / 3;
                    buttonTemplateTemp = Convert.ToInt32(Math.Ceiling(temp));
                    if (buttonTemplateTemp > 10)
                    {
                        buttonTemplateTemp = 10;
                    }
                }

                // 每片buttonTemplate的內容配置
                if (orderinfoList.Count == buttonIndex)
                {
                    carouselTemplatesList.Add(carouselTemplate);
                }
                else
                {
                    for (int j = 0; j < buttonTemplateTemp; j++)
                    {
                        var actions = new List<TemplateActionBase>();
                        string labeltemp;
                        string datatemp;

                        {// 第一個button
                            actions.Add(new PostbackAction()
                            {
                                label = $"{orderinfoList[buttonIndex].OrderName}",
                                data = DateTime.Now.ToString()
                                        + "," + questionDetailNext.QID
                                        + "," + questionDetailNext.OID
                                        + "," + orderinfoList[buttonIndex].OrderID
                            });
                            buttonIndex++;
                            buttonNumber++;
                        }

                        {// 第二個button
                            if (orderinfoList.Count <= buttonNumber)
                            {
                                labeltemp = "   ";
                                datatemp = "   ";
                            }
                            else
                            {
                                labeltemp = $"{orderinfoList[buttonIndex].OrderName}";
                                datatemp = DateTime.Now.ToString()
                                    + "," + questionDetailNext.QID
                                    + "," + questionDetailNext.OID
                                    + "," + orderinfoList[buttonIndex].OrderID;
                                buttonIndex++;
                            }
                            actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                            buttonNumber++;
                        }

                        { // 第三個button
                            if (orderinfoList.Count <= buttonNumber)
                            {
                                labeltemp = "   ";
                                datatemp = "   ";
                            }
                            else
                            {
                                labeltemp = $"{orderinfoList[buttonIndex].OrderName}";
                                datatemp = DateTime.Now.ToString()
                                    + "," + questionDetailNext.QID
                                    + "," + questionDetailNext.OID
                                    + "," + orderinfoList[buttonIndex].OrderID;
                                buttonIndex++;
                            }
                            actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                            buttonNumber++;
                        }

                        var Column = new Column
                        {
                            // title = "選擇訂單",
                            text = "選擇我的訂單",
                            thumbnailImageUrl = new Uri("https://i220.photobucket.com/albums/dd130/jung_04/sob.png"),
                            actions = actions
                        };
                        carouselTemplate.columns.Add(Column);
                    }
                    carouselTemplatesList.Add(carouselTemplate);
                }
            }

            var actionsFunction = new List<TemplateActionBase>();
            actionsFunction.Add(new PostbackAction() { label = "上一步", data = DateTime.Now.ToString() + $",{questionDetailPrevious.QID},{questionDetailPrevious.OID},default" });
            actionsFunction.Add(new PostbackAction() { label = "取消", data = DateTime.Now.ToString() + $",{questionDetailCancel.QID},{questionDetailCancel.OID},default" });
            actionsFunction.Add(new PostbackAction() { label = "   ", data = DateTime.Now.ToString() + ",,," });

            var ColumnFunction = new Column
            {
                //  title = "這是功能片",
                text = "選擇功能",
                thumbnailImageUrl = new Uri("https://i220.photobucket.com/albums/dd130/jung_04/funcb.png"),
                actions = actionsFunction
            };
            carouselTemplatesList[carouselNumber - 1].columns.Add(ColumnFunction);

            return carouselTemplatesList;
        }

        internal static List<CarouselTemplate> MakeSearchingUserOrder(int QIDnow, int OIDnow, string UserID)
        {
            // 給UserID即可選出UserID底下所有的訂單資訊
            OrderInfo orderinfo = new OrderInfo(UserID);

            // 這個方法即將回傳一個CarouselTemplate的List
            List<CarouselTemplate> carouselTemplatesList = new List<CarouselTemplate>();

            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            PreviousHelper previousHelper = new PreviousHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailPrevious = previousHelper.GetPrevious();

            CancelHelper cancelHelper = new CancelHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailCancel = cancelHelper.GetCancel();


            #region 做MyOrder的carousel
            List<OrderInfo> MyOrderInfoList = orderinfo.SelectUserMyOrderByUserID();
            int buttonNumber = 0; // 已經製造出的button數，最後應該跟UserID下的社團數量一樣多
            int buttonIndex = 0;

            int carouselNumberMy = 1; // 共要發出的Carousel訊息數量

            // 總社團數量+3個功能button，再除以30來決定總共需要發出幾則Carousel訊息
            // 如果button數量大於30則carouselNumber無條件進位(等於是需要多發出一個訊息)
            double doubleTemp = (Convert.ToDouble(MyOrderInfoList.Count) + 3) / 30;
            if (doubleTemp > 1.0)
            {
                carouselNumberMy = Convert.ToInt32(Math.Ceiling(doubleTemp));
            }

            // 開始製造carousel
            for (int i = 0; i < carouselNumberMy; i++)
            {
                CarouselTemplate carouselTemplate = new CarouselTemplate();

                int buttonTemplateTemp;
                if ((MyOrderInfoList.Count - buttonIndex) / 3 < 1)  // 如果button數目少於三的話，還是給出一片buttonTemplate
                {
                    buttonTemplateTemp = 1;
                }
                else
                {
                    double temp = (Convert.ToDouble(MyOrderInfoList.Count) - buttonIndex) / 3;
                    buttonTemplateTemp = Convert.ToInt32(Math.Ceiling(temp));
                    if (buttonTemplateTemp > 10)
                    {
                        buttonTemplateTemp = 10;
                    }
                }

                // 每片buttonTemplate的內容配置
                if (MyOrderInfoList.Count == buttonIndex)
                {
                    carouselTemplatesList.Add(carouselTemplate);
                }
                else
                {
                    for (int j = 0; j < buttonTemplateTemp; j++)
                    {
                        var actions = new List<TemplateActionBase>();
                        string labeltemp;
                        string datatemp;

                        {// 第一個button
                            actions.Add(new PostbackAction()
                            {
                                label = $"{MyOrderInfoList[buttonIndex].OrderName}",
                                data = DateTime.Now.ToString()
                                        + "," + questionDetailNext.QID
                                        + "," + questionDetailNext.OID
                                        + "," + MyOrderInfoList[buttonIndex].OrderID
                            });
                            buttonIndex++;
                            buttonNumber++;
                        }

                        {// 第二個button
                            if (MyOrderInfoList.Count <= buttonNumber)
                            {
                                labeltemp = "   ";
                                datatemp = "   ";
                            }
                            else
                            {
                                labeltemp = $"{MyOrderInfoList[buttonIndex].OrderName}";
                                datatemp = DateTime.Now.ToString()
                                    + "," + questionDetailNext.QID
                                    + "," + questionDetailNext.OID
                                    + "," + MyOrderInfoList[buttonIndex].OrderID;
                                buttonIndex++;
                            }
                            actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                            buttonNumber++;
                        }

                        { // 第三個button
                            if (MyOrderInfoList.Count <= buttonNumber)
                            {
                                labeltemp = "   ";
                                datatemp = "   ";
                            }
                            else
                            {
                                labeltemp = $"{MyOrderInfoList[buttonIndex].OrderName}";
                                datatemp = DateTime.Now.ToString()
                                    + "," + questionDetailNext.QID
                                    + "," + questionDetailNext.OID
                                    + "," + MyOrderInfoList[buttonIndex].OrderID;
                                buttonIndex++;
                            }
                            actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                            buttonNumber++;
                        }

                        var Column = new Column
                        {
                            // title = "選擇我的訂單",
                            text = "選擇我的訂單",
                            thumbnailImageUrl = new Uri("https://i220.photobucket.com/albums/dd130/jung_04/soy.png"),
                            actions = actions
                        };
                        carouselTemplate.columns.Add(Column);
                    }
                    carouselTemplatesList.Add(carouselTemplate);
                }
            }

            var actionsFunction = new List<TemplateActionBase>();
            actionsFunction.Add(new PostbackAction() { label = "上一步", data = DateTime.Now.ToString() + $",{questionDetailPrevious.QID},{questionDetailPrevious.OID},default" });
            actionsFunction.Add(new PostbackAction() { label = "取消", data = DateTime.Now.ToString() + $",{questionDetailCancel.QID},{questionDetailCancel.OID},default" });
            actionsFunction.Add(new PostbackAction() { label = "   ", data = DateTime.Now.ToString() + ",,," });

            var ColumnFunction = new Column
            {
                // title = "這是功能片",
                text = "選擇功能",
                thumbnailImageUrl = new Uri("https://i220.photobucket.com/albums/dd130/jung_04/funcy.png"),
                actions = actionsFunction
            };
            carouselTemplatesList[carouselNumberMy - 1].columns.Add(ColumnFunction);
            #endregion

            #region 做ClubOrder的carousel
            List<OrderInfo> ClubOrderInfoList = orderinfo.SelectUserClubOrderByUserID();

            buttonNumber = 0; // 已經製造出的button數，最後應該跟UserID下的社團數量一樣多
            buttonIndex = 0;

            int carouselNumberClub = 1; // 共要發出的Carousel訊息數量

            // 總社團數量+3個功能button，再除以30來決定總共需要發出幾則Carousel訊息
            // 如果button數量大於30則carouselNumber無條件進位(等於是需要多發出一個訊息)
            doubleTemp = (Convert.ToDouble(ClubOrderInfoList.Count) + 3) / 30;
            if (doubleTemp > 1.0)
            {
                carouselNumberClub = Convert.ToInt32(Math.Ceiling(doubleTemp));
            }

            // 開始製造carousel
            for (int i = 0; i < carouselNumberClub; i++)
            {
                CarouselTemplate carouselTemplate = new CarouselTemplate();

                int buttonTemplateTemp;
                if ((ClubOrderInfoList.Count - buttonIndex) / 3 < 1)  // 如果button數目少於三的話，還是給出一片buttonTemplate
                {
                    buttonTemplateTemp = 1;
                }
                else
                {
                    double temp = (Convert.ToDouble(ClubOrderInfoList.Count) - buttonIndex) / 3;
                    buttonTemplateTemp = Convert.ToInt32(Math.Ceiling(temp));
                    if (buttonTemplateTemp > 10)
                    {
                        buttonTemplateTemp = 10;
                    }
                }

                // 每片buttonTemplate的內容配置
                if (ClubOrderInfoList.Count == buttonIndex)
                {
                    carouselTemplatesList.Add(carouselTemplate);
                }
                else
                {
                    for (int j = 0; j < buttonTemplateTemp; j++)
                    {
                        var actions = new List<TemplateActionBase>();
                        string labeltemp;
                        string datatemp;

                        {// 第一個button
                            actions.Add(new PostbackAction()
                            {
                                label = $"{ClubOrderInfoList[buttonIndex].OrderName}",
                                data = DateTime.Now.ToString()
                                        + "," + questionDetailNext.QID
                                        + "," + questionDetailNext.OID
                                        + "," + ClubOrderInfoList[buttonIndex].OrderID
                            });
                            buttonIndex++;
                            buttonNumber++;
                        }

                        {// 第二個button
                            if (ClubOrderInfoList.Count <= buttonNumber)
                            {
                                labeltemp = "   ";
                                datatemp = "   ";
                            }
                            else
                            {
                                labeltemp = $"{ClubOrderInfoList[buttonIndex].OrderName}";
                                datatemp = DateTime.Now.ToString()
                                    + "," + questionDetailNext.QID
                                    + "," + questionDetailNext.OID
                                    + "," + ClubOrderInfoList[buttonIndex].OrderID;
                                buttonIndex++;
                            }
                            actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                            buttonNumber++;
                        }

                        { // 第三個button
                            if (ClubOrderInfoList.Count <= buttonNumber)
                            {
                                labeltemp = "   ";
                                datatemp = "   ";
                            }
                            else
                            {
                                labeltemp = $"{ClubOrderInfoList[buttonIndex].OrderName}";
                                datatemp = DateTime.Now.ToString()
                                    + "," + questionDetailNext.QID
                                    + "," + questionDetailNext.OID
                                    + "," + ClubOrderInfoList[buttonIndex].OrderID;
                                buttonIndex++;
                            }
                            actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                            buttonNumber++;
                        }

                        var Column = new Column
                        {
                            // title = "選擇社團訂單",
                            text = "選擇社團訂單(社團訂單只有在退出社團時才會刪除喔~",
                            thumbnailImageUrl = new Uri("https://i220.photobucket.com/albums/dd130/jung_04/soy.png"),
                            actions = actions
                        };
                        carouselTemplate.columns.Add(Column);
                    }
                    carouselTemplatesList.Add(carouselTemplate);
                }
            }
            // 加上功能片       
            carouselTemplatesList[carouselNumberMy + carouselNumberClub - 1].columns.Add(ColumnFunction);

            #endregion

            return carouselTemplatesList;
        }

        internal static List<CarouselTemplate> MakeSearchingUserPartitionOrder(int QIDnow, int OIDnow, string OrderID, string UserID)
        {
            // 給UserID即可選出UserID底下所有的訂單資訊
            OrderInfo orderinfo = new OrderInfo(UserID);
            orderinfo.OrderID = OrderID;

            // 這個方法即將回傳一個CarouselTemplate的List
            List<CarouselTemplate> carouselTemplatesList = new List<CarouselTemplate>();

            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            PreviousHelper previousHelper = new PreviousHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailPrevious = previousHelper.GetPrevious();

            CancelHelper cancelHelper = new CancelHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailCancel = cancelHelper.GetCancel();

            // 共用的變數
            int buttonNumber;
            int buttonIndex;

            // 製做功能片共用的
            var actionsFunction = new List<TemplateActionBase>();
            actionsFunction.Add(new PostbackAction() { label = "上一步", data = DateTime.Now.ToString() + $",{questionDetailPrevious.QID},{questionDetailPrevious.OID},default" });
            actionsFunction.Add(new PostbackAction() { label = "取消", data = DateTime.Now.ToString() + $",{questionDetailCancel.QID},{questionDetailCancel.OID},default" });
            actionsFunction.Add(new PostbackAction() { label = "   ", data = DateTime.Now.ToString() + ",,," });

            var ColumnFunction = new Column
            {
                // title = "這是功能片",
                text = "選擇功能",
                thumbnailImageUrl = new Uri("https://i220.photobucket.com/albums/dd130/jung_04/funcy.png"),
                actions = actionsFunction
            };

            #region 做MyOrder的carousel
            List<OrderInfo> MyOrderInfoList = orderinfo.SelectUserMyPartitionOrderByOrderID();

            if (MyOrderInfoList.Count > 0)
            {
                buttonNumber = 0; // 已經製造出的button數，最後應該跟UserID下的社團數量一樣多
                buttonIndex = 0;

                int carouselNumberMy = 1; // 共要發出的Carousel訊息數量

                // 總社團數量+3個功能button，再除以30來決定總共需要發出幾則Carousel訊息
                // 如果button數量大於30則carouselNumber無條件進位(等於是需要多發出一個訊息)
                double doubleTemp = (Convert.ToDouble(MyOrderInfoList.Count) + 3) / 30;
                if (doubleTemp > 1.0)
                {
                    carouselNumberMy = Convert.ToInt32(Math.Ceiling(doubleTemp));
                }

                // 開始製造carousel
                for (int i = 0; i < carouselNumberMy; i++)
                {
                    CarouselTemplate carouselTemplate = new CarouselTemplate();

                    int buttonTemplateTemp;
                    if ((MyOrderInfoList.Count - buttonIndex) / 3 < 1)  // 如果button數目少於三的話，還是給出一片buttonTemplate
                    {
                        buttonTemplateTemp = 1;
                    }
                    else
                    {
                        double temp = (Convert.ToDouble(MyOrderInfoList.Count) - buttonIndex) / 3;
                        buttonTemplateTemp = Convert.ToInt32(Math.Ceiling(temp));
                        if (buttonTemplateTemp > 10)
                        {
                            buttonTemplateTemp = 10;
                        }
                    }

                    // 每片buttonTemplate的內容配置
                    if (MyOrderInfoList.Count == buttonIndex)
                    {
                        carouselTemplatesList.Add(carouselTemplate);
                    }
                    else
                    {
                        for (int j = 0; j < buttonTemplateTemp; j++)
                        {
                            var actions = new List<TemplateActionBase>();
                            string labeltemp;
                            string datatemp;
                            string ShopNameTemp;

                            {// 第一個button
                                if (MyOrderInfoList[buttonIndex].ShopName.Length > 10)
                                {
                                    ShopNameTemp = MyOrderInfoList[buttonIndex].ShopName.Substring(0, 10);
                                }
                                else
                                {
                                    ShopNameTemp = MyOrderInfoList[buttonIndex].ShopName;
                                }
                                actions.Add(new PostbackAction()
                                {
                                    label = $"[{MyOrderInfoList[buttonIndex].StartTime.ToString("yyyyMMdd")}]{ShopNameTemp}",
                                    data = DateTime.Now.ToString()
                                            + "," + questionDetailNext.QID
                                            + "," + questionDetailNext.OID
                                            + "," + MyOrderInfoList[buttonIndex].OrderPartitionID
                                });
                                buttonIndex++;
                                buttonNumber++;
                            }

                            {// 第二個button
                                if (MyOrderInfoList.Count <= buttonNumber)
                                {
                                    labeltemp = "   ";
                                    datatemp = "   ";
                                }
                                else
                                {
                                    if (MyOrderInfoList[buttonIndex].ShopName.Length > 10)
                                    {
                                        ShopNameTemp = MyOrderInfoList[buttonIndex].ShopName.Substring(0, 10);
                                    }
                                    else
                                    {
                                        ShopNameTemp = MyOrderInfoList[buttonIndex].ShopName;
                                    }

                                    labeltemp = $"[{MyOrderInfoList[buttonIndex].StartTime.ToString("yyyyMMdd")}]{ShopNameTemp}";
                                    datatemp = DateTime.Now.ToString()
                                        + "," + questionDetailNext.QID
                                        + "," + questionDetailNext.OID
                                        + "," + MyOrderInfoList[buttonIndex].OrderPartitionID;
                                    buttonIndex++;
                                }
                                actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                                buttonNumber++;
                            }

                            { // 第三個button
                                if (MyOrderInfoList.Count <= buttonNumber)
                                {
                                    labeltemp = "   ";
                                    datatemp = "   ";
                                }
                                else
                                {
                                    if (MyOrderInfoList[buttonIndex].ShopName.Length > 10)
                                    {
                                        ShopNameTemp = MyOrderInfoList[buttonIndex].ShopName.Substring(0, 10);
                                    }
                                    else
                                    {
                                        ShopNameTemp = MyOrderInfoList[buttonIndex].ShopName;
                                    }

                                    labeltemp = $"[{MyOrderInfoList[buttonIndex].StartTime.ToString("yyyyMMdd")}]{ShopNameTemp}";
                                    datatemp = DateTime.Now.ToString()
                                        + "," + questionDetailNext.QID
                                        + "," + questionDetailNext.OID
                                        + "," + MyOrderInfoList[buttonIndex].OrderPartitionID;
                                    buttonIndex++;
                                }
                                actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                                buttonNumber++;
                            }

                            var Column = new Column
                            {
                                //  title = "這裡是我的訂單",
                                text = "選擇個別訂單",
                                thumbnailImageUrl = new Uri("https://i220.photobucket.com/albums/dd130/jung_04/sopy.png"),
                                actions = actions
                            };
                            carouselTemplate.columns.Add(Column);
                        }
                        carouselTemplatesList.Add(carouselTemplate);
                    }
                }
                // 加上功能片             
                carouselTemplatesList[carouselNumberMy - 1].columns.Add(ColumnFunction);
            }
            #endregion

            #region 做ClubOrder的carousel
            List<OrderInfo> ClubOrderInfoList = orderinfo.SelectUserClubPartitionOrderByOrderID();
            if (ClubOrderInfoList.Count > 0)
            {
                buttonNumber = 0; // 已經製造出的button數，最後應該跟UserID下的社團數量一樣多
                buttonIndex = 0;

                int carouselNumberClub = 1; // 共要發出的Carousel訊息數量

                // 總社團數量+3個功能button，再除以30來決定總共需要發出幾則Carousel訊息
                // 如果button數量大於30則carouselNumber無條件進位(等於是需要多發出一個訊息)
                double doubleTemp = (Convert.ToDouble(ClubOrderInfoList.Count) + 3) / 30;
                if (doubleTemp > 1.0)
                {
                    carouselNumberClub = Convert.ToInt32(Math.Ceiling(doubleTemp));
                }

                // 開始製造carousel
                for (int i = 0; i < carouselNumberClub; i++)
                {
                    CarouselTemplate carouselTemplate = new CarouselTemplate();

                    int buttonTemplateTemp;
                    if ((ClubOrderInfoList.Count - buttonIndex) / 3 < 1)  // 如果button數目少於三的話，還是給出一片buttonTemplate
                    {
                        buttonTemplateTemp = 1;
                    }
                    else
                    {
                        double temp = (Convert.ToDouble(ClubOrderInfoList.Count) - buttonIndex) / 3;
                        buttonTemplateTemp = Convert.ToInt32(Math.Ceiling(temp));
                        if (buttonTemplateTemp > 10)
                        {
                            buttonTemplateTemp = 10;
                        }
                    }

                    // 每片buttonTemplate的內容配置
                    if (ClubOrderInfoList.Count == buttonIndex)
                    {
                        carouselTemplatesList.Add(carouselTemplate);
                    }
                    else
                    {
                        for (int j = 0; j < buttonTemplateTemp; j++)
                        {
                            var actions = new List<TemplateActionBase>();
                            string labeltemp;
                            string datatemp;
                            string ShopNameTemp;

                            {// 第一個button
                                if (ClubOrderInfoList[buttonIndex].ShopName.Length > 10)
                                {
                                    ShopNameTemp = ClubOrderInfoList[buttonIndex].ShopName.Substring(0, 10);
                                }
                                else
                                {
                                    ShopNameTemp = ClubOrderInfoList[buttonIndex].ShopName;
                                }

                                actions.Add(new PostbackAction()
                                {
                                    label = $"[{ClubOrderInfoList[buttonIndex].StartTime.ToString("yyyyMMdd")}]{ShopNameTemp}",
                                    data = DateTime.Now.ToString()
                                            + "," + questionDetailNext.QID
                                            + "," + questionDetailNext.OID
                                            + "," + ClubOrderInfoList[buttonIndex].OrderPartitionID
                                });
                                buttonIndex++;
                                buttonNumber++;
                            }

                            {// 第二個button
                                if (ClubOrderInfoList.Count <= buttonNumber)
                                {
                                    labeltemp = "   ";
                                    datatemp = "   ";
                                }
                                else
                                {
                                    if (ClubOrderInfoList[buttonIndex].ShopName.Length > 10)
                                    {
                                        ShopNameTemp = ClubOrderInfoList[buttonIndex].ShopName.Substring(0, 10);
                                    }
                                    else
                                    {
                                        ShopNameTemp = ClubOrderInfoList[buttonIndex].ShopName;
                                    }

                                    labeltemp = $"[{ClubOrderInfoList[buttonIndex].StartTime.ToString("yyyyMMdd")}]{ShopNameTemp}";
                                    datatemp = DateTime.Now.ToString()
                                        + "," + questionDetailNext.QID
                                        + "," + questionDetailNext.OID
                                        + "," + ClubOrderInfoList[buttonIndex].OrderPartitionID;
                                    buttonIndex++;
                                }
                                actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                                buttonNumber++;
                            }

                            { // 第三個button
                                if (ClubOrderInfoList.Count <= buttonNumber)
                                {
                                    labeltemp = "   ";
                                    datatemp = "   ";
                                }
                                else
                                {
                                    if (ClubOrderInfoList[buttonIndex].ShopName.Length > 10)
                                    {
                                        ShopNameTemp = ClubOrderInfoList[buttonIndex].ShopName.Substring(0, 10);
                                    }
                                    else
                                    {
                                        ShopNameTemp = ClubOrderInfoList[buttonIndex].ShopName;
                                    }

                                    labeltemp = $"[{ClubOrderInfoList[buttonIndex].StartTime.ToString("yyyyMMdd")}]{ShopNameTemp}";
                                    datatemp = DateTime.Now.ToString()
                                        + "," + questionDetailNext.QID
                                        + "," + questionDetailNext.OID
                                        + "," + ClubOrderInfoList[buttonIndex].OrderPartitionID;
                                    buttonIndex++;
                                }
                                actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                                buttonNumber++;
                            }

                            var Column = new Column
                            {
                                //  title = "這裡是社團訂單",
                                text = "選擇個別訂單(社團訂單只有在退出社團時才會刪除喔~)",
                                thumbnailImageUrl = new Uri("https://i220.photobucket.com/albums/dd130/jung_04/sopy.png"),
                                actions = actions
                            };
                            carouselTemplate.columns.Add(Column);
                        }
                        carouselTemplatesList.Add(carouselTemplate);
                    }
                }
                // 加上功能片       
                carouselTemplatesList[carouselNumberClub - 1].columns.Add(ColumnFunction);
            }
            #endregion

            return carouselTemplatesList;
        }

        internal static List<CarouselTemplate> MakeSearchingMyShop(int QIDnow, int OIDnow, string UserID)
        {


            string carouselImageUri = "";
            string carouselFCImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/funcr.png";
            switch (QIDnow)
            {

                case 201:
                    carouselFCImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/funcb.png";
                    break;

                case 211:
                    carouselFCImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/funcb.png";
                    break;
                default:
                    break;
            }
            // 給UserID即可選出UserID底下所有的社團資訊
            ShopInfo shopInfo = new ShopInfo(UserID);
            List<ShopInfo> shopInfoList = shopInfo.MyShopSelectByUserid();

            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            PreviousHelper previousHelper = new PreviousHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailPrevious = previousHelper.GetPrevious();

            CancelHelper cancelHelper = new CancelHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailCancel = cancelHelper.GetCancel();

            // 這個方法即將回傳一個CarouselTemplate的List
            List<CarouselTemplate> carouselTemplatesList = new List<CarouselTemplate>();
            //int NextBtnQID = Recongnize(QID, OID);
            //OID = 3;//調整因上一步導致OID換成0
            int buttonNumber = 0; // 已經製造出的button數，最後應該跟UserID下的社團數量一樣多
            int buttonIndex = 0;

            int carouselNumber = 1; // 共要發出的Carousel訊息數量

            // 總社團數量+3個功能button，再除以30來決定總共需要發出幾則Carousel訊息
            // 如果button數量大於30則carouselNumber無條件進位(等於是需要多發出一個訊息)
            double doubleTemp = (Convert.ToDouble(shopInfoList.Count) + 3) / 30;
            if (doubleTemp > 1.0)
            {
                carouselNumber = Convert.ToInt32(Math.Ceiling(doubleTemp));
            }

            double bubbleCount = Convert.ToDouble(shopInfoList.Count) / 3;
            if (bubbleCount > 1.0)
            {
                bubbleCount = Convert.ToInt32(Math.Ceiling(bubbleCount));
            }
            else
            {
                bubbleCount = 1;
            }

            //抓取廣告圖片
            AdImage adImage = new AdImage();
            List<string> adImages = adImage.SelectAllAdUri();
            ShuffleImage(adImages);//洗亂廣告圖片
            if (bubbleCount > adImages.Count)
            {
                int NeedAddcount = (int)bubbleCount - adImages.Count;
                for (int i = 0; i <= NeedAddcount; i++)
                {
                    adImages.Add(adImages[i]);
                }
            }
            //////





            // 開始製造carousel
            for (int i = 0; i < carouselNumber; i++)
            {
                CarouselTemplate carouselTemplate = new CarouselTemplate();

                int buttonTemplateTemp;
                if ((shopInfoList.Count - buttonIndex) / 3 < 1)  // 如果button數目少於三的話，還是給出一片buttonTemplate
                {
                    buttonTemplateTemp = 1;
                }
                else
                {
                    double temp = (Convert.ToDouble(shopInfoList.Count) - buttonIndex) / 3;
                    buttonTemplateTemp = Convert.ToInt32(Math.Ceiling(temp));
                    if (buttonTemplateTemp > 10)
                    {
                        buttonTemplateTemp = 10;
                    }
                }

                // 每片buttonTemplate的內容配置
                if (shopInfoList.Count == buttonIndex)
                {
                    carouselTemplatesList.Add(carouselTemplate);
                }
                else
                {
                    for (int j = 0; j < buttonTemplateTemp; j++)
                    {
                        var actions = new List<TemplateActionBase>();
                        string labeltemp;
                        string datatemp;

                        actions.Add(new PostbackAction()
                        {
                            label = shopInfoList[buttonIndex].ShopName,
                            data = DateTime.Now.ToString()
                                    + "," + questionDetailNext.QID
                                    + "," + questionDetailNext.OID
                                    + "," + shopInfoList[buttonIndex].ShopID
                        });
                        buttonIndex++;
                        buttonNumber++;

                        if (shopInfoList.Count <= buttonNumber)
                        {
                            labeltemp = "   ";
                            datatemp = "   ";
                        }
                        else
                        {
                            labeltemp = shopInfoList[buttonIndex].ShopName;
                            datatemp = DateTime.Now.ToString() + "," + questionDetailNext.QID + "," + questionDetailNext.OID + "," + shopInfoList[buttonIndex].ShopID;
                            buttonIndex++;
                        }
                        actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                        buttonNumber++;

                        if (shopInfoList.Count <= buttonNumber)
                        {
                            labeltemp = "   ";
                            datatemp = "   ";
                        }
                        else
                        {
                            labeltemp = shopInfoList[buttonIndex].ShopName;
                            datatemp = DateTime.Now.ToString() + "," + questionDetailNext.QID + "," + questionDetailNext.OID + "," + shopInfoList[buttonIndex].ShopID;
                            buttonIndex++;
                        }
                        actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                        buttonNumber++;

                        var Column = new Column
                        {
                            // title = "選擇我的商店",
                            text = "選擇我的商店",
                            thumbnailImageUrl = new Uri($"{adImages[j]}"),
                            actions = actions
                        };
                        carouselTemplate.columns.Add(Column);
                    }
                    carouselTemplatesList.Add(carouselTemplate);
                }
            }

            var actionsFunction = new List<TemplateActionBase>();
            actionsFunction.Add(new PostbackAction() { label = "上一步", data = DateTime.Now.ToString() + $",{questionDetailPrevious.QID},{questionDetailPrevious.OID},default" });
            actionsFunction.Add(new PostbackAction() { label = "取消", data = DateTime.Now.ToString() + $",{questionDetailCancel.QID},{questionDetailCancel.OID},default" });
            actionsFunction.Add(new PostbackAction() { label = "   ", data = DateTime.Now.ToString() + ",,," });

            var ColumnFunction = new Column
            {
                // title = "這是功能片",
                text = "選擇功能",
                thumbnailImageUrl = new Uri(carouselFCImageUri),
                actions = actionsFunction
            };
            carouselTemplatesList[carouselNumber - 1].columns.Add(ColumnFunction);

            return carouselTemplatesList;
        }

        private static void ShuffleImage(List<string> adImages)
        {
            Random random = new Random();
            for (int i = 0; i < adImages.Count; i++)
            {
                string tmp = "";
                int Change = random.Next(i, adImages.Count);
                tmp = adImages[i];
                adImages[i] = adImages[Change];
                adImages[Change] = tmp;

            }
        }

        internal static List<CarouselTemplate> MakeSearchingMyShopItem(int QIDnow, int OIDnow, string UserID)
        {

            string carouselImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/scr.png";
            string carouselFCImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/funcr.png";


            switch (QIDnow)
            {
                case 194:
                    carouselImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/sir.png";
                    //          carouselFCImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/funcb.png";
                    break;

                default:
                    break;
            }


            // 拿到存在ShopTemp裡的ShopID
            ShopTemp shopTemp = new ShopTemp(UserID);
            shopTemp.SelectByUserID();

            // 再用ShopID到ShopInfo，找出資料庫裡的Item和ItemPrice
            ShopInfo shopInfo = new ShopInfo();
            shopInfo.ShopID = shopTemp.ShopID;
            List<ShopInfo> shopInfoList = shopInfo.SelectItemByMyShopID();

            // 取得button裡存的Postback資料
            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            PreviousHelper previousHelper = new PreviousHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailPrevious = previousHelper.GetPrevious();

            CancelHelper cancelHelper = new CancelHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailCancel = cancelHelper.GetCancel();

            // 這個方法即將回傳一個CarouselTemplate的List
            List<CarouselTemplate> carouselTemplatesList = new List<CarouselTemplate>();
            //int NextBtnQID = Recongnize(QID, OID);

            int buttonNumber = 0; // 已經製造出的button數，最後應該跟UserID下的社團數量一樣多
            int buttonIndex = 0;

            int carouselNumber = 1; // 共要發出的Carousel訊息數量

            // 總社團數量+3個功能button，再除以30來決定總共需要發出幾則Carousel訊息
            // 如果button數量大於30則carouselNumber無條件進位(等於是需要多發出一個訊息)
            double doubleTemp = (Convert.ToDouble(shopInfoList.Count) + 3) / 30;
            if (doubleTemp > 1.0)
            {
                carouselNumber = Convert.ToInt32(Math.Ceiling(doubleTemp));
            }

            // 開始製造carousel
            for (int i = 0; i < carouselNumber; i++)
            {
                CarouselTemplate carouselTemplate = new CarouselTemplate();

                int buttonTemplateTemp;
                if ((shopInfoList.Count - buttonIndex) / 3 < 1)  // 如果button數目少於三的話，還是給出一片buttonTemplate
                {
                    buttonTemplateTemp = 1;
                }
                else
                {
                    double temp = (Convert.ToDouble(shopInfoList.Count) - buttonIndex) / 3;
                    buttonTemplateTemp = Convert.ToInt32(Math.Ceiling(temp));
                    if (buttonTemplateTemp > 10)
                    {
                        buttonTemplateTemp = 10;
                    }
                }

                // 每片buttonTemplate的內容配置
                if (shopInfoList.Count == buttonIndex)
                {
                    carouselTemplatesList.Add(carouselTemplate);
                }
                else
                {
                    for (int j = 0; j < buttonTemplateTemp; j++)
                    {
                        var actions = new List<TemplateActionBase>();
                        string labeltemp;
                        string datatemp;

                        actions.Add(new PostbackAction()
                        {
                            label = $"{shopInfoList[buttonIndex].ShopItem}　　{shopInfoList[buttonIndex].ShopItemPrice}TWD",
                            data = DateTime.Now.ToString()
                                    + "," + questionDetailNext.QID
                                    + "," + questionDetailNext.OID
                                    + "," + shopInfoList[buttonIndex].ShopItem
                        });
                        buttonIndex++;
                        buttonNumber++;

                        if (shopInfoList.Count <= buttonNumber)
                        {
                            labeltemp = "   ";
                            datatemp = "   ";
                        }
                        else
                        {
                            labeltemp = $"{shopInfoList[buttonIndex].ShopItem}　　{shopInfoList[buttonIndex].ShopItemPrice}TWD";
                            datatemp = DateTime.Now.ToString()
                                + "," + questionDetailNext.QID
                                + "," + questionDetailNext.OID
                                + "," + shopInfoList[buttonIndex].ShopItem;
                            buttonIndex++;
                        }
                        actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                        buttonNumber++;

                        if (shopInfoList.Count <= buttonNumber)
                        {
                            labeltemp = "   ";
                            datatemp = "   ";
                        }
                        else
                        {
                            labeltemp = $"{shopInfoList[buttonIndex].ShopItem}　　{shopInfoList[buttonIndex].ShopItemPrice}TWD";
                            datatemp = DateTime.Now.ToString()
                                + "," + questionDetailNext.QID
                                + "," + questionDetailNext.OID
                                + "," + shopInfoList[buttonIndex].ShopItem;
                            buttonIndex++;
                        }
                        actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                        buttonNumber++;

                        var Column = new Column
                        {
                            //  title = "選擇我的商店的品項",
                            text = "選擇品項",
                            thumbnailImageUrl = new Uri(carouselImageUri),
                            actions = actions
                        };
                        carouselTemplate.columns.Add(Column);
                    }
                    carouselTemplatesList.Add(carouselTemplate);
                }
            }

            var actionsFunction = new List<TemplateActionBase>();
            actionsFunction.Add(new PostbackAction() { label = "上一步", data = DateTime.Now.ToString() + $",{questionDetailPrevious.QID},{questionDetailPrevious.OID},default" });
            actionsFunction.Add(new PostbackAction() { label = "取消", data = DateTime.Now.ToString() + $",{questionDetailCancel.QID},{questionDetailCancel.OID},default" });
            actionsFunction.Add(new PostbackAction() { label = "   ", data = DateTime.Now.ToString() + ",,," });

            var ColumnFunction = new Column
            {
                // title = "這是功能片",
                text = "選擇功能",
                thumbnailImageUrl = new Uri(carouselFCImageUri),
                actions = actionsFunction
            };
            carouselTemplatesList[carouselNumber - 1].columns.Add(ColumnFunction);

            return carouselTemplatesList;
        }

        internal static List<CarouselTemplate> MakeSearchingClubShopItem(int QIDnow, int OIDnow, string UserID)
        {

            string carouselImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/scr.png";
            string carouselFCImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/funcr.png";


            switch (QIDnow)
            {
                case 294:
                    carouselImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/sir.png";
                    //          carouselFCImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/funcb.png";
                    break;

                default:
                    break;
            }


            // 拿到存在ShopTemp裡的ShopID
            ShopTemp shopTemp = new ShopTemp(UserID);
            shopTemp.SelectByUserID();

            // 再用ShopID到ShopInfo，找出資料庫裡的Item和ItemPrice
            ShopInfo shopInfo = new ShopInfo();
            shopInfo.ShopID = shopTemp.ShopID;
            List<ShopInfo> shopInfoList = shopInfo.SelectItemByClubShopID();

            // 取得button裡存的Postback資料
            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            PreviousHelper previousHelper = new PreviousHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailPrevious = previousHelper.GetPrevious();

            CancelHelper cancelHelper = new CancelHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailCancel = cancelHelper.GetCancel();

            // 這個方法即將回傳一個CarouselTemplate的List
            List<CarouselTemplate> carouselTemplatesList = new List<CarouselTemplate>();
            //int NextBtnQID = Recongnize(QID, OID);

            int buttonNumber = 0; // 已經製造出的button數，最後應該跟UserID下的社團數量一樣多
            int buttonIndex = 0;

            int carouselNumber = 1; // 共要發出的Carousel訊息數量

            // 總社團數量+3個功能button，再除以30來決定總共需要發出幾則Carousel訊息
            // 如果button數量大於30則carouselNumber無條件進位(等於是需要多發出一個訊息)
            double doubleTemp = (Convert.ToDouble(shopInfoList.Count) + 3) / 30;
            if (doubleTemp > 1.0)
            {
                carouselNumber = Convert.ToInt32(Math.Ceiling(doubleTemp));
            }

            // 開始製造carousel
            for (int i = 0; i < carouselNumber; i++)
            {
                CarouselTemplate carouselTemplate = new CarouselTemplate();

                int buttonTemplateTemp;
                if ((shopInfoList.Count - buttonIndex) / 3 < 1)  // 如果button數目少於三的話，還是給出一片buttonTemplate
                {
                    buttonTemplateTemp = 1;
                }
                else
                {
                    double temp = (Convert.ToDouble(shopInfoList.Count) - buttonIndex) / 3;
                    buttonTemplateTemp = Convert.ToInt32(Math.Ceiling(temp));
                    if (buttonTemplateTemp > 10)
                    {
                        buttonTemplateTemp = 10;
                    }
                }

                // 每片buttonTemplate的內容配置
                if (shopInfoList.Count == buttonIndex)
                {
                    carouselTemplatesList.Add(carouselTemplate);
                }
                else
                {
                    for (int j = 0; j < buttonTemplateTemp; j++)
                    {
                        var actions = new List<TemplateActionBase>();
                        string labeltemp;
                        string datatemp;

                        actions.Add(new PostbackAction()
                        {
                            label = $"{shopInfoList[buttonIndex].ShopItem}　　{shopInfoList[buttonIndex].ShopItemPrice}TWD",
                            data = DateTime.Now.ToString()
                                    + "," + questionDetailNext.QID
                                    + "," + questionDetailNext.OID
                                    + "," + shopInfoList[buttonIndex].ShopItem
                        });
                        buttonIndex++;
                        buttonNumber++;

                        if (shopInfoList.Count <= buttonNumber)
                        {
                            labeltemp = "   ";
                            datatemp = "   ";
                        }
                        else
                        {
                            labeltemp = $"{shopInfoList[buttonIndex].ShopItem}　　{shopInfoList[buttonIndex].ShopItemPrice}TWD";
                            datatemp = DateTime.Now.ToString()
                                + "," + questionDetailNext.QID
                                + "," + questionDetailNext.OID
                                + "," + shopInfoList[buttonIndex].ShopItem;
                            buttonIndex++;
                        }
                        actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                        buttonNumber++;

                        if (shopInfoList.Count <= buttonNumber)
                        {
                            labeltemp = "   ";
                            datatemp = "   ";
                        }
                        else
                        {
                            labeltemp = $"{shopInfoList[buttonIndex].ShopItem}　　{shopInfoList[buttonIndex].ShopItemPrice}TWD";
                            datatemp = DateTime.Now.ToString()
                                + "," + questionDetailNext.QID
                                + "," + questionDetailNext.OID
                                + "," + shopInfoList[buttonIndex].ShopItem;
                            buttonIndex++;
                        }
                        actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                        buttonNumber++;

                        var Column = new Column
                        {
                            // title = "選擇社團商店的品項",
                            text = "選擇品項",
                            thumbnailImageUrl = new Uri(carouselImageUri),
                            actions = actions
                        };
                        carouselTemplate.columns.Add(Column);
                    }
                    carouselTemplatesList.Add(carouselTemplate);
                }
            }

            var actionsFunction = new List<TemplateActionBase>();
            actionsFunction.Add(new PostbackAction() { label = "上一步", data = DateTime.Now.ToString() + $",{questionDetailPrevious.QID},{questionDetailPrevious.OID},default" });
            actionsFunction.Add(new PostbackAction() { label = "取消", data = DateTime.Now.ToString() + $",{questionDetailCancel.QID},{questionDetailCancel.OID},default" });
            actionsFunction.Add(new PostbackAction() { label = "   ", data = DateTime.Now.ToString() + ",,," });

            var ColumnFunction = new Column
            {
                // title = "這是功能片",
                text = "選擇功能",
                thumbnailImageUrl = new Uri(carouselFCImageUri),
                actions = actionsFunction
            };
            carouselTemplatesList[carouselNumber - 1].columns.Add(ColumnFunction);

            return carouselTemplatesList;
        }

        internal static List<CarouselTemplate> MakeSearchingClubShop(int QIDnow, int OIDnow, string UserID, string ClubID)
        {
            string carouselImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/scb.png";
            string carouselFCImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/funcb.png";


            switch (QIDnow)
            {
                case 272:
                    carouselImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/scr.png";
                    carouselFCImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/funcr.png";
                    break;

                default:
                    break;
            }

            ShopInfo shopInfo = new ShopInfo(ClubID);
            List<ShopInfo> shopInfoList = shopInfo.ClubShopSelectByClubID();

            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            PreviousHelper previousHelper = new PreviousHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailPrevious = previousHelper.GetPrevious();

            CancelHelper cancelHelper = new CancelHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailCancel = cancelHelper.GetCancel();

            // 這個方法即將回傳一個CarouselTemplate的List
            List<CarouselTemplate> carouselTemplatesList = new List<CarouselTemplate>();


            int buttonNumber = 0; // 已經製造出的button數，最後應該跟UserID下的社團數量一樣多
            int buttonIndex = 0;

            int carouselNumber = 1; // 共要發出的Carousel訊息數量

            // 總社團數量+3個功能button，再除以30來決定總共需要發出幾則Carousel訊息
            // 如果button數量大於30則carouselNumber無條件進位(等於是需要多發出一個訊息)
            double doubleTemp = (Convert.ToDouble(shopInfoList.Count) + 3) / 30;
            if (doubleTemp > 1.0)
            {
                carouselNumber = Convert.ToInt32(Math.Ceiling(doubleTemp));
            }

            // 開始製造carousel
            for (int i = 0; i < carouselNumber; i++)
            {
                CarouselTemplate carouselTemplate = new CarouselTemplate();

                int buttonTemplateTemp;
                if ((shopInfoList.Count - buttonIndex) / 3 < 1)  // 如果button數目少於三的話，還是給出一片buttonTemplate
                {
                    buttonTemplateTemp = 1;
                }
                else
                {
                    double temp = (Convert.ToDouble(shopInfoList.Count) - buttonIndex) / 3;
                    buttonTemplateTemp = Convert.ToInt32(Math.Ceiling(temp));
                    if (buttonTemplateTemp > 10)
                    {
                        buttonTemplateTemp = 10;
                    }
                }

                // 每片buttonTemplate的內容配置
                if (shopInfoList.Count == buttonIndex)
                {
                    carouselTemplatesList.Add(carouselTemplate);
                }
                else
                {
                    for (int j = 0; j < buttonTemplateTemp; j++)
                    {
                        var actions = new List<TemplateActionBase>();
                        string labeltemp;
                        string datatemp;

                        actions.Add(new PostbackAction()
                        {
                            label = shopInfoList[buttonIndex].ShopName,
                            data = DateTime.Now.ToString()
                                    + "," + questionDetailNext.QID
                                    + "," + questionDetailNext.OID
                                    + "," + shopInfoList[buttonIndex].ShopID
                        });
                        buttonIndex++;
                        buttonNumber++;

                        if (shopInfoList.Count <= buttonNumber)
                        {
                            labeltemp = "   ";
                            datatemp = "   ";
                        }
                        else
                        {
                            labeltemp = shopInfoList[buttonIndex].ShopName;
                            datatemp = DateTime.Now.ToString() + "," + questionDetailNext.QID + "," + questionDetailNext.OID + "," + shopInfoList[buttonIndex].ShopID;
                            buttonIndex++;
                        }
                        actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                        buttonNumber++;

                        if (shopInfoList.Count <= buttonNumber)
                        {
                            labeltemp = "   ";
                            datatemp = "   ";
                        }
                        else
                        {
                            labeltemp = shopInfoList[buttonIndex].ShopName;
                            datatemp = DateTime.Now.ToString() + "," + questionDetailNext.QID + "," + questionDetailNext.OID + "," + shopInfoList[buttonIndex].ShopID;
                            buttonIndex++;
                        }
                        actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                        buttonNumber++;

                        var Column = new Column
                        {
                            // title = "選擇社團商店",
                            text = "選擇社團商店",
                            thumbnailImageUrl = new Uri(carouselImageUri),
                            actions = actions
                        };
                        carouselTemplate.columns.Add(Column);
                    }
                    carouselTemplatesList.Add(carouselTemplate);
                }
            }

            var actionsFunction = new List<TemplateActionBase>();
            actionsFunction.Add(new PostbackAction() { label = "上一步", data = DateTime.Now.ToString() + $",{questionDetailPrevious.QID},{questionDetailPrevious.OID},default" });
            actionsFunction.Add(new PostbackAction() { label = "取消", data = DateTime.Now.ToString() + $",{questionDetailCancel.QID},{questionDetailCancel.OID},default" });
            actionsFunction.Add(new PostbackAction() { label = "   ", data = DateTime.Now.ToString() + ",,," });

            var ColumnFunction = new Column
            {
                // title = "這是功能片",
                text = "選擇功能",
                thumbnailImageUrl = new Uri(carouselFCImageUri),
                actions = actionsFunction
            };
            carouselTemplatesList[carouselNumber - 1].columns.Add(ColumnFunction);

            return carouselTemplatesList;
        }

        internal static List<CarouselTemplate> MakeSearchingClubShopForClubOrder(int QIDnow, int OIDnow, string UserID)
        {
            // 給UserID即可選出UserID底下所有的社團資訊
            OrderTemp orderTmp = new OrderTemp(UserID);
            orderTmp.ClubIDSelectByUserID();

            ShopInfo shopInfo = new ShopInfo(orderTmp.ClubID);
            List<ShopInfo> shopInfoList = shopInfo.ClubShopSelectByClubID();

            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            PreviousHelper previousHelper = new PreviousHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailPrevious = previousHelper.GetPrevious();

            CancelHelper cancelHelper = new CancelHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailCancel = cancelHelper.GetCancel();

            // 這個方法即將回傳一個CarouselTemplate的List
            List<CarouselTemplate> carouselTemplatesList = new List<CarouselTemplate>();
            //int NextBtnQID = Recongnize(QID, OID);
            //OID = 3;//調整因上一步導致OID換成0
            int buttonNumber = 0; // 已經製造出的button數，最後應該跟UserID下的社團數量一樣多
            int buttonIndex = 0;

            int carouselNumber = 1; // 共要發出的Carousel訊息數量

            // 總社團數量+3個功能button，再除以30來決定總共需要發出幾則Carousel訊息
            // 如果button數量大於30則carouselNumber無條件進位(等於是需要多發出一個訊息)
            double doubleTemp = (Convert.ToDouble(shopInfoList.Count) + 3) / 30;
            if (doubleTemp > 1.0)
            {
                carouselNumber = Convert.ToInt32(Math.Ceiling(doubleTemp));
            }
            double bubbleCount = Convert.ToDouble(shopInfoList.Count) / 3;
            if (bubbleCount > 1.0)
            {
                bubbleCount = Convert.ToInt32(Math.Ceiling(bubbleCount));
            }
            else
            {
                bubbleCount = 1;
            }

            //抓取廣告圖片
            AdImage adImage = new AdImage();
            List<string> adImages = adImage.SelectAllAdUri();
            ShuffleImage(adImages);//洗亂廣告圖片
            if (bubbleCount > adImages.Count)
            {
                int NeedAddcount = (int)bubbleCount - adImages.Count;
                for (int i = 0; i <= NeedAddcount; i++)
                {
                    adImages.Add(adImages[i]);
                }
            }
            //////

            // 開始製造carousel
            for (int i = 0; i < carouselNumber; i++)
            {
                CarouselTemplate carouselTemplate = new CarouselTemplate();

                int buttonTemplateTemp;
                if ((shopInfoList.Count - buttonIndex) / 3 < 1)  // 如果button數目少於三的話，還是給出一片buttonTemplate
                {
                    buttonTemplateTemp = 1;
                }
                else
                {
                    double temp = (Convert.ToDouble(shopInfoList.Count) - buttonIndex) / 3;
                    buttonTemplateTemp = Convert.ToInt32(Math.Ceiling(temp));
                    if (buttonTemplateTemp > 10)
                    {
                        buttonTemplateTemp = 10;
                    }
                }

                // 每片buttonTemplate的內容配置
                if (shopInfoList.Count == buttonIndex)
                {
                    carouselTemplatesList.Add(carouselTemplate);
                }
                else
                {
                    for (int j = 0; j < buttonTemplateTemp; j++)
                    {
                        var actions = new List<TemplateActionBase>();
                        string labeltemp;
                        string datatemp;

                        actions.Add(new PostbackAction()
                        {
                            label = shopInfoList[buttonIndex].ShopName,
                            data = DateTime.Now.ToString()
                                    + "," + questionDetailNext.QID
                                    + "," + questionDetailNext.OID
                                    + "," + shopInfoList[buttonIndex].ShopID
                        });
                        buttonIndex++;
                        buttonNumber++;

                        if (shopInfoList.Count <= buttonNumber)
                        {
                            labeltemp = "   ";
                            datatemp = "   ";
                        }
                        else
                        {
                            labeltemp = shopInfoList[buttonIndex].ShopName;
                            datatemp = DateTime.Now.ToString() + "," + questionDetailNext.QID + "," + questionDetailNext.OID + "," + shopInfoList[buttonIndex].ShopID;
                            buttonIndex++;
                        }
                        actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                        buttonNumber++;

                        if (shopInfoList.Count <= buttonNumber)
                        {
                            labeltemp = "   ";
                            datatemp = "   ";
                        }
                        else
                        {
                            labeltemp = shopInfoList[buttonIndex].ShopName;
                            datatemp = DateTime.Now.ToString() + "," + questionDetailNext.QID + "," + questionDetailNext.OID + "," + shopInfoList[buttonIndex].ShopID;
                            buttonIndex++;
                        }
                        actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                        buttonNumber++;

                        var Column = new Column
                        {
                            // title = "選擇社團商店",
                            text = "選擇社團商店",
                            thumbnailImageUrl = new Uri($"{adImages[j]}"),
                            actions = actions
                        };
                        carouselTemplate.columns.Add(Column);
                    }
                    carouselTemplatesList.Add(carouselTemplate);
                }
            }

            var actionsFunction = new List<TemplateActionBase>();
            actionsFunction.Add(new PostbackAction() { label = "上一步", data = DateTime.Now.ToString() + $",{questionDetailPrevious.QID},{questionDetailPrevious.OID},default" });
            actionsFunction.Add(new PostbackAction() { label = "取消", data = DateTime.Now.ToString() + $",{questionDetailCancel.QID},{questionDetailCancel.OID},default" });
            actionsFunction.Add(new PostbackAction() { label = "   ", data = DateTime.Now.ToString() + ",,," });

            var ColumnFunction = new Column
            {
                // title = "這是功能片",
                text = "選擇功能",
                thumbnailImageUrl = new Uri("https://i220.photobucket.com/albums/dd130/jung_04/funcb.png"),
                actions = actionsFunction
            };
            carouselTemplatesList[carouselNumber - 1].columns.Add(ColumnFunction);

            return carouselTemplatesList;
        }

        internal static List<CarouselTemplate> MakeSearchingBossShop(int QIDnow, int OIDnow)
        {
            // 給UserID即可選出UserID底下所有的社團資訊
            ShopInfo shopInfo = new ShopInfo();
            List<ShopInfo> shopInfoList = shopInfo.BossShopSelectByUserID();

            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            PreviousHelper previousHelper = new PreviousHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailPrevious = previousHelper.GetPrevious();

            CancelHelper cancelHelper = new CancelHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailCancel = cancelHelper.GetCancel();

            // 這個方法即將回傳一個CarouselTemplate的List
            List<CarouselTemplate> carouselTemplatesList = new List<CarouselTemplate>();
            //int NextBtnQID = Recongnize(QID, OID);
            //OID = 3;//調整因上一步導致OID換成0
            int buttonNumber = 0; // 已經製造出的button數，最後應該跟UserID下的社團數量一樣多
            int buttonIndex = 0;

            int carouselNumber = 1; // 共要發出的Carousel訊息數量

            // 總社團數量+3個功能button，再除以30來決定總共需要發出幾則Carousel訊息
            // 如果button數量大於30則carouselNumber無條件進位(等於是需要多發出一個訊息)
            double doubleTemp = (Convert.ToDouble(shopInfoList.Count) + 3) / 30;
            if (doubleTemp > 1.0)
            {
                carouselNumber = Convert.ToInt32(Math.Ceiling(doubleTemp));
            }

            double bubbleCount = Convert.ToDouble(shopInfoList.Count) / 3;
            if (bubbleCount > 1.0)
            {
                bubbleCount = Convert.ToInt32(Math.Ceiling(bubbleCount));
            }
            else
            {
                bubbleCount = 1;
            }

            //抓取廣告圖片
            AdImage adImage = new AdImage();
            List<string> adImages = adImage.SelectAllAdUri();
            ShuffleImage(adImages);//洗亂廣告圖片
            if (bubbleCount > adImages.Count)
            {
                int NeedAddcount = (int)bubbleCount - adImages.Count;
                for (int i = 0; i <= NeedAddcount; i++)
                {
                    adImages.Add(adImages[i]);
                }
            }
            //////

            // 開始製造carousel
            for (int i = 0; i < carouselNumber; i++)
            {
                CarouselTemplate carouselTemplate = new CarouselTemplate();

                int buttonTemplateTemp;
                if ((shopInfoList.Count - buttonIndex) / 3 < 1)  // 如果button數目少於三的話，還是給出一片buttonTemplate
                {
                    buttonTemplateTemp = 1;
                }
                else
                {
                    double temp = (Convert.ToDouble(shopInfoList.Count) - buttonIndex) / 3;
                    buttonTemplateTemp = Convert.ToInt32(Math.Ceiling(temp));
                    if (buttonTemplateTemp > 10)
                    {
                        buttonTemplateTemp = 10;
                    }
                }

                // 每片buttonTemplate的內容配置
                if (shopInfoList.Count == buttonIndex)
                {
                    carouselTemplatesList.Add(carouselTemplate);
                }
                else
                {
                    for (int j = 0; j < buttonTemplateTemp; j++)
                    {
                        var actions = new List<TemplateActionBase>();
                        string labeltemp;
                        string datatemp;

                        actions.Add(new PostbackAction()
                        {
                            label = shopInfoList[buttonIndex].ShopName,
                            data = DateTime.Now.ToString()
                                    + "," + questionDetailNext.QID
                                    + "," + questionDetailNext.OID
                                    + "," + shopInfoList[buttonIndex].ShopID
                        });
                        buttonIndex++;
                        buttonNumber++;

                        if (shopInfoList.Count <= buttonNumber)
                        {
                            labeltemp = "   ";
                            datatemp = "   ";
                        }
                        else
                        {
                            labeltemp = shopInfoList[buttonIndex].ShopName;
                            datatemp = DateTime.Now.ToString() + "," + questionDetailNext.QID + "," + questionDetailNext.OID + "," + shopInfoList[buttonIndex].ShopID;
                            buttonIndex++;
                        }
                        actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                        buttonNumber++;

                        if (shopInfoList.Count <= buttonNumber)
                        {
                            labeltemp = "   ";
                            datatemp = "   ";
                        }
                        else
                        {
                            labeltemp = shopInfoList[buttonIndex].ShopName;
                            datatemp = DateTime.Now.ToString() + "," + questionDetailNext.QID + "," + questionDetailNext.OID + "," + shopInfoList[buttonIndex].ShopID;
                            buttonIndex++;
                        }
                        actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                        buttonNumber++;

                        var Column = new Column
                        {
                            text = "選擇合作商店",
                            thumbnailImageUrl = new Uri($"{adImages[j]}"),
                            actions = actions
                        };
                        carouselTemplate.columns.Add(Column);
                    }
                    carouselTemplatesList.Add(carouselTemplate);
                }
            }

            var actionsFunction = new List<TemplateActionBase>();
            actionsFunction.Add(new PostbackAction() { label = "上一步", data = DateTime.Now.ToString() + $",{questionDetailPrevious.QID},{questionDetailPrevious.OID},default" });
            actionsFunction.Add(new PostbackAction() { label = "取消", data = DateTime.Now.ToString() + $",{questionDetailCancel.QID},{questionDetailCancel.OID},default" });
            actionsFunction.Add(new PostbackAction() { label = "   ", data = DateTime.Now.ToString() + ",,," });

            var ColumnFunction = new Column
            {
                text = "選擇功能",
                thumbnailImageUrl = new Uri("https://i220.photobucket.com/albums/dd130/jung_04/funcb.png"),
                actions = actionsFunction
            };
            carouselTemplatesList[carouselNumber - 1].columns.Add(ColumnFunction);

            return carouselTemplatesList;
        }

        internal static List<CarouselTemplate> MakeSearchingItem(int QIDnow, int OIDnow, string UserID, string BuyerInfoID)
        {
            string carouselImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/soy.png";
            string carouselFCImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/funcy.png";
            switch (QIDnow)
            {

                case 121:
                    switch (OIDnow)
                    {
                        case 1:
                             carouselImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/siy.png";
                             carouselFCImageUri = "https://i220.photobucket.com/albums/dd130/jung_04/funcy.png";
                            break;
                        default:
                            break;
                    }
                    break;

            }
            // 從OrderTemp裡面拿到OrderPartitionID
            OrderTemp orderTemp = new OrderTemp(UserID);
            orderTemp.SelectByUserID();

            // 用OrderPartitionID去找到ShopID，再用ShopID找到ShopItem
            OrderInfo orderInfo = new OrderInfo(UserID);
            orderInfo.OrderPartitionID = orderTemp.OrderPartitionID;


            // 這個List最後是拿來做carousel的
            List<ShopItem> shopItemList = new List<ShopItem>();

            // 擷取OrderPartitionID前兩個字辨別是MyOrder還是ClubOrder
            string OrderPartitionIDSplitted = orderTemp.OrderPartitionID.Substring(0, 2);

            switch (OrderPartitionIDSplitted)
            {
                case "MO": // MyOrder
                    orderInfo.SelectMyOrderTableByOrderPartitionID();
                    shopItemList = GetShopItemList(orderInfo.ShopID);
                    break;

                case "CO": // ClubOrder
                    orderInfo.SelectClubOrderTableByOrderPartitionID();
                    shopItemList = GetShopItemList(orderInfo.ShopID);
                    break;

                default:
                    break;
            }

            // 這個方法即將回傳一個CarouselTemplate的List
            List<CarouselTemplate> carouselTemplatesList = new List<CarouselTemplate>();

            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            PreviousHelper previousHelper = new PreviousHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailPrevious = previousHelper.GetPrevious();

            CancelHelper cancelHelper = new CancelHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailCancel = cancelHelper.GetCancel();

            int buttonNumber = 0; // 已經製造出的button數，最後應該跟UserID下的社團數量一樣多
            int buttonIndex = 0;

            int carouselNumber = 1; // 共要發出的Carousel訊息數量

            // 總社團數量+3個功能button，再除以30來決定總共需要發出幾則Carousel訊息
            // 如果button數量大於30則carouselNumber無條件進位(等於是需要多發出一個訊息)
            double doubleTemp = (Convert.ToDouble(shopItemList.Count) + 3) / 30;
            if (doubleTemp > 1.0)
            {
                carouselNumber = Convert.ToInt32(Math.Ceiling(doubleTemp));
            }

            // 開始製造carousel
            for (int i = 0; i < carouselNumber; i++)
            {
                CarouselTemplate carouselTemplate = new CarouselTemplate();

                int buttonTemplateTemp;
                if ((shopItemList.Count - buttonIndex) / 3 < 1)  // 如果button數目少於三的話，還是給出一片buttonTemplate
                {
                    buttonTemplateTemp = 1;
                }
                else
                {
                    double temp = (Convert.ToDouble(shopItemList.Count) - buttonIndex) / 3;
                    buttonTemplateTemp = Convert.ToInt32(Math.Ceiling(temp));
                    if (buttonTemplateTemp > 10)
                    {
                        buttonTemplateTemp = 10;
                    }
                }

                // 每片buttonTemplate的內容配置
                if (shopItemList.Count == buttonIndex)
                {
                    carouselTemplatesList.Add(carouselTemplate);
                }
                else
                {
                    for (int j = 0; j < buttonTemplateTemp; j++)
                    {
                        var actions = new List<TemplateActionBase>();
                        string labeltemp;
                        string datatemp;

                        {// 第一個button
                            actions.Add(new PostbackAction()
                            {
                                label = $"{shopItemList[buttonIndex].shopItem}({shopItemList[buttonIndex].ShopItemPrice}TWD)",
                                data = DateTime.Now.ToString()
                                        + "," + questionDetailNext.QID
                                        + "," + questionDetailNext.OID
                                        + "," + orderInfo.OrderPartitionID
                                        + "," + shopItemList[buttonIndex].shopItem
                                        + "," + shopItemList[buttonIndex].ShopItemPrice
                                        + "," + BuyerInfoID
                            });
                            buttonIndex++;
                            buttonNumber++;
                        }

                        {// 第二個button
                            if (shopItemList.Count <= buttonNumber)
                            {
                                labeltemp = "   ";
                                datatemp = "   ";
                            }
                            else
                            {
                                labeltemp = $"{shopItemList[buttonIndex].shopItem}({shopItemList[buttonIndex].ShopItemPrice}TWD)";
                                datatemp = DateTime.Now.ToString()
                                    + "," + questionDetailNext.QID
                                    + "," + questionDetailNext.OID
                                    + "," + orderInfo.OrderPartitionID
                                    + "," + shopItemList[buttonIndex].shopItem
                                    + "," + shopItemList[buttonIndex].ShopItemPrice
                                    + "," + BuyerInfoID;
                                buttonIndex++;
                            }
                            actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                            buttonNumber++;
                        }

                        { // 第三個button
                            if (shopItemList.Count <= buttonNumber)
                            {
                                labeltemp = "   ";
                                datatemp = "   ";
                            }
                            else
                            {
                                labeltemp = $"{shopItemList[buttonIndex].shopItem}({shopItemList[buttonIndex].ShopItemPrice}TWD)";
                                datatemp = DateTime.Now.ToString()
                                    + "," + questionDetailNext.QID
                                    + "," + questionDetailNext.OID
                                    + "," + orderInfo.OrderPartitionID
                                    + "," + shopItemList[buttonIndex].shopItem
                                    + "," + shopItemList[buttonIndex].ShopItemPrice
                                + "," + BuyerInfoID;

                                buttonIndex++;
                            }
                            actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                            buttonNumber++;
                        }

                        var Column = new Column
                        {
                            // title = "選擇品項",
                            text = "選擇品項",
                            thumbnailImageUrl = new Uri(carouselImageUri),
                            actions = actions
                        };
                        carouselTemplate.columns.Add(Column);
                    }
                    carouselTemplatesList.Add(carouselTemplate);
                }
            }
            var ColumnFunction = new Column();
            if (QIDnow == 9999)
            {
                var actionsFunction = new List<TemplateActionBase>();
                actionsFunction.Add(new PostbackAction() { label = "   ", data = DateTime.Now.ToString() + $",,," });
                actionsFunction.Add(new PostbackAction() { label = "結束訂購", data = DateTime.Now.ToString() + $",{questionDetailCancel.QID},{questionDetailCancel.OID},default" });
                actionsFunction.Add(new PostbackAction() { label = "   ", data = DateTime.Now.ToString() + ",,," });

                ColumnFunction = new Column
                {
                    //  title = "這是功能片",
                    text = "結束訂購(之前訂購的項目已訂購完成)",
                    thumbnailImageUrl = new Uri(carouselFCImageUri),
                    actions = actionsFunction
                };
            }
            else
            {
                var actionsFunction = new List<TemplateActionBase>();
                actionsFunction.Add(new PostbackAction() { label = "上一步", data = DateTime.Now.ToString() + $",{questionDetailPrevious.QID},{questionDetailPrevious.OID},default" });
                actionsFunction.Add(new PostbackAction() { label = "取消", data = DateTime.Now.ToString() + $",{questionDetailCancel.QID},{questionDetailCancel.OID},default" });
                actionsFunction.Add(new PostbackAction() { label = "   ", data = DateTime.Now.ToString() + ",,," });

                ColumnFunction = new Column
                {
                    //  title = "這是功能片",
                    text = "選擇功能",
                    thumbnailImageUrl = new Uri(carouselFCImageUri),
                    actions = actionsFunction
                };
            }

            carouselTemplatesList[carouselNumber - 1].columns.Add(ColumnFunction);

            return carouselTemplatesList;
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

        internal static List<CarouselTemplate> MakeSearchingBuyerItem(int QIDnow, int OIDnow, string UserID)
        {
            // 從OrderTemp裡面拿到OrderPartitionID
            OrderTemp orderTemp = new OrderTemp(UserID);
            orderTemp.SelectByUserID();

            // 去BuyerInfoTable找到已經訂的品項
            BuyerInfo buyerInfo = new BuyerInfo(UserID);
            buyerInfo.OrderPartitionID = orderTemp.OrderPartitionID;

            // 這個List最後是拿來做carousel的
            List<BuyerInfo> buyerInfoList = new List<BuyerInfo>();
            buyerInfoList = buyerInfo.GetBuyerInfoListByOrderPartitionIDandBuyerID();


            // 這個方法即將回傳一個CarouselTemplate的List
            List<CarouselTemplate> carouselTemplatesList = new List<CarouselTemplate>();

            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            PreviousHelper previousHelper = new PreviousHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailPrevious = previousHelper.GetPrevious();

            CancelHelper cancelHelper = new CancelHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailCancel = cancelHelper.GetCancel();

            int buttonNumber = 0; // 已經製造出的button數，最後應該跟UserID下的社團數量一樣多
            int buttonIndex = 0;

            int carouselNumber = 1; // 共要發出的Carousel訊息數量

            // 總社團數量+3個功能button，再除以30來決定總共需要發出幾則Carousel訊息
            // 如果button數量大於30則carouselNumber無條件進位(等於是需要多發出一個訊息)
            double doubleTemp = (Convert.ToDouble(buyerInfoList.Count) + 3) / 30;
            if (doubleTemp > 1.0)
            {
                carouselNumber = Convert.ToInt32(Math.Ceiling(doubleTemp));
            }

            // 開始製造carousel
            for (int i = 0; i < carouselNumber; i++)
            {
                CarouselTemplate carouselTemplate = new CarouselTemplate();

                int buttonTemplateTemp;
                if ((buyerInfoList.Count - buttonIndex) / 3 < 1)  // 如果button數目少於三的話，還是給出一片buttonTemplate
                {
                    buttonTemplateTemp = 1;
                }
                else
                {
                    double temp = (Convert.ToDouble(buyerInfoList.Count) - buttonIndex) / 3;
                    buttonTemplateTemp = Convert.ToInt32(Math.Ceiling(temp));
                    if (buttonTemplateTemp > 10)
                    {
                        buttonTemplateTemp = 10;
                    }
                }

                // 每片buttonTemplate的內容配置
                if (buyerInfoList.Count == buttonIndex)
                {
                    carouselTemplatesList.Add(carouselTemplate);
                }
                else
                {
                    for (int j = 0; j < buttonTemplateTemp; j++)
                    {
                        var actions = new List<TemplateActionBase>();
                        string labeltemp;
                        string datatemp;

                        {// 第一個button
                            actions.Add(new PostbackAction()
                            {
                                label = $"{buyerInfoList[buttonIndex].Item}-數量：{buyerInfoList[buttonIndex].Amount}",
                                data = DateTime.Now.ToString()
                                        + "," + questionDetailNext.QID
                                        + "," + questionDetailNext.OID
                                        + "," + buyerInfoList[buttonIndex].Item
                                        + "," + buyerInfoList[buttonIndex].ID

                            });
                            buttonIndex++;
                            buttonNumber++;
                        }

                        {// 第二個button
                            if (buyerInfoList.Count <= buttonNumber)
                            {
                                labeltemp = "   ";
                                datatemp = "   ";
                            }
                            else
                            {
                                labeltemp = $"{buyerInfoList[buttonIndex].Item}-數量：{buyerInfoList[buttonIndex].Amount}";
                                datatemp = DateTime.Now.ToString()
                                        + "," + questionDetailNext.QID
                                        + "," + questionDetailNext.OID
                                        + "," + buyerInfoList[buttonIndex].Item
                                + "," + buyerInfoList[buttonIndex].ID;

                                buttonIndex++;
                            }
                            actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                            buttonNumber++;
                        }

                        { // 第三個button
                            if (buyerInfoList.Count <= buttonNumber)
                            {
                                labeltemp = "   ";
                                datatemp = "   ";
                            }
                            else
                            {
                                labeltemp = $"{buyerInfoList[buttonIndex].Item}-數量：{buyerInfoList[buttonIndex].Amount}";
                                datatemp = DateTime.Now.ToString()
                                        + "," + questionDetailNext.QID
                                        + "," + questionDetailNext.OID
                                        + "," + buyerInfoList[buttonIndex].Item
                                + "," + buyerInfoList[buttonIndex].ID;

                                buttonIndex++;
                            }
                            actions.Add(new PostbackAction() { label = labeltemp, data = datatemp });
                            buttonNumber++;
                        }

                        var Column = new Column
                        {
                            //  title = "選擇已訂購品項",
                            text = "選擇已訂購品項",
                            thumbnailImageUrl = new Uri("https://i220.photobucket.com/albums/dd130/jung_04/siy.png"),
                            actions = actions
                        };
                        carouselTemplate.columns.Add(Column);
                    }
                    carouselTemplatesList.Add(carouselTemplate);
                }
            }

            var actionsFunction = new List<TemplateActionBase>();
            actionsFunction.Add(new PostbackAction() { label = "上一步", data = DateTime.Now.ToString() + $",{questionDetailPrevious.QID},{questionDetailPrevious.OID},default" });
            actionsFunction.Add(new PostbackAction() { label = "取消", data = DateTime.Now.ToString() + $",{questionDetailCancel.QID},{questionDetailCancel.OID},default" });
            actionsFunction.Add(new PostbackAction() { label = "   ", data = DateTime.Now.ToString() + ",,," });

            var ColumnFunction = new Column
            {
                // title = "這是功能片",
                text = "選擇功能",
                thumbnailImageUrl = new Uri("https://i220.photobucket.com/albums/dd130/jung_04/funcy.png"),
                actions = actionsFunction
            };

            carouselTemplatesList[carouselNumber - 1].columns.Add(ColumnFunction);

            return carouselTemplatesList;
        }
    }
}