using System;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Threading;
using System.ComponentModel;
using System.Collections.Generic;

namespace KoDTicketing.Utilities
{
    public class ReceiptUtils
    {
        //const string mailSenderAccount = "sales@kingdomofdreams.co.in";
        //const string mailSenderAccountName = "Kingdom of Dreams";
        //const string mailEmailAccountID = "comcenter@kingdomofdreams.co.in";
        //const string mailEmailAccountSMTPServer = "gcell-in-smtp.mail.lotuslive.com:465";
        //const string mailEmailAccountPassword = "KingdomC@2011";

        const string mailSenderAccount = "noreplyKOD@gmail.com";
        const string mailSenderAccountName = "Kingdom Of Dreams";
        const string mailEmailAccountID = "noreplyKOD@gmail.com";
        const string mailEmailAccountSMTPServer = "smtp.gmail.com";
        const string mailEmailAccountPassword = "k1ngd0m2012";

        public static void FailurePaymentResponse()
        {
            System.Text.StringBuilder MailBod = new System.Text.StringBuilder("<b><u>This is a system generated message.</u></b><br/>");
            MailBod.Append("Dear Sir/Madam,<br/>");
            MailBod.Append("System has encountered an error while web-booking and the corresponding information is given below.<br/><br/>");
            MailBod.Append("No User Details Available<br/>No Seat information available<br/>");
            //SendMailToCustomerSupport(MailBod);
            //SendMailToCustomerSupport(MailBod, "comcenter@kingdomofdreams.co.in");
            //SendMailToCustomerSupport(MailBod, "rashi.patra@kingdomofdreams.co.in");
            SendMailToCustomerSupport(MailBod, "failure.web.kod@gmail.com");
        }
        public static void PaymentNotCaptureResponse(String _ReceiptNo, DataRow dr, String email)
        {
            try
            {
                System.Text.StringBuilder MailBod = new System.Text.StringBuilder();
                MailBod.Append("Dear ");
                MailBod.Append(dr["Name"]);
                MailBod.Append("<br/><br/>");
                MailBod.Append("Due to some technical reason your Payment was not successful and your seats were not Booked.");
                MailBod.Append(" Please contact customer care at 0124 - 4528000  for further assistance. Below are the details...<br/><br/>");
                MailBod.Append("Receipt No : " + _ReceiptNo + "<br/>");
                MailBod.Append("Transaction Id : " + dr["BookingID"] + "<br/>");
                if (dr["PromotionCode"].ToString() != "")
                    MailBod.Append("Promo Code : " + dr["PromotionCode"] + "<br/>");
                MailBod.Append("Name          : <b>" + dr["Name"] + "</b><br/>");
                MailBod.Append("Contact No    : <b>" + dr["MobileNo"] + "</b><br/>");
                MailBod.Append("Email Address : <b>" + dr["EmailId"] + "</b><br/>");
                MailBod.Append("Booking Date  : <b>" + Convert.ToDateTime(dr["DateOfBooking"].ToString()).ToLongDateString() +
                    " at " + Convert.ToDateTime(dr["TimeOfBooking"]).ToShortTimeString() + "</b><br/>");
                MailBod.Append("Show               : <b>" + dr["Play"] + "</b><br/>");
                MailBod.Append("Show Date          : <b>" + Convert.ToDateTime(dr["ShowDate"].ToString()).ToLongDateString() +
                    " at " + Convert.ToDateTime(dr["ShowTime"]).ToShortTimeString() + "</b><br/>");
                MailBod.Append("Category           : <b>" + dr["Category"] + "</b><br/>");
                MailBod.Append("Total No. of Seats : <b>" + dr["TotalSeats"] + "</b><br/>");
                MailBod.Append("Seat Details       : <b>" + dr["SeatInfo"] + "</b><br/>");
                decimal DiscountPercentage = decimal.Parse(dr["DiscountPercentage"].ToString());
                decimal tktAmount = decimal.Parse(dr["TotalAmount"].ToString());
                int numberOfSeats = int.Parse(dr["TotalSeats"].ToString());

                if (DiscountPercentage > 0)
                {

                    decimal SingleTicketPrice = tktAmount / numberOfSeats;

                    decimal DiscountedPrice = SingleTicketPrice - (SingleTicketPrice * DiscountPercentage / 100);
                    DiscountedPrice = decimal.Truncate(DiscountedPrice);
                    if (DiscountedPrice == 1274)
                        DiscountedPrice = DiscountedPrice + 1;
                    else if (DiscountedPrice == 2124)
                        DiscountedPrice = DiscountedPrice + 1;
                    else if (DiscountedPrice == 2974)
                        DiscountedPrice = DiscountedPrice + 1;
                    else if (DiscountedPrice == 4249)
                        DiscountedPrice = DiscountedPrice + 1;

                    tktAmount = DiscountedPrice * numberOfSeats;

                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Total Amount Price For a Ticket" + tktAmount.ToString());


                }
                String Enddate = "2013.09.29";
                DateTime End = Convert.ToDateTime(Enddate);
                String Presentdate = Convert.ToDateTime(dr["ShowDate"]).ToString("yyyy-MM-dd");
                DateTime Present = Convert.ToDateTime(Presentdate);
                if (Convert.ToDecimal(dr["TotalSeats"].ToString()) > 3 && dr["Play"].ToString() == "JHUMROO" && dr["Category"].ToString() != "BRONZE" && dr["Category"].ToString() != "COPPER" && Present <= End && (dr["PromotionCode"].ToString() == "" || dr["PromotionCode"] == null))
                {
                    tktAmount = decimal.Parse(dr["PayableAmount"].ToString());
                }
                if (dr["PromotionCode"].ToString() == "VIVANTABYTAJ" || dr["PromotionCode"].ToString() == "OBEROI" || dr["PromotionCode"].ToString() == "TRIDENT" || dr["PromotionCode"].ToString() == "OBEROIDELHI" || dr["PromotionCode"].ToString() == "MMTDOMESTIC" || dr["PromotionCode"].ToString() == "EROSMANAGED" || dr["PromotionCode"].ToString() == "MCOTHERS" || dr["PromotionCode"].ToString() == "MMT" || dr["PromotionCode"].ToString() == "MANA" || dr["PromotionCode"].ToString() == "CROWNEPLAZAROHINI")
                {
                    tktAmount = decimal.Parse(dr["PayableAmount"].ToString());
                }

                MailBod.Append("Amount : " + tktAmount + "<br/>");
                MailBod.Append("<br/><br/>");
                MailBod.Append("Regard ," + "<br/>");
                MailBod.Append("Team  Kingdom of Dreams" + "<br/>");
                MailBod.Append("For any queries contact Customer Support" + "<br/>");
                MailBod.Append("Tel:- 0124-4528000/ 01246677000 Email Id: info@kingdomofdreams.co.in" + "<br/>");
                SendMailToCustomerSupport(MailBod, dr["EmailId"].ToString());
                SendMailToCustomerSupport(MailBod, "kod.quality@gmail.com");
                //SendMailToCustomerSupport(MailBod, "comcenter@kingdomofdreams.co.in");
                //SendMailToCustomerSupport(MailBod, "rashi.patra@kingdomofdreams.co.in");
                SendMailToCustomerSupport(MailBod, "failure.web.kod@gmail.com");
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Email Notification Error: " + ex.Message);
            }
        }

        public static void FailurePaymentResponse(DataRow dr)
        {

            try
            {
                String subject;
                System.Text.StringBuilder MailBod = new System.Text.StringBuilder("<b><u>This is a system generated message.</u></b><br/>");
                MailBod.Append("Dear Sir/Madam,<br/>");
                MailBod.Append("System has encountered an error while web-booking and the corresponding information is given below.<br/><br/>");

                subject = "BOOKING ERROR - " + dr["Name"].ToString();
                MailBod.Append("<b>User Details :</b><br/>");
                MailBod.Append("Name          : <b>" + dr["Name"] + "</b><br/>");
                MailBod.Append("Contact No    : <b>" + dr["MobileNo"] + "</b><br/>");
                MailBod.Append("Email Address : <b>" + dr["EmailId"] + "</b><br/>");
                MailBod.Append("Booking Date  : <b>" + Convert.ToDateTime(dr["DateOfBooking"].ToString()).ToLongDateString() +
                    " at " + Convert.ToDateTime(dr["TimeOfBooking"]).ToShortTimeString() + "</b><br/>");
                MailBod.Append("<hr/>");
                MailBod.Append("<b>Show/Seats Selection Details :</b><br/>");
                MailBod.Append("Show               : <b>" + dr["Play"] + "</b><br/>");
                MailBod.Append("Show Date          : <b>" + Convert.ToDateTime(dr["ShowDate"].ToString()).ToLongDateString() +
                    " at " + Convert.ToDateTime(dr["ShowTime"]).ToShortTimeString() + "</b><br/>");
                MailBod.Append("Category           : <b>" + dr["Category"] + "</b><br/>");
                MailBod.Append("Total No. of Seats : <b>" + dr["TotalSeats"] + "</b><br/>");
                MailBod.Append("Seat Details       : <b>" + dr["SeatInfo"] + "</b><br/>");
                MailBod.Append("<hr/><br/>");
                //SendMailToCustomerSupport(MailBod);
                //SendMailToCustomerSupport(MailBod, "comcenter@kingdomofdreams.co.in");
                //SendMailToCustomerSupport(MailBod, "rashi.patra@kingdomofdreams.co.in");
                SendMailToCustomerSupport(MailBod, "failure.web.kod@gmail.com");
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Failure Response generation error: " + ex.Message);
            }
        }

        public static void SuccessPaymentResponse(String _ReceiptNo, DataRow dr, String email)
        {
            try
            {
                String Enddate = "2013.09.29";
                DateTime End = Convert.ToDateTime(Enddate);
                String Presentdate = Convert.ToDateTime(dr["ShowDate"]).ToString("yyyy-MM-dd");
                DateTime Present = Convert.ToDateTime(Presentdate);
                System.Text.StringBuilder MailBod = new System.Text.StringBuilder();
                MailBod.Append("Dear ");
                MailBod.Append(dr["Name"]);
                MailBod.Append("<br/><br/>");
                MailBod.Append("Your Payment was successful, but due to some technical reason your seats were not Booked.");
                MailBod.Append(" Please contact customer care at 0124 - 4528000  for further assistance. Below are the details...<br/><br/>");
                MailBod.Append("Booking Id : " + dr["BookingID"] + "<br/>");
                if (dr["PromotionCode"].ToString() != "")
                    MailBod.Append("Promo Code : " + dr["PromotionCode"] + "<br/>");
                MailBod.Append("Receipt No : " + _ReceiptNo + "<br/>");
                MailBod.Append("Name          : <b>" + dr["Name"] + "</b><br/>");
                MailBod.Append("Contact No    : <b>" + dr["MobileNo"] + "</b><br/>");
                MailBod.Append("Email Address : <b>" + dr["EmailId"] + "</b><br/>");
                MailBod.Append("Booking Date  : <b>" + Convert.ToDateTime(dr["DateOfBooking"].ToString()).ToLongDateString() +
                    " at " + Convert.ToDateTime(dr["TimeOfBooking"]).ToShortTimeString() + "</b><br/>");
                MailBod.Append("Show               : <b>" + dr["Play"] + "</b><br/>");
                MailBod.Append("Show Date          : <b>" + Convert.ToDateTime(dr["ShowDate"].ToString()).ToLongDateString() +
                    " at " + Convert.ToDateTime(dr["ShowTime"]).ToShortTimeString() + "</b><br/>");
                MailBod.Append("Category           : <b>" + dr["Category"] + "</b><br/>");
                MailBod.Append("Total No. of Seats : <b>" + dr["TotalSeats"] + "</b><br/>");
                MailBod.Append("Seat Details       : <b>" + dr["SeatInfo"] + "</b><br/>");
                decimal DiscountPercentage = decimal.Parse(dr["DiscountPercentage"].ToString());
                decimal tktAmount = decimal.Parse(dr["TotalAmount"].ToString());
                int numberOfSeats = int.Parse(dr["TotalSeats"].ToString());

                if (DiscountPercentage > 0)
                {

                    decimal SingleTicketPrice = tktAmount / numberOfSeats;

                    decimal DiscountedPrice = SingleTicketPrice - (SingleTicketPrice * DiscountPercentage / 100);
                    DiscountedPrice = decimal.Truncate(DiscountedPrice);
                    if (DiscountedPrice == 1274)
                        DiscountedPrice = DiscountedPrice + 1;
                    else if (DiscountedPrice == 2124)
                        DiscountedPrice = DiscountedPrice + 1;
                    else if (DiscountedPrice == 2974)
                        DiscountedPrice = DiscountedPrice + 1;
                    else if (DiscountedPrice == 4249)
                        DiscountedPrice = DiscountedPrice + 1;

                    tktAmount = DiscountedPrice * numberOfSeats;

                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Total Amount Price For a Ticket" + tktAmount.ToString());
                }
                if (Convert.ToDecimal(dr["TotalSeats"].ToString()) > 3 && dr["Play"].ToString() == "JHUMROO" && dr["Category"].ToString() != "BRONZE" && dr["Category"].ToString() != "COPPER" && Present <= End && (dr["PromotionCode"].ToString() == "" || dr["PromotionCode"] == null))
                {
                    tktAmount = decimal.Parse(dr["PayableAmount"].ToString());
                }
                if (dr["PromotionCode"].ToString() == "VIVANTABYTAJ" || dr["PromotionCode"].ToString() == "OBEROI" || dr["PromotionCode"].ToString() == "TRIDENT" || dr["PromotionCode"].ToString() == "OBEROIDELHI" || dr["PromotionCode"].ToString() == "MMTDOMESTIC" || dr["PromotionCode"].ToString() == "EROSMANAGED" || dr["PromotionCode"].ToString() == "MCOTHERS" || dr["PromotionCode"].ToString() == "MMT" || dr["PromotionCode"].ToString() == "MANA" || dr["PromotionCode"].ToString() == "CROWNEPLAZAROHINI")
                {
                    tktAmount = decimal.Parse(dr["PayableAmount"].ToString());
                }

                MailBod.Append("Total Amount : " + tktAmount + "<br/>");

                MailBod.Append("<br/><br/>");
                MailBod.Append("Regard ," + "<br/>");
                MailBod.Append("Team  Kingdom of Dreams" + "<br/>");
                MailBod.Append("For any queries contact Customer Support" + "<br/>");
                MailBod.Append("Tel:- 0124-4528000/ 01246677000 Email Id: info@kingdomofdreams.co.in" + "<br/>");
                SendMailToCustomer(dr["EmailId"].ToString(), dr["Name"].ToString(), MailBod);
                SendMailToCustomer("comcenter@kingdomofdreams.co.in","", MailBod);
                SendMailToCustomer("rashi.patra@kingdomofdreams.co.in", "", MailBod);
                SendMailToCustomer("failure.web.kod@gmail.com", "", MailBod);
                SendMailToCustomer("rajeev@kingdomofdreams.co.in", "", MailBod);
                SendMailToCustomer("manish.kishore@kingdomofdreams.co.in", "", MailBod);
                SendMailToCustomer("kod.quality@gmail.com", "", MailBod);
                //BMess.Append("BookingID: " + _BookingID + ", and receiptNo: " + _ReceiptNo + ", Please Contact 0124 - 4528000 for Seat Confirmation");
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Email Notification Error: " + ex.Message);
            }
        }
        public static void DSuccessPaymentResponse(String _ReceiptNo, DataRow dr, String email)
        {
            try
            {
                System.Text.StringBuilder MailBod = new System.Text.StringBuilder();
                MailBod.Append("Dear ");
                MailBod.Append(dr["Name"]);
                MailBod.Append("<br/><br/>");
                MailBod.Append("Your Payment was successful, but due to some technical reason your seats were not Booked.");
                MailBod.Append(" Please contact customer care at 0124 - 4528000  for further assistance. Below are the details...<br/><br/>");
                MailBod.Append("Booking Id : " + dr["BookingId"] + "<br/>");
                MailBod.Append("Promo Code : " + "Dandiya night" + "<br/>");
                MailBod.Append("Name          : <b>" + dr["Name"] + "</b><br/>");
                MailBod.Append("Contact No    : <b>" + dr["ContactNumber"] + "</b><br/>");
                MailBod.Append("Email Address : <b>" + dr["EmailId"] + "</b><br/>");
                MailBod.Append("Booking Date  : <b>" + Convert.ToDateTime(dr["DateOfBooking"].ToString()).ToLongDateString() + "</b><br/>");
                MailBod.Append("Amount : " + dr["TotalAmount"] + "<br/>");
                MailBod.Append("<br/><br/>");
                MailBod.Append("Regard ," + "<br/>");
                MailBod.Append("Team  Kingdom of Dreams" + "<br/>");
                MailBod.Append("For any queries contact Customer Support" + "<br/>");
                MailBod.Append("Tel:- 0124-4528000/ 01246677000 Email Id: info@kingdomofdreams.co.in" + "<br/>");
                SendMailToCustomer(dr["EmailId"].ToString(), dr["Name"].ToString(), MailBod);
                SendMailToCustomer("comcenter@kingdomofdreams.co.in", "", MailBod);
                SendMailToCustomer("rashi.patra@kingdomofdreams.co.in", "", MailBod);
                SendMailToCustomer("failure.web.kod@gmail.com", "", MailBod);
                //System.Text.StringBuilder BMess = new System.Text.StringBuilder();
                //BMess.Append("BookingID: " + _BookingID + ", and receiptNo: " + _ReceiptNo + ", Please Contact 0124 - 4528000 for Seat Confirmation");
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Email Notification Error: " + ex.Message);
            }
        }

