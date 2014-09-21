<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditLayout.aspx.cs" Inherits="Audit_AuditLayout" %>

<%@ Register Src="../Controls/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <script src="../js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui-1.8.18.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Kingdom Of Dreams : Ticket Booking</title>

    <script type="text/javascript">
        history.forward();
    </script>
    <link href="../css/style.css" type="text/css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="../images/favicon.ico" />
   
<style type="text/css">
    
.scroll-pane 
{
width: 550px;
overflow: auto;
height:200px;
float: left;
}

.box_content 
{ 
margin-left:-43px;
position:absolute;
width:550px;
height:280px;
display:none;
z-index:9999;
padding:20px;
top:100px;
left:400px;
background-color: #000000;
border:solid 1px #FF9900;
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

.ModalWindow
{
background-image: url(images/bar-bg.jpg);
background-color: #4D103C;
background-repeat: repeat-x;
border-width: 3px;
border-style: solid;
border-color: #711B5B;
margin: 10px 10px 3px 10px;
color: White;
font-family: Verdana;
font-size: 12px;
width: 1020px;
text-align: left;
}

.pad
{
    height: 10px;
    padding: 2px;
}

</style>
     <script type="text/javascript">
         $(document).ready(function () {
             Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {

                 /* Method for vaccant seats*/
                 $('#Button2').click(function () {
                     var aa = "<%=otu%>".slice(0, -1);
                     
                     var bb = aa.toString().split('~').reverse();
                     var SessionAuditno = "<%=SessionAuditno%>";
                     var session = "<%=Session_value%>";
                     var sessionvalue = session.toString().split(',');
                     var vaccant = "";
                     var jh = 0;
                     var table1 = document.getElementById("Table1");
                     for (var i = 0; i < bb.length; i++) {
                         var cc = bb[i].toString();
                         var dd = cc.toString().split(',');
                         var ee = dd[0].toString();
                         var category = ee.toString().split('-');
                         var id = cc.toString();
                         //document.getElementById('TextBox2').value = id.toString();

                         //alert(document.getElementById("Vaccant").innerHTML);
                         //alert(cc.split(',')[0].split('-')[0].charAt(0));
                         //cc.split(',')[0].split('-')[0].charAt(0);
                         if (document.getElementById("Vaccant").innerHTML.split(' ')[2] == cc.split(',')[0].split('-')[0].charAt(0)) {
                             var id1 = 'status1' + jh;
                             var statt = document.getElementById(id1).value;
                             var remkt = 'remark1' + jh;
                             var remarkt = document.getElementById(remkt).value;
                             var erp = "";
                             if (dd[3] == "V")
                                 erp = "Vaccant";
                             if (dd[3] == "U")
                                 erp = "Unpaid Tele Booking";
                             if (dd[3] == "B")
                                 erp = "Booked";

                             vaccant = vaccant + "{'SeatID':'" + id + "_" + SessionAuditno + "', 'AuditNo':'" + SessionAuditno + "','ShowName':'" + sessionvalue[3] + "','ShowLocation':'" + sessionvalue[0] + "','ShowDate':'" + ' ' + "','ShowTime':'" + sessionvalue[2] + "','Status':'" + statt.toString() + "','Remark':'" + remarkt.toString() + "','EditTime':'" + ' ' + "','SeatDescription':'" + ee.toString() + "','Iscompleted':'" + '0' + "','Category':'" + category[1].toString() + "','ERPStatus':'" + erp + "'}" + ",";
                             jh = jh + 1;
                         }
                         var insrt = vaccant.slice(0, -1);
                         var row_vaccant = '[' + insrt + ']';
                     }
                     //alert(row_vaccant);
                     $.ajax({
                         type: 'POST',
                         contentType: "application/json; charset=utf-8",
                         url: 'AuditLayout.aspx/InsertMethod_Vacant',
                         // data: "{'SeatID':'" + document.getElementById('TextBox2').value + "_" + SessionAuditno + "', 'AuditNo':'" + SessionAuditno + "','ShowName':'" + sessionvalue[3] + "','ShowLocation':'" + sessionvalue[0] + "','ShowDate':'" + ' ' + "','ShowTime':'" + sessionvalue[2] + "','Status':'" + selectedText + "','Remark':'" + document.getElementById('Textarea1').value + "','EditTime':'" + ' ' + "','SeatDescription':'" + document.getElementById('TextBox1').value + "','Iscompleted':'" + '0' + "','Category':'" + document.getElementById('Category1').value + "'}",
                         data: JSON.stringify({ myData_vaccant: row_vaccant }),
                         async: false,
                         success: function (data) {
                             var aa = "<%=otu%>".slice(0, -1);
                             
                             //alert(aa.toString());
                             var bb = aa.toString().split('~').reverse();
                             if (data.d == "true") {
                                 //var table1 = document.getElementById("Table1");
                                 var jm = 0;
                                 for (var i = 0; i < bb.length; i++) {
                                     //alert(bb[i].toString());
                                     var cc = bb[i].toString();

                                     var dd = cc.toString().split(',');
                                     var ee = dd[0].toString();
                                     var id = cc.toString();
                                     //alert(cc.toString());
                                     //                                     document.getElementById('TextBox2').value = id.toString();
                                     //                                     var t = document.getElementById('TextBox2').value;
                                     //alert(t);
                                     var g = cc.toString().split(',');
                                     var ddk = g[0].toString().split('-');

                                     var k1 = ddk[1].toString();

                                     //alert(k1);

                                     //alert(k1);

                                     var k = document.getElementById(id.toString());
                                     //alert(k);
                                     if (document.getElementById("Vaccant").innerHTML.split(' ')[2] == cc.split(',')[0].split('-')[0].charAt(0)) {
                                         var id1 = 'status1' + jm;
                                         var stat = document.getElementById(id1).value;
                                         var remk = 'remark1' + jm;
                                         var remark = document.getElementById(remk).value;
                                         jm = jm + 1;
                                         //alert(g[3]);
                                         //if (g[3] == "V") {
                                         if (k1 == "GOLD" && g[3] == "V") {
                                             k.src = "Images/Gold_chair.gif";
                                         }
                                         else if (k1 == "COPPER" && g[3] == "V") {
                                             k.src = "Images/Copper_chair.gif";
                                         }
                                         else if (k1 == "DIAMOND" && g[3] == "V") {
                                             k.src = "Images/Diamond_chair.gif";
                                         }
                                         else if (k1 == "PLATINUM" && g[3] == "V") {
                                             k.src = "Images/Platinum_chair.gif";
                                         }
                                         else if (k1 == "SILVER" && g[3] == "V") {
                                             k.src = "Images/Silver_chair.gif";
                                         }
                                         else if (k1 == "GALLERY" && g[3] == "V") {
                                             k.src = "Images/Gallery_chair.gif";
                                         }
                                         else if (k1 == "BRONZE" && g[3] == "V") {
                                             k.src = "Images/Brown_chair.gif";
                                         }
                                         else if (g[3] == "B") {
                                             k.src = "Images/W_chair.gif";
                                         }
                                         else if (g[3] == "U") {
                                             k.src = "Images/Unpaid_seat.gif";
                                         }

                                         if (stat != 'Select' || remark != '') {

                                             if (k1 == "GOLD" && g[3] == "V") {
                                                 k.src = "Images/Gold_chair1.gif";
                                                 k.title = remark + "%" + stat;
                                             }
                                             else if (k1 == "COPPER" && g[3] == "V") {
                                                 k.src = "Images/Copper_chair1.gif";
                                                 k.title = remark + "%" + stat;
                                             }
                                             else if (k1 == "DIAMOND" && g[3] == "V") {
                                                 k.src = "Images/Diamond_chair1.gif";
                                                 k.title = remark + "%" + stat;
                                             }
                                             else if (k1 == "PLATINUM" && g[3] == "V") {
                                                 k.src = "Images/Platinum_chair1.gif";
                                                 k.title = remark + "%" + stat;
                                             }
                                             else if (k1 == "SILVER" && g[3] == "V") {
                                                 k.src = "Images/Silver_chair1.gif";
                                                 k.title = remark + "%" + stat;
                                             }
                                             else if (k1 == "GALLERY" && g[3] == "V") {
                                                 k.src = "Images/Gallery_chair1.gif";
                                                 k.title = remark + "%" + stat;
                                             }
                                             else if (k1 == "BRONZE" && g[3] == "V") {
                                                 k.src = "Images/Brown_chair1.gif";
                                                 k.title = remark + "%" + stat;
                                             }
                                             else if (g[3] == "B") {
                                                 k.src = "Images/W_chair1.gif";
                                                 k.title = remark + "%" + stat;
                                             }
                                             else if (g[3] == "U") {
                                                 k.src = "Images/unpaidcross_seat.gif";
                                                 k.title = remark + "%" + stat;
                                             }

                                         }

                                     } //if
                                 }
                                 alert("Data is Successfully added");
                                 $("#Div1").hide();
                                 $("#Div3").hide();
                             }
                             else {
                                 $("#Div1").hide();
                                 $("#Div3").hide();
                                 alert("Error:Please try again");
                             }
                         },
                         error: function () {
                             $("#Div1").hide();
                             $("#Div3").hide();
                             alert("Error:Please try again");
                         }
                     });
                 });


                 /* Method for booked seats*/
                 //                 $('#Button1').click(function () {
                 //                     var yu = "<%=ocu%>";
                 //                     var gf = yu.toString().split('~');
                 //                     var SessionAuditno = "<%=SessionAuditno%>";
                 //                     var session = "<%=Session_value%>";
                 //                     var sessionvalue = session.toString().split(',');
                 //                     var j = "";
                 //                     for (var i = 0; i < gf.length - 1; i++) {
                 //                         var gk = gf[i].toString();
                 //                         var gg = gk.toString().split(',');
                 //                         var gh = gg[0].toString();
                 //                         var cat = gh.toString().split('-');
                 //                         var id = gk.toString();
                 //                         document.getElementById('TextBox3').value = id.toString();
                 //                         var id = 'status' + (i);
                 //                         var stat = document.getElementById(id).value;
                 //                         var rem = 'remark' + (i);
                 //                         var remark = document.getElementById(rem).value;
                 //                         j = j + "{'SeatID':'" + document.getElementById('TextBox3').value + "_" + SessionAuditno + "', 'AuditNo':'" + SessionAuditno + "','ShowName':'" + sessionvalue[3] + "','ShowLocation':'" + sessionvalue[0] + "','ShowDate':'" + ' ' + "','ShowTime':'" + sessionvalue[2] + "','Status':'" + stat.toString() + "','Remark':'" + remark.toString() + "','EditTime':'" + ' ' + "','SeatDescription':'" + gh.toString() + "','Iscompleted':'" + '0' + "','Category':'" + cat[1].toString() + "'}" + ",";
                 //                         var ne = j.slice(0, -1);
                 //                         var row = '[' + ne + ']';
                 //                     }
                 //                     $.ajax({
                 //                         type: 'POST',
                 //                         contentType: "application/json; charset=utf-8",
                 //                         url: 'AuditLayout.aspx/InsertMethod_booked',
                 //                         // data: "{'SeatID':'" + document.getElementById('TextBox3').value + "_" + SessionAuditno + "', 'AuditNo':'" + SessionAuditno + "','ShowName':'" + sessionvalue[3] + "','ShowLocation':'" + sessionvalue[0] + "','ShowDate':'" + ' ' + "','ShowTime':'" + sessionvalue[2] + "','Status':'" + selectedText1 + "','Remark':'" + document.getElementById('dd1').value + "','EditTime':'" + ' ' + "','SeatDescription':'" + document.getElementById('Label1').value + "','Iscompleted':'" + '0' + "','Category':'" + document.getElementById('Category2').value + "'}",
                 //                         data: JSON.stringify({ myData: row }),
                 //                         async: false,
                 //                         success: function (data) {
                 //                             var yu = "<%=ocu%>";
                 //                             var gf = yu.toString().split('~');
                 //                             if (data.d == "true") {
                 //                                 for (var i = 0; i < gf.length - 1; i++) {
                 //                                     var gk = gf[i].toString();
                 //                                     var gg = gk.toString().split(',');
                 //                                     var gh = gg[0].toString();
                 //                                     var id = gk.toString();
                 //                                     document.getElementById('TextBox3').value = id.toString();
                 //                                     var t = document.getElementById('TextBox3').value;
                 //                                     var g = t.toString().split(',');
                 //                                     var dd = g[0].toString().split('-');
                 //                                     var k = dd[1].toString();
                 //                                     var o = document.getElementById(t);
                 //                                     //alert(o);
                 //                                     var id = 'status' + (i);
                 //                                     var stat = document.getElementById(id).value;
                 //                                     var rem = 'remark' + (i);
                 //                                     var remark = document.getElementById(rem).value;
                 //                                     o.src = "Images/W_chair.gif";
                 //                                     if (stat != "Select" || remark != "") {
                 //                                         o.src = "Images/W_chair1.gif";
                 //                                     }
                 //                                 } //foreach
                 //                                 alert("Data is Successfully added");
                 //                                 $("#grayBG").hide();
                 //                                 $("#Container").hide();

                 //                             } //if
                 //                             else {
                 //                                 $("#grayBG").hide();
                 //                                 $("#Container").hide();
                 //                                 alert("Error:Please try again");
                 //                             }
                 //                         },
                 //                         error: function () {
                 //                             $("#grayBG").hide();
                 //                             $("#Container").hide();
                 //                             alert("Error:Please try again");
                 //                         }
                 //                     });
                 //                 });


                 //                 /* Method for unpaid telebooking seats*/
                 //                 $('#Button3').click(function () {
                 //                     var yu = "<%=unpaid%>";
                 //                     var gf = yu.toString().split('~');
                 //                     var SessionAuditno = "<%=SessionAuditno%>";
                 //                     var session = "<%=Session_value%>";
                 //                     var sessionvalue = session.toString().split(',');
                 //                     var j = "";
                 //                     for (var i = 0; i < gf.length - 1; i++) {
                 //                         var gk = gf[i].toString();
                 //                         var gg = gk.toString().split(',');
                 //                         var gh = gg[0].toString();
                 //                         var cat = gh.toString().split('-');
                 //                         var id = gk.toString();
                 //                         document.getElementById('TextBox4').value = id.toString();
                 //                         var id = 'status3' + (i);
                 //                         var stat = document.getElementById(id).value;
                 //                         var rem = 'remark3' + (i);
                 //                         var remark = document.getElementById(rem).value;
                 //                         j = j + "{'SeatID':'" + document.getElementById('TextBox4').value + "_" + SessionAuditno + "', 'AuditNo':'" + SessionAuditno + "','ShowName':'" + sessionvalue[3] + "','ShowLocation':'" + sessionvalue[0] + "','ShowDate':'" + ' ' + "','ShowTime':'" + sessionvalue[2] + "','Status':'" + stat.toString() + "','Remark':'" + remark.toString() + "','EditTime':'" + ' ' + "','SeatDescription':'" + gh.toString() + "','Iscompleted':'" + '0' + "','Category':'" + cat[1].toString() + "'}" + ",";
                 //                         var ne = j.slice(0, -1);
                 //                         var row = '[' + ne + ']';
                 //                     }
                 //                     $.ajax({
                 //                         type: 'POST',
                 //                         contentType: "application/json; charset=utf-8",
                 //                         url: 'AuditLayout.aspx/InsertMethod_Unpaid',
                 //                         // data: "{'SeatID':'" + document.getElementById('TextBox3').value + "_" + SessionAuditno + "', 'AuditNo':'" + SessionAuditno + "','ShowName':'" + sessionvalue[3] + "','ShowLocation':'" + sessionvalue[0] + "','ShowDate':'" + ' ' + "','ShowTime':'" + sessionvalue[2] + "','Status':'" + selectedText1 + "','Remark':'" + document.getElementById('dd1').value + "','EditTime':'" + ' ' + "','SeatDescription':'" + document.getElementById('Label1').value + "','Iscompleted':'" + '0' + "','Category':'" + document.getElementById('Category2').value + "'}",
                 //                         data: JSON.stringify({ myData: row }),
                 //                         async: false,
                 //                         success: function (data) {
                 //                             var yu = "<%=unpaid%>";
                 //                             var gf = yu.toString().split('~');
                 //                             if (data.d == "true") {
                 //                                 for (var i = 0; i < gf.length - 1; i++) {
                 //                                     var gk = gf[i].toString();
                 //                                     var gg = gk.toString().split(',');
                 //                                     var gh = gg[0].toString();
                 //                                     var id = gk.toString();
                 //                                     document.getElementById('TextBox4').value = id.toString();
                 //                                     var t = document.getElementById('TextBox4').value;
                 //                                     var g = t.toString().split(',');
                 //                                     var dd = g[0].toString().split('-');
                 //                                     var k = dd[1].toString();
                 //                                     var o = document.getElementById(t);
                 //                                     var id = 'status3' + (i);
                 //                                     var stat = document.getElementById(id).value;
                 //                                     var rem = 'remark3' + (i);
                 //                                     var remark = document.getElementById(rem).value;
                 //                                     o.src = "Images/Unpaid_seat.gif";
                 //                                     if (stat != "Select" || remark != "") {
                 //                                         o.src = "Images/unpaidcross_seat.gif";
                 //                                     }
                 //                                 } //foreach
                 //                                 alert("Data is Successfully added");
                 //                                 $("#Div4").hide();
                 //                                 $("#Div5").hide();

                 //                             } //if
                 //                             else {
                 //                                 $("#Div4").hide();
                 //                                 $("#Div5").hide();
                 //                                 alert("Error:Please try again");
                 //                             }
                 //                         },
                 //                         error: function () {
                 //                             $("#Div4").hide();
                 //                             $("#Div5").hide();
                 //                             alert("Error:Please try again");
                 //                         }
                 //                     });
                 //                 });

                 $('#btnProceed').click(function () {
                     //alert('k');
                     var SessionAuditno = "<%=SessionAuditno%>";
                     var session = "<%=Session_value%>";
                     var sessionvalue = session.toString().split(',')
                     $.ajax({
                         type: 'POST',
                         contentType: "application/json; charset=utf-8",
                         url: 'AuditLayout.aspx/Set_Status',
                         data: "{'AuditNo':'" + SessionAuditno + "','ShowName':'" + sessionvalue[3] + "','ShowLocation':'" + sessionvalue[0] + "','ShowDate':'" + ' ' + "','ShowTime':'" + sessionvalue[2] + "','Iscompleted':'" + '0' + "'}",
                         async: false,
                         success: function (data) {
                             if (data.d == "true") {
                                 //document.getElementById("DropDownList2").selectedIndex = 0;
                                 //alert(this.data)

                                 //alert("Record Has been Saved in Database");
                                 window.location.href = "http://msticket.kingdomofdreams.in/Admin/Default.aspx";
                             }
                             else {
                                 alert("Error:Please try again");
                             }
                         },
                         error: function ()
                         { alert("Error:Please try again"); }
                     });
                 });
             });
         });     
    </script>
