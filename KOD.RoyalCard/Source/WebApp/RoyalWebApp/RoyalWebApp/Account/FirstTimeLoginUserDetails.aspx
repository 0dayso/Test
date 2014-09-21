<%@ Page Title="" Language="C#" MasterPageFile="~/Skins/Master/AccountMaster.Master"
    AutoEventWireup="true" CodeBehind="FirstTimeLoginUserDetails.aspx.cs" Inherits="ROYALCARD.Account.FirstTimeLoginUserDetails" %>
    <%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Data.DataSetExtensions" %>
<script runat="server"> 
    string emailid = "", pwd = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindyear();
            bindData();
            trAnn.Visible = false;
        }


        string RegID = Session["MembershipID"].ToString();
        //string[] strarr = Session["DateOfBirth"].ToString().Split('/');
        //ddlday.SelectedValue = strarr[0];
        //ddlmonth.SelectedValue = strarr[1];
        //ddlyear.SelectedValue = strarr[2];
        lblpasswrdgenerate.Visible = false;
        btnGeneratePassword.Visible = false;
    }

    void bindData()
    {
        RoyalWebApp.EntityServiceReference.EntityServiceClient ServiceClient = new RoyalWebApp.EntityServiceReference.EntityServiceClient();
        ServiceClient.Open();
        var details = ServiceClient.GetUserDetails(Session["MembershipID"].ToString());
        if (details.Length > 0)
        {
            txtFirstName.Text = details[0].FirstName.ToString();
            txtLastName.Text = details[0].LastName.ToString();
            txtAddress.Text = details[0].Address.ToString();
            txtCity.Text = details[0].City.ToString();
            txtEmailId.Text = details[0].Email.ToString();
            txtMobileNo.Text = details[0].Mobile.ToString();
            DdlCountry.SelectedValue = details[0].County.ToString();
            if (!details[0].DOB.ToString().Equals(""))
            {
                string[] strDOB = details[0].DOB.ToString().Split('/');
                ddlday.SelectedValue = strDOB[0];
                ddlmonth.SelectedValue = strDOB[1];
                ddlyear.SelectedValue = strDOB[2];
            }
            RdMartialStatus.SelectedValue = details[0].MaritalStatus.ToString();
            if (RdMartialStatus.SelectedValue.ToString().Equals("Married"))
            {
                if (!details[0].AnniversaryDate.ToString().Equals(""))
                {
                    string[] strarr = details[0].AnniversaryDate.ToString().Split('/');
                    DdlDayAnniversary.SelectedValue = strarr[0];
                    DdlMonthAnniversary.SelectedValue = strarr[1];
                    DdlYearAnniversary.SelectedValue = strarr[2];
                }
            }
            txtDesignation.Text = details[0].Designation.ToString();

            RdGender.SelectedValue = details[0].Salutation.ToString();
            if (RdMartialStatus.SelectedValue.ToString().Equals("Married"))
            {
                trAnn.Visible = true;
            }
            else
            {
                trAnn.Visible = false;
            }

        }
        ServiceClient.Close();

    }


    void bindyear()
    {
        ddlyear.DataSource = BindYear18Years();
        ddlyear.DataBind();
        DdlYearAnniversary.DataSource = BindYear();
        DdlYearAnniversary.DataBind();
    }

    public ArrayList BindYear()
    {

        ArrayList arr = new ArrayList();
        for (int i = 1940; i <= DateTime.Now.Year; i++)
        {
            arr.Add(i);
        }
        return arr;
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
        String Dob = ddlmonth.SelectedValue.ToString() + "/" + ddlday.SelectedValue.ToString() + "/" + ddlyear.SelectedValue.ToString();
        String Doa = DdlMonthAnniversary.SelectedValue.ToString() + "/" + DdlDayAnniversary.SelectedValue.ToString() + "/" + DdlYearAnniversary.SelectedValue.ToString();
        DateTime dtdob = Convert.ToDateTime(Dob);
        DateTime dtdoa = Convert.ToDateTime(Doa);
        if (RdMartialStatus.SelectedValue.ToString().Equals("Married"))
        {
            dtdoa = Convert.ToDateTime(Doa);
        }
        else
        {
            dtdoa = System.DateTime.Now;
        }
        RoyalWebApp.EntityServiceReference.EntityServiceClient ServiceClient = new RoyalWebApp.EntityServiceReference.EntityServiceClient();
        ServiceClient.Open();
        int Confirm = ServiceClient.UpdateUserDetails(Session["MembershipID"].ToString(), RdGender.SelectedItem.Text.ToString(), txtFirstName.Text.ToString(), txtLastName.Text.ToString(), txtAddress.Text.ToString(), "", txtCity.Text.ToString(), DdlCountry.SelectedItem.Text.ToString(), txtMobileNo.Text.ToString(), txtEmailId.Text.ToString(), dtdob, dtdoa, RdMartialStatus.SelectedValue.ToString(), txtDesignation.Text.ToString(), txtMobileNo.Text.ToString());
        if (Confirm == 1)
        {
            Session["EmailId"] = txtEmailId.Text;
        }
        ServiceClient.Close();
        lblpasswrdgenerate.Visible = true;
        btnGeneratePassword.Visible = true;
    }

    protected void RdMartialStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RdMartialStatus.Items[1].Selected)
        {
            trAnn.Visible = true;
        }
        else
        {
            trAnn.Visible = false;
        }
    }

    protected void btnPassword_Click(object sender, EventArgs e)
    {
        RoyalWebApp.EntityServiceReference.EntityServiceClient ServiceRefClient = new RoyalWebApp.EntityServiceReference.EntityServiceClient();
        ServiceRefClient.Open();
        var arr = ServiceRefClient.ForgotPassword(Session["MembershipID"].ToString(), Session["EmailId"].ToString());
        if (arr.Length > 0)
        {
            //LblMsg.Text = arr[0].Password.ToString();
            emailid = Session["EmailId"].ToString();
            if (arr[0].Value.ToString().Equals("1"))// pwd exist already
            {

                if (arr[0].Password.ToString().Trim().Equals("") || arr[0].Password.ToString().Trim().Equals(null))// pwd exist already
                {
                    //generate pwd
                    //var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    //var random = new Random();
                    //var pwd = new string(
                    //    Enumerable.Repeat(chars, 8)
                    //              .Select(s => s[random.Next(s.Length)])
                    //              .ToArray());
                    Guid g = Guid.NewGuid();

                    string random = g.ToString();

                    var pwd = random.Substring(0, 8);
                    //update pwd in db
                    RoyalWebApp.EntityServiceReference.EntityServiceClient ServiceRefClient1 = new RoyalWebApp.EntityServiceReference.EntityServiceClient();
                    ServiceRefClient1.Open();
                    int cnfrm = ServiceRefClient1.ChangePassword(Session["MembershipID"].ToString(), arr[0].Password.ToString(), pwd.ToString());
                    ServiceRefClient1.Close();
                    Mailer(emailid.ToString(), pwd.ToString(), Session["MembershipID"].ToString());
                }
                else
                {
                    pwd = arr[0].Password.ToString();
                    HTMLMailer(emailid.ToString(), pwd.ToString(), Session["MembershipID"].ToString());
                }

                Response.Redirect("GeneratePassword.aspx");
            }
            else
            {
                LblMsg.Text = "Wrong Member Id or Email Id entered";
            }
        }
        ServiceRefClient.Close();
    }

    void Mailer(String FromMail, String pwd, String WebId)
    {

        RoyalMail.Mail objMail = new RoyalMail.Mail();
        RoyalMail.MailData objmaildata = new RoyalMail.MailData();
        objmaildata.from = "loyalty.programme@kingdomofdreams.co.in";
        objmaildata.fromName = "KOD ROYAL CARD TEAM";
        //objmaildata.to = "loyalty.programme@kingdomofdreams.co.in";
        objmaildata.to = FromMail.ToString();
        objmaildata.toName = "Royal Card First Time Login";
        objmaildata.subject = "Royal Card First Time Login";
        string BodyMaggage = "<div style='height:770px; width:727px; background:url(http://royalty.kingdomofdreams.in/Skins/images/Emailer2.jpg) no-repeat;'>"
            + "<div style='text-align:center; padding:260px 290px 50px 65px; font-family:Palatino Linotype, Century Gothic, Arial; font-weight:bold; font-size:14px;'>"
            + "<p> Email Id: " + FromMail.ToString() + "</p>"
            + "<p> New Password : " + pwd.ToString() + "</p>"
            + "<p>Regards</p><p>KOD ROYAL CARD TEAM</p>"
            + "</div></div>"
            ;
        objmaildata.bodyMessage = BodyMaggage;
        objMail.SendMailKOD(objmaildata);
    }
    void HTMLMailer(String FromMail, String pwd, String WebId)
    {
        RoyalMail.Mail objMail = new RoyalMail.Mail();
        RoyalMail.MailData objmaildata = new RoyalMail.MailData();
        objmaildata.from = "loyalty.programme@kingdomofdreams.co.in";
        objmaildata.fromName = "KOD ROYAL CARD TEAM";

        objmaildata.to = FromMail.ToString();
        objmaildata.toName = "Royal Card Forgot Password: Mail";
        objmaildata.subject = "Royal Card Forgot Password Mail";
        string BodyMaggage = "<div style='height:770px; width:727px; background:url(http://royalty.kingdomofdreams.in/Skins/images/Emailer2.jpg) no-repeat;'>"
            + "<div style='text-align:center; padding:260px 290px 50px 65px; font-family:Palatino Linotype, Century Gothic, Arial; font-weight:bold; font-size:14px;'>"
            + "<p> Your holiness :" + WebId.ToString() + ",</p>"
             + "<p>Your Password is : " + pwd.ToString() + "</p>"
            + "</div></div>"
            ;
        objmaildata.bodyMessage = BodyMaggage;
        objMail.SendMailKOD(objmaildata);

    }
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeading" runat="server">
    Thank You ! Your Membership has been successfully verified.
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageData" runat="server">
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
                
                if(<%Response.Write(txtEmailId.ClientID);%>.value=="")
                {
                alert("Please enter Email Address");
                <%Response.Write(txtEmailId.ClientID);%>.focus();
                return false;
                }
                var emailPat = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/; 
                var emailid = <%Response.Write(txtEmailId.ClientID);%>.value;
                var matchArray = emailid.match(emailPat);
                if (matchArray == null)
                  {
                     alert("Please Enter Valid Email ID");
                     <%Response.Write(txtEmailId.ClientID);%>.focus();
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
                  if((<%Response.Write(txtMobileNo.ClientID);%>.value).length>10)
                {
                alert("Invalid Mobile Number");
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
        <table width="87%" cellpadding="1" cellspacing="1" class="divborder">
            <tr>
                <td valign="top" align="left" class="divtext" colspan="4">
                    <strong>Plese verify & update your membership details below:</strong>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" colspan="4">
                    <center>
                        <asp:Label ID="lblpasswrdgenerate" runat="server" Text="" Visible="false" ForeColor="Red">Thank you for updating your information. Click on button to generate a password for your account</asp:Label></center>
                </td>
            </tr>
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
                    <asp:Label ID="LblMsg" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext">
                    First Name: *
                </td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="inputbg" Text="" />
                </td>
                <td valign="top" align="left" class="divtext">
                    Last Name: *
                </td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="inputbg" Text="" />
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext">
                    Address:
                </td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="inputbg"
                        Height="30px" Text="" />
                </td>
                <td valign="top" align="left" class="divtext">
                    City:
                </td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtCity" runat="server" CssClass="inputbg" />
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext">
                    Country:
                </td>
                <td valign="top" align="left">
                    <asp:DropDownList ID="DdlCountry" runat="server" CssClass="inputbg">
                        <asp:ListItem Value="India" Text="India"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td valign="top" align="left" class="divtext">
                    Email ID: *
                </td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtEmailId" runat="server" CssClass="inputbg" Text="" />
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext">
                    Mobile No: *
                </td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtMobileNo" runat="server" CssClass="inputbg" Text="" />
                </td>
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
                        <asp:ListItem Value="02" Text="Feb"></asp:ListItem>
                        <asp:ListItem Value="03" Text="Mar"></asp:ListItem>
                        <asp:ListItem Value="04" Text="Apr"></asp:ListItem>
                        <asp:ListItem Value="05" Text="May"></asp:ListItem>
                        <asp:ListItem Value="06" Text="Jun"></asp:ListItem>
                        <asp:ListItem Value="07" Text="Jul"></asp:ListItem>
                        <asp:ListItem Value="08" Text="Aug"></asp:ListItem>
                        <asp:ListItem Value="09" Text="Sep"></asp:ListItem>
                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlyear" runat="server" CssClass="inputdropdown">
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
                <%--<td valign="top" align="left" class="divtext">
                    Organization:</td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="inputbg" Text="Kranti Tech Services Pvt. Ltd." />
                </td>--%>
                <td valign="top" align="left" class="divtext">
                    Designation:
                </td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtDesignation" runat="server" CssClass="inputbg" Text="" />
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext">
                    Marital Status:*
                </td>
                <td valign="top" align="left" colspan="2">
                    <b>
                        <asp:RadioButtonList ID="RdMartialStatus" runat="server" RepeatDirection="Horizontal"
                            class="divtext" AutoPostBack="True" OnSelectedIndexChanged="RdMartialStatus_SelectedIndexChanged">
                            <asp:ListItem Value="Single" Text="Single" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="Married" Text="Married"></asp:ListItem>
                            <asp:ListItem Value="Prefer Not to Say" Text="Prefer Not to Say"></asp:ListItem>
                        </asp:RadioButtonList>
                        <%--onselectedindexchanged="RdMartialStatus_SelectedIndexChanged"--%>
                    </b>
                </td>
                <td valign="top" align="left" id="trAnn" runat="server" visible="false">
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
                        <asp:ListItem Value="02" Text="Feb"></asp:ListItem>
                        <asp:ListItem Value="03" Text="Mar"></asp:ListItem>
                        <asp:ListItem Value="04" Text="Apr"></asp:ListItem>
                        <asp:ListItem Value="05" Text="May"></asp:ListItem>
                        <asp:ListItem Value="06" Text="Jun"></asp:ListItem>
                        <asp:ListItem Value="07" Text="Jul"></asp:ListItem>
                        <asp:ListItem Value="08" Text="Aug"></asp:ListItem>
                        <asp:ListItem Value="09" Text="Sep"></asp:ListItem>
                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="DdlYearAnniversary" runat="server" CssClass="inputdropdown">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext">
                    &nbsp;
                </td>
                <td valign="top" align="center">
                    <div class="button" style="float: right;">
                        <a href="#">
                            <asp:LinkButton ID="BtnSubmit" runat="server" OnClientClick="javascript:return validUpdate();"
                                Width="45px" Text="Update" OnClick="BtnSubmit_Click"></asp:LinkButton>
                        </a><span></span>
                    </div>
                </td>
                <td valign="top" align="center">
                    <div class="button" style="float: left;">
                        <a href="FirstTimeLogin.aspx">Cancel </a><span></span>
                    </div>
                </td>
                <td>
                    <div class="button" runat="server" id="btnGeneratePassword" style="float: right;">
                        <a href="#">
                            <asp:LinkButton runat="server" ID="btnPassword" OnClientClick="javascript:return validUpdate();"
                                Text="Generate Password" OnClick="btnPassword_Click"></asp:LinkButton>
                        </a><span></span>
                    </div>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
