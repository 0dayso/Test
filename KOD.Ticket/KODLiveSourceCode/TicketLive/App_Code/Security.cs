using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using KoDTicketing.BusinessLayer;

/// <summary>
/// Summary description for Security
/// </summary>
[WebService(Namespace = "http://msticket.kingdomofdreams.in/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
//[System.Web.Script.Services.ScriptService]
public class Security : System.Web.Services.Protocols.SoapHeader
{
    public String username;
    public String password;
}

public class SecurityHeaderValidation
{
    public static bool Validate(Security objSecurity)
    {
        return GTICKBOL.ValidateAgent(objSecurity.username, objSecurity.password);
    }
}

