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

namespace M4Transfer
{
    public class DBClass
    {
        private SqlConnection con = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter sda = new SqlDataAdapter();
        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();

        private string M4ConString = WebConfigurationManager.ConnectionStrings["M4ConnectionString"].ConnectionString;

        public string GetSiteConnectionString(string site)
        {
            if (site == "m1")
            {
                return WebConfigurationManager.ConnectionStrings["M1ConnectionString"].ConnectionString;
            }
            else if (site == "m3")
            {
                return WebConfigurationManager.ConnectionStrings["M3ConnectionString"].ConnectionString;
            }
            else if (site == "m5")
            {
                return WebConfigurationManager.ConnectionStrings["M5ConnectionString"].ConnectionString;
            }
            else if (site == "m6")
            {
                return WebConfigurationManager.ConnectionStrings["M6ConnectionString"].ConnectionString;
            }

            return M4ConString;
        }

        //Is there a better way to do/call this function without it requiring 4 parameters?
        public void ShowData(GridView gridView, string FileNos, Label LblTxt, string selectedSite = "")
        {
            using (con)
            {
                con.ConnectionString = GetSiteConnectionString(selectedSite);
                con.Open();

                ds = new DataSet();

                try
                {
                    cmd.CommandText = "SELECT * FROM vw_physical_select_all WHERE FileNos = @FileNos";
                    cmd.Parameters.AddWithValue("@FileNos", FileNos);
                    cmd.Connection = con;
                    sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds);
                    cmd.ExecuteNonQuery();
                    gridView.DataSource = ds;
                    gridView.DataBind();
                }
                catch (SqlException)
                {
                    LblTxt.Text = "The system has detected an error! Please contact the System Administrator or IT for support.";
                }
                finally
                {
                    con.Dispose();
                }
            }
        }

        //Change this part later.
        public bool CheckIfExists(string FileNos)
        {
            using (con)
            {
                con.ConnectionString = M4ConString;
                con.Open();

                try
                {
                    cmd.CommandText = "usp";
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
                catch(SqlException)
                {

                }
                finally
                {
                    con.Dispose();
                }
            }

            return true;
        }

        public void InsertData(GridView gridView, string selectedSite)
        {
            using (con)
            {
                con.ConnectionString = M4ConString;
                con.Open();

                foreach (GridViewRow g1 in gridView.Rows)
                {
                    cmd.CommandText = "usp";
                    cmd.Parameters.AddWithValue("@FileNum", g1.Cells[0].Text);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }

                con.Dispose();
            }
                
        }
    }
}