using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RoyalWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEntityService" in both code and config file together.
    [ServiceContract]
    public interface IEntityService
    {
        //get user
        [OperationContract]
        List<Proc_ViewAccountDetails_Result> GetUserDetails(String memberId);
        //insert user
        [OperationContract]
        List<Proc_InsertWebAccount_Result1>  InsertUserDetails(String membershipid, String salutation, String firstName, String lastName, String address, String addressTwo, String city, String country, String phoneNo, String mobileNo, String email, DateTime dateOfBirth, DateTime anniversaryDate, String maritalStatus, String gender, Decimal paymentAmount, DateTime paymentDate, String cardNo, String bankName, String userName, String password, String designation, bool boxOffiec, bool coupon);
        //register user without payment(as per policy)
        [OperationContract]
        List<Proc_RegisterUsers_Result> registeruserwithoutpayment(String membershipid, String salutation, String firstName, String lastName, String address, String addressTwo, String city, String country, String phoneNo, String mobileNo, String email, DateTime dateOfBirth, DateTime anniversaryDate, String maritalStatus, String gender, Decimal paymentAmount, DateTime paymentDate, String cardNo, String bankName, String userName, String password, String designation, bool boxOffiec, bool coupon);
        // getlogin      
        [OperationContract]
        List<Proc_Login_Result> GetUserLogin(String Membershipid, String pwd);
       
        //Update Password 
        [OperationContract]
        int ChangePassword(String Membershipid, String oldpwd, String newpwd);
        
        //Forgot Password 
         [OperationContract]
        List<Proc_ForgetPassword_Result> ForgotPassword(String Membershipid, String emailid);
       
        //update details
         [OperationContract]
         int UpdateUserDetails(String membershipid, String salutation, String firstName, String lastName, String address, String addressTwo, String city, String country, String mobileNo, String email, DateTime dateOfBirth, DateTime anniversaryDate, String maritalStatus, String designation, String phoneNo);
            
         // getmaxmemberid
         [OperationContract]
         List<Proc_GetTopWebId_Result1> GetMaxMemberId();
      
       // GetUserDetails ByEmailID 
         [OperationContract]
         List<Proc_GetWebAccountByEmailId_Result> GetUserDetailsByEmailId(String useremailid);

         // Get all cards Details
         [OperationContract]
         List<Proc_AllTierDetails_Result> GetAllCardsDetails();

         // Get cards Details by type
         [OperationContract]
         List<Proc_TierDetails_Result> GetCardsDetailsbyType(String CardType);

         //update reg payment details
         [OperationContract]
         int UpdatePaymentDetails(String membershipid, int PayStatus,int Readytocreate, decimal paidamt,String receiptno);
        
         // Get cards Balance by memberid   
         [OperationContract]
         List<Proc_PointStatment_Result> GetPointStatusByMemberId(String membershipid);

         // Get cards Details by type
         [OperationContract]
         List<Proc_GetMemberBalance_Result> GetCardsBalanceByMemberId(String membershipid);

         //insert top up by memberid 
         [OperationContract]        
         List<Proc_CardTransaction_Result> InsertTopUpDetails(String membershipid, Decimal paidAmount, DateTime paiddate, String PaidReceiptNo, int TransType);

         //update top up by transid 
         [OperationContract]
         int UpdateTopUpStatus(String Transactionid, int success,int transType);

         //get  temp web details by temp memberid 
         [OperationContract]
         List<Proc_GetWebAccountByTempWebId_Result> GetTempUserDetailsByWebId(String tempwebid);

         [OperationContract]
         int EmailExists(String email);

         [OperationContract]
         int MobilenoExist(String mobile);

         [OperationContract]
         int FirstTimeLogin(String memberid, DateTime doB);

         [OperationContract]
         bool PasswordExistsCheck(String memberid, DateTime doB);
    }
}
