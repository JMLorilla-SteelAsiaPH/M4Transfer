using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using M4Transfer.Class;

namespace M4Transfer.Class
{
    public class LoginClass
    {
        private Label lblTxt;
        private string selectedMill;

        private SqlConnection con = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();

        public LoginClass(Label aLabel, string aSelectedMill)
        {
            lblTxt = aLabel;
            selectedMill = aSelectedMill;
        }

        public bool AuthenticateUser(string user, string password)
        {
            GetConnectionStrings getConnString = new GetConnectionStrings(lblTxt, selectedMill);
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
                    cmd.CommandText = "SELECT COUNT(QCStaffName) FROM UserAccess WHERE QCStaffName = @Username AND QCStaffPassword = @Password";
                    cmd.Parameters.AddWithValue("@Username", user);
                    cmd.Parameters.AddWithValue("@Password", password);
                    x = (int)cmd.ExecuteScalar();
                }
                catch (SqlException ex)
                {
                    lblTxt.Text = $"Error: {ex.Message}";
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