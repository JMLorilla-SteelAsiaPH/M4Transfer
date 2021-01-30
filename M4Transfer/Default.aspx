<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="M4Transfer.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>M4 Site Transfer Order</title>
    <link rel="stylesheet" type="text/css" href="style/m4transfer_style.css" />
</head>
<body>
    <div id="SiteHeader">
        <img src="assets/sa_logo.png" id="SiteLogo" />
        <h2>M4 Site Order Transfer</h2>
    </div>

    <form id="form1" runat="server">
        <div id="SearchDiv">
            <asp:Label ID="SearchLabel" runat="server" Text="Search: " Font-Bold="True"></asp:Label>
            <asp:TextBox ID="SearchTxt" runat="server"></asp:TextBox>
            <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem Value="M1" Text="M1"></asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" />
        </div>
        <div id="SearchGVDiv">
            <asp:GridView ID="PhysicalGV" CssClass="PhysicalGV" runat="server">
            </asp:GridView>
            <br />
            <asp:GridView ID="MechanicalGV" runat="server"></asp:GridView>
            <br />
            <asp:Label ID="LabelSqlError" CssClass="LblSqlError" runat="server" Text=""></asp:Label>
            <br />
            <asp:Button ID="InsertDataBtn" CssClass="InsertDataBtn" runat="server" Text="TRANSFER TEST RESULT" OnClick="InsertDataBtn_Click" />
       </div>
    </form>
</body>
</html>
