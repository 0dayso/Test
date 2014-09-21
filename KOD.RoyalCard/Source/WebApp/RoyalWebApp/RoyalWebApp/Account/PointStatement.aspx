<%@ Page Language="C#" MasterPageFile="~/Skins/Master/AccountMaster.Master" AutoEventWireup="true"
    CodeBehind="PointStatement.aspx.cs" Inherits="RoyalWebApp.Account.PointStatement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageHeading" runat="server">
   <div style="color:Black"> Point Statement </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageData" runat="server">
<asp:Label ID="Label1" runat="server" Text="COMING SOON......!!!" Font-Bold="true" ForeColor="Red" Font-Size="Large"></asp:Label>
    <%--<div style="float: left; width:95%;" class="divborder">
        <div class="home-content-left01">
            <div id="section" class="divscroll">--%>
                <table width="100%" border="0" cellspacing="2" cellpadding="2" 
                    style="height: 145px">
                    <tr>
                        <td valign="top" width="100%">
                            <asp:GridView ID="GridPointList" Width="630px" runat="server" AllowPaging="false" EmptyDataText="No Card Found"
                                AutoGenerateColumns="false" GridLines="Vertical" Visible="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="Type">
                <ItemTemplate>
                    <%#DataBinder.Eval(Container.DataItem,"PointType") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Issue Date">
                <ItemTemplate>
                    <%#DataBinder.Eval(Container.DataItem,"DateOfIssue") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Points">
                <ItemTemplate>
                    <%#DataBinder.Eval(Container.DataItem, "Points") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Expiration Date">
                <ItemTemplate>
                    <%#DataBinder.Eval(Container.DataItem, "ExpirationDate") %>
                </ItemTemplate>
            </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="tableBG" VerticalAlign="Top" HorizontalAlign="Center" />
                                <RowStyle CssClass="tableBdr tablefontBold" VerticalAlign="Top" HorizontalAlign="Center" />
                            </asp:GridView>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                  </td>
                    </tr>
                </table>
        <%--    </div>
        </div>
    </div>--%> 
</asp:Content>
