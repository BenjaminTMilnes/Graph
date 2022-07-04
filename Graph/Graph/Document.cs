using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class Document
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Abstract { get; set; }
        public IList<string> Keywords { get; set; }
        public string Draft { get; set; }
        public string Edition { get; set; }
        public string ISBN { get; set; }
        public DateTime PublicationDate { get; set; }
        public IList<Section> Sections { get; set; }

        public Document()
        {
            Keywords = new List<string>();
            PublicationDate = DateTime.Now;
            Sections = new List<Section>();
        }
    }
}