        public static void JHUMROOOFFERPaymentResponse(bool seatsBooked, DataRow dr, String _ReferenceNo, String _BookingID, String _ReceiptNo, String AdminId)
        {
            AdminId = "kod.quality@gmail.com";  //mail id for quality department
            try
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("JhumrooOffer Mail Content ");
                String Enddate = "2014.04.09";
                DateTime End = Convert.ToDateTime(Enddate);
                String Presentdate = Convert.ToDateTime(dr["ShowDate"]).ToString("yyyy-MM-dd");
                DateTime Present = Convert.ToDateTime(Presentdate);
                System.Text.StringBuilder BMess = new System.Text.StringBuilder();
                BMess.Append("Dear ");
                BMess.Append(dr["Name"]);
                BMess.Append("<br/><br/>");
                if (seatsBooked)
                {
                    BMess.Append("We thank you for booking the tickets for <b>Jhumroo</b>. Please print this mail to claim your tickets from the Box Office at Kingdom of Dreams.<br/><br/>");
                    BMess.Append("Booking ID : " + dr["BookingID"] + "<br/>");
                }
                else
                {
                    BMess.Append("Your Transaction was successful, but due to some technical reason your seats were not Booked.");
                    BMess.Append(" Please Contact 0124 - 4528000 to confirm your seats. Below are the details...<br/><br/>");
                    BMess.Append("Booking ID : " + _BookingID + "<br/>");
                    BMess.Append("Receipt No : " + _ReceiptNo + "<br/>");
                }


                //Promotion Code Specific Code
                decimal DiscountPercentage = decimal.Parse(dr["DiscountPercentage"].ToString());
                decimal tktAmount = decimal.Parse(dr["TotalAmount"].ToString());
                int numberOfSeats = int.Parse(dr["TotalSeats"].ToString());
                int ticketqty = numberOfSeats / 2;

                if (DiscountPercentage > 0)
                {

                    decimal SingleTicketPrice = tktAmount / numberOfSeats;

                    decimal DiscountedPrice = SingleTicketPrice - (SingleTicketPrice * DiscountPercentage / 100);
                    DiscountedPrice = decimal.Truncate(DiscountedPrice);
                    if (DiscountedPrice == 1274)
                        DiscountedPrice = DiscountedPrice + 1;
                    else if (DiscountedPrice == 2124)
                        DiscountedPrice = DiscountedPrice + 1;
                    else if (DiscountedPrice == 2974)
                        DiscountedPrice = DiscountedPrice + 1;
                    else if (DiscountedPrice == 4249)
                        DiscountedPrice = DiscountedPrice + 1;

                    tktAmount = DiscountedPrice * numberOfSeats;

                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Total Amount Price For a Ticket" + tktAmount.ToString());
                }
                if (dr["PromotionCode"].ToString() == "JHUMROOOFFER")
                {
                    tktAmount = decimal.Parse(dr["PayableAmount"].ToString());
                }

                BMess.Append("Guest Name : " + dr["Name"] + "<br/>");
                BMess.Append("Show Name : " + dr["Play"] + "<br/>");
                BMess.Append("Show Date : " + Convert.ToDateTime(dr["ShowDate"].ToString()).ToLongDateString() + "<br/>");
                BMess.Append("Show Time : " + Convert.ToDateTime(dr["ShowTime"]).ToShortTimeString() + "<br/>");
                BMess.Append("Seat Info : " + dr["SeatInfo"] + "<br/>");
                BMess.Append("Tickets Qty : " + Convert.ToInt32(ticketqty) + "+" + Convert.ToInt32(ticketqty) + " <br/>");
                BMess.Append("Category : " + dr["Category"] + "<br/>");

                if (dr["PromotionCode"].ToString() == "JHUMROOOFFER")
                {
                    BMess.Append("Total Amount : " + tktAmount + "<br/>");
                }
                BMess.Append("Jhumroo Offer ID  : " + dr["BookingID"] + "<br/><br/>");
                if (seatsBooked)
                {
                    if (dr["PromotionCode"].ToString() == "JHUMROOOFFER")
                    {
                        BMess.Append("<b><u> Admission Rights</u> :" + "</b><br/>" +
                            " 1.Infants or Children below 2 years are strictly not allowed inside the theater." + "<br/>" +
                            " 2.Please carry the Credit Card used for booking tickets .or its copy for verification." + "<br/>" +
                            "3.Entry should be made half an hour before the show starts or else it will be restricted." + "<br/>" +
                            "4.As per policy we do not cancel/refund/change any tickets once booked." + "<br/>" +
                            "5.If you are a Royal Card Member, Make your payment only through your Royal Membership Card to earn reward points." + "<br/>" +
                            "No claim will be entertained after printing of the tickets." + "<br/>" +
                            "6.The Rewards point earned will be available for redemption only on next visit." + "<br/><br/>" +
                            "We wish you a pleasant experience at Kingdom of Dreams." + "<br/><br/>");
                    }
                }
                BMess.Append("<b>Best Compliments" + "<br/>Kingdom of Dreams</b>");
                if (Convert.ToBoolean(dr["IsChecked"]) == true)
                {
                    if (SendMailToCustomer(dr["OptionalEmail"].ToString(), dr["Name"].ToString(), BMess))
                        KoDTicketing.GTICKV.LogEntry(_ReferenceNo.ToString(), "Mail Sent", "23", dr["BookingID"].ToString());
                }
                if (Convert.ToBoolean(dr["IsChecked"]) == false)
                {
                    if (SendMailToCustomer(dr["EmailID"].ToString(), dr["Name"].ToString(), BMess))
                        KoDTicketing.GTICKV.LogEntry(_ReferenceNo.ToString(), "Mail Sent", "23", dr["BookingID"].ToString());
                    if (dr["OptionalEmail"].ToString() != "")
                    {
                        if (SendMailToCustomer(dr["OptionalEmail"].ToString(), dr["Name"].ToString(), BMess))
                            KoDTicketing.GTICKV.LogEntry(_ReferenceNo.ToString(), "Mail Sent", "23", dr["BookingID"].ToString());
                    }

                }
                if (AdminId != "")
                {
                    if (SendMailToCustomer(AdminId.ToString(), "Admin", BMess))
                        KoDTicketing.GTICKV.LogEntry(_ReferenceNo.ToString(), "Mail Sent", "23", dr["BookingID"].ToString());
                }
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Email Notification Error: " + ex.Message);
            }

            try
            {
                string promo = dr["PromotionCode"].ToString();
                if (promo == "JHUMROOOFFER")
                    SendSignUpSMS(dr["Name"].ToString(), dr["BookingId"].ToString(), dr["MobileNo"].ToString(), dr["TotalSeats"].ToString(), dr["Play"].ToString(), Convert.ToDateTime(dr["ShowDate"].ToString()), Convert.ToDateTime(dr["ShowTime"].ToString()));
                KoDTicketing.GTICKV.LogEntry(_ReferenceNo.ToString(), "SMS Sent", "24", dr["BookingID"].ToString());
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("SMS Notification Error: " + ex.Message);
            }
        }

