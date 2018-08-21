using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using isRock.LineBot;
using OrderBot.RecoginzeManager;
using OrderBot.SQLObject;
using OrderBot.Utility;

namespace OrderBot
{
    public class ButtonMaker
    {       

        internal static ButtonsTemplate Make(int QIDnow, int OIDnow, string data)
        {
            //建立actions，作為ButtonTemplate的用戶回覆行為
            var actions = new List<isRock.LineBot.TemplateActionBase>();

            // 以傳進來的QID找到按鈕選項
            QuestionDetail questionDetail = new QuestionDetail(QIDnow);
            List<QuestionDetail> questionDetailList = questionDetail.SelectByQid();

            // 以選項的QID跟OID找到button裡要藏的QID和OID (即按下按鈕後下一個出現的QID和OID)
            for (int i = 0; i < questionDetailList.Count; i++)
            {
                NextHelper nextHelper = new NextHelper(questionDetailList[i].QID, questionDetailList[i].OID);
                QuestionDetail questionDetailNext = nextHelper.GetNext();

                actions.Add(new isRock.LineBot.PostbackAction()
                {
                    label = questionDetailList[i].AnswerOption,
                    data = DateTime.Now.ToString() + $",{questionDetailNext.QID},{questionDetailNext.OID},{data}"
                });
            }

            // QID 19和29都有四個button了不能加上一步
            if (!(QIDnow == 19 || QIDnow == 29))
            {
                // 製作上一步的button
                PreviousHelper previousHelper = new PreviousHelper(QIDnow, OIDnow);
                QuestionDetail questionDetailPrevious = previousHelper.GetPrevious();

                actions.Add(new isRock.LineBot.PostbackAction()
                {
                    label = "上一步",
                    data = DateTime.Now.ToString() + $",{questionDetailPrevious.QID}," + $"{questionDetailPrevious.OID},default"
                });
            }

            //製作模板框架
            Question question = new Question(QIDnow);
            question.SelectByQid();

            var ButtonTemplate = new isRock.LineBot.ButtonsTemplate()
            {
                altText = $"請使用手機查看喔~{question.QuestionTitle}",
                //title = question.QuestionTitle,
                text = question.QuestionText,
                //設定圖片
                thumbnailImageUrl = new Uri(question.ImageUrl),
                actions = actions //設定回覆動作
            };


            return ButtonTemplate;
        }


        internal static ButtonsTemplate DateTimeBtnMake(int QIDnow, int OIDnow, string data)
        {
            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            PreviousHelper previousHelper = new PreviousHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailPrevious = previousHelper.GetPrevious();

            var ButtonTemplate = new isRock.LineBot.ButtonsTemplate();
            var actions = new List<isRock.LineBot.TemplateActionBase>();

            actions.Add(new isRock.LineBot.DateTimePickerAction()
            {
                label = "選擇開始日期和時間",
                mode = "datetime",
                data = $"{DateTime.Now},{questionDetailNext.QID},{questionDetailNext.OID},start"
            });
            actions.Add(new isRock.LineBot.DateTimePickerAction()
            {
                label = "選擇結束日期和時間",
                mode = "datetime",
                data = $"{DateTime.Now},{questionDetailNext.QID},{questionDetailNext.OID},end"
            });

            actions.Add(new isRock.LineBot.PostbackAction()
            {
                label = "上一步",
                data = DateTime.Now.ToString() + $",{questionDetailPrevious.QID}," + $"{questionDetailPrevious.OID},default"
            });
            ButtonTemplate = new isRock.LineBot.ButtonsTemplate()
            {
                text = "訂單",
                // title = "選擇日期",
                //設定圖片
                thumbnailImageUrl = new Uri("https://i220.photobucket.com/albums/dd130/jung_04/DateTime.png"),
                actions = actions
            };


            return ButtonTemplate;
        }

