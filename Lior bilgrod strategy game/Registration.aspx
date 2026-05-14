<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">
    <label for="firstName">שם פרטי:</label><br>
    <input runat="server" type="text" id="firstName" name="firstName"/><br/>
    
    <label for="lastName">שם משפחה:</label><br>
    <input runat="server" type="text" id ="lastName" name="lastName"/><br/>
    
    <label for="userName">שם משתמש:</label><br>
    <input runat="server" type="text" id="userName" name="userName"/><br/>
    
    <label for="pswd">סיסמא:</label><br>
    <input runat="server" type="Password" id="pswd" name="pswd"/><br/>

    <label for="pswdValidate">אימות סיסמא:</label><br>
    <input runat="server" type="Password" id="pswdValidate" name="pswdValidate"/><br/> 
    
    <label for="idNum">ת"ז:</label><br>
    <input runat="server" type="text" id ="idNum" name="idNum"/><br/>

    <label for="phone">טלפון:</label><br>
    <input runat="server" type="text" id="phone" name="phone"/><br/>

    <label for="mail">דוא"ל:</label><br>
    <input runat="server" type="text" id="mail" name="mail"/><br/>
    
    <label for="gender">מין:</label><br>
    <input type="radio" id="genderFemale" name="gender" value="female" checked/>נקבה<br/>
    <input type="radio" id="genderMale" name="gender" value="male"/>זכר<br/>
    <input type="radio" id="genderOther" name="gender" value="other"/>אחר<br/>
    
    <label for="approval">אני מאשר\ת את תקנון האתר:</label><br>
    <input runat="server" type="checkbox" id = "approval" name = "approval" value="approve"/>מאשר\ת<br/>

    <input type="submit" value="הירשם עכשיו">
    
    <div runat="server" id="RegistrationResult"></div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScript" Runat="Server">
</asp:Content>