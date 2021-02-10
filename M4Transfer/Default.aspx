﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="M4Transfer.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
        <div id="SearchDiv">
            <p>Current User: <i><asp:Label ID="LblUser" runat="server" Text=""></asp:Label></i>&nbsp;(<asp:LinkButton ID="lbLogout" runat="server" OnClick="LinkButton1_Click">Logout</asp:LinkButton>)</p>
            <asp:Label ID="SearchLabel" runat="server" Text="Search: " Font-Bold="True"></asp:Label>
            <asp:TextBox ID="SearchTxt" runat="server"></asp:TextBox>
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
            <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" />
        </div>
        <div id="SearchGVDiv">
            <asp:GridView ID="PhysicalGV" CssClass="PhysicalGV" runat="server">
            </asp:GridView>
            <br />
            <asp:GridView ID="ChemicalGV" CssClass="ChemicalGV" runat="server">
            </asp:GridView>
            <asp:Label ID="LabelSqlError" CssClass="LblSqlError" runat="server" Text=""></asp:Label><br />
            <asp:Button ID="InsertDataBtn" CssClass="InsertDataBtn" runat="server" Text="TRANSFER TEST RESULT" OnClick="InsertDataBtn_Click" />
       </div>
</asp:Content>
