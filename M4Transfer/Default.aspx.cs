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
            if (!IsPostBack)
            {
                SetPage();
            }
        }

        protected void SetPage()
        {
            BindMillDropDown bindDDL = new BindMillDropDown(DropDownList1, LabelSqlError);

            InsertDataBtn.Enabled = false;
            bindDDL.BindMillDropDownData();
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ShowData PopulateGridView = new ShowData(LabelSqlError, PhysicalGV, ChemicalGV);

            string selectedDdlValue = DropDownList1.SelectedValue.ToString();

            PopulateGridView.ShowPhysicalData(SearchTxt.Text, selectedDdlValue);
            PopulateGridView.ShowChemicalData(SearchTxt.Text, selectedDdlValue);

            if(!PopulateGridView.CheckIfDataExists(SearchTxt.Text, "M1"))
            {
                InsertDataBtn.Enabled = true;
            }
        }

        protected void InsertDataBtn_Click(object sender, EventArgs e)
        {

        }
    }
}