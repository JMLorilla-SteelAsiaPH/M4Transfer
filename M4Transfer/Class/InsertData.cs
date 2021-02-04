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

namespace M4Transfer.Class
{
    public class InsertData
    {
        private SqlConnection con = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();

        private Label formLabel;
        private GridView gridView;
        private string selectedSite;

        public InsertData(Label aLabel, GridView aGridView, string aSelectedSite)
        {
            formLabel = aLabel;
            gridView = aGridView;
            selectedSite = aSelectedSite;
        }

        //Change this part later.
        public bool CheckIfExists(string FileNos)
        {
            using (con = new SqlConnection())
            {
                //con.ConnectionString = M4ConString;
                con.Open();
                cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "usp";
                    cmd.Connection = con;
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

            return true;
        }

        //Change this part later.
        public void TransferTestResult()
        {
            using (con = new SqlConnection())
            {
                //con.ConnectionString;
                con.Open();

                cmd = new SqlCommand();

                foreach (GridViewRow g1 in gridView.Rows)
                {
                    cmd.CommandText = "usp";
                    cmd.Parameters.AddWithValue("@FileNum", g1.Cells[0].Text);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }

                con.Close();
                con.Dispose();
            }
        }
    }
}