using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            if ((Request.Form["userName"] == "test" && Request.Form["password"] == "1234")||(Request.Form["userName"] == "avi" && Request.Form["password"] == "4321"))
            {
                Session["userName"] = Request.Form["userName"];
                Session["isLoggedIn"] = true;

                Response.Redirect("Default.aspx");
            }
            else
            {
                LoginResult.InnerText = "username or password are incorrect";
            }
        }

    }
}