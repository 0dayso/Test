using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KoDTicketing.BusinessLayer
{
    public class Ticket
    {
        String _filmCode = "";  //One of the shows at KoD
        DateTime _PlayDateTime;
        String playdate, playtime;
        String _day;

        String _Location = "";  //Nautanki Mahal or Outside
        String _AudiNo = "";



        String _Category = "";  //Bronze to Platinum
        String _SeatNo = "";    //Seat#

        public String Play { get { return _filmCode; } set { _filmCode = value; } }
        public String PlayDate { get { return playdate; } set { playdate = value; } }
        public String PlayTime { get { return playtime; } set { playtime = value; } }
        public String Location { get { return _Location; } set { _Location = value; } }
        public String AudiNo { get { return _AudiNo; } set { _AudiNo = value; } }
        public String Category { get { return _Category; } set { _Category = value; } }
        public String SeatNo { get { return _SeatNo; } set { _SeatNo = value; } }
        public string Day { get { return _day; } set { _day = value; } }
    }

    public class Customer
    {
        String _customerName;
        String _mobileNo;
        String _emailID;
        String _Address = "";
        String _Pin = "";
        String _Street = "";
        String _Country = "";

        public String CustomerName { get { return _customerName; } set { _customerName = value; } }
        public String MobileNo { get { return _mobileNo; } set { _mobileNo = value; } }
        public String EmailID { get { return _emailID; } set { _emailID = value; } }
        public String Address { get { return _Address; } set { _Address = value; } }
        public String Pin { get { return _Pin; } set { _Pin = value; } }
        public String Street { get { return _Street; } set { _Street = value; } }
        public String Country { get { return _Country; } set { _Country = value; } }
    }
    public class AuditReport
    {
        String _ShowName="";
        String _LoctionText="";
        String _LocationValue = "";
        DateTime _timetext;
        String _timevalue;
        DateTime _date;
        public string ShowName { get { return _ShowName; } set { _ShowName = value; } }
        public string LocationText { get { return _LoctionText; } set { _LoctionText = value; } }
        public string LocationValue { get { return _LocationValue; } set { _LocationValue = value; } }
        public DateTime Timetext { get { return _timetext; } set { _timetext = value; } }
        public String Timevalue { get { return _timevalue; } set { _timevalue = value; } }
        public DateTime Date { get { return _date; } set { _date = value; } }
    }

    public class TransactionRecord
    {  
        Customer _customer = new Customer();
        Ticket _ticket = new Ticket(); //TODO: make it a collection to allow multiple categories in one transaction

        int _ID = 0;
        long _BookingID = 0;
        long _ReferenceNo = 0;
        string _Source = "";
        string _DateOfBooking = "";
        string _TimeOfBooking = "";
        String _KeyNo = "";

        int _TotalSeats = 0;
        decimal _TotalAmount = 0;

        string _AgentCode = "";
        string _BookingType = "";
        bool _Status = false;
        string _PaymentType = "";
        string _CardType = "";
        string _CardNo = "";
        string _PaymentGateway = "";
        string _ReceiptNo = "";
        protected string _VoucherType = "";
        protected long _VoucherBookingID = 0;
        protected string _VoucherNo = "";


        ////for new year
        //protected int _OptionSelected = 0;
        //protected decimal _ChargePerCouple = 0;
        //protected int _NoOfCouple = 0;
        //protected decimal _ChargePerindiv = 0;
        //protected int _NoOfindiv = 0;
        //protected decimal _ChargePer12 = 0;
        //protected int _NoOfindiv12 = 0;
        //protected decimal _ChargePer5 = 0;
        //protected int _NoOfindiv5 = 0;

        //For HDFC Gateway
        protected string _IP = "";
        protected string _Remark = "";
        //--------------------------------------------------------------------------------------------

        public int ID { get { return _ID; } set { _ID = value; } }
        public long BookingID { get { return _BookingID; } set { _BookingID = value; } }
        public long ReferenceNo { get { return _ReferenceNo; } set { _ReferenceNo = value; } }
        public string ReceiptNo { get { return _ReceiptNo; } set { _ReceiptNo = value; } }

        public string AgentCode { get { return _AgentCode; } set { _AgentCode = value; } }
        public string Source { get { return _Source; } set { _Source = value; } }
        public string BookingType { get { return _BookingType; } set { _BookingType = value; } }

        public string VoucherType { get { return _VoucherType; } set { _VoucherType = value; } }
        public string VoucherNo { get { return _VoucherNo; } set { _VoucherNo = value; } }
        public long VoucherBookingID { get { return _VoucherBookingID; } set { _VoucherBookingID = value; } }

        public String KeyNo { get { return _KeyNo; } set { _KeyNo = value; } }
        public string IP { get { return _IP; } set { _IP = value; } }

        public int TotalSeats { get { return _TotalSeats; } set { _TotalSeats = value; } }
        public decimal TotalAmount { get { return _TotalAmount; } set { _TotalAmount = value; } }

        public string CardType { get { return _CardType; } set { _CardType = value; } }
        public string CardNo { get { return _CardNo; } set { _CardNo = value; } }
        public string PaymentGateway { get { return _PaymentGateway; } set { _PaymentGateway = value; } }
        public string PaymentType { get { return _PaymentType; } set { _PaymentType = value; } }

        public string DateOfBooking { get { return _DateOfBooking; } set { _DateOfBooking = value; } }
        public string TimeOfBooking { get { return _TimeOfBooking; } set { _TimeOfBooking = value; } }

        public bool Status { get { return _Status; } set { _Status = value; } }
        public string Remark { get { return _Remark; } set { _Remark = value; } }

        public string ShowDate { get { return _ticket.PlayDate; } set { _ticket.PlayDate = value; } }
        public string ShowTime { get { return _ticket.PlayTime; } set { _ticket.PlayTime = value; } }
        public string Play { get { return _ticket.Play; } set { _ticket.Play = value; } }
        public string Location { get { return _ticket.Location; } set { _ticket.Location = value; } }
        public string Category { get { return _ticket.Category; } set { _ticket.Category = value; } }
        public string Day { get { return _ticket.Day; } set { _ticket.Day = value; } }
        public string SeatInfo { get { return _ticket.SeatNo; } set { _ticket.SeatNo = value; } }

        public string Name { get { return _customer.CustomerName; } set { _customer.CustomerName = value; } }
        public string EmailID { get { return _customer.EmailID; } set { _customer.EmailID = value; } }
        public string MobileNo { get { return _customer.MobileNo; } set { _customer.MobileNo = value; } }
        public string Address { get { return _customer.Address; } set { _customer.Address = value; } }
        public string Pin { get { return _customer.Pin; } set { _customer.Pin = value; } }
        public string Street { get { return _customer.Street; } set { _customer.Street = value; } }
        public string Country { get { return _customer.Country; } set { _customer.Country = value; } }

        public int IsProcessed { get; set; }
        public int PaymentStatus { get; set; }
        public string router { get; set; }

        //public int NoOfCouple { get { return _NoOfCouple; } set { _NoOfCouple = value; } }
        
        //**********Promotion code properties ****** Start ********
        public String PromotionCode { get; set; }
        public Decimal DiscountPercentage { get; set; }
        public Decimal DiscountedAmount { get; set; }
        public string WebPromotionId { get; set; }
        //**********Promotion code properties ****** END ********

        public bool IsChecked { get; set; }
        public string PlaceOfPick { get; set; }
        public string TimeOfPick { get; set; }
        public string PlaceOfDrop { get; set; }
        public string TimeOfDrop { get; set; }
        public bool WantComplimentary { get; set; }
        public bool WantComplimentaryDrop { get; set; }
        //**********Royal code properties ****** Start ********
        public string RegId { get; set; }
        public decimal AvailedPoints { get; set; }
        public decimal AvailedAmount { get; set; }
        public string TopUpTransactionId { get; set; }
        public decimal TopUpAmount { get; set; }
        public string OptionalEmail { get; set; }
        public string OptionalContact { get; set; }
        //**********Royal code properties ****** END ********
        //*******New Year************//
        public string NYBookingID { get; set; }
        public decimal NYTotalAmount { get; set; }
        public string NYReceiptNo { get; set; }
        public short QntyCouple { get; set; }
        public short QntyKids { get; set; }
        public short QntyTeens { get; set; }
        public short QntySingle { get; set; }
        //********************************//  
        //*******MMT promotion************//
        public string MMTBookingID { get; set; }
        public decimal MMTPayableAmount { get; set; }
        public string MMTReceiptNo { get; set; }
        //********************************// 
        //*******Summer Camp Event promotion************//
        public string SummerBookingID { get; set; }
        public decimal SummerPayableAmount { get; set; }
        public string SummerReceiptNo { get; set; }
        //********************************// 
        //*******Valentine Affair********//
        public string VLBookingID { get; set; }
        public decimal VLTotalAmount { get; set; }
        public string VLReceiptNo { get; set; }
        public short Quantity { get; set; }
        //********For Detailed Report********//
        public DateTime startDateDetailed { get; set; }
        public DateTime EndDateDetailed { get; set; }
        //**********************************//
        public Decimal PayableAmount { get; set; }
        //*********ForEventTransaction*********//
        public string EvtBookingID { get; set; }
        public decimal EvtTotalAmount { get; set; }
        public string EvtReceiptNo { get; set; }
        public short EvtQuantity { get; set; }
    }
}
