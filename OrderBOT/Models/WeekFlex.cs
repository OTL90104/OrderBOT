using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderBot.Models
{
    public class WeekFlex
    {
        public string type { get; set; }
        public string altText { get; set; }
        public RootObject contents { get; set; }
        public WeekFlex()
        {
            this.type = "flex";
            this.altText = "This is a Flex Message";
            this.contents = new RootObject();
            this.contents.type = "carousel";
            this.contents.contents[0].type = "bubble";
            this.contents.contents[0].styles.footer.separator = true;
            this.contents.contents[0].styles.body.backgroundColor = "#FFFFFF";//Bubble顏色
            this.contents.contents[0].body.type = "box";
            this.contents.contents[0].body.layout = "vertical";
            this.contents.contents[0].body.contents[0].type = "text";
            this.contents.contents[0].body.contents[0].text = "WEEK SELECTION";
            this.contents.contents[0].body.contents[0].weight = "bold";
            this.contents.contents[0].body.contents[0].color = "#4b607c";//Menu字體顏色
            this.contents.contents[0].body.contents[0].align = "center";
            this.contents.contents[0].body.contents[0].size = "sm";
            this.contents.contents[0].body.contents[1].type = "text";
            this.contents.contents[0].body.contents[1].weight = "bold";
            this.contents.contents[0].body.contents[1].color = "#4b607c";
            this.contents.contents[0].body.contents[1].size = "lg";
            this.contents.contents[0].body.contents[1].margin = "md";
            this.contents.contents[0].body.contents[1].align = "center";
            this.contents.contents[0].body.contents[2].type = "separator";
            this.contents.contents[0].body.contents[2].margin = "xxl";
            this.contents.contents[0].body.contents[3].type = "box";
            this.contents.contents[0].body.contents[3].layout = "vertical";
            this.contents.contents[0].body.contents[3].margin = "xxl";
            this.contents.contents[0].body.contents[3].spacing = "sm";
        }
        public class Footer
        {
            public bool separator { get; set; }
        }

        public class Body2
        {
            public string backgroundColor { get; set; }
        }

        public class Styles
        {
            public Styles()
            {
                this.footer = new Footer();
                this.body = new Body2();
            }
            public Footer footer { get; set; }
            public Body2 body { get; set; }
        }

        public class Action
        {
            public string type { get; set; }
            public string label { get; set; }
            public string data { get; set; }
            public string text { get; set; }
        }

        public class Content3//Button
        {

            public string type { get; set; }
            public string style { get; set; }
            public string color { get; set; }
            public Action action { get; set; }
        }

        public class Content2
        {
            public Content2()
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
            public List<Content3> contents { get; set; }
            public string align { get; internal set; }
        }

        public class Body
        {

            public Body()
            {
                this.contents = new List<Content2>();
                contents.Add(new Content2());
                contents.Add(new Content2());
                contents.Add(new Content2());
                contents.Add(new WeekBtn());
            }
            public string type { get; set; }
            public string layout { get; set; }
            public List<Content2> contents { get; set; }
        }

        public class Content
        {
            public Content()
            {
                this.styles = new Styles();
                this.body = new Body();
            }
            public string type { get; set; }
            public Styles styles { get; set; }
            public Body body { get; set; }
        }

        public class RootObject
        {
            public RootObject()
            {
                this.contents = new List<Content>();
                contents.Add(new Content());
            }
            public string type { get; set; }
            public List<Content> contents { get; set; }
        }

        public class WeekBtn : Content2
        {
            public WeekBtn()
            {
                this.contents = new List<Content3>();
                contents.Add(new Content3());
                contents.Add(new Content3());
                contents.Add(new Content3());
                contents.Add(new Content3());
                contents.Add(new Content3());
                contents.Add(new Content3());
                contents.Add(new Content3());
                contents.Add(new Content3());
                this.type = "box";
                this.layout = "vertical";
                this.spacing = "sm";

                this.contents[0].type = "button";
                this.contents[0].style = "primary";
                this.contents[0].color = "#8fb7ed";
                this.contents[0].action = new WeekFlex.Action();
                this.contents[0].action.type = "postback";
                this.contents[0].action.label = "星期一";
              //  this.contents[0].action.text = "選擇了星期一";
                this.contents[0].action.data = "action=buy&itemid=111";

                this.contents[1].type = "button";
                this.contents[1].style = "primary";
                this.contents[1].color = "#8fb7ed";
                this.contents[1].action = new WeekFlex.Action();
                this.contents[1].action.type = "postback";
                this.contents[1].action.label = "星期二";
                this.contents[1].action.data = "action=buy&itemid=111";


                this.contents[2].type = "button";
                this.contents[2].style = "primary";
                this.contents[2].color = "#8fb7ed";
                this.contents[2].action = new WeekFlex.Action();
                this.contents[2].action.type = "postback";
                this.contents[2].action.label = "星期三";
                this.contents[2].action.data = "action=buy&itemid=111";

                this.contents[3].type = "button";
                this.contents[3].style = "primary";
                this.contents[3].color = "#8fb7ed";
                this.contents[3].action = new WeekFlex.Action();
                this.contents[3].action.type = "postback";
                this.contents[3].action.label = "星期四";
                this.contents[3].action.data = "action=buy&itemid=111";

                this.contents[4].type = "button";
                this.contents[4].style = "primary";
                this.contents[4].color = "#8fb7ed";
                this.contents[4].action = new WeekFlex.Action();
                this.contents[4].action.type = "postback";
                this.contents[4].action.label = "星期五";
                this.contents[4].action.data = "action=buy&itemid=111";

                this.contents[5].type = "button";
                this.contents[5].style = "primary";
                this.contents[5].color = "#8fb7ed";
                this.contents[5].action = new WeekFlex.Action();
                this.contents[5].action.type = "postback";
                this.contents[5].action.label = "星期六";
                this.contents[5].action.data = "action=buy&itemid=111";

                this.contents[6].type = "button";
                this.contents[6].style = "primary";
                this.contents[6].color = "#8fb7ed";
                this.contents[6].action = new WeekFlex.Action();
                this.contents[6].action.type = "postback";
                this.contents[6].action.label = "星期天";
                this.contents[6].action.data = "action=buy&itemid=111";

                this.contents[7].type = "button";
                this.contents[7].action = new WeekFlex.Action();
                this.contents[7].action.type = "postback";
                this.contents[7].action.label = "完成";
                this.contents[7].action.data = "action=buy&itemid=111";

            }
        }
    }
}