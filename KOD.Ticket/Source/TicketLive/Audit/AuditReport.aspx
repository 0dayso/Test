<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Audit/AuditReport.master" CodeFile="AuditReport.aspx.cs" Inherits="Audit_AuditReport" %>

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
            <span style="font-size: medium; font-weight: bold;">Report - Audit </span>
            <br />
            <center><asp:Label ID="Label1" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label></center>
            <br />
            <table width="100%">
                <tr>
                    <td align="right">
                        Show Name
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_Play" Width="160px" runat="server" CssClass="text-small"
                                 OnSelectedIndexChanged="ddl_Play_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                    </td>
                    <td align="right">
                        Show Location
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_Location" runat="server" CssClass="text-small" Width="160px" OnSelectedIndexChanged="ddl_Location_SelectedIndexChanged"
                               AutoPostBack="True">
                            </asp:DropDownList>
                            
                    </td>
                    <td><div id="dt" style="display: none">
                            <asp:DropDownList ID="ddl_Date" runat="server" Width="160px"  CssClass="text-small" 
                                 AutoPostBack="true">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList></div>
                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="show"
                                ControlToValidate="ddl_Date" Display="None" ErrorMessage="Date" InitialValue="0"
                                ForeColor="#ECA035" />--%>
                        </td>
                    <td align="right">
                        Show Date
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="dateofshow" runat="server" ontextchanged="dateofshow_TextChanged" AutoPostBack="true" />
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd MMM yyyy" 
                            TargetControlID="dateofshow">
                        </cc1:CalendarExtender>
                         
                    </td>
                    
                   <td align="right">
                        Show Time
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_ShowTimes" Width="160px" runat="server" CssClass="text-small"
                                 AutoPostBack="true">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                            
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Button ID="Btn_CreateAudit" runat="server" OnClick="btnCreateAudit_Click" Text="  Create Audit " ValidationGroup="show" OnClientClick="javascript:return validInsert();"/>
                        <input id="btnPrint" runat="server" disabled="disabled" onclick="printPreviewDiv('divPrint')" type="button" value="  Print  " />
                        <asp:Button ID="Btn_Excel" runat="server" Enabled="false" OnClick="Btn_Excel_Click" Text="  Import to Excel  " />
                    </td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="GridView_ShowDetail" runat="server" ShowHeader="false" AllowSorting="true">
            </asp:GridView>

            <b>
                <br />
                <br />
                <asp:Label ID="lblMess12" runat="server"></asp:Label></b>
            <div id="divPrint" style="font-family: Verdana;">
                <asp:GridView ID="gv_Report" Width="100%" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="12px"
                    FooterStyle-BorderWidth="2px" FooterStyle-Font-Bold="true" 
                    HeaderStyle-BorderWidth="2px" ShowFooter="True"
                    AutoGenerateColumns="False" runat="server" OnRowDataBound="gv_Report_RowDataBound"
                    OnRowCommand="gv_Report_RowCommand" 
                    onselectedindexchanged="gv_Report_SelectedIndexChanged">
                    <Columns>
                   
                        <asp:BoundField DataField="Seats" HeaderText="Seats"/>
                        <asp:BoundField DataField="Category" FooterText="Total" 
                            HeaderText="Category" />
                        <asp:BoundField HeaderText="Count Pre Interval" DataField="AuditNo1" />
                        <asp:BoundField HeaderText="Count Post Interval" DataField="AuditNo2" />
                        <asp:BoundField DataField="BookedSeats" HeaderText="Sold as per ERP" />

                        <asp:BoundField DataField="TicketnotPrinted" HeaderText="Ticket not Printed" />
                        <asp:BoundField HeaderText="Actual Sold as per ERP" />
                        <asp:BoundField HeaderText="Occupancy" />
                        <asp:TemplateField HeaderText="Foils as per ERP">
                        <ItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" ></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="TextBox2" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Up-grade"><ItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="TextBox3" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                            </ItemTemplate></asp:TemplateField>
                        <asp:BoundField HeaderText="Effective Occupants" />
                        <asp:BoundField HeaderText="Foil Diff As ERP" />
                        <asp:BoundField HeaderText="Difference as per Foil Pre-Interval" />
                        <asp:BoundField HeaderText="Difference as per Foil Post-Interval" />
                    </Columns>
                    <FooterStyle BorderWidth="2px" />
                    <HeaderStyle BorderWidth="2px" Font-Size="12px" />
                    <RowStyle Font-Size="11px" />
                </asp:GridView>
                <asp:Button ID="btnGridCalculation" runat="server" Text="Calculation" 
                    onclick="btnGridCalculation_Click" Visible="false" />
                <br />
                <br />
                <asp:GridView ID="GridView_FinalDetail" runat="server" Width="100%" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="12px"
                    FooterStyle-BorderWidth="2px" FooterStyle-Font-Bold="true" 
                    HeaderStyle-BorderWidth="2px" ShowFooter="True"
                    AutoGenerateColumns="False" >
                    <Columns>
                        <asp:BoundField DataField="Seats" HeaderText="Seats" />
                        <asp:BoundField DataField="Category" HeaderText="Category" FooterText="Total" />
                        <asp:BoundField DataField="AuditNo1" HeaderText="Count Pre Interval" />
                        <asp:BoundField DataField="AuditNo2" HeaderText="Count Post Interval" />
                        <asp:BoundField DataField="BookedSeats" HeaderText="Sold as Per ERP" />
                        <asp:BoundField DataField="TicketnotPrinted" 
                            HeaderText="Ticket not Printed" />
                        <asp:BoundField DataField="Actual Sold as per ERP" 
                            HeaderText="Actual Sold as per ERP" />
                        <asp:BoundField DataField="Occupanccy" HeaderText="Occupancy" />
                        <asp:BoundField DataField="Foils as per Gate" HeaderText="Foils As per Gate" />
                        <asp:BoundField DataField="Up-grade" HeaderText="Up- grade" />
                        <asp:BoundField DataField="Effective Occupants" 
                            HeaderText="Effective Occupants" />
                        <asp:BoundField HeaderText="Foil Diff As ERP" DataField="Foil Diff As ERP" />
                        <asp:BoundField DataField="Pre-Interval" 
                            HeaderText="Difference as per Foil Pre-Interval" />
                        <asp:BoundField DataField="Post-Interval" 
                            HeaderText="Difference as per Foil Post-Interval" />
                    </Columns>
                    <FooterStyle BorderWidth="2px" Font-Bold="True" />
                    <HeaderStyle BorderWidth="2px" Font-Size="12px" />
                    <RowStyle Font-Size="11px" />
                </asp:GridView>
            </div>
            <cc1:ModalPopupExtender ID="mo12" TargetControlID="knpop" CancelControlID="lncloe"
                BackgroundCssClass="modalBackground2" PopupControlID="dv_Show" runat="server">
            </cc1:ModalPopupExtender>
            <asp:LinkButton ID="knpop" Text="" Style="display: none" runat="server"></asp:LinkButton>
            <div id="dv_Show" class="ModalWindow" runat="server" style="display: none">
                <div style="display: block; width: 540px; text-align: right;">
                    <asp:LinkButton ID="lncloe" runat="server" Text="Close"></asp:LinkButton></div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Btn_CreateAudit" />
            <asp:PostBackTrigger ControlID="Btn_Excel" />
        </Triggers>
    </asp:UpdatePanel>
    <script language="javascript" type="text/javascript">
             function validInsert()
             {
             if (<% Response.Write(ddl_Play.ClientID);%>.value=="0")
             {
                alert("Please Select Your Play");
             <%Response.Write(ddl_Play.ClientID);%>.focus();
                return false;
             }
             if (<% Response.Write(ddl_Location.ClientID);%>.value=="0")
             {
                alert("Please Select the Location");
             <%Response.Write(ddl_Location.ClientID);%>.focus();
                return false;
             }
             if (<% Response.Write(dateofshow.ClientID);%>.value=="Select")
             {
                alert("Please Select the Date");
             <%Response.Write(dateofshow.ClientID);%>.focus();
                return false;
             }
             if (<% Response.Write(ddl_ShowTimes.ClientID);%>.value=="0")
             {
                alert("Please Select the Show Time");
             <%Response.Write(ddl_ShowTimes.ClientID);%>.focus();
                return false;
              }
              }
               </script>
</asp:Content>
