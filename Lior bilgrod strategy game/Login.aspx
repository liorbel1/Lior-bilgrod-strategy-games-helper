<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">
    <h3>Login to the Council</h3>
    
    <label for="userName">שם משתמש:</label><br>
    <input type="text" id="userName" name="userName"><br>
    
    <label for="password">סיסמה:</label><br>
    <input type="password" id="password" name="password"><br>
    
    <input type="submit" value="Submit">
    
    <div runat="server" id="LoginResult"></div>

    <br><br>
    <hr />
    <p>New Commander? Join the ranks: </p>
    <asp:Button ID="btnGoToReg" runat="server" Text="להרשמה" PostBackUrl="Registration.aspx" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScript" Runat="Server">
</asp:Content>