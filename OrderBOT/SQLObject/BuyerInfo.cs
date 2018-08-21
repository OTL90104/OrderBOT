using OrderBot.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OrderBot.SQLObject
{
    public class BuyerInfo
    {
        public string OrderPartitionID { get; set; }
        public string BuyerID { get; set; }
        public string Item { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public string Note { get; set; }
        public int ID { get;  set; }

        public BuyerInfo()
        {

        }

        public BuyerInfo(string UserID)
        {
            this.BuyerID = UserID;
        }

        internal List<BuyerInfo> GetBuyerInfoListByOrderPartitionIDandBuyerID()
        {
            List<BuyerInfo> buyerInfoList = new List<BuyerInfo>();
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(GetBuyerInfoListByOrderPartitionIDandBuyerIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@BuyerID", this.BuyerID);
                    cmd.Parameters.AddWithValue("@OrderPartitionID", this.OrderPartitionID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BuyerInfo buyerInfo = new BuyerInfo(BuyerID);
                            buyerInfo.OrderPartitionID = reader.GetString(0);
                            buyerInfo.Item = reader.GetString(1);
                            buyerInfo.Price = reader.GetDouble(2);
                            buyerInfo.Amount = reader.GetInt32(3);
                            buyerInfo.Note = reader.GetString(4);
                            buyerInfo.ID = reader.GetInt32(5);
                            buyerInfoList.Add(buyerInfo);
                        }
                    }
                }
            }
            return buyerInfoList;
        }
        private string GetBuyerInfoListByOrderPartitionIDandBuyerIDCmd()
        {
            return @"
                    SELECT 
                           [OrderPartitionID]
                          ,[Item]
                          ,[Price]
                          ,[Amount]
                          ,[Note]
                          ,[ID]
                      FROM [dbo].[BuyerInfoTable]
                    WHERE BuyerID = @BuyerID and OrderPartitionID = @OrderPartitionID
                    ";
        }

        internal int SelectByBuyerIDandOrderPartitionIDandItem()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectByBuyerIDandOrderPartitionIDandItemCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@BuyerID", this.BuyerID);
                    cmd.Parameters.AddWithValue("@OrderPartitionID", this.OrderPartitionID);
                    cmd.Parameters.AddWithValue("@Item", this.Item);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.Amount = reader.GetInt32(0);
                            this.Note = reader.GetString(1);
                            result++;
                        }
                    }
                }
            }
            return result;
        }

        private string SelectByBuyerIDandOrderPartitionIDandItemCmd()
        {
            return @"
                    SELECT 
                           [Amount]
                          ,[Note]
                      FROM [dbo].[BuyerInfoTable]
                    WHERE BuyerID = @BuyerID and OrderPartitionID = @OrderPartitionID and Item = @Item
                    ";
        }

        internal List<string> SelectAllBuyerTotalPartitionID()
        {
            List<string> list = new List<string>();
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectAllBuyerTotalPartitionIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@OrderPartitionID", this.OrderPartitionID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BuyerInfo buyerInfo = new BuyerInfo();
                            buyerInfo.BuyerID = reader.GetString(0);
                            list.Add(buyerInfo.BuyerID);

                        }
                    }
                }
            }
            return list;
        }

        private string SelectAllBuyerTotalPartitionIDCmd()
        {
            return @"
SELECT B.[BuyerID]
  FROM [Linebot].[dbo].[BuyerInfoTable]  AS B
  WHERE [OrderPartitionID]= @OrderPartitionID
  GROUP BY B.[BuyerID];
";
        }


        internal List<BuyerInfo> SelectBuyerSUMItemAndAmountByOrderPartitionID()
        {

            List<BuyerInfo> list = new List<BuyerInfo>();
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectBuyerSUMItemAndAmountByOrderPartitionIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@OrderPartitionID", this.OrderPartitionID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BuyerInfo buyerInfo = new BuyerInfo();
                            buyerInfo.OrderPartitionID = reader.GetString(0);
                            buyerInfo.Item = reader.GetString(1);
                            buyerInfo.Amount = reader.GetInt32(2);
                            list.Add(buyerInfo);

                        }
                    }
                }
            }
            return list;
        }

        private string SelectBuyerSUMItemAndAmountByOrderPartitionIDCmd()
        {
            return @"
SELECT B.OrderPartitionID
	  ,B.Item

	  ,SUM(B.Amount) AS ItemAmount
  FROM [Linebot].[dbo].[BuyerInfoTable] AS B
  WHERE OrderPartitionID = @OrderPartitionID
  GROUP BY B.OrderPartitionID,B.Item 
";
        }

        internal List<BuyerInfo> SelectItemAndAmontByBuyerID()
        {
            List<BuyerInfo> list = new List<BuyerInfo>();
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectItemAndAmontByBuyerIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@BuyerID", this.BuyerID);
                    cmd.Parameters.AddWithValue("@OrderPartitionID", this.OrderPartitionID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BuyerInfo buyerInfo = new BuyerInfo();
                            buyerInfo.OrderPartitionID = reader.GetString(0);
                            buyerInfo.BuyerID = reader.GetString(1);
                            buyerInfo.Item = reader.GetString(2);
                            buyerInfo.Price = reader.GetDouble(3);
                            buyerInfo.Amount = reader.GetInt32(4);
                            buyerInfo.Note = reader.GetString(5);
                            list.Add(buyerInfo);

                        }
                    }
                }
            }
            return list;
        }

        private string SelectItemAndAmontByBuyerIDCmd()
        {
            return @"
SELECT [OrderPartitionID]
      ,[BuyerID]
      ,[Item]
      ,[Price]
      ,[Amount]
      ,[Note]
      ,[CreateTime]
  FROM [Linebot].[dbo].[BuyerInfoTable]
  WHERE [OrderPartitionID]= @OrderPartitionID  and [BuyerID] = @BuyerID;
";
        }

        internal void SelectAllByBuyerInfoID()
        {

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectAllByBuyerInfoIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ID", this.ID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.OrderPartitionID = reader.GetString(0);
                            this.BuyerID = reader.GetString(1);
                            this.Item = reader.GetString(2);
                            this.Price = reader.GetDouble(3);
                            this.Amount = reader.GetInt32(4);
                            this.Note = reader.GetString(5);
                        }
                    }
                }
            }
        }

        private string SelectAllByBuyerInfoIDCmd()
        {
            return @"
SELECT 
      [OrderPartitionID]
      ,[BuyerID]
      ,[Item]
      ,[Price]
      ,[Amount]
      ,[Note]
   
  FROM [Linebot].[dbo].[BuyerInfoTable]
  WHERE [ID] = @ID
";
        }

        internal int DeletByBuyerInfoID()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(DeletByBuyerInfoIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ID", this.ID);
                    DBHelper dBHelper = new DBHelper();
                    result = dBHelper.ExecuteNonQuery(cmd);
                }
            }
            return result;
        }

        private string DeletByBuyerInfoIDCmd()
        {
            return @"
DELETE FROM [dbo].[BuyerInfoTable]
      WHERE [ID] = @ID
";
        }
    }
}