using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrderBot.Utility;

namespace OrderBot.RecoginzeManager
{
    public class PreviousHelper
    {
        public int QID { get; private set; }
        public int OID { get; private set; }

        public PreviousHelper()
        {

        }

        public PreviousHelper(int QID, int OID)
        {
            this.QID = QID;
            this.OID = OID;
        }

        internal QuestionDetail GetPrevious()
        {
            QuestionDetail questionDetail;

            switch (QID)
            {
                case 2:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(1, 0);
                        default:
                            break;
                    }
                    break;

                case 3:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(2, 0);
                        default:
                            break;
                    }
                    break;

                case 4:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(3, 0);
                        default:
                            break;
                    }
                    break;

                case 5:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(32, 1);
                        default:
                            break;
                    }
                    break;

                case 6:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(5, 0);
                        default:
                            break;
                    }
                    break;

                case 7:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(1, 0);
                        default:
                            break;
                    }
                    break;

                case 8:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(72, 2);
                        default:
                            break;
                    }
                    break;

                case 9:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(1, 0);
                        //case 1:
                        //    return questionDetail = new QuestionDetail(1, 0);
                        //case 2:
                        //    return questionDetail = new QuestionDetail(1, 0);
                        case 3:
                            return questionDetail = new QuestionDetail(9, 0);
                        default:
                            break;
                    }
                    break;

                case 10:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(93, 1);
                        //case 1:
                        //    return questionDetail = new QuestionDetail(93, 1);
                        //case 2:
                        //    return questionDetail = new QuestionDetail(93, 1);
                        default:
                            break;
                    }
                    break;

                case 11:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(1, 0);
                        default:
                            break;
                    }
                    break;

                case 12:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(81, 1);
                        default:
                            break;
                    }
                    break;

                case 13:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(22, 1);
                        default:
                            break;
                    }
                    break;

                case 14:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(13, 0);
                        default:
                            break;
                    }
                    break;

                case 15:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(132, 1);
                        default:
                            break;
                    }
                    break;

                case 16:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(15, 0);
                        default:
                            break;
                    }
                    break;

                case 17:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(11, 0);
                        default:
                            break;
                    }
                    break;

                case 18:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(172, 1);
                        default:
                            break;
                    }
                    break;

                case 19:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(18, 0);
                        default:
                            break;
                    }
                    break;

                case 20:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(41, 1);
                        default:
                            break;
                    }
                    break;

                case 21:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(42, 3);
                        default:
                            break;
                    }
                    break;

                case 23:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(6, 0);
                        default:
                            break;
                    }
                    break;

                case 24:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(141, 1);
                        default:
                            break;
                    }
                    break;

                case 25:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(142, 3);
                        default:
                            break;
                    }
                    break;

                case 26:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(16, 0);
                        default:
                            break;
                    }
                    break;

                case 27:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(112, 1);
                        default:
                            break;
                    }
                    break;

                case 28:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(272, 1);
                        default:
                            break;
                    }
                    break;

                case 29:
                    switch (OID)
                    {
                        case 0:
                            return questionDetail = new QuestionDetail(28, 1);
                        default:
                            break;
                    }
                    break;

                case 32:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(3, 0);
                        default:
                            break;
                    }
                    break;

                case 41:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(4, 0);
                        default:
                            break;
                    }
                    break;

                case 42:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(4, 0);
                        case 3:
                            return questionDetail = new QuestionDetail(42, 2);
                        default:
                            break;
                    }
                    break;

                case 51:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(5, 0);
                        default:
                            break;
                    }
                    break;

                case 61:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(6, 0);
                        default:
                            break;
                    }
                    break;

                case 72:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(7, 0);
                        case 2:
                            return questionDetail = new QuestionDetail(72, 1);
                        default:
                            break;
                    }
                    break;

                case 73:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(7, 0);
                        default:
                            break;
                    }
                    break;

                case 81:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(8, 0);
                        default:
                            break;
                    }
                    break;

                case 93:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(9, 0);
                        default:
                            break;
                    }
                    break;

                case 112:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(11, 0);
                        default:
                            break;
                    }
                    break;

                case 121:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(12, 0);
                        default:
                            break;
                    }
                    break;

                case 141:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(14, 0);
                        default:
                            break;
                    }
                    break;

                case 142:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(14, 0);
                        case 3:
                            return questionDetail = new QuestionDetail(142, 2);
                        default:
                            break;
                    }
                    break;

                case 151:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(15, 0);
                        default:
                            break;
                    }
                    break;

                case 161:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(16, 0);
                        default:
                            break;
                    }
                    break;

                case 172:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(17, 0);
                        default:
                            break;
                    }
                    break;

                case 182:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(18, 0);
                        default:
                            break;
                    }
                    break;

                case 194:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(19, 0);
                        default:
                            break;
                    }
                    break;

                case 201:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(20, 0);
                        case 2:
                            return questionDetail = new QuestionDetail(201, 1);
                        default:
                            break;
                    }
                    break;

                case 202:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(20, 0);
                        case 2:
                            return questionDetail = new QuestionDetail(202, 1);
                        default:
                            break;
                    }
                    break;

                case 211:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(21, 0);
                        default:
                            break;
                    }
                    break;

                case 231:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(23, 0);
                        default:
                            break;
                    }
                    break;

                case 241:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(24, 0);
                        default:
                            break;
                    }
                    break;

                case 251:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(25, 0);
                        default:
                            break;
                    }
                    break;

                case 261:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(26, 0);
                        default:
                            break;
                    }
                    break;

                case 272:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(27, 0);
                        default:
                            break;
                    }
                    break;

                case 282:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(28, 0);
                        default:
                            break;
                    }
                    break;

                case 294:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(29, 0);
                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }
            return questionDetail = new QuestionDetail(0, 0);
        }
    }
}