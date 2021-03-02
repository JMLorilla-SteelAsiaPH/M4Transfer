<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="M4Transfer.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div id="loading" style="  height: 10em; display: flex; align-items: center; justify-content: center">
        <img src="assets/loading_gif.gif" />
        <span>Transferring Data<br />Please wait...<br />This might take a while depending on the network traffic to and from source database.</span>
    </div>

        <div id="SearchDiv">
            <p>Current User: <i><asp:Label ID="LblUser" runat="server" Text=""></asp:Label></i>&nbsp;(<asp:LinkButton ID="lbLogout" runat="server" OnClick="LinkButton1_Click">Logout</asp:LinkButton>)</p>
            <asp:Label ID="SearchLabel" runat="server" Text="Search: " Font-Bold="True"></asp:Label>
            <asp:TextBox ID="SearchTxt" ClientIDMode="Static" runat="server"></asp:TextBox>
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
            <asp:Button ID="SearchButton" ClientIDMode="Static" runat="server" Text="Search" OnClick="SearchButton_Click" />
        </div>

        <div id="PhysicalGVDiv">
            <asp:GridView ID="PhysicalGV" CssClass="PhysicalGV" ClientIDMode="Static" runat="server">
            </asp:GridView>
            <input id="TransferPhysicalBtn" type="button" value="TRANSFER PHYSICAL" />
       </div>

        <div id="ChemicalGVDiv" style="margin-top: 20px;">
            <asp:GridView ID="ChemicalGV" ClientIDMode="Static" CssClass="ChemicalGV" runat="server">
            </asp:GridView>
            <input id="TransferChemicalBtn" type="button" value="TRANSFER CHEMICAL" />
        </div>

            <asp:Label ID="LabelSqlError" CssClass="LblSqlError" runat="server" Text=""></asp:Label><br />
            <asp:Label ID="QueryResultLbl" runat="server" Text=""></asp:Label>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script>
        $(document).ready(function () {
            let searchTxt = $(`input[id$='SearchTxt']`).val().trim();

            $("#loading").hide();

            $('#TransferPhysicalBtn').prop('disabled', true);
            $('#TransferChemicalBtn').prop('disabled', true);

 
            if (searchTxt !== '') {
                checkPhysicalData();
                checkChemicalData();
            }

            $('#TransferPhysicalBtn').click(function () {

            });

            $('#TransferChemicalBtn').click(function () {
                insertChemical();
            });

            $(document).ajaxStart(function () {
                $("#loading").show();
                $('#TransferPhysicalBtn').prop('disabled', true);
                $('#TransferChemicalBtn').prop('disabled', true);
            }).ajaxStop(function () {
                $("#loading").hide();
            });

            function insertPhysical() {
                let searchTxtVal = $(`#SearchTxt`).val().trim();
                let userMillCode = localStorage.getItem('userLocation');

                $.ajax({
                    type: "POST",
                    url: 'WebService1.asmx/CheckIfDataExists',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    data: JSON.stringify({ FileNos: searchTxtVal, selectedSite: userMillCode, checkMode: 0 }),
                    success: function (data) {
                        if (data.d === 'success') {
                            alert('TEST RESULT DATA TRANSFERRED');
                            $('#TransferPhysicalBtn').prop('disabled', true);
                        }
                        else {
                            console.log(data.d);
                        }
                    },
                    error: function (data, success, error) {
                        console.log(error);
                    }
                });
            }

            function insertChemical() {
                let searchTxtVal = $(`#SearchTxt`).val().trim();
                let userMillCode = localStorage.getItem('userLocation');

                $.ajax({
                    type: "POST",
                    url: 'WebService1.asmx/InsertChemical',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    data: JSON.stringify({ FileNos: searchTxtVal, selectedSite: userMillCode, checkMode: 1 }),
                    success: function (data) {
                        if (data.d === 'success') {
                            alert('TEST RESULT DATA TRANSFERRED');
                            $('#TransferChemicalBtn').prop('disabled', true);
                        }
                        else {
                            console.log(data.d);
                            $('#TransferChemicalBtn').prop('disabled', true);
                        }
                    },
                    error: function (data, success, error) {
                        console.log(error);
                    }
                });
            }

            function checkPhysicalData() {
                let searchTxtVal = $(`#SearchTxt`).val().trim();
                let userMillCode = localStorage.getItem('userLocation');

                $.ajax({
                    type: "POST",
                    url: 'WebService1.asmx/CheckIfDataExists',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    data: JSON.stringify({ FileNos: searchTxtVal, selectedSite: userMillCode, CheckMode: 0 }),
                    success: function (data) {
                        if (data.d === false && $('#PhysicalGV tr').length > 0) {
                            $('#TransferPhysicalBtn').prop('disabled', false);
                            console.log(data.d);
                        }
                        else {
                            $('#TransferPhysicalBtn').prop('disabled', true);
                            console.log(data.d);
                        }
                    },
                    error: function (data, success, error) {
                        console.log(error);
                    }
                });
            }

            function checkChemicalData() {

                let searchTxtVal = $(`#SearchTxt`).val().trim();
                let userMillCode = localStorage.getItem('userLocation');

                $.ajax({
                    type: "POST",
                    url: 'WebService1.asmx/CheckIfDataExists',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    data: JSON.stringify({ FileNos: searchTxtVal, selectedSite: userMillCode, CheckMode: 1 }),
                    success: function (data) {
                        console.log(data.d);
                        if (data.d === false && $('#ChemicalGV tr').length > 0) {
                            $('#TransferChemicalBtn').prop('disabled', false);
                        }
                        else {
                            $('#TransferChemicalBtn').prop('disabled', true);
                        }
                    },
                    error: function (data, success, error) {
                        console.log(error);
                    }
                });
            }

        });
    </script>
</asp:Content>
