<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Report.master" AutoEventWireup="true" CodeFile="DetailedReport.aspx.cs" Inherits="Reports_DetailedReport" %>


<%@ Register Src="~/Controls/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Kingdom of Dreams : Ticket Booking</title>
    <link href="../css/style.css" type="text/css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="../images/favicon.ico" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
<span style="font-size: medium; font-weight: bold;">Report - WebBooking Detailed Transaction
            </span>
            <br />
            <br />
            <table width="100%">
                
                <tr>
                    <td align="right">
                        Booking Date From
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_BookingDate" runat="server" />
                        <cc1:CalendarExtender ID="txt_BookingDate_CalendarExtender" runat="server" Format="dd MMM yyyy"
                            Enabled="True" TargetControlID="txt_BookingDate">
                        </cc1:CalendarExtender>
                    </td>
                    <td align="right">
                        Booking Date To
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_bookingDateTo" runat="server" />
                        <cc1:CalendarExtender ID="txt_ShowDate_CalendarExtender" runat="server" Format="dd MMM yyyy"
                            Enabled="True" TargetControlID="txt_bookingDateTo">
                        </cc1:CalendarExtender>
                    </td>
                    <td align="right">
                        Agent Type
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAgent" runat="server">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="1">Web</asp:ListItem>
                            <asp:ListItem Value="2">Agent</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                <td align="right">
                        Play
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPlay" runat="server">
                         <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="1">Jhumroo</asp:ListItem>
                            <asp:ListItem Value="2">Zangoora</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="9" align="right">
                        <asp:Button ID="btnGo" runat="server" Text="  Fetch Data  " 
                            onclick="btnGo_Click" />
                        <input disabled="disabled" type="button" runat="server" id="btnPrint" value="  Print  "
                            onclick="printPreviewDiv('divPrint')" />
                        <asp:Button Enabled="false" ID="Btn_Excel" runat="server" 
                            Text="  Import to Excel  " onclick="Btn_Excel_Click"
                         />
                    </td>
                </tr>
            </table>
            <b>
                <asp:Label ID="lblMess" runat="server"></asp:Label></b>
            <br />
            <div id="divPrint" style="font-family: Verdana;">
                <asp:GridView ID="gv_Report" HeaderStyle-HorizontalAlign="Right" RowStyle-HorizontalAlign="Right"
                    FooterStyle-HorizontalAlign="Right" Width="100%" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="12px"
                    FooterStyle-BorderWidth="2px" HeaderStyle-BorderWidth="2px" ShowFooter="true"
                    AutoGenerateColumns="true" runat="server" EmptyDataText="0">
<FooterStyle HorizontalAlign="Right" BorderWidth="2px" Font-Bold="True"></FooterStyle>

<HeaderStyle HorizontalAlign="Right" BorderWidth="2px" Font-Size="12px"></HeaderStyle>

<RowStyle HorizontalAlign="Right" Font-Size="11px"></RowStyle>
                </asp:GridView>
                <b>
                    <asp:Label ID="lblTotSeats" runat="server"></asp:Label></b>
            </div>
</asp:Content>

