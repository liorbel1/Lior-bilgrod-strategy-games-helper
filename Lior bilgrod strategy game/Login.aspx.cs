using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // אם המשתמש כבר מחובר, אין טעם שיהיה בדף התחברות
        if (Session["isLoggedIn"] != null && (bool)Session["isLoggedIn"])
        {
            Response.Redirect("Default.aspx");
        }

        if (IsPostBack)
        {
            string uName = Request.Form["userName"];
            string pswd = Request.Form["password"];

            int userType = GetUserTypeFromDB(uName, pswd);

            if (userType > 0)
            {
                Session["userName"] = uName; // וודא שזה userName עם N גדולה!
                Session["isLoggedIn"] = true;

                if (userType == 2)
                    Session["isAdmin"] = true;
                else
                    Session["isAdmin"] = false;

                Response.Redirect("Default.aspx");
            }
            else
            {
                LoginResult.InnerText = "Username or password incorrect.";
            }
        }
    }

    private int GetUserTypeFromDB(string uName, string pswd)
    {
        string dbPath = this.MapPath("App_Data/Database.mdf");
        DAL dal = new DAL(dbPath);

        // שימוש בשמות העמודות כפי שהם מופיעים בטבלה (user_name ו-pswd)
        string sqlQuery = "SELECT * FROM Users WHERE user_name = '" + uName + "' AND pswd = '" + pswd + "'";
        DataTable dt = dal.GetDataTable(sqlQuery);

        if (dt != null && dt.Rows.Count == 1)
        {
            DataRow row = dt.Rows[0];

            // בדיקה בטוחה של is_admin
            if (row["is_admin"] != DBNull.Value && Convert.ToBoolean(row["is_admin"]))
                return 2; // אדמין

            return 1; // משתמש רגיל
        }

        return 0; // לא נמצא
    }
}