        internal static ButtonsTemplate DateBtnMake(int QIDnow, int OIDnow, string data)
        {
            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            PreviousHelper previousHelper = new PreviousHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailPrevious = previousHelper.GetPrevious();

            var ButtonTemplate = new isRock.LineBot.ButtonsTemplate();
            var actions = new List<isRock.LineBot.TemplateActionBase>();

            actions.Add(new isRock.LineBot.DateTimePickerAction()
            {
                label = "選擇週期開始日期",
                mode = "date",
                data = $"{DateTime.Now},{questionDetailNext.QID},{questionDetailNext.OID},start"
            });
            actions.Add(new isRock.LineBot.DateTimePickerAction()
            {
                label = "選擇週期結束日期",
                mode = "date",
                data = $"{DateTime.Now},{questionDetailNext.QID},{questionDetailNext.OID},end"
            });

            actions.Add(new isRock.LineBot.PostbackAction()
            {
                label = "上一步",
                data = DateTime.Now.ToString() + $",{questionDetailPrevious.QID}," + $"{questionDetailPrevious.OID},default"
            });
            ButtonTemplate = new isRock.LineBot.ButtonsTemplate()
            {
                text = "訂單",
                // title = "選擇日期",
                //設定圖片
                thumbnailImageUrl = new Uri("https://i220.photobucket.com/albums/dd130/jung_04/Date.png"),
                actions = actions
            };


            return ButtonTemplate;
        }

        internal static ButtonsTemplate TimeBtnMake(int QIDnow, int OIDnow, string data)
        {
            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();

            PreviousHelper previousHelper = new PreviousHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailPrevious = previousHelper.GetPrevious();

            var ButtonTemplate = new isRock.LineBot.ButtonsTemplate();
            var actions = new List<isRock.LineBot.TemplateActionBase>();

            actions.Add(new isRock.LineBot.DateTimePickerAction()
            {
                label = "選擇每次開單時間",
                mode = "time",
                data = $"{DateTime.Now},{questionDetailNext.QID},{questionDetailNext.OID},start"
            });
            actions.Add(new isRock.LineBot.DateTimePickerAction()
            {
                label = "選擇每次結單時間",
                mode = "time",
                data = $"{DateTime.Now},{questionDetailNext.QID},{questionDetailNext.OID},end"
            });

            actions.Add(new isRock.LineBot.PostbackAction()
            {
                label = "上一步",
                data = DateTime.Now.ToString() + $",{questionDetailPrevious.QID}," + $"{questionDetailPrevious.OID},default"
            });
            ButtonTemplate = new isRock.LineBot.ButtonsTemplate()
            {
                text = "訂單",
                // title = "選擇日期",
                //設定圖片
                thumbnailImageUrl = new Uri("https://i220.photobucket.com/albums/dd130/jung_04/Time.png"),
                actions = actions
            };


            return ButtonTemplate;
        }


        internal static ButtonsTemplate MakeDeleteMyPeriodOrderConfirmBtn(int QIDnow, int OIDnow, OrderInfo orderInfo)
        {
            //建立actions，作為ButtonTemplate的用戶回覆行為
            var actions = new List<isRock.LineBot.TemplateActionBase>();

            // 製作Button
            NextHelper nextHelper = new NextHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailNext = nextHelper.GetNext();
            actions.Add(new isRock.LineBot.PostbackAction()
            {
                label = "刪除單一週期性訂單",
                data = DateTime.Now.ToString() + $",{questionDetailNext.QID},{questionDetailNext.OID},MyOrderPartition"
            });
            actions.Add(new isRock.LineBot.PostbackAction()
            {
                label = "刪除完整週期性訂單",
                data = DateTime.Now.ToString() + $",{questionDetailNext.QID},{questionDetailNext.OID},MyOrder"
            });

            // 製作取消的button
            CancelHelper cancelHelper = new CancelHelper(QIDnow, OIDnow);
            QuestionDetail questionDetailCancel = cancelHelper.GetCancel();
            actions.Add(new isRock.LineBot.PostbackAction()
            {
                label = "取消",
                data = DateTime.Now.ToString() + $",{questionDetailCancel.QID}," + $"{questionDetailCancel.OID},default"
            });

            //製作模板框架
            var ButtonTemplate = new isRock.LineBot.ButtonsTemplate()
            {
                altText = $"請使用手機查看喔~",
                title = "請選擇週期性訂單的刪除範圍",
                text = $"單一：僅刪除{orderInfo.OrderName}裡{orderInfo.StartTime.ToString("yyyyMMdd")}的訂單，完整：刪除{orderInfo.OrderName}的所有訂單",
                ////設定圖片
                //thumbnailImageUrl = new Uri("https://i220.photobucket.com/albums/dd130/jung_04/p117516087231.jpg"),
                actions = actions //設定回覆動作
            };

            return ButtonTemplate;
        }

       
    }
}