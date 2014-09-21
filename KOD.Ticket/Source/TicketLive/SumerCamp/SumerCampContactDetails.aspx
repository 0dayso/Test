<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" 
CodeFile="SumerCampContactDetails.aspx.cs" Inherits="Sumer_Camp_SumerCampContactDetails" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<style type="text/css">
        .ModalWindow
        {
            background-color: #000000;
            border-width: 3px;
            border-style: solid;
            border-color: #E7C54A;
            padding: 3px;
            width: 300px;
            height: 200px;
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
            z-index:2000;
        }
        .grayBox
        { 
        position: fixed; 
        top: 0%; 
        left: 0%; 
        width: 100%; 
        height: 100%; 
        background-color: black; 
        z-index:1001; 
        -moz-opacity: 1.0; 
        opacity:.95; 
        filter: alpha(opacity=90); 
        } 
       .box_content 
       { 
        position:absolute;
        width:350px;
        height:100%px;
        display:none;
        z-index:9999;
        padding:20px;
        top:200px;
        left:400px;
        background-color: #000000;
        border:solid 1px #FF9900;
      } 
     .buttontickets
     {
        float: none;
        background-color:#eed075;
        border-color:Orange;
        color: #231f20;
        width:80px;
        font-weight:bold;
        position:absolute;
        
        
        
     }
      
    </style>
</asp:Content>   
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="Server">
<script language="javascript" type="text/javascript" src="../js/jquery-1.8.2.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("input[type=radio]").change(function () {
            var test = $(this).val();
            if (test == "AMEX") {
                $("#grayBG").show();
                $("#Container").show();
            }
        });
    });
