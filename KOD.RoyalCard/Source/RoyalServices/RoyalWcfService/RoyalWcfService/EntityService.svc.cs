using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RoyalWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EntityService" in code, svc and config file together.
    public class EntityService : IEntityService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //view details
        public List<Proc_ViewAccountDetails_Result> GetUserDetails(String memberId)
        {
            log.Warn("this is test log memberId: " + memberId);
            //log.Error("exception message", exception);
            //log.Debug("debug message",obj);
            LOYALTYNMEntities dataContext = new LOYALTYNMEntities();
            return dataContext.Proc_ViewAccountDetails(memberId).ToList();
        }
        //insert user details
        public List<Proc_InsertWebAccount_Result1> InsertUserDetails(String membershipid, String salutation, String firstName, String lastName, String address, String addressTwo, String city, String country, String phoneNo, String mobileNo, String email, DateTime dateOfBirth, DateTime anniversaryDate, String maritalStatus, String gender, Decimal paymentAmount, DateTime paymentDate, String cardNo, String bankName, String userName, String password, String designation, bool boxOffiec, bool coupon)
        {
            LOYALTYNMEntities dataContext1 = new LOYALTYNMEntities();
            return  dataContext1.Proc_InsertWebAccount(membershipid, salutation, firstName, lastName, address, addressTwo, city, country, phoneNo, mobileNo, email, dateOfBirth, anniversaryDate, maritalStatus, gender, paymentAmount, paymentDate, cardNo, bankName, userName, password, designation, boxOffiec, coupon).ToList();
             
        }

        public List<Proc_RegisterUsers_Result> registeruserwithoutpayment(String membershipid, String salutation, String firstName, String lastName, String address, String addressTwo, String city, String country, String phoneNo, String mobileNo, String email, DateTime dateOfBirth, DateTime anniversaryDate, String maritalStatus, String gender, Decimal paymentAmount, DateTime paymentDate, String cardNo, String bankName, String userName, String password, String designation, bool boxOffiec, bool coupon)
        {
            LOYALTYNMEntities dataContext1 = new LOYALTYNMEntities();
            return dataContext1.Proc_RegisterUsers(membershipid, salutation, firstName, lastName, address, addressTwo, city, country, phoneNo, mobileNo, email, dateOfBirth, anniversaryDate, maritalStatus, gender, paymentAmount, paymentDate, cardNo, bankName, userName, password, designation, boxOffiec, coupon).ToList();
            
        }
        //Get Login details
        public List<Proc_Login_Result> GetUserLogin(String Membershipid, String pwd)
        {
            LOYALTYNMEntities dataContext1 = new LOYALTYNMEntities();
            return dataContext1.Proc_Login(Membershipid, pwd).ToList();
        }
        //Update Password 
        public int ChangePassword(String Membershipid, String oldpwd, String newpwd)
        {
            LOYALTYNMEntities dataContext1 = new LOYALTYNMEntities();
            int cnfrm = dataContext1.Proc_UpdatePassword(Membershipid, oldpwd, newpwd);
            return cnfrm;
        }
        //Forgot Password 
        public List<Proc_ForgetPassword_Result> ForgotPassword(String Membershipid,String emailid)
        {
            LOYALTYNMEntities dataContext1 = new LOYALTYNMEntities();
            return dataContext1.Proc_ForgetPassword(Membershipid, emailid).ToList();
           
        }       
        //update details
        public int UpdateUserDetails(String membershipid,String salutation,String firstName, String lastName, String address, String addressTwo, String city, String country, String mobileNo, String email, DateTime dateOfBirth, DateTime anniversaryDate, String maritalStatus,String designation, String phoneNo)
        {
            LOYALTYNMEntities dataContext1 = new LOYALTYNMEntities();
            int cnfrm = dataContext1.Proc_UpdateAccountDetails(membershipid, salutation, firstName, lastName, address, addressTwo, city, country, mobileNo, email, dateOfBirth, anniversaryDate, maritalStatus, designation, phoneNo);
            return cnfrm;
        }
        // getTopmemberid       
       public List<Proc_GetTopWebId_Result1> GetMaxMemberId()
        {
             LOYALTYNMEntities dataContext1 = new LOYALTYNMEntities();
             return dataContext1.Proc_GetTopWebId().ToList();
        }
       // GetUserDetails ByEmailID       
       public List<Proc_GetWebAccountByEmailId_Result> GetUserDetailsByEmailId(String useremailid)
       {
           LOYALTYNMEntities dataContext1 = new LOYALTYNMEntities();
           return dataContext1.Proc_GetWebAccountByEmailId(useremailid).ToList();
       }


       // Get all cards Details

       public List<Proc_AllTierDetails_Result> GetAllCardsDetails()
       {
           LOYALTYNMEntities dataContext1 = new LOYALTYNMEntities();
           return dataContext1.Proc_AllTierDetails().ToList();
       }

       // Get cards Details by type     
       public List<Proc_TierDetails_Result> GetCardsDetailsbyType(String CardType)
       {
           LOYALTYNMEntities dataContext1 = new LOYALTYNMEntities();
           return dataContext1.Proc_TierDetails(CardType).ToList();
       }
       //update details
       public int UpdatePaymentDetails(String membershipid, int PayStatus, int Readytocreate, decimal paidamt, String receiptno)
       {
           LOYALTYNMEntities dataContext1 = new LOYALTYNMEntities();
           int cnfrm = dataContext1.Proc_UpdateAccountPaymentDetails(membershipid, PayStatus, Readytocreate, paidamt, receiptno);
           return cnfrm;
       }
       // Get point status by memberid    
       public List<Proc_GetMemberBalance_Result> GetCardsBalanceByMemberId(String membershipid)
       {
           LOYALTYNMEntities dataContext1 = new LOYALTYNMEntities();
           return dataContext1.Proc_GetMemberBalance(membershipid).ToList();
       }
        
       // Get cards Balance by memberid    
       public List<Proc_PointStatment_Result> GetPointStatusByMemberId(String membershipid)
       {
           LOYALTYNMEntities dataContext1 = new LOYALTYNMEntities();
           return dataContext1.Proc_PointStatment(membershipid).ToList();
       }

       //insert top up by memberid    
       public List<Proc_CardTransaction_Result> InsertTopUpDetails(String membershipid,Decimal paidAmount,DateTime paiddate,String PaidReceiptNo, int PaidType)
       {
            LOYALTYNMEntities dataContext1 = new LOYALTYNMEntities();
            return dataContext1.Proc_CardTransaction(membershipid, paidAmount, paiddate, PaidReceiptNo, PaidType).ToList();
           
       }
       //update top up details      
       public int UpdateTopUpStatus(String Transactionid, int success, int transType)
       {
           LOYALTYNMEntities dataContext1 = new LOYALTYNMEntities();
           int cnfrm = dataContext1.Proc_PaymentSuccessfullUpdate(Transactionid, success, transType);
           return cnfrm;
       }

       //get  temp web details by temp memberid    
       public List<Proc_GetWebAccountByTempWebId_Result> GetTempUserDetailsByWebId(String tempwebid)
       {
           LOYALTYNMEntities dataContext1 = new LOYALTYNMEntities();
           return dataContext1.Proc_GetWebAccountByTempWebId(tempwebid).ToList();
       }


       public int EmailExists(String email)
       {
           LOYALTYNMEntities dataContext1 = new LOYALTYNMEntities();
           var query = (from c in dataContext1.Great_Indian_Nautanki_Company_Temp_Web_Booking
                        where c.E_Mail == email
                        select c.No_).Count();
           return query;
       }
       public int MobilenoExist(String mobile)
       {
           LOYALTYNMEntities dataContext1 = new LOYALTYNMEntities();
           var query = (from c in dataContext1.Great_Indian_Nautanki_Company_Temp_Web_Booking
                        where c.Mobile_Phone_No_ == mobile
                        select c.No_).Count();
           return query;
       }
       public int FirstTimeLogin(String memberid, DateTime doB)
       {
           LOYALTYNMEntities dataContext1 = new LOYALTYNMEntities();
           var query = (from c in dataContext1.Great_Indian_Nautanki_Company_Contact
             where c.No_ == memberid && c.DOB==doB
             select c.No_).Count();
           return query;
       }


       public bool PasswordExistsCheck(String memberid, DateTime doB)
       {
           try
           {
               LOYALTYNMEntities dataContext1 = new LOYALTYNMEntities();

               var query = (from c in dataContext1.Great_Indian_Nautanki_Company_Contact
                            where c.No_ == memberid && c.DOB == doB
                            select c.Web_Password).First();
               if (query == null || string.IsNullOrEmpty(query))
                  return false;

               return true;
           }
           catch (Exception ex)
           {
               return false;
           }          
       }

    }
}
