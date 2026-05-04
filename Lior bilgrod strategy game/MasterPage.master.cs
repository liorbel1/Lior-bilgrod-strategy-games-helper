using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((bool)Session["isLoggedIn"])
        {
            LoginLogout.HRef = "Logout.aspx";
            LoginLogout.InnerText = "hello, " + Session["userName"] + "(click to logout)";
            no.Visible = true;
        }
        else
        {
            LoginLogout.HRef = "Login.aspx";
            LoginLogout.InnerText = "Login";
            no.Visible = false;
        }


        if (!IsPostBack)
        {
            // Display the current server date in the header
            DateLabel.Text = DateTime.Now.ToString("d");

            // Set the image based on the day of the week
            string dayOfWeek = DateTime.Now.DayOfWeek.ToString();
            string imagePath = GetImagePathForDay(dayOfWeek);

            DayImage.ImageUrl = imagePath;
            DayImage.AlternateText = "Image for " + dayOfWeek;
        }

    }
    private string GetImagePathForDay(string dayOfWeek)
    {
        string path = "images/week/";
        // Use different image paths for each day
        switch (dayOfWeek)
        {
            case "Sunday":
                return path + "sunday.jpg";
            case "Monday":
                return path + "monday.jpg";
            case "Tuesday":
                return path + "tuesday.jpg";
            case "Wednesday":
                return path + "wednesday.jpg";
            case "Thursday":
                return path + "thursday.jpg";
            case "Friday":
                return path + "friday.jpg";
            case "Saturday":
                return path + "saturday.jpg";
            default:
                return path + "default.jpg"; // Fallback image
        }
    }

}
