using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using M4Transfer.Class;

namespace M4Transfer
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPage();
        }

        protected void SetPage()
        {
            BindMillDropDown bindDDL = new BindMillDropDown(DropDownList1, LblSqlError);
            bindDDL.BindMillDropDownData();
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            Session["QCStaffUser"] = TxtUsername.Text;
            Response.Redirect("~/Default.aspx");
        }
    }
}