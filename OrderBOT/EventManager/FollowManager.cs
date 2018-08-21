using isRock.LineBot;
using OrderBot.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderBot.EventManager
{
    public class FollowManager
    {
        public FollowManager()
        {

        }

        internal void process(Event item, ReceievedMessage receivedMessage, string channelAccessToken, Bot bot)
        {
            string userDisplayName = isRock.LineBot.Utility.GetUserInfo(item.source.userId, channelAccessToken).displayName;
            UserStatus userStatus = new UserStatus(item.source.userId, userDisplayName);
            userStatus.InitializeByUserID();
          
            PeriodOrderTmp periodOrderTmp = new PeriodOrderTmp(item.source.userId);
            periodOrderTmp.InsertInitialPeriodOrderTmp();

            OrderTemp orderTmp = new OrderTemp(item.source.userId);
            orderTmp.InsertInitialOrderTmp();

            ShopTemp shopTemp = new ShopTemp(item.source.userId);
            shopTemp.InsertInitialShopTemp();

        }
    }
}