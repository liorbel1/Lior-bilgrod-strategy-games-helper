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
                RegistrationResult.InnerText =
                        firstName.Value + ", Registration successful, go to the login page.";
            }
        }
    }

    private bool Form_Validation()
    {
        return
            First_Name_Validation() &&
            Last_Name_Validation() &&
            User_Name_Validation() &&
            Password_Validation() &&
            ID_Validation() &&
            Phone_Validation() &&
            Email_Validation() &&
            Approval_Validation();
    }

    private bool First_Name_Validation()
    {
        string fname = firstName.Value;

        if (fname.Length < 2)
        {
            RegistrationResult.InnerText += "private name must contain a minimum of 2 letters. ";
            return false;
        }

        return true;
    }

    private bool Last_Name_Validation()
    {
        string lname = lastName.Value;

        if (lname.Length < 2)
        {
            RegistrationResult.InnerText += "family name must contain a minimum of 2 letters. ";
            return false;
        }

        return true;
    }

    private bool User_Name_Validation()
    {
     
        string uname = userName.Value;

        if (uname.Length < 3|| uname.Length > 8)
        {
            RegistrationResult.InnerText += "username must contain a minimum of 3 letters and a maximum of 8. ";
            return false;
        }

        return true;
    }

    private bool Password_Validation()
    {
        string password = pswd.Value;
        string pswdV = pswdValidate.Value;

        if (password.Length < 6 || password.Length > 10)
        {
            RegistrationResult.InnerText += "password must contain 6-10 letters. ";
            return false;
        }

        
        bool letterExist = false;
        bool numberExist = false;
        for (int i = 0; i < password.Length; i++)
        {
           
            if (password[i] >= 'a' && password[i] <= 'z' || password[i] >= 'A' && password[i] <= 'Z')
                letterExist = true;
            
            else if (password[i] >= '0' && password[i] <= '9')
                numberExist = true;
        }
        if (!letterExist || !numberExist)
        {
            RegistrationResult.InnerText += "password must contain words and numbers. ";
            return false;
        }

        
        if (password != pswdV)
        {
            RegistrationResult.InnerText += "The password and password confirmation are not the same. ";
            return false;
        }

        return true;
    }

    private bool ID_Validation()
    {

        string Id = idNum.Value;
      

        if (Id.Length != 9)
        {
            RegistrationResult.InnerText += "ID must contain 9 numbers. ";
            return false;
        }

        bool numberExist = false;
        for (int i = 0; i < Id.Length; i++)
        {
            if (Id[i] >= '0' && Id[i] <= '9')
            {
                numberExist = true;
            }
            if (!(Id[i] >= '0' && Id[i] <= '9'))
            {
                RegistrationResult.InnerText += "ID must be ONLY numbers. ";
                return false;
            }
        }
        if (!numberExist)
        {
            RegistrationResult.InnerText += "ID must be numbers. ";
            return false;
        }

        return true;
        
    }

    private bool Phone_Validation()
    {
        string phonenum = phone.Value;

        if (phonenum.Length != 10)
        {
            RegistrationResult.InnerText += "phone numbers must contain 10 numbers. ";
            return false;
        }
       
        if(phonenum[0] != '0')
        {
            RegistrationResult.InnerText += "phone number first number must be 0. ";
            return false;
        }
     
        for (int i = 0; i < phonenum.Length; i++)
        {
           
            if (!(phonenum[i] >= '0' && phonenum[i] <= '9'))
            {
                RegistrationResult.InnerText += "phone number must be ONLY numbers. ";
                return false;
            }
        }
        return true;
    }

    private bool Email_Validation()
    {
        

        string email = mail.Value;

        bool shtrodelexist = false;
        bool pointexist = false;
        int countdot = 0;
        int countshtrodel = 0;
        for(int i = 0; i < email.Length; i++)
        {
            if (email[i] == '@')
            {
                shtrodelexist = true;
                countshtrodel = i;
            }
            else if (email[i] == '.')
            {
                pointexist = true;
                countdot = i;
            }
        }
        if (!shtrodelexist || !pointexist)
        {
            RegistrationResult.InnerText += "email must contain shtrodel and a dot. ";
            return false;
        }
        if (countdot < countshtrodel)
        {
            RegistrationResult.InnerText += "shtrodel must be placed before the dot ";
            return false;
        }

        return true;
    }

    private bool Approval_Validation()
    {
        if (!approval.Checked)
        {
            RegistrationResult.InnerText += "The site regulations must be approved. ";
            return false;
        }

        return true;
    }

   
        private bool Insert_Into_Database()
    {
        string dbPath = this.MapPath("App_Data/Database.mdf");
        DAL dal = new DAL(dbPath);

        string sqlQuery = "SELECT * FROM Users WHERE user_name = '" + userName.Value + "'";
        DataTable dt = dal.GetDataTable(sqlQuery);

        if (dt.Rows.Count > 0)
        {
            RegistrationResult.InnerText = "שם משתמש קיים במערכת. אנא בחר.י שם אחר.";
            return false;
        }

        sqlQuery = "INSERT INTO Users VALUES (" +
        "'" + firstName.Value + "', " +
        "'" + lastName.Value + "', " +
        "'" + userName.Value + "', " +
        "'" + pswd.Value + "', " +
        "'" + idNum.Value + "'," +
        "'" + phone.Value + "'," +
        "'" + mail.Value + "'," +
        "'" + Request.Form["gender"] + "'," +
        "'" + DateTime.Now.ToString("yyyy-MM-dd") + "', 0);";

        dal.UpdateDB(sqlQuery);

        return true;
    }

}