</head>
<body class="home-page-bg" onload="a()">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
    </asp:ScriptManager>
    <div class="wrapper_seatlayout">
        <div class="logo-row">
            <div class="logo">
                <a href="http://kingdomofdreams.in/index.html" target="_blank">
                    <img src="../images/logo.jpg" /></a>
            </div>
        </div>
        <!--logo-row ends here -->
        <div class="seats-main_02">
            <div>
                <input type="hidden" id="hidtempseats1" />
                <asp:HiddenField ID="hidtempseats" runat="server" />
                <asp:HiddenField ID="HiddenBrowser" runat="server" />
                <center>
                    <div class="ModalWindow">
                        <div id="div_seatlayo">
                         <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                         <ContentTemplate>
                            <asp:Button ID="btnProceed" CssClass="common-button" runat="server" 
                                Text="Save Audit"/>&nbsp;<asp:Button ID="btnCancel" CssClass="common-button"
                                    runat="server" Text="Cancel" OnClientClick="s(false);" OnClick="btnCancel_Click" />
                            &nbsp;
                            <img src="images/W_chair.gif" align="absmiddle" />&nbsp;Booked Seats&nbsp;&nbsp;
                            <img src="images/Diamond_chair.gif" align="absmiddle" />&nbsp;Diamond Seats &nbsp;&nbsp;
                            <img src="images/Platinum_chair.gif" align="absmiddle" />&nbsp;Platinum Seats &nbsp;&nbsp;
                            <img src="images/Gold_chair.gif" align="absmiddle" />&nbsp;Gold Seats &nbsp;&nbsp;
                            <img src="images/Gallery_chair.gif" align="absmiddle" />&nbsp;Gallery Seats &nbsp;&nbsp;
                            <img src="images/Silver_chair.gif" align="absmiddle" />&nbsp;Silver Seats &nbsp;&nbsp;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <img src="images/Copper_chair.gif" align="absmiddle" />&nbsp;Copper Seats &nbsp;&nbsp;
                            <img src="images/Brown_chair.gif" align="absmiddle" />&nbsp;Bronze Seats &nbsp;&nbsp;
                            <img src="images/Unpaid_seat.gif" align="absmiddle" />&nbsp;Unpaid Seats &nbsp;&nbsp;
                        </div>
                         </ContentTemplate>
                        </asp:UpdatePanel>
                       
                        <label id="lblpleasewit" style="display: none; font-family: Verdana">
                            Please Wait...</label>
                    </div>
                  
                    <div id="myform"  runat="server" />  
                </center>
              
              <!--Div for booked seats -->
