<%@ Page Title="" Language="C#" MasterPageFile="~/Skins/Master/AccountMaster.Master" AutoEventWireup="true" CodeBehind="TicketBookingPage.aspx.cs" Inherits="ROYALCARD.TicketBookingPage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">
protected void BtnBookTickets_Click(object sender, EventArgs e)
        {
            Response.Redirect("SeatLayout.aspx");
        }

protected void Page_Load(object sender, EventArgs e)
{
    
        lblRoyalcardBalance.Text = Request.QueryString["RemainingAmount"];
        lblRoyalcardPoints.Text = Request.QueryString["RemainingPoints"];
        //lblTotalpayableAmount.Text = "1500";
   
}
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 158px;
        }
        .style2
        {
            width: 138px;
        }
        .style3
        {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #3f260a;
            text-decoration: none;
            margin-left: 14px;
            width: 138px;
        }
        .text-small
{
    font: 12px verdana;
    color: #000000;
}
.clickhere
{
    font: 12px Verdana;
    color: #ECA035;
}
.ModalWindow
        {
            background-color: #eed075;
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
        .buttontickets{
	float: none;
	background-color:#eed075;
	border-color:Orange;
	color: #231f20;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeading" runat="server">
    BooK Tickets
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageData" runat="server">

<table style="width: 530px">


<asp:UpdateProgress ID="uppero" runat="server" DynamicLayout="true">
                        <ProgressTemplate>
                            <div style="height: 300px; background-color: #C79EA7; width: 280px; position: absolute;
                                top: 0px; left: 0px; border-left: 4px solid #A67782; border-right: 4px solid #A67782;
                                color: #FFC419; filter: alpha(opacity=80); opacity: 0.8; text-align: center;
                                z-index: 100">
                                <div style="font-family: Verdana; font-size: 12px; color: Black; font-weight: bold;
                                    margin-top: 95px">
                                    Loading Please Wait...
                                </div>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <tr>
                      <td class="style3">
                            <b>Play</b>
                        </td>
                        <td style="width: 13px">
                            :
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddl_Play" Width="160px" runat="server" 
                                 AutoPostBack="true"  CssClass="text-small">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="show"
                                ControlToValidate="ddl_Play" Display="None" ErrorMessage="Play" InitialValue="0"
                                ForeColor="#ECA035" />
                        </td>
</tr>

<tr>
                        <td class="style2">
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_Location" runat="server" CssClass="text-small" Width="160px"
                                AutoPostBack="True">
                            </asp:DropDownList><br>
                            <asp:RequiredFieldValidator ID="req1" Display="None" ErrorMessage="Location" runat="server"
                                ValidationGroup="show" ControlToValidate="ddl_Location" InitialValue="0" ForeColor="#ECA035" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <b>Date</b>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_Date" runat="server" Width="160px" CssClass="text-small"
                                 AutoPostBack="true">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="show"
                                ControlToValidate="ddl_Date" Display="None" ErrorMessage="Date" InitialValue="0"
                                ForeColor="#ECA035" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <b>Show time</b>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_ShowTimes" Width="160px" runat="server" CssClass="text-small"
                                AutoPostBack="true">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="show"
                                ControlToValidate="ddl_ShowTimes" Display="None" ErrorMessage="Show time" InitialValue="0"
                                ForeColor="#ECA035" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <strong>View Seating Layout</strong>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <a id="A1" href="javascript:;" runat="server" onclick="ChangeImage()">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Skins/images/map.jpg" />
                            </a>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <b>Category</b>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_Category" runat="server" CssClass="text-small" Width="160px"
                                AutoPostBack="True" >
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="show"
                                ControlToValidate="ddl_Category" Display="None" ErrorMessage="Category" InitialValue="0"
                                ForeColor="#ECA035" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <b>Total Seats</b>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList ID="drp_TotalSeats" runat="server" Height="20px" Width="160px"
                                CssClass="text-small">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="show"
                                ControlToValidate="drp_TotalSeats" Display="None" ErrorMessage="Total Seats"
                                InitialValue="0" ForeColor="#ECA035" />
                        </td>
                    </tr>
<tr>
<td class="style3">
Total Ticket Amount (Rs.)
</td>
<td>
:
</td>
<td class="style3">
    <asp:Label ID="lbltotalAmount" runat="server" Text="1500"></asp:Label>
</td>
</tr>
<tr class="style3">
<td>
Your Royal Card Balance (Rs.)
</td>
<td>:</td>
<td class="style3">
 <asp:Label ID="lblRoyalcardBalance" runat="server" Text="0"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
 <asp:TextBox ID="txtRoyalcardBalance" runat="server" Width="55px" 
        CssClass="text-small" OnBlur="javascript:validateBalance(this.value);" OnFocus="javascript:RBal();">0</asp:TextBox>
</td>
<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
<td class="style3">
     <asp:CheckBox ID="chkbxRoyalcardBalance" runat="server" OnClick="chkbxRoyalcardBalance(this)" Text="use this for purchase" />
 </td>
</tr>
 <tr>
 <td class="style3">
 Your Royal Card Points
 </td>
 <td>:</td>
 <td class="style3">
 <asp:Label ID="lblRoyalcardPoints" runat="server" Text="0"></asp:Label>&nbsp;&nbsp;&nbsp; &nbsp;
 <asp:TextBox ID="txtRoyalcardPoints" runat="server" Width="55px"  
         CssClass="text-small" OnBlur="javascript:validatePoints(this.value);" OnFocus="javascript:RPoints();">0</asp:TextBox>
 </td>
 <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
 
 <td class="style3">
     <asp:CheckBox ID="chkbxRoyalcardPoints" runat="server" OnClick = "chkbxRoyalcardBalance(this)" Text="use this for purchase" />
 </td>
 </tr>   
 <tr>
 <td class="style3">
 Total Payable Amount (Rs.)
 </td>
 <td>:</td>
 <td class="style3">
<asp:Label ID="lblTotalpayableAmount" runat="server" Text="0"></asp:Label>
 </td>
 </tr>                
</table>
    <asp:CheckBox runat="server" ID="chkterms" />
    <asp:CustomValidator ID="chkvalidato" Display="None" runat="server" EnableClientScript="true"
        ErrorMessage="accept terms and conditions" ClientValidationFunction="terms_Conditions"
        ValidationGroup="show"></asp:CustomValidator>
    I Agree to the Terms and Conditions, to read &nbsp;
    <asp:LinkButton ID="Button1" CssClass="clickhere" runat="server" Text="Click Here" /><br />
    
    <div style="display: none">
        <asp:LinkButton CssClass="clickhere" ID="lnbShowCast" runat="server" Text="Click here"></asp:LinkButton>&nbsp;for
        the Show Cast
        <br />
        <br />
    </div>   
       <div class="button" style="float:right;">
                    <asp:LinkButton ID="BtnBookTickets" runat="server" 
                        OnClientClick="javascript:return validateAmount();" Width="97px" 
                        Text="Select Seats" onclick="BtnBookTickets_Click"></asp:LinkButton>
              <span></span>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
        ShowSummary="false" HeaderText="Below fields are missing.." ValidationGroup="show" />
    <cc1:ModalPopupExtender ID="mo12" PopupControlID="dv_pop" BackgroundCssClass="modalBackground2"
        CancelControlID="btnClose" TargetControlID="Button1" runat="server">
    </cc1:ModalPopupExtender>
    <div id="dv_pop" class="ModalWindow" style="display: none; width: 550px; height: 500px;"
        runat="server">
        <div style="overflow: auto; width: 530px; height: 460px; padding: 0px 10px 0px 10px">
            <h3>
                HEIGHT POLICY</h3>
            <b><font color="red"><p>
                * Children below the height of 33 inches (2.9 ft) are not allowed inside the theatre.</p></font></b>
            <h3>
                CANCELLATION/REFUND POLICY</h3>
            <b><font color="red"><p>
                * As per policy we do not cancel/refund/change any tickets once booked.</p></font></b>
            <p>
                * Performances and Performers of the theater programme are subject to change without
                prior notice.
            </p>
            <p>
                Kingdom of Dreams has the right to make alterations to the advertised theatre programme
                or cast & crew in the event of an illness or unavoidable cause, without prior notice.
            </p>
            <h3>
                Ticket Terms and Conditions</h3>
            <ol>
                <li>Any pricing errors or omissions will be corrected on our systems and the corrected
                    price will be charged to your order (including those orders already submitted and
                    accepted). </li>
                <li>The right of admission is reserved by the management. Latecomers will not be able
                    to join the performance and will only be admitted to a performance during a suitable
                    break which could be the first interval (if an interval is scheduled for the applicable
                    performance). </li>
                <li>Replacement of lost, stolen or missing tickets may be permitted under certain conditions
                    and fees may apply. Please contact us for more information at: 0124 - 4528000</li>
                <li>Any bag or item larger than A4 must be cloaked and may be inspected. </li>
                <li>Camera, video and audio recorders may not be used inside Kingdom of Dreams, unless
                    expressly authorized. Kingdom of Dreams reserves the right to broadcast or telecast
                    any event. .</li>
                <li>Ticket holders enter the venue at their own risk. Kingdom of Dreams will not be
                    responsible for any loss, damage or injury arising from a pre-existing medical condition
                    or due to a breach of these conditions. </li>
                <li>This ticket may not, without the prior written consent of Kingdom of Dreams, be
                    resold for commercial packages or at a premium, nor May it be packaged or used for
                    advertising, promotional or other commercial purposes. If a ticket is sold in breach
                    of this condition, the bearer of the ticket may be refused admission. </li>
            </ol>
            <h3>
                HUDA RESIDENTS SCHEME</h3>
            <p>
                Huda residents are entitled to get 25% discount on show tickets of Zangoora (subject
                to booking done 7 days prior to the show only from the box office counter). They
                also have to bring the Allotment Letter from HUDA and it has to be in the name of
                the person who has come to buy the tickets at the counter along with his ID Proof.
                Only 4pax allowed against 1 allotment letter and for all 4 members id proof&#8217;s are
                required. No internet booking is entitled for discounts. Ticket holders must carry
                their proof of residence, photo ID proof for verification at time of entry. Documents:
                Electric Bill / Telephone Bill / Ration Card / PAN Card / Passport / Driving License
                / Photo ID / Voter ID card and for Kids above the required age in the policy, school
                ID card will be required.
            </p>
            <h3>
                CANCELLATION AND POSTPONE POLICY
            </h3>
            <p>
                Occasionally, performances are cancelled or postponed. Should this occur, we will
                attempt to contact you to inform you of refund or exchange procedures for the scheduled
                performances / event. For exact instructions on any cancelled or postponed performance,
                please contact us at 0124 &#8211; 4528000</p>
            <h3>
                PAYMENT METHODS</h3>
            <p>
                Kingdom of Dream accepts several methods of payment to accommodate your needs. Kingdomofdreams.co.in
                accepts all the major credit cards to facilitate the same. Tickets are also available
                at the box office or can be availed at call centre at: 0124 - 4528000 Located at
                Kingdom of Dreams.</p>
            <h3>
                CURRENCY</h3>
            <p>
                All ticket prices for events that occur in India are stated in Rupees. All the payments
                are accepted at the box office and online in Rupees.</p>
            <h3>
                PRIVACY POLICY</h3>
            <p>
                The Great Indian Nautanki company Private Limited recognises the importance of protecting
                your privacy. We are committed to ensuring the continued integrity and security
                of the personal information you entrust to us.</p>
            <p>
                We appreciate that the success of our business is largely dependent upon a relationship
                of trust being established and maintained with past, current and prospective customers
                with whom we conduct business. We will therefore continue to collect and manage
                your personal information with a high degree of diligence and care.</p>
            <h3>
                STORAGE AND SECURITY OF YOUR PERSONAL INFORMATION</h3>
            <p>
                We will take reasonable steps to keep the personal information that we hold about
                you secure to ensure that it is protected from loss, unauthorised access, use, modification
                or disclosure.</p>
            <p>
                Your personal information is stored within secure systems that are protected in
                controlled facilities. Our employees and authorised agents are obliged to respect
                the confidentiality of any personal information held by us.</p>
            <h3>
                OUR WEBSITE</h3>
            <p>
                We use our best efforts to ensure that information received via our websites remains
                secured within our systems. We are regularly reviewing developments in online security,
                however users should be aware that there are inherent risks in transmitting information
                across the internet. Information transmitted via our websites is protected by a
                encryption technology.</p>
            <p>
                The information is used in an aggregate form and generally no personal information
                is collected by the third party service provider.
            </p>
            <h3>
                NOTE</h3>
            <p>
                We as a merchant shall be under no liability whatsoever in respect of any loss or
                damage arising directly or indirectly out of the decline of authorization for any
                Transaction, on Account of the Cardholder having exceeded the preset limit mutually
                agreed by us with our acquiring bank from time to time
            </p>
        </div>
        <hr /> 
        <asp:Button Text="Close" runat="server" ID="btnClose" CssClass="buttontickets"  />  
    </div>

    <script type="text/javascript">
        function ChangeImage() {
            var DropdownList = document.getElementById('<%=ddl_Play.ClientID %>');
            var SelectedText = DropdownList.options[DropdownList.selectedIndex].text;
            var ImageName1;
            if (SelectedText == "ZANGOORA") //ZANGOORA
            {
                ImageName1 = "/Skins/images/zangoora-seat-layout.jpg";
            }
            else if (SelectedText == "JHUMROO") //JHUMROO
            {
                ImageName1 = "/Skins/images/jhumroo-seat-layout.jpg";
            }
            else if (SelectedText == "CONCERT") {
                ImageName1 = "/Skins/images/kailash-kher-seat-layout.jpg";
            }
            else {
                ImageName1 = "/Skins/images/booking-engine-seat-layout.jpg";
            }

            window.open(ImageName1, '_blank', 'tools=0,status=0,history=0,width=867,height=620,top=50,left=50');
        }

        function terms_Conditions(oSrc, args) {
            args.IsValid = document.getElementById("<%=chkterms.ClientID %>").checked;
        }
    </script>
    <script type="text/javascript">
        window.onload = function () {
            var TotalPayment = document.getElementById("<%=lblTotalpayableAmount.ClientID %>");
            var TotalBalAmount = document.getElementById("<%=lbltotalAmount.ClientID %>");
            TotalPayment.firstChild.data = TotalBalAmount.firstChild.data;
            document.getElementById("<%=txtRoyalcardBalance.ClientID %>").disabled = true;
            document.getElementById("<%=txtRoyalcardPoints.ClientID %>").disabled = true;
        }
        function RBal() {
            var lblRoyalcardBalance = document.getElementById("<%=lblRoyalcardBalance.ClientID %>");
            var txtTotalRCBal = document.getElementById("<%=txtRoyalcardBalance.ClientID %>");
            var lblTotalpayableAmount = document.getElementById("<%=lblTotalpayableAmount.ClientID %>");
            var TotalPayment = document.getElementById("<%=lblTotalpayableAmount.ClientID %>");
            var a = Math.floor(lblRoyalcardBalance.firstChild.data);
            var b = Math.floor(txtTotalRCBal.value);
            var c = Math.floor(TotalPayment.firstChild.data);
            lblRoyalcardBalance.firstChild.data = b + a;
            TotalPayment.firstChild.data = c + b;
            txtTotalRCBal.value = "0";
        }
        function validateBalance(value) {
            var chkboxRoyalcard = document.getElementById("<%=chkbxRoyalcardBalance.ClientID %>");
            var lblRoyalcardBalance = document.getElementById("<%=lblRoyalcardBalance.ClientID %>");
            var txtTotalRCBal = document.getElementById("<%=txtRoyalcardBalance.ClientID %>");
            
            var chkboxRoyalPoints = document.getElementById("<%=chkbxRoyalcardPoints.ClientID %>");

            var lblTotalpayableAmount = document.getElementById("<%=lblTotalpayableAmount.ClientID %>");
            var TotalPayment = document.getElementById("<%=lblTotalpayableAmount.ClientID %>");
            var b = Math.floor(txtTotalRCBal.value);

            if (lblRoyalcardBalance.firstChild.data < b) {
                alert("Amount can not be less than Zero");
                document.getElementById("<%=txtRoyalcardBalance.ClientID %>").value = "0";
                document.getElementById("<%=chkbxRoyalcardBalance.ClientID %>").checked = false;
                document.getElementById("<%=txtRoyalcardBalance.ClientID %>").disabled = true;
            }
            else {
                if (chkboxRoyalcard.checked && chkboxRoyalPoints.checked) {
                    document.getElementById("<%=txtRoyalcardBalance.ClientID %>").disabled = false;
                    lblRoyalcardBalance.firstChild.data = lblRoyalcardBalance.firstChild.data - txtTotalRCBal.value; 
                    lblTotalpayableAmount.firstChild.data = TotalPayment.firstChild.data - b;

                }
                else {
                    if (chkboxRoyalcard.checked) {
                        document.getElementById("<%=txtRoyalcardBalance.ClientID %>").disabled = false;
                        lblRoyalcardBalance.firstChild.data = lblRoyalcardBalance.firstChild.data - txtTotalRCBal.value;
                        lblTotalpayableAmount.firstChild.data = TotalPayment.firstChild.data - txtTotalRCBal.value;

                    }
                    if (!chkboxRoyalcard.checked) {
                        document.getElementById("<%=txtRoyalcardBalance.ClientID %>").disabled = true;

                    }
                }
            }
        }
        function RPoints() {
            var lblRoyalcardPoints = document.getElementById("<%=lblRoyalcardPoints.ClientID %>");
            var txtTotalRCPoints = document.getElementById("<%=txtRoyalcardPoints.ClientID %>");
            var lblTotalpayableAmount = document.getElementById("<%=lblTotalpayableAmount.ClientID %>");
            var TotalPayment = document.getElementById("<%=lblTotalpayableAmount.ClientID %>");
            var c = Math.floor(TotalPayment.firstChild.data);
            var d = Math.floor(lblRoyalcardPoints.firstChild.data);
            var f = Math.floor(txtTotalRCPoints.value);
            lblRoyalcardPoints.firstChild.data = d + f;
            TotalPayment.firstChild.data = c + f;
            txtTotalRCPoints.value = "0";
        }
        function validatePoints(value) {
            var chkboxRoyalcard = document.getElementById("<%=chkbxRoyalcardBalance.ClientID %>");

            var chkboxRoyalPoints = document.getElementById("<%=chkbxRoyalcardPoints.ClientID %>");
            var lblRoyalcardPoints = document.getElementById("<%=lblRoyalcardPoints.ClientID %>");
            var txtTotalRCPoints = document.getElementById("<%=txtRoyalcardPoints.ClientID %>");

            var lblTotalpayableAmount = document.getElementById("<%=lblTotalpayableAmount.ClientID %>");
            var TotalPayment = document.getElementById("<%=lblTotalpayableAmount.ClientID %>");
            var a = Math.floor(txtTotalRCPoints.value);

            if (lblRoyalcardPoints.firstChild.data < a) {
                alert("Amount can not be less than Zero");
                document.getElementById("<%=txtRoyalcardPoints.ClientID %>").value = "0";
                document.getElementById("<%=chkbxRoyalcardPoints.ClientID %>").checked = false;
                document.getElementById("<%=txtRoyalcardPoints.ClientID %>").disabled = true;
            }
            else {
                if (chkboxRoyalcard.checked && chkboxRoyalPoints.checked) {
                    document.getElementById("<%=txtRoyalcardPoints.ClientID %>").disabled = false;
                    lblRoyalcardPoints.firstChild.data = lblRoyalcardPoints.firstChild.data - txtTotalRCPoints.value;
                    lblTotalpayableAmount.firstChild.data = TotalPayment.firstChild.data - a;

                }
                else {
                    if (chkboxRoyalPoints.checked) {
                        document.getElementById("<%=txtRoyalcardPoints.ClientID %>").disabled = false;
                        lblRoyalcardPoints.firstChild.data = lblRoyalcardPoints.firstChild.data - txtTotalRCPoints.value;
                        lblTotalpayableAmount.firstChild.data = TotalPayment.firstChild.data - txtTotalRCPoints.value;
                    }
                    if (!chkboxRoyalPoints.checked) {
                        document.getElementById("<%=txtRoyalcardPoints.ClientID %>").disabled = true;

                    }
                }
            }
        }

        function chkbxRoyalcardBalance(checkbox) {
            var lblTotalpayableAmount = document.getElementById("<%=lblTotalpayableAmount.ClientID %>");
            var TotalPayment = document.getElementById("<%=lblTotalpayableAmount.ClientID %>");

            var chkboxRoyalcard = document.getElementById("<%=chkbxRoyalcardBalance.ClientID %>");
            var lblRoyalcardBalance = document.getElementById("<%=lblRoyalcardBalance.ClientID %>");
            var txtTotalRCBal = document.getElementById("<%=txtRoyalcardBalance.ClientID %>");
            var a = Math.floor(lblRoyalcardBalance.firstChild.data);
            var b = Math.floor(txtTotalRCBal.value);
            var c = Math.floor(TotalPayment.firstChild.data);

            var chkboxRoyalPoints = document.getElementById("<%=chkbxRoyalcardPoints.ClientID %>");
            var lblRoyalcardPoints = document.getElementById("<%=lblRoyalcardPoints.ClientID %>");
            var txtTotalRCPoints = document.getElementById("<%=txtRoyalcardPoints.ClientID %>");
            var d = Math.floor(lblRoyalcardPoints.firstChild.data);
            var f = Math.floor(txtTotalRCPoints.value);
                if (chkboxRoyalcard.checked) {
                    document.getElementById("<%=txtRoyalcardBalance.ClientID %>").disabled = false;
                }
                if (!chkboxRoyalcard.checked) {
                    document.getElementById("<%=txtRoyalcardBalance.ClientID %>").disabled = true;
                    lblRoyalcardBalance.firstChild.data = a + b;
                    lblTotalpayableAmount.firstChild.data = c + b;
                    document.getElementById("<%=txtRoyalcardBalance.ClientID %>").value = "0";
                }
                if (chkboxRoyalPoints.checked) {
                    document.getElementById("<%=txtRoyalcardPoints.ClientID %>").disabled = false;
                }
                if (!chkboxRoyalPoints.checked) {
                    document.getElementById("<%=txtRoyalcardPoints.ClientID %>").disabled = true;
                    lblRoyalcardPoints.firstChild.data = d + f;
                    lblTotalpayableAmount.firstChild.data = Math.floor(TotalPayment.firstChild.data) + Math.floor(txtTotalRCPoints.value);
                    document.getElementById("<%=txtRoyalcardPoints.ClientID %>").value = "0";
                }
        }
    </script>
     <script type="text/javascript">
         function validateAmount() {     
             var chkboxRoyalcard = document.getElementById("<%=chkbxRoyalcardBalance.ClientID %>");
             var chkboxRoyalPoints = document.getElementById("<%=chkbxRoyalcardPoints.ClientID %>");
             var oDDL = document.getElementById('<%=lblTotalpayableAmount.ClientID %>');
             var curText = oDDL.firstChild.data;
             if (curText > 0 && chkboxRoyalcard.checked && chkboxRoyalPoints.checked) {
                 alert("Your cumulative amount & point balance is short of the total purchase value of the tickets. To purchse the tickets, the balance amount shall be payable by debit/credit card.");
             }
             else if (curText > 0 && chkboxRoyalcard.checked) {
                 alert("Your cumulative amount & point balance is short of the total purchase value of the tickets. To purchse the tickets, the balance amount shall be payable by debit/credit card.");
             }
             else if (curText > 0 && chkboxRoyalPoints.checked) {
                 alert("Your cumulative amount & point balance is short of the total purchase value of the tickets. To purchse the tickets, the balance amount shall be payable by debit/credit card.");
             }
             else if (curText > 0 && !chkboxRoyalcard.checked && !chkboxRoyalPoints.checked) {
                 alert("Your cumulative amount & point balance is short of the total purchase value of the tickets. To purchse the tickets, the balance amount shall be payable by debit/credit card.");
             }
             else if (curText < 0) {
                 alert("Balance can't be negative");
                 return false;
             }
         }
    </script>
</asp:Content>
