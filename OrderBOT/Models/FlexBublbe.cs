using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static OrderBot.Models.FlexBublbe;

namespace OrderBot.Models
{
    // rootobject.contents[0].body.contents[1].text 商店名稱
    //             this.rootobject.bubbles[0].body.elements[3].ContentsList    物品清單
    public class FlexBublbe
    {
        public string type { get; set; }
        public string altText { get; set; }
        public Rootobject contents { get; set; }
        public FlexBublbe()
        {
            this.contents = new Rootobject();
            this.type = "flex";
            this.altText = "This is a Flex Message";
            this.contents.type = "carousel";
            this.contents.contents[0].type = "bubble";
            this.contents.contents[0].styles.footer.separator = true;
            this.contents.contents[0].body.type = "box";
            this.contents.contents[0].body.layout = "vertical";
            this.contents.contents[0].body.contents[0].type = "text";
            this.contents.contents[0].body.contents[0].text = "MENU";
            this.contents.contents[0].body.contents[0].weight = "bold";
            this.contents.contents[0].body.contents[0].color = "#4b607c";
            this.contents.contents[0].body.contents[0].size = "sm";
            this.contents.contents[0].body.contents[0].align = "center";
            this.contents.contents[0].body.contents[1].type = "text";
            this.contents.contents[0].body.contents[1].weight = "bold";
            this.contents.contents[0].body.contents[1].size = "xxl";
            this.contents.contents[0].body.contents[1].margin = "md";
            this.contents.contents[0].body.contents[1].color = "#111111";
            this.contents.contents[0].body.contents[1].wrap = true;
            this.contents.contents[0].body.contents[1].align = "center";
            this.contents.contents[0].body.contents[2].type = "separator";
            this.contents.contents[0].body.contents[2].margin = "xxl";
            this.contents.contents[0].body.contents[3].type = "box";
            this.contents.contents[0].body.contents[3].layout = "vertical";
            this.contents.contents[0].body.contents[3].margin = "xxl";
            this.contents.contents[0].body.contents[3].spacing = "sm";
        }

        public class Topobject
        {
            public Topobject()
            {
               
            }

        }



        public class Rootobject
        {
           public Rootobject()
            {
                this.contents = new List<Bubble>();
                contents.Add(new Bubble());
            }
            public string type { get; set; }
            public List<Bubble> contents { get; set; }
        }

        public class Bubble
        {
          public Bubble()
            {
                this.styles = new Styles();
                this.body = new Body();
            }
            public string type { get; set; }
            public Styles styles { get; set; }
            public Body body { get; set; }
        }

        public class Styles
        {
           public Styles()
            {
                this.footer = new Footer();
            }
            public Footer footer { get; set; }
        }

        public class Footer
        {
            public bool separator { get; set; }
        }

        public class Body
        {
           public Body()
            {
                this.contents = new List<Element>();
                contents.Add(new Element());
                contents.Add(new Element());
                contents.Add(new Element());
                contents.Add(new Element());
            }
            public string type { get; set; }
            public string layout { get; set; }
            public List<Element> contents { get; set; }
        }
         
        public class Element
        {
           public Element()
            {

            }
            public string type { get; set; }
            public string text { get; set; }
            public string weight { get; set; }
            public string color { get; set; }
            public string size { get; set; }
            public string margin { get; set; }
            public string layout { get; set; }
            public string spacing { get; set; }
            public List<Content> contents { get; set; }
            public bool? wrap { get; internal set; }
            public string align { get; internal set; }
        }

        public class Content
        {
           public  Content()
            {
                this.contents = new List<ShopItemText>();
            }
            public string type { get; set; }
            public string layout { get; set; }
            public List<ShopItemText> contents { get; set; }
            public string style { get; set; }
            public Action action { get; set; }
            public string color { get; internal set; }
        }

        public class Action
        {

            public string type { get; set; }
            public string label { get; set; }
            public string data { get; set; }
            public string text { get; set; }
        }




       
    }
    public class ShopItemText
    {
        public ShopItemText()
        {
            this.type = "text";
            this.size = "sm";
        }
        public string type { get; set; }
        public string text { get; set; }
        public string size { get; set; }
        public string color { get; set; }
        public int? flex { get; set; }
        public string align { get; set; }
    }

    public class PartitionLine : Element
    {
       public PartitionLine()
        {
            this.type = "separator";
            this.margin = "xxl";
        }
    }
    
    public class FooterBtn:Element
    {
       public FooterBtn()
        {
            this.contents = new List<Content>();
            contents.Add(new Content());
            contents.Add(new Content());
            this.type = "box";
            this.layout = "vertical";
            this.spacing = "sm";
            this.contents[0].contents = null;
            this.contents[0].type = "button";
            this.contents[0].style = "primary";
            this.contents[0].color = "#4b607c";
            this.contents[0].action = new FlexBublbe.Action();
            this.contents[0].action.type = "postback";
            this.contents[0].action.label = "確認";
            //this.contents[0].action.data = "201,3,";
            //this.contents[0].action.text = "確認";
            this.contents[1].contents = null;
            this.contents[1].action = new FlexBublbe.Action();
            this.contents[1].type = "button";
            this.contents[1].action.type = "postback";
            this.contents[1].action.label = "上一步";
            //this.contents[1].action.data = "action=buy&itemid=111";
            //this.contents[1].action.text = "取消";
        }

    }
}