<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="Voucher.aspx.cs" Inherits="Voucher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="Server">
    <table width="100%">
        <tr>
            <td>
                Voucher No(s).
            </td>
        </tr>
        <tr>
            <td>
                <asp:Repeater ID="rep_Vouchers" runat="server">
                    <HeaderTemplate>
                    <table><tr><td style="width:120px;padding-left:35px">Sr No</td><td>Code</td></tr></table> 
                        <ol>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <asp:TextBox Width="100px" ID='txtSerials' runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="*" runat="server"
                                Display="Static" ValidationGroup="voucher" ControlToValidate="txtvasls"></asp:RequiredFieldValidator>
                            <asp:TextBox Width="100px" ID='txtvasls' runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rreq" Text="*" runat="server" Display="Static" ValidationGroup="voucher"
                                ControlToValidate="txtvasls"></asp:RequiredFieldValidator>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ol>
                    </FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMess" runat="server" CssClass="error"></asp:Label>
                <asp:Button ID="btnBackHome" CssClass="common-button" runat="server" Text="Back To Home"
                    OnClick="btnBackHome_Click" />
                <asp:Button ID="btnVarify" Text="Confirm" CssClass="common-button" runat="server"
                    ValidationGroup="voucher" OnClick="btnVarify_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
