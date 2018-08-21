using OrderBot.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OrderBot.SQLObject
{
    public class BuyerTemp
    {
        public string OrderPartitionID { get; set; }
        public string BuyerID { get; set; }
        public string Item { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public string Note { get; set; }

        public BuyerTemp()
        {

        }

        public BuyerTemp(string UserID)
        {
            this.BuyerID = UserID;
        }

        internal void SelectByBuyerID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectByBuyerIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@BuyerID", this.BuyerID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.OrderPartitionID = reader.GetString(0);
                            this.Item = reader.GetString(1);
                            this.Price = reader.GetDouble(2);
                            this.Amount = reader.GetInt32(3);
                            this.Note = reader.GetString(4);
                        }
                    }
                }
            }
        }

        private string SelectByBuyerIDCmd()
        {
            return @"
                    SELECT 
                           [OrderPartitionID]
                          ,[Item]
                          ,[Price]
                          ,[Amount]
                          ,[Note]
                      FROM [dbo].[BuyerInfoTempTable]
                    WHERE BuyerID = @BuyerID
                    ";
        }

        internal void UpdateNoteByBuyerID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateNoteByBuyerIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@BuyerID", this.BuyerID);
                    cmd.Parameters.AddWithValue("@Note", this.Note);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        private string UpdateNoteByBuyerIDCmd()
        {
            return @"
                    UPDATE [dbo].[BuyerInfoTempTable]
                       SET
                          [Note] = @Note
                     WHERE BuyerID = @BuyerID
                    ";
        }

        internal void InsertBuyerItemInfoTemp()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(InsertBuyerItemInfoTempCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@BuyerID", this.BuyerID);
                    cmd.Parameters.AddWithValue("@OrderPartitionID", this.OrderPartitionID);
                    cmd.Parameters.AddWithValue("@Item", this.Item);
                    cmd.Parameters.AddWithValue("@Price", this.Price);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        private string InsertBuyerItemInfoTempCmd()
        {
            return @"
                   INSERT INTO [dbo].[BuyerInfoTempTable]
                           ([OrderPartitionID]
                           ,[BuyerID]
                           ,[Item]
                           ,[Price]
                           ,[Amount]
                           ,[Note])
                     VALUES
                           (@OrderPartitionID
                           ,@BuyerID
                           ,@Item
                           ,@Price
                           ,1
                           ,'no')
                    ";
        }

        internal void UpdateAmountByBuyerID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateAmountByBuyerIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@BuyerID", this.BuyerID);
                    cmd.Parameters.AddWithValue("@Amount", this.Amount);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        private string UpdateAmountByBuyerIDCmd()
        {
            return @"
                    UPDATE [dbo].[BuyerInfoTempTable]
                       SET
                          [Amount] = @Amount
                     WHERE BuyerID = @BuyerID
                    ";
        }

        internal void InsertAllTempToBuyerInfoTable()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(InsertAllTempToBuyerInfoTableCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@OrderPartitionID", this.OrderPartitionID);
                    cmd.Parameters.AddWithValue("@BuyerID", this.BuyerID);
                    cmd.Parameters.AddWithValue("@Item", this.Item);
                    cmd.Parameters.AddWithValue("@Price", this.Price);
                    cmd.Parameters.AddWithValue("@Amount", this.Amount);
                    cmd.Parameters.AddWithValue("@Note", this.Note);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        private string InsertAllTempToBuyerInfoTableCmd()
        {
            return @"
                   INSERT INTO [dbo].[BuyerInfoTable]
                           ([OrderPartitionID]
                           ,[BuyerID]
                           ,[Item]
                           ,[Price]
                           ,[Amount]
                           ,[Note]
                           ,[CreateTime])
                     VALUES
                           (@OrderPartitionID
                           ,@BuyerID
                           ,@Item
                           ,@Price
                           ,@Amount
                           ,@Note
                           ,SYSDATETIME())
                    ";
        }

        internal void UpdateAllTempToBuyerInfoTableByOldBuyerInfoID(string OldBuyerInfoID)
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateAllTempToBuyerInfoTableByItemOldCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@OldBuyerInfoID", OldBuyerInfoID);
                    cmd.Parameters.AddWithValue("@OrderPartitionID", this.OrderPartitionID);
                    cmd.Parameters.AddWithValue("@BuyerID", this.BuyerID);
                    cmd.Parameters.AddWithValue("@Item", this.Item);
                    cmd.Parameters.AddWithValue("@Price", this.Price);
                    cmd.Parameters.AddWithValue("@Amount", this.Amount);
                    cmd.Parameters.AddWithValue("@Note", this.Note);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        private string UpdateAllTempToBuyerInfoTableByItemOldCmd()
        {
            return @"
                    UPDATE [dbo].[BuyerInfoTable]
                       SET                                     
                           [Item] = @Item               
                          ,[Price] = @Price              
                          ,[Amount] = @Amount             
                          ,[Note] = @Note               
                          ,[CreateTime] = SYSDATETIME()         
                     WHERE [ID] = @OldBuyerInfoID
                    ";
                    }

        internal void DeleteByBuyerID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(DeleteByBuyerIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@BuyerID", this.BuyerID);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        private string DeleteByBuyerIDCmd()
        {
            return @"
                    DELETE FROM [dbo].[BuyerInfoTempTable]
                          WHERE BuyerID = @BuyerID
                    ";
        }
    }
}