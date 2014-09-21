<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Report.master" AutoEventWireup="true"
    CodeFile="Log-Report.aspx.cs" Inherits="Reports_Log_Report" %>

<%@ Register Src="~/Controls/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Kingdom of Dreams : Ticket Booking</title>
    <link href="../css/style.css" type="text/css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="../images/favicon.ico" />
    <style type="text/css">
        .ModalWindow
        {
            background-color: #ffffff;
            border-width: 3px;
            border-style: solid;
            border-color: Gray;
            padding: 3px;
            width: 550px;
        }
        .modalBackground2
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="Server">
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <span style="font-size: medium; font-weight: bold;">Report - Transaction Logs </span>
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
                   <td align="right">
                        Status
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="dlStatus" runat="server">
                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                            <asp:ListItem Value="1" Text="All"></asp:ListItem>
                            <asp:ListItem Value="2" Text="All -- Before Payment Gateway"></asp:ListItem>
                            <asp:ListItem Value="3" Text="All -- After Payment Gateway"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Payment Unsuccessful"></asp:ListItem>
                            <asp:ListItem Value="5" Text="Payment Successful and Seats Booked"></asp:ListItem>
                            <asp:ListItem Value="6" Text="Payment Successful but Seats not Booked"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>                        
                    </td>
                    <td>                        
                    </td>
                    <td>                        
                    </td>
                    <td>                        
                    </td>
                    <td>                        
                    </td>
                    <td>                        
                    </td>
                    <td colspan="3">
                        <asp:Button ID="Button1" runat="server" OnClick="btnGo_Click" Text="  Fetch Data  " />
                        <input id="btnPrint" runat="server" disabled="disabled" onclick="printPreviewDiv('divPrint')" type="button" value="  Print  " />
                        <asp:Button ID="Btn_Excel" runat="server" Enabled="false" OnClick="Btn_Excel_Click" Text="  Import to Excel  " />
                    </td>
                </tr>
            </table>
            <b>
                <br />
                <br />
                <asp:Label ID="lblMess12" runat="server"></asp:Label></b>
            <div id="divPrint" style="font-family: Verdana;">
                <asp:GridView ID="gv_Report" Width="100%" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="12px"
                    FooterStyle-BorderWidth="2px" FooterStyle-Font-Bold="true" HeaderStyle-BorderWidth="2px" ShowFooter="true"
                    AutoGenerateColumns="false" runat="server" OnRowDataBound="gv_Report_RowDataBound"
                    OnRowCommand="gv_Report_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Booking ID">
                            <ItemTemplate>
                                <%#Eval("BookingID")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Receipt No">
                            <ItemTemplate>
                                <%#Eval("[ReceiptNo]") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Booking Date" ItemStyle-Width="133px">
                            <ItemTemplate>
                                <%#Eval("[Booking Date]")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Show Date" ItemStyle-Width="133px">
                            <ItemTemplate>
                                <%#Eval("[Show Date]")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Play" ItemStyle-Width="80px">
                            <ItemTemplate>
                                <%#Eval("[Play]")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category">
                            <ItemTemplate>
                                <%#Eval("Category") %>
                            </ItemTemplate>
                            <FooterTemplate>
                                Total
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Seats" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Right"
                            FooterStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%#Eval("TotalSeats") %>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label   ID="lblTotalSeats" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount (INR)" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Right"
                            FooterStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%#Eval("TotalAmount") %>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label  ID="lblTotAmt" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Card Type">
                            <ItemTemplate>
                                <%#Eval("Card Type") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer Name">
                            <ItemTemplate>
                                <%#Eval("Name") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mobile No">
                            <ItemTemplate>
                                <%#Eval("MobileNo") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email Address">
                            <ItemTemplate>
                                <%#Eval("EmailID") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Seats Info" >
                            <ItemTemplate>
                                <%#Eval("SeatInfo").ToString().Replace(",",", ") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Agent Code" ItemStyle-Width="40px">
                            <ItemTemplate>
                                <%#Eval("AgentCode") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" ItemStyle-Width="40px">
                            <ItemTemplate>
                                <%#Eval("sta") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Details" ItemStyle-Width="50px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnbSettle" runat="server" Text='Details' CommandArgument='<%#Eval("BookingID") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <cc1:ModalPopupExtender ID="mo12" TargetControlID="knpop" CancelControlID="lncloe"
                BackgroundCssClass="modalBackground2" PopupControlID="dv_Show" runat="server">
            </cc1:ModalPopupExtender>
            <asp:LinkButton ID="knpop" Text="" Style="display: none" runat="server"></asp:LinkButton>
            <div id="dv_Show" class="ModalWindow" runat="server" style="display: none">
                <div style="display: block; width: 540px; text-align: right;">
                    <asp:LinkButton ID="lncloe" runat="server" Text="Close"></asp:LinkButton></div>
                <b>
                    <asp:Label ID="lblMess" runat="server"></asp:Label></b>
                <ol>
                    <asp:Repeater ID="rep_Details" runat="server">
                        <ItemTemplate>
                            <li>
                                <%#Eval("Description") %></li></ItemTemplate>
                    </asp:Repeater>
                </ol>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Button1" />
            <asp:PostBackTrigger ControlID="Btn_Excel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
