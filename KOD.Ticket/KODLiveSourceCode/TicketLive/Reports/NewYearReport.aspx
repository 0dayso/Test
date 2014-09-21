<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Report.master" AutoEventWireup="true" CodeFile="NewYearReport.aspx.cs" Inherits="Reports_NewYearReport" %>
<%@ Register Src="~/Controls/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Kingdom of Dreams : Ticket Booking</title>
    <link href="../css/style.css" type="text/css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="../images/favicon.ico" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <span style="font-size: medium; font-weight: bold;">Report - WebBooking Transaction
            </span>
            <b><center><font color="red">
            <asp:Label ID="lblValidation" runat="server" Text="Label" Visible="false"></asp:Label>
            </font></center></b>
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
                        <cc1:CalendarExtender ID="cal1" runat="server" Format="dd MMM yyyy" TargetControlID="txt_BookingDate" />
                    </td>
                    <td align="right">
                        Booking Date To
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_bookingDateTo" runat="server" />
                        <cc1:CalendarExtender ID="cal2" runat="server" Format="dd MMM yyyy" TargetControlID="txt_bookingDateTo" />
                    </td>
                 <td align="right">
                        Payment Status
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPaymentStatus" runat="server">
                            <asp:ListItem Value="2">All</asp:ListItem>
                            <asp:ListItem Value="1"  Selected="True">Success</asp:ListItem>
                            <asp:ListItem Value="0">Failed</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <%--<td align="right">
                Packages
                </td>
                <td>
                :
                </td>--%>
                <td>
                <asp:DropDownList ID="ddlPackage" runat="server" Visible="false">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="1">Kids</asp:ListItem>
                            <asp:ListItem Value="2">Teens</asp:ListItem>
                            <asp:ListItem Value="3">Single</asp:ListItem>
                            <asp:ListItem Value="4">Couples</asp:ListItem>
                        </asp:DropDownList>
                </td>
                </tr>
                <%--<tr>
                    <td align="right">
                        Show Date From
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_ShowDate" runat="server" />
                        <cc1:CalendarExtender ID="cal3" runat="server" Format="dd MMM yyyy" TargetControlID="txt_ShowDate" />
                    </td>
                    <td align="right">
                        Show Date To
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_ShowDateTo" runat="server" />
                        <cc1:CalendarExtender ID="cal4" runat="server" Format="dd MMM yyyy" TargetControlID="txt_ShowDateTo" />
                    </td>--%>
                </tr>
                <tr align="right">
                
                
                    <td colspan="3">
                        <asp:Button ID="btnGo" runat="server" onclick="btnGo_Click" 
                            Text="  Fetch Data " />
                        <input type="button" runat="server" disabled="disabled" id="btnPrint" value="  Print  "
                            onclick="printPreviewDiv('divPrint')" />
                        <asp:Button ID="Btn_Excel" runat="server" Enabled="false" 
                            onclick="Btn_Excel_Click" Text="  Import to Excel  " />
                    </td>
                
                
                </tr>
            </table>
            <b>
                <asp:Label ID="lblMess" runat="server"></asp:Label></b>
            <br />
            <div id="divPrint" style="font-family: Verdana;">
                <asp:GridView ID="gv_Report" FooterStyle-Font-Bold="true" Width="100%" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="12px"
                    FooterStyle-BorderWidth="2px" HeaderStyle-BorderWidth="2px" ShowFooter="true"
                    AutoGenerateColumns="true" runat="server"  />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGo" />
            <asp:PostBackTrigger ControlID="Btn_Excel" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

