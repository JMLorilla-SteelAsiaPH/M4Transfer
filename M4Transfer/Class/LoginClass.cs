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
       
        //public void AuthenticateUser()
        //{
        //    GetConnectionStrings getConnString = new GetConnectionStrings(lblTxt, selectedMill);
        //    string millConnString = getConnString.GetMillConnectionString();

        //    using (con = new SqlConnection())
        //    {
        //        con.ConnectionString = millConnString;
        //        con.Open();

        //        try
        //        {
        //            cmd.Connection = con;
        //            cmd.CommandText = "SELECT COUNT(QCStaffName) WHERE QCStaffName = @Username AND QCStaffPassword = @Password";
        //            cmd.Parameters.AddWithValue("@Username", );
        //        }
        //        catch(SqlException ex)
        //        {
        //            lblTxt.Text = ex.Message;
        //        }
        //        finally
        //        {
        //            con.Close();
        //            con.Dispose();
        //        }
        //    }
        //}
    }
}