<%--                <div id="grayBG" runat="server" class="grayBox" style="display:none;">
           <div id="showcontainer" runat="server" class="box_content">
           </div> 
           <div id="Container" runat="server" class="box_content">
               <center><b><asp:Label ID="Booked" runat="server" Text="Booked Seats" Style="font-size:20px;"></asp:Label></b></center>
               &nbsp;<asp:Image ID="Image1" runat="server" ImageUrl="~/images/close4.jpg" style="float:right;cursor:pointer;" OnClick="CloseFunction1()" /><br/><br/>
         
                    <div id="section" runat="server" class="scroll-pane">
                     <%--<div id="Div3" runat="server">
                     </div>--%>
                           <%-- <table id="myTable" border="1">
  <tr>
  <td>Seat Description</td>
  <td>Status</td>
  <td>Remark</td>
  </tr>
</table>
 </div>                
     <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Always">
                 <ContentTemplate>
               <asp:TextBox ID="Category2" runat="server" BackColor="Black" BorderStyle="None" ></asp:TextBox>
               <asp:TextBox ID="TextBox3" runat="server" BackColor="Black" BorderStyle="None" ></asp:TextBox>
               <br /><br /><hr /><br />
               <asp:Button ID="Button1" CssClass="common-button" runat="server"  Text="save" />
             </ContentTemplate>
           </asp:UpdatePanel>
           </div>
           </div>--%>
             
             <!--Div for vaccant seats -->
           <div id="Div3" runat="server" class="grayBox" style="display:none;">
           <div id="Div1" runat="server" class="box_content">
              <center><b> <label id="Vaccant"></label></b></center>
              <%--</label><asp:Label ID="Label1"  Text="" runat="server"  Style="font-size:20px;"></asp:Label>--%>
               <asp:Image ID="Image2" runat="server" ImageUrl="~/images/close4.jpg" style="float:right;cursor:pointer;" OnClick="CloseFunction()" /><br/><br/>
                   <div id="Div2" class="scroll-pane">
         <table id="Table1" border="1">
  <tr>
  <td>Seat Description</td>
  <td>Status</td>
  <td>Remark</td>
  </tr>
