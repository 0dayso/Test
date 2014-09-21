<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.master" AutoEventWireup="true" CodeFile="OctoberFest.aspx.cs" Inherits="OctoberFest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.8.18.min.js" type="text/javascript"></script>
    <style type="text/css">
 
         .grayBox
        { 
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.9;
        position: fixed; 
        top: 0%; 
        left: 0%; 
        width: 100%; 
        height: 100%; 
        } 
        .box_content
        {
            position:relative;
            background-color: #000000;
            filter: alpha(opacity=100);
            opacity: 5.0;
            border-width: 3px;
            border-style:solid;
            border-color: #E7C54A;
            padding: 3px;
            width: 550px;
            height: 500px;
            left:30%;
            top:15%;
        }
        .ModalWindow
        {
            background-color: #000000;
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
       .ui-datepicker
        {
            font-size: 8pt !important;
            background-color: #290627;   
        }
    </style> 
   <%--<link href="css/datepicker.css" rel="stylesheet" type="text/css"/>--%>
    <link href="css/datestyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        var datelist = [];
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
                $("[id$=dateofshow]").datepicker({
                    dateFormat: 'D, M dd, yy',
                    minDate: '0d',
                    maxDate: 60,
                    showOn: "button",
                    buttonImage: "images/calendar.gif",
                    buttonImageOnly: true,
                    showAnim: 'blind',
                    numberOfMonths: 2,
                    beforeShowDay: function (d) {
                        // normalize the date for searching in array
                        var dmy = "";
                        //                        for local "-" and for live'/'
                        dmy += ("00" + d.getDate()).slice(-2) + "/";
                        dmy += ("00" + (d.getMonth() + 1)).slice(-2) + "/";
                        dmy += d.getFullYear();
                        if ($.inArray(dmy, datelist) >= 0) {
                            return [true, ""];
                        }
                        else {
                            return [false, ""];
                        }
                    }
                });

                $(".ui-datepicker-trigger").css("vertical-align", "middle");
                //$(".ui-datepicker-trigger").css("padding", "2px 0px 0px 0px");
                $(".ui-datepicker-trigger").css("height", "18px");

                $('img.ui-datepicker-trigger').click(function () {
                    //$("[id$=dateofshow]").val('');
                    var k = "";
                    var j = document.getElementById('<%=ddl_Date.ClientID %>').options;
                    for (i = 1; i < j.length; i++) {
                        k = k + j[i].value + ",";
                    }
                    //alert(k);
                    datelist = []; // empty the array
                    //var result = "26/07/2013";// dummy result
                    datelist = k.split(",") // populate the array
                    $("[id$=dateofshow]").datepicker("refresh"); // tell datepicker that it needs to draw itself again
                });
                $("input[type=checkbox]").change(function () {
                    var test = $(this).attr('id');
                    var ischecked = $(this).attr('checked')
                    if (test == "ctl00_mainContent_chkterms" && ischecked == "checked") {
                        $("#grayBG").show();
                        $("#Container").show();
                    }
                    else if (test == "ctl00_mainContent_chkterms" && ischecked != "checked") {
                        document.getElementById("chktc").checked = false;
                    }
                });
            });
        });
    </script>
    <%--<script type="text/javascript">
        $(document).ready(function () {
            $("[id$=ddl_Play]").change(function () {
                $("[id$=dateofshow]").val('Select');
                __doPostBack('', '');
            });
        });
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="Server">
    <script language="javascript" type="text/javascript">
        var boolRe;
        //$("[id*=ddl_Date]")
        
        
    </script>
 
    <b style="font-size: medium">Booking Details</b>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div style="position: relative">
                <table style="position: relative" width="100%">
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
                        <td>
                            <b>Play</b>
                        </td>
                        <td style="width: 13px">
                        :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_Play" Width="160px" runat="server" CssClass="text-small"
                                  AutoPostBack="true">
                                <asp:ListItem Value="JHUMROO">JHUMROO</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="show"
                                ControlToValidate="ddl_Play" Display="None" ErrorMessage="Play" InitialValue="0"
                                ForeColor="#ECA035" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_Location" runat="server" CssClass="text-small" Width="160px" 
                               AutoPostBack="True">
                               <asp:ListItem Value="NMJM">October</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="req1" Display="None" ErrorMessage="Location" runat="server"
                                ValidationGroup="show" ControlToValidate="ddl_Location" InitialValue="0" ForeColor="#ECA035" />
                        </td>
                    </tr>
                    <tr>
                       <%-- <td>
                            <b>Date</b>
                        </td>
                        <td>
                            :
                        </td>--%>
                       <td><div id="dt" style="display: none">
                            <asp:DropDownList ID="ddl_Date" runat="server" Width="160px"  CssClass="text-small" 
                                 AutoPostBack="true" onchange="javascript:validateBookingShow();" >
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList></div>
                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="show"
                                ControlToValidate="ddl_Date" Display="None" ErrorMessage="Date" InitialValue="0"
                                ForeColor="#ECA035" />--%>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <b>Date</b>
                        </td>
                        <td>
                        :
                        </td>
                        <td>
                            <asp:TextBox ID="dateofshow" runat="server" 
                                AutoPostBack="true" Width="129px" Height="12" style=" margin-right:5px; vertical-align:middle" Text="Select" 
                                ontextchanged="dateofshow_TextChanged"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="show"
                                ControlToValidate="dateofshow" Display="None" ErrorMessage="Date" InitialValue="0"
                                ForeColor="#ECA035" />
                            </td>
                            </tr>
                    <tr>
                        <td>
                            <%--<input type="text" id="dateofshow" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Show time</b>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_ShowTimes" Width="160px" runat="server" CssClass="text-small"
                                OnSelectedIndexChanged="ddl_ShowTimes_SelectedIndexChanged" AutoPostBack="true">
                                <%--onclick="javascript:return validateBookingShow();"--%>
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="show"
                                ControlToValidate="ddl_ShowTimes" Display="None" ErrorMessage="Show time" InitialValue="0"
                                ForeColor="#ECA035" />
                        </td>
                    </tr>
                    <tr>
                        <td>
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
                        <td>
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
                        <td>
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
                </table>
            </div>
             </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_Submit" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:CheckBox runat="server" ID="chkterms" />
    <asp:CustomValidator ID="chkvalidato" Display="None" runat="server" EnableClientScript="true"
        ErrorMessage="accept terms and conditions" ClientValidationFunction="terms_Conditions"
        ValidationGroup="show"></asp:CustomValidator>
    I Agree to the Terms and Conditions, to read &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="Button1" CssClass="clickhere" runat="server" Text="Click Here" /><br />
    <br />
    <div style="display: none">
        <asp:LinkButton CssClass="clickhere" ID="lnbShowCast" runat="server" Text="Click here"></asp:LinkButton>&nbsp;for
        the Show Cast
        <br />
        <br />
    </div>
    <div style="float: left">
        <asp:Button ID="btn_Submit" CssClass="common-button" ValidationGroup="show" runat="server"
            Text="Select Seats" OnClick="btn_Submit_Click" /></div>
    <%--OnClientClick="javascript:return BookTicket();"--%>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
        ShowSummary="false" HeaderText="Below fields are missing.." ValidationGroup="show" />
    <cc1:ModalPopupExtender ID="mo12" PopupControlID="dv_pop" BackgroundCssClass="modalBackground2"
        CancelControlID="btnClose" TargetControlID="Button1" runat="server">
    </cc1:ModalPopupExtender>
    <div id="dv_pop" class="ModalWindow" style="display: none; width: 550px; height: 500px;"
        runat="server">
        <div style="overflow: auto; width: 530px; height: 460px; padding: 0px 10px 0px 10px">
            <h3>
                AGE CRITERIA</h3>
            <b>
                <p>
                    Children of age two years & below, are not allowed inside Nautanki Mahal for shows due to high decibel level. Price of a show ticket for a child above 2 years would be same as for Adults for all categories.</p>
               </b>
            <h3>
                CANCELLATION/REFUND POLICY</h3>
            
                <p>
                     Tickets once booked cannot be cancelled or refunded or exchange and change of date/time of show is also not allowed.</p>
            
            <p>
               The Artists/Schedule & Timing for the show may change without prior notice. No refunds are allowed for change in the advertised artists.
            </p>
          
            <h3>
                Ticket Terms and Conditions</h3>
            <ol>
                <li>This offer is valid for students only. </li>
                <li>Both the students must carry their valid college ID Card in order to avail the offer</li>
                <li>This offer is applicable only on silver category and above.</li>
                <li>This offer is applicable only on Jhumroo show and is for specified show date/time and cannot be clubbed/ exchanged/ transferred with any other show or event or service.</li>
                <li>This offer is currently valid till 31 October  2013.</li>
                <li>This offer cannot be redeemed with cash.</li>
                <li>Any pricing errors or omissions will be corrected on our systems and the corrected price will be charged to your order (including those orders already submitted and accepted). </li>
                <li>Entry will not be allowed inside Nautanki Mahal Auditorium during certain segments of the show. Late Entry for the events/show will only be permitted in breaks or as and when indicated by Kingdom of Dreams ushers. </li>
                <li>Kindly re- check date, time and schedule of the performances/shows of the ticket.Ticket is strictly non-refundable/non-transferable. Ticket once issued will not be reissued, even if misplaced/ stolen etc.</li>
                <li>All tickets must be purchased directly from Kingdom of Dreams or through an official ticket agency authorized by the Kingdom of Dreams . Any queries in relation to ticket agencies should be referred directly to Kingdom of Dreams . Any attempt to present any other ticket may lead to refusal of admission to the Venue and possible prosecution. Unauthorized vendors will be prosecuted.</li>
                <li>The ticket holder has a right only to a seat of a value corresponding to that stated on the ticket. Management reserve the right to provide an alternative seat to that stated on the ticket. Management reserves the right to cancel the show/event or schedule it on an alternate date without assigning any reasons. </li>
                <li>Carrying of alcohol, cigarettes & banned substances & outside food & Beverages in the premises of  Kingdom of Dreams is  strictly prohibited. The Management shall be entitled to seize any prohibited items without liability and without any obligation to return such items or to compensate the holder in respect thereof.</li>
                <li>Smoking is strictly prohibited inside Nautanki Mahal and other areas except the designated smoking areas.</li>
                <li>Kingdom of Dreams reserve the right to perform security check on guests at the entry points for security reasons.</li>
                <li>All the rates are inclusive of applicable Government taxes as on date (unless specified).</li>
                <li>Photography through cameras or mobile phones is strictly prohibited inside Nautanki Mahal. The use of laser light & audio/video equipment by the patrons are also strictly prohibited inside the Nautanki Mahal. Management reserves the right to confiscate memory cards, film and other media, or observe it being deleted if posted signs are ignored. Kingdom of Dreams reserves the right to broadcast or telecast any event. </li>
                <li>Kindly switch off all pagers, mobile phones and any other communication devices upon entering Nautanki Mahal. Kindly maintain silence during the shows.</li>
                <li>Camera, baggage, handbags, bottles, cans/tins, weapons or any other belongings are not allowed inside the Nautanki Mahal.   Lockers are available in a limited manner on First-cum-First-use basis. </li>
                <li>Creche facility is available on demand, subject to availability, on First-cum-First-use basis. </li>
                <li>Ticket holders enter the venue at their own risk. Kingdom of Dreams will not be responsible for any loss, damage or injury arising from a pre-existing medical condition or due to a breach of these conditions. </li>
                <li>This ticket may not, without the prior written consent of Kingdom of Dreams, be resold for commercial packages or at a premium, nor May it be packaged or used for advertising, promotional or other commercial purposes. If a ticket is sold in breach of this condition, the bearer of the ticket may be refused admission. </li>
                <li>Parking facilities are available at Car/Vehicle owner’s own risk.</li>
                <li>All trademarks, brand names and intellectual property displayed on the ticket, at the venue and the Kingdom of Dreams belong to the Great Indian Nautanki Company Private Limited or to their rightful owners.</li>
                <li>Pricing on the Public Holidays is equivalent to that of Weekend pricing for Nautanki Mahal & Culture Gully. </li>
                <li>Kingdom of Dreams has a “no re-entry” policy unless allowed through security.</li>
                <li>All standard Kingdom of Dreams terms and conditions are applicable.</li>
                <li>All disputes shall be subject to the jurisdiction of Courts in Gurgaon, Haryana. </li>
                <li>The decision of the Management of Kingdom of Dreams shall be final & binding.</li>
                <li>Kingdom of Dreams reserve the Right of Admission. </li>

            </ol>
            
            <p>
               <b>Kingdom of Dreams reserve the right without refund or other recourse, to refuse admission to anyone who is found to be in breach of these terms and conditions including, if necessary, ejecting the holder/s of the ticket from the premise of Kingdom of Dreams after they have entered the premise of Kingdom of Dreams.</b>
            </p>
            <h3>
                CANCELLATION AND POSTPONE POLICY
            </h3>
            <p>
               Occasionally, performances are cancelled or postponed. If Kingdom of Dreams cancels the show in advance, we  will attempt to contact the guest and offer a free exchange or refund of your tickets. For this reason, make sure when you book tickets, you  have  given accurate contact details. The service charge or any other charges paid for and included in the ticket sale, such as convenience charge or delivery charge paid for booking of the ticket will not be refunded. This ticket is the sole property of KOD. The ticket should be bought from an authorized point of sale only.</p>

        </div>
        <hr />
        <asp:Button Text="Close" runat="server" CssClass="common-button" ID="btnClose" />
    </div>
    <div id="Div_ShowCast" class="ModalWindow2" style="display: none;" runat="server">
        <iframe height="350px" width="400px" frameborder="0" src="Reports/Show_Cast.htm">
        </iframe>
        <hr />
        <asp:Button Text="Close" runat="server" CssClass="common-button" ID="btnClose2" />
        <select id="dddd">
        </select>
    </div>
     <div id="grayBG" class="grayBox" style="display:none;">
        <div id="Container" class="box_content" style=" width: 500px; height: 320px;">
            <div style="overflow: auto; width: 450px; height: 255px; padding: 0px 10px 0px 10px">
             <center><u><h3><font color="red">
                Ticket Terms and Conditions</font></h3></u></center><br />
                <ul>
                    <li>
                    As per policy we do not cancel/refund/change any tickets once booked.
                    </li><br />
                    <li>
                   The company reserves the right to cancel or re-schedule the theatre programme to an alternate date or time.
                    </li><br />
                    <li>
                    Performances and Performers of the theater programme are subject to change without prior notice.
                    </li><br />
                    <li>
                    Price of a show ticket for a child above 2 years would be same as for Adults for all categories.
                    </li><br />
                </ul>
            </div>
            <input id="chktc" type="checkbox" />I Agree to the Terms and Conditions
            <hr />
           <center> <input type="button" value="OK" class="common-button" id="Buttonclose" runat="server" onclick="HideBox1();"/> </center>
        </div>
    </div>

  <%--<script language="javascript" type="text/javascript" src="js/jquery-1.8.2.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("input[type=checkbox]").change(function () {
            var test = $(this).attr('id');
            var ischecked = $(this).attr('checked')
            if (test == "ctl00_mainContent_chkterms" && ischecked == "checked") {
                $("#grayBG").show();
                $("#Container").show();
            }
            else if (test == "ctl00_mainContent_chkterms" && ischecked != "checked") {
                document.getElementById("chktc").checked = false;
            }
        });
    });
