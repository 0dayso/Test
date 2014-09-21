using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.Linq.Mapping; 
using System.Data;
using System.Data.Linq;

namespace RoyalWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class ServiceRoyal:IServiceRoyal
    {
        //public Array GetAllUsers()
        //{
           //   LinqToSqlClsDataContext LinqContext = new LinqToSqlClsDataContext();
           /// try
           // {              
           //     LinqContext.Connection.Open();               
           // }
           // catch (Exception ex)
           // {

           // }
           // finally
           // {
           //     LinqContext.Connection.Close();
           // }
           // return LinqContext.RegistrationDetailsGetALL_Admin().ToArray();
       //}

        public String GetPrint()
        {
            return "Hiiiiiiiiiiiii";
        }
    }   
}
