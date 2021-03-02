<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="M4Transfer.Login" %>
<asp:Content ID="LoginContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="LoginForm">
        <asp:Label ID="LblUsername" runat="server" Text="Username: "></asp:Label>&nbsp;<asp:TextBox ID="TxtUsername" runat="server"></asp:TextBox>
        <asp:Label ID="LblPassword" runat="server" Text="Password: "></asp:Label>&nbsp;<asp:TextBox ID="TxtPassword" runat="server" TextMode="Password"></asp:TextBox>&nbsp;
        <asp:DropDownList ID="DropDownList1" ClientIDMode="Static" runat="server"></asp:DropDownList>
        <br />
        <asp:Button ID="BtnLogin" runat="server" ClientIDMode="Static" Text="Login" OnClick="BtnLogin_Click" />
    </div>
    <div class="LabelSqlErrorDiv">
        <asp:Label ID="LblSqlError" CssClass="LblSqlError" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="InvalidUserCred" runat="server" Text="Invalid user credentials. Please try again." style="color: red;"></asp:Label>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script>
        $(document).ready(function () {
            $(`#BtnLogin`).click(function () {
                let locDropDownVal = $(`#DropDownList1 option:selected`).text();
                localStorage.setItem('userLocation', locDropDownVal);
            });
            
        });
    </script>
</asp:Content>
