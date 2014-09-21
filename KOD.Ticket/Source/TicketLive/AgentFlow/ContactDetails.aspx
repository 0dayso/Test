<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="ContactDetails.aspx.cs" Inherits="AgentFlow_ContactDetails" %>

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
        top:100px;
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
                <asp:TextBox ID="txtContactNo" Width="124px" runat="server" MaxLength="10"></asp:TextBox>
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
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderadd" FilterType="LowercaseLetters,UppercaseLetters,Custom,Numbers" ValidChars="-/ " TargetControlID="txtAddress"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
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
            <td align="left" colspan="3">
                <asp:Label ID="Label1" style="color:Red" runat="server" Text="*please avoid using [+,$,#] etc"></asp:Label>
            </td>
        </tr>
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
                <asp:Button ID="btnBackHome" CssClass="common-button" runat="server" Text="Back To Home"
                    OnClick="btnBackHome_Click" />
                <asp:Button ID="btnSubmit" runat="server" CssClass="common-button" ValidationGroup="cont"
                    Text="Proceed" OnClick="btnSubmit_Click" />
                <asp:ValidationSummary ID="ValidationSummary1" HeaderText="Some Fields were missing..."
                    ShowMessageBox="true" DisplayMode="List" ValidationGroup="cont" ShowSummary="false"
                    runat="server" />
            </td>
        </tr>
    </table>
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
