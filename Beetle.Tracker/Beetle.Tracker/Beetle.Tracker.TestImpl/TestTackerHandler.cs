using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.Tracker.TestImpl
{
    public class TestTackerHandler : Beetle.Tracker.IAppTrackerHandler
    {
        public TestTackerHandler()
        {
            Formater = new TestFormater();
        }

        private long mCursorIndex = 0;

        private List<Group> mGroups = new List<Group>();

        public IInfoFormater Formater
        {
            get;
            set;
        }

        public IProperties Register(IProperties properties)
        {
            lock (mGroups)
            {
                TestProperties tp = new TestProperties();
                tp.FromHeaders(properties.ToHeaders());
                Group group = mGroups.Find(e => e.Name == tp.Group);
                if (group == null)
                {
                    group = new Group();
                    group.Name = tp.Group;
                    group.Nodes = new List<Node>();
                    group.Nodes.Add(new Node { Name = tp.Node, Host = tp.Host, Port = tp.Port, LastTrackTime=DateTime.Now });
                    mGroups.Add(group);
                }
                else
                {
                    Node node = group.Nodes.Find(n =>  n.Name== tp.Node );
                    if(node !=null)
                        node.LastTrackTime = DateTime.Now;
                    else
                        group.Nodes.Add(new Node { Name = tp.Node, Host = tp.Host, Port = tp.Port, LastTrackTime = DateTime.Now });
                }
                return new Properties();
            }
        }

        public object GetInfo(IProperties properties)
        {
            TestProperties tp = new TestProperties();
            tp.FromHeaders(properties.ToHeaders());
            Group group = mGroups.Find(e => e.Name == tp.Group);
            if (group == null)
                return new Group();
            return group;
        }

        public AppHost GetHost(IProperties properties)
        {
            int g = 0;
            while (g < mGroups.Count)
            {
                int i = 0;
                Group group = mGroups[(int)(mCursorIndex % mGroups.Count)];
                mCursorIndex++;
                while (i < group.Nodes.Count)
                {

                    Node node = group.Nodes[(int)(group.CursorIndex % group.Nodes.Count)];
                    group.CursorIndex++;
                    if ((DateTime.Now - node.LastTrackTime).TotalSeconds < 5)
                        return new AppHost { Host = node.Host, Port = int.Parse(node.Port) };
                    i++;
                }
               
                g++;
            }
            return null;

           
        }
    }
}
