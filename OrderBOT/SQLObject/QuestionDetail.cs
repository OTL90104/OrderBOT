using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OrderBot.Utility
{
    public class QuestionDetail
    {
        public int QID { get; private set; }
        public int OID { get; private set; }
        public string AnswerOption { get; set; }

        public QuestionDetail()
        {

        }
        public QuestionDetail(int QID)
        {
            this.QID = QID;
        }

        public QuestionDetail(int QID, int OID)
        {
            this.QID = QID;
            this.OID = OID;
        }

        public List<QuestionDetail> SelectByQidAndOid()
        {
            List<QuestionDetail> list = new List<QuestionDetail>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectByQidAndOidCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@QID", this.QID);
                    cmd.Parameters.AddWithValue("@OID", this.OID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            QuestionDetail details = new QuestionDetail();
                            details.QID = reader.GetInt32(1);
                            details.OID = reader.GetInt32(2);
                            details.AnswerOption = reader.GetString(3);
                            list.Add(details);
                        }
                    }
                }
            }
            return list;
        }

        private string SelectByQidAndOidCmd()
        {
            return @"
                      SELECT [ID]
                            ,[QID]
                            ,[OID]
                            ,[AnswerOption]
                      FROM [Linebot].[dbo].[QuestionDetailTable]
                      WHERE QID = @QID and OID = @OID;
                      ";
        }

        public List<QuestionDetail> SelectByQid()
        {
            List<QuestionDetail> list = new List<QuestionDetail>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectByQidCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@QID", this.QID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            QuestionDetail details = new QuestionDetail();
                            details.QID = reader.GetInt32(1);
                            details.OID = reader.GetInt32(2);
                            details.AnswerOption = reader.GetString(3);
                            list.Add(details);
                        }
                    }
                }
            }
            return list;
        }

        private string SelectByQidCmd()
        {
            return @"
                      SELECT [ID]
                            ,[QID]
                            ,[OID]
                            ,[AnswerOption]
                      FROM [Linebot].[dbo].[QuestionDetailTable]
                      WHERE QID = @QID;
                      ";
        }
    }
}