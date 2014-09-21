<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs"
    Inherits="Report" MasterPageFile="~/Reports/Report.master" %>

<%@ Register Src="~/Controls/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Kingdom of Dreams : Ticket Booking</title>
    <link href="../css/style.css" type="text/css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="../images/favicon.ico" />
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="mainContent" runat="server">
    <%--<asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>--%>
            <span style="font-size: medium; font-weight: bold;">Report - WebBooking Transaction
            </span>
            <br />
            <br />
            <table width="100%">
                <tr>
                    <td align="right">
                        Booking ID
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_BookingID" runat="server" />
                    </td>
                    <td align="right">
                        Receipt No
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_ReceiptNo" runat="server" />
                    </td>
                    <td align="right">
                        Name
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_Name" runat="server" />
                    </td>
                </tr>
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
                        Show Date From
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_ShowDate" runat="server" />
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd MMM yyyy"
                            TargetControlID="txt_ShowDate">
                        </cc1:CalendarExtender>
                    </td>
                    <td align="right">
                        Show Date To
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_ShowDateTo" runat="server" />
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd MMM yyyy"
                            Enabled="True" TargetControlID="txt_ShowDateTo">
                        </cc1:CalendarExtender>
                    </td>
                    <td colspan="3">
                        <asp:Button ID="btnGo" runat="server" Text="  Fetch Data  " OnClick="btnGo_Click" />
                        <input disabled="disabled" type="button" runat="server" id="btnPrint" value="  Print  "
                            onclick="printPreviewDiv('divPrint')" />
                        <asp:Button Enabled="false" ID="Btn_Excel" runat="server" Text="  Import to Excel  "
                            OnClick="Btn_Excel_Click" />
                    </td>
                </tr>
            </table>
            <b>
                <asp:Label ID="lblMess" runat="server"></asp:Label></b>
            <br />
            <div id="divPrint" style="font-family: Verdana;">
                <asp:GridView ID="gv_Report" HeaderStyle-HorizontalAlign="Right" RowStyle-HorizontalAlign="Right"
                    FooterStyle-HorizontalAlign="Right" Width="50%" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="12px"
                    FooterStyle-BorderWidth="2px" HeaderStyle-BorderWidth="2px" ShowFooter="true"
                    AutoGenerateColumns="true" runat="server">
                </asp:GridView>
                <b>
                    <asp:Label ID="lblTotSeats" runat="server"></asp:Label></b>
            </div>
      <%--  </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGo" />
            <asp:PostBackTrigger ControlID="Btn_Excel" />
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>
