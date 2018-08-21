using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using isRock.LineBot;
using OrderBot.RecoginzeManager;
using OrderBot.SQLObject;
using OrderBot.Utility;

namespace OrderBot.TemplateMakers
{
    public class ConfirmMaker
    {
        // 如果confirm button上面的字是從message得到的話，UserIDorClubIDorOrderID用不到
        internal static ConfirmTemplate MakeMessageConfirmBtn(string message, int QIDnow, int OIDnow, string UserIDorClubIDorOrderID)
        {
            var actions = new List<isRock.LineBot.TemplateActionBase>();
            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            CancelHelper cancelHelper = new CancelHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailCancel = cancelHelper.GetCancel();

            string confirmText = GetConfirmText(QIDnow, OIDnow, UserIDorClubIDorOrderID);
            if (confirmText == "")
            {
                confirmText = message;
            }

            actions.Add(new isRock.LineBot.PostbackAction() { label = "確定", data = $"{DateTime.Now},{questionDetailNext.QID},{questionDetailNext.OID}," });
            actions.Add(new isRock.LineBot.PostbackAction() { label = "取消", data = $"{DateTime.Now},{questionDetailCancel.QID},{questionDetailCancel.OID}," });
            var ConfirmTemplateMsg = new isRock.LineBot.ConfirmTemplate()
            {
                altText = "替代文字(在無法顯示Confirm Template的時候顯示)",
                text = confirmText,
                actions = actions //設定回覆動作
            };
            return ConfirmTemplateMsg;
        }


        internal static ConfirmTemplate MakeDeleteMyOnceOrderConfirmBtn(string message, int QIDnow, int OIDnow, string UserIDorClubIDorOrderID)
        {
            var actions = new List<isRock.LineBot.TemplateActionBase>();
            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            CancelHelper cancelHelper = new CancelHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailCancel = cancelHelper.GetCancel();

            string confirmText = GetConfirmText(QIDnow, OIDnow, UserIDorClubIDorOrderID);
            if (confirmText == "")
            {
                confirmText = message;
            }

            actions.Add(new isRock.LineBot.PostbackAction() { label = "確定", data = $"{DateTime.Now},{questionDetailNext.QID},{questionDetailNext.OID},MyOnceOrder" });
            actions.Add(new isRock.LineBot.PostbackAction() { label = "取消", data = $"{DateTime.Now},{questionDetailCancel.QID},{questionDetailCancel.OID}," });
            var ConfirmTemplateMsg = new isRock.LineBot.ConfirmTemplate()
            {
                altText = "替代文字(在無法顯示Confirm Template的時候顯示)",
                text = confirmText,
                actions = actions //設定回覆動作
            };
            return ConfirmTemplateMsg;
        }



        internal static ConfirmTemplate MakeCreateClubConfirmBtn(string message, int QIDnow, int OIDnow, string clubID, string clubName)
        {
            var actions = new List<isRock.LineBot.TemplateActionBase>();

            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            CancelHelper cancelHelper = new CancelHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailCancel = cancelHelper.GetCancel();

            string confirmText = GetConfirmText(QIDnow, OIDnow, clubID);
            if (confirmText == "")
            {
                confirmText = message;
            }

            //參加社團(91)和退出社團(102)需要放clubID
            if (questionDetailNext.QID == 91 || questionDetailNext.QID == 102)
            {
                actions.Add(new isRock.LineBot.PostbackAction() { label = "確定", data = $"{DateTime.Now},{questionDetailNext.QID},{questionDetailNext.OID},{clubID}" });
            }
            else
            {
                actions.Add(new isRock.LineBot.PostbackAction() { label = "確定", data = $"{DateTime.Now},{questionDetailNext.QID},{questionDetailNext.OID},{clubName}" });
            }
            actions.Add(new isRock.LineBot.PostbackAction() { label = "取消", data = $"{DateTime.Now},{questionDetailCancel.QID},{questionDetailCancel.OID}," });


            var ConfirmTemplateMsg = new isRock.LineBot.ConfirmTemplate()
            {
                altText = "替代文字(在無法顯示Confirm Template的時候顯示)",
                text = confirmText,
                actions = actions //設定回覆動作
            };
            return ConfirmTemplateMsg;
        }

