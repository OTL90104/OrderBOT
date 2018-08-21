using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushChecker.Models
{
    class FlexPushOrderMenu
    {
        public string type { get; set; }
        public string altText { get; set; }
        public Contents contents { get; set; }

        public FlexPushOrderMenu()
        {
            this.type = "flex";
            this.altText = "This is a Flex Message";
            this.contents = new Contents();
            this.contents.type = "carousel";
            this.contents.contents = new List<Content>();
           // this.contents.contents.Add(new Bubble("https://scdn.line-apps.com/n/channel_devcenter/img/fx/01_5_carousel.png"));//第一個Bubble
        }
        public class Bubble: Content
        {
            public Bubble()
            {
                this.type = "bubble";
              //  this.hero = new Hero();
                this.body = new Body();
                this.body.type = "box";
                this.body.layout = "vertical";
                this.body.spacing = "sm";
                this.body.contents = new List<Content2>();
                this.footer = new Footer();
            }

            public Bubble(string HeroUri)
            {
                this.type = "bubble";
                this.hero = new Hero(HeroUri);
                this.body = new Body();
                this.body.type = "box";
                this.body.layout = "vertical";
                this.body.spacing = "sm";
                this.body.contents = new List<Content2>();
                this.footer = new Footer();

            }
        }


        public class Hero
        {
            public Hero()
            {
                this.type = "image";
                this.size = "full";
                this.aspectRatio = "20:13";
                this.aspectMode = "cover";
            }
            public Hero(string imageUri)
            {
                this.type = "image";
                this.size = "full";
                this.aspectRatio = "20:13";
                this.aspectMode = "cover";
                this.url = imageUri;
            }
            public string type { get; set; }
            public string size { get; set; }
            public string aspectRatio { get; set; }
            public string aspectMode { get; set; }
            public string url { get; set; }
        }

        public class OrderName : Content2
        {
            public OrderName(string OrderName)
            {
                this.type = "text";
                this.text = OrderName;
                this.weight = "bold";
                this.size = "xxl";
                this.margin = "md";
            }
        }

        public class DateAndShopName: Content2
        {
            public DateAndShopName(string DateOrShopNameText)
            {
                this.type = "text";
                this.text = DateOrShopNameText;
                this.weight = "bold";
                this.size = "md";
                this.margin = "md";
                this.wrap = true;
            }
        }
            public class Content2
        {
            public string type { get; set; }
            public string text { get; set; }
            public string weight { get; set; }
            public string color { get; set; }
            public string size { get; set; }
            public string margin { get; set; }
            public bool? wrap { get; set; }
        }

        public class Body
        {
            public string type { get; set; }
            public string layout { get; set; }
            public string spacing { get; set; }
            public List<Content2> contents { get; set; }
        }

        public class Action
        {
            public string type { get; set; }
            public string label { get; set; }
            public string data { get; set; }
        }

        public class Content3
        {
            public string type { get; set; }
            public string style { get; set; }
            public string color { get; set; }
            public Action action { get; set; }
        }

        public class OrderButton : Content3
        {
            public OrderButton(string item ,double itemPrice)
            {
                this.type = "button";
                this.style = "primary";
                this.color = "#4b607c";
                this.action = new Action() { type = "postback",label = $"{item}：{itemPrice} TWD" ,data ="default"};
            }
        }


        public class Footer
        {
            public Footer()
            {
                this.type = "box";
                this.layout = "vertical";
                this.spacing = "sm";
                this.contents = new List<Content3>();
            }
            public string type { get; set; }
            public string layout { get; set; }
            public string spacing { get; set; }
            public List<Content3> contents { get; set; }
        }

        public class Content
        {
            public string type { get; set; }
            public Hero hero { get; set; }
            public Body body { get; set; }
            public Footer footer { get; set; }
        }

        public class Contents
        {
            public string type { get; set; }
            public List<Content> contents { get; set; }
        }



        
    }
}
