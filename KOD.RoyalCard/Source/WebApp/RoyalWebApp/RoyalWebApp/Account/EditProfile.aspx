<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="RoyalWebApp.Account.EditProfile"  MasterPageFile="~/Skins/Master/AccountMaster.master"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageHeading" runat="server">
    <div style="color:Black">  Edit Profile </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="CPHPageData" runat="server">  
    <script language="javascript" type="text/javascript">
    function validUpdate() {           
                if(<%Response.Write(txtFirstName.ClientID);%>.value=="")
                {
                alert("Please enter First name");
                <%Response.Write(txtFirstName.ClientID);%>.focus();
                return false;
                }
                 if(<%Response.Write(txtLastName.ClientID);%>.value=="")
                {
                alert("Please enter Last name");
                <%Response.Write(txtLastName.ClientID);%>.focus();
                return false;
                }

                 if(<%Response.Write(txtMobileNo.ClientID);%>.value=="")
                {
                alert("Please enter mobile no");
                <%Response.Write(txtMobileNo.ClientID);%>.focus();
                return false;
                }
                if(IsNumeric(<%Response.Write(txtMobileNo.ClientID);%>.value)==false)
                {
                alert("Please enter Numeric Only");
                <%Response.Write(txtMobileNo.ClientID);%>.focus();
                return false;
                } 
                if((<%Response.Write(txtMobileNo.ClientID);%>.value).length<10)
                {
                alert("Mobile no should be minimum 10 digits");
                <%Response.Write(txtMobileNo.ClientID);%>.focus();
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
   <table cellpadding="1" cellspacing="1" class="divborder">         
           
            <tr>
                <td valign="top" align="left" class="divtext" colspan="4">
                    <strong>MEMBERSHIP DETAILS</strong></td>
                     
            </tr>          
           <tr align="center">
           <td valign="top" class="divtext" colspan="4">
               <asp:Label ID="LblMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label></td>
           </tr>
            <tr>
                <td valign="top" align="left" class="divtext">
                    Salutation*
                </td>
                <td valign="top" align="left" class="style1">
                    <b>
                        <asp:RadioButtonList ID="RdGender" runat="server" 
                        RepeatDirection="Horizontal" class="divtext" Width="209px">
                           <asp:ListItem Value="Mr." Text="Mr." Selected="True"></asp:ListItem>
                            <asp:ListItem Value="Miss" Text="Ms"></asp:ListItem>
                            <asp:ListItem Value="Dr." Text="Others"></asp:ListItem>
                        </asp:RadioButtonList>
                     </b>
                </td>
               
            </tr>          
            <tr>
                <td valign="top" align="left" class="divtext" >
                    First Name: *</td>
                <td valign="top" align="left" class="style1">
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="inputbg" 
                        Enabled="False" />
                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender"  FilterType="LowercaseLetters, UppercaseLetters"   TargetControlID="txtFirstName"
                    runat="server">
                </cc1:FilteredTextBoxExtender>

                </td>
                <td valign="top" align="left" class="divtext" >
                   Last Name: *</td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtLastName" runat="server"  CssClass="inputbg" 
                        Enabled="False" />
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"  FilterType="LowercaseLetters, UppercaseLetters"   TargetControlID="txtLastName"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" >
                    Address: 
                </td>
                <td valign="top" align="left" class="style1">
                    <asp:TextBox ID="txtAddress" runat="server"  TextMode="MultiLine" CssClass="inputbg"
                        Height="30px" />
                </td>
                <td valign="top" align="left" class="divtext" >
          City:</td>
                <td valign="top" align="left">
            <asp:TextBox ID="txtCity" runat="server" CssClass="inputbg"/>
                </td>
            </tr>
             <tr>
                <td valign="top" align="left" class="divtext" >
          Country:</td>
                <td valign="top" align="left" class="style1">
               <asp:DropDownList ID="DdlCountry" runat="server" CssClass="inputbg" Visible="false">
                 <asp:ListItem Value="India" Text="India"></asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="TextBox1" runat="server"  CssClass="inputbg" 
                   text="India"  Enabled="False" />
                </td>
                <td valign="top" align="left" class="divtext" >
                    Email ID: *
                </td>
                <td valign="top" align="left">
                     <asp:TextBox ID="TxtEmail" runat="server" CssClass="inputbg"/>
                 </td>
            </tr>         
            <tr>
                <td valign="top" align="left" class="divtext">
                    Mobile No: *
                </td>
                <td valign="top" align="left" class="style1">
                    <asp:TextBox ID="txtMobileNo" runat="server"  CssClass="inputbg" />
                </td>
                <td valign="top" align="left" class="divtext">
                    Date Of Birth:* </td>
                <td valign="top" align="left">
                      <asp:DropDownList ID="ddlday" runat="server" CssClass="inputdropdown" 
                          Enabled="False">
                        <asp:ListItem Value="01" Selected="True" Text="01"></asp:ListItem>
                        <asp:ListItem Value="02" Text="02"></asp:ListItem>
                        <asp:ListItem Value="03" Text="03"></asp:ListItem>
                        <asp:ListItem Value="04" Text="04"></asp:ListItem>
                        <asp:ListItem Value="05" Text="05"></asp:ListItem>
                        <asp:ListItem Value="06" Text="06"></asp:ListItem>
                        <asp:ListItem Value="07" Text="07"></asp:ListItem>
                        <asp:ListItem Value="08" Text="08"></asp:ListItem>
                        <asp:ListItem Value="09" Text="09"></asp:ListItem>
                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                        <asp:ListItem Value="11" Text="11"></asp:ListItem>
                        <asp:ListItem Value="12" Text="12"></asp:ListItem>
                        <asp:ListItem Value="13" Text="13"></asp:ListItem>
                        <asp:ListItem Value="14" Text="14"></asp:ListItem>
                        <asp:ListItem Value="15" Text="15"></asp:ListItem>
                        <asp:ListItem Value="16" Text="16"></asp:ListItem>
                        <asp:ListItem Value="17" Text="17"></asp:ListItem>
                        <asp:ListItem Value="18" Text="18"></asp:ListItem>
                        <asp:ListItem Value="19" Text="19"></asp:ListItem>
                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                        <asp:ListItem Value="21" Text="21"></asp:ListItem>
                        <asp:ListItem Value="22" Text="22"></asp:ListItem>
                        <asp:ListItem Value="23" Text="23"></asp:ListItem>
                        <asp:ListItem Value="24" Text="24"></asp:ListItem>
                        <asp:ListItem Value="25" Text="25"></asp:ListItem>
                        <asp:ListItem Value="26" Text="26"></asp:ListItem>
                        <asp:ListItem Value="27" Text="27"></asp:ListItem>
                        <asp:ListItem Value="28" Text="28"></asp:ListItem>
                        <asp:ListItem Value="29" Text="29"></asp:ListItem>
                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                        <asp:ListItem Value="31" Text="31"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlmonth" runat="server" CssClass="inputdropdown" 
                          Enabled="False">
                        <asp:ListItem Value="01" Selected="True" Text="Jan"></asp:ListItem>
                        <asp:ListItem Value="02"  Text="Feb"></asp:ListItem>
                         <asp:ListItem Value="03"  Text="Mar"></asp:ListItem>
                          <asp:ListItem Value="04"  Text="Apr"></asp:ListItem>
                           <asp:ListItem Value="05"  Text="May"></asp:ListItem>
                           <asp:ListItem Value="06"  Text="Jun"></asp:ListItem>
                         <asp:ListItem Value="07"  Text="Jul"></asp:ListItem>
                          <asp:ListItem Value="08"  Text="Aug"></asp:ListItem>
                           <asp:ListItem Value="09"  Text="Sep"></asp:ListItem>
                       <asp:ListItem Value="10"  Text="Oct"></asp:ListItem>
                          <asp:ListItem Value="11"  Text="Nov"></asp:ListItem>
                           <asp:ListItem Value="12"  Text="Dec"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlyear" runat="server" CssClass="inputdropdown" 
                          Enabled="False" >                   
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext">
                    Organization:</td>
                <td valign="top" align="left" class="style1">
                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="inputbg" />
                </td>
                <td valign="top" align="left" class="divtext">
                    Designation:
                </td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtDesignation" runat="server" CssClass="inputbg"/>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext">
                    Marital Status:*</td>
                <td valign="top" align="left" colspan="2">
                    <b>
                        <asp:RadioButtonList ID="RdMartialStatus" runat="server" 
                        RepeatDirection="Horizontal" class="divtext"                        
                        AutoPostBack="True" 
                        onselectedindexchanged="RdMartialStatus_SelectedIndexChanged">
                            <asp:ListItem Value="Single" Text="Single"></asp:ListItem>
                            <asp:ListItem Value="Married" Text="Married"></asp:ListItem>
                            <asp:ListItem Value="Prefer Not to Say" Text="Prefer Not to Say"></asp:ListItem>
                        </asp:RadioButtonList>
                    </b>
                </td>
                <td valign="top" align="left" id="trAnn" runat="server" visible="false">
                         <asp:DropDownList ID="DdlDayAnniversary" runat="server" 
                             CssClass="inputdropdown" Enabled="False">
                        <asp:ListItem Value="01" Selected="True" Text="01"></asp:ListItem>
                        <asp:ListItem Value="02" Text="02"></asp:ListItem>
                        <asp:ListItem Value="03" Text="03"></asp:ListItem>
                        <asp:ListItem Value="04" Text="04"></asp:ListItem>
                        <asp:ListItem Value="05" Text="05"></asp:ListItem>
                        <asp:ListItem Value="06" Text="06"></asp:ListItem>
                        <asp:ListItem Value="07" Text="07"></asp:ListItem>
                        <asp:ListItem Value="08" Text="08"></asp:ListItem>
                        <asp:ListItem Value="09" Text="09"></asp:ListItem>
                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                        <asp:ListItem Value="11" Text="11"></asp:ListItem>
                        <asp:ListItem Value="12" Text="12"></asp:ListItem>
                        <asp:ListItem Value="13" Text="13"></asp:ListItem>
                        <asp:ListItem Value="14" Text="14"></asp:ListItem>
                        <asp:ListItem Value="15" Text="15"></asp:ListItem>
                        <asp:ListItem Value="16" Text="16"></asp:ListItem>
                        <asp:ListItem Value="17" Text="17"></asp:ListItem>
                        <asp:ListItem Value="18" Text="18"></asp:ListItem>
                        <asp:ListItem Value="19" Text="19"></asp:ListItem>
                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                        <asp:ListItem Value="21" Text="21"></asp:ListItem>
                        <asp:ListItem Value="22" Text="22"></asp:ListItem>
                        <asp:ListItem Value="23" Text="23"></asp:ListItem>
                        <asp:ListItem Value="24" Text="24"></asp:ListItem>
                        <asp:ListItem Value="25" Text="25"></asp:ListItem>
                        <asp:ListItem Value="26" Text="26"></asp:ListItem>
                        <asp:ListItem Value="27" Text="27"></asp:ListItem>
                        <asp:ListItem Value="28" Text="28"></asp:ListItem>
                        <asp:ListItem Value="29" Text="29"></asp:ListItem>
                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                        <asp:ListItem Value="31" Text="31"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="DdlMonthAnniversary" runat="server" CssClass="inputdropdown" 
                             Enabled="False">
                        <asp:ListItem Value="01" Selected="True" Text="Jan"></asp:ListItem>
                        <asp:ListItem Value="02"  Text="Feb"></asp:ListItem>
                         <asp:ListItem Value="03"  Text="Mar"></asp:ListItem>
                          <asp:ListItem Value="04"  Text="Apr"></asp:ListItem>
                           <asp:ListItem Value="05"  Text="May"></asp:ListItem>
                           <asp:ListItem Value="06"  Text="Jun"></asp:ListItem>
                         <asp:ListItem Value="07"  Text="Jul"></asp:ListItem>
                          <asp:ListItem Value="08"  Text="Aug"></asp:ListItem>
                           <asp:ListItem Value="09"  Text="Sep"></asp:ListItem>
                       <asp:ListItem Value="10"  Text="Oct"></asp:ListItem>
                          <asp:ListItem Value="11"  Text="Nov"></asp:ListItem>
                           <asp:ListItem Value="12"  Text="Dec"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="DdlYearAnniversary" runat="server" CssClass="inputdropdown" 
                             Enabled="False">                        
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext">
                    &nbsp;</td>
                <td valign="top" align="right" class="style1">
                    <div class="button" style="float:right;">
                <a href="#">
                <asp:LinkButton ID="BtnSubmit" runat="server"  OnClick="BtnSubmit_Click" OnClientClick="javascript:return validUpdate();" Width="45px" Text="Submit" ></asp:LinkButton>
                </a><span></span>
                </div>
</td>
                <td valign="top" align="left">
              
                 <div class="button"  style="float:left;">
                <a href="UserCard.aspx">
                Home
                </a><span></span>
                </div>
                </td>
                <td valign="top" align="left">
                    &nbsp;</td>
            </tr>
            </table>
 </center> 
</asp:Content>
<asp:Content ID="Content4" runat="server" contentplaceholderid="head">
    <style type="text/css">
    .style1
    {
        width: 199px;
    }
</style>
</asp:Content>