</script>--%>
<script type="text/javascript">
    function HideBox1() {
        var ischkchecked = document.getElementById("chktc").checked;
        if (ischkchecked == false) {
            alert("Please accept Terms and Conditions");
            document.getElementById("chktc").focus();
        }
        else {
            document.getElementById("grayBG").style.display = "none";
            document.getElementById("Container").style.display = "none";
        }
    }
</script>
    <script type="text/javascript">
        function ChangeImage() {
            var DropdownList = document.getElementById('<%=ddl_Play.ClientID %>');
            var SelectedText = DropdownList.options[DropdownList.selectedIndex].text;
            var ImageName1;
            if (SelectedText == "ZANGOORA") //ZANGOORA
            {
                var DropdownList1 = document.getElementById('<%=ddl_Location.ClientID %>');
                var SelectedText1 = DropdownList1.options[DropdownList1.selectedIndex].text;
                if (SelectedText1 == "November") {
                    ImageName1 = "images/zangoora-seat-layout.jpg";
                }
                else {
                    ImageName1 = "images/zangoora-seat-layout1.jpg";
                }
            }
            else if (SelectedText == "JHUMROO") //JHUMROO
            {
                var DropdownList1 = document.getElementById('<%=ddl_Location.ClientID %>');
                var SelectedText1 = DropdownList1.options[DropdownList1.selectedIndex].text;
                if (SelectedText1 == "November") {
                    ImageName1 = "images/jhumroo-seat-layout.jpg";
                }
                else {
                    ImageName1 = "images/jhumroo-seat-layout1.jpg";
                }
            }
            else if (SelectedText == "CONCERT") {
                ImageName1 = "images/kailash-kher-seat-layout.jpg";
            }
            else if (SelectedText == "LIMITLESS") {
                ImageName1 = "images/Limitless-booking-engine-seat-layout.jpg";
            }
            else {
                ImageName1 = "images/booking-engine-seat-layout.jpg";
            }

            window.open(ImageName1, '_blank', 'tools=0,status=0,history=0,width=867,height=620,top=50,left=50');
        }

        function terms_Conditions(oSrc, args) {
            args.IsValid = document.getElementById("<%=chkterms.ClientID %>").checked;
        }
    </script>
    <script type="text/javascript">

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
                alert("BOOKING TO OPEN 2 MONTH BEFORE THE SHOW");
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


