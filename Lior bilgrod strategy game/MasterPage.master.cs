using System;
using System.Data;
using System.Data.SqlClient; // שינינו ל-SqlClient במקום OleDb!
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class MasterPage : System.Web.UI.MasterPage
{
    // --- שורה 13: כאן מחליפים את "YourDatabaseName.mdf" בשם הקובץ שלך מתיקיית App_Data ---
    string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["isLoggedIn"] != null && (bool)Session["isLoggedIn"])
        {
            LoginLogout.HRef = "Logout.aspx";
            LoginLogout.InnerText = "hello, " + Session["userName"] + " (click to logout)";
            no.Visible = true;

            PanelAddComment.Visible = true;
            PanelGuestMessage.Visible = false;
        }
        else
        {
            LoginLogout.HRef = "Login.aspx";
            LoginLogout.InnerText = "Login";
            no.Visible = false;

            PanelAddComment.Visible = false;
            PanelGuestMessage.Visible = true;
        }

        if (!IsPostBack)
        {
            DateLabel.Text = DateTime.Now.ToString("d");
            string dayOfWeek = DateTime.Now.DayOfWeek.ToString();
            DayImage.ImageUrl = GetImagePathForDay(dayOfWeek);
            DayImage.AlternateText = "Image for " + dayOfWeek;

            LoadComments();
        }
    }

    private string GetImagePathForDay(string dayOfWeek)
    {
        string path = "images/week/";
        switch (dayOfWeek)
        {
            case "Sunday": return path + "sunday.jpg";
            case "Monday": return path + "monday.jpg";
            case "Tuesday": return path + "tuesday.jpg";
            case "Wednesday": return path + "wednesday.jpg";
            case "Thursday": return path + "thursday.jpg";
            case "Friday": return path + "friday.jpg";
            case "Saturday": return path + "saturday.jpg";
            default: return path + "default.jpg";
        }
    }

    private void LoadComments()
    {
        string currentPage = Path.GetFileName(Request.Url.AbsolutePath);

        using (SqlConnection conn = new SqlConnection(connString))
        {
            string query = "SELECT Username, CommentText, CommentDate FROM Comments WHERE PageName = @page ORDER BY CommentDate DESC";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@page", currentPage);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    RepeaterComments.DataSource = dt;
                    RepeaterComments.DataBind();
                }
            }
        }
    }

    protected void btnSubmitComment_Click(object sender, EventArgs e)
    {
        if (Session["userName"] != null && !string.IsNullOrWhiteSpace(txtComment.Text))
        {
            string currentPage = Path.GetFileName(Request.Url.AbsolutePath);

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "INSERT INTO Comments (Username, PageName, CommentText, CommentDate) VALUES (@user, @page, @text, @date)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", Session["userName"].ToString());
                    cmd.Parameters.AddWithValue("@page", currentPage);
                    cmd.Parameters.AddWithValue("@text", txtComment.Text);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            txtComment.Text = "";
            LoadComments();
        }
    }
}