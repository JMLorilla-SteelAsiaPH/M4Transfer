using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using M4Transfer.Class;

namespace M4Transfer
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            if (Session["QCStaffUsername"] == null)
            {
                Response.Redirect("~/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }

            if (!IsPostBack && Session["QCStaffUsername"] != null)
            {
                LoadDropDownSelections();
                SetPage();
            }
            else
            {
                SetPage();
            }
        }

        protected void SetPage()
        {
            InsertDataBtn.Enabled = false;
            LblUser.Text = Session["QCStaffUsername"].ToString();
        }

        protected void LoadDropDownSelections()
        {
            BindMillDropDown bindDDL = new BindMillDropDown(DropDownList1, LabelSqlError);
            bindDDL.BindMillDropDownData();
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ShowData PopulateGridView = new ShowData(LabelSqlError, PhysicalGV, ChemicalGV);

            string selectedDdlValue = DropDownList1.SelectedValue.ToString();

            PopulateGridView.ShowPhysicalData(SearchTxt.Text, selectedDdlValue);
            PopulateGridView.ShowChemicalData(SearchTxt.Text, selectedDdlValue);

            if(!PopulateGridView.CheckIfDataExists(SearchTxt.Text, Session["MillCode"].ToString()))
            {
                InsertDataBtn.Enabled = true;
            }
        }

        protected void InsertDataBtn_Click(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}