        internal static ConfirmTemplate MakeCreateOrderConfirmBtn(string message, int QIDnow, int OIDnow, string OrderID, string UserID, string ChannelAccessToken)
        {
            var actions = new List<isRock.LineBot.TemplateActionBase>();
            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            CancelHelper cancelHelper = new CancelHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailCancel = cancelHelper.GetCancel();
            string confirmText = GetConfirmText(QIDnow, OIDnow, OrderID);
            //OrderTmp orderTmp = new OrderTmp(UserID);
            //orderTmp.SelectByUserID();
            //string confirmText = $@"開始時間:{orderTmp.StartTime.ToString()}\結束時間:{orderTmp.EndTime.ToString()}";
            if (confirmText == "")
            {
                confirmText = message;
            }
            actions.Add(new isRock.LineBot.PostbackAction() { label = "確定", data = $"{DateTime.Now},{questionDetailNext.QID},{questionDetailNext.OID},{OrderID}" });
            actions.Add(new isRock.LineBot.PostbackAction() { label = "取消", data = $"{DateTime.Now},{questionDetailCancel.QID},{questionDetailCancel.OID}," });
            var ConfirmTemplateMsg = new isRock.LineBot.ConfirmTemplate()
            {
                altText = "替代文字(在無法顯示Confirm Template的時候顯示)",
                text = confirmText,
                actions = actions //設定回覆動作
            };
            return ConfirmTemplateMsg;
        }

        internal static ConfirmTemplate MakeModifyOrderConfirmBtn(string message, int QIDnow, int OIDnow, string ShopID, string UserID)
        {
            var actions = new List<isRock.LineBot.TemplateActionBase>();
            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            CancelHelper cancelHelper = new CancelHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailCancel = cancelHelper.GetCancel();
            string confirmText = GetConfirmText(QIDnow, OIDnow, ShopID);
            //OrderTmp orderTmp = new OrderTmp(UserID);
            //orderTmp.SelectByUserID();
            //string confirmText = $@"開始時間:{orderTmp.StartTime.ToString()}\結束時間:{orderTmp.EndTime.ToString()}";
            if (confirmText == "")
            {
                confirmText = message;
            }
            actions.Add(new isRock.LineBot.PostbackAction() { label = "確定", data = $"{DateTime.Now},{questionDetailNext.QID},{questionDetailNext.OID},{ShopID}" });
            actions.Add(new isRock.LineBot.PostbackAction() { label = "取消", data = $"{DateTime.Now},{questionDetailCancel.QID},{questionDetailCancel.OID}," });
            var ConfirmTemplateMsg = new isRock.LineBot.ConfirmTemplate()
            {
                altText = "替代文字(在無法顯示Confirm Template的時候顯示)",
                text = confirmText,
                actions = actions //設定回覆動作
            };
            return ConfirmTemplateMsg;
        }