</script>

    <table width="100%">
       <tr>
            <td colspan="2">
                   <asp:Label ID="lblttlAmt" runat="server" CssClass="error" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                   <asp:Label ID="lblpayAmt" runat="server" CssClass="error" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                Name :
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=" " TargetControlID="txtName"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
                <asp:RequiredFieldValidator ID="re1" ControlToValidate="txtName" ValidationGroup="cont"
                    Display="None" ErrorMessage="Name" ForeColor="#ECA035" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                Contact No. :
            </td>
            <td>
                <asp:TextBox ID="txtISDCode" Text="+91" Width="22px" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtContactNo" Width="124px" runat="server"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="F1" FilterType="Numbers" TargetControlID="txtContactNo"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
                <asp:RequiredFieldValidator ID="re2" Text="required!" ControlToValidate="txtContactNo"
                    ValidationGroup="cont" ErrorMessage="Contact No" Display="None" ForeColor="#ECA035"
                    runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                Address :
            </td>
            <td>
                <asp:TextBox ID="txtAddress" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                Email Address :
            </td>
            <td>
                <asp:TextBox ID="txtEmailAddress" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="re3" ControlToValidate="txtEmailAddress" ValidationGroup="cont"
                    ErrorMessage="Email Address" Display="None" ForeColor="#ECA035" runat="server" />
                <asp:RegularExpressionValidator ID="rg1" ErrorMessage="Valid Email" ControlToValidate="txtEmailAddress"
                    ValidationGroup="cont" Display="None" ForeColor="#ECA035" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
            </td>
        </tr>
        <tr>
            <td align="left">
                Payment Mode :
            </td>
            <td>
                <asp:DropDownList ID="ddlPaymentMode" onchange="Change_Paytype()" runat="server">
                    <asp:ListItem Value="MODE" Text="Select Payment Mode"></asp:ListItem>
                    <asp:ListItem Value="CREDIT" Text="Credit/Debit Card"></asp:ListItem>
               </asp:DropDownList>
                 <asp:RequiredFieldValidator ID="re4" Text="required!" ControlToValidate="ddlPaymentMode"
                    ValidationGroup="cont" ErrorMessage="Payment" InitialValue="MODE" Display="None" ForeColor="#ECA035"
                    runat="server" />
            </td>
        </tr>


     
        <tr>
            <td valign="top" align="left">
                <div id="tr_CardType" style="display: none">
                    Card Type :</div>
            </td>
           <td>
                <div id="tr_CardType1" style="display: none">
                  <%-- <asp:RadioButtonList ID="rbl_CardType" runat="server">
                    <asp:ListItem Value="HDFC" Text="MASTER/VISA"></asp:ListItem>
                       <asp:ListItem Value="IDBI" Text="IDBI(MASTER/VISA)" ></asp:ListItem>
                    </asp:RadioButtonList>--%><asp:Label ID="Label1" runat="server" Text="MASTER/VISA"></asp:Label>
                </div>
            </td>
        </tr>
     
    
     <div id="grayBG" class="grayBox" style="display:none;">
           <div id="showcontainer" class="box_content">
           </div> 
           <div id="Container" class="box_content">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/close4.jpg" style="float:right" onclick="Hide()"/><br/>
            Card issued From:&nbsp;<asp:DropDownList ID="ddl_country" onchange="Changeing()" runat="server" >
               <asp:ListItem Value="india" Text="India,Singapore,UK"></asp:ListItem>
               <asp:ListItem Value="others" Text="Others"></asp:ListItem>
               </asp:DropDownList><br /><br />
           <div id="divshow" runat="server" style="display:none">
            Title:&nbsp;<asp:DropDownList ID="Ddl_title" runat="server" >
                  <asp:ListItem Value="Mr." Text="Mr."></asp:ListItem>
                  <asp:ListItem Value="Mrs." Text="Mrs."></asp:ListItem>
                  </asp:DropDownList>
            Firstname*:&nbsp;<asp:TextBox ID="Txtfname" runat="server"></asp:TextBox><br /><br />
            Middlename*:&nbsp;<asp:TextBox ID="Txtmname" runat="server"></asp:TextBox><br /><br />
            Lastname*:&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Txtlname" runat="server"></asp:TextBox><br /><br />
            Street*:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:TextBox ID="txt_street" runat="server"></asp:TextBox><br /><br />
            City*:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:TextBox ID="Txtcity" runat="server"></asp:TextBox><br /><br />
            State*:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:TextBox ID="Txtstate" runat="server"></asp:TextBox><br /><br />
            Pincode*:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:TextBox ID="txt_pin" runat="server"></asp:TextBox><br /><br /> 
            Country*:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
            <asp:DropDownList ID="txt_country" runat="server" Width="200px">   
            <asp:ListItem value="">Please Select</asp:ListItem>
            <asp:ListItem value="AFG">Afghanistan</asp:ListItem>
            <asp:ListItem value="ALB">Albania</asp:ListItem>
            <asp:ListItem value="DZA">Algeria</asp:ListItem>
            <asp:ListItem value="ASM">American Samoa</asp:ListItem>
            <asp:ListItem value="AND">Andorra</asp:ListItem>
            <asp:ListItem value="AGO">Angola</asp:ListItem>
            <asp:ListItem value="AIA">Anguilla</asp:ListItem>
            <asp:ListItem value="ATA">Antarctica</asp:ListItem>
            <asp:ListItem value="ATG">Antigua And Barbuda</asp:ListItem>
            <asp:ListItem value="ARG">Argentina</asp:ListItem>
            <asp:ListItem value="ARM">Armenia</asp:ListItem>
            <asp:ListItem value="ABW">Aruba</asp:ListItem>
            <asp:ListItem value="AUS">Australia</asp:ListItem>
            <asp:ListItem value="AUT">Austria</asp:ListItem>
            <asp:ListItem value="AZE">Azerbaijan</asp:ListItem>
            <asp:ListItem value="BHS">Bahamas</asp:ListItem>
            <asp:ListItem value="BHR">Bahrain</asp:ListItem>
            <asp:ListItem value="BGD">Bangladesh</asp:ListItem>
            <asp:ListItem value="BRB">Barbados</asp:ListItem>
            <asp:ListItem value="BLR">Belarus</asp:ListItem>
            <asp:ListItem value="BEL">Belgium</asp:ListItem>
            <asp:ListItem value="BLZ">Belize</asp:ListItem>
            <asp:ListItem value="BEN">Benin</asp:ListItem>
            <asp:ListItem value="BMU">Bermuda</asp:ListItem>
            <asp:ListItem value="BTN">Bhutan</asp:ListItem>
            <asp:ListItem value="BOL">Bolivia</asp:ListItem>
            <asp:ListItem value="BIH">Bosnia And Herzegowina</asp:ListItem>
            <asp:ListItem value="BWA">Botswana</asp:ListItem>
            <asp:ListItem value="BVT">Bouvet Island</asp:ListItem>
            <asp:ListItem value="BRA">Brazil</asp:ListItem>
            <asp:ListItem value="IOT">British Indian Ocean Territory</asp:ListItem>
            <asp:ListItem value="BRN">Brunei Darussalam</asp:ListItem>
            <asp:ListItem value="BGR">Bulgaria</asp:ListItem>
            <asp:ListItem value="BFA">Burkina Faso</asp:ListItem>
            <asp:ListItem value="BDI">Burundi</asp:ListItem>
            <asp:ListItem value="KHM">Cambodia</asp:ListItem>
            <asp:ListItem value="CMR">Cameroon</asp:ListItem>
            <asp:ListItem value="CAN">Canada</asp:ListItem>
            <asp:ListItem value="CPV">Cape Verde</asp:ListItem>
            <asp:ListItem value="CYM">Cayman Islands</asp:ListItem>
            <asp:ListItem value="CAF">Central African Republic</asp:ListItem>
            <asp:ListItem value="TCD">Chad</asp:ListItem>
            <asp:ListItem value="CHL">Chile</asp:ListItem>
            <asp:ListItem value="CHN">China</asp:ListItem>
            <asp:ListItem value="CXR">Christmas Island</asp:ListItem>
            <asp:ListItem value="CCK">Cocos (Keeling) Islands</asp:ListItem>
            <asp:ListItem value="COL">Colombia</asp:ListItem>
            <asp:ListItem value="COM">Comoros</asp:ListItem>
            <asp:ListItem value="COG">Congo</asp:ListItem>
            <asp:ListItem value="COK">Cook Islands</asp:ListItem>
            <asp:ListItem value="CRI">Costa Rica</asp:ListItem>
            <asp:ListItem value="CIV">Cote D'ivoire</asp:ListItem>
            <asp:ListItem value="HRV">Croatia</asp:ListItem>
            <asp:ListItem value="CUB">Cuba</asp:ListItem>
            <asp:ListItem value="CYP">Cyprus</asp:ListItem>
            <asp:ListItem value="CZE">Czech Republic</asp:ListItem>
            <asp:ListItem value="DNK">Denmark</asp:ListItem>
            <asp:ListItem value="DJI">Djibouti</asp:ListItem>
            <asp:ListItem value="DMA">Dominica</asp:ListItem>
            <asp:ListItem value="DOM">Dominican Republic</asp:ListItem>
            <asp:ListItem value="TMP">East Timor</asp:ListItem>
            <asp:ListItem value="ECU">Ecuador</asp:ListItem>
            <asp:ListItem value="EGY">Egypt</asp:ListItem>
            <asp:ListItem value="SLV">El Salvador</asp:ListItem>
            <asp:ListItem value="GNQ">Equatorial Guinea</asp:ListItem>
            <asp:ListItem value="ERI">Eritrea</asp:ListItem>
            <asp:ListItem value="EST">Estonia</asp:ListItem>
            <asp:ListItem value="ETH">Ethiopia</asp:ListItem>
            <asp:ListItem value="FLK">Falkland Islands (Malvinas)</asp:ListItem>
            <asp:ListItem value="FRO">Faroe Islands</asp:ListItem>
            <asp:ListItem value="FJI">Fiji</asp:ListItem>
            <asp:ListItem value="FIN">Finland</asp:ListItem>
            <asp:ListItem value="FRA">France</asp:ListItem>
            <asp:ListItem value="FXX">France, Metropolitan</asp:ListItem>
            <asp:ListItem value="GUF">French Guiana</asp:ListItem>
            <asp:ListItem value="PYF">French Polynesia</asp:ListItem>
            <asp:ListItem value="ATF">French Southern Territories</asp:ListItem>
            <asp:ListItem value="GAB">Gabon</asp:ListItem>
            <asp:ListItem value="GMB">Gambia</asp:ListItem>
            <asp:ListItem value="GEO">Georgia</asp:ListItem>
            <asp:ListItem value="DEU">Germany</asp:ListItem>
            <asp:ListItem value="GHA">Ghana</asp:ListItem>
            <asp:ListItem value="GIB">Gibraltar</asp:ListItem>
            <asp:ListItem value="GRC">Greece</asp:ListItem>
            <asp:ListItem value="GRL">Greenland</asp:ListItem>
            <asp:ListItem value="GRD">Grenada</asp:ListItem>
            <asp:ListItem value="GLP">Guadeloupe</asp:ListItem>
            <asp:ListItem value="GUM">Guam</asp:ListItem>
            <asp:ListItem value="GTM">Guatemala</asp:ListItem>
            <asp:ListItem value="GIN">Guinea</asp:ListItem>
            <asp:ListItem value="GNB">Guinea-Bissau</asp:ListItem>
            <asp:ListItem value="GUY">Guyana</asp:ListItem>
            <asp:ListItem value="HTI">Haiti</asp:ListItem>
            <asp:ListItem value="HMD">Heard And Mc Donald Islands</asp:ListItem>
            <asp:ListItem value="HND">Honduras</asp:ListItem>
            <asp:ListItem value="HKG">Hong Kong</asp:ListItem>
            <asp:ListItem value="HUN">Hungary</asp:ListItem>
            <asp:ListItem value="ISL">Iceland</asp:ListItem>
            <%--<asp:ListItem value="IND">India</asp:ListItem>--%>
            <asp:ListItem value="IDN">Indonesia</asp:ListItem>
            <asp:ListItem value="IRN">Iran (Islamic Republic Of)</asp:ListItem>
            <asp:ListItem value="IRQ">Iraq</asp:ListItem>
            <asp:ListItem value="IRL">Ireland</asp:ListItem>
            <asp:ListItem value="ISR">Israel</asp:ListItem>
            <asp:ListItem value="ITA">Italy</asp:ListItem>
            <asp:ListItem value="JAM">Jamaica</asp:ListItem>
            <asp:ListItem value="JPN">Japan</asp:ListItem>
            <asp:ListItem value="JOR">Jordan</asp:ListItem>
            <asp:ListItem value="KAZ">Kazakhstan</asp:ListItem>
            <asp:ListItem value="KEN">Kenya</asp:ListItem>
            <asp:ListItem value="KIR">Kiribati</asp:ListItem>
            <asp:ListItem value="PRK">Korea, Democratic People's Republic Of</asp:ListItem>
            <asp:ListItem value="KOR">Korea, Republic Of</asp:ListItem>
            <asp:ListItem value="KWT">Kuwait</asp:ListItem>
            <asp:ListItem value="KGZ">Kyrgyzstan</asp:ListItem>
            <asp:ListItem value="LAO">Lao People's Democratic Republic</asp:ListItem>
            <asp:ListItem value="LVA">Latvia</asp:ListItem>
            <asp:ListItem value="LBN">Lebanon</asp:ListItem>
            <asp:ListItem value="LSO">Lesotho</asp:ListItem>
            <asp:ListItem value="LBR">Liberia</asp:ListItem>
            <asp:ListItem value="LBY">Libyan Arab Jamahiriya</asp:ListItem>
            <asp:ListItem value="LIE">Liechtenstein</asp:ListItem>
            <asp:ListItem value="LTU">Lithuania</asp:ListItem>
            <asp:ListItem value="LUX">Luxembourg</asp:ListItem>
            <asp:ListItem value="MAC">Macau</asp:ListItem>
            <asp:ListItem value="MKD">Macedonia, The Former Yugoslav Republic Of</asp:ListItem>
            <asp:ListItem value="MDG">Madagascar</asp:ListItem>
            <asp:ListItem value="MWI">Malawi</asp:ListItem>
            <asp:ListItem value="MYS">Malaysia</asp:ListItem>
            <asp:ListItem value="MDV">Maldives</asp:ListItem>
            <asp:ListItem value="MLI">Mali</asp:ListItem>
            <asp:ListItem value="MLT">Malta</asp:ListItem>
            <asp:ListItem value="MHL">Marshall Islands</asp:ListItem>
            <asp:ListItem value="MTQ">Martinique</asp:ListItem>
            <asp:ListItem value="MRT">Mauritania</asp:ListItem>
            <asp:ListItem value="MUS">Mauritius</asp:ListItem>
            <asp:ListItem value="MYT">Mayotte</asp:ListItem>
            <asp:ListItem value="MEX">Mexico</asp:ListItem>
            <asp:ListItem value="FSM">Micronesia (Federated States Of)</asp:ListItem>
            <asp:ListItem value="MDA">Moldova, Republic Of</asp:ListItem>
            <asp:ListItem value="MCO">Monaco</asp:ListItem>
            <asp:ListItem value="MNG">Mongolia</asp:ListItem>
            <asp:ListItem value="MSR">Montserrat</asp:ListItem>
            <asp:ListItem value="MAR">Morocco</asp:ListItem>
            <asp:ListItem value="MOZ">Mozambique</asp:ListItem>
            <asp:ListItem value="MMR">Myanmar</asp:ListItem>
            <asp:ListItem value="NAM">Namibia</asp:ListItem>
            <asp:ListItem value="NRU">Nauru</asp:ListItem>
            <asp:ListItem value="NPL">Nepal</asp:ListItem>
            <asp:ListItem value="NLD">Netherlands</asp:ListItem>
            <asp:ListItem value="ANT">Netherlands Antilles</asp:ListItem>
            <asp:ListItem value="NCL">New Caledonia</asp:ListItem>
            <asp:ListItem value="NZL">New Zealand</asp:ListItem>
            <asp:ListItem value="NIC">Nicaragua</asp:ListItem>
            <asp:ListItem value="NER">Niger</asp:ListItem>
            <asp:ListItem value="NGA">Nigeria</asp:ListItem>
            <asp:ListItem value="NIU">Niue</asp:ListItem>
            <asp:ListItem value="NFK">Norfolk Island</asp:ListItem>
            <asp:ListItem value="MNP">Northern Mariana Islands</asp:ListItem>
            <asp:ListItem value="NOR">Norway</asp:ListItem>
            <asp:ListItem value="OMN">Oman</asp:ListItem>
            <asp:ListItem value="PAK">Pakistan</asp:ListItem>
            <asp:ListItem value="PLW">Palau</asp:ListItem>
            <asp:ListItem value="PAN">Panama</asp:ListItem>
            <asp:ListItem value="PNG">Papua New Guinea</asp:ListItem>
            <asp:ListItem value="PRY">Paraguay</asp:ListItem>
            <asp:ListItem value="PER">Peru</asp:ListItem>
            <asp:ListItem value="PHL">Philippines</asp:ListItem>
            <asp:ListItem value="PCN">Pitcairn</asp:ListItem>
            <asp:ListItem value="POL">Poland</asp:ListItem>
            <asp:ListItem value="PRT">Portugal</asp:ListItem>
            <asp:ListItem value="PRI">Puerto Rico</asp:ListItem>
            <asp:ListItem value="QAT">Qatar</asp:ListItem>
            <asp:ListItem value="REU">Reunion</asp:ListItem>
            <asp:ListItem value="ROM">Romania</asp:ListItem>
            <asp:ListItem value="RUS">Russian Federation</asp:ListItem>
            <asp:ListItem value="RWA">Rwanda</asp:ListItem>
            <asp:ListItem value="SHN">St. Helena</asp:ListItem>
            <asp:ListItem value="KNA">Saint Kitts And Nevis</asp:ListItem>
            <asp:ListItem value="LCA">Saint Lucia</asp:ListItem>
            <asp:ListItem value="SPM">St. Pierre And Miquelon</asp:ListItem>
            <asp:ListItem value="VCT">Saint Vincent And The Grenadines</asp:ListItem>
            <asp:ListItem value="WSM">Samoa</asp:ListItem>
            <asp:ListItem value="SMR">San Marino</asp:ListItem>
            <asp:ListItem value="STP">Sao Tome And Principe</asp:ListItem>
            <asp:ListItem value="SAU">Saudi Arabia</asp:ListItem>
            <asp:ListItem value="SEN">Senegal</asp:ListItem>
            <asp:ListItem value="SYC">Seychelles</asp:ListItem>
            <asp:ListItem value="SLE">Sierra Leone</asp:ListItem>
            <%--<asp:ListItem value="SGP">Singapore</asp:ListItem>--%>
            <asp:ListItem value="SVK">Slovakia</asp:ListItem>
            <asp:ListItem value="SVN">Slovenia</asp:ListItem>
            <asp:ListItem value="SLB">Solomon Islands</asp:ListItem>
            <asp:ListItem value="SOM">Somalia</asp:ListItem>
            <asp:ListItem value="ZAF">South Africa</asp:ListItem>
            <asp:ListItem value="SGS">South Georgia And The South Sandwich Islands</asp:ListItem>
            <asp:ListItem value="ESP">Spain</asp:ListItem>
            <asp:ListItem value="LKA">Sri Lanka</asp:ListItem>
            <asp:ListItem value="SDN">Sudan</asp:ListItem>
            <asp:ListItem value="SUR">Suriname</asp:ListItem>
            <asp:ListItem value="SJM">Svalbard And Jan Mayen Islands</asp:ListItem>
            <asp:ListItem value="SWZ">Swaziland</asp:ListItem>
            <asp:ListItem value="SWE">Sweden</asp:ListItem>
            <asp:ListItem value="CHE">Switzerland</asp:ListItem>
            <asp:ListItem value="SYR">Syrian Arab Republic</asp:ListItem>
            <asp:ListItem value="TWN">Taiwan, Province Of China</asp:ListItem>
            <asp:ListItem value="TJK">Tajikistan</asp:ListItem>
            <asp:ListItem value="TZA">Tanzania, United Republic Of</asp:ListItem>
            <asp:ListItem value="THA">Thailand</asp:ListItem>
            <asp:ListItem value="TGO">Togo</asp:ListItem>
            <asp:ListItem value="TKL">Tokelau</asp:ListItem>
            <asp:ListItem value="TON">Tonga</asp:ListItem>
            <asp:ListItem value="TTO">Trinidad And Tobago</asp:ListItem>
            <asp:ListItem value="TUN">Tunisia</asp:ListItem>
            <asp:ListItem value="TUR">Turkey</asp:ListItem>
            <asp:ListItem value="TKM">Turkmenistan</asp:ListItem>
            <asp:ListItem value="TCA">Turks And Caicos Islands</asp:ListItem>
            <asp:ListItem value="TUV">Tuvalu</asp:ListItem>
            <asp:ListItem value="UGA">Uganda</asp:ListItem>
            <asp:ListItem value="UKR">Ukraine</asp:ListItem>
            <asp:ListItem value="ARE">United Arab Emirates</asp:ListItem>
            <%--<asp:ListItem value="GBR">United Kingdom</asp:ListItem>--%>
            <asp:ListItem value="USA">United States</asp:ListItem>
            <asp:ListItem value="UMI">United States Minor Outlying Islands</asp:ListItem>
            <asp:ListItem value="URY">Uruguay</asp:ListItem>
            <asp:ListItem value="UZB">Uzbekistan</asp:ListItem>
            <asp:ListItem value="VUT">Vanuatu</asp:ListItem>
            <asp:ListItem value="VAT">Vatican City State (Holy See)</asp:ListItem>
            <asp:ListItem value="VEN">Venezuela</asp:ListItem>
            <asp:ListItem value="VNM">Viet Nam</asp:ListItem>
            <asp:ListItem value="VGB">Virgin Islands (British)</asp:ListItem>
            <asp:ListItem value="VIR">Virgin Islands (U.S.)</asp:ListItem>
            <asp:ListItem value="WLF">Wallis And Futuna Islands</asp:ListItem>
            <asp:ListItem value="ESH">Western Sahara</asp:ListItem>
            <asp:ListItem value="YEM">Yemen</asp:ListItem>
            <asp:ListItem value="YUG">Yugoslavia</asp:ListItem>
            <asp:ListItem value="ZAR">Zaire</asp:ListItem>
            <asp:ListItem value="ZMB">Zambia</asp:ListItem>
            <asp:ListItem value="ZWE">Zimbabwe</asp:ListItem>
                 </asp:DropDownList>
            <%--<asp:TextBox ID="Txtcountry" runat="server"></asp:TextBox>--%><br /><br />
          <hr />    
