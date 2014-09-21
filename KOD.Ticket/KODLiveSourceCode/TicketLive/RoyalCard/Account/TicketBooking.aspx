<%@ Page Title="" Language="C#" MasterPageFile="~/RoyalCard/Skins/Master/AccountMaster.Master"
    AutoEventWireup="true" CodeFile="TicketBooking.aspx.cs" Inherits="Royal_Card_Account_TicketBooking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 158px;
        }
        .style3
        {
            font-family: "Trebuchet MS" , Arial, Helvetica, sans-serif;
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
        .buttontickets
        {
            float: none;
            background-color: #eed075;
            border-color: Orange;
            color: #231f20;
        }
        .style6
        {
            width: 172px;
        }
        .style7
        {
            font-family: "Trebuchet MS" , Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #3f260a;
            text-decoration: none;
            margin-left: 14px;
            width: 172px;
        }
        .style8
        {
            width: 242px;
        }
        .style9
        {
            font-family: "Trebuchet MS" , Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #3f260a;
            text-decoration: none;
            margin-left: 14px;
            width: 242px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeading" runat="Server">
    BooK Tickets
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageData" runat="Server">
    <script language="javascript" type="text/javascript" src="../../js/jquery-1.8.2.min.js"></script>
    <script language="javascript" type="text/javascript">
        var boolRe;
        //$("[id*=ddl_Date]")
        
        
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 524px">
                <asp:UpdateProgress ID="uppero" runat="server" DynamicLayout="true">
                    <ProgressTemplate>
                        <div style="height: 340px; background-color: #ECA035; width: 380px; position: absolute;
                            top: 300px; left: 580px; border-left: 4px solid #A67782; border-right: 4px solid #A67782;
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
                    <td class="style7">
                        <b>Play</b>
                    </td>
                    <td style="width: 13px">
                        :
                    </td>
                    <td class="style1">
                        <asp:DropDownList ID="ddl_Play" Width="160px" runat="server" AutoPostBack="true"
                            CssClass="text-small" OnSelectedIndexChanged="ddl_Play_SelectedIndexChanged">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="show"
                            ControlToValidate="ddl_Play" Display="None" ErrorMessage="Play" InitialValue="0"
                            ForeColor="#ECA035" />
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_Location" runat="server" CssClass="text-small" Width="160px"
                            AutoPostBack="True" OnSelectedIndexChanged="ddl_Location_SelectedIndexChanged">
                        </asp:DropDownList>
                        <br>
                        <asp:RequiredFieldValidator ID="req1" Display="None" ErrorMessage="Location" runat="server"
                            ValidationGroup="show" ControlToValidate="ddl_Location" InitialValue="0" ForeColor="#ECA035" />
                    </td>
                </tr>
                <tr>
                    <td class="style7">
                        <b>Date</b>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_Date" runat="server" Width="160px" CssClass="text-small"
                            AutoPostBack="true" onchange="javascript:validateBookingShow();" OnSelectedIndexChanged="ddl_Date_SelectedIndexChanged">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="show"
                            ControlToValidate="ddl_Date" Display="None" ErrorMessage="Date" InitialValue="0"
                            ForeColor="#ECA035" />
                    </td>
                </tr>
                <tr>
                    <td class="style7">
                        <b>Show time</b>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_ShowTimes" Width="160px" runat="server" CssClass="text-small"
                            AutoPostBack="true" OnSelectedIndexChanged="ddl_ShowTimes_SelectedIndexChanged">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="show"
                            ControlToValidate="ddl_ShowTimes" Display="None" ErrorMessage="Show time" InitialValue="0"
                            ForeColor="#ECA035" />
                    </td>
                </tr>
                <tr>
                    <td class="style7">
                        <strong>View Seating Layout</strong>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <a id="A1" href="javascript:;" runat="server" onclick="ChangeImage()">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/map.jpg" />
                        </a>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="style7">
                        <b>Category</b>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_Category" runat="server" CssClass="text-small" Width="160px"
                            AutoPostBack="True" OnSelectedIndexChanged="ddl_Category_SelectedIndexChanged">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="show"
                            ControlToValidate="ddl_Category" Display="None" ErrorMessage="Category" InitialValue="0"
                            ForeColor="#ECA035" />
                    </td>
                </tr>
                <tr>
                    <td class="style7">
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
                    <td class="style7">
                        Total Ticket Amount (Rs.)
                    </td>
                    <td>
                        :
                    </td>
                    <td class="style3">
                        <asp:Label ID="lbltotalAmount" runat="server" Text="0"></asp:Label>
                    </td>
                </tr>
                <tr class="style3">
                    <td class="style6">
                        Your RoyalCard Balance (Rs.)
                    </td>
                    <td>
                        :
                    </td>
                    <td class="style3">
                        <div style="width: 70px; float: left">
                            <asp:Label ID="lblRoyalcardBalance" runat="server" Text="0"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</div>
                        <div>
                            <asp:TextBox ID="txtRoyalcardBalance" runat="server" Width="55px" CssClass="text-small"
                                OnBlur="javascript:validateBalance(this.value);" OnFocus="javascript:RBal();"
                                Enabled="False" Text="0"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender"  FilterType="Numbers"   TargetControlID="txtRoyalcardBalance"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
                                </div>
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td class="style3">
                        <asp:CheckBox ID="chkbxRoyalcardBalance" runat="server" OnClick="chkbxRoyalcardBalance(this)"
                            Text="use this for purchase" />
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        Your RoyalCard Points
                    </td>
                    <td>
                        :
                    </td>
                    <td class="style3">
                        <div style="width: 70px; float: left">
                            <asp:Label ID="lblRoyalcardPoints" runat="server" Text="0 "></asp:Label></div>
                        <div>
                            <asp:TextBox ID="txtRoyalcardPoints" runat="server" Width="55px" CssClass="text-small"
                                OnBlur="javascript:validatePoints(this.value);" OnFocus="javascript:RPoints();"
                                Enabled="False">0</asp:TextBox>
                                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"  FilterType="Numbers"   TargetControlID="txtRoyalcardPoints"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
                                
                                </div>
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td class="style3">
                        <asp:CheckBox ID="chkbxRoyalcardPoints" runat="server" OnClick="chkbxRoyalcardBalance(this)"
                            Text="use this for purchase" />
                    </td>
                </tr>
                <tr style="height: 50px; vertical-align: top">
                    <td class="style3">
                        Total Payable Amount (Rs.)
                    </td>
                    <td>
                        :
                    </td>
                    <td class="style3">
                        <div style="float: left;">
                            <asp:Label ID="lblTotalpayableAmount" runat="server" Text="0"></asp:Label></div>
                        &nbsp;&nbsp;&nbsp;
                        <div style="width: 300px; position: absolute; padding-bottom: 5px; display: none;
                            color: red" id="divAlertMessage">
                            Royality Points for this payment shall automatically be added to your royal card
                            after 24 hrs
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnRoyalCardSubmit" />
        </Triggers>
    </asp:UpdatePanel>
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
    <div class="button" style="float: right;">
        <asp:LinkButton ID="btnRoyalCardSubmit" runat="server" Width="97px" Text="Select Seats"
            OnClick="btn_Submit_Click" ValidationGroup="show"></asp:LinkButton>
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
            <b><font color="red">
                <p>
                    * Children below the height of 33 inches (2.9 ft) are not allowed inside the theatre.</p>
            </font></b>
            <h3>
                CANCELLATION/REFUND POLICY</h3>
            <b><font color="red">
                <p>
                    * As per policy we do not cancel/refund/change any tickets once booked.</p>
            </font></b>
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
                Only 4pax allowed against 1 allotment letter and for all 4 members id proof&#8217;s
                are required. No internet booking is entitled for discounts. Ticket holders must
                carry their proof of residence, photo ID proof for verification at time of entry.
                Documents: Electric Bill / Telephone Bill / Ration Card / PAN Card / Passport /
                Driving License / Photo ID / Voter ID card and for Kids above the required age in
                the policy, school ID card will be required.
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
        <asp:Button Text="Close" runat="server" ID="btnClose" CssClass="buttontickets" />
    </div>
    <script type="text/javascript">
        function ChangeImage() {
            var DropdownList = document.getElementById('<%=ddl_Play.ClientID %>');
            var SelectedText = DropdownList.options[DropdownList.selectedIndex].text;
            var ImageName1;
            if (SelectedText == "ZANGOORA") //ZANGOORA
            {
                ImageName1 = "../../images/zangoora-seat-layout.jpg";
            }
            else if (SelectedText == "JHUMROO") //JHUMROO
            {
                ImageName1 = "../../images/jhumroo-seat-layout.jpg";
            }
            else if (SelectedText == "CONCERT") {
                ImageName1 = "../../images/kailash-kher-seat-layout.jpg";
            }
            else {
                ImageName1 = "../../images/booking-engine-seat-layout.jpg";
            }

            window.open(ImageName1, '_blank', 'tools=0,status=0,history=0,width=867,height=620,top=50,left=50');
        }

        function terms_Conditions(oSrc, args) {
            args.IsValid = document.getElementById("<%=chkterms.ClientID %>").checked;
        }
    </script>
    <script type="text/javascript">
        //        window.onload = function () {
        //            var TotalPayment = document.getElementById("<%=lblTotalpayableAmount.ClientID %>");
        //            var TotalBalAmount = document.getElementById("<%=lbltotalAmount.ClientID %>");
        //            TotalPayment.firstChild.data = TotalBalAmount.firstChild.data;
        //            document.getElementById("<%=txtRoyalcardBalance.ClientID %>").disabled = true;
        //            document.getElementById("<%=txtRoyalcardPoints.ClientID %>").disabled = true;
        //        }
        var ticketPriceAfterDeduction;
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
                    ticketPriceAfterDeduction = TotalPayment.firstChild.data - b;
                    if (ticketPriceAfterDeduction < 0) {
                        alert("Please correct the value to be deducted from your royal card");
                        $("[id*=divAlertMessage]").hide();
                    }
                    else if (ticketPriceAfterDeduction > 0) {
                        $("[id*=divAlertMessage]").show();
                    }
                    if (ticketPriceAfterDeduction == 0) {
                        $("[id*=divAlertMessage]").hide();
                    }
                    lblTotalpayableAmount.firstChild.data = ticketPriceAfterDeduction;

                }
                else {
                    if (chkboxRoyalcard.checked) {
                        document.getElementById("<%=txtRoyalcardBalance.ClientID %>").disabled = false;
                        lblRoyalcardBalance.firstChild.data = lblRoyalcardBalance.firstChild.data - txtTotalRCBal.value;
                        ticketPriceAfterDeduction = TotalPayment.firstChild.data - b;
                        if (ticketPriceAfterDeduction < 0) {
                            alert("Please correct the value to be deducted from your royal card");
                            $("[id*=divAlertMessage]").hide();
                        }
                        else if (ticketPriceAfterDeduction > 0) {
                            $("[id*=divAlertMessage]").show();
                        }
                        if (ticketPriceAfterDeduction == 0) {
                            $("[id*=divAlertMessage]").hide();
                        }
                        lblTotalpayableAmount.firstChild.data = ticketPriceAfterDeduction;

                    }
                    if (!chkboxRoyalcard.checked) {
                        document.getElementById("<%=txtRoyalcardBalance.ClientID %>").disabled = true;

                    }
                }
            }
        }
        function RPoints() {
            $(this).forceNumeric();
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
            $(this).forceNumeric();
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
                    ticketPriceAfterDeduction = TotalPayment.firstChild.data - a;
                    if (ticketPriceAfterDeduction < 0) {
                        alert("Please correct the value to be deducted from your royal card");
                        $("[id*=divAlertMessage]").hide();
                    }
                    else if (ticketPriceAfterDeduction > 0) {
                        $("[id*=divAlertMessage]").show();
                    }
                    if (ticketPriceAfterDeduction == 0) {
                        $("[id*=divAlertMessage]").hide();
                    }
                    lblTotalpayableAmount.firstChild.data = ticketPriceAfterDeduction;

                }
                else {
                    if (chkboxRoyalPoints.checked) {
                        document.getElementById("<%=txtRoyalcardPoints.ClientID %>").disabled = false;
                        lblRoyalcardPoints.firstChild.data = lblRoyalcardPoints.firstChild.data - txtTotalRCPoints.value;
                        ticketPriceAfterDeduction = TotalPayment.firstChild.data - txtTotalRCPoints.value;
                        if (ticketPriceAfterDeduction < 0) {
                            alert("Please correct the value to be deducted from your royal card");
                            $("[id*=divAlertMessage]").hide();
                        }
                        else if (ticketPriceAfterDeduction > 0) {
                            $("[id*=divAlertMessage]").show();
                        }
                        if (ticketPriceAfterDeduction == 0) {
                            $("[id*=divAlertMessage]").hide();
                        }
                        lblTotalpayableAmount.firstChild.data = ticketPriceAfterDeduction;
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
    <script type="text/javascript">
        // forceNumeric() plug-in implementation
        var seatSelected = 0;
        jQuery.fn.forceNumeric = function () {
            return this.each(function () {
                $(this).keydown(function (e) {
                    //alert(e.keyCode);

                    var key = e.which || e.keyCode;
                    if (key == 109) return false;
                    if (!e.shiftKey && !e.altKey && !e.ctrlKey &&
                    // numbers   
                         key >= 48 && key <= 57 ||
                    // Numeric keypad
                         key >= 96 && key <= 105 ||
                    // comma, period and minus, . on keypad
                        key == 190 || key == 188 || key == 109 || key == 110 ||
                    // Backspace and Tab and Enter
                        key == 8 || key == 9 || key == 13 ||
                    // Home and End
                        key == 35 || key == 36 ||
                    // left and right arrows
                        key == 37 || key == 39 ||
                    // Del and Ins
                        key == 46 || key == 45)
                        return true;
                    return false;
                });
            });
        }


        $(document).ready(function () {
            $("[id*=txtRoyalcardBalance]").forceNumeric();
            $("[id*=txtRoyalcardPoints]").forceNumeric();
            $("[id*=chkbxRoyalcardBalance]").attr("disabled", true);
            $("[id*=chkbxRoyalcardPoints]").attr("disabled", true);
            $("[id*=btnRoyalCardSubmit]").click(function () {
                if (ticketPriceAfterDeduction < 0) {
                    alert("Please correct the value to be deducted from your royal card");
                    return false;
                }

                if (seatSelected == 1 && ticketPriceAfterDeduction > 0) {
                    alert("Your cumulative amount & point balance is short of the total purchase value of the tickets. To purchse the tickets, the balance amount shall be payable by debit/credit card.");
                }
            });

            //            $("[id*=txtRoyalcardBalance]").keydown(function () {
            //                $(this).forceNumeric();
            //            });
            //            //
            //            $("[id*=txtRoyalcardPoints]").keydown(function () {
            //                $(this).forceNumeric();
            //            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            // re-bind your jQuery events here
            $("[id*=chkbxRoyalcardBalance]").attr("disabled", true);
            $("[id*=chkbxRoyalcardPoints]").attr("disabled", true);
            $("[id*=txtRoyalcardBalance]").forceNumeric();
            $("[id*=txtRoyalcardPoints]").forceNumeric();
            //            $("[id*=txtRoyalcardBalance]").keydown(function () {
            //                $(this).forceNumeric();
            //            });
            //            //
            //            $("[id*=txtRoyalcardPoints]").keydown(function () {
            //                $(this).forceNumeric();
            //            });
            //
            $("[id*=TotalSeats]").change(function () {
                seatSelected = 1;
                if ($(this).val() != "0") {
                    $("[id*=divAlertMessage]").show();
                }

                if ($(this).val() == "0") {
                    $("[id*=divAlertMessage]").hide();
                }
                var ticCategoty = $("[id*=ddl_Category] option:selected").text();
                // alert(ticCategoty);
                var count = $(this).val();
                var TicketpriceArray = ticCategoty.split(".");
                //alert(TicketpriceArray[1] * count);
                $("[id*=lblTotalpayableAmount]").text(TicketpriceArray[1] * count);
                $("[id*=lbltotalAmount]").text(TicketpriceArray[1] * count);

                $("[id*=chkbxRoyalcardBalance]").attr("disabled", false);

                $("[id*=chkbxRoyalcardPoints]").attr("disabled", false);
            });
            $("[id*=Category]").change(function () {
                $("[id*=chkbxRoyalcardBalance]").attr("disabled", true);
                $("[id*=chkbxRoyalcardPoints]").attr("disabled", true);

                $("[id*=chkbxRoyalcardBalance]").attr("checked", false);
                $("[id*=chkbxRoyalcardPoints]").attr("checked", false);

                $("[id*=txtRoyalcardPoints]").val("0");
                $("[id*=txtRoyalcardBalance]").val("0");
            });
        });


        function BookTicket() {

        }
        function validateBookingShow() {

            var oDDL = document.getElementById('<%=ddl_Date.ClientID %>');
            var curText = oDDL.options[oDDL.selectedIndex].text;
            PageMethods.BookingdateValidation(curText, onSucceed, onError);
            return true;
            // alert("asdsa");
        }
        function onSucceed(result) {
            if (result == 'Welcome') {
                boolRe = 1;
            }
            else {
                boolRe = 2;
                alert("BOOKING TO OPEN 1 MONTH BEFORE THE SHOW");
                $("[id*=ddl_Date]").val("0");
                $("[id*=ddl_ShowTimes]").html("");
                var myOptions = {
                    0: 'Select'
                };
                var mySelect = $("[id*=ddl_ShowTimes]");
                $.each(myOptions, function (val, text) {
                    mySelect.append(
        $('<option></option>').val(val).html(text)
    );
                });


            }
        }
        function onError(result) {
            boolRe = 1;
        }
        
    </script>
</asp:Content>