</table>  
 </div>
           <asp:UpdatePanel ID="UpdatePanel3"  runat="server" UpdateMode="Always">
                 <ContentTemplate>
             <%--  <asp:TextBox ID="Category1" runat="server" BackColor="Black" BorderStyle="None" ></asp:TextBox>
               <asp:TextBox ID="TextBox2" runat="server" BackColor="Black" BorderStyle="None" ></asp:TextBox>--%>
               <br /><br /><hr /><br />
               <asp:Button ID="Button2" CssClass="common-button" runat="server" Text="save"/>
                   </ContentTemplate>
           </asp:UpdatePanel>
               </div>
           </div>

           <!--Div for Unpaid telebooking seats -->
             <%-- <div id="Div4" runat="server" class="grayBox" style="display:none;">
              <div id="Div5" runat="server" class="box_content">
              <center> <b><asp:Label ID="Unpaid" runat="server" Text="Unpaid TeleBooking Seats" Style="font-size:20px;"></asp:Label></b></center>
              <asp:Image ID="Image3" runat="server" ImageUrl="~/images/close4.jpg" style="float:right;cursor:pointer;" OnClick="CloseFunction4()" /><br/><br/>
              <div id="Div6" class="scroll-pane">
         <table id="Table2" border="1">
  <tr>
  <td>Seat Description</td>
  <td>Status</td>
  <td>Remark</td>
  </tr>
