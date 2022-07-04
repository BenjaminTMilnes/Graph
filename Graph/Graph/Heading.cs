using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class Heading : FlowElement
    {
        public int Level { get; set; }

        public Heading(int level = 1) : base()
        {
            Level = level;
        }
    }
}
