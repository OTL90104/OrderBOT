using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderBot.SQLObject
{
    public class ShopDetailTemp
    {
        public string OrderPartitionID { get; set; }
        public string BuyerID { get; set; }
        public string Item { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public string Note { get; set; }

        public ShopDetailTemp()
        {

        }







    }
}