        public static void SuccessPaymentResponse(bool seatsBooked, DataRow dr, String _ReferenceNo, String _BookingID, String _ReceiptNo, String AdminId)
        {
            AdminId = "kod.quality@gmail.com";  //mail id for quality department
            try
            {
                String Enddate = "2013.09.29";
                DateTime End = Convert.ToDateTime(Enddate);
                String Presentdate = Convert.ToDateTime(dr["ShowDate"]).ToString("yyyy-MM-dd");
                DateTime Present = Convert.ToDateTime(Presentdate);
                System.Text.StringBuilder BMess = new System.Text.StringBuilder();
                BMess.Append("Dear ");
                BMess.Append(dr["Name"]);
                BMess.Append("<br/><br/>");
                if (seatsBooked)
                {
                    BMess.Append("Your Tickets have been successfully booked, and details are mentioned below...<br/><br/>");
                    BMess.Append("Booking ID : " + dr["BookingID"] + "<br/>");
                    if (dr["PromotionCode"].ToString() == "OCTOBERFEST")
                        BMess.Append("Promo Code : " + "OCTOBER FEST (Buy one get one free)" + "<br/>");
                }
                else
                {
                    BMess.Append("Your Transaction was successful, but due to some technical reason your seats were not Booked.");
                    BMess.Append(" Please Contact 0124 - 4528000 to confirm your seats. Below are the details...<br/><br/>");
                    BMess.Append("Booking Id : " + _BookingID + "<br/>");
                    BMess.Append("Receipt No : " + _ReceiptNo + "<br/>");
                }


                //Promotion Code Specific Code
                decimal DiscountPercentage = decimal.Parse(dr["DiscountPercentage"].ToString());
                decimal tktAmount = decimal.Parse(dr["TotalAmount"].ToString());
                int numberOfSeats = int.Parse(dr["TotalSeats"].ToString());

                if (DiscountPercentage > 0)
                {

                    decimal SingleTicketPrice = tktAmount / numberOfSeats;

                    decimal DiscountedPrice = SingleTicketPrice - (SingleTicketPrice * DiscountPercentage / 100);
                    DiscountedPrice = decimal.Truncate(DiscountedPrice);
                    if (DiscountedPrice == 1274)
                        DiscountedPrice = DiscountedPrice + 1;
                    else if (DiscountedPrice == 2124)
                        DiscountedPrice = DiscountedPrice + 1;
                    else if (DiscountedPrice == 2974)
                        DiscountedPrice = DiscountedPrice + 1;
                    else if (DiscountedPrice == 4249)
                        DiscountedPrice = DiscountedPrice + 1;

                    tktAmount = DiscountedPrice * numberOfSeats;

                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Total Amount Price For a Ticket" + tktAmount.ToString());
                    //*************Jhumroo Offer (1+1) mail content changes START here*******************
                    //String Enddatte = "2014.04.10";
                    //DateTime Endt = Convert.ToDateTime(Enddatte);
                    //String Presentdatte = Convert.ToDateTime(dr["ShowDate"]).ToString("yyyy-MM-dd");
                    //DateTime Presentt = Convert.ToDateTime(Presentdatte);
                    //if (dr["Play"].ToString() == "JHUMROO" && (dr["Category"].ToString() == "SILVER" || dr["Category"].ToString() == "GALLERY" || dr["Category"].ToString() == "GOLD" || dr["Category"].ToString() == "DIAMOND" || dr["Category"].ToString() == "PLATINUM") && Presentt <= Endt && (dr["PromotionCode"].ToString() == "" || dr["PromotionCode"] == null))
                    //{
                    //    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Enter into Jhumroo offer");
                    //    tktAmount = decimal.Parse(dr["PayableAmount"].ToString());
                    //}
                    //*************Jhumroo Offer (1+1) mail content changes END here*******************

                }
                if (Convert.ToDecimal(dr["TotalSeats"].ToString()) > 3 && dr["Play"].ToString() == "JHUMROO" && dr["Category"].ToString() != "BRONZE" && dr["Category"].ToString() != "COPPER" && Present <= End && (dr["PromotionCode"].ToString() == "" || dr["PromotionCode"] == null))
                {
                    tktAmount = decimal.Parse(dr["TotalAmount"].ToString());
                }
                if (dr["PromotionCode"].ToString() == "VIVANTABYTAJ" || dr["PromotionCode"].ToString() == "OBEROI" || dr["PromotionCode"].ToString() == "TRIDENT" || dr["PromotionCode"].ToString() == "OBEROIDELHI" || dr["PromotionCode"].ToString() == "EROSMANAGED" || dr["PromotionCode"].ToString() == "OCTOBERFEST" || dr["PromotionCode"].ToString() == "CROWNEPLAZAROHINI")
                {
                    tktAmount = decimal.Parse(dr["PayableAmount"].ToString());
                }
               

                HotelList<string> PromoClass = new HotelList<string>();
                List<string> Promotions = PromoClass.listofHotels();

                if (dr["PromotionCode"].ToString() != "" || dr["PromotionCode"] != null)
                {
                    if (Promotions.Contains(dr["PromotionCode"].ToString()))
                    {
                        BMess.Append("Promo Code : " + dr["PromotionCode"].ToString() + "<br/>");
                    }
                    if (Convert.ToBoolean(dr["WantComplimentary"].ToString()) == true || Convert.ToBoolean(dr["WantComplimentaryDrop"].ToString()) == true)
                    {
                        BMess.Append("Contact No. :" + dr["MobileNo"] + "<br/>");

                        if (Convert.ToBoolean(dr["WantComplimentary"].ToString()) == true)
                        {
                            BMess.Append("Place Of Pick :" + dr["PlaceOfPick"] + "<br/>");
                            BMess.Append("Time Of Pick :" + dr["TimeOfPick"] + "<br/>");
                        }
                        if (Convert.ToBoolean(dr["WantComplimentaryDrop"].ToString()) == true)
                        {
                            BMess.Append("Place Of Pick For Drop :" + dr["PlaceOfDrop"] + "<br/>");
                            BMess.Append("Time Of Pick For Drop :" + dr["TimeOfDrop"] + "<br/>");
                        }
                    }
                }
                


                ////////
                //if (dr["PromotionCode"].ToString() == "PULMAN")
                //{
                //    if (Convert.ToBoolean(dr["WantComplimentary"].ToString()) == true)
                //    {
                //        BMess.Append("Place Of Pick :" + dr["PlaceOfPick"] + "<br/>");
                //        BMess.Append("Time Of Pick :" + dr["TimeOfPick"] + "<br/>");
                //        BMess.Append("Contact No. :" + dr["MobileNo"] + "<br/>");
                //    }
                //    BMess.Append("Promo Code : Pullman Hotel <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "EROSMANAGED")
                //{
                //    BMess.Append("Promo Code : Eros managed by Hilton  <br/>");
                //}

                //if (dr["PromotionCode"].ToString() == "GINGERROOTS")
                //{
                //    BMess.Append("Promo Code : Ginger Roots  <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "OPTUSSAROVAR")
                //{
                //    BMess.Append("Promo Code : Optus Sarovar Premiere <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "HERITAGEVILLAGE")
                //{
                //    BMess.Append("Promo Code : Heritage Village Resort & Spa     <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "THEUPPAL")
                //{
                //    BMess.Append("Promo Code : The Uppal     <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "FORTUNEDJPARKAVENUE")
                //{
                //    BMess.Append("Promo Code : Fortune Dj Park Avenue    <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "KEYSHOTEL")
                //{
                //    BMess.Append("Promo Code : Keys Hotel Chattarpur   <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "KEYOODLESNP")
                //{
                //    BMess.Append("Promo Code : Key oodles Nehru Place  <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "MAPPLEEMERALD")
                //{
                //    BMess.Append("Promo Code : Mapple Emerald <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "BRISTOL")
                //{
                //    BMess.Append("Promo Code : BRISTOL <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "MAPPLEEXOTICA")
                //{
                //    BMess.Append("Promo Code : MAPPLE EXOTICA <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "THESUNRISEHOUSE")
                //{
                //    BMess.Append("Promo Code : THE SUN RISE HOUSE <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "GALAXY")
                //{
                //    BMess.Append("Promo Code : GALAXY <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "ROYALPARKPLAZA")
                //{
                //    BMess.Append("Promo Code : The Royal Park Plaza <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "THEROYALPARK")
                //{
                //    BMess.Append("Promo Code : The Royal Park <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "STARGRANDVILLA")
                //{
                //    BMess.Append("Promo Code : STAR GRAND VILLA <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "RAMADAGURGAONCENTRAL")
                //{
                //    BMess.Append("Promo Code : Ramada Gurgaon Central <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "COUNTRYINN")
                //{
                //    BMess.Append("Promo Code : Country Inn & Suites <br/>");
                //}

                //if (dr["PromotionCode"].ToString() == "DOUBLETREE")
                //{
                //    BMess.Append("Promo Code : Double Tree by Hilton <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "ITCMAURYA")
                //{
                //    BMess.Append("Promo Code : ITC Maurya <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "SONAHOSPITALITY")
                //{
                //    BMess.Append("Promo Code : Sona Hospitality <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "PLLAZIO")
                //{
                //    BMess.Append("Promo Code : PLLAZIO <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "KODHDFCEMPMAR13")
                //{
                //    BMess.Append("Promo Code : HDFC <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "BESTWESTERN")
                //{
                //    BMess.Append("Promo Code : BEST WESTERN <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "COUNTRYINNSEC12")
                //{
                //    BMess.Append("Promo Code : COUNTRY INN SEC12 <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "KINGSTON")
                //{
                //    BMess.Append("Promo Code : KINGSTON <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "VATIKA")
                //{
                //    BMess.Append("Promo Code : VATIKA <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "COUNTRYINN&SUIT")
                //{
                //    BMess.Append("Promo Code :  Country Inn & Suites, Chattarpur <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "MADHUBAN")
                //{
                //    BMess.Append("Promo Code :  Madhuban, managed by Peppermint, Greater Kailash <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "VIVANTABYTAJ")
                //{
                //    BMess.Append("Promo Code :    Vivanta by Taj <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "OBEROI")
                //{
                //    BMess.Append("Promo Code : Oberoi <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "OBEROIDELHI")
                //{
                //    BMess.Append("Promo Code : Oberoi delhi <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "TRIDENT")
                //{
                //    BMess.Append("Promo Code : Trident <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "THEUMRAO")
                //{
                //    BMess.Append("Promo Code : THEUMRAO <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "MARCHPROMOTION")
                //{
                //    BMess.Append("Promo Code : March Promotion <br/>");
                //    BMess.Append("Amount of Per Package :" + "Rs.4999" + "<br/>");
                //    BMess.Append("Category :" + "GOLD" + "<br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "MONTHOFMARCH")
                //{
                //    BMess.Append("Promo Code : Month of March <br/>");
                //    BMess.Append("Amount of Per Package :" + "Rs.1275" + "<br/>");
                //    BMess.Append("Category :" + "SILVER" + "<br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "LEELAKEMPINSKI")
                //{
                //    if (Convert.ToBoolean(dr["WantComplimentary"].ToString()) == true)
                //    {
                //        BMess.Append("Place Of Pick :" + dr["PlaceOfPick"] + "<br/>");
                //        BMess.Append("Time Of Pick :" + dr["TimeOfPick"] + "<br/>");
                //        BMess.Append("Contact No. :" + dr["MobileNo"] + "<br/>");
                //    }
                //    BMess.Append("Promo Code : Leela Kempinski  <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "THECLAREMONT")
                //{
                //    BMess.Append("Promo Code : THE CLAREMONT <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "ROYALRAMIRORESIDENCY")
                //{
                //    BMess.Append("Promo Code : ROYAL RAMIRO RESIDENCY <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "RADISSONBLUSUITES")
                //{
                //    BMess.Append("Promo Code : RADISSON BLU SUITES <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "KARONINN")
                //{
                //    BMess.Append("Promo Code : KARON INN <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "GOLDENTULIP")
                //{
                //    BMess.Append("Promo Code : GOLDEN TULIP <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "HOTELOSCAR")
                //{
                //    BMess.Append("Promo Code : HOTEL OSCAR <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "KARONHOTELS")
                //{
                //    BMess.Append("Promo Code : KARON HOTELS <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "MMTDOMESTIC")
                //{
                //    BMess.Append("Promo Code : MMT domestic <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "CROWNPLAZA")
                //{
                //    if (Convert.ToBoolean(dr["WantComplimentary"].ToString()) == true || Convert.ToBoolean(dr["WantComplimentaryDrop"].ToString()) == true)
                //    {
                //        BMess.Append("Contact No. :" + dr["MobileNo"] + "<br/>");
                //    }
                //    if (Convert.ToBoolean(dr["WantComplimentary"].ToString()) == true)
                //    {
                //        BMess.Append("Place Of Pick Up :" + dr["PlaceOfPick"] + "<br/>");
                //        BMess.Append("Time Of Pick Up :" + dr["TimeOfPick"] + "<br/>");
                //    }
                //    if (Convert.ToBoolean(dr["WantComplimentaryDrop"].ToString()) == true)
                //    {
                //        BMess.Append("Place Of Pick Up For Drop :" + dr["PlaceOfDrop"] + "<br/>");
                //        BMess.Append("Time Of Pick Up For Drop :" + dr["TimeOfDrop"] + "<br/>");
                //    }

                //    BMess.Append("Promo Code : CROWNPLAZA HOTEL <br/>");
                //}
                if (Convert.ToDecimal(dr["TotalSeats"].ToString()) > 3 && dr["Play"].ToString() == "JHUMROO" && dr["Category"].ToString() != "BRONZE" && dr["Category"].ToString() != "COPPER" && Present <= End && (dr["PromotionCode"].ToString() == "" || dr["PromotionCode"] == null))
                {
                    BMess.Append("Promo Offer : " + "Jhoomro Anniversary offer" + "<br/>");
                }
                BMess.Append("Venue : Kingdom Of Dreams, Gurgaon<br/>");
                BMess.Append("Location : NCR<br/>");
                BMess.Append("Show Name : " + dr["Play"] + "<br/>");
                BMess.Append("Show Date : " + Convert.ToDateTime(dr["ShowDate"].ToString()).ToLongDateString() +
                    " at " + Convert.ToDateTime(dr["ShowTime"]).ToShortTimeString() + "<br/>");
                BMess.Append("Seat Info : " + dr["Category"] + " - " + dr["SeatInfo"] + "<br/>");

                if (dr["PromotionCode"].ToString() != "MONTHOFMARCH" && dr["PromotionCode"].ToString() != "MARCHPROMOTION")
                {
                    BMess.Append("Total Amount : " + tktAmount + "<br/><br/>");

                }
                if (Convert.ToDecimal(dr["TotalSeats"].ToString()) > 3 && dr["Play"].ToString() == "JHUMROO" && dr["Category"].ToString() != "BRONZE" && dr["Category"].ToString() != "COPPER" && Present <= End && (dr["PromotionCode"].ToString() == "" || dr["PromotionCode"] == null))
                {
                    BMess.Append("Paid Amount : " + dr["PayableAmount"].ToString() + " (Buy three get one free - Jhoomro anniversary offer)<br/>");
                }
                BMess.Append("Payment Mode : " + dr["PaymentType"] + "<br/>");
                BMess.Append("Booking Date : " + Convert.ToDateTime(dr["DateOfBooking"].ToString()).ToLongDateString() +
                    " at " + Convert.ToDateTime(dr["TimeOfBooking"]).ToShortTimeString() + "<br/><br/>");

                if (dr["PromotionCode"].ToString() == "MONTHOFMARCH")
                {
                    BMess.Append("Package Details : Silver Category Show Ticket for Jhumroo, Welcome Drink, Culture Gully Smart Card Worth Rs.500 per person, 10% Discount on Retail Purchase at Culture Gully.<br/>");
                }
                if (dr["PromotionCode"].ToString() == "MARCHPROMOTION")
                {
                    BMess.Append("Gold Category Show Ticket for Jhumroo, Welcome Drink, Culture Gully Smart Card Worth Rs.2500 per couple, 10% Discount on Retail Purchase at Culture Gully.<br/> ");
                }

                if (seatsBooked)
                {
                    if (dr["PromotionCode"].ToString() == "MONTHOFMARCH" || dr["PromotionCode"].ToString() == "MARCHPROMOTION")
                    {
                        BMess.Append("Please bring this Booking ID along with you to collect your tickets" +
                            " and also you need to present the same credit card on which the booking has" +
                            " been done, if tickets booked with credit card." +
                            " Collection of tickets and smart cards will be on the same day for which you have booked your package.<br/><br/>" +
                            "<b>Terms & conditions :" + "<br/>" +
                            " 1.A child less than two years of age, will not be allowed for the show due to high decibel level. Price of a show ticket for a child above two years would be same as for adults for all categories."+"<br/>"+
                            " 2.Make your Payment only through your Royal Membership Card to earn reward points. No claim will be entertained after printing of the tickets." + "<br/>" +
                            " 3.The Rewards point earned will be available for redemption only on next visit." + "<b><br/><br/>" +
                            "<b>CANCELLATION/REFUND POLICY<b/><br/>As per policy we do not cancel/refund/change any tickets once booked." +
                            "<br/><br/>");
                    }
                    else
                    {
                        BMess.Append("Please Bring this Booking ID to the Box-office of Kingdom of Dreams to collect your tickets" +
                            " and also you need to present the same credit card on which the booking has" +
                            " been done, if tickets booked with credit card.<br/><br/>" +
                            " <b>Terms & conditions :" + "<br/>" +
                            " 1.A child less than two years of age, will not be allowed for the show due to high decibel level. Price of a show ticket for a child above two years would be same as for adults for all categories." + "<br/>" +
                            " 2.Make your Payment only through your Royal Membership Card to earn reward points. No claim will be entertained after printing of the tickets." + "<br/>" +
                            " 3.The Rewards point earned will be available for redemption only on next visit." + "<b/><br/><br/>" +
                            "<b>CANCELLATION/REFUND POLICY<b/><br/>As per policy we do not cancel/refund/change any tickets once booked." +
                            "<br/><br/>");
                    }
                }
                BMess.Append("Regards,<br/>Team<br/>Kingdom of Dreams");
                if (Convert.ToBoolean(dr["IsChecked"]) == true)
                {
                    if (SendMailToCustomer(dr["OptionalEmail"].ToString(), dr["Name"].ToString(), BMess))
                        KoDTicketing.GTICKV.LogEntry(_ReferenceNo.ToString(), "Mail Sent", "23", dr["BookingID"].ToString());
                }
                if (Convert.ToBoolean(dr["IsChecked"]) == false)
                {
                    if (SendMailToCustomer(dr["EmailID"].ToString(), dr["Name"].ToString(), BMess))
                        KoDTicketing.GTICKV.LogEntry(_ReferenceNo.ToString(), "Mail Sent", "23", dr["BookingID"].ToString());
                    if (dr["OptionalEmail"].ToString() != "")
                    {
                        if (SendMailToCustomer(dr["OptionalEmail"].ToString(), dr["Name"].ToString(), BMess))
                            KoDTicketing.GTICKV.LogEntry(_ReferenceNo.ToString(), "Mail Sent", "23", dr["BookingID"].ToString());
                    }

                }
                if (AdminId != "")
                {
                    if (SendMailToCustomer(AdminId.ToString(), "Admin", BMess))
                        KoDTicketing.GTICKV.LogEntry(_ReferenceNo.ToString(), "Mail Sent", "23", dr["BookingID"].ToString());
                }
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Email Notification Error: " + ex.Message);
            }

            try
            {
                string promo = dr["PromotionCode"].ToString();
                if (promo == "MMTDOMESTIC")
                    SendSignUpSMSForMMT(dr["Name"].ToString(), dr["BookingId"].ToString(), dr["MobileNo"].ToString(), dr["TotalSeats"].ToString(), dr["Play"].ToString(), Convert.ToDateTime(dr["ShowDate"].ToString()), Convert.ToDateTime(dr["ShowTime"].ToString()), dr["PromotionCode"].ToString());
                else
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Send SMS: " + dr["MobileNo"].ToString());
                SendSignUpSMS(dr["Name"].ToString(), dr["BookingId"].ToString(), dr["MobileNo"].ToString(), dr["TotalSeats"].ToString(), dr["Play"].ToString(), Convert.ToDateTime(dr["ShowDate"].ToString()), Convert.ToDateTime(dr["ShowTime"].ToString()));
                KoDTicketing.GTICKV.LogEntry(_ReferenceNo.ToString(), "SMS Sent", "24", dr["BookingID"].ToString());
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("SMS Notification Error: " + ex.Message);
            }
        }

