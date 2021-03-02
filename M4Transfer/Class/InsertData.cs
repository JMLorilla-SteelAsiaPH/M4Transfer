using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Configuration;
using System.Globalization;
using M4Transfer.Class;

namespace M4Transfer.Class
{
    public class InsertData
    {
        private SqlConnection con = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();

        private Label formLabel;
        //private GridView gridView;
        private string selectedSite;

        public InsertData(Label aLabel, string aSelectedSite)
        {
            formLabel = aLabel;
            //gridView = aGridView;
            selectedSite = aSelectedSite;
        }

        public void TransferPhyiscalTestResult(string FileNos)
        {
            GetConnectionStrings getConnString = new GetConnectionStrings(formLabel, selectedSite);
            string millConnString = getConnString.GetMillConnectionString();

            using (con = new SqlConnection(millConnString))
            {
                try
                {
                    cmd = new SqlCommand("usp_m3_qa_result_transfer");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FileNum", FileNos);
                    cmd.Parameters.AddWithValue("@CheckMode", 0);
                    cmd.Connection = con;
                    cmd.CommandTimeout = 200;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    formLabel.Text = $"Error: {ex.Message}";
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        //Change this part later.
        public void TransferMechanicalTestResult(string FileNos)
        {
            GetConnectionStrings getConnString = new GetConnectionStrings(formLabel, selectedSite);
            string millConnString = getConnString.GetMillConnectionString();

            using (con = new SqlConnection(millConnString))
            {
                try
                {
                    cmd = new SqlCommand("usp_m3_qa_result_transfer");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FileNum", FileNos);
                    cmd.Parameters.AddWithValue("@CheckMode", 1);
                    cmd.Connection = con;
                    cmd.CommandTimeout = 200;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    formLabel.Text = $"Error: {ex.Message}";
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
    }
}