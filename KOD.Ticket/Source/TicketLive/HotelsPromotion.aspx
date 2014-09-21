<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="HotelsPromotion.aspx.cs" Inherits="HotelsPromotion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript" type="text/javascript">
    function validation() {
        
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="position: relative;" width="100%">
            
                <tr>
                    <td align="center">
                        <b style="font-size: medium">Corporate Promotion</b>
                    </td>
                </tr>
               
            </table>
            <br /><br />
            <table style="position: relative;" width="100%">
            <asp:UpdateProgress ID="uppero" runat="server" DynamicLayout="true">
                        <ProgressTemplate>
                            <div style="height: 300px; background-color: #C79EA7; width: 280px; position: absolute;
                                top: 265px; left: 535px; border-left: 4px solid #A67782; border-right: 4px solid #A67782;
                                color: #FFC419; filter: alpha(opacity=80); opacity: 0.8; text-align: center;
                                z-index: 100">
                                <div style="font-family: Verdana; font-size: 12px; color: Black; font-weight: bold;
                                    margin-top: 95px">
                                    Loading Please Wait...
                                </div>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
               
                
                
                 <tr>
                <td></td>
                
                </tr>
                  <tr>
                <td><br /><br />
                </td>
                
                </tr>
                 <tr>
                    <td>
                        <b><font color="red">
                            <asp:Label ID="lblMsgPromotionCode" runat="server" Text="Label"></asp:Label></font><b>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <b>Enter Your Promotion Code Below</b> &nbsp&nbsp<b>:</b>
                    </td>
                </tr>
               
                <tr>
                    <td>
                        <asp:TextBox ID="txtHtlPromotionCode" Width="160px" runat="server" CssClass="text-small"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="re3" ControlToValidate="txtHtlPromotionCode" ValidationGroup="show"
                    ErrorMessage="Promotion Code" Display="None" ForeColor="#ECA035" runat="server" />
                       
                        <br>
                        <br />
                        <asp:Button ID="btnHtlPromotionCode" runat="server" CssClass="common-button" ValidationGroup="show"
                            onclick="btnHtlPromotionCode_Click" Text="Submit"  />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
        ShowSummary="false" HeaderText="Below fields are missing.." ValidationGroup="show" />
                        </br>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

