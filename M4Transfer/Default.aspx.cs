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
                return;
            }

            if (!IsPostBack && Session["QCStaffUsername"] != null)
            {
                LoadDropDownSelections();
                SetPage();
            }
        }

        protected void SetPage()
        {
            LblUser.Text = Session["QCStaffUsername"].ToString();
        }

        protected void LoadDropDownSelections()
        {
            BindMillDropDown bindDDL = new BindMillDropDown(DropDownList1, LabelSqlError);
            bindDDL.BindMillDropDownData();
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            CheckData dataCheck = new CheckData();

            ShowData PopulateGridView = new ShowData(LabelSqlError, PhysicalGV, ChemicalGV);
            
            string userMillCode = Session["MillCode"].ToString();
            string selectedDdlValue = DropDownList1.SelectedValue.ToString();
            string searchFileNum = SearchTxt.Text.Trim();

            PopulateGridView.ShowPhysicalData(searchFileNum, selectedDdlValue);
            PopulateGridView.ShowChemicalData(searchFileNum, selectedDdlValue);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
            return;
        }

        //protected void TransferPhysical_Click(object sender, EventArgs e)
        //{
        //    InsertData insertToDb = new InsertData(LabelSqlError, Session["MillCode"].ToString());

        //    string FileNum = SearchTxt.Text.Trim();

        //    insertToDb.TransferPhyiscalTestResult(FileNum);
        //}

        //protected void TransferChemical_Click(object sender, EventArgs e)
        //{
        //    InsertData insertToDb = new InsertData(LabelSqlError, Session["MillCode"].ToString());

        //    string FileNum = SearchTxt.Text.Trim();

        //    insertToDb.TransferMechanicalTestResult(FileNum);
        //}
    }
}