        private static string PrepareContentMailForHotel(bool seatsBooked, DataRow dr, string _BookingID, string _ReferenceNo, string _ReceiptNo)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Entered to take Mail Content ");
            try
            {
                System.Text.StringBuilder BMess = new System.Text.StringBuilder();
                BMess.Append("Dear ");
                BMess.Append(dr["Name"]);
                BMess.Append("<br/><br/>");
                if (seatsBooked)
                {
                    BMess.Append("Your Tickets have been successfully booked, and details are mentioned below...<br/><br/>");
                    BMess.Append("Booking ID : " + dr["BookingID"] + "<br/>");
                }
                else
                {
                    BMess.Append("Your Transaction was successful, but due to some technical reason your seats were not Booked.");
                    BMess.Append(" Please Contact 0124 - 4528000 to confirm your seats. Below are the details...<br/><br/>");
                    BMess.Append("Booking Id : " + _BookingID + "<br/>");
                    BMess.Append("Receipt No : " + _ReceiptNo + "<br/>");
                }


                //Promotion Code Specific Code
                decimal DiscountPercentage = decimal.Parse(dr["DiscountPercentage"].ToString());
                decimal tktAmount = decimal.Parse(dr["TotalAmount"].ToString());
                int numberOfSeats = int.Parse(dr["TotalSeats"].ToString());
                if (DiscountPercentage > 0)
                {

                    decimal SingleTicketPrice = tktAmount / numberOfSeats;

                    decimal DiscountedPrice = SingleTicketPrice - (SingleTicketPrice * DiscountPercentage / 100);
                    DiscountedPrice = decimal.Truncate(DiscountedPrice);
                    if (DiscountedPrice == 1274)
                        DiscountedPrice = DiscountedPrice + 1;
                    else if (DiscountedPrice == 2124)
                        DiscountedPrice = DiscountedPrice + 1;
                    else if (DiscountedPrice == 2974)
                        DiscountedPrice = DiscountedPrice + 1;
                    else if (DiscountedPrice == 4249)
                        DiscountedPrice = DiscountedPrice + 1;

                    tktAmount = DiscountedPrice * numberOfSeats;

                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Total Amount Price For a Ticket" + tktAmount.ToString());


                }
                if (dr["PromotionCode"].ToString() == "VIVANTABYTAJ" || dr["PromotionCode"].ToString() == "OBEROI" || dr["PromotionCode"].ToString() == "TRIDENT" || dr["PromotionCode"].ToString() == "OBEROIDELHI" || dr["PromotionCode"].ToString() == "EROSMANAGED" || dr["PromotionCode"].ToString() == "CROWNEPLAZAROHINI")
                {
                    tktAmount = decimal.Parse(dr["PayableAmount"].ToString());
                }

                HotelList<string> PromoClass = new HotelList<string>();
                List<string> Promotions = PromoClass.listofHotels();

                if (dr["PromotionCode"].ToString() != "" || dr["PromotionCode"] != null)
                {
                    if (Promotions.Contains(dr["PromotionCode"].ToString()))
                    {
                        BMess.Append("Promo Code : " + dr["PromotionCode"].ToString() + "<br/>");
                    }
                    if (Convert.ToBoolean(dr["WantComplimentary"].ToString()) == true || Convert.ToBoolean(dr["WantComplimentaryDrop"].ToString()) == true)
                    {
                        BMess.Append("Contact No. :" + dr["MobileNo"] + "<br/>");

                        if (Convert.ToBoolean(dr["WantComplimentary"].ToString()) == true)
                        {
                            BMess.Append("Place Of Pick :" + dr["PlaceOfPick"] + "<br/>");
                            BMess.Append("Time Of Pick :" + dr["TimeOfPick"] + "<br/>");
                        }
                        if (Convert.ToBoolean(dr["WantComplimentaryDrop"].ToString()) == true)
                        {
                            BMess.Append("Place Of Pick For Drop :" + dr["PlaceOfDrop"] + "<br/>");
                            BMess.Append("Time Of Pick For Drop :" + dr["TimeOfDrop"] + "<br/>");
                        }
                    }
                }
                ////////
                //if (dr["PromotionCode"].ToString() == "PULMAN")
                //{
                //    if (Convert.ToBoolean(dr["WantComplimentary"].ToString()) == true)
                //    {
                //        BMess.Append("Place Of Pick :" + dr["PlaceOfPick"] + "<br/>");
                //        BMess.Append("Time Of Pick :" + dr["TimeOfPick"] + "<br/>");
                //        BMess.Append("Contact No. :" + dr["MobileNo"] + "<br/>");
                //    }
                //    BMess.Append("Promo Code : Pullman Hotel <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "EROSMANAGED")
                //{
                //    BMess.Append("Promo Code : Eros managed by Hilton  <br/>");
                //}

                //if (dr["PromotionCode"].ToString() == "GINGERROOTS")
                //{
                //    BMess.Append("Promo Code : Ginger Roots  <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "OPTUSSAROVAR")
                //{
                //    BMess.Append("Promo Code : Optus Sarovar Premiere <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "HERITAGEVILLAGE")
                //{
                //    BMess.Append("Promo Code : Heritage Village Resort & Spa     <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "THEUPPAL")
                //{
                //    BMess.Append("Promo Code : The Uppal     <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "FORTUNEDJPARKAVENUE")
                //{
                //    BMess.Append("Promo Code : Fortune Dj Park Avenue    <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "KEYSHOTEL")
                //{
                //    BMess.Append("Promo Code : Keys Hotel Chattarpur   <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "KEYOODLESNP")
                //{
                //    BMess.Append("Promo Code : Key oodles Nehru Place  <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "MAPPLEEMERALD")
                //{
                //    BMess.Append("Promo Code : Mapple Emerald <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "BRISTOL")
                //{
                //    BMess.Append("Promo Code : BRISTOL <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "MAPPLEEXOTICA")
                //{
                //    BMess.Append("Promo Code : MAPPLE EXOTICA <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "THESUNRISEHOUSE")
                //{
                //    BMess.Append("Promo Code : THE SUN RISE HOUSE <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "GALAXY")
                //{
                //    BMess.Append("Promo Code : GALAXY <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "RAMADAGURGAONCENTRAL")
                //{
                //    BMess.Append("Promo Code : Ramada Gurgaon Central <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "STARGRANDVILLA")
                //{
                //    BMess.Append("Promo Code : STAR GRAND VILLA <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "THEROYALPARK")
                //{
                //    BMess.Append("Promo Code : THE ROYAL PARK <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "ROYALPARKPLAZA")
                //{
                //    BMess.Append("Promo Code : The Royal Park Plaza <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "COUNTRYINN")
                //{
                //    BMess.Append("Promo Code : Country Inn & Suites <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "DOUBLETREE")
                //{
                //    BMess.Append("Promo Code : Double Tree by Hilton <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "ITCMAURYA")
                //{
                //    BMess.Append("Promo Code : ITC Maurya <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "SONAHOSPITALITY")
                //{
                //    BMess.Append("Promo Code : Sona Hospitality <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "PLLAZIO")
                //{
                //    BMess.Append("Promo Code : PLLAZIO <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "BESTWESTERN")
                //{
                //    BMess.Append("Promo Code : BEST WESTERN <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "COUNTRYINNSEC12")
                //{
                //    BMess.Append("Promo Code : COUNTRY INN SEC12 <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "KINGSTON")
                //{
                //    BMess.Append("Promo Code : KINGSTON <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "VATIKA")
                //{
                //    BMess.Append("Promo Code : VATIKA <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "COUNTRYINN&SUIT")
                //{
                //    BMess.Append("Promo Code :  Country Inn & Suites, Chattarpur <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "MADHUBAN")
                //{
                //    BMess.Append("Promo Code :  Madhuban, managed by Peppermint, Greater Kailash <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "VIVANTABYTAJ")
                //{
                //    BMess.Append("Promo Code :    Vivanta by Taj <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "OBEROI")
                //{
                //    BMess.Append("Promo Code : Oberoi <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "OBEROIDELHI")
                //{
                //    BMess.Append("Promo Code : Oberoi delhi  <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "TRIDENT")
                //{
                //    BMess.Append("Promo Code : Trident <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "THEUMRAO")
                //{
                //    BMess.Append("Promo Code : THEUMRAO <br/>");
                //}

                //if (dr["PromotionCode"].ToString() == "THECLAREMONT")
                //{
                //    BMess.Append("Promo Code : THE CLAREMONT <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "ROYALRAMIRORESIDENCY")
                //{
                //    BMess.Append("Promo Code : ROYAL RAMIRO RESIDENCY <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "RADISSONBLUSUITES")
                //{
                //    BMess.Append("Promo Code : RADISSON BLU SUITES <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "KARONINN")
                //{
                //    BMess.Append("Promo Code : KARON INN <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "GOLDENTULIP")
                //{
                //    BMess.Append("Promo Code : GOLDEN TULIP <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "HOTELOSCAR")
                //{
                //    BMess.Append("Promo Code : HOTEL OSCAR <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "KARONHOTELS")
                //{
                //    BMess.Append("Promo Code : KARON HOTELS <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "LEELAKEMPINSKI")
                //{
                //    if (Convert.ToBoolean(dr["WantComplimentary"].ToString()) == true)
                //    {
                //        BMess.Append("Place Of Pick :" + dr["PlaceOfPick"] + "<br/>");
                //        BMess.Append("Time Of Pick :" + dr["TimeOfPick"] + "<br/>");
                //        BMess.Append("Contact No. :" + dr["MobileNo"] + "<br/>");
                //    }
                //    BMess.Append("Promo Code : Leela Kempinski  <br/>");
                //}
                //if (dr["PromotionCode"].ToString() == "CROWNPLAZA")
                //{
                //    if (Convert.ToBoolean(dr["WantComplimentary"].ToString()) == true || Convert.ToBoolean(dr["WantComplimentaryDrop"].ToString()) == true)
                //    {
                //        BMess.Append("Contact No. :" + dr["MobileNo"] + "<br/>");
                //    }
                //    if (Convert.ToBoolean(dr["WantComplimentary"].ToString()) == true)
                //    {
                //        BMess.Append("Place Of Pick :" + dr["PlaceOfPick"] + "<br/>");
                //        BMess.Append("Time Of Pick :" + dr["TimeOfPick"] + "<br/>");
                //    }
                //    if (Convert.ToBoolean(dr["WantComplimentaryDrop"].ToString()) == true)
                //    {
                //        BMess.Append("Place Of Pick For Drop :" + dr["PlaceOfDrop"] + "<br/>");
                //        BMess.Append("Time Of Pick For Drop :" + dr["TimeOfDrop"] + "<br/>");
                //    }

                //    BMess.Append("Promo Code : CROWNPLAZA HOTEL  <br/>");
                //}
                BMess.Append("Venue : Kingdom Of Dreams, Gurgaon<br/>");
                BMess.Append("Location : NCR<br/>");
                BMess.Append("Show Name : " + dr["Play"] + "<br/>");
                BMess.Append("Show Date : " + Convert.ToDateTime(dr["ShowDate"].ToString()).ToLongDateString() +
                    " at " + Convert.ToDateTime(dr["ShowTime"]).ToShortTimeString() + "<br/>");
                BMess.Append("Seat Info : " + dr["Category"] + " - " + dr["SeatInfo"] + "<br/>");
                BMess.Append("Total Amount : " + tktAmount + "<br/><br/>");
                BMess.Append("Payment Mode : " + dr["PaymentType"] + "<br/>");
                BMess.Append("Booking Date : " + Convert.ToDateTime(dr["DateOfBooking"].ToString()).ToLongDateString() +
                    " at " + Convert.ToDateTime(dr["TimeOfBooking"]).ToShortTimeString() + "<br/><br/>");
                if (seatsBooked)
                {
                    BMess.Append("Please Bring this Booking ID to the Box-office of Kingdom of Dreams to collect your tickets" +
                        " and also you need to present the same credit card on which the booking has" +
                        " been done, if tickets booked with credit card.<br/><br/>" +
                        "<b> Terms & conditions :" + "<br/>" +
                        " 1.A child less than two years of age, will not be allowed for the show due to high decibel level. Price of a show ticket for a child above two years would be same as for adults for all categories." + "<br/>" +
                        " 2.Make your Payment only through your Royal Membership Card to earn reward points. No claim will be entertained after printing of the tickets." + "<br/>" +
                        " 3.The Rewards point earned will be available for redemption only on next visit." + "<b/><br/><br/>" +
                        "<b>CANCELLATION/REFUND POLICY<b/><br/>As per policy we do not cancel/refund/change any tickets once booked." +
                        "<br/><br/>");
                }
                BMess.Append("Regards,<br/>Team<br/>Kingdom of Dreams");
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("returned " + BMess.ToString());
                return BMess.ToString();

            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Email Notification Error: " + ex.Message);
                return string.Empty;
            }
        }

        private static bool SendMailForGCab(DataRow dtRow, string GCabEmail, string GCabEmail2, string KODRefEmail, string KODRefEmail2, string name, string mailBody, string referenceNo, string bookingId)
        {
            System.Text.StringBuilder BMess = new System.Text.StringBuilder();
            BMess.Append(mailBody);
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("send mail to " + GCabEmail + "," + GCabEmail2 + "," + KODRefEmail + "," + KODRefEmail2);
            if (SendMailToCustomer(KODRefEmail.ToString(), name.ToString(), BMess))
                KoDTicketing.GTICKV.LogEntry(referenceNo.ToString(), "Mail Sent", "23", bookingId.ToString());
            if (SendMailToCustomer(KODRefEmail2.ToString(), name.ToString(), BMess))
                KoDTicketing.GTICKV.LogEntry(referenceNo.ToString(), "Mail Sent", "23", bookingId.ToString());
            if (SendMailToCustomer(GCabEmail.ToString(), name.ToString(), BMess))
                KoDTicketing.GTICKV.LogEntry(referenceNo.ToString(), "Mail Sent", "23", bookingId.ToString());
            if (SendMailToCustomer(GCabEmail2.ToString(), name.ToString(), BMess))
                KoDTicketing.GTICKV.LogEntry(referenceNo.ToString(), "Mail Sent", "23", bookingId.ToString());
            SendSignUpSMS(dtRow["Name"].ToString(), bookingId, dtRow["MobileNo"].ToString(), dtRow["TotalSeats"].ToString(), dtRow["Play"].ToString(), Convert.ToDateTime(dtRow["ShowDate"].ToString()), Convert.ToDateTime(dtRow["ShowTime"].ToString()));
            return true;
        }
        private static bool SendMailForWithoutGCab(DataRow dtRow, string RefEmail, string RefEmail2, string name, string mailBody, string referenceNo, string bookingId)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("send mail to " + RefEmail + "," + RefEmail2);
            System.Text.StringBuilder BMess = new System.Text.StringBuilder();
            BMess.Append(mailBody);
            if (SendMailToCustomer(RefEmail.ToString(), name.ToString(), BMess))
                KoDTicketing.GTICKV.LogEntry(referenceNo.ToString(), "Mail Sent", "23", bookingId.ToString());
            if (SendMailToCustomer(RefEmail2.ToString(), name.ToString(), BMess))
                KoDTicketing.GTICKV.LogEntry(referenceNo.ToString(), "Mail Sent", "23", bookingId.ToString());
            SendSignUpSMS(dtRow["Name"].ToString(), bookingId, dtRow["MobileNo"].ToString(), dtRow["TotalSeats"].ToString(), dtRow["Play"].ToString(), Convert.ToDateTime(dtRow["ShowDate"].ToString()), Convert.ToDateTime(dtRow["ShowTime"].ToString()));
            return true;
        }



        public static void SuccessPaymentResponseForHotels(bool seatsBooked, DataRow dtRow, string referecneNo, string bookingId, string receiptNo, string GCabEmail1, string GCabEmail2, string KODrefEmail, string KODrefEmail2)
        {
            // TO check which hotel booking & to which users, mail needs to be sent
            string prmotionCode = "";
            bool iscaballocatedforPick = false;
            bool iscaballocatedforDrop = false;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Mail send to GCAB and REF. KOD");
            string mailContent = PrepareContentMailForHotel(seatsBooked, dtRow, referecneNo, bookingId, receiptNo);
            prmotionCode = dtRow["PromotionCode"].ToString();
            iscaballocatedforPick = Convert.ToBoolean(dtRow["WantComplimentary"]);
            iscaballocatedforDrop = Convert.ToBoolean(dtRow["WantComplimentaryDrop"]);
            HotelList<string> PromoClass = new HotelList<string>();
            List<string> Promotions = PromoClass.listofHotels();

            switch (Promotions.Contains(prmotionCode))
            {
                default:
                    {

                    }
                    break;
                case (true):
                    {

                        if (iscaballocatedforPick == true || iscaballocatedforDrop == true)
                        {
                            SendMailForGCab(dtRow, GCabEmail1.ToString(), GCabEmail2.ToString(), KODrefEmail.ToString(), KODrefEmail2.ToString(), dtRow["Name"].ToString(), mailContent, referecneNo, bookingId);

                        }
                        else if (iscaballocatedforPick == false && iscaballocatedforDrop == false)
                        {
                            SendMailForWithoutGCab(dtRow, KODrefEmail.ToString(), KODrefEmail2.ToString(), dtRow["Name"].ToString(), mailContent, referecneNo, bookingId);
                        }
                        else
                        {
                            SendMailForWithoutGCab(dtRow, KODrefEmail.ToString(), KODrefEmail2.ToString(), dtRow["Name"].ToString(), mailContent, referecneNo, bookingId);
                        }
                        break;
                    }
                case (false):
                    {
                        break;
                    }
            }
        }

        private static void SendSignUpSMS(string Name, string bookingId, string MobileNo, string TotalSeats, string Play, DateTime ShowDate, DateTime ShowTime)
        {
            string tracName = Name.ToString();
            string[] dtrName = tracName.Split(' ');
            string tracDate = ShowDate.ToString("dd/MM/yyyy,h:mm tt");
            string[] dtrDate = tracDate.Split(',');
            string tracTime = ShowTime.ToString("dd/MM/yyyy,h:mm tt");
            string[] dtrTime = tracTime.Split(',');
            //DateTime tracTime = Convert.ToDateTime(ShowTime.ToString(""));
            sendSMS SmsObj = new sendSMS();
            string Msg = null;
            Msg = "Booking Details KOD Booking ID " + bookingId + " Dear " + dtrName[0] + " we confirm the booking of " + TotalSeats + " seats for " + Play + " Show on " + dtrDate[0] + " starting at " + dtrTime[1] + ".";
            SmsObj.SendSMS_Sender(MobileNo, Msg, "KINGDM");
        }
        private static void SendSignUpSMSForMANA(string Name, string bookingId, string MobileNo, string TotalSeats, string Play, DateTime ShowDate)
        {
            string tracName = Name.ToString();
            string[] dtrName = tracName.Split(' ');
            string tracDate = ShowDate.ToString("dd/MM/yyyy,h:mm tt");
            string[] dtrDate = tracDate.Split(',');

            //DateTime tracTime = Convert.ToDateTime(ShowTime.ToString(""));
            sendSMS SmsObj = new sendSMS();
            string Msg = null;

            Msg = "Dear " + dtrName[0] + "," + "Booking Id : " + bookingId + "." + "Seats " + TotalSeats + " for " + Play + " on " + dtrDate[0] + " at KoD. Please carry your CC/DC/RC which was used for booking tickets.";
            SmsObj.SendSMS_Sender(MobileNo, Msg, "KINGDM");
        }
        private static void SendSignUpSMSForSummer(string Name, string bookingId, string MobileNo, string TotalSeats, string Play, DateTime ShowDate, DateTime ShowTime)
        {
            string tracName = Name.ToString();
            string[] dtrName = tracName.Split(' ');
            string tracDate = ShowDate.ToString("dd/MM/yyyy,h:mm tt");
            string[] dtrDate = tracDate.Split(',');
            string tracTime = ShowTime.ToString("dd/MM/yyyy,h:mm tt");
            string[] dtrTime = tracTime.Split(',');
            //DateTime tracTime = Convert.ToDateTime(ShowTime.ToString(""));
            sendSMS SmsObj = new sendSMS();
            string Msg = null;
            Msg = "Dear " + dtrName[0] + "," + "Booking Id : " + bookingId + "." + "Seats " + TotalSeats + " for " + Play + " on " + dtrDate[0] + " at KoD. Please carry your CC/DC/RC which was used for booking tickets.";
            SmsObj.SendSMS_Sender(MobileNo, Msg, "KINGDM");
        }

        private static void SendSignUpSMSForMMT(string Name, string bookingId, string MobileNo, string TotalSeats, string Play, DateTime ShowDate, DateTime ShowTime, string promo)
        {
            string tracName = Name.ToString();
            string[] dtrName = tracName.Split(' ');
            string tracDate = ShowDate.ToString("dd/MM/yyyy,h:mm tt");
            string[] dtrDate = tracDate.Split(',');
            string tracTime = ShowTime.ToString("dd/MM/yyyy,h:mm tt");
            string[] dtrTime = tracTime.Split(',');
            //DateTime tracTime = Convert.ToDateTime(ShowTime.ToString(""));
            sendSMS SmsObj = new sendSMS();
            string Msg = null;
            if (promo == "MMTDOMESTIC")
                Msg = "Dear " + dtrName[0] + "," + "Booking Id : " + bookingId + "." + "Seats " + TotalSeats + " for " + Play + " on " + dtrDate[0] + "," + dtrTime[1] + " at KoD. Please carry your CC/DC/RC which was used for booking tickets.";
            else
                Msg = "Dear " + dtrName[0] + "," + "Booking Id : " + bookingId + "." + "Seats " + TotalSeats + " for " + Play + " on " + dtrDate[0] + " at KoD. Please carry your CC/DC/RC which was used for booking tickets.";
            SmsObj.SendSMS_Sender(MobileNo, Msg, "KINGDM");
        }

        public static bool SendSMSToCustomer(String mobileNo, String message)
        {
            return false;
            sendSMS sms = new sendSMS();
            String response = sms.SendSMS_Sender(mobileNo, message, "KOD");
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("SMS Sending Status: " + response);
            return response.Contains("Sent Successfully");
        }


        public static bool SendMailToCustomer(string toEmail, string toName, System.Text.StringBuilder message)
        {
            try
            {
                Mail.MailData amail = new Mail.MailData();
                amail.to = toEmail;//dr["EmailID"].ToString();
                amail.toName = toName; //dr["Name"].ToString();
                amail.from = mailSenderAccount;
                amail.fromName = mailSenderAccountName;
                amail.fromUID = mailEmailAccountID;
                amail.fromPwd = mailEmailAccountPassword;
                amail.smtpServer = mailEmailAccountSMTPServer;

                amail.subject = "Booking Receipt";
                amail.bodyMessage = message.ToString();

                Mail mail = new Mail();
                return mail.sendMail_Net(amail, false);
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Email Sending Error: " + ex.Message);
                return false;
            }
            finally
            {

            }

        }
        public static bool SendMailToTestUser(string toEmail, string toName, System.Text.StringBuilder message)
        {
            try
            {
                Mail.MailData amail = new Mail.MailData();
                amail.to = toEmail;
                amail.toName = toName;
                amail.from = mailSenderAccount;
                amail.fromName = mailSenderAccountName;
                amail.fromUID = mailEmailAccountID;
                amail.fromPwd = mailEmailAccountPassword;
                amail.smtpServer = mailEmailAccountSMTPServer;

                amail.subject = "IDBI Return Receipt Error";
                amail.bodyMessage = message.ToString();

                Mail mail = new Mail();
                return mail.sendMail_Net(amail, false);
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Email Sending To TestUser Error: " + ex.Message);
                return false;
            }
            finally
            {

            }

        }
        public static bool SendMailToRoyality(string toEmail, string toName, string id, System.Text.StringBuilder message)
        {
            try
            {
                Mail.MailData amail = new Mail.MailData();
                amail.to = toEmail;//dr["EmailID"].ToString();
                amail.toName = toName; //dr["Name"].ToString();
                amail.from = mailSenderAccount;
                amail.fromName = mailSenderAccountName;
                amail.fromUID = mailEmailAccountID;
                amail.fromPwd = mailEmailAccountPassword;
                amail.smtpServer = mailEmailAccountSMTPServer;
                Char pad = '0';
                amail.subject = "Enrollment No. - " + (id.ToString().PadLeft(4, pad)) + " : " + "Master Card Platinum Royal Card Enrollment";
                amail.bodyMessage = message.ToString();

                Mail mail = new Mail();
                return mail.sendMail_Net(amail, false);
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Email Sending Error: " + ex.Message);
                return false;
            }
            finally
            {

            }

        }

        public static void SendMailToCustomerSupport(System.Text.StringBuilder message, string recipient)
        {
            try
            {
                Mail.MailData errmaildata = new Mail.MailData();
                //errmaildata.to = mailEmailAccountID;
                errmaildata.to = recipient;
                errmaildata.toName = "";
                errmaildata.from = mailSenderAccount;
                errmaildata.fromName = mailSenderAccountName;
                errmaildata.fromUID = mailEmailAccountID;
                errmaildata.fromPwd = mailEmailAccountPassword;
                errmaildata.smtpServer = mailEmailAccountSMTPServer;
                errmaildata.subject = "Booking Failure";
                errmaildata.bodyMessage = message.ToString();

                Mail mail = new Mail();
                mail.sendMail_Net(errmaildata, false);
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Failure Email send error: " + ex.Message);
            }
        }

        public static void NewYearSuccessPaymentResponse(DataRow dr, string BookingId, string ReceiptNo, string CocertEmailId1)
        {
            try
            {
                System.Text.StringBuilder BMess = new System.Text.StringBuilder();
                //BMess.Append("Dear Mr/Mrs ");
                //BMess.Append(dr["Name"]);
                //BMess.Append("<br/><br/>");

               // BMess.Append("We thank you for booking the tickets for The Grandest New year party at Kingdom of Dreams. Please print this mail to claim your tickets from the Box Office at Kingdom of Dreams..<br/><br/>");
                BMess.Append("Your Tickets have been successfully booked, and details are mentioned below...<br/><br/>");
                BMess.Append("Booking ID : " + dr["BookingId"] + "<br/>");
                BMess.Append("Guest Name : " + dr["Name"] + "<br/>");
                BMess.Append("Venue : Kingdom Of Dreams, Gurgaon <br/>");
                BMess.Append("Location : NCR <br/>");
                BMess.Append("Event Name : The Grandest New Year Celebration <br/>");
                BMess.Append("Event Date : Tuesday, December 31 , 2013 <br/>");
                BMess.Append("Entry Time : 8:00 PM  <br/>");
                if (Convert.ToInt16(dr["Qty_PackageTypeCouple"]) > 0)
                {
                    BMess.Append("Category : Couple :" + dr["Qty_PackageTypeCouple"] + "<br/>");
                }
                if (Convert.ToInt16(dr["Qty_PackageTypeSingle"]) > 0)
                {
                    BMess.Append("Single :" + dr["Qty_PackageTypeSingle"] + "<br/>");
                }
                if (Convert.ToInt16(dr["Qty_PackageTypeTeen"]) > 0)
                {
                    BMess.Append("Teen :" + dr["Qty_PackageTypeTeen"] + "<br/>");
                }
                if (Convert.ToInt16(dr["Qty_PackageTypeKid"]) > 0)
                {
                    BMess.Append("Kids :" + dr["Qty_PackageTypeKid"] + "<br/>");
                }

                BMess.Append("Total Amount : " + dr["TotalAmount"] + "<br/><br/>");
                BMess.Append("Payment Mode : Credit/Debit Card <br/>");
                BMess.Append("Booking Date : " + Convert.ToDateTime(dr["DateOfBooking"].ToString()).ToLongDateString() +
                   " at " + Convert.ToDateTime(dr["DateOfBooking"]).ToLocalTime().ToShortTimeString() + "<br/><br/>");

                BMess.Append("Please Bring this Booking ID to the Box-office of Kingdom of Dreams to collect your tickets and also you need to present the same credit card on which the booking has been done, if tickets booked with credit card.<br/><br/>");
                BMess.Append("Terms & Conditions<br/>");
                BMess.Append("1. Only Couples and Families allowed. <br/>");
                BMess.Append("2. Children between the age of 5 to 12 years will be classified  as Kids. <br/>");
                BMess.Append("3. Children between the age of 13 to 19 years will be classified as Teens. <br/>");
                BMess.Append("4. Guest needs to present his/her Royal Card before purchasing the tickets from box office in order to avail discounts and points.<br/>");
                BMess.Append("5. Spend value for tier up gradation of those members which are buying tickets from com-centre or from our website will be considered after the new year event.<br/>");

                //BMess.Append("<b><u>Admission Rights<u/></b><br/>");
                //BMess.Append("1. It is not advisable to bring Kids below the age of 5 years. All children/Kids will be your own responsibility. <br/>");
                //BMess.Append("2. Infants or Children below age of 2 years are strictly not allowed inside the theater. <br/>");
                //BMess.Append("3. Please carry valid ID proof ( Specially Date Of Birth ) along with the ticket  for the smooth entry to the event .<br/> The  alcohol will not be served to the guests under the age of 25 years. <br/>");
                //BMess.Append("4. Please carry the Credit Card used for booking tickets .or its copy for verification. <br/>");
                //BMess.Append("5. Entry 7 pm onwards. <br/>");
                //BMess.Append("6. As per policy we do not cancel/refund/change any tickets once booked. <br/>");
                //BMess.Append("We wish you a pleasant experience at Kingdom of Dreams.");
                //BMess.Append("Best Compliments <br/>Kingdom of Dreams");

                if (SendMailToCustomer(dr["EmailId"].ToString(), dr["Name"].ToString(), BMess))
                    KoDTicketing.GTICKV.LogEntry(ReceiptNo.ToString(), "Mail Sent", "23", dr["BookingId"].ToString());

                //if (SendMailToCustomer(CocertEmailId, dr["Name"].ToString(), BMess))
                //    KoDTicketing.GTICKV.LogEntry(ReceiptNo.ToString(), "Mail Sent to concert user", "15", dr["BookingId"].ToString());

                if (SendMailToCustomer(CocertEmailId1, dr["Name"].ToString(), BMess))
                    KoDTicketing.GTICKV.LogEntry(ReceiptNo.ToString(), "Mail Sent to concert second user", "23", dr["BookingId"].ToString());


            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Email Notification Error: " + ex.Message);
            }

            try
            {
                string TotalSeats = Convert.ToString(Convert.ToInt16(dr["Qty_PackageTypeCouple"]) + Convert.ToInt16(dr["Qty_PackageTypeSingle"]) + Convert.ToInt16(dr["Qty_PackageTypeTeen"]) + Convert.ToInt16(dr["Qty_PackageTypeKid"]));
                SendSignUpSMS(dr["Name"].ToString(), dr["BookingId"].ToString(), dr["ContactNumber"].ToString(), TotalSeats, "New Year", Convert.ToDateTime("2013-12-31 00:00:00.000"), Convert.ToDateTime("1900-01-01 20:00:00.000"));
                //if (SendSMSToCustomer(dr["ContactNumber"].ToString(), BMess.ToString()))
                KoDTicketing.GTICKV.LogEntry(ReceiptNo.ToString(), "SMS Sent", "24", dr["BookingId"].ToString());
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("SMS Notification Error: " + ex.Message);
            }
        }
        public static void BollyLandPaymentResponse(DataRow dr, string BookingId, string ReceiptNo, string CocertEmailId1)
        {
            try
            {
                System.Text.StringBuilder BMess = new System.Text.StringBuilder();
                //BMess.Append("Dear Mr/Mrs ");
                //BMess.Append(dr["Name"]);
                //BMess.Append("<br/><br/>");

                BMess.Append("Your Tickets have been successfully booked, and details are mentioned below...<br/><br/>");
                BMess.Append("Booking ID : " + dr["BookingId"] + "<br/>");
                BMess.Append("Guest Name : " + dr["Name"] + "<br/>");
                BMess.Append("Venue : Kingdom Of Dreams, Gurgaon <br/>");
                BMess.Append("Location : NCR <br/>");
                BMess.Append("Event Name : BollyLand <br/>");
                BMess.Append("Event Date : Friday, December 20 , 2013 <br/>");
                BMess.Append("Entry Time : 7:00 PM  <br/>");
                if (Convert.ToInt16(dr["Qty_Gold"]) > 0)
                {
                    BMess.Append("Category : Gold :" + dr["Qty_Gold"] + "<br/>");
                }
                if (Convert.ToInt16(dr["Qty_silver"]) > 0)
                {
                    BMess.Append("Category : Silver :" + dr["Qty_silver"] + "<br/>");
                }


                BMess.Append("Total Amount : " + dr["TotalAmount"] + "<br/>");
                BMess.Append("Payment Mode : Credit/Debit Card <br/>");
                BMess.Append("Booking Date : " + Convert.ToDateTime(dr["DateOfBooking"].ToString()).ToLongDateString() +
                   " at " + Convert.ToDateTime(dr["DateOfBooking"]).ToLocalTime().ToShortTimeString() + "<br/><br/>");

                BMess.Append("Please Bring this Booking ID to the Box-office of Kingdom of Dreams to collect your tickets and also you need to present the same credit card on which the booking has been done, if tickets booked with credit card.<br/><br/>");
                BMess.Append("Terms & Conditions<br/>");
                BMess.Append("1. Event is open to all persons who are aged 15 years and above. Entry to the Event implies acceptance of all the terms and conditions of the Event. In case of entry to the Event by any persons under the age of 18 years (minors), such entry to the Event by any such entrant shall be with the prior consent of the parent/guardians and will be deemed as entry with the prior consent and knowledge of the entrant's parents. <br/>");
                BMess.Append("2. Carry your Id Card to the event for age validation. <br/>");
                BMess.Append("3. The Event is scheduled to be conducted on 20th December 2013 from 7:30 PM at Kingdom Of Dreams: Gurgaon. <br/>");

                //BMess.Append("<b><u>Admission Rights<u/></b><br/>");
                //BMess.Append("1. It is not advisable to bring Kids below the age of 5 years. All children/Kids will be your own responsibility. <br/>");
                //BMess.Append("2. Infants or Children below age of 2 years are strictly not allowed inside the theater. <br/>");
                //BMess.Append("3. Please carry valid ID proof ( Specially Date Of Birth ) along with the ticket  for the smooth entry to the event .<br/> The  alcohol will not be served to the guests under the age of 25 years. <br/>");
                //BMess.Append("4. Please carry the Credit Card used for booking tickets .or its copy for verification. <br/>");
                //BMess.Append("5. Entry 8 pm onwards. <br/>");
                //BMess.Append("6. As per policy we do not cancel/refund/change any tickets once booked. <br/>");
                //BMess.Append("We wish you a pleasant experience at Kingdom of Dreams.");
                //BMess.Append("Best Compliments <br/>Kingdom of Dreams");

                if (SendMailToCustomer(dr["EmailId"].ToString(), dr["Name"].ToString(), BMess))
                    KoDTicketing.GTICKV.LogEntry(ReceiptNo.ToString(), "Mail Sent", "23", dr["BookingId"].ToString());

                //if (SendMailToCustomer(CocertEmailId1, dr["Name"].ToString(), BMess))
                //    KoDTicketing.GTICKV.LogEntry(ReceiptNo.ToString(), "Mail Sent to concert second user", "23", dr["BookingId"].ToString());

            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Email Notification Error: " + ex.Message);
            }

            try
            {
                string TotalSeats = Convert.ToString(Convert.ToInt16(dr["Qty_Gold"]) + Convert.ToInt16(dr["Qty_silver"]));
                SendSignUpSMS(dr["Name"].ToString(), dr["BookingId"].ToString(), dr["ContactNumber"].ToString(), TotalSeats, "Bolly Land", Convert.ToDateTime("2013-12-20 00:00:00.000"), Convert.ToDateTime("1900-01-01 19:00:00.000"));
                //if (SendSMSToCustomer(dr["ContactNumber"].ToString(), BMess.ToString()))
                KoDTicketing.GTICKV.LogEntry(ReceiptNo.ToString(), "SMS Sent", "24", dr["BookingId"].ToString());
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("SMS Notification Error: " + ex.Message);
            }
        }

        public static void MMTPaymentResponse(DataRow dr1, DataRow dr, string BookingId, string ReceiptNo, string CocertEmailId2, string CocertEmailId3, string showtime, string showdate)
        {
            try
            {
                DateTime day = Convert.ToDateTime(dr["ShowDate"]);
                string day1 = day.Day.ToString() + "/" + day.Month.ToString() + "/" + day.Year.ToString();
                System.Text.StringBuilder BMess = new System.Text.StringBuilder();
                BMess.Append("Dear Mr/Mrs ");
                BMess.Append(dr["Name"]);
                BMess.Append("<br/><br/>");

                BMess.Append("Thank You for booking with us.<br/><br/>");
                BMess.Append("Your transaction was successful and confirmed details are as follows:<br/><br/>");
                BMess.Append("Booking ID : " + dr["MMTBookingId"] + "<br/>");
                BMess.Append("Promo Code : " + dr["promocode"].ToString().ToUpper() + " <br/>");
                BMess.Append("PNR No. : " + dr["PnrNo"].ToString().ToUpper() + "<br/>");
                BMess.Append("Show Date : " + Convert.ToDateTime(showdate.ToString()).ToLongDateString() +
                    " at " + Convert.ToDateTime(showtime).ToShortTimeString() + "<br/>");
                BMess.Append("Promo Name : MakeMyTrip <br/>");
                BMess.Append("Location : NCR <br/>");
                BMess.Append("Venue : Kingdom of Dreams, Gurgaon <br/>");
                // BMess.Append("Offer Date : 07-May-2013 to 06-August-2013 <br/>");
                BMess.Append("No. of Packages: " + dr["NoofPackages"] + "<br/>");
                BMess.Append("Seat Info : " + dr1["Category"] + " - " + dr1["SeatInfo"] + "<br/>");
                BMess.Append("Payment Mode : Credit/Debit Card <br/>");
                BMess.Append("Total Amount : " + Convert.ToInt32(dr["PayableAmount"]).ToString() + "<br/><br/>");
                BMess.Append("Booking Date : " + dr["Dateofbooking"] + "<br/>");
                BMess.Append("Email Id : " + dr["Email"] + "<br/>");
                BMess.Append("Contact Number : " + dr["ContactNumber"] + "<br/><br/>");
                BMess.Append("Booking Note : " + "<br/>");
                BMess.Append("This offer is valid on the shows Jhumroo and Zangoora." + "<br/>"
                    + "Please bring this Booking ID to the Box Office counter to collect your tickets and Rs.1000/- value food coupon/card" + "<br/>"
                    + "Please present the MakeMyTrip booking confirmation copy when collecting tickets and food card" + "<br/>"
                    + "The Credit/Debit Card and Credit/Debit Card Holder must be present at the ticket counter while collecting the ticket(s)" + "<br/>"
                    + "<b>Terms & conditions :" + "<br/>"
                    + "1.A child less than two years of age, will not be allowed for the show due to high decibel level. Price of a show ticket for a child above two years would be same as for adults for all categories." + "<br/>" 
                    + "2.Make your Payment only through your Royal Membership Card to earn reward points. No claim will be entertained after printing of the tickets." + "<br/>"
                    + "3.The Rewards point earned will be available for redemption only on next visit." + "<br/><br/>"
                    + "As per policy, we do not Cancel/Refund/Exchange tickets once booked." + "<b/><br/><br/>");

                BMess.Append("Regard ," + "<br/>");
                BMess.Append("Team  Kingdom of Dreams" + "<br/>");
                BMess.Append("For any queries contact Customer Support" + "<br/>");
                BMess.Append("Tel:- 0124-4528000/ 01246677000 Email Id: info@kingdomofdreams.co.in" + "<br/>");


                if (SendMailToCustomer(dr["Email"].ToString(), dr["Name"].ToString(), BMess))
                    KoDTicketing.GTICKV.LogEntry(ReceiptNo.ToString(), "Mail Sent", "23", dr["BookingId"].ToString());

                if (SendMailToCustomer(CocertEmailId2, dr["Name"].ToString(), BMess))
                    KoDTicketing.GTICKV.LogEntry(ReceiptNo.ToString(), "Mail Sent to concert user", "23", dr["BookingId"].ToString());

                if (SendMailToCustomer(CocertEmailId3, dr["Name"].ToString(), BMess))
                    KoDTicketing.GTICKV.LogEntry(ReceiptNo.ToString(), "Mail Sent to concert second user", "23", dr["BookingId"].ToString());


            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Email Notification Error: " + ex.Message);
            }

            try
            {
                string TotalSeats = Convert.ToString(Convert.ToInt16(dr["NoofPackages"]));
                SendSignUpSMSForMMT(dr["Name"].ToString(), dr["MMTBookingId"].ToString(), dr["ContactNumber"].ToString(), TotalSeats, "MMT promotion", Convert.ToDateTime(dr["ShowDate"].ToString()), Convert.ToDateTime("1900-01-01 19:00:00.000"), "");
                //if (SendSMSToCustomer(dr["ContactNumber"].ToString(), BMess.ToString()))
                KoDTicketing.GTICKV.LogEntry(ReceiptNo.ToString(), "SMS Sent", "24", dr["BookingId"].ToString());
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("SMS Notification Error: " + ex.Message);
            }
        }
        public static void MCPaymentResponse(DataRow dr, DataRow dr3, string BookingId, string ReceiptNo, string CocertEmailId2, string CocertEmailId3, string showtime, string showdate)
        {
            try
            {
                DateTime day = Convert.ToDateTime(dr["ShowDate"]);
                string day1 = day.Day.ToString() + "/" + day.Month.ToString() + "/" + day.Year.ToString();
                System.Text.StringBuilder BMess = new System.Text.StringBuilder();
                BMess.Append("Dear Mr/Mrs ");
                BMess.Append(dr["Name"]);
                BMess.Append("<br/><br/>");

                BMess.Append("Thank You for booking with us.<br/><br/>");
                BMess.Append("Your transaction was successful and confirmed details are as follows:<br/><br/>");
                BMess.Append("Booking ID : " + dr["BookingId"] + "<br/>");
                BMess.Append("Promo Code : " + dr3["Promo Code"].ToString().ToUpper() + " <br/>");
                BMess.Append("Promo Name : Master Card Promotion <br/>");
                BMess.Append("Master Card Bin No : " + dr3["Card No"].ToString().ToUpper() + "<br/>");
                BMess.Append("Master Card Bank Name : " + dr3["Bank Name"].ToString().ToUpper() + "<br/>");
                BMess.Append("Show Name : " + dr["Play"].ToString().ToUpper() + "<br/>");
                BMess.Append("Show Date : " + Convert.ToDateTime(showdate.ToString()).ToLongDateString() +
                    " at " + Convert.ToDateTime(showtime).ToShortTimeString() + "<br/>");
                if (dr3["Type"].ToString() == "PACKAGE")
                    BMess.Append("No. of Packages: " + dr3["NoofPackages"].ToString() + "<br/>");
                if (dr3["Type"].ToString() == "TICKET")
                    BMess.Append("No. of seats: " + dr["TotalSeats"].ToString() + "<br/>");
                BMess.Append("Seat Info : " + dr["Category"] + " - " + dr["SeatInfo"] + "<br/>");
                BMess.Append("Payment Mode : Credit/Debit Card <br/>");
                decimal DiscountPercentage1 = decimal.Parse(dr["DiscountPercentage"].ToString());
                decimal tktAmount1 = decimal.Parse(dr["TotalAmount"].ToString());
                int numberOfSeats1 = int.Parse(dr["TotalSeats"].ToString());

                if (DiscountPercentage1 > 0)
                {

                    decimal SingleTicketPrice = tktAmount1 / numberOfSeats1;

                    decimal DiscountedPrice = SingleTicketPrice - (SingleTicketPrice * DiscountPercentage1 / 100);
                    DiscountedPrice = decimal.Truncate(DiscountedPrice);
                    if (DiscountedPrice == 1274)
                        DiscountedPrice = DiscountedPrice + 1;
                    else if (DiscountedPrice == 2124)
                        DiscountedPrice = DiscountedPrice + 1;
                    else if (DiscountedPrice == 2974)
                        DiscountedPrice = DiscountedPrice + 1;
                    else if (DiscountedPrice == 4249)
                        DiscountedPrice = DiscountedPrice + 1;

                    tktAmount1 = DiscountedPrice * numberOfSeats1;

                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Total Amount Price For a Ticket" + tktAmount1.ToString());


                }
                if (dr3["Type"].ToString() == "PACKAGE")
                    tktAmount1 = Decimal.Parse(dr3["PayableAmount"].ToString());
                BMess.Append("Total Amount : " + tktAmount1.ToString() + "<br/><br/>");
                BMess.Append("Venue : Kingdom of Dreams, Gurgaon <br/>");
                BMess.Append("Location : NCR<br/>");



                BMess.Append("Booking Date : " + dr["DateOfBooking"] + "<br/>");
                BMess.Append("Email Id : " + dr["EmailID"] + "<br/>");
                BMess.Append("Contact Number : " + dr["MobileNo"] + "<br/><br/>");
                BMess.Append("Please Bring this Booking ID to the Box-office of Kingdom of Dreams to collect your tickets and also you need to present the same credit card on which the booking has been done, if tickets booked with credit card." + "<br/><br/>" +
                     "<b>Terms & conditions :</b><br/>" +
                     "<b>1.A Child less than two years of age, will not be allowed for the show due to High decibel level. Price of a show ticket for a child above two years would be same as for Adults for all categories.</b><br/>" +
                     "<b>2.Make your Payment only through your Royal Membership Card to earn reward points. No claim will be entertained after printing of the tickets.</b><br/>" +
                     "<b>3.The Rewards point earned will be available for redemption only on next visit.</b><br/><br/>" +
                     "<b>CANCELLATION/REFUND POLICY</b><br/>" +
                     "<b>As per policy we do not cancel/refund/change any tickets once booked.<b><br/><br/>");
                if (dr3["Type"].ToString() == "PACKAGE")
                {
                    BMess.Append("Please bring this Booking ID to the Box Office counter to collect your NM tickets, Reverse Bungee couple tickets per package and Rs.2000/- " + "<br/>"
                        + "card applicable per package." + "<br/>");
                }
               // BMess.Append("Please present the Mastercard used for booking when collecting tickets");
                if (dr3["Type"].ToString() == "PACKAGE")
                {
                    BMess.Append("and food/retail card");
                }
               // BMess.Append(" at Box Office" + "<br/>"
                //    + "The Credit/Debit Card and Credit/Debit Card Holder must be present at the ticket counter while collecting the ticket(s)" + "<br/>"
                //    + "As per policy, we do not Cancel/Refund/Exchange tickets once booked.<br/><br/>");

                BMess.Append("<a href='http://royalty.kingdomofdreams.in/Account/Membership.aspx'><img src='http://msticket.kingdomofdreams.in/images/Email-banner_bottom-new-N.JPG'/></a><br/><br/>");

                BMess.Append("Regard ," + "<br/>");
                BMess.Append("Team  Kingdom of Dreams" + "<br/>");
                BMess.Append("For any queries contact Customer Support" + "<br/>");
                BMess.Append("Tel:- 0124-4528000/ 01246677000 Email Id: info@kingdomofdreams.co.in" + "<br/>");


                if (SendMailToCustomer(dr["EmailID"].ToString(), dr["Name"].ToString(), BMess))
                    KoDTicketing.GTICKV.LogEntry(ReceiptNo.ToString(), "Mail Sent", "23", dr["BookingId"].ToString());

            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Email Notification Error: " + ex.Message);
            }

            try
            {
                //string TotalSeats = Convert.ToString(Convert.ToInt16(dr["NoofPackages"]));
                SendSignUpSMS(dr["Name"].ToString(), dr["BookingId"].ToString(), dr["MobileNo"].ToString(), dr["TotalSeats"].ToString(), dr["Play"].ToString(), Convert.ToDateTime(dr["ShowDate"].ToString()), Convert.ToDateTime(dr["ShowTime"].ToString()));
                //if (SendSMSToCustomer(dr["ContactNumber"].ToString(), BMess.ToString()))
                KoDTicketing.GTICKV.LogEntry(ReceiptNo.ToString(), "SMS Sent", "24", dr["BookingId"].ToString());
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("SMS Notification Error: " + ex.Message);
            }
        }
        public static void MANAPaymentResponse(DataRow dr1, DataRow dr, string BookingId, string ReceiptNo, string CocertEmailId2, string CocertEmailId3, string showtime, string showdate)
        {
            try
            {
                DateTime day = Convert.ToDateTime(dr["ShowDate"]);
                string day1 = day.Day.ToString() + "/" + day.Month.ToString() + "/" + day.Year.ToString();
                System.Text.StringBuilder BMess = new System.Text.StringBuilder();
                BMess.Append("Dear Mr/Mrs ");
                BMess.Append(dr["Name"]);
                BMess.Append("<br/><br/>");

                BMess.Append("Thank You for booking with us.<br/><br/>");
                BMess.Append("Your transaction was successful and confirmed details are as follows:<br/><br/>");
                BMess.Append("Booking ID : " + dr["MANABookingId"] + "<br/>");
                //BMess.Append("Promo Code : " + dr["promocode"].ToString().ToUpper() + " <br/>");
                //BMess.Append("PNR No. : " + dr["PnrNo"].ToString().ToUpper() + "<br/>");
                BMess.Append("Show Date : " + Convert.ToDateTime(showdate.ToString()).ToLongDateString() +
                    " at " + Convert.ToDateTime(showtime).ToShortTimeString() + "<br/>");
                BMess.Append("Promo Name : MANA <br/>");
                BMess.Append("Location : NCR <br/>");
                BMess.Append("Venue : Kingdom of Dreams, Gurgaon <br/>");
                // BMess.Append("Offer Date : 07-May-2013 to 06-August-2013 <br/>");
                BMess.Append("No. of Packages: " + dr["NoofPackages"] + "<br/>");
                BMess.Append("Seat Info : " + dr1["Category"] + " - " + dr1["SeatInfo"] + "<br/>");
                BMess.Append("Payment Mode : Credit/Debit Card <br/>");
                BMess.Append("Total Amount : " + Convert.ToInt32(dr["PayableAmount"]).ToString() + "<br/><br/>");
                BMess.Append("Booking Date : " + dr["Dateofbooking"] + "<br/>");
                BMess.Append("Email Id : " + dr["Email"] + "<br/>");
                BMess.Append("Contact Number : " + dr["ContactNumber"] + "<br/><br/>");
                BMess.Append("Booking Note : " + "<br/>");
                BMess.Append("This offer is valid on the shows MANA." + "<br/>"
                    + "Please bring this Booking ID to the Box Office counter to collect your tickets and Rs.1000/- value food coupon/card, Valid on show date only." + "<br/>"
                    + "The Credit/Debit Card and Credit/Debit Card Holder must be present at the ticket counter while collecting the ticket(s)" + "<br/>"
                    + "<b>Terms & conditions :" + "<br/>"
                    + "1.A child less than two years of age, will not be allowed for the show due to high decibel level. Price of a show ticket for a child above two years would be same as for adults for all categories." + "<br/>" 
                    + "2.Make your Payment only through your Royal Membership Card to earn reward points. No claim will be entertained after printing of the tickets." + "<br/>"
                    + "3.The Rewards point earned will be available for redemption only on next visit." + "<br/><br/>"
                    + "As per policy, we do not Cancel/Refund/Exchange tickets once booked." + "<b/><br/><br/>");

                BMess.Append("Regard ," + "<br/>");
                BMess.Append("Team  Kingdom of Dreams" + "<br/>");
                BMess.Append("For any queries contact Customer Support" + "<br/>");
                BMess.Append("Tel:- 0124-4528000/ 01246677000 Email Id: info@kingdomofdreams.co.in" + "<br/>");


                if (SendMailToCustomer(dr["Email"].ToString(), dr["Name"].ToString(), BMess))
                    KoDTicketing.GTICKV.LogEntry(ReceiptNo.ToString(), "Mail Sent", "23", dr["BookingId"].ToString());

                if (SendMailToCustomer(CocertEmailId2, dr["Name"].ToString(), BMess))
                    KoDTicketing.GTICKV.LogEntry(ReceiptNo.ToString(), "Mail Sent to concert user", "23", dr["BookingId"].ToString());

                if (SendMailToCustomer(CocertEmailId3, dr["Name"].ToString(), BMess))
                    KoDTicketing.GTICKV.LogEntry(ReceiptNo.ToString(), "Mail Sent to concert second user", "23", dr["BookingId"].ToString());


            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Email Notification Error: " + ex.Message);
            }

            try
            {
                //string TotalSeats = Convert.ToString(Convert.ToInt16(dr["NoofPackages"])*4);
                SendSignUpSMSForMANA(dr["Name"].ToString(), dr["MANABookingId"].ToString(), dr["ContactNumber"].ToString(), dr1["TotalSeats"].ToString(), "MANA", Convert.ToDateTime(dr["ShowDate"].ToString()));
                //if (SendSMSToCustomer(dr["ContactNumber"].ToString(), BMess.ToString()))
                KoDTicketing.GTICKV.LogEntry(ReceiptNo.ToString(), "SMS Sent", "24", dr["BookingId"].ToString());
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("SMS Notification Error: " + ex.Message);
            }
        }

        public static void SummerPaymentResponse(DataRow dr, string BookingId, string ReceiptNo, string CocertEmailId, string CocertEmailId1)
        {
            try
            {
                //DateTime day = Convert.ToDateTime(dr["ShowDate"]);
                //string day1 = day.Day.ToString() + "/" + day.Month.ToString() + "/" + day.Year.ToString();
                System.Text.StringBuilder BMess = new System.Text.StringBuilder();
                BMess.Append("Dear Mr/Mrs ");
                BMess.Append(dr["Name"]);
                BMess.Append("<br/><br/>");

                BMess.Append("Thank You for booking with us.<br/><br/>");
                BMess.Append("Your transaction was successful and confirmed details are as follows:<br/><br/>");
                BMess.Append("Booking ID : " + dr["BookingId"] + "<br/>");
                BMess.Append("Event Name : Summer Camp <br/>");
                BMess.Append("Location : NCR <br/>");
                BMess.Append("Venue : Kingdom of Dreams, Gurgaon <br/>");
                BMess.Append("No. of Sheats: " + dr["Nooftickets"] + "<br/>");
                BMess.Append("Payment Mode : Credit/Debit Card <br/>");
                BMess.Append("Total Amount : " + Convert.ToInt32(dr["PayableAmount"]).ToString() + "<br/><br/>");
                BMess.Append("Booking Date : " + dr["Dateofbooking"] + "<br/><br/>");
                BMess.Append("Booking Note : " + "<br/>");
                BMess.Append("Note:Please bring this Booking ID to the Box Office counter to collect your tickets" + "<br/>" +
                "The Credit/Debit Card and Credit/Debit Card Holder must be present at the ticket counter while collecting the ticket(s)"
                + "<br/>" + "As per policy, we do not Cancel/Refund/Exchange tickets once booked." + "<br/><br/>");
                BMess.Append("Regard ," + "<br/>");
                BMess.Append("Team  Kingdom of Dreams" + "<br/>");
                BMess.Append("For any queries contact Customer Support" + "<br/>");
                BMess.Append("Tel:- 0124-4528000/ 01246677000 Email Id: info@kingdomofdreams.co.in" + "<br/>");

                if (SendMailToCustomer(dr["Email"].ToString(), dr["Name"].ToString(), BMess))
                    KoDTicketing.GTICKV.LogEntry(ReceiptNo.ToString(), "Mail Sent", "23", dr["BookingId"].ToString());

                if (SendMailToCustomer(CocertEmailId, dr["Name"].ToString(), BMess))
                    KoDTicketing.GTICKV.LogEntry(ReceiptNo.ToString(), "Mail Sent to concert user", "23", dr["BookingId"].ToString());

                if (SendMailToCustomer(CocertEmailId1, dr["Name"].ToString(), BMess))
                    KoDTicketing.GTICKV.LogEntry(ReceiptNo.ToString(), "Mail Sent to concert second user", "23", dr["BookingId"].ToString());


            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Email Notification Error: " + ex.Message);
            }

            try
            {
                string TotalSeats = Convert.ToString(Convert.ToInt16(dr["Nooftickets"]));
                SendSignUpSMSForSummer(dr["Name"].ToString(), dr["BookingId"].ToString(), dr["ContactNumber"].ToString(), TotalSeats, "Summer", Convert.ToDateTime(dr["Dateofbooking"].ToString()), Convert.ToDateTime("1900-01-01 19:00:00.000"));
                //if (SendSMSToCustomer(dr["ContactNumber"].ToString(), BMess.ToString()))
                KoDTicketing.GTICKV.LogEntry(ReceiptNo.ToString(), "SMS Sent", "24", dr["BookingId"].ToString());
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("SMS Notification Error: " + ex.Message);
            }
        }
        public static void RoyalCardMcResponse(DataRow dr)
        {
            try
            {
                Char pad = '0';
                System.Text.StringBuilder BMess = new System.Text.StringBuilder();
                BMess.Append("Enrollment no. : " + (dr["id"].ToString().PadLeft(4, pad)) + "<br/><br/>");
                BMess.Append("Dear ");
                BMess.Append(dr["Gender"] + " ");
                BMess.Append(dr["FirstName"] + " ");
                BMess.Append(dr["LastName"]);
                BMess.Append("<br/><br/>");
                BMess.Append("<b>Greetings from Kingdom Of Dreams!!!</b><br/>" +
                     "Welcome to <b>The Royal Card Membership</b> Programme of Kingdom Of Dreams.<br/>" +
                     "You have been especially selected for direct membership to the Royal Platinum Tier. This level of membership is attained by valued members upon achieving a spend of Rupees 50,000. The programme has been thoughtfully designed to offer you an unmatched experience to fulfill your entertainment needs.<br/>" +
                     "As a Royal Platinum Card member, you have access to a world of privileges & services which includes, amongst others, separate entry podium & free valet services, generous reward points on on-going spends & bonus points on special occasions, free vouchers, special offers on upcoming events and much more!<br/>" +
                     "<b>Please note, your Royal Platinum Membership Card will be delivered to you in-person on your visit to Kingdom of Dreams. Please ensure you carry your registered WORLD MasterCard for verification purpose. You are requested to inform us(9654124167/9540002851/0124-4847435) in advance about your date of visit to avoid any delay.</b><br/>" +
                     "Please find the benefit grid of <b>Royal Card Membership Programme</b> attached for your ready reference.<br/>" +
                     "We look forward to an enduring association with you.<br/><br/>");





                BMess.Append("Sincerely ," + "<br/>");
                BMess.Append("Loyalty Department,Kingdom of Dreams" + "<br/>");
                BMess.Append("Tel:- 0124-4847435/9654124167" + "<br/>" + " Email Id: loyalty.programme@kingdomofdreams.co.in" + "<br/>");


                if (SendMailToRoyality(dr["Email"].ToString(), dr["FirstName"].ToString(), dr["id"].ToString(), BMess))
                    KoDTicketing.GTICKV.LogEntry(dr["Email"].ToString(), "Mail Sent", "23", dr["FirstName"].ToString());

                //if (SendMailToCustomer(CocertEmailId2, dr["Name"].ToString(), BMess))
                //    KoDTicketing.GTICKV.LogEntry(dr["Email"].ToString(), "Mail Sent to concert user", "15", dr["FirstName"].ToString());

            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Email Notification Error: " + ex.Message);
            }
        }
        public static void RoyalCardMcResponseloyality(DataRow dr, string royal)
        {
            try
            {
                Char pad = '0';
                System.Text.StringBuilder BMess = new System.Text.StringBuilder();
                BMess.Append("Dear Loyalty Team,<br/><br/>Please find below Platinum Royal Card enrollment details for Master Card – World Card promotion:<br/><br/>");
                BMess.Append("Enrollment no. : " + (dr["id"].ToString().PadLeft(4, pad)) + "<br/>");
                BMess.Append("Gender : " + dr["Gender"] + "<br/>");
                BMess.Append("First Name : " + dr["FirstName"] + "<br/>");
                BMess.Append("Last Name : " + dr["LastName"] + "<br/>");
                BMess.Append("Address : " + dr["Address"] + "<br/>");
                BMess.Append("City : " + dr["City"] + "<br/>");
                BMess.Append("Country : " + dr["Country"] + "<br/>");
                BMess.Append("Pin Code : " + dr["Pin"] + "<br/>");
                BMess.Append("Email : " + dr["Email"] + "<br/>");
                BMess.Append("Mobile No : " + dr["MobileNo"] + "<br/>");
                BMess.Append("Date Of Birth : " + dr["DateOfBirth"] + "<br/>");
                BMess.Append("Marital Status : " + dr["MaritalStatus"] + "<br/>");
                if (dr["MaritalStatus"].ToString() == "Married")
                {
                    BMess.Append("Marriage Anniversary : " + dr["Marriage Anniversary"] + "<br/>");
                }
                BMess.Append("First six digit of card : " + dr["CardNo"] + "<br/>");
                BMess.Append("Bank Name : " + dr["BankName"] + "<br/>");
                BMess.Append("Card Type : " + dr["CardType"] + "<br/>" + "<br/>");
                BMess.Append("Regard ," + "<br/>");
                BMess.Append("Loyalty Department,Kingdom of Dreams" + "<br/>");
                BMess.Append("For any queries contact Customer Support" + "<br/>");
                BMess.Append("Tel:- 0124-4847435 Email Id: loyalty.programme@kingdomofdreams.co.in" + "<br/>");


                //if (SendMailToCustomer(dr["Email"].ToString(), dr["FirstName"].ToString(), BMess))
                //    KoDTicketing.GTICKV.LogEntry(dr["Email"].ToString(), "Mail Sent", "15", dr["FirstName"].ToString());

                if (SendMailToRoyality(royal, dr["FirstName"].ToString(), dr["id"].ToString(), BMess))
                    KoDTicketing.GTICKV.LogEntry(dr["Email"].ToString(), "Mail Sent to concert user", "23", dr["FirstName"].ToString());

            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Email Notification Error: " + ex.Message);
            }
        }


        public static void ValentinePaymentSuccess(DataRow dr, string VLReceiptNo, string BookingID)
        {
            try
            {
                System.Text.StringBuilder BMess = new System.Text.StringBuilder();
                BMess.Append("Dear Mr/Mrs ");
                BMess.Append(dr["Name"]);
                BMess.Append("<br/><br/>");

                BMess.Append("We thank you for booking A Royal Valentine Affair, at Kingdom of Dreams. Please print this mail to claim your tickets from the Box Office at Kingdom of Dreams.<br/><br/>");
                BMess.Append("Booking ID : " + dr["BookingId"] + "<br/>");
                BMess.Append("Guest Name : " + dr["Name"] + "<br/>");
                BMess.Append("Event : Valentine Day <br/>");
                BMess.Append("Event Date : Friday, Feb 14, 2014 <br/>");
                BMess.Append("Entry Time : 6:00 Pm Onwards. <br/>");
                BMess.Append("Ticket Qty. : " + dr["Quantity"] + "<br/>");
                BMess.Append("Category   : Couple - " + dr["Package"] + "<br/>");
                Double amount = Convert.ToDouble(dr["TotalAmount"]);
                BMess.Append("Total Amount : Rs. " + amount + ".00 <br/><br/>");

                //if (Convert.ToInt64(dr["Package"].ToString()) == 5999)
                //{
                //    BMess.Append("Please call us on 0124-4528000/6677000 to confirm your seats for Jhumroo on 14th Feb 2012 and your Reverse Bungee air launch. All seats are subject to availability, till offer lasts. Limited seats available.<br/><br/>");

                //}
                //if (Convert.ToInt64(dr["Package"].ToString()) == 3999)
                //{
                //    BMess.Append("Please call us on 0124-4528000/6677000 to confirm your Reverse Bungee air launch. Limited seats available.<br/><br/>");

                //}
                //if (Convert.ToInt64(dr["Package"].ToString()) == 3499)
                //{
                //    BMess.Append("Please call us on 0124-4528000/6677000 to confirm your night with DJ, snacks, drinks, & buffet dinner.<br/><br/>");

                //}
                BMess.Append("<b><u>Admission Rights</u></b><br/><br/>");
                BMess.Append("1. Infants or Children below age of 2 years are strictly not allowed inside the Nautanki Mahal theater.<br/>");
                BMess.Append("2. Please carry valid ID proof ( Stating Date Of Birth ) along with the ticket  for entry to the event . Alcohol will not be served to the guests under the age of 25 years of age.<br/>");
                BMess.Append("3. Please carry the Credit Card used for booking tickets or its copy for verification.<br/>");
                BMess.Append("4. Entry starts 6pm onwards.<br/>");
                BMess.Append("5. As per policy we do not cancel/refund/change any tickets once booked<br/><br/>");
                BMess.Append("We wish you a pleasant experience at Kingdom of Dreams.<br/><br/>");
                BMess.Append("<b>Best Compliments</b><br/><br/>");
                BMess.Append("<b>Team Kingdom of Dreams</b>");

                if (SendMailToCustomer(dr["EmailId"].ToString(), dr["Name"].ToString(), BMess))
                    KoDTicketing.GTICKV.LogEntry(VLReceiptNo.ToString(), "Mail Sent", "23", dr["BookingId"].ToString());
            }

            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Email Notification Error: " + ex.Message);
            }
        }
        public static void DandiyaPaymentSuccess(DataRow dr, string VLReceiptNo, string BookingID)
        {
            try
            {
                System.Text.StringBuilder BMess = new System.Text.StringBuilder();
                BMess.Append("Dear Mr/Mrs ");
                BMess.Append(dr["Name"]);
                BMess.Append("<br/><br/>");
                BMess.Append("We thank you for booking A Royal Dandiya night, at Kingdom of Dreams. Please print this mail to claim your tickets from Kingdom of Dreams.<br/><br/>");
                BMess.Append("Booking ID : " + dr["BookingId"] + "<br/>");
                BMess.Append("Guest Name : " + dr["Name"] + "<br/>");
                BMess.Append("Event : Dandiya night <br/>");
                BMess.Append("Date Of Event : " + dr["Event Date"] + " ,7:00 pm" + "<br/>");
                BMess.Append("Package Type : " + dr["Package Type"] + "<br/>");
                BMess.Append("Package Qty. : " + dr["Quantity"] + "<br/>");
                Double amount = Convert.ToDouble(dr["TotalAmount"]);
                BMess.Append("Total Amount : Rs. " + amount + ".00 <br/><br/>");
                BMess.Append("As per policy we do not cancel/refund/change any tickets once booked<br/><br/>");
                BMess.Append("We wish you a pleasant experience at Kingdom of Dreams.<br/><br/>");
                BMess.Append("<b>Best Compliments</b><br/><br/>");
                BMess.Append("<b>Team Kingdom of Dreams</b>");

                if (SendMailToCustomer(dr["EmailId"].ToString(), dr["Name"].ToString(), BMess))
                    KoDTicketing.GTICKV.LogEntry(VLReceiptNo.ToString(), "Mail Sent", "23", dr["BookingId"].ToString());
            }

            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Email Notification Error: " + ex.Message);
            }
        }

        public static void IDBIReturnReceipt(string error, string AdminId)
        {
            AdminId = "bharatgarg.imsec@gmail.com";
            try
            {
                System.Text.StringBuilder BMess = new System.Text.StringBuilder();
                BMess.Append(error.ToString());
                if (AdminId != "")
                {
                    SendMailToTestUser(AdminId.ToString(), "Admin", BMess);
                }
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Email Sending To TestUser Error: " + ex.Message);
            }
        }
    }




    /// <summary>
    /// Summary description for Mail
    /// </summary>
    /// 
    public class Mail
    {

        public String fileName, imageName;
        public Mail()
        {

        }
        private bool check_EMail_Address(string emailID)
        {
            Regex emailregex = new Regex("(?<user>[^@]+)@(?<host>.+)");
            Match m = emailregex.Match(emailID);
            return m.Success;
        }
        public class MailData : Mail
        {
            public string to = null;
            public string toName = null;
            public string subject = null;
            public string bodyMessage = null;
            public string from = null;
            public string fromName = null;
            public string smtpServer = null;
            public string fromUID = null;
            public string fromPwd = null;
            public string tocc = null;
        }
        public bool sendMail_Net(MailData aMail, bool asyncSend)
        {
            try
            {
                MailMessage oMsg = new MailMessage();
                oMsg.From = new MailAddress(aMail.from, aMail.fromName);
                oMsg.To.Add(new MailAddress(aMail.to, aMail.toName));
                if (aMail.tocc != null)
                    oMsg.CC.Add(aMail.tocc);
                oMsg.Subject = aMail.subject;
                if (aMail.subject.Contains("Royal Card"))
                {
                    Attachment mail = new Attachment("C:\\inetpub\\wwwroot\\TicketLive\\images\\Benefit Grid.jpg");
                    oMsg.Attachments.Add(mail);
                }
                oMsg.SubjectEncoding = System.Text.Encoding.UTF8;
                oMsg.Body = aMail.bodyMessage;
                oMsg.BodyEncoding = System.Text.Encoding.UTF8;
                oMsg.IsBodyHtml = true;

                oMsg.Priority = MailPriority.High;

                SmtpClient oSmtp = new SmtpClient(aMail.smtpServer);
                oSmtp.Port = 587;
                oSmtp.UseDefaultCredentials = false;
                NetworkCredential oCredential = new NetworkCredential(aMail.fromUID, aMail.fromPwd);
                oSmtp.Credentials = oCredential;
                oSmtp.EnableSsl = true;

                if (check_EMail_Address(aMail.to) == true)
                {
                    if (true == asyncSend)
                    {
                        // Set the method that is called back when the send operation ends.
                        oSmtp.SendCompleted += new
                        SendCompletedEventHandler(SendCompletedCallback);
                        // The userState can be any object that allows your callback 
                        // method to identify this send operation.
                        // For this example, the userToken is a string constant.
                        string userState = "test message1";
                        oSmtp.SendAsync(oMsg, userState);

                    }
                    else
                    {
                        oSmtp.Send(oMsg);
                    }
                }
                else
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Invalid email address." + aMail.to);
                    return false;
                }
            }
            catch (SmtpException ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Exception thrown while sending email. Exception: " + ex.Message);
                return false;
            }
            return true;
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(string.Format("[{0}] {1}", token, e.Error.ToString()));
            }
            else
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Email sent.");
            }
        }

    }
}