using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace M4Transfer
{
    public partial class Default : System.Web.UI.Page
    {
        private DBClass DBOperation = new DBClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack && Session["SelectedSite"] == null)
            {
                InsertDataBtn.Enabled = false;
            }
            else
            {
                string SelectedSite = DropDownList1.SelectedValue.ToString().ToLower();
                DBOperation.ShowData(SearchGV, SearchTxt.Text, LabelSqlError, SelectedSite);
                Session.Clear();
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedItem.Value.ToLower() != "m4")
            {
                InsertDataBtn.Enabled = true;
            }
        }

        protected void InsertDataBtn_Click(object sender, EventArgs e)
        {
            string SelectedSite = DropDownList1.SelectedValue.ToString().ToLower();
            DBOperation.InsertData(SearchGV, SelectedSite);
            InsertDataBtn.Enabled = false;
        }
    }
}