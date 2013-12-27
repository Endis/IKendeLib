using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.Tracker
{
    public interface IAppTrackerHandler
    {

        IInfoFormater Formater
        {
            get;
            set;
        }

        AppHost GetHost(IProperties properties);

         IProperties Register(IProperties properties);

         object GetInfo(IProperties properties);
    }
}