<input type="button" value="Proceed" class="buttontickets" id="Closebutton" runat="server" onclick="HideBox();"/>
    </div>
    <div id="button" runat="server" style="display:inline">
    <hr />
    <input type="button" value="Proceed" class="buttontickets" id="Buttonclose" runat="server" onclick="HideBox1();"/>     
    </div>
    <br />
    </div>
</div>

    
        

        
        <%--<tr>
            <td colspan="2">
            
                <asp:CheckBox ID="CheckBox1" runat="server"
                    Text=" I am interested in Royal Card Membership." 
                    />
            </td>
        </tr>--%>
        


   



       
        <tr>
            <td align="right" colspan="2">
                <asp:Label ID="lblMess" runat="server" CssClass="error"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <asp:Button ID="btnBackHome" CssClass="common-button" runat="server" 
                    Text="Back To Home" onclick="btnBackHome_Click"
                     />
                <asp:Button ID="btnSubmit" runat="server" CssClass="common-button" ValidationGroup="cont"
                    Text="Proceed" onclick="btnSubmit_Click"  />
                <asp:ValidationSummary ID="ValidationSummary1" HeaderText="Some Fields were missing..."
                    ShowMessageBox="true" DisplayMode="List" ValidationGroup="cont" ShowSummary="false"
                    runat="server" />
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        function Change_Paytype() {
            var val = document.getElementById("<%=ddlPaymentMode.ClientID %>").value;
            if (val == "CREDIT") {
                document.getElementById("tr_CardType").style.display = "inline";
                document.getElementById("tr_CardType1").style.display = "inline";
            }
            else {
                document.getElementById("tr_CardType").style.display = "none";
                document.getElementById("tr_CardType1").style.display = "none";
            }
        }
    </script>
    
   
    <script type="text/javascript">
        function HideBox() {
            var street = document.getElementById("<%=txt_street.ClientID %>");
            var country = document.getElementById("<%=txt_country.ClientID %>");
            var pin = document.getElementById("<%=txt_pin.ClientID %>");
            var city = document.getElementById("<%=Txtcity.ClientID %>");
            var state = document.getElementById("<%=Txtstate.ClientID %>");
            var firstname = document.getElementById("<%=Txtfname.ClientID %>");
            var middlename = document.getElementById("<%=Txtmname.ClientID %>");
            var lastname = document.getElementById("<%=Txtlname.ClientID %>");
            if (firstname.value == "") {
                alert("Please enter your First Name");
                firstname.focus();
            }
            else if (middlename.value == "") {
                alert("Please enter your Middle Name");
                middlename.focus();
            }
            else if (lastname.value == "") {
                alert("Please enter your Last Name");
                lastname.focus();
            }
            else if (street.value == "") {
                alert("Please enter your Street");
                street.focus();
            }
            else if (city.value == "") {
                alert("Please enter your City");
                city.focus();
            }
            else if (state.value == "") {
                alert("Please enter your State");
                state.focus();
            }
            else if (pin.value == "") {
                alert("Please enter your Pin");
                pin.focus();
            }
            else if (country.value == "") {
                alert("Please enter your Country");
                country.focus();
            }
            else {
                document.getElementById("grayBG").style.display = "none";
                document.getElementById("Container").style.display = "none";
            }
        }
     </script>
    <script type="text/javascript">
        function HideBox1() {
            document.getElementById("grayBG").style.display = "none";
            document.getElementById("Container").style.display = "none";
        }
    </script>
    
    <script type="text/javascript">
        function Hide() {
            document.getElementById("grayBG").style.display = "none";
            document.getElementById("Container").style.display = "none";
        }
    </script>

    <script type="text/javascript">
        function Changeing() {
            var val = document.getElementById("<%=ddl_country.ClientID %>").value;
            var val2 = document.getElementById("<%=divshow.ClientID %>");
            var val3 = document.getElementById("<%=button.ClientID %>");
            if (val == "others") {
                val2.style.display = "inline";
                val3.style.display = "none";
            } else {
                val2.style.display = "none";
                val3.style.display = "inline";
            }

        }
    </script>
  

  
       <script type="text/javascript">

           var _gaq = _gaq || [];
           _gaq.push(['_setAccount', 'UA-35374139-1']);
           _gaq.push(['_setDomainName', 'kingdomofdreams.in']);
           _gaq.push(['_trackPageview']);

           (function () {
               var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
               ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
               var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
           })();

</script>
</asp:Content>