</table>  
 </div>
             <asp:UpdatePanel ID="UpdatePanel4"  runat="server" UpdateMode="Always">
               <ContentTemplate>
               <asp:TextBox ID="Category4" runat="server" BackColor="Black" BorderStyle="None" ></asp:TextBox>
               <asp:TextBox ID="TextBox4" runat="server" BackColor="Black" BorderStyle="None" ></asp:TextBox>
               <br /><br /><hr /><br />
               <asp:Button ID="Button3" CssClass="common-button" runat="server" Text="save"/>
               </ContentTemplate>
             </asp:UpdatePanel>
               </div>
           </div>--%>

                <script type="text/javascript">
                    function getBrowser() {
                        getDoc("<%= HiddenBrowser.ClientID %>").value = navigator.appVersion;
                    }

                    function s(c) {

                        if (c) {

                            if (lTS('hidtempseats1')) {
                                getDoc("<%= hidtempseats.ClientID %>").value = getDoc("hidtempseats1").value;
                                getDoc("div_seatlayo").style.display = "none";
                                getDoc("lblpleasewit").style.display = "block";
                                return true;
                            }
                            else
                                return false;
                        }
                        else {
                            getDoc("div_seatlayo").style.display = "none";
                            getDoc("lblpleasewit").style.display = "block";
                        }
                    }
                </script>

                <script src="../js/jquery-1.7.2.min.js" type="text/javascript"></script>

                <!--Function for booked seats -->
               <%-- <script language="javascript" type="text/javascript">
                    function myFunction(item) 
                    {
                        var table = document.getElementById("myTable");
                        var yu = "<%=ocu%>";
                        var gf = yu.toString().split('~');
                        if (table.rows.length < gf.length - 1) {
                            for (var i = 0; i < gf.length - 1; i++) {
                                var gk = gf[i].toString();
                                var gg = gk.toString().split(',');
                                var gh = gg[0].toString();
                                var row = table.insertRow(1);
                                var cell1 = row.insertCell(0);
                                var cell2 = row.insertCell(1);
                                var cell3 = row.insertCell(2);
                                cell1.innerHTML = '<asp:TextBox id="id1" runat="server" Width="100px"></asp:TextBox>';
                                cell2.innerHTML = "<select id='status" + i + "'><option>Select</option><option>Not Occupied by Customer</option><option>Occupied by Customer</option></select>";
                                cell3.innerHTML = "<textarea id='remark" + i + "' ></textarea>";
                                document.getElementById("id1").value = gh.toString();
                                var idk = gk.toString();
                                document.getElementById('TextBox3').value = idk.toString();
                            }
                        }
                        $("#grayBG").show();
                        $("#Container").show();
                        $("#Div1").hide();
                        $("#Div3").hide();
                        $("#Div4").hide();
                        $("#Div5").hide();
                    }    
                </script>   --%>
                  <%--<script language="javascript" type="text/javascript">
                      function CloseFunction1() 
                      {   
                          $("#grayBG").hide();
                          $("#Container").hide();
                          $("#Div1").hide();
                          $("#Div3").hide();
                      }
                </script>--%>

                <!--Function for vaccant seats -->
                 <script language="javascript" type="text/javascript">
                     function myFunction1(item) {
                         var table1 = document.getElementById("Table1");
                         
                         for (var m = document.getElementById("Table1").rows.length; m > 1; m--) {
                             document.getElementById("Table1").deleteRow(m - 1);
                         }

                         var aa = "<%=otu%>".slice(0, -1);
                         //alert(aa);
                         // alert(aa);
                         var bb = aa.toString().split('~').reverse();
                         var cc = "";
                         var dd = "";
                         var ee = "";
                         var k = 0;
                         for (var i = 0; i < bb.length ; i++) {
                             var cc = bb[i].toString();
                             var dd = cc.toString().split(',');
                             var ee = dd[0].toString();
                             var cat = item.id;
                             var catspecific = cat.split(',')[0].split('-')[0];
                             document.getElementById("Vaccant").innerHTML = "ROW - " + catspecific.charAt(0);
                             
                             var specific = ee.toString().split('-');
                             var desc = specific[0].toString();
                             if (catspecific.charAt(0) == desc.charAt(0)) {
                                 k = k + 1
                             }
                         }
                         var jh = 0;
                         if (table1.rows.length < k) {
                             for (var i = 0; i < bb.length ; i++) {
                                 var cc = bb[i].toString();
                                 var dd = cc.toString().split(',');
                                 var ee = dd[0].toString();
                                 var cat = item.id;
                                 var catspecific = cat.split(',')[0].split('-')[0];
                                 var specific = ee.toString().split('-');
                                 var desc = specific[0].toString();
                                 //alert(catspecific + "," + desc);
                                 if (catspecific.charAt(0) == desc.charAt(0)) {
                                     //table1.deleteRow();
                                     var row = table1.insertRow(1);
                                     var cel1 = row.insertCell(0);
                                     var cel2 = row.insertCell(1);
                                     var cel3 = row.insertCell(2);
                                     cel1.innerHTML = '<asp:TextBox id="id2" runat="server" Width="100px"></asp:TextBox>';
                                     cel2.innerHTML = "<select id='status1" + jh + "'><option>Select</option><option>Occupied by Customer</option><option>Not Occupied by Customer</option></select>";
                                     cel3.innerHTML = "<textarea id='remark1" + jh + "'></textarea>";
                                     document.getElementById("id2").value = ee.toString();
                                     if (document.getElementById(bb[i]).title != "") {
                                         document.getElementById("remark1" + jh).innerHTML = document.getElementById(bb[i]).title.split('%')[0];
                                         if(document.getElementById(bb[i]).title.split('%')[1]=="Select")
                                                 document.getElementById("status1" + jh).selectedIndex = 0;
                                         if(document.getElementById(bb[i]).title.split('%')[1]=="Occupied by Customer")
                                                 document.getElementById("status1" + jh).selectedIndex = 1;
                                         if(document.getElementById(bb[i]).title.split('%')[1]=="Not Occupied by Customer")
                                                 document.getElementById("status1" + jh).selectedIndex = 2;
                                     }
                                     jh = jh + 1;
                                 }
                                     //var id = cc.toString();
                                     //document.getElementById('TextBox2').value = id.toString();  
                             }
                         }
                        // $("#grayBG").hide();
                         $("#Div1").show();
                         $("#Div3").show();
                         //$("#Container").hide();
                         //$("#Div4").hide();
                         //$("#Div5").hide();
                     }    
                </script>
                  <script language="javascript" type="text/javascript">
                      function CloseFunction() 
                      {
                          $("#Div1").hide();
                          $("#Div3").hide();
                      }
                </script>

                <!--Function for Unpaid Telebooking seats -->
                <%-- <script language="javascript" type="text/javascript">
                     function myFunction2(item)
                      {
                         var table = document.getElementById("Table2");
                         var yu = "<%=unpaid%>";
                         var gf = yu.toString().split('~');
                         if (table.rows.length < gf.length - 1) {
                             for (var i = 0; i < gf.length - 1; i++) {
                                 var gk = gf[i].toString();
                                 var gg = gk.toString().split(',');
                                 var gh = gg[0].toString();
                                 var row = table.insertRow(1);
                                 var cell1 = row.insertCell(0);
                                 var cell2 = row.insertCell(1);
                                 var cell3 = row.insertCell(2);
                                 cell1.innerHTML = '<asp:TextBox id="id3" runat="server" Width="100px"></asp:TextBox>';
                                 cell2.innerHTML = "<select id='status3" + i + "'><option>Select</option><option>Not Occupied by Customer</option><option>Occupied by Customer</option></select>";
                                 cell3.innerHTML = "<textarea id='remark3" + i + "' ></textarea>";
                                 document.getElementById("id3").value = gh.toString();
                                 var idk = gk.toString();
                                 document.getElementById('TextBox4').value = idk.toString();
                             }
                         }
                         $("#Div4").show();
                         $("#Div5").show();
                         $("#grayBG").hide();
                         $("#Container").hide();
                         $("#Div1").hide();
                         $("#Div3").hide();
                     }
                  </script>  
                  <script language="javascript" type="text/javascript">
                      function CloseFunction4()
                       {
                           $("#Div4").hide();
                           $("#Div5").hide();
                      }
                </script>--%>

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
            </div>
            <!-- seats-main ends here -->
        </div>
        <!-- wrapper ends here -->
    </div>
    <uc1:Footer ID="Footer1" runat="server" />
    </form>
    <!-- footer-main ends here -->
</body>
</html>
