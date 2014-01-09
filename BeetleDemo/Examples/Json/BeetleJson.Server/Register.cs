using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeetleJson
{
   [Serializable]
    public class Register
    {
        
        public string UserName
        {
            get;
            set;
        }
       
        public string EMail
        {
            get;
            set;
        }
       
        public DateTime RegTime
        {
            get;
            set;
        }
    }
}
