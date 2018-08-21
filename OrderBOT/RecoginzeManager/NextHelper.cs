using OrderBot.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderBot.RecoginzeManager
{
    public class NextHelper
    {
        public int QID { get; private set; }
        public int OID { get; private set; }

        public NextHelper()
        {

        }

        public NextHelper(int QID, int OID)
        {
            this.QID = QID;
            this.OID = OID;
        }

        internal QuestionDetail GetNext()
        {
            QuestionDetail questionDetail;

            switch (QID)
            {
                case 1:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(2, 0);
                        case 2:
                            return questionDetail = new QuestionDetail(7, 0);
                        case 3:
                            return questionDetail = new QuestionDetail(9, 0);
                        case 4:
                            return questionDetail = new QuestionDetail(11, 0);
                        default:
                            break;

                    }
                    break;

                case 2:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(3, 0);
                        case 2:
                            return questionDetail = new QuestionDetail(22, 1);
                        default:
                            break;
                    }
                    break;

                case 3:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(4, 0);
                        case 2:
                            return questionDetail = new QuestionDetail(32, 1);
                        case 3:
                            break;
                        default:
                            break;
                    }
                    break;

                case 4:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(41, 1);
                        case 2:
                            return questionDetail = new QuestionDetail(42, 1);
                        default:
                            break;
                    }
                    break;

                case 5:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(51, 1);
                        case 2:
                            return questionDetail = new QuestionDetail(52, 1);
                        case 3:
                            return questionDetail = new QuestionDetail(53, 1);
                        default:
                            break;
                    }
                    break;

                case 6:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(61, 1);
                        case 2:
                            return questionDetail = new QuestionDetail(23, 0);
                        case 3:
                            return questionDetail = new QuestionDetail(63, 1);
                        default:
                            break;
                    }
                    break;

                case 7:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(71, 1);
                        case 2:
                            return questionDetail = new QuestionDetail(72, 1);
                        case 3:
                            return questionDetail = new QuestionDetail(73, 1);
                        default:
                            break;
                    }
                    break;

                case 8:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(81, 1);
                        case 2:
                            return questionDetail = new QuestionDetail(82, 1);
                        default:
                            break;
                    }
                    break;

                case 9: // 社團模式>選擇社團動作
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(91, 1);
                        case 2:
                            return questionDetail = new QuestionDetail(92, 1);
                        case 3:
                            return questionDetail = new QuestionDetail(93, 1);
                        default:
                            break;
                    }
                    break;

                case 10: // 社團模式>查詢社團>查詢社團動作
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(101, 1);
                        case 2:
                            return questionDetail = new QuestionDetail(102, 1);
                        default:
                            break;
                    }
                    break;

                case 11:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(17, 0);
                        case 2:
                            return questionDetail = new QuestionDetail(112, 1);
                        default:
                            break;
                    }
                    break;

                case 12:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(121, 1);
                        case 2:
                            return questionDetail = new QuestionDetail(122, 1);
                        default:
                            break;
                    }
                    break;

                case 13:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(14, 0);
                        case 2:
                            return questionDetail = new QuestionDetail(132, 1);
                        default:
                            break;
                    }
                    break;

                case 14:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(141, 1);
                        case 2:
                            return questionDetail = new QuestionDetail(142, 1);
                        default:
                            break;
                    }
                    break;

                case 15:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(151, 1);
                        case 2:
                            return questionDetail = new QuestionDetail(152, 1);
                        case 3:
                            return questionDetail = new QuestionDetail(153, 1);
                        default:
                            break;
                    }
                    break;

                case 16:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(161, 1);
                        case 2:
                            return questionDetail = new QuestionDetail(26, 0);
                        case 3:
                            return questionDetail = new QuestionDetail(163, 1);
                        default:
                            break;
                    }
                    break;

                case 17:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(171, 1);
                        case 2:
                            return questionDetail = new QuestionDetail(172, 1);
                        default:
                            break;
                    }
                    break;

                case 18:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(19, 0);
                        case 2:
                            return questionDetail = new QuestionDetail(182, 1);
                        default:
                            break;
                    }
                    break;

                case 19:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(191, 1);
                        case 2:
                            return questionDetail = new QuestionDetail(192, 1);
                        case 3:
                            return questionDetail = new QuestionDetail(193, 1);
                        case 4:
                            return questionDetail = new QuestionDetail(194, 1);
                        default:
                            break;
                    }
                    break;

                case 20:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(201, 1);
                        case 2:
                            return questionDetail = new QuestionDetail(202, 1);
                        default:
                            break;
                    }
                    break;

                case 21:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(211, 1);
                        case 2:
                            return questionDetail = new QuestionDetail(212, 1);
                        default:
                            break;
                    }
                    break;

                case 22:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(13, 0);
                        default:
                            break;
                    }
                    break;

                case 23:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(231, 1);
                        case 2:
                            return questionDetail = new QuestionDetail(232, 1);
                        default:
                            break;
                    }
                    break;

                case 24:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(241, 1);
                        case 2:
                            return questionDetail = new QuestionDetail(242, 1);
                        default:
                            break;
                    }
                    break;

                case 25:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(251, 1);
                        case 2:
                            return questionDetail = new QuestionDetail(252, 1);
                        default:
                            break;
                    }
                    break;

                case 26:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(261, 1);
                        case 2:
                            return questionDetail = new QuestionDetail(262, 1);
                        default:
                            break;
                    }
                    break;

                case 27:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(271, 1);
                        case 2:
                            return questionDetail = new QuestionDetail(272, 1);
                        default:
                            break;
                    }
                    break;

                case 28:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(29, 0);
                        case 2:
                            return questionDetail = new QuestionDetail(282, 1);
                        default:
                            break;
                    }
                    break;

                case 29:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(291, 1);
                        case 2:
                            return questionDetail = new QuestionDetail(292, 1);
                        case 3:
                            return questionDetail = new QuestionDetail(293, 1);
                        case 4:
                            return questionDetail = new QuestionDetail(294, 1);
                        default:
                            break;
                    }
                    break;

                case 32:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(5, 0);
                        default:
                            break;
                    }
                    break;

                case 41:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(20, 0);
                        default:
                            break;
                    }
                    break;

                case 42:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(42, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(42, 3);
                        case 3:
                            return questionDetail = new QuestionDetail(21, 0);
                        default:
                            break;
                    }
                    break;

                case 51:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(6, 0);
                        default:
                            break;
                    }
                    break;

                case 52:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(52, 2);
                        default:
                            break;
                    }
                    break;

                case 61:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(61, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(61, 3);
                        default:
                            break;
                    }
                    break;

                case 71:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(71, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(71, 3);
                        default:
                            break;
                    }
                    break;

                case 72:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(72, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(8, 0);
                        default:
                            break;
                    }
                    break;

                case 81:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(12, 0);
                        default:
                            break;
                    }
                    break;

                case 82:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(82, 2);
                        default:
                            break;
                    }
                    break;

                case 91:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(91, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(91, 3);
                        case 3:
                            return questionDetail = new QuestionDetail(0, 0);
                        default:
                            break;
                    }
                    break;

                case 92:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(92, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(92, 3);
                        default:
                            break;
                    }
                    break;

                case 93:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(10, 0);
                        default:
                            break;
                    }
                    break;

                case 102:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(102, 2);
                        default:
                            break;
                    }
                    break;

                case 112:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(27, 0);
                        default:
                            break;
                    }
                    break;

                case 121:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(121, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(121, 3);
                        case 21:
                            return questionDetail = new QuestionDetail(121, 3);
                        case 3:
                            return questionDetail = new QuestionDetail(121, 4);
                        case 31:
                            return questionDetail = new QuestionDetail(121, 4);
                        case 4:
                            return questionDetail = new QuestionDetail(121, 5);
                        case 5:
                            return questionDetail = new QuestionDetail(81, 1);
                        default:
                            break;
                    }
                    break;

                case 122:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(122, 2);
                        default:
                            break;
                    }
                    break;

                case 132:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(15, 0);
                        default:
                            break;
                    }
                    break;

                case 141:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(24, 0);
                        default:
                            break;
                    }
                    break;

                case 142:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(142, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(142, 3);
                        case 3:
                            return questionDetail = new QuestionDetail(25, 0);
                        default:
                            break;
                    }
                    break;

                case 151:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(16, 0);
                        default:
                            break;
                    }
                    break;

                case 152:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(152, 2);
                        default:
                            break;
                    }
                    break;

                case 153:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(153, 2);
                        default:
                            break;
                    }
                    break;

                case 161:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(161, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(161, 3);
                        default:
                            break;
                    }
                    break;

                case 171:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(171, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(171, 3);
                        case 3:
                            return questionDetail = new QuestionDetail(171, 4);
                        case 4:
                            return questionDetail = new QuestionDetail(171, 5);
                        case 5:
                            return questionDetail = new QuestionDetail(171, 6);
                        case 6:
                            return questionDetail = new QuestionDetail(171, 4);
                        case 7:
                            return questionDetail = new QuestionDetail(171, 8);
                        default:
                            break;
                    }
                    break;

                case 172:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(18, 0);
                        default:
                            break;
                    }
                    break;

                case 182:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(182, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(182, 3);
                        default:
                            break;
                    }
                    break;

                case 191:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(191, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(191, 3);
                        default:
                            break;
                    }
                    break;

                case 192:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(192, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(192, 3);
                        default:
                            break;
                    }
                    break;

                case 193:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(193, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(193, 3);
                        default:
                            break;
                    }
                    break;

                case 194:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(194, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(194, 3);
                        case 3:
                            return questionDetail = new QuestionDetail(194, 4);
                        case 4:
                            return questionDetail = new QuestionDetail(194, 5);
                        case 5:
                            return questionDetail = new QuestionDetail(194, 1);
                        default:
                            break;
                    }
                    break;

                case 201:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(201, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(201, 3);
                        case 3:
                            return questionDetail = new QuestionDetail(201, 4);
                        case 4:
                            return questionDetail = new QuestionDetail(201, 5);
                        default:
                            break;
                    }
                    break;

                case 202:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(202, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(201, 3);
                        default:
                            break;
                    }
                    break;

                case 211:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(211, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(211, 3);
                        case 3:
                            return questionDetail = new QuestionDetail(21, 99);
                        case 4:
                            return questionDetail = new QuestionDetail(211, 5);
                        case 5:
                            return questionDetail = new QuestionDetail(211, 6);
                        default:
                            break;
                    }
                    break;

                case 212:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(212, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(211, 3);
                        default:
                            break;
                    }
                    break;

                case 231:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(231, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(231, 3);
                        case 3:
                            return questionDetail = new QuestionDetail(231, 4);
                        default:
                            break;
                    }
                    break;

                case 232:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(232, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(231, 3);
                        default:
                            break;
                    }
                    break;

                case 241:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(241, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(241, 3);
                        case 3:
                            return questionDetail = new QuestionDetail(241, 4);
                        case 4:
                            return questionDetail = new QuestionDetail(241, 5);
                        default:
                            break;
                    }
                    break;

                case 242:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(242, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(241, 3);
                        default:
                            break;
                    }
                    break;

                case 251:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(251, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(251, 3);
                        case 3:
                            return questionDetail = new QuestionDetail(25, 99);
                        case 4:
                            return questionDetail = new QuestionDetail(251, 5);
                        case 5:
                            return questionDetail = new QuestionDetail(251, 6);
                        default:
                            break;
                    }
                    break;

                case 252:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(252, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(251, 3);
                        default:
                            break;
                    }
                    break;

                case 261:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(261, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(261, 3);
                        case 3:
                            return questionDetail = new QuestionDetail(261, 4);
                        default:
                            break;
                    }
                    break;

                case 262:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(262, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(261, 3);

                        default:
                            break;
                    }
                    break;

                case 271:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(271, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(271, 3);
                        case 3:
                            return questionDetail = new QuestionDetail(271, 4);
                        case 4:
                            return questionDetail = new QuestionDetail(271, 5);
                        case 5:
                            return questionDetail = new QuestionDetail(271, 6);
                        case 6:
                            return questionDetail = new QuestionDetail(271, 4);
                        case 7:
                            return questionDetail = new QuestionDetail(271, 8);
                        default:
                            break;
                    }
                    break;

                case 272:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(28, 0);
                        default:
                            break;
                    }
                    break;

                case 282:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(282, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(282, 3);
                        default:
                            break;
                    }
                    break;

                case 291:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(291, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(291, 3);
                        default:
                            break;
                    }
                    break;

                case 292:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(292, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(292, 3);
                        default:
                            break;
                    }
                    break;

                case 293:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(293, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(293, 3);
                        default:
                            break;
                    }
                    break;

                case 294:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(294, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(294, 3);
                        case 3:
                            return questionDetail = new QuestionDetail(294, 4);
                        case 4:
                            return questionDetail = new QuestionDetail(294, 5);
                        case 5:
                            return questionDetail = new QuestionDetail(294, 1);
                        default:
                            break;
                    }
                    break;

                case 9999:
                    switch (OID)
                    {
                        case 1:
                            return questionDetail = new QuestionDetail(9999, 2);
                        case 2:
                            return questionDetail = new QuestionDetail(9999, 3);
                        case 21:
                            return questionDetail = new QuestionDetail(9999, 3);
                        case 3:
                            return questionDetail = new QuestionDetail(9999, 4);
                        case 31:
                            return questionDetail = new QuestionDetail(9999, 4);
                        case 4:
                            return questionDetail = new QuestionDetail(9999, 5);
                        case 5:
                            return questionDetail = new QuestionDetail(9999, 1);
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