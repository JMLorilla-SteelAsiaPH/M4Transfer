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
    public partial class Default : Page
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

            }

            SetDBClassValues();
            //DBOperation.BindMillDropDownData(DropDownList1);
        }

        protected void SetDBClassValues()
        {
            DBOperation.gridView1 = PhysicalGV;
            DBOperation.gridView2 = MechanicalGV;
            DBOperation.LblTxt = LabelSqlError;
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedItem.Value.ToLower() != "m4")
            {
                InsertDataBtn.Enabled = true;
            }

            DBOperation.ShowPhysicalData(SearchTxt.Text, DropDownList1.SelectedValue.ToString().ToUpper());
            DBOperation.ShowMechanicalData(SearchTxt.Text, DropDownList1.SelectedValue.ToString().ToUpper());
        }

        protected void InsertDataBtn_Click(object sender, EventArgs e)
        {
            string SelectedSite = DropDownList1.SelectedValue.ToString().ToUpper();
            DBOperation.InsertData(PhysicalGV, SelectedSite);
            InsertDataBtn.Enabled = false;
        }
    }
}