using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Admin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(bool)Session["isAdmin"])
        {
            Response.Redirect("Unauthorized.aspx");
        }

        updateTable();
        generateAdminTable();

    }

    private void generateAdminTable()
    {
        string dbPath = this.MapPath("App_Data/Database.mdf");
        DAL dal = new DAL(dbPath);

        string sqlQuery =
            "SELECT * FROM Users";

        DataTable dt = dal.GetDataTable(sqlQuery);

        HtmlTableRow title = new HtmlTableRow();

        for (int j = 0; j < dt.Columns.Count; j++)
        {
            HtmlTableCell cell = new HtmlTableCell();
            cell.InnerText = dt.Columns[j].ColumnName;
            title.Cells.Add(cell);
        }

        adminTable.Rows.Add(title);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow drow = dt.Rows[i];
            HtmlTableRow row = new HtmlTableRow();

            for (int j = 0; j < dt.Columns.Count; j++)
            {
                HtmlTableCell cell = new HtmlTableCell();
                cell.InnerText = drow[j].ToString();
                row.Cells.Add(cell);
            }
            row.Cells.Add(GenerateTableButtons(drow));

            adminTable.Rows.Add(row);
        }

    }

    private HtmlTableCell GenerateTableButtons(DataRow row)
    {
        HtmlTableCell cell = new HtmlTableCell();

        string str = "";
        // Code for adding a column for delete/set buttons //////////////////

        str += "\t\t<form method=\"post\" action=\"Admin.aspx\">\n";
        str += "\t\t\t<input type=\"hidden\" id=\"userName\" name=\"userName\" value=\"" + row["user_name"].ToString() + "\"/>\n";
        str += "\t\t\t<input type=\"submit\" name=\"delete\" value=\"Delete\"/>\n";
        if (row["is_admin"].ToString() == "False")
        {
            str += "\t\t\t<input type=\"submit\" name=\"setAdmin\" value=\"Set Admin\"/>\n";
        }
        else
        {
            str += "\t\t\t<input type=\"submit\" name=\"resetAdmin\" value=\"Reset Admin\"/>\n";
        }
        str += "\t\t</form>\n";
        cell.InnerHtml = str;

        return cell;
    }

    private void updateTable()
    {
        string dbPath = this.MapPath("App_Data/Database.mdf");
        DAL dal = new DAL(dbPath);

        string sqlQuery = "";
        if (Request.Form["delete"] != null)
        {
            sqlQuery = "DELETE FROM Users WHERE user_name = '" + Request.Form["userName"] + "'";
            dal.UpdateDB(sqlQuery);
        }
        else if (Request.Form["setAdmin"] != null)
        {
            sqlQuery = "UPDATE Users SET is_admin=1 WHERE user_name = '" + Request.Form["userName"] + "'";
            dal.UpdateDB(sqlQuery);
        }
        else if (Request.Form["resetAdmin"] != null)
        {
            sqlQuery = "UPDATE Users SET is_admin=0 WHERE user_name = '" + Request.Form["userName"] + "'";
            dal.UpdateDB(sqlQuery);
        }

    }

}