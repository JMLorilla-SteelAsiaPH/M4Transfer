using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace M4Transfer.Class
{
    public class BindMillDropDown
    {
        private DropDownList millDropDownList;
        private Label LblTxt;

        private SqlConnection con = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter sda = new SqlDataAdapter();
        private DataTable dt = new DataTable();

        public BindMillDropDown(DropDownList aMillDDL, Label aLabel)
        {
            millDropDownList = aMillDDL;
            LblTxt = aLabel;
        }
        public void BindMillDropDownData()
        {
            using (con = new SqlConnection())
            {
                con.ConnectionString = WebConfigurationManager.ConnectionStrings["TransactionLogConnString"].ToString();
                con.Open();

                try
                {
                    sda = new SqlDataAdapter();
                    dt = new DataTable();
                    cmd = new SqlCommand("SELECT mill_code FROM tbl_mills", con);
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                    millDropDownList.DataSource = dt;
                    millDropDownList.DataTextField = "mill_code";
                    millDropDownList.DataValueField = "mill_code";
                    millDropDownList.DataBind();
                }
                catch (SqlException ex)
                {
                    LblTxt.Text = $"Error: {ex.Message}";
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