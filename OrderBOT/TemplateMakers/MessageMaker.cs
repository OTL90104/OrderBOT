using OrderBot.RecoginzeManager;
using OrderBot.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderBot.TemplateMakers
{
    public class MessageMaker
    {
        internal static string make(string UserID, int QIDnow, int OIDnow, string data)
        {
            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            //如果是取得參加碼,直接回傳藏在postback裡的data 
            if (QIDnow == 101 && OIDnow == 1)
            {
                return data;
            }
            else if (QIDnow == 73 && OIDnow == 2)
            {
                return data;
            }
            else if (QIDnow == 53 && OIDnow == 1)
            {
                return data;
            }
            else if (QIDnow == 153 && OIDnow == 1)
            {
                return data;
            }
            else if (QIDnow == 9998 )
            {
                switch (data)
                {
                    case "accept":
                        return "已向訂購者傳送確認訊息!";
                    case "refuse":
                        return "已向訂購者傳送婉拒訊息!";
                    default:
                        break;
                }
            }
            QuestionDetail questionDetail = new QuestionDetail(QIDnow, OIDnow);
            List<QuestionDetail> questionDetailList = questionDetail.SelectByQidAndOid();

            switch (QIDnow)
            {
                case 121:
                    UserStatus userStatustemp = new UserStatus(UserID);
                    userStatustemp.SelectByUserID();
                    data = userStatustemp.TempData;
                    break;
                default:
                    break;
            }
            UserStatus userStatus = new UserStatus(UserID, questionDetailNext.QID, questionDetailNext.OID, data);
            userStatus.UpdateByUserID();

            // 先偷做一些事情
            switch (QIDnow)
            {
                case 71:
                    switch (OIDnow)
                    {
                        case 3:
                            // 把UserID加入訂單
                       
                            break;
                        default:
                            break;
                    }
                    break;

                case 194:
                    switch (OIDnow)
                    {
                        case 2:
                            // 改品項的時候要先記錄改之前的品項到ShopTemp，之後才知道要改哪一筆
                            ShopTemp shopTemp = new ShopTemp(UserID);
                            shopTemp.ShopItem = data;
                            shopTemp.UpdateItemToShopTemp();
                            break;
                        case 6:
                            // 清空
                            shopTemp = new ShopTemp(UserID);
                            shopTemp.InitializeShopTempByUserID();
                            shopTemp.DeleteShopItemTempByUserID();
                            userStatus.InitializeUserStatusByUserID();
                            break;
                        default:
                            break;
                    }
                    break;

                case 294:
                    switch (OIDnow)
                    {
                        case 2:
                            // 改品項的時候要先記錄改之前的品項到ShopTemp，之後才知道要改哪一筆
                            ShopTemp shopTemp = new ShopTemp(UserID);
                            shopTemp.ShopItem = data;
                            shopTemp.UpdateItemToShopTemp();
                            break;
                        case 6:
                            // 清空
                            shopTemp = new ShopTemp(UserID);
                            shopTemp.InitializeShopTempByUserID();
                            shopTemp.DeleteShopItemTempByUserID();
                            userStatus.InitializeUserStatusByUserID();
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

            return questionDetailList[0].AnswerOption;
        }

        internal static string MakeLeaveClubMessage(string UserID, int QID, int OID, string clubID)
        {
            ClubInfo clubInfo = new ClubInfo(UserID, clubID, "");
            List<ClubInfo> clubInfos = clubInfo.SelectByUseridandClubID();
            if (clubInfos.Count > 0)
            {
                return $"已經離開:\n{clubInfos[0].ClubName}";
            }
            else
            {
                return "系統錯誤!";
            }
        }
    }
}