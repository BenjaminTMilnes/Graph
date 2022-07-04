using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class Section : FlowElement
    {
        public Document Document { get; set; }
        public string PageTemplateReference { get; set; }
        public bool Exclude { get; set; }
    }
}
