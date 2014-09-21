<%@ Page Language="C#" MasterPageFile="~/Skins/Master/AccountMaster.Master" AutoEventWireup="true"
    CodeBehind="TopUp.aspx.cs" Inherits="RoyalWebApp.Account.TopUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageData" runat="server">


    <script language="javascript" type="text/javascript">
    function validAmt() {           
                if(<%Response.Write(txtAmount.ClientID);%>.value=="")
                {
                alert("Please enter amount");
                <%Response.Write(txtAmount.ClientID);%>.focus();
                return false;
                }    
                 if(IsNumeric(<%Response.Write(txtAmount.ClientID);%>.value)==false)
                {
                alert("Please enter numeric only");
                <%Response.Write(txtAmount.ClientID);%>.focus();
                return false;
                }       
       }
    </script>
  
  <script language="JavaScript" type="text/javascript">

      function IsNumeric(strString)
      //  check for valid numeric strings	
      {
          //   var strValidChars = "0123456789.-";
          var strValidChars = "0123456789";
          var strChar;
          var blnResult = true;

          if (strString.length == 0) return false;

          //  test strString consists of valid characters listed above
          for (i = 0; i < strString.length && blnResult == true; i++) {
              strChar = strString.charAt(i);
              if (strValidChars.indexOf(strChar) == -1) {
                  blnResult = false;
              }
          }
          return blnResult;
      }
    </script>
    <center>
        <table width="60%" cellpadding="3" cellspacing="3" class="divborder" >
            <tr>
                <td valign="top" align="left" class="divtext" width="150px" >
                Member Id:
                </td>
                <td valign="top" align="left" class="divtext">
                    <asp:Label ID="LblMemberId" runat="server"  Style="font-weight: 700"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" width="150px">
                Remaining Points:
                </td>
                <td valign="top" align="left" class="divtext">
                    <asp:Label ID="LblRemainingPoints"  runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" width="150px">
                Ledger Point Balance:</td>
                <td valign="top" align="left" class="divtext">
                    <asp:Label ID="LblAfter24Points" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" width="150px">
                Remaining Amount:
                </td>
                <td valign="top" align="left" class="divtext">
                    <asp:Label ID="LblRemainingAmount" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" width="150px">
                Recharge :
                </td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="inputbg" MaxLength="5"></asp:TextBox>
                &nbsp;*                    
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" width="150px">
            &nbsp;
                </td>
                <td valign="top" align="left">
                    <div class="button">
                        <a href="#">
                        <asp:LinkButton ID="BtnRecharge" runat="server"  OnClick="BtnRecharge_Click" Width="45px" Text="Submit" OnClientClick="javascript:return validAmt()" ></asp:LinkButton>
                        </a><span></span>
                    </div>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
