<%@ Page Language="C#" MasterPageFile="~/Audit/Audit.master" AutoEventWireup="true" CodeFile="Audit.aspx.cs" Inherits="Audit_Audit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
    <asp:Label ID="Label1" runat="server" Visible="False" ForeColor="Red"></asp:Label>
<asp:GridView ID="GridView_ShowDetail" runat="server" HeaderStyle-HorizontalAlign="Right" RowStyle-HorizontalAlign="Right"
                    FooterStyle-HorizontalAlign="Right" Width="100%" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="12px"
                    FooterStyle-BorderWidth="2px" HeaderStyle-BorderWidth="2px"
                    AutoGenerateColumns="False" EmptyDataText="0" CellPadding="2" 
        CellSpacing="2" onrowcommand="GridViewShowDetail_RowCommand">
    <Columns>
        <asp:BoundField DataField="ShowName" HeaderText="Show Name" />
        <asp:BoundField DataField="Location" HeaderText="Location" />
        <asp:BoundField DataField="Show Date" HeaderText="Show Date" />
        <asp:BoundField DataField="Show Time" HeaderText="Show Time" />
        <asp:BoundField DataField="Show Time Code" HeaderText="show time1" 
            Visible="False" />
        <asp:BoundField DataField="locationcode" HeaderText="Location1" 
            Visible="False" />
        <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <asp:LinkButton ID="lbNewAudit" runat="server" CausesValidation="false" 
                    CommandName="NewAudit" Text="New Audit" CommandArgument='<%#String.Format("{0},{1},{2},{3}",(Eval("locationcode").ToString()),(Eval("Show Time Code").ToString()),(Eval("Show Time").ToString()),(Eval("ShowName").ToString()))%>'></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <EmptyDataTemplate>
        <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
    </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Right"></HeaderStyle>

<RowStyle HorizontalAlign="Right"></RowStyle>
</asp:GridView>
</asp:Content>
