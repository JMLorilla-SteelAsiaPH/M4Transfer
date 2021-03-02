using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.Configuration;

namespace M4Transfer.Class
{
    public class GetConnectionStrings
    {
        private Label lblError;
        private string selectedMill;

        private SqlConnection con = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();

        private static string DbConnString = WebConfigurationManager.ConnectionStrings["TransactionLogConnString"].ToString();

        public GetConnectionStrings(Label aLabel, string aSelectedMill)
        {
            lblError = aLabel;
            selectedMill = aSelectedMill;
        }

        public string GetMillConnectionString()
        {
            using (con = new SqlConnection())
            {
                string millConnString = "";
                con.ConnectionString = DbConnString;
                con.Open();

                try
                {
                    cmd = new SqlCommand("usp_get_mill_connstring", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MillCode", selectedMill);
                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        millConnString = dr["ConnString"].ToString();
                    }
                }
                catch (SqlException ex)
                {
                    lblError.Text = $"Error: {ex.Message}";
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }

                return millConnString;
            }
        }
    }
}