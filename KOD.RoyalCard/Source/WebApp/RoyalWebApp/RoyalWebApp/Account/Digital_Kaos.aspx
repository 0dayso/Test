<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Digital_Kaos.aspx.cs"  MasterPageFile="~/Skins/Master/Digital.Master" Inherits="RoyalWebApp.Account.Digital_Kaos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageHeading" runat="server">

    <%--Digital_Kaos--%>
  

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHPageData" runat="server">
    
        <table width="87%" cellpadding="2" cellspacing="2" class="divborder">
            <tr>
                <td valign="top" align="left" class="divtext">
                    Salutation*
                </td>
                <td valign="top" align="left">
                    <b>
                    <asp:RadioButtonList ID="RdGender" runat="server" RepeatDirection="Horizontal" class="divtext">
                        <asp:ListItem Value="Mr." Text="Mr." Selected="True"></asp:ListItem>
                        <asp:ListItem Value="Miss" Text="Ms"></asp:ListItem>
                        <asp:ListItem Value="Dr." Text="Others"></asp:ListItem>
                    </asp:RadioButtonList>
                    </b>
                </td>
                <td valign="top" align="left" class="divtext" width="150px" colspan="2">
                  <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>   </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" >
                    First Name: *</td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="inputbg" />
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender"  FilterType="LowercaseLetters, UppercaseLetters"   TargetControlID="txtFirstName"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
                   
         

                </td>
                <td valign="top" align="left" class="divtext" >
                   Last Name: *</td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtLastName" runat="server"  CssClass="inputbg" />
                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"  FilterType="LowercaseLetters, UppercaseLetters"   TargetControlID="txtLastName"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" >
                    Address: 
                    *</td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtAddress" runat="server"  TextMode="MultiLine" CssClass="inputbg"
                        Height="30px" />
                </td>
                <td valign="top" align="left" class="divtext" >
          City:*</td>
                <td valign="top" align="left">
<asp:TextBox ID="txtCity" runat="server" CssClass="inputbg"/>
<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2"  FilterType="LowercaseLetters, UppercaseLetters"   TargetControlID="txtCity"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" >
          Country:</td>
                <td valign="top" align="left">
             <asp:TextBox ID="txtCountry" runat="server" CssClass="inputbg" Text="India" />
                </td>
                <td valign="top" align="left" class="divtext" >
                    Email ID: *
                </td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtEmailId" runat="server" CssClass="inputbg" />
                </td>
            </tr>
            <tr>
           <td valign="top" align="left" class="divtext" >
                    Pin Code: *
                </td>
                 <td valign="top" align="left">
                    <asp:TextBox ID="txtpin" runat="server"
                       CssClass="inputbg"  />
                  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3"  FilterType="Numbers"  TargetControlID="txtpin"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
                   
                </td>
                <td valign="top" align="left" class="divtext">
                    MobileNo: *
                </td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtMobileNo" runat="server"  CssClass="inputbg" />
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5"  FilterType="Numbers"    TargetControlID="txtMobileNo"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
                </td>
                </tr>
              
                <td valign="top" align="left">
                    <asp:TextBox ID="txtPassword" runat="server"   TextMode="Password"
                       CssClass="inputbg" Visible="false" />
                </td>
              
                <td valign="top" align="left">
                    <asp:TextBox ID="txtConfirmPassword" runat="server"  TextMode="Password"
                       CssClass="inputbg" Visible="false" />
                </td>
            
            <tr>
           
                <td valign="top" align="left" class="divtext">
                    Date Of Birth:* 
                </td>
                <td valign="top" align="left">
                    <asp:DropDownList ID="ddlday" runat="server" CssClass="inputdropdown">
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
                    <asp:DropDownList ID="ddlmonth" runat="server" CssClass="inputdropdown">
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
                    <asp:DropDownList ID="ddlyear" runat="server" CssClass="inputdropdown" >
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="inputbg" Visible="false" />
                </td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtDesignation" runat="server" CssClass="inputbg" Visible="false"/>
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
                        <asp:ListItem Value="Single" Text="Single" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="Married" Text="Married"></asp:ListItem>
                        <asp:ListItem Value="Prefer Not to Say" Text="Prefer Not to Say"></asp:ListItem>
                    </asp:RadioButtonList>
                    </b>
                </td>
                <td valign="top" align="left" id="trAnn" runat="server">Anniversary Date<br />
                    <asp:DropDownList ID="DdlDayAnniversary" runat="server" CssClass="inputdropdown">
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
                    <asp:DropDownList ID="DdlMonthAnniversary" runat="server" CssClass="inputdropdown">
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
                    <asp:DropDownList ID="DdlYearAnniversary" runat="server" CssClass="inputdropdown">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" colspan="4">
                    <input type="checkbox" id="ChkTerms" value="" />I have been through and accept the
                    <a href="javascript:OpenpopUp()" style=" color:Red;">terms and conditions </a> of the programme
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext">
                    &nbsp;</td>
                <td valign="top" align="left">
                    &nbsp;</td>
                <td valign="top" align="left" class="divtext">
                    <div>
                        <asp:Button ID="btnSubmit" runat="server"  OnClick="btnSubmit_Click" OnClientClick="javascript:return validInsert();" Width="65px" BackColor="#FBB434" BorderColor="#FBB434" ForeColor="#45194E" Font-Bold="true" Text="Submit"/>
                        <%--<asp:LinkButton ID="BtnSubmit" runat="server" OnClick="BtnSubmit_Click" OnClientClick="javascript:return validInsert();" Width="45px" Text="Submit" BackColor="#FBB434" BorderColor="#FBB434" ForeColor="#45194E" Font-Bold="true" ></asp:LinkButton>--%>
                        <span></span>
                    </div>
                </td>
                <td valign="top" align="left">
                    &nbsp;</td>
            </tr>
        </table>     
