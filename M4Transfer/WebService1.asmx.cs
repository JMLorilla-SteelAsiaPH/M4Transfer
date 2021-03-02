using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Web.Script.Serialization;

namespace M4Transfer
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        private static readonly NLog.Logger devLogger = NLog.LogManager.GetCurrentClassLogger();

        private static string DbConnString = WebConfigurationManager.ConnectionStrings["TransactionLogConnString"].ToString();

        private string GetMillConnectionString(string selectedMill)
        {
            using (SqlConnection con = new SqlConnection())
            {
                string millConnString = "";
                con.ConnectionString = DbConnString;
                con.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand("usp_get_mill_connstring", con);
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
                    devLogger.Debug(ex, $"Dev Error: {ex.Message}");
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }

                return millConnString;
            }
        }

        [WebMethod]
        public string InsertPhysical(string FileNos, string selectedSite, int checkMode)
        {
            string millConnString = GetMillConnectionString(selectedSite);
            string returnString = $"success";

            using (SqlConnection con = new SqlConnection(millConnString))
            {
                con.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand("usp_m3_qa_result_transfer", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FileNum", FileNos);
                    cmd.Parameters.AddWithValue("@CheckMode", checkMode);
                    cmd.CommandTimeout = 200;
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    devLogger.Debug(ex, $"Dev Error: {ex.Message}");
                    returnString = $"{ex.Message}";
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }

            return returnString;
        }

        [WebMethod]
        public string InsertChemical(string FileNos, string selectedSite, int checkMode)
        {
            string millConnString = GetMillConnectionString(selectedSite);
            string returnString = $"success";

            using (SqlConnection con = new SqlConnection(millConnString))
            {
                con.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand("usp_m3_qa_result_transfer", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FileNum", FileNos);
                    cmd.Parameters.AddWithValue("@CheckMode", checkMode);
                    cmd.CommandTimeout = 200;
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    devLogger.Debug(ex, $"Dev Error: {ex.Message}");
                    returnString = $"{ex.Message}";
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }

            return returnString;
        }

        [WebMethod]
        public bool CheckIfDataExists(string FileNos, string selectedSite, int CheckMode)
        {
            string millConnString = GetMillConnectionString(selectedSite);

            int x = 0;

            using (SqlConnection con = new SqlConnection(millConnString))
            {
                con.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand("usp_check_if_qa_result_exists", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FileNum", FileNos);
                    cmd.Parameters.AddWithValue("@CheckMode", CheckMode);
                    x = (int)cmd.ExecuteScalar();
                }
                catch (SqlException ex)
                {
                    //LblTxt.Text = ex.Message;
                    devLogger.Debug(ex, $"Dev Error: {ex.Message}");
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }

            if (x > 0)
            {
                return true;
            }

            return false;
        }
    }
}
