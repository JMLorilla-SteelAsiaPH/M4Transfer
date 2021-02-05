using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using M4Transfer.Class;

namespace M4Transfer
{
    public class ShowData
    {
        private Label LblTxt;
        private GridView gridView1;
        private GridView gridView2;

        private SqlConnection con = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter sda = new SqlDataAdapter();
        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();

        //Constructor
        public ShowData(Label aLabel, GridView aGridView1, GridView aGridView2)
        {
            LblTxt = aLabel;
            gridView1 = aGridView1;
            gridView2 = aGridView2;
        }

        public void ShowPhysicalData(string FileNos, string selectedSite)
        {
            GetConnectionStrings getConnString = new GetConnectionStrings(LblTxt, selectedSite);
            string millConnString = getConnString.GetMillConnectionString();

            using (con = new SqlConnection())
            {
                con.ConnectionString = millConnString;
                con.Open();

                try
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "SELECT * FROM vw_select_physical_m4_transfer WHERE FileNos = @FileNos";
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

        public void ShowChemicalData(string FileNos, string selectedSite)
        {
            GetConnectionStrings getConnString = new GetConnectionStrings(LblTxt, selectedSite);
            string millConnString = getConnString.GetMillConnectionString();

            using (con = new SqlConnection())
            {
                con.ConnectionString = millConnString;
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

        public bool CheckIfDataExists(string FileNos, string selectedSite)
        {
            GetConnectionStrings getConnString = new GetConnectionStrings(LblTxt, selectedSite);
            string millConnString = getConnString.GetMillConnectionString();

            int x = 0;

            using (con = new SqlConnection())
            {
                con.ConnectionString = millConnString;
                con.Open();

                try
                {
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT COUNT(FileNos) FROM Physical WHERE FileNos = @FileNos";
                    cmd.Parameters.AddWithValue("@FileNos", FileNos);
                    x = (int)cmd.ExecuteScalar();
                }
                catch(SqlException ex)
                {
                    LblTxt.Text = ex.Message;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }

            if(x > 0)
            {
                return true;
            }

            return false;
        }
    }
}