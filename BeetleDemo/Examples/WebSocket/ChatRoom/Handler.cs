using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using Beetle.WebSockets;
using Beetle.WebSockets.Controllers;
using System.Runtime.Serialization;
using Beetle;
namespace ChatRoom
{
    /// <summary>
    /// Copyright © henryfan 2012		 
    ///Email:	henryfan@msn.com	
    ///HomePage:	http://www.ikende.com		
    ///CreateTime:	2012/12/7 21:45:25
    /// </summary>
    class Handler
    {
        public long Register(string name)
        {
           
            IChannel channel = MethodContext.Current.Channel;
            Console.WriteLine("{0} register name:{1}", channel.EndPoint, name);
            channel.Name = name;
            JsonMessage msg = new JsonMessage();
            User user = new User();
            user.Name = name;
            user.ID = channel.ClientID;
            user.IP = channel.EndPoint.ToString();
            channel.Tag = user;
            msg.type = "register";
            msg.data = user;
            foreach (IChannel item in channel.Server.GetOnlines())
            {
                if (item != channel)
                    item.Send(msg);
            }
            return channel.ClientID;
        }
        public IList<User> List()
        {
            IChannel channel = MethodContext.Current.Channel;
            IList<User> result = new List<User>();
            foreach (IChannel item in channel.Server.GetOnlines())
            {
                if (item != channel)
                    result.Add((User)item.Tag);
            }
            return result;
        }
        public void Say(string Content)
        {
            IChannel channel = MethodContext.Current.Channel;
            JsonMessage msg = new JsonMessage();
            SayText st = new SayText();
            st.Name = channel.Name;
            st.ID = channel.ClientID;
            st.Date = DateTime.Now;
            st.Content = Content;
            st.IP = channel.EndPoint.ToString();
            msg.type = "say";
            msg.data = st;
            foreach (IChannel item in channel.Server.GetOnlines())
            {
                item.Send(msg);
            }

        }

    }

   
    public class User
    {
        
        public string Name { get; set; }
    
        public long ID { get; set; }
       
        public string IP { get; set; }

    }
    
    public class SayText
    {
     
        public string Name { get; set; }
      
        public long ID { get; set; }
        
        public string IP { get; set; }
     
        public DateTime Date { get; set; }
     
        public string Content { get; set; }
    }

   

}
