﻿<%@ Page Language="C#" MasterPageFile="~/Receipt.master" AutoEventWireup="true" CodeFile="Print-Receipt.aspx.cs" Inherits="BollyLand_Print_Receipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
    <div id="dvDetails" runat="server">
        <div id="DivPrint" style="overflow: auto; width: 100%"  style="height:600px">
            <table width="100%" cellspacing="5"colspan="5">
                <tr>
                    <td align="left" style="font-size:medium" colspan="7">
                        <b>TRANSACTION RECEIPT</b>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                <td align="center" style="font-size:medium" colspan="6">
                Congratulations !! Your Booking Details are given Below
                <br />
                <br />
                </td>
                </tr>
                <tr>
                <td style="width: 213px">
                Booking ID 
                </td>
                <td>
                :
                </td>
                <td style="width: 102px">
                <asp:Label ID="lblBookingID" runat="server" />
                </td>
                   <td style="width: 236px">
                Receipt No. 
                </td>
                <td>
                :
                </td>
                <td style="width: 597px">
                <asp:Label ID="lblReceiptNo" runat="server" />
                </td>
                </tr>
            <%--    <tr>
                <td style="width: 102px">
                Receipt No. 
                </td>
                <td>
                :
                </td>
                <td style="width: 597px">
                <asp:Label ID="lblReceiptNo" runat="server" />
                </td>
                </tr>--%>
                <tr>
                <td style="width: 213px">
                Name
                </td>
                <td>
                :
                </td>
                <td style="width: 597px">
                <asp:Label ID="lnlname" runat="server" />
                </td>
                <td style="width: 236px">
                Email ID
                </td>
                <td>
                :
                </td>
                <td style="width: 597px">
                <asp:Label ID="lblemailid" runat="server" />
                </td>
                </tr>
                <%--<tr>
                <td style="width: 201px">
                Email ID
                </td>
                <td>
                :
                </td>
                <td style="width: 597px">
                <asp:Label ID="lblemailid" runat="server" />
                </td>
                </tr>
                <tr>--%>
                <tr>
                <td style="width: 213px">
                Contact No.
                </td>
                <td>
                :
                </td>
                <td style="width: 597px">
                <asp:Label ID="lblcontactNo" runat="server" />
                </td>
               <td style="width: 213px">
                Category  
                </td>
                <td>
                :
                </td>
                <td style="width: 597px">
                <asp:Label ID="lblCouple" runat="server" Text="Gold :"></asp:Label>
                <asp:Label ID="lblCoupleQnty" runat="server" />&nbsp;
                <asp:Label ID="lblSingle" runat="server" Text="Silver :"></asp:Label>
                <asp:Label ID="lblSingleQnty" runat="server" />&nbsp;
                </td>
                </tr>
                
                <tr>
                <td style="width: 236px">
                Total Amount
                </td>
                <td>
                :
                </td>
                <td style="width: 597px">
                <asp:Label ID="lbltotalamount" runat="server" />
                </td>
                  <td valign="top" style="width: 213px">
                        Payment Mode 
                    </td>
                    <td>
                :
                </td>
                    <td valign="top" style="width: 597px">
                        <asp:Label ID="lblPayMode" runat="server" />
                    </td>
                </tr>
                
              <%--  <tr>
                <td style="width: 134px">
                Quantity
                </td>
                <td>
                :
                </td>
                <td>
                <asp:Label ID="lblquantity" runat="server" />
                </td>
                </tr>
              
                <tr>
                <td style="width: 201px">
                Total Amount
                </td>
                <td>
                :
                </td>
                <td style="width: 597px">
                <asp:Label ID="lbltotalamount" runat="server" />
                </td>
                </tr>--%>
                <tr>
                
                    
                <td style="width: 213px">
                        Event Time</td>
                    <td>:</td>
                    <td colspan="1">
                        Fri,Dec 20,2013
                    </td>
                     <td style="width: 213px">
                        Entry Time</td>
                    <td>:</td>
                    <td colspan="1">
                        7:00 Pm Onwards
                    </td>
                    </tr>
                    <tr>               
                    <td style="width: 236px">
                Date Of Booking
                </td>
                <td>
                :
                </td>
                <td><asp:Label ID="lblDOB" runat="server" /></td> 
                <td style="width: 213px">
                        Trans Details</td>
                    <td>:</td>
                    <td colspan="1">
                        <asp:Label ID="lbltrnsresponse" runat="server" />
                    </td>
            <%--    <td style="width: 597px">
                <asp:Label ID="Label5" runat="server" />
                </td>
                </tr>
                <tr>
                <td style="width: 213px">
                Date Of Booking
                </td>
                <td>
                :
                </td>
                <td style="width: 597px">
                <asp:Label ID="lblDOB" runat="server" />
                </td>--%>
              
                
                     
                </tr>
                <tr>
                <td colspan="7">
                    </br>
               *Terms & Conditions Applied*
                <p>If Transaction is Successful, Please Bring this Booking ID to the Auditorium to
                        collect your tickets and also you need to present the same credit card on which
                        the booking has been done, if tickets booked with credit card.</p>
                       <div>   <input type="button" id="Button1" class="common-button" value="Print" onclick="window.print()" /></div>
                       </td>
                       </tr>
                       </table>
            </div>
            
          <%--      </td>
             </tr>
                 <tr>
                    <td colspan="4">
                        &nbsp;<br />
                        If Transaction is Successful, Please Bring this Booking ID to the Auditorium to
                        collect your tickets and also you need to present the same credit card on which
                        the booking has been done, if tickets booked with credit card.
                    </td>
                </tr>
                <tr>
                <td colspan="3">
                <div>
            <input type="button" id="BtnPrint" class="common-button" value="Print" onclick="window.print()" />
            </div>
                </td>
                </tr>--%>
      
            </div>
             
                <div id="dvErrorDetail" runat="server" visible="false">
        <br />
        <br />
        <asp:Label ID="lblFinalMess" CssClass="error" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblDate" runat="server"></asp:Label>
    </div>
</asp:Content>


