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

        public Label LblTxt;
        public GridView gridView1;
        public GridView gridView2;

        public string GetMillConnectionString(string mill)
        {
            using (con = new SqlConnection())
            {
                string MillConnString = "";
                con.ConnectionString = WebConfigurationManager.ConnectionStrings["TransactionLogConnString"].ToString();
                con.Open();
                try
                {
                    cmd = new SqlCommand("usp_get_mill_connstring", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MillCode", mill);
                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        MillConnString = dr["connection_string"].ToString();
                    }
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

                return MillConnString;
            }
        }

        public void BindMillDropDownData(DropDownList millDropDownList)
        {
            using (con = new SqlConnection())
            {
                con.ConnectionString = WebConfigurationManager.ConnectionStrings["TransactionLogConnString"].ToString();
                con.Open();

                try
                {
                    sda = new SqlDataAdapter();
                    dt = new DataTable();
                    cmd = new SqlCommand("SELECT mill FROM mills", con);
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                    millDropDownList.DataSource = dt;
                    millDropDownList.DataTextField = "mill";
                    millDropDownList.DataValueField = "mill";
                    millDropDownList.DataBind();
                }
                catch(SqlException ex)
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

        public void ShowPhysicalData(string FileNos, string selectedSite)
        {
            //string MillConnString = GetMillConnectionString(selectedSite);

            string MillConnString = "Server=ERAGON;Database=QANet;User Id=sb;Password=sb;";

            using (con = new SqlConnection())
            {
                con.ConnectionString = MillConnString;
                con.Open();

                try
                {
                    cmd = new SqlCommand();
                    //cmd.CommandText = "SELECT * FROM vw_select_physical_m4_transfer WHERE FileNos = @FileNos";
                    cmd.CommandText = "SELECT * FROM gago WHERE FileNos = @FileNos";
                    cmd.Parameters.AddWithValue("@FileNos", FileNos);
                    cmd.Connection = con;
                    sda = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    cmd.ExecuteNonQuery();
                    gridView1.DataSource = ds;
                    gridView1.DataBind();
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

        public void ShowMechanicalData(string FileNos, string selectedSite)
        {
            //string MillConnString = GetMillConnectionString(selectedSite);

            string MillConnString = "Server=ERAGON;Database=QANet;User Id=sb;Password=sb;";

            using (con = new SqlConnection())
            {
                con.ConnectionString = MillConnString;
                con.Open();

                try
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "SELECT * FROM vw_select_mechanical_m4_transfer WHERE FileNos = @FileNos";
                    cmd.Parameters.AddWithValue("@FileNos", FileNos);
                    cmd.Connection = con;
                    sda = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    cmd.ExecuteNonQuery();
                    gridView2.DataSource = ds;
                    gridView2.DataBind();
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
                catch(SqlException ex)
                {
                    LblTxt.Text = $"Error: {ex.Message}";
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
        public void InsertData(GridView gridView, string selectedSite)
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