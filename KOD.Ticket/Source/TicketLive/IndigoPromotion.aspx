<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="IndigoPromotion.aspx.cs" Inherits="IndigoPromotion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
.txtcolor
{
    border-color: #E7C54A;
    color:#E7C54A;
}
 .ModalWindow
        {
            background-color: #000000;
            border-width: 3px;
            border-style: solid;
            border-color: #E7C54A;
            padding: 3px;
            width: 550px;
            height: 500px;
        }
        .ModalWindow2
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: #E7C54A;
            padding: 3px;
        }
        .modalBackground2
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <table style="position: relative; width="100%">
        <tr>
                    <td>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/Indigo-Web.PNG" Width="250"
                            />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b><font color="red">
                            <asp:Label ID="lblMsgPromotionCode" runat="server" Text="Label"></asp:Label></font></b>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <b>Enter Your Promotion Code Below</b> &nbsp&nbsp<b>:</b>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtIndigoPromotionCode" Width="160px" runat="server" CssClass="text-small"></asp:TextBox>
                        </td>
                </tr>
                <tr>
                <td class="style1">
                        <b>Enter Your PNR Number Below</b> &nbsp&nbsp<b>:</b>                   
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtIndigoPNR" Width="160px" runat="server" CssClass="text-small"></asp:TextBox>
                         &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        <asp:LinkButton ID="TCiNdigo" runat="server" CssClass="clickhere">Terms & Conditions</asp:LinkButton>
                        </td>
                </tr>
                <tr>
                <td>
                        <asp:Button ID="btnIndigoPromotionCode" runat="server" CssClass="common-button" 
                            onclick="btnIndigoPromotionCode_Click" Text="Submit" />
                        </td>
                </tr>
        </table>
        
 <cc1:ModalPopupExtender ID="mo12" PopupControlID="dv_pop" BackgroundCssClass="modalBackground2"
        CancelControlID="btnClose" TargetControlID="TCiNdigo" runat="server">
    </cc1:ModalPopupExtender>
     <div id="dv_pop" class="ModalWindow" style="display: none; width: 550px; height: 200px;"
        runat="server">
              
        <div id="Div1" runat="server" style="overflow: auto; width: 530px; height: 160px; padding: 0px 10px 0px 10px">

           
         <font color="red"> <u><b> <center>Terms & Conditions</center></b></u></font>
           <p>1.The discount offer is applicable for all the bookings made till 31st December 2012 with date of travel on or before 31st January 2013.<br /><br />
                2.Customers can avail the offer after within 30 days of completing the travel upon production of boarding card in original.<br /><br />
                    3.These discounts cannot be clubbed with any other promotion and are subject to availability.</p>
                </div>  
       
    <hr />
        <asp:Button Text="Close" runat="server" ID="btnClose" CssClass="common-button"  />

        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

