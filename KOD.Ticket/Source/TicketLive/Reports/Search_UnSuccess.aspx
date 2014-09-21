<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Search_UnSuccess.aspx.cs"
    MasterPageFile="~/Reports/Report.master" Inherits="Search_UnSuccess" %>

<%@ Register Src="~/Controls/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
            width: 400px;
            height: 200px;
        }
        .modalBackground2
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
        .gd
        {
            word-wrap: break-word;
            word-break: break-word;
            white-space: normal !important;
        }
    </style>
</asp:Content>
<asp:Content ID="maincontent" runat="server" ContentPlaceHolderID="mainContent">
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <span style="font-size: medium; font-weight: bold;">Report - Temporary/Failed Transaction
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
                        <input type="button" id="btnPrint" runat="server" disabled="disabled" value="  Print  " onclick="printPreviewDiv('divPrint')" />
                        <asp:Button ID="Btn_Excel" Enabled="false" runat="server" Text="  Import to Excel  " OnClick="Btn_Excel_Click" />
                    </td>
                </tr>
            </table>
            <b>
                <asp:Label ID="lblMess" runat="server"></asp:Label></b>
            <br />
            <br />
            <div id="divPrint" style="font-family: Verdana;">
                <asp:GridView ID="gv_Report" Width="100%" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="12px"
                    FooterStyle-BorderWidth="2px" HeaderStyle-BorderWidth="2px" ShowFooter="true" 
                    AutoGenerateColumns="false" runat="server" OnRowDataBound="gv_Report_RowDataBound"
                    OnRowCommand="gv_Report_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="S.N.">
                            <ItemTemplate>
                                <%#Eval("SNO")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Booking ID">
                            <ItemTemplate>
                                <%#Eval("[Booking Id]")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Receipt No">
                            <ItemTemplate>
                                <%#Eval("[Receipt No]")%>
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
                                <span style="color: Red">Total </span>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Seats" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Right"
                            FooterStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%#Eval("Seats") %>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label Font-Bold="true" ForeColor="Green" ID="lblTotalSeats" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount (INR)" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Right"
                            FooterStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%#Eval("TotalAmount")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label Font-Bold="true" ForeColor="Green" ID="lblTotAmt" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Discounted %age" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Right"
                            FooterStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%#Eval("Discounted %age")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Discounted Amount" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Right"
                            FooterStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%#Eval("Discounted Amount")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label Font-Bold="true" ForeColor="Green" ID="lbldisamt" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Card Type">
                            <ItemTemplate>
                                <%#Eval("Card Type")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                      <asp:TemplateField HeaderText="Customer Name">
                            <ItemTemplate>
                                <%#Eval("Customer Name")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Contact No.">
                            <ItemTemplate>
                                <%#Eval("MobileNo")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Seats Info" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <%#Eval("[SeatInfo]").ToString().Replace(",", ", ")%>
                            </ItemTemplate>                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Agent Code" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <%#Eval("AgentCode") %>
                            </ItemTemplate>                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Error Description" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <%#Eval("ErrorTxt")%>
                            </ItemTemplate>                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Utility Result" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="gd">
                            <ItemTemplate>
                                <%#Eval("Utility Result")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnbSettle" runat="server" Text='Settle' CommandName='<%#Eval("SeatInfo").ToString().Replace(",",", ") %>'
                                    CommandArgument='<%#Eval("[Booking Id]") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Details" ItemStyle-Width="50px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnbDetails" runat="server" Text='Details' CommandArgument='<%#Eval("[Booking Id]")%>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            </table>
            <cc1:ModalPopupExtender ID="mo12" TargetControlID="knpop" CancelControlID="lncloe"
                BackgroundCssClass="modalBackground2" PopupControlID="dv_Show" runat="server">
            </cc1:ModalPopupExtender>
            <asp:LinkButton ID="knpop" Text="" Style="display: none" runat="server"></asp:LinkButton>
            <div id="dv_Show" class="ModalWindow" runat="server" style="display: none">
                <div style="display: block; width: 390px; text-align: right;">
                    <asp:LinkButton ID="lncloe" runat="server" Text="Close"></asp:LinkButton></div>
                <table width="100%" style="padding-top: 20px">
                    <tr>
                        <td style="width: 130px" align="right">
                            Booking ID :
                        </td>
                        <td>
                            <asp:Label ID="lblBookingID" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Current Seats :
                        </td>
                        <td>
                            <asp:Label ID="lblCurrentseats" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">
                            Remarks :
                        </td>
                        <td>
                            <asp:TextBox ID="textRemarks" Width="200px" Height="50px" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnSettle" runat="server" Text="  Settle  " OnCommand="btnSettle_Click" />
                        </td>
                    </tr>
                </table>
            </div>

            <cc1:ModalPopupExtender ID="ModalPopupDetails" TargetControlID="LinkButtonDetails" CancelControlID="lncloe"
                BackgroundCssClass="modalBackground2" PopupControlID="DivDetails" runat="server">
            </cc1:ModalPopupExtender>
            <asp:LinkButton ID="LinkButtonDetails" Text="" Style="display: none" runat="server"></asp:LinkButton>
            <div id="DivDetails" class="ModalWindow" runat="server" style="display: none">
                <div style="display: block; width: 540px; text-align: right;">
                    <asp:LinkButton ID="LinkButton2" runat="server" Text="Close"></asp:LinkButton></div>
                <b>
                    <asp:Label ID="Label1" runat="server"></asp:Label></b>
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
            <asp:AsyncPostBackTrigger ControlID="btnGo" />
            <asp:PostBackTrigger ControlID="Btn_Excel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
