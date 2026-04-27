using System;
using System.Collections.Generic;
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

        // קוד שמוודא שהסיסמה בין 6 ל-10 תווים בלבד
        if (password.Length < 6 || password.Length > 10)
        {
            RegistrationResult.InnerText += "password must contain 6-10 letters. ";
            return false;
        }

        // קוד שמוודא שהסיסמה מכילה אותיות ומספרים
        bool letterExist = false;
        bool numberExist = false;
        for (int i = 0; i < password.Length; i++)
        {
            // בדיקת קיום אותיות
            if (password[i] >= 'a' && password[i] <= 'z' || password[i] >= 'A' && password[i] <= 'Z')
                letterExist = true;
            // בדיקת קיום מספרים
            else if (password[i] >= '0' && password[i] <= '9')
                numberExist = true;
        }
        if (!letterExist || !numberExist)
        {
            RegistrationResult.InnerText += "password must contain words and numbers. ";
            return false;
        }

        // קוד לוידוא סיסמה ווידוא סיסמה זהים
        if (password != pswdV)
        {
            RegistrationResult.InnerText += "הסיסמה ווידוא הסיסמה אינם זהים. ";
            return false;
        }

        return true;
    }

    private bool ID_Validation()
    {
        // === משימה לתלמיד: וידוא תעודת זהות ===
        // 1. ודא שאורך תעודת הזהות הוא בדיוק 9 תווים
        // 2. ודא שכל התווים במחרוזת הם ספרות בלבד
        // כדי לעבור על כל התווים, תוכל להיעזר בלולאה שמופיעה בפעולה:
        // Password_Validation
        // 3. אם יש שגיאה, אל תשכח להוסיף הודעה אל:
        // RegistrationResult.InnerText
        // ולהחזיר:
        // return false;

        return true;
    }

    private bool Phone_Validation()
    {
        // === משימה לתלמיד: וידוא מספר טלפון ===
        // 1. ודא שאורך מספר הטלפון הוא בדיוק 10 תווים
        // 2. ודא שהתו הראשון במספר הוא הספרה אפס
        // 3. ודא שכל התווים במחרוזת הם ספרות בלבד
        // 4. במקרה שאחד מהתנאים לא מתקיים, עדכן את:
        // RegistrationResult.InnerText
        // וסיים את הפעולה עם:
        // return false;

        return true;
    }

    private bool Email_Validation()
    {
        // === משימה לתלמיד: וידוא כתובת אימייל בסיסית ===
        // ודא שהתנאים הבאים מתקיימים:
        // 1. המחרוזת מכילה את התו שטרודל
        // 2. המחרוזת מכילה את התו נקודה
        // 3. הנקודה מופיעה אחרי השטרודל
        // רמז: כדי למצוא את המיקום של התווים, חפש ברשת איך להשתמש בפעולה:
        // IndexOf
        // במקרה שאחד התנאים לא מתקיים, הוסף הודעת שגיאה מתאימה והחזר:
        // return false;

        return true;
    }

    private bool Approval_Validation()
    {
        if (!approval.Checked)
        {
            RegistrationResult.InnerText += "יש לאשר את תקנון האתר. ";
            return false;
        }

        return true;
    }

    private bool Insert_Into_Database()
    {
        return true;
    }



}