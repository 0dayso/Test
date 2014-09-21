using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Collections;

namespace RoyalClassDBLib
{
    [DataContract]
    public class Class1
    {      
        public string PrintString()
        {
            return "Hiiiiiiiiiiiiiii pooja";
        }      
        public ArrayList BindYear()
        {
            ArrayList arr = new ArrayList();
            for (int i = 1940; i <= DateTime.Now.Year; i++)
            {  
                arr.Add(i);
            }
            return arr;
        }
    }
}
