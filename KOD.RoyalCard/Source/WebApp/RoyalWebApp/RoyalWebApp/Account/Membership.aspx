<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Membership.aspx.cs" MasterPageFile="~/Skins/Master/AccountMaster.master" Inherits="RoyalWebApp.Account.Membership" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageHeading" runat="server">

    <%--Membership Forms--%>
  

</asp:Content>
<asp:Content ContentPlaceHolderID="CPHPageData" runat="server">
    
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
                     </td>
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
                   
<%--  <asp:DropDownList ID="DDLCity" runat="server" CssClass="inputbg">
<asp:ListItem Value="Andhra Pradesh" Text="Andhra Pradesh" Selected="True"></asp:ListItem>
<asp:ListItem Value="Arunachal Pradesh" Text="Arunachal Pradesh"></asp:ListItem>
<asp:ListItem Value="Assam" Text="Assam"></asp:ListItem>
<asp:ListItem Value="Bihar" Text="Bihar"></asp:ListItem>
<asp:ListItem Value="Chhattisgarh" Text="Chhattisgarh"></asp:ListItem>
<asp:ListItem Value="Goa" Text="Goa"></asp:ListItem>
<asp:ListItem Value="Gujarat" Text="Gujarat"></asp:ListItem>
<asp:ListItem Value="Haryana" Text="Haryana"></asp:ListItem>
<asp:ListItem Value="Himachal Pradesh" Text="Himachal Pradesh"></asp:ListItem>
<asp:ListItem Value="Jammu & Kashmir" Text="Jammu & Kashmir"></asp:ListItem>
<asp:ListItem Value="Jharkhand" Text="Jharkhand"></asp:ListItem>
<asp:ListItem Value="Karnataka" Text="Karnataka"></asp:ListItem>
<asp:ListItem Value="Kerala" Text="Kerala"></asp:ListItem>
<asp:ListItem Value="Madhya Pradesh" Text="Madhya Pradesh"></asp:ListItem>
<asp:ListItem Value="Maharashtra" Text="Maharashtra"></asp:ListItem>
<asp:ListItem Value="Manipur" Text="Manipur"></asp:ListItem>
<asp:ListItem Value="Meghalaya" Text="Meghalaya"></asp:ListItem>
<asp:ListItem Value="Mizoram" Text="Mizoram"></asp:ListItem>
<asp:ListItem Value="Nagaland" Text="Nagaland"></asp:ListItem>
<asp:ListItem Value="Orissa" Text="Orissa"></asp:ListItem>
<asp:ListItem Value="Punjab" Text="Punjab"></asp:ListItem>
<asp:ListItem Value="Rajasthan" Text="Rajasthan"></asp:ListItem>
<asp:ListItem Value="Sikkim" Text="Sikkim"></asp:ListItem>
<asp:ListItem Value="Tamil Nadu" Text="Tamil Nadu"></asp:ListItem>
<asp:ListItem Value="Tripura" Text="Tripura"></asp:ListItem>
<asp:ListItem Value="Uttar Pradesh" Text="Uttar Pradesh"></asp:ListItem>
<asp:ListItem Value="Uttaranchal" Text="Uttaranchal"></asp:ListItem>
<asp:ListItem Value="West Bengal" Text="West Bengal"></asp:ListItem>
<asp:ListItem Value="Andaman & Nicobar Islands" Text="Andaman & Nicobar Islands"></asp:ListItem>
<asp:ListItem Value="Chandigarh" Text="Chandigarh"></asp:ListItem>
<asp:ListItem Value="Dadra & Nagar Haveli" Text="Dadra & Nagar Haveli"></asp:ListItem>
<asp:ListItem Value="Daman & Diu" Text="Daman & Diu"></asp:ListItem>
<asp:ListItem Value="Delhi" Text="Delhi"></asp:ListItem>
<asp:ListItem Value="Lakshadweep" Text="Lakshadweep"></asp:ListItem>
<asp:ListItem Value="Puducherry" Text="Puducherry"></asp:ListItem>

