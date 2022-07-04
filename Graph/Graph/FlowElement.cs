using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class FlowElement : IElement
    {
        public FlowElement Superelement { get; set; }
        public int Depth { get; set; }
        public IList<IElement> Subelements { get; set; }

        public FlowElement()
        {
            Superelement = null;
            Depth = 0;
            Subelements = new List<IElement>();
        }
    }
}
