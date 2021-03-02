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
            if (!IsPostBack)
            {
                SetPage();
                ClearPageForm();
            }

            if(Session["QCStaffUsername"] != null)
            {
                Response.Redirect("~/Default.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }
            else
            {
                SetPage();
            }
        }

        protected void SetPage()
        {
            BindMillDropDown bindDDL = new BindMillDropDown(DropDownList1, LblSqlError);
            bindDDL.BindMillDropDownData();

            InvalidUserCred.Visible = false;
        }

        protected void ClearPageForm()
        {
            TxtUsername.Text = "";
            TxtPassword.Text = "";
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            LoginClass authenticateUser = new LoginClass(LblSqlError, DropDownList1.Text);
            bool UserAuthenticated = authenticateUser.AuthenticateUser(TxtUsername.Text, TxtPassword.Text); 

            if (UserAuthenticated)
            {
                Session["QCStaffUsername"] = TxtUsername.Text;
                Session["MillCode"] = DropDownList1.Text;
                ClearPageForm();
                Response.Redirect("~/Default.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }
            else
            {
                ClearPageForm();
                InvalidUserCred.Visible = true;
            }

        }
    }
}