</asp:DropDownList>--%>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" >
          Country:</td>
                <td valign="top" align="left">
             <%--<asp:DropDownList ID="DdlCountry" runat="server" CssClass="inputbg">
                 <asp:ListItem Value="India" Text="India"></asp:ListItem>
            </asp:DropDownList>--%><asp:TextBox ID="txtCountry" runat="server" CssClass="inputbg" Text="India" />
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
               <%-- <td valign="top" align="left" class="divtext">
                    Password:*</td>--%>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtPassword" runat="server"   TextMode="Password"
                       CssClass="inputbg" Visible="false" />
                </td>
                <%--<td valign="top" align="left" class="divtext">
                    Confirm Password:*</td>--%>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtConfirmPassword" runat="server"  TextMode="Password"
                       CssClass="inputbg" Visible="false" />
                </td>
            
            <tr>
            <%-- <td valign="top" align="left" class="divtext">
                    MobileNo: *
                </td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtMobileNo" runat="server"  CssClass="inputbg" />
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4"  FilterType="Numbers"    TargetControlID="txtMobileNo"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
                </td>
                --%>
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
                <%--<td valign="top" align="left" class="divtext">
                    Organization:</td>--%>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="inputbg" Visible="false" />
                </td>
               <%-- <td valign="top" align="left" class="divtext">
                    Designation:
                </td>--%>
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
                    <a href="javascript:OpenpopUp()">terms and conditions </a> of the programme
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext">
                    &nbsp;</td>
                <td valign="top" align="left">
                    &nbsp;</td>
                <td valign="top" align="left" class="divtext">
                    <div class="button">
                        <a href="#">
                        <asp:LinkButton ID="BtnSubmit" runat="server"  OnClick="BtnSubmit_Click" OnClientClick="javascript:return validInsert();" Width="45px" Text="Submit" ></asp:LinkButton>
                        </a><span></span>
                    </div>
                </td>
                <td valign="top" align="left">
                    &nbsp;<asp:Label ID="LblMsg" runat="server" Text=""></asp:Label></td>
            </tr>
        </table>     
   
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
<script language="javascript" type="text/javascript">
    // Begin pop up
    function OpenpopUp() {
        // day = new Date();
        // id = day.getTime();
        // window.open(URL, "Terms", "toolbar=yes,scrollbars=yes,location=no,statusbar=no,menubar=no,resizable=no,width=590,height=300, top=200,left=100");

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
            if ($("[id*=txtFirstName]").val() == "") {
                alert("Please enter First name");
                $("[id*=txtFirstName]").focus();
                return false;
            }
            if ($("[id*=txtLastName]").val() == "") {
                alert("Please enter Last name");
                $("[id*=txtLastName]").focus();
                return false;
            }
            if ($("[id*=txtAddress]").val() == "") {
                alert("Please enter your Address");
                $("[id*=txtAddress]").focus();
                return false;
            }
            if ($("[id*=txtCity]").val() == "") {
                alert("Please enter your City");
                $("[id*=txtCity]").focus();
                return false;
            }
            if ($("[id*=txtpin]").val() == "") {
                alert("Please enter Pin Code");
                $("[id*=txtpin]").focus();
                return false;
            }

            if ($("[id*=txtEmailId]").val() == "") {
                alert("Please enter Email Id");
                $("[id*=txtEmailId]").focus();
                return false;
            }
            //var emailPat = /^([a-zA-Z0-9_.+-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,6})+$/;
            var emailPat = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;

            var emailid = $("[id*=txtEmailId]").val();
            var matchArray = emailid.match(emailPat);
            if (matchArray == null) {
                alert("Please Enter Valid Email ID");
                $("[id*=txtEmailId]").focus();
                return false;
            }
            //                   if (<%Response.Write(txtPassword.ClientID);%>.value == "") 
            //                    {
            //                        alert("Please enter Password");
            //                       <%Response.Write(txtPassword.ClientID);%>.focus();
            //                        return false;
            //                    }
            //                    if ((<%Response.Write(txtPassword.ClientID);%>.value) != (<%Response.Write(txtConfirmPassword.ClientID);%>.value)) 
            //                    {
            //                        alert("Password should be same");
            //                       <%Response.Write(txtConfirmPassword.ClientID);%>.focus();
            //                        return false;
            //                    }

            if ($("[id*=txtMobileNo]").val() == "") {
                alert("Please enter mobile no");
                $("[id*=txtMobileNo]").focus();
                return false;
            }
            if (IsNumeric($("[id*=txtMobileNo]").val()) == false) {
                alert("Please enter Numeric Only");
                $("[id*=txtMobileNo]").focus();
                return false;
            }

            if (($("[id*=txtMobileNo]").val()).length < 10) {
                alert("Mobile no should be minimum 10 digits");
                $("[id*=txtMobileNo]").focus();
                return false;
            }
            if (($("[id*=txtMobileNo]").val()).length > 10) {
                alert("Mobile Number Should be of 10 digits only");
                $("[id*=txtMobileNo]").focus();
                return false;
            }
//            if (($("[id*=txtpin]").val() == "")){
//                alert("Please Enter PinCode");
//                $("[id*=txtpin]").focus();
//                return false;
//            }
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

