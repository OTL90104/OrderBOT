using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrderBot.Utility;

namespace OrderBot.RecoginzeManager
{
    public class CancelHelper
    {
        public int QID { get; private set; }
        public int OID { get; private set; }

        public CancelHelper()
        {
        }

        public CancelHelper(int QID, int OID)
        {
            this.QID = QID;
            this.OID = OID;
        }

        internal QuestionDetail GetCancel()
        {
            QuestionDetail questionDetail;

            switch (QID)
            {
                case 9:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(1, 0);
                        case 2:
                            return questionDetail = new QuestionDetail(1, 0);
                        case 3:
                            return questionDetail = new QuestionDetail(1, 0);
                        default:
                            break;
                    }
                    break;

                case 10:
                    switch (OID)
                    {
                        case 2:
                            return questionDetail = new QuestionDetail(9, 99999);
                        default:
                            break;
                    }
                    break;

                case 32:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(2, 0);
                        default:
                            break;
                    }
                    break;

                case 51:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(3, 0);
                        default:
                            break;
                    }
                    break;

                case 52:
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
                            return questionDetail = new QuestionDetail(5, 0);
                        case 2:
                            return questionDetail = new QuestionDetail(5, 0);
                        default:
                            break;
                    }
                    break;

                case 71:
                    switch (OID)
                    {
                        case 2:
                            return questionDetail = new QuestionDetail(7, 0);
                        default:
                            break;
                    }
                    break;

                case 72:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(1, 0);
                        case 2:
                            return questionDetail = new QuestionDetail(1, 0);
                        default:
                            break;
                    }
                    break;

                case 73:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(1, 0);
                        default:
                            break;
                    }
                    break;

                case 81:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(7, 0);
                        default:
                            break;
                    }
                    break;

                case 82:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(7, 0);
                        default:
                            break;
                    }
                    break;

                case 91:
                    switch (OID)
                    {
                        case 2:
                            return questionDetail = new QuestionDetail(9, 0);
                        default:
                            break;
                    }
                    break;

                case 92:
                    switch (OID)
                    {
                        case 2:
                            return questionDetail = new QuestionDetail(9, 0);
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

                case 102:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(10, 0);
                        default:
                            break;
                    }
                    break;

                case 112:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(1, 0);
                        default:
                            break;
                    }
                    break;
                                 
                case 121:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(12, 0);
                        case 2:
                            return questionDetail = new QuestionDetail(121, 21);
                        case 3:
                            return questionDetail = new QuestionDetail(121, 31);
                        case 4:
                            return questionDetail = new QuestionDetail(12, 0);
                        case 5:
                            return questionDetail = new QuestionDetail(121, 6);
                        default:
                            break;
                    }
                    break;

                case 122:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(12, 0);
                        default:
                            break;
                    }
                    break;

                case 151:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(13, 0);
                        default:
                            break;
                    }
                    break;

                case 152:
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
                            return questionDetail = new QuestionDetail(15, 0);
                        case 2:
                            return questionDetail = new QuestionDetail(15, 0);
                        default:
                            break;
                    }
                    break;

                case 171:
                    switch (OID)
                    {
                        case 6:
                            return questionDetail = new QuestionDetail(171, 7);
                        case 7:
                            return questionDetail = new QuestionDetail(17, 0);
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
                            return questionDetail = new QuestionDetail(17, 0);
                        default:
                            break;
                    }
                    break;

                case 191:
                    switch (OID)
                    {
                        case 2:
                            return questionDetail = new QuestionDetail(19, 0);
                        default:
                            break;
                    }
                    break;

                case 192:
                    switch (OID)
                    {
                        case 2:
                            return questionDetail = new QuestionDetail(19, 0);
                        default:
                            break;
                    }
                    break;

                case 193:
                    switch (OID)
                    {
                        case 2:
                            return questionDetail = new QuestionDetail(19, 0);
                        default:
                            break;
                    }
                    break;

                case 194:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(19, 0);
                        case 2:
                            return questionDetail = new QuestionDetail(19, 0);
                        case 5:
                            return questionDetail = new QuestionDetail(194, 6);
                        default:
                            break;
                    }
                    break;

                case 201:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(4, 0);
                        case 2:
                            return questionDetail = new QuestionDetail(20, 0);
                        case 3:
                            return questionDetail = new QuestionDetail(4, 0);
                        case 4:
                            return questionDetail = new QuestionDetail(4, 0);
                        default:
                            break;
                    }
                    break;

                case 211:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(4, 0);
                        case 2:
                            return questionDetail = new QuestionDetail(21, 0);
                        case 3:
                            return questionDetail = new QuestionDetail(211, 4);
                        case 4:
                            return questionDetail = new QuestionDetail(4, 0);
                        default:
                            break;
                    }
                    break;

                case 231:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(6, 0);
                        case 2:
                            return questionDetail = new QuestionDetail(23, 0);
                        case 3:
                            return questionDetail = new QuestionDetail(6, 0);
                        default:
                            break;
                    }
                    break;

                case 241:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(14, 0);
                        case 2:
                            return questionDetail = new QuestionDetail(24, 0);
                        case 3:
                            return questionDetail = new QuestionDetail(14, 0);
                        default:
                            break;
                    }
                    break;

                case 251:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(14, 0);
                        case 2:
                            return questionDetail = new QuestionDetail(25, 0);
                        case 3:
                            return questionDetail = new QuestionDetail(251, 4);
                        case 4:
                            return questionDetail = new QuestionDetail(14, 0);
                        default:
                            break;
                    }
                    break;

                case 261:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(16, 0);
                        case 2:
                            return questionDetail = new QuestionDetail(26, 0);
                        case 3:
                            return questionDetail = new QuestionDetail(16, 0);
                        default:
                            break;
                    }
                    break;

                case 271:
                    switch (OID)
                    {
                        case 6:
                            return questionDetail = new QuestionDetail(271, 7);
                        case 7:
                            return questionDetail = new QuestionDetail(27, 0);
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
                            return questionDetail = new QuestionDetail(27, 0);
                        default:
                            break;
                    }
                    break;

                case 291:
                    switch (OID)
                    {
                        case 2:
                            return questionDetail = new QuestionDetail(29, 0);
                        default:
                            break;
                    }
                    break;

                case 292:
                    switch (OID)
                    {
                        case 2:
                            return questionDetail = new QuestionDetail(29, 0);
                        default:
                            break;
                    }
                    break;

                case 293:
                    switch (OID)
                    {
                        case 2:
                            return questionDetail = new QuestionDetail(29, 0);
                        default:
                            break;
                    }
                    break;

                case 294:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(29, 0);
                        case 2:
                            return questionDetail = new QuestionDetail(29, 0);
                        case 5:
                            return questionDetail = new QuestionDetail(294, 6);
                        default:
                            break;
                    }
                    break;

                case 9999:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(9999, 6);
                        case 2:
                            return questionDetail = new QuestionDetail(9999, 21);
                        case 3:
                            return questionDetail = new QuestionDetail(9999, 31);
                        case 4:
                            return questionDetail = new QuestionDetail(9999, 1);
                        case 5:
                            return questionDetail = new QuestionDetail(9999, 6);
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