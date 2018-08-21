using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OrderBot.Utility
{
    public class Question
    {
        public int QID { get; private set; }
        public string QuestionTitle { get; set; }
        public string QuestionText { get; set; }
        public string ImageUrl { get; set; }

        public Question()
        {

        }

        public Question(int QID)
        {
            this.QID = QID;
        }

        public void SelectByQid()
        {


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
                            this.QuestionTitle = reader.GetString(1);
                            this.QuestionText = reader.GetString(2);
                            this.ImageUrl = reader.GetString(3);
                        }
                    }
                }
            }
        }

        private string SelectByQidCmd()
        {
            return @"
                    SELECT  [QID]
                           ,[QuestionTitle]
                           ,[QuestionText]
                           ,[ImageUrl]
                    FROM [Linebot].[dbo].[QuestionTable]
                    WHERE QID = @QID;
                    ";
        }
    }
}