<div id="grayBG" runat="server" class="grayBox" style="display:inline;" >
   <div id="showcontainer" runat="server" class="box_content" style="border:solid 1px #FF9900;display:inline;"  >
    </div>         
<div id="Container" class="box_content1" runat="server" style="background-color:White;display:inline;">
<center><p style="color: #000000"><b><asp:Label ID="lb_digitalmsg" runat="server" Visible="true"></asp:Label></b></p></center> 
<br />
<hr />
<asp:Button ID="Button1" class="buttontickets" runat="server" Text="OK" onclick="btnclose_Click" />
</div></div>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
.grayBox{ 
    position: fixed; 
    top: 0%; 
    left: 0%; 
    width: 100%; 
    height: 100%; 
    background-color: Black; 
    z-index:1001; 
    -moz-opacity: .90; 
    opacity:.90; 
    filter: alpha(opacity=90); 
} 
.box_content { 
    position:absolute;
	width:400px;
	height:140px;
	display:none;
	z-index:9999;
	padding:20px;
	top:200px;
	left:400px;
	background-color: #000000;
	border:solid 1px #FF9900;
} 
.buttontickets{
	float: right;
	margin-right:150px;
	background-color:#eed075;
	border-color:Orange;
	color: #231f20;
	width:80px;
    font-weight:bold;
}
.buttontickets1{
	float:left;
	background-color:#eed075;
	border-color:Orange;
	color: #231f20;
	width:80px;
    font-weight:bold;
}
.box_content1 { 
    position:absolute;
	width:390px;
	height:130px;
	display:none;
	z-index:9999;
	padding:20px;
	top:206px;
	left:406px;
	background-color: #000000;
} 
</style>
<script language="javascript" type="text/javascript">
    function digital_kaos() {
        setTimeout(function () { window.location = "http://localhost/RoyalWebApp/Account/Digital_Kaos.aspx"; }, 20000);
    }
    // Begin pop up
      function OpenpopUp() {
          var retVal = ""
          var valReturned;
          retVal = showModalDialog('TermsAndConditions.aspx');
          valReturned = retVal;
          if (valReturned == "1") {
              document.getElementById("ChkTerms").checked = true;
          }
      }
    // End
    </script>
    <script language="javascript" type="text/javascript">
        function validInsert() {
            var firstname = document.getElementById("<%=txtFirstName.ClientID %>");
            if (firstname.value == "") {
                alert("Please enter First name");
                firstname.focus();
                return false;
            }
            var lastname = document.getElementById("<%=txtLastName.ClientID %>");
            if (lastname.value == "") {
                alert("Please enter Last name");
                lastname.focus();
                return false;
            }
            var address = document.getElementById("<%=txtAddress.ClientID %>");
            if (address.value == "") {
                alert("Please enter your Address");
                address.focus();
                return false;
            }
            var city = document.getElementById("<%=txtCity.ClientID %>");
            if (city.value == "") {
                alert("Please enter your City");
                city.focus();
                return false;
            }
            var pin = document.getElementById("<%=txtpin.ClientID %>");
            if (pin.value == "") {
                alert("Please enter Pin Code");
                pin.focus();
                return false;
            }
            var mail = document.getElementById("<%=txtEmailId.ClientID %>");
            if (mail.value == "") {
                alert("Please enter Email Id");
                mail.focus();
                return false;
            }
            var emailPat = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;

            var emailid = mail.value;
            var matchArray = emailid.match(emailPat);
            if (matchArray == null) {
                alert("Please Enter Valid Email ID");
                mail.focus();
                return false;
            }
            var mobile = document.getElementById("<%=txtMobileNo.ClientID %>");
            if (mobile.value == "") {
                alert("Please enter mobile no");
                mobile.focus();
                return false;
            }
            if (IsNumeric(mobile.value) == false) {
                alert("Please enter Numeric Only");
                mobile.focus();
                return false;
            }
            if ((mobile.value).length < 10) {
                alert("Mobile no should be minimum 10 digits");
                mobile.focus();
                return false;
            }
            if ((mobile.value).length > 10) {
                alert("Mobile Number Should be of 10 digits only");
                mobile.focus();
                return false;
            }
            if (document.getElementById("ChkTerms").checked == false) {
                alert("Please Check the Terms And Conditions");
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
    </asp:Content>

