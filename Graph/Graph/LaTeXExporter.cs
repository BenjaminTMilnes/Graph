using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Graph
{
    public class LaTeXExporter
    {
        public virtual void ExportDocument(Document document, string filePath)
        {
            var t = ExportDocument(document);

            File.WriteAllText(filePath, t, Encoding.UTF8);
        }

        public virtual string ExportDocument(Document document)
        {
            var t = "";

            t += "\\documentclass{book}\n";

            t += ExportUsePackages(document);

            t += "\\setlength{\\parindent}{0pt}\n\n";
            t += $"\\title{{{document.Title}}}\n\n";
            t += "\\begin{document}\n\n";

            foreach (var section in document.Sections)
            {
                t += ExportElement(section);
            }

            t += "\n\n\\end{document}";

            return t;
        }

        public virtual string ExportUsePackages(Document document)
        {
            var t = "";

            t += "\\usepackage[utf8]{inputenc}\n";
            t += "\\usepackage[a4paper, margin=2.5cm]{geometry}\n";
            t += "\\usepackage{fancyhdr}\n";
            t += "\\usepackage{tikz}\n\n";
            t += "\\usepackage{blindtext}\n\n";

            return t;
        }

        public virtual string ExportElements(IList<IElement> elements)
        {
            var t = "";

            foreach (var element in elements)
            {
                t += ExportElement(element);
            }

            return t;
        }

        public virtual string ExportElement(IElement element)
        {
            if (element is FlowElement)
            {
                if (element is Section)
                {
                    return string.Join("\n\n", (element as FlowElement).Subelements.Select(e => ExportElement(e)));
                }
                else if (element is Paragraph || element is Division)
                {
                    return "\n\n" + string.Join("", (element as FlowElement).Subelements.Select(e => ExportElement(e)));
                }
                else if (element is Heading)
                {
                    var heading = element as Heading;

                    if (heading.Level == 1)
                    {
                        return "\n\n\\part{" + string.Join("", (element as FlowElement).Subelements.Select(e => ExportElement(e))) + "}";
                    }
                    if (heading.Level == 2)
                    {
                        return "\n\n\\chapter{" + string.Join("", (element as FlowElement).Subelements.Select(e => ExportElement(e))) + "}";
                    }
                    if (heading.Level == 3)
                    {
                        return "\n\n\\section*{" + string.Join("", (element as FlowElement).Subelements.Select(e => ExportElement(e))) + "}";
                    }
                    if (heading.Level == 4)
                    {
                        return "\n\n\\subsection*{" + string.Join("", (element as FlowElement).Subelements.Select(e => ExportElement(e))) + "}";
                    }
                    if (heading.Level == 5)
                    {
                        return "\n\n\\subsubsection*{" + string.Join("", (element as FlowElement).Subelements.Select(e => ExportElement(e))) + "}";
                    }
                    if (heading.Level == 6)
                    {
                        return "\n\n\\paragraph*{" + string.Join("", (element as FlowElement).Subelements.Select(e => ExportElement(e))) + "}";
                    }
                    if (heading.Level == 7)
                    {
                        return "\n\n\\subparagraph*{" + string.Join("", (element as FlowElement).Subelements.Select(e => ExportElement(e))) + "}";
                    }
                }
                else if (element is UnorderedList)
                {
                    return "\n\n\\begin{itemize}\n\t\\setlength{\\itemsep}{-0.25em}" + string.Join("", (element as FlowElement).Subelements.Select(e => ExportElement(e))) + "\n\\end{itemize}";
                }
                else if (element is OrderedList)
                {
                    return "\n\n\\begin{enumerate}\n\t\\setlength{\\itemsep}{-0.25em}" + string.Join("", (element as FlowElement).Subelements.Select(e => ExportElement(e))) + "\n\\end{enumerate}";
                }
                else if (element is ListItem)
                {
                    return "\n\t\\item " + string.Join("", (element as FlowElement).Subelements.Select(e => ExportElement(e)));
                }
            }
            else if (element is TextElement)
            {
                return (element as TextElement).Text;
            }

            return "";
        }
    }
}
