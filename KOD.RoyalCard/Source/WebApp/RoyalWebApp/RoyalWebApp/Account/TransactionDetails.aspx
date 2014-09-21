<%@ Page Title="" Language="C#" MasterPageFile="~/Skins/Master/AccountMaster.Master" AutoEventWireup="true" CodeBehind="TransactionDetails.aspx.cs" Inherits="ROYALCARD.Account.TransactionDetails" %>

<script runat="server">
    protected void GridTransactionDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridTransactionDetails.PageIndex = e.NewPageIndex;
    }
</script>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeading" runat="server">
<div style="color:Black">Transaction Details </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageData" runat="server">
    <asp:Label ID="Label1" runat="server" Font-Bold="True" 
        ForeColor="Red" Font-Size ="Large" Text="COMING SOON....!!"></asp:Label>
<br />
<asp:GridView ID="GridTransactionDetails" runat="server" AllowPaging="True" AllowSorting="True" 
        EnableSortingAndPagingCallbacks="True" HorizontalAlign="Center" 
        onpageindexchanging="GridTransactionDetails_PageIndexChanging" 
        PageSize="5" EmptyDataText="No Data Found" Height="237px" Width="554px">
    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
    <HeaderStyle HorizontalAlign="Center" 
        VerticalAlign="Middle" />
    <PagerStyle VerticalAlign="Middle" />
    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
    <Columns>
    <asp:BoundField HeaderText="Transaction Date"  />
    <asp:BoundField HeaderText="Show Name" />
    <asp:BoundField HeaderText="Show Date"  />
    <asp:BoundField HeaderText="Category" />
    <asp:BoundField HeaderText="No. of Tickets"  />
    <asp:BoundField HeaderText="Ticket Price"  />
    <asp:BoundField HeaderText="Card Point Utilized"  />
    <asp:BoundField HeaderText="Card Amount Utilized"  />
    <asp:BoundField HeaderText="Paymnet Made"  />
    <asp:BoundField HeaderText="Points Earned"  />
    </Columns>
</asp:GridView>
</asp:Content>

