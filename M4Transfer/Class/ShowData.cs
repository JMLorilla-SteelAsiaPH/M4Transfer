using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.Configuration;

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

        public string GetMillConnectionString(string mill)
        {
            using (con = new SqlConnection())
            {
                string millConnString = "";
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
                        millConnString = dr["ConnString"].ToString();
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

                return millConnString;
            }
        }

        public void ShowPhysicalData(string FileNos, string selectedSite)
        {
            string millConnString = GetMillConnectionString(selectedSite);

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
            string millConnString = GetMillConnectionString(selectedSite);

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
            int x = 0;

            string millConnString = GetMillConnectionString(selectedSite);

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