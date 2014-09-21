<%@ Page Title="" Language="C#" MasterPageFile="~/Skins/Master/AccountMaster.Master" AutoEventWireup="true" CodeFile="FirstTimeLogin.aspx.cs" Inherits="Account_FirstTimeLogin1" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    { 
        if(!IsPostBack)
        {
        lblMsg.Visible = false;
        bindyear();
    }
    }

    void bindyear()
    {
        DropDownList3.DataSource = BindYear18Years();
        DropDownList3.DataBind();
    }

    public ArrayList BindYear18Years()
    {

        ArrayList arr = new ArrayList();
        for (int i = 1900; i <= DateTime.Now.Year - 18; i++)
        {
            arr.Add(i);
        }
        return arr;
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        Session["MembershipID"] = txtMemebershipID.Text;
        Session["DateOfBirth"] = DropDownList1.SelectedItem.Text + '/' + DropDownList2.SelectedItem.Text + '/' + DropDownList3.SelectedValue;
        //string year = ;
        RoyalWebApp.EntityServiceReference.EntityServiceClient ServiceClient = new RoyalWebApp.EntityServiceReference.EntityServiceClient();
        ServiceClient.Open();
        bool pass = ServiceClient.PasswordExistsCheck(txtMemebershipID.Text.ToString(), Convert.ToDateTime(Session["DateOfBirth"].ToString()));
        var arr = ServiceClient.FirstTimeLogin(txtMemebershipID.Text.ToString(),Convert.ToDateTime(Session["DateOfBirth"].ToString()));
        if (arr > 0)
        {
            if (pass)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Dear Guest, You are a registered user,Please use <a href=ForgotPassword.aspx> Forgot Password</a> to know your password.";
                txtMemebershipID.Text = "";
                txtMemebershipID.Focus();
            }
            else
            {
                Response.Redirect("FirstTimeLoginUserDetails.aspx");
	        }
            
        }
        else
        {
            lblMsg.Visible = true;
            lblMsg.Text = "Invalid Member Id or Date of Birth";
            txtMemebershipID.Text = "";
            txtMemebershipID.Focus();
        }
        ServiceClient.Close();
    }
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #3f260a;
            text-decoration: none;
            width: 276px;
        }
        .style2
        {
            width: 230px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeading" Runat="Server">
    First Time Login
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageData" Runat="Server">
    <script language="javascript" type="text/javascript">
    // Begin pop up
        function OpenpopUp() {

            window.open("TermsAndConditions.aspx", '_blank', 'toolbar=yes,scrollbars=yes,location=no,statusbar=no,menubar=no,resizable=no,width=620,height=400, top=200,left=200');
        }
    // End
    </script>
    <script language="javascript" type="text/javascript">
    function validInsert() {
        if ($("[id*=txtMemebershipID]").val() == "") {   
            alert("Please enter Membership ID");
            $("[id*=txtMemebershipID]").focus();
            return false;
        }
        if (document.getElementById("ChkTerms").checked==false) {
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
        <table width="87%" cellpadding="2" cellspacing="2" class="divborder">
            <tr>
            <td colspan="4" align="center">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" colspan="4">
                  Kindly verify your account by providing the information below:
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="style1" >
                    Membership Id: *</td>
                <td valign="top" align="left" class="style2">
                    <asp:TextBox ID="txtMemebershipID" runat="server" CssClass="inputbg"  Text=""/>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="style1" >
                    Date of Birth (dd/mm/yyyy): * 
                    <br />
                    (as per our records)</td>
                <td valign="top" align="left">
                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="inputdropdown">
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
                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="inputdropdown">
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
                    <asp:DropDownList ID="DropDownList3" runat="server" CssClass="inputdropdown" AutoPostBack="false" >
                    
                    <%--<asp:ListItem Value="01" Selected="True" Text="1910"></asp:ListItem>
                        <asp:ListItem Value="02" Text="1911"></asp:ListItem>
                        <asp:ListItem Value="03" Text="1912"></asp:ListItem>
                        <asp:ListItem Value="04" Text="1913"></asp:ListItem>
                        <asp:ListItem Value="05" Text="1914"></asp:ListItem>
                        <asp:ListItem Value="06" Text="1915"></asp:ListItem>
                        <asp:ListItem Value="07" Text="1916"></asp:ListItem>
                        <asp:ListItem Value="08" Text="1917"></asp:ListItem>
                        <asp:ListItem Value="09" Text="1918"></asp:ListItem>
                        <asp:ListItem Value="10" Text="1919"></asp:ListItem>
                        <asp:ListItem Value="11" Text="1920"></asp:ListItem>
                        <asp:ListItem Value="12" Text="1921"></asp:ListItem>
                        <asp:ListItem Value="13" Text="1922"></asp:ListItem>
                        <asp:ListItem Value="14" Text="1923"></asp:ListItem>
                        <asp:ListItem Value="15" Text="1924"></asp:ListItem>
                        <asp:ListItem Value="16" Text="1925"></asp:ListItem>
                        <asp:ListItem Value="17" Text="1926"></asp:ListItem>
                        <asp:ListItem Value="18" Text="1927"></asp:ListItem>
                        <asp:ListItem Value="19" Text="1928"></asp:ListItem>
                        <asp:ListItem Value="20" Text="1929"></asp:ListItem>
                        <asp:ListItem Value="21" Text="1930"></asp:ListItem>
                        <asp:ListItem Value="22" Text="1931"></asp:ListItem>
                        <asp:ListItem Value="23" Text="1932"></asp:ListItem>
                        <asp:ListItem Value="24" Text="1933"></asp:ListItem>
                        <asp:ListItem Value="25" Text="1934"></asp:ListItem>
                        <asp:ListItem Value="26" Text="1935"></asp:ListItem>
                        <asp:ListItem Value="27" Text="1936"></asp:ListItem>
                        <asp:ListItem Value="28" Text="1937"></asp:ListItem>
                        <asp:ListItem Value="29" Text="1938"></asp:ListItem>
                        <asp:ListItem Value="30" Text="1939"></asp:ListItem>
                        <asp:ListItem Value="31" Text="1940"></asp:ListItem>
                        <asp:ListItem Value="32" Text="1941"></asp:ListItem>
                        <asp:ListItem Value="33" Text="1942"></asp:ListItem>
                        <asp:ListItem Value="34" Text="1943"></asp:ListItem>
                        <asp:ListItem Value="35" Text="1944"></asp:ListItem>
                        <asp:ListItem Value="36" Text="1945"></asp:ListItem>
                        <asp:ListItem Value="37" Text="1946"></asp:ListItem>
                        <asp:ListItem Value="38" Text="1947"></asp:ListItem>
                        <asp:ListItem Value="39" Text="1948"></asp:ListItem>
                        <asp:ListItem Value="40" Text="1949"></asp:ListItem>
                        <asp:ListItem Value="41" Text="1950"></asp:ListItem>
                        <asp:ListItem Value="42" Text="1951"></asp:ListItem>
                        <asp:ListItem Value="43" Text="1952"></asp:ListItem>
                        <asp:ListItem Value="44" Text="1953"></asp:ListItem>
                        <asp:ListItem Value="45" Text="1954"></asp:ListItem>
                        <asp:ListItem Value="46" Text="1955"></asp:ListItem>
                        <asp:ListItem Value="47" Text="1956"></asp:ListItem>
                        <asp:ListItem Value="48" Text="1957"></asp:ListItem>
                        <asp:ListItem Value="49" Text="1958"></asp:ListItem>
                        <asp:ListItem Value="50" Text="1959"></asp:ListItem>
                        <asp:ListItem Value="51" Text="1960"></asp:ListItem>
                        <asp:ListItem Value="52" Text="1961"></asp:ListItem>
                        <asp:ListItem Value="53" Text="1962"></asp:ListItem>
                        <asp:ListItem Value="54" Text="1963"></asp:ListItem>
                        <asp:ListItem Value="55" Text="1964"></asp:ListItem>
                        <asp:ListItem Value="56" Text="1965"></asp:ListItem>
                        <asp:ListItem Value="57" Text="1966"></asp:ListItem>
                        <asp:ListItem Value="58" Text="1967"></asp:ListItem>
                        <asp:ListItem Value="59" Text="1968"></asp:ListItem>
                        <asp:ListItem Value="60" Text="1969"></asp:ListItem>
                        <asp:ListItem Value="61" Text="1970"></asp:ListItem>
                        <asp:ListItem Value="62" Text="1971"></asp:ListItem>
                        <asp:ListItem Value="63" Text="1972"></asp:ListItem>
                        <asp:ListItem Value="64" Text="1973"></asp:ListItem>
                        <asp:ListItem Value="65" Text="1974"></asp:ListItem>
                        <asp:ListItem Value="66" Text="1975"></asp:ListItem>
                        <asp:ListItem Value="67" Text="1976"></asp:ListItem>
                        <asp:ListItem Value="68" Text="1977"></asp:ListItem>
                        <asp:ListItem Value="69" Text="1978"></asp:ListItem>
                        <asp:ListItem Value="70" Text="1979"></asp:ListItem>
                        <asp:ListItem Value="71" Text="1980"></asp:ListItem>
                        <asp:ListItem Value="72" Text="1981"></asp:ListItem>
                        <asp:ListItem Value="73" Text="1982"></asp:ListItem>
                        <asp:ListItem Value="74" Text="1983"></asp:ListItem>
                        <asp:ListItem Value="75" Text="1984"></asp:ListItem>
                        <asp:ListItem Value="76" Text="1985"></asp:ListItem>
                        <asp:ListItem Value="77" Text="1986"></asp:ListItem>
                        <asp:ListItem Value="78" Text="1987"></asp:ListItem>
                        <asp:ListItem Value="79" Text="1988"></asp:ListItem>
                        <asp:ListItem Value="80" Text="1989"></asp:ListItem>
                        <asp:ListItem Value="81" Text="1990"></asp:ListItem>
                        <asp:ListItem Value="82" Text="1991"></asp:ListItem>
                        <asp:ListItem Value="83" Text="1992"></asp:ListItem>
                        <asp:ListItem Value="84" Text="1993"></asp:ListItem>
                        <asp:ListItem Value="85" Text="1994"></asp:ListItem>
                        <asp:ListItem Value="86" Text="1995"></asp:ListItem>
                        <asp:ListItem Value="87" Text="1996"></asp:ListItem>
                        <asp:ListItem Value="88" Text="1997"></asp:ListItem>
                        <asp:ListItem Value="89" Text="1998"></asp:ListItem>
                        <asp:ListItem Value="90" Text="1999"></asp:ListItem>
                        <asp:ListItem Value="91" Text="2000"></asp:ListItem>
                        <asp:ListItem Value="92" Text="2001"></asp:ListItem>
                        <asp:ListItem Value="93" Text="2002"></asp:ListItem>
                        <asp:ListItem Value="94" Text="2003"></asp:ListItem>
                        <asp:ListItem Value="95" Text="2004"></asp:ListItem>
                        <asp:ListItem Value="96" Text="2005"></asp:ListItem>
                        <asp:ListItem Value="97" Text="2006"></asp:ListItem>
                        <asp:ListItem Value="98" Text="2007"></asp:ListItem>
                        <asp:ListItem Value="99" Text="2008"></asp:ListItem>
                        <asp:ListItem Value="100" Text="2009"></asp:ListItem>
                        <asp:ListItem Value="101" Text="2010"></asp:ListItem>
                        <asp:ListItem Value="102" Text="2011"></asp:ListItem>
                        <asp:ListItem Value="103" Text="2012"></asp:ListItem>--%>
                 
                    </asp:DropDownList>
                </td>
                
            </tr>
            
            
            <tr>
                <td valign="top" align="left" class="divtext" colspan="4">
                    <input type="checkbox" id="ChkTerms" value=""/>
                    I have been through and accept the
                    <a href="javascript:OpenpopUp()">terms and conditions </a>of the programme
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="style1">
                    &nbsp;</td>
                <td valign="top" align="left" class="style2">
                    &nbsp;</td>
                <%--<td valign="top" align="left" class="divtext">
                    <div class="button">
                        <a href="#">
                        <asp:LinkButton ID="BtnSubmit" runat="server"  OnClick="BtnSubmit_Click" OnClientClick="javascript:return validInsert();" Width="45px" Text="Submit" ></asp:LinkButton>
                        </a><span></span>
                    </div>
                </td>--%>
                <td valign="top" align="left" class="divtext">
                <div class="button">
                <a href="#">
                    <asp:LinkButton ID="BtnSubmit" runat="server"  Width="46px" Text="Submit"  
                        onClientClick="javascript:return validInsert();" onclick="BtnSubmit_Click" ></asp:LinkButton>
                    </a><span></span>
                </div>
                </td>
                <td valign="top" align="left">
                    &nbsp;</td>
            </tr>
        </table>
</asp:Content>

