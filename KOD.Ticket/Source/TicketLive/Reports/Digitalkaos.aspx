<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Digitalkaos.aspx.cs" MasterPageFile="~/Reports/Report.master"
    Inherits="Reports_Digitalkaos" %>

<%@ Register Src="~/Controls/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Kingdom of Dreams : Ticket Booking</title>
    <link href="../css/style.css" type="text/css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="../images/favicon.ico" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="Server">
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <span style="font-size: medium; font-weight: bold;">Report - Digital Kaos
            </span><b>
                <center>
                    <font color="red">
                        <asp:Label ID="lblValidation" runat="server" Text="Label" Visible="false"></asp:Label>
                    </font>
                </center>
            </b>
            <br />
            <br />
            <table width="100%">
                <tr align="left">
                <td align="right">
                        Date From
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_Date" runat="server" />
                        <cc1:CalendarExtender ID="cal1" runat="server" Format="dd MMM yyyy" TargetControlID="txt_Date" />
                    </td>
                     <td align="right">
                        Date To
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_DateTo" runat="server" />
                        <cc1:CalendarExtender ID="cal2" runat="server" Format="dd MMM yyyy" TargetControlID="txt_DateTo" />
                    </td>
                    <td colspan="3">
                        <asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" Text="  Fetch Data " OnClientClick="javascript:return validDate();" />
                        <input type="button" runat="server" disabled="disabled" id="btnPrint" value="  Print  "
                            onclick="printPreviewDiv('divPrint')" />
                        <asp:Button ID="Btn_Excel" runat="server" Enabled="false" OnClick="Btn_Excel_Click"
                            Text="  Import to Excel  " />
                    </td>
                </tr>
            </table>
            <b>
                <asp:Label ID="lblMess" runat="server"></asp:Label></b>
            <br />
            <div id="divPrint" style="font-family: Verdana;">
                <asp:GridView ID="gv_Report" FooterStyle-Font-Bold="true" Width="100%" RowStyle-Font-Size="11px"
                    HeaderStyle-Font-Size="12px" FooterStyle-BorderWidth="2px" HeaderStyle-BorderWidth="2px"
                    ShowFooter="true" AutoGenerateColumns="true" runat="server" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGo" />
            <asp:PostBackTrigger ControlID="Btn_Excel" />
        </Triggers>
    </asp:UpdatePanel>
    <script language="javascript" type="text/javascript">
        function validDate() {
            var datefrom = document.getElementById("<%=txt_Date.ClientID %>");
            if (datefrom.value == "") {
                alert("Please Select the Date From");
                datefrom.focus();
                return false;
            }
            var dateto = document.getElementById("<%=txt_DateTo.ClientID %>");
            if (dateto.value == "") {
                alert("Please Select the Date To");
                dateto.focus();
                return false;
            }
        }
    </script>
</asp:Content>
