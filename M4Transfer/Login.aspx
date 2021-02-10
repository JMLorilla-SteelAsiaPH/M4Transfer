<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="M4Transfer.Login" %>
<asp:Content ID="LoginContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="LoginForm">
        <asp:Label ID="LblUsername" runat="server" Text="Username: "></asp:Label>&nbsp;<asp:TextBox ID="TxtUsername" runat="server"></asp:TextBox>
        <asp:Label ID="LblPassword" runat="server" Text="Password: "></asp:Label>&nbsp;<asp:TextBox ID="TxtPassword" runat="server" TextMode="Password"></asp:TextBox>&nbsp;
        <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
        <br />
        <asp:Button ID="BtnLogin" runat="server" Text="Login" OnClick="BtnLogin_Click" />
    </div>
    <div class="LabelSqlErrorDiv">
        <asp:Label ID="LblSqlError" CssClass="LblSqlError" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="InvalidUserCred" runat="server" Text="Invalid user credentials. Please try again." style="color: red;"></asp:Label>
    </div>
</asp:Content>
