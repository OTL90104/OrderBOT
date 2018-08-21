using OrderBot.SQLObject;
using OrderBot.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushChecker.Models
{
    class FlexReportToShop
    {
       public FlexReportToShop(string OrderLeaderName ,string OrderPartitionID)
        {
            this.type = "flex";
            this.altText = "This is a Flex Message";
            this.contents = new Contents();
            this.contents.type = "bubble";
            this.contents.styles = new Styles();
           // this.contents.styles.footer = new Footer();
         //   this.contents.styles.footer.separator = true;
            this.contents.body = new Body();
            this.contents.body.type = "box";
            this.contents.body.layout = "vertical";
            this.contents.body.contents = new List<Content>();
            this.contents.body.contents.Add(new Content() {type= "text", text= "ORDER", weight= "bold", color= "#1DB446", size="sm" , align = "center" });
            this.contents.body.contents.Add(new Content() {type= "text", text = $"訂購者：{OrderLeaderName}",weight= "bold", margin= "md", size="xl" , align = "center" });//contents.body.contents[0].text
                                                                                                                                                                       // this.contents.body.contents.Add(new Content() {type= "separator", margin= "xxl" });
            this.contents.footer = new Footer(OrderPartitionID);

        }
        public string type { get; set; }
        public string altText { get; set; }
        public Contents contents { get; set; }

        public class ShopNameAndDate : Content//加文字 this.text
        {
           public ShopNameAndDate()
            {
                this.type = "text";
                this.weight = "bold";
                this.size = "md";
                this.margin = "md";
                this.wrap = true;
                this.align = "center";
            }
        }

        public class AddressAndTel: Content//加文字 this.text
        {
            public AddressAndTel()
            {
                this.type = "text";
                this.weight = "bold";
                this.size = "sm";
                this.margin = "md";
                this.wrap = true;
                this.align = "center";
            }
        }
        public class Footer
        {
            public string type { get; set; }
            public string layout { get; set; }
            public string spacing { get; set; }
            public bool? separator { get; set; }
            public List<Content> contents { get; set; }
            public Footer(string OrderPartitionID)
            {
                this.type = "box";
                this.layout = "vertical";
                this.spacing = "sm";
                this.contents = new List<Content>();
                this.contents.Add(new FooterAcceptBtn($"{DateTime.Now},9998,2,accept,{OrderPartitionID}"));
                this.contents.Add(new FooterRefuseBtn($"{DateTime.Now},9998,2,refuse,{OrderPartitionID}"));
            }
        }

        public class Styles
        {
            public Footer footer { get; set; }
        }

        public class Content3
        {
            public string type { get; set; }
            public string text { get; set; }
            public string weight { get; set; }
            public string size { get; set; }
            public string color { get; set; }
            public int flex { get; set; }
            public bool wrap { get; internal set; }
        }

        public class Content2
        {
            public string type { get; set; }
            public string layout { get; set; }
            public List<Content3> contents { get; set; }
            public string margin { get; set; }
            public string text { get; set; }
            public string weight { get; set; }
            public string size { get; set; }
            public string color { get; set; }
            public string align { get; set; }
            public int? flex { get; set; }
            public bool? wrap { get; internal set; }
        }

        public class NameBox:Content
        {
            public NameBox(string name)
            {
                this.type = "box";
                this.layout = "vertical";
                this.margin = "xxl";
                this.spacing = "sm";
                this.contents = new List<Content2>();
                this.contents.Add(new Content2() { type = "box", layout = "horizontal" });
                this.contents[0].contents = new List<Content3>();
                this.contents[0].contents.Add(new Content3() { type = "text",text = name ,weight = "bold", color = "#000000", size = "lg" ,flex = 2});//名子在這輸入 contents[0].contents[0].text
            }
        }

        public class TotalTitleBox : Content
        {
            public TotalTitleBox()
            {
                this.type = "box";
                this.layout = "horizontal";
                this.margin = "lg";
                this.contents = new List<Content2>();
                this.contents.Add(new Content2() { type = "text", text = "Total:",weight ="bold", color = "#000000", size = "lg" ,flex = 2});            }
        }

        public class TotalPrice : Content
        {
            public TotalPrice(double TotalPrice )
            {
                this.type = "box";
                this.layout = "horizontal";
                this.margin = "md";
                this.contents = new List<Content2>();
                this.contents.Add(new Content2() { type = "text", text = $"{TotalPrice} TWD",  color = "#000000", size = "xxl", align = "end" });
            }
        }

        internal void AddTotal(List<TotalItem> totalItems ,double TotalPrice, List<string> Buyers,string OrderPartitionID)
        {
            this.contents.body.contents.Add(new TotalTitleBox());
            foreach (TotalItem TotalItem in totalItems)
            {
                this.contents.body.contents.Add(new TotalItemBox(TotalItem.Item,TotalItem.Amount));//加入品項

                foreach (string Buyer in Buyers)//檢查每個訂餐者是否有針對此品項備註
                {
                    UserStatus userStatus = new UserStatus(Buyer);
                    BuyerInfo buyer = new BuyerInfo();
                    buyer.OrderPartitionID = OrderPartitionID;
                    buyer.BuyerID = Buyer;
                    List<BuyerInfo> list = buyer.SelectItemAndAmontByBuyerID();//選出訂餐者的訂餐細節
                    foreach (BuyerInfo BuyerInfo in list)//處理訂餐細節
                    {
                        if (TotalItem.Item == BuyerInfo.Item)
                        {
                            if (BuyerInfo.Note != "no")
                            {
                                this.contents.body.contents.Add(new NoteBox($"Note:{BuyerInfo.Note} "));
                            }
                        }
                    }

                }

            }
            this.contents.body.contents.Add(new TotalPrice(TotalPrice));
        }


        public class TotalItemBox : Content
        {
            public TotalItemBox(string item , int Amount)
            {
                this.type = "box";
                this.layout = "horizontal";
                this.margin = "md";
                this.contents = new List<Content2>();
                this.contents.Add(new Content2() { type = "text", text = item, weight="bold" ,size = "lg" ,color = "#000000", wrap =true,flex = 1});
                this.contents.Add(new Content2() { type = "text", text = Amount.ToString(), weight = "bold", size = "lg" ,color = "#000000", flex = 1});
            }
        }




        public class NoteBox : Content
        {
            public NoteBox(string NoteText)
            {
                this.type = "box";
                this.layout = "vertical";
                this.contents = new List<Content2>();
                this.contents.Add(new Content2() { type = "box", layout = "horizontal" });
                this.contents[0].contents = new List<Content3>();
                this.contents[0].contents.Add(new Content3() { type = "text", text = NoteText, color = "#555555", size = "xs" ,wrap = true });//名子在這輸入 contents[0].contents[0].text
            }
        }

        public class SubTotalBox : Content
        {
            public SubTotalBox(double SubTotal)
            {
                this.type = "box";
                this.layout = "vertical";
                this.margin = "xs";
                this.contents = new List<Content2>();
                this.contents.Add(new Content2() { type = "box", layout = "horizontal" });
                this.contents[0].contents = new List<Content3>();
                this.contents[0].contents.Add(new Content3() { type = "text", text = "Subtotal(TWD)", color = "#000000", size = "lg" ,flex = 4 });//名子在這輸入 contents[0].contents[0].text
                this.contents[0].contents.Add(new Content3() { type = "text", text = $"{SubTotal}", color = "#000000", size = "lg", flex = 2 });//名子在這輸入 contents[0].contents[0].text
            }
        }


        public class ItemBox : Content
        {
            public ItemBox(string Item, int Amount ,double Price)
            {
                this.type = "box";
                this.layout = "vertical";
                this.contents = new List<Content2>();
                this.contents.Add(new Content2() { type = "box", layout = "horizontal" });
                this.contents[0].contents = new List<Content3>();
                this.contents[0].contents.Add(new Content3() { type = "text", text = Item, color = "#555555", size = "md", flex = 3 });//名子在這輸入 contents[0].contents[0].text
                this.contents[0].contents.Add(new Content3() { type = "text", text = $"{Amount}", color = "#555555", size = "sm", flex = 1 });//名子在這輸入 contents[0].contents[0].text
                this.contents[0].contents.Add(new Content3() { type = "text", text = $"{Price}TWD", color = "#555555", size = "sm", flex = 2 });//名子在這輸入 contents[0].contents[0].text
                                                                                                                                                        // this.contents.Add(new Content3() { type = "text", text = Item,  color = "#555555", size = "md", flex = 3 });//名子在這輸入 contents[0].contents[0].text
                                                                                                                                                        //  this.contents.Add(new Content3() { type = "text", text = $"{Amount}",  color = "#555555", size = "md", flex = 1 });//名子在這輸入 contents[0].contents[0].text
                                                                                                                                                        //  this.contents.Add(new Content3() { type = "text", text = $"{Price}TWD",  color = "#555555", size = "md", flex = 2 });//名子在這輸入 contents[0].contents[0].text
            }
        }
        public class FooterAcceptBtn : Content
        {
            public FooterAcceptBtn(string data)
            {
                this.type = "button";
                this.style = "primary";
                this.color = "#1DB446";
                this.action = new Action();
                this.action.type = "postback";
                this.action.label = "接受";
                this.action.data = data;

            }

            public string style { get; private set; }
            public Action action { get; private set; }
        }

        public class FooterRefuseBtn : Content
        {
            public FooterRefuseBtn(string data)
            {
                this.type = "button";

                this.action = new Action();
                this.action.type = "postback";
                this.action.label = "拒絕";
                this.action.data = data;
            }

            public string style { get; private set; }
            public Action action { get; private set; }
        }

        public class PushToShopBox : Content
        {
            public PushToShopBox(string data)
            {
                this.type = "button";
                this.style = "primary";
                this.color = "#555555";
                this.margin = "md";
                this.action = new Action();
                this.action.type = "postback";
                this.action.label = "將訂單傳送給老闆";
                this.action.data = data;
            }

            public string style { get; private set; }
            public Action action { get; private set; }
        }
        public class Action
        {
            public string type { get; set; }
            public string label { get; set; }
            public string data { get; set; }
        }
        //文字方塊在這box
        public class Content
        {
            public string type { get; set; }
            public string text { get; set; }
            public string weight { get; set; }
            public string color { get; set; }
            public string size { get; set; }
            public string margin { get; set; }
            public string layout { get; set; }
            public string spacing { get; set; }
            public bool? wrap { get; set; }
            public List<Content2> contents { get; set; }
            public string align { get; internal set; }
        }

        public class Body
        {
            public string type { get; set; }
            public string layout { get; set; }
            public List<Content> contents { get; set; }
        }

        public class Contents
        {
            public string type { get; set; }
            public Styles styles { get; set; }
            public Body body { get; set; }
            public Footer footer { get; set; }
        }

        public class TotalItem
        {
            public string Item { get; set; }
            public int Amount { get; set; }
        }

    }
}
