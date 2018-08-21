using isRock.LineBot;
using OrderBot.SQLObject;
using OrderBot.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace OrderBot.TemplateMakers
{
    public class ImagemapMaker
    {
        internal static ImagemapMessage MakeMenu(Event item)
        {
            // 先做初始化
            UserStatus userStatus = new UserStatus(item.source.userId);
            userStatus.InitializeUserStatusByUserID();
            ShopTemp shopTemp = new ShopTemp(item.source.userId);
            shopTemp.InitializeShopTempByUserID();
            shopTemp.DeleteShopItemTempByUserID();
            OrderTemp orderTemp = new OrderTemp(item.source.userId);
            orderTemp.UpdateInitialOrderTemp();
            PeriodOrderTmp periodOrderTmp = new PeriodOrderTmp(item.source.userId);
            periodOrderTmp.UpdateInitialPeriodOrderTmp();
            ShopListTemp shopListTemp = new ShopListTemp(item.source.userId);
            shopListTemp.DeleteByUserID();
            BuyerTemp buyerTemp = new BuyerTemp(item.source.userId);
            buyerTemp.DeleteByBuyerID();
            // 先做初始化


            ImagemapMessage imagemapMessage = new ImagemapMessage();

            Uri uri = new Uri("https://i220.photobucket.com/albums/dd130/jung_04/Menu2.png#");
            imagemapMessage.baseUrl = uri;
            imagemapMessage.altText = "這是imagemap";
            Size size = new Size(1040, 1040);
            imagemapMessage.baseSize = size;

            #region LeftUp
            isRock.LineBot.ImagemapArea imagemapAreaLeftUp = new isRock.LineBot.ImagemapArea()
            {
                x = 0,
                y = 0,
                width = 520,
                height = 520
            };
            isRock.LineBot.ImagemapMessageAction imagemapMessageActionLeftUp = new ImagemapMessageAction();

            imagemapMessageActionLeftUp.area = imagemapAreaLeftUp;
            imagemapMessageActionLeftUp.text = "我要進入訂單模式!!!";

            imagemapMessage.actions.Add(imagemapMessageActionLeftUp);


            #endregion
            #region RightUp
            isRock.LineBot.ImagemapArea imagemapAreaRightUp = new isRock.LineBot.ImagemapArea()
            {
                x = 520,
                y = 0,
                width = 520,
                height = 520
            };

            isRock.LineBot.ImagemapMessageAction imagemapMessageActionRightUp = new ImagemapMessageAction();

            imagemapMessageActionRightUp.area = imagemapAreaRightUp;
            imagemapMessageActionRightUp.text = "我要進入個人模式!!!";

            imagemapMessage.actions.Add(imagemapMessageActionRightUp);

            #endregion
            #region LeftDown
            isRock.LineBot.ImagemapArea imagemapAreaLeftDown = new isRock.LineBot.ImagemapArea()
            {
                x = 0,
                y = 520,
                width = 520,
                height = 520
            };

            isRock.LineBot.ImagemapMessageAction imagemapMessageActionLeftDown = new ImagemapMessageAction();

            imagemapMessageActionLeftDown.area = imagemapAreaLeftDown;
            imagemapMessageActionLeftDown.text = "我要進入社團模式!!!";

            imagemapMessage.actions.Add(imagemapMessageActionLeftDown);
            #endregion
            #region RightDown
            var actions1 = new List<isRock.LineBot.ImagemapActionBase>();
            isRock.LineBot.ImagemapArea imagemapAreaRightDown = new isRock.LineBot.ImagemapArea()
            {
                x = 520,
                y = 520,
                width = 520,
                height = 520
            };

            isRock.LineBot.ImagemapMessageAction imagemapMessageActionRightDown = new ImagemapMessageAction();

            imagemapMessageActionRightDown.area = imagemapAreaRightDown;
            imagemapMessageActionRightDown.text = "我要進入商店模式!!!";

            imagemapMessage.actions.Add(imagemapMessageActionRightDown);

            #endregion


            return imagemapMessage;
        }

        internal static ImagemapMessage MakeDemo(Event item)
        {
            ImagemapMessage imagemapMessage = new ImagemapMessage();

            Uri uri = new Uri("https://i220.photobucket.com/albums/dd130/jung_04/Demo_1.gif#");
            imagemapMessage.baseUrl = uri;
            imagemapMessage.altText = "這是imagemap";
            Size size = new Size(1040, 1040);
            imagemapMessage.baseSize = size;

            #region LeftUp
            isRock.LineBot.ImagemapArea imagemapAreaLeftUp = new isRock.LineBot.ImagemapArea()
            {
                x = 0,
                y = 0,
                width = 520,
                height = 520
            };
            isRock.LineBot.ImagemapMessageAction imagemapMessageActionLeftUp = new ImagemapMessageAction();

            imagemapMessageActionLeftUp.area = imagemapAreaLeftUp;
            imagemapMessageActionLeftUp.text = "訂單模式介紹";

            imagemapMessage.actions.Add(imagemapMessageActionLeftUp);


            #endregion
            #region RightUp
            isRock.LineBot.ImagemapArea imagemapAreaRightUp = new isRock.LineBot.ImagemapArea()
            {
                x = 520,
                y = 0,
                width = 520,
                height = 520
            };

            isRock.LineBot.ImagemapMessageAction imagemapMessageActionRightUp = new ImagemapMessageAction();

            imagemapMessageActionRightUp.area = imagemapAreaRightUp;
            imagemapMessageActionRightUp.text = "個人模式介紹";

            imagemapMessage.actions.Add(imagemapMessageActionRightUp);

            #endregion
            #region LeftDown
            isRock.LineBot.ImagemapArea imagemapAreaLeftDown = new isRock.LineBot.ImagemapArea()
            {
                x = 0,
                y = 520,
                width = 520,
                height = 520
            };

            isRock.LineBot.ImagemapMessageAction imagemapMessageActionLeftDown = new ImagemapMessageAction();

            imagemapMessageActionLeftDown.area = imagemapAreaLeftDown;
            imagemapMessageActionLeftDown.text = "社團模式介紹";

            imagemapMessage.actions.Add(imagemapMessageActionLeftDown);
            #endregion
            #region RightDown
            var actions1 = new List<isRock.LineBot.ImagemapActionBase>();
            isRock.LineBot.ImagemapArea imagemapAreaRightDown = new isRock.LineBot.ImagemapArea()
            {
                x = 520,
                y = 520,
                width = 520,
                height = 520
            };

            isRock.LineBot.ImagemapMessageAction imagemapMessageActionRightDown = new ImagemapMessageAction();

            imagemapMessageActionRightDown.area = imagemapAreaRightDown;
            imagemapMessageActionRightDown.text = "商店模式介紹";

            imagemapMessage.actions.Add(imagemapMessageActionRightDown);

            #endregion


            return imagemapMessage;
        }

        internal static ImagemapMessage MakeDemoOrder(Event item)
        {
            ImagemapMessage imagemapMessage = new ImagemapMessage();

            Uri uri = new Uri("https://i220.photobucket.com/albums/dd130/jung_04/Order_demo.png#");
            imagemapMessage.baseUrl = uri;
            imagemapMessage.altText = "這是imagemap";
            Size size = new Size(1040, 1040);
            imagemapMessage.baseSize = size;

           
            #region LeftDown
            isRock.LineBot.ImagemapArea imagemapAreaLeftDown = new isRock.LineBot.ImagemapArea()
            {
                x = 0,
                y = 840,
                width = 200,
                height = 200
            };

            isRock.LineBot.ImagemapMessageAction imagemapMessageActionLeftDown = new ImagemapMessageAction();

            imagemapMessageActionLeftDown.area = imagemapAreaLeftDown;
            imagemapMessageActionLeftDown.text = "Demo選單";

            imagemapMessage.actions.Add(imagemapMessageActionLeftDown);
            #endregion
            #region RightDown
            var actions1 = new List<isRock.LineBot.ImagemapActionBase>();
            isRock.LineBot.ImagemapArea imagemapAreaRightDown = new isRock.LineBot.ImagemapArea()
            {
                x = 840,
                y = 840,
                width = 200,
                height = 200
            };

            isRock.LineBot.ImagemapMessageAction imagemapMessageActionRightDown = new ImagemapMessageAction();

            imagemapMessageActionRightDown.area = imagemapAreaRightDown;
            imagemapMessageActionRightDown.text = "個人模式介紹";

            imagemapMessage.actions.Add(imagemapMessageActionRightDown);

            #endregion


            return imagemapMessage;
        }

        internal static ImagemapMessage MakeDemoUser(Event item)
        {
            ImagemapMessage imagemapMessage = new ImagemapMessage();

            Uri uri = new Uri("https://i220.photobucket.com/albums/dd130/jung_04/User_demo.png#");
            imagemapMessage.baseUrl = uri;
            imagemapMessage.altText = "這是imagemap";
            Size size = new Size(1040, 1040);
            imagemapMessage.baseSize = size;


            #region LeftDown
            isRock.LineBot.ImagemapArea imagemapAreaLeftDown = new isRock.LineBot.ImagemapArea()
            {
                x = 0,
                y = 840,
                width = 200,
                height = 200
            };

            isRock.LineBot.ImagemapMessageAction imagemapMessageActionLeftDown = new ImagemapMessageAction();

            imagemapMessageActionLeftDown.area = imagemapAreaLeftDown;
            imagemapMessageActionLeftDown.text = "訂單模式介紹";

            imagemapMessage.actions.Add(imagemapMessageActionLeftDown);
            #endregion
            #region RightDown
            var actions1 = new List<isRock.LineBot.ImagemapActionBase>();
            isRock.LineBot.ImagemapArea imagemapAreaRightDown = new isRock.LineBot.ImagemapArea()
            {
                x = 840,
                y = 840,
                width = 200,
                height = 200
            };

            isRock.LineBot.ImagemapMessageAction imagemapMessageActionRightDown = new ImagemapMessageAction();

            imagemapMessageActionRightDown.area = imagemapAreaRightDown;
            imagemapMessageActionRightDown.text = "社團模式介紹";

            imagemapMessage.actions.Add(imagemapMessageActionRightDown);

            #endregion


            return imagemapMessage;
        }

        internal static ImagemapMessage MakeDemoClub(Event item)
        {
            ImagemapMessage imagemapMessage = new ImagemapMessage();

            Uri uri = new Uri("https://i220.photobucket.com/albums/dd130/jung_04/Club_demo.png#");
            imagemapMessage.baseUrl = uri;
            imagemapMessage.altText = "這是imagemap";
            Size size = new Size(1040, 1040);
            imagemapMessage.baseSize = size;


            #region LeftDown
            isRock.LineBot.ImagemapArea imagemapAreaLeftDown = new isRock.LineBot.ImagemapArea()
            {
                x = 0,
                y = 840,
                width = 200,
                height = 200
            };

            isRock.LineBot.ImagemapMessageAction imagemapMessageActionLeftDown = new ImagemapMessageAction();

            imagemapMessageActionLeftDown.area = imagemapAreaLeftDown;
            imagemapMessageActionLeftDown.text = "個人模式介紹";

            imagemapMessage.actions.Add(imagemapMessageActionLeftDown);
            #endregion
            #region RightDown
            var actions1 = new List<isRock.LineBot.ImagemapActionBase>();
            isRock.LineBot.ImagemapArea imagemapAreaRightDown = new isRock.LineBot.ImagemapArea()
            {
                x = 840,
                y = 840,
                width = 200,
                height = 200
            };

            isRock.LineBot.ImagemapMessageAction imagemapMessageActionRightDown = new ImagemapMessageAction();

            imagemapMessageActionRightDown.area = imagemapAreaRightDown;
            imagemapMessageActionRightDown.text = "商店模式介紹";

            imagemapMessage.actions.Add(imagemapMessageActionRightDown);

            #endregion


            return imagemapMessage;
        }

        internal static ImagemapMessage MakeDemoShop(Event item)
        {
            ImagemapMessage imagemapMessage = new ImagemapMessage();

            Uri uri = new Uri("https://i220.photobucket.com/albums/dd130/jung_04/Shop_demo.png#");
            imagemapMessage.baseUrl = uri;
            imagemapMessage.altText = "這是imagemap";
            Size size = new Size(1040, 1040);
            imagemapMessage.baseSize = size;


            #region LeftDown
            isRock.LineBot.ImagemapArea imagemapAreaLeftDown = new isRock.LineBot.ImagemapArea()
            {
                x = 0,
                y = 840,
                width = 200,
                height = 200
            };

            isRock.LineBot.ImagemapMessageAction imagemapMessageActionLeftDown = new ImagemapMessageAction();

            imagemapMessageActionLeftDown.area = imagemapAreaLeftDown;
            imagemapMessageActionLeftDown.text = "社團模式介紹";

            imagemapMessage.actions.Add(imagemapMessageActionLeftDown);
            #endregion
            #region RightDown
            var actions1 = new List<isRock.LineBot.ImagemapActionBase>();
            isRock.LineBot.ImagemapArea imagemapAreaRightDown = new isRock.LineBot.ImagemapArea()
            {
                x = 840,
                y = 840,
                width = 200,
                height = 200
            };

            isRock.LineBot.ImagemapMessageAction imagemapMessageActionRightDown = new ImagemapMessageAction();

            imagemapMessageActionRightDown.area = imagemapAreaRightDown;
            imagemapMessageActionRightDown.text = "Demo選單";

            imagemapMessage.actions.Add(imagemapMessageActionRightDown);

            #endregion


            return imagemapMessage;
        }
    }
}