        internal static ConfirmTemplate MakeContinueConfirmBtn(string message, int QIDnow, int OIDnow)
        {
            var actions = new List<isRock.LineBot.TemplateActionBase>();
            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            CancelHelper cancelHelper = new CancelHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailCancel = cancelHelper.GetCancel();

            string confirmText = "";
            confirmText = GetConfirmText(QIDnow, OIDnow, "");

            if (confirmText == "")
            {
                confirmText = message;
            }
            switch (QIDnow)
            {
                case 121:
                    switch (OIDnow)
                    {
                        case 5:
                            actions.Add(new isRock.LineBot.PostbackAction() { label = "繼續修改", data = $"{DateTime.Now},{questionDetailNext.QID},{questionDetailNext.OID}," });
                            actions.Add(new isRock.LineBot.PostbackAction() { label = "結束修改", data = $"{DateTime.Now},{questionDetailCancel.QID},{questionDetailCancel.OID}," });
                            break;
                        default:
                            break;
                    }
                    break;

                case 194:
                    switch (OIDnow)
                    {
                        case 5:
                            actions.Add(new isRock.LineBot.PostbackAction() { label = "繼續修改", data = $"{DateTime.Now},{questionDetailNext.QID},{questionDetailNext.OID}," });
                            actions.Add(new isRock.LineBot.PostbackAction() { label = "結束修改", data = $"{DateTime.Now},{questionDetailCancel.QID},{questionDetailCancel.OID}," });
                            break;
                        default:
                            break;
                    }
                    break;

                case 211:
                    switch (OIDnow)
                    {
                        case 3:
                            actions.Add(new isRock.LineBot.PostbackAction() { label = "繼續選擇", data = $"{DateTime.Now},{questionDetailNext.QID},{questionDetailNext.OID}," });
                            actions.Add(new isRock.LineBot.PostbackAction() { label = "結束選擇", data = $"{DateTime.Now},{questionDetailCancel.QID},{questionDetailCancel.OID}," });
                            break;
                        default:
                            break;
                    }
                    break;

                case 251:
                    switch (OIDnow)
                    {
                        case 3:
                            actions.Add(new isRock.LineBot.PostbackAction() { label = "繼續選擇", data = $"{DateTime.Now},{questionDetailNext.QID},{questionDetailNext.OID}," });
                            actions.Add(new isRock.LineBot.PostbackAction() { label = "結束選擇", data = $"{DateTime.Now},{questionDetailCancel.QID},{questionDetailCancel.OID}," });
                            break;
                        default:
                            break;
                    }
                    break;

                case 294:
                    switch (OIDnow)
                    {
                        case 5:
                            actions.Add(new isRock.LineBot.PostbackAction() { label = "繼續修改", data = $"{DateTime.Now},{questionDetailNext.QID},{questionDetailNext.OID}," });
                            actions.Add(new isRock.LineBot.PostbackAction() { label = "結束修改", data = $"{DateTime.Now},{questionDetailCancel.QID},{questionDetailCancel.OID}," });
                            break;
                        default:
                            break;
                    }
                    break;

                case 9999:
                    switch (OIDnow)
                    {
                        case 5:
                            actions.Add(new isRock.LineBot.PostbackAction() { label = "繼續訂購", data = $"{DateTime.Now},{questionDetailNext.QID},{questionDetailNext.OID}," });
                            actions.Add(new isRock.LineBot.PostbackAction() { label = "結束訂購", data = $"{DateTime.Now},{questionDetailCancel.QID},{questionDetailCancel.OID}," });
                            break;
                        default:
                            break;
                    }
                    break;

                default:
                    actions.Add(new isRock.LineBot.PostbackAction() { label = "繼續輸入", data = $"{DateTime.Now},{questionDetailNext.QID},{questionDetailNext.OID}," });
                    actions.Add(new isRock.LineBot.PostbackAction() { label = "結束輸入", data = $"{DateTime.Now},{questionDetailCancel.QID},{questionDetailCancel.OID}," });
                    break;
            }


            var ConfirmTemplateMsg = new isRock.LineBot.ConfirmTemplate()
            {
                altText = "替代文字(在無法顯示Confirm Template的時候顯示)",
                text = confirmText,
                actions = actions //設定回覆動作
            };
            return ConfirmTemplateMsg;
        }


        internal static ConfirmTemplate MakeAmountConfirmBtn(int QIDnow, int OIDnow)
        {
            var actions = new List<isRock.LineBot.TemplateActionBase>();
            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            CancelHelper cancelHelper = new CancelHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailCancel = cancelHelper.GetCancel();

            string confirmText = "預設訂購數量為1，請問你要修改數量嗎?";

            actions.Add(new isRock.LineBot.PostbackAction() { label = "訂購一項", data = $"{DateTime.Now},{questionDetailNext.QID},{questionDetailNext.OID}," });
            actions.Add(new isRock.LineBot.PostbackAction() { label = "更改數量", data = $"{DateTime.Now},{questionDetailCancel.QID},{questionDetailCancel.OID}," });
            var ConfirmTemplateMsg = new isRock.LineBot.ConfirmTemplate()
            {
                altText = "替代文字(在無法顯示Confirm Template的時候顯示)",
                text = confirmText,
                actions = actions //設定回覆動作
            };
            return ConfirmTemplateMsg;
        }

        internal static ConfirmTemplate MakeNoteConfirmBtn(int QIDnow, int OIDnow)
        {
            var actions = new List<isRock.LineBot.TemplateActionBase>();
            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            CancelHelper cancelHelper = new CancelHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailCancel = cancelHelper.GetCancel();

            string confirmText = "請問你需要寫個備註嗎??";

            actions.Add(new isRock.LineBot.PostbackAction() { label = "不用哦~", data = $"{DateTime.Now},{questionDetailNext.QID},{questionDetailNext.OID}," });
            actions.Add(new isRock.LineBot.PostbackAction() { label = "填寫備註", data = $"{DateTime.Now},{questionDetailCancel.QID},{questionDetailCancel.OID}," });
            var ConfirmTemplateMsg = new isRock.LineBot.ConfirmTemplate()
            {
                altText = "替代文字(在無法顯示Confirm Template的時候顯示)",
                text = confirmText,
                actions = actions //設定回覆動作
            };
            return ConfirmTemplateMsg;
        }

        private static string GetConfirmText(int QID, int OID, string UserIDorClubIDorOrderID)
        {
            OrderInfo orderInfo;
            switch (QID)
            {
                case 52:
                    orderInfo = new OrderInfo();
                    orderInfo.OrderID = UserIDorClubIDorOrderID;
                    orderInfo.SelectMyOrderNameByOrderID();
                    return $"確定要刪除:{orderInfo.OrderName} 嗎???";



                case 61:
                    switch (OID)
                    {
                        case 2:
                            OrderTemp orderTemp = new OrderTemp(UserIDorClubIDorOrderID);
                            orderTemp.SelectByUserID();
                            return $"請確認將開單時間修改為：{orderTemp.StartTime.ToString("yyyy-MM-dd HH:mm")}，結單時間修改為：{orderTemp.EndTime.ToString("yyyy-MM-dd HH:mm")} 嗎???";

                        default:
                            break;
                    }
                    break;
                case 102:
                    switch (OID)
                    {
                        case 1:
                            ClubInfo clubInfo = new ClubInfo();
                            clubInfo.ClubID = UserIDorClubIDorOrderID; // 這裡用ClubID
                            return $"確定離開 {clubInfo.SelectByClubid()[0].ClubName} 嗎???";

                        default:
                            break;
                    }
                    break;

                case 152:
                    orderInfo = new OrderInfo();
                    orderInfo.OrderID = UserIDorClubIDorOrderID;
                    orderInfo.SelectClubOrderNameByOrderID();
                    return $"確定要刪除:{orderInfo.OrderName} 嗎???";


                case 161:
                    switch (OID)
                    {
                        case 2:
                            OrderTemp orderTemp = new OrderTemp(UserIDorClubIDorOrderID);
                            orderTemp.SelectByUserID();
                            return $"請確認將開單時間修改為：{orderTemp.StartTime.ToString("yyyy-MM-dd HH:mm")}，結單時間修改為：{orderTemp.EndTime.ToString("yyyy-MM-dd HH:mm")} 嗎???";

                        default:
                            break;
                    }
                    break;
                case 171:
                    switch (OID)
                    {
                        case 7:
                            ShopTemp shopTemp = new ShopTemp(UserIDorClubIDorOrderID); // 這裡用UserID
                            shopTemp.GetMyShopTempInfoFromSQL();
                            List<ShopTemp> shopItems = shopTemp.GetShopItemTempFromSQL();

                            string confirmText1 = $"請確認建立商店資訊：" +
                                                 $"商店名稱：{shopTemp.ShopName}，" +
                                                 $"商店電話：{shopTemp.ShopPhone}，" +
                                                 $"商店地址：{shopTemp.ShopAddress}，商店品項：";

                            string confirmText2 = "";
                            for (int i = 0; i < shopItems.Count; i++)
                            {
                                confirmText2 += $"{shopItems[i].ShopItem}({shopItems[i].ShopItemPrice} TWD)　";
                            }

                            return confirmText1 + confirmText2;

                        default:
                            break;
                    }
                    break;

                case 201:
                    switch (OID)
                    {
                        case 3:
                            return $"確定建立嗎???";
                        default:
                            break;
                    }
                    break;


                case 211:
                    switch (OID)
                    {
                        case 3:
                            return $"要繼續選擇商店嗎???";
                        default:
                            break;
                    }
                    break;

                case 231:
                    switch (OID)
                    {
                        case 3:
                            ShopInfo shopInfo = new ShopInfo();
                            shopInfo.ShopID = UserIDorClubIDorOrderID;
                            shopInfo.SelectMyShopNameByShopID();
                            if (shopInfo.ShopName != null)
                            {
                                return $"確定將此訂單商店換成 {shopInfo.ShopName} 嗎???";
                            }
                            else
                            {
                                List<ShopInfo> shopInfos = shopInfo.BossShopSelectShopNameByShopid();
                                return $"確定將此訂單商店換成 {shopInfos[0].ShopName} 嗎???";
                            }

                        default:
                            break;
                    }
                    return "";


                case 241:
                    switch (OID)
                    {
                        case 3:
                            return $"確定建立嗎???";
                        default:
                            break;
                    }
                    break;

                case 251:
                    switch (OID)
                    {
                        case 3:
                            return $"要繼續選擇商店嗎???";
                        default:
                            break;
                    }
                    break;

                case 261:
                    switch (OID)
                    {
                        case 3:
                            ShopInfo shopInfo = new ShopInfo();
                            shopInfo.ShopID = UserIDorClubIDorOrderID;
                            shopInfo.SelectMyShopNameByShopID();
                            if (shopInfo.ShopName != null)
                            {
                                return $"確定將此訂單商店換成 {shopInfo.ShopName} 嗎???";
                            }
                            else
                            {
                                List<ShopInfo> shopInfos = shopInfo.BossShopSelectShopNameByShopid();
                                return $"確定將此訂單商店換成 {shopInfos[0].ShopName} 嗎???";
                            }

                        default:
                            break;
                    }
                    return "";
                case 121:
                    switch (OID)
                    {
                        case 5:
                            return "需要繼續修改其他品項嗎?";
                        default:
                            break;
                    }
                    break;

                case 271:
                    switch (OID)
                    {
                        case 7:
                            ShopTemp shopTemp = new ShopTemp(UserIDorClubIDorOrderID); // 這裡用UserID
                            shopTemp.GetClubShopTempInfoFromSQL();
                            List<ShopTemp> shopItems = shopTemp.GetShopItemTempFromSQL();

                            string confirmText1 = $"請確認建立社團商店資訊：" +
                                                 $"商店名稱：{shopTemp.ShopName}，" +
                                                 $"商店電話：{shopTemp.ShopPhone}，" +
                                                 $"商店地址：{shopTemp.ShopAddress}，商店品項：";

                            string confirmText2 = "";
                            for (int i = 0; i < shopItems.Count; i++)
                            {
                                confirmText2 += $"{shopItems[i].ShopItem}({shopItems[i].ShopItemPrice} TWD)　";
                            }

                            return confirmText1 + confirmText2;



                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }
            return "";
        }

        internal static ConfirmTemplate MakeDeletOrderItem(string message, int QIDnow, int OIDnow, string BuyerInfoID, string userId, string channelAccessToken)
        {
            var actions = new List<isRock.LineBot.TemplateActionBase>();
            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            CancelHelper cancelHelper = new CancelHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailCancel = cancelHelper.GetCancel();

            BuyerInfo buyerInfo = new BuyerInfo();
            buyerInfo.ID = int.Parse(BuyerInfoID);
            buyerInfo.SelectAllByBuyerInfoID();

            message = "缺定要刪除?";

            actions.Add(new isRock.LineBot.PostbackAction() { label = "確定", data = $"{DateTime.Now},{questionDetailNext.QID},{questionDetailNext.OID},{BuyerInfoID}" });
            actions.Add(new isRock.LineBot.PostbackAction() { label = "取消", data = $"{DateTime.Now},{questionDetailCancel.QID},{questionDetailCancel.OID},{buyerInfo.Item},{BuyerInfoID}" });
            var ConfirmTemplateMsg = new isRock.LineBot.ConfirmTemplate()
            {
                altText = "替代文字(在無法顯示Confirm Template的時候顯示)",
                text = message,
                actions = actions //設定回覆動作
            };
            return ConfirmTemplateMsg;
        }
    }
}