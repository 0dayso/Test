using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace RoyalWebApp.Account
{
    public partial class UserCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["RegId"] != null && Session["RegId"] != "")
                {
                    BindData();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
        void BindData()
        {
            if (Session["RegId"] != null && Session["RegId"] != "")
            {
                EntityServiceReference.EntityServiceClient ServiceClient1 = new EntityServiceReference.EntityServiceClient();
                ServiceClient1.Open();
                // get card details by m id
                var dbreturns = ServiceClient1.GetUserDetails(Session["RegId"].ToString());
                String Name = dbreturns[0].FirstName.ToString();
                lblName.Text = Name;
                lblMemberID.Text = dbreturns[0].MemberID.ToString();
                lblDate.Text = dbreturns[0].DateonCard.ToString();
                if (dbreturns.Length > 0)
                {
                    String CardType = dbreturns[0].CardCode.ToString();
                    // get card details by card type
                    EntityServiceReference.EntityServiceClient ServiceClient = new EntityServiceReference.EntityServiceClient();
                    ServiceClient.Open();
                    var arr = ServiceClient.GetCardsDetailsbyType(CardType);
                    GridCardList.DataSource = arr;
                    GridCardList.DataBind();
                    ServiceClient.Close();
                    if (CardType.ToString().ToUpper().Equals("BLUE"))
                    {
                        ImgCard.ImageUrl = "../Skins/images/blueCard.png";
                    }
                    else if (CardType.ToString().ToUpper().Equals("GOLD"))
                    {
                        ImgCard.ImageUrl = "../Skins/images/goldCard.png";
                    }
                    else if (CardType.ToString().ToUpper().Equals("PLATINUM"))
                    {
                        ImgCard.ImageUrl = "../Skins/images/platinumCard.png";
                    }
                    else if (CardType.ToString().ToUpper().Equals("PURPLE"))
                    {
                        ImgCard.ImageUrl = "../Skins/images/purpleCard.png";
                    }
                    else if (CardType.ToString().ToUpper().Equals("TITANIUM"))
                    {
                        ImgCard.ImageUrl = "../Skins/images/titaniumCard.png";
                    }
                    else if (CardType.ToString().ToUpper().Equals("PRINCE"))
                    {
                        ImgCard.ImageUrl = "../Skins/images/princeCard.png";
                    }
                    else if (CardType.ToString().ToUpper().Equals("KING"))
                    {
                        ImgCard.ImageUrl = "../Skins/images/kingCard.png";
                    }
                }
                EntityServiceReference.EntityServiceClient ServiceClient2 = new EntityServiceReference.EntityServiceClient();
                ServiceClient2.Open();
                var CardDetails = ServiceClient2.GetCardsBalanceByMemberId(Session["RegId"].ToString());
                String RemainingAmount = CardDetails[0].RemainingAmount.ToString();
                lblavailableVal.Text = RemainingAmount;
                String RemainingPoints = CardDetails[0].RemainingPoints.ToString();
                lblAvailablePoints.Text = RemainingPoints;
                String After24Hrs = CardDetails[0].After24HourPoints.ToString();
                lblAfter24hrs.Text = After24Hrs;
                String RechargeAmount = CardDetails[0].RechargeAmount.ToString();
                lblTotalRedemption.Text = RechargeAmount;
                ServiceClient1.Close();
                ServiceClient2.Close();

            }
        }

        protected void BtnBookTickets_Click(object sender, EventArgs e)
        {
            EntityServiceReference.EntityServiceClient ServiceRefClient = new EntityServiceReference.EntityServiceClient();
            ServiceRefClient.Open();
            var arr = ServiceRefClient.GetUserDetails(Session["RegId"].ToString());
            String FirstName = arr[0].FirstName.ToString();
            String LastName = arr[0].LastName.ToString();
            String EmailID = arr[0].Email.ToString();
            String MobileNo = arr[0].Mobile.ToString();
            String Address = arr[0].Address.ToString();
            String Address2 = arr[0].Address2.ToString();
            ServiceRefClient.Close();

            //Response.Redirect(ConfigurationManager.AppSettings.Get("MsTicketURL").ToString() + "?RemainingAmount=" + Server.UrlEncode(Encrypt(lblavailableVal.Text)) + "&RemainingPoints=" + Server.UrlEncode(Encrypt(lblAvailablePoints.Text)) + "&MemberShipId=" + Server.UrlEncode(Encrypt(Session["RegId"].ToString())) + "&FirstName=" + Server.UrlEncode(Encrypt(FirstName)) + "&LastName=" + Server.UrlEncode(Encrypt(LastName))+"&Email="+Server.UrlEncode(Encrypt(EmailID))+"&Mobile="+Server.UrlEncode(Encrypt(MobileNo))+"&Address="+Server.UrlEncode(Encrypt(Address)));
            string password = "A87C7B95932E9";
            string ss = Common.Encrypt(lblAvailablePoints.Text, password);
            string ss1 = Common.Encrypt(FirstName, password);
            string ss2 = Common.Encrypt(LastName, password);
            string url = ConfigurationManager.AppSettings.Get("MsTicketURL").ToString() + "?RemainingAmount=" + Server.UrlEncode(Common.Encrypt(lblavailableVal.Text, password))
                + "&RemainingPoints=" + Server.UrlEncode(Common.Encrypt(lblAvailablePoints.Text, password)) + "&MemberShipId=" +
                Server.UrlEncode(Common.Encrypt(Session["RegId"].ToString(), password)) + "&FirstName=" + Server.UrlEncode(Common.Encrypt(FirstName, password))
                + "&LastName=" + Server.UrlEncode(Common.Encrypt(LastName, password)) + "&Email="
                + Server.UrlEncode(Common.Encrypt(EmailID, password)) + "&Mobile=" + Server.UrlEncode(Common.Encrypt(MobileNo, password)) +
                "&Address=" + Server.UrlEncode(Common.Encrypt(Address, password));

            Response.Redirect(url);

            //+"&Address2="+Server.UrlEncode(Encrypt(Address2))
        }
        public string Encrypt(string val)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(val);
            var encBytes = System.Security.Cryptography.ProtectedData.Protect(bytes, new byte[0], System.Security.Cryptography.DataProtectionScope.LocalMachine);
            return Convert.ToBase64String(encBytes);
        }
    }
}
