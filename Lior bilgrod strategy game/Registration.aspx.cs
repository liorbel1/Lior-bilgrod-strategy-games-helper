using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RegistrationResult.InnerText = "";

        if (IsPostBack)
        {
            if (Form_Validation() && Insert_Into_Database())
            {
                RegistrationResult.InnerText = firstName.Value + ", Registration successful! You can now log in.";
                // אופציונלי: ריקון השדות אחרי הרשמה מוצלחת
            }
        }
    }

    // --- לוגיקת הולידציה נשארת כפי שכתבת (היא עובדת מצוין) ---
    private bool Form_Validation()
    {
        return First_Name_Validation() && Last_Name_Validation() && User_Name_Validation() &&
               Password_Validation() && ID_Validation() && Phone_Validation() &&
               Email_Validation() && Approval_Validation();
    }

    private bool First_Name_Validation() { if (firstName.Value.Length < 2) { RegistrationResult.InnerText += "First name too short. "; return false; } return true; }
    private bool Last_Name_Validation() { if (lastName.Value.Length < 2) { RegistrationResult.InnerText += "Last name too short. "; return false; } return true; }
    private bool User_Name_Validation() { if (userName.Value.Length < 3 || userName.Value.Length > 8) { RegistrationResult.InnerText += "Username must be 3-8 chars. "; return false; } return true; }

    private bool Password_Validation()
    {
        string p = pswd.Value;
        if (p.Length < 6 || p.Length > 10) { RegistrationResult.InnerText += "Password must be 6-10 chars. "; return false; }
        if (p != pswdValidate.Value) { RegistrationResult.InnerText += "Passwords do not match. "; return false; }
        return true;
    }

    private bool ID_Validation() { if (idNum.Value.Length != 9) { RegistrationResult.InnerText += "ID must be 9 digits. "; return false; } return true; }
    private bool Phone_Validation() { if (phone.Value.Length != 10 || phone.Value[0] != '0') { RegistrationResult.InnerText += "Invalid phone format. "; return false; } return true; }
    private bool Email_Validation() { if (!mail.Value.Contains("@") || !mail.Value.Contains(".")) { RegistrationResult.InnerText += "Invalid Email. "; return false; } return true; }
    private bool Approval_Validation() { if (!approval.Checked) { RegistrationResult.InnerText += "Must approve regulations. "; return false; } return true; }

    private bool Insert_Into_Database()
    {
        string dbPath = this.MapPath("App_Data/Database.mdf");
        DAL dal = new DAL(dbPath);

        // בדיקה אם המשתמש כבר קיים
        string checkQuery = "SELECT * FROM Users WHERE user_name = '" + userName.Value + "'";
        DataTable dt = dal.GetDataTable(checkQuery);

        if (dt.Rows.Count > 0)
        {
            RegistrationResult.InnerText = "Username already exists. Please choose another.";
            return false;
        }

        // שים לב: שיניתי ל-mail ו-reg_date בדיוק כמו בטבלה שלך
        string sqlQuery = "INSERT INTO Users (first_name, last_name, user_name, pswd, id_num, phone, mail, gender, reg_date, is_admin) " +
                          "VALUES (" +
                          "'" + firstName.Value + "', " +
                          "'" + lastName.Value + "', " +
                          "'" + userName.Value + "', " +
                          "'" + pswd.Value + "', " +
                          "'" + idNum.Value + "', " +
                          "'" + phone.Value + "', " +
                          "'" + mail.Value + "', " +
                          "'" + Request.Form["gender"] + "', " +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd") + "', 0);";

        dal.UpdateDB(sqlQuery);
        return true;
    }
}