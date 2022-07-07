using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Graph
{
    public class Importer
    {
        public virtual Document ImportDocument(string filePath)
        {
            var xmlDocument = new XmlDocument();

            xmlDocument.Load(filePath);

            var document = new Document();

            if (xmlDocument.SelectNodes("/document/title").Count > 0)
            {
                document.Title = xmlDocument.SelectSingleNode("/document/title").InnerText.Trim();
            }

            if (xmlDocument.SelectNodes("/document/subtitle").Count > 0)
            {
                document.Subtitle = xmlDocument.SelectSingleNode("/document/subtitle").InnerText.Trim();
            }

            if (xmlDocument.SelectNodes("/document/abstract").Count > 0)
            {
                document.Abstract = xmlDocument.SelectSingleNode("/document/abstract").InnerText.Trim();
            }

            if (xmlDocument.SelectNodes("/document/keywords").Count > 0)
            {
                document.Keywords = xmlDocument.SelectSingleNode("/document/keywords").InnerText.Split(',').Select(k => k.Trim()).ToList();
            }

            if (xmlDocument.SelectNodes("/document/draft").Count > 0)
            {
                document.Draft = xmlDocument.SelectSingleNode("/document/draft").InnerText.Trim();
            }

            if (xmlDocument.SelectNodes("/document/edition").Count > 0)
            {
                document.Edition = xmlDocument.SelectSingleNode("/document/edition").InnerText.Trim();
            }

            if (xmlDocument.SelectNodes("/document/isbn").Count > 0)
            {
                document.ISBN = xmlDocument.SelectSingleNode("/document/isbn").InnerText.Trim();
            }

            foreach (var xmlElement in xmlDocument.SelectNodes("/document/sections/section").OfType<XmlElement>())
            {
                var section = new Section();

                section.Subelements = ImportElements(xmlElement.ChildNodes);

                document.Sections.Add(section);
            }

            return document;
        }

        public virtual IList<IElement> ImportElements(XmlNodeList xmlNodes)
        {
            var elements = new List<IElement>();

            foreach (var xmlNode in xmlNodes)
            {
                elements.Add(ImportElement(xmlNode as XmlNode));
            }

            return elements;
        }

        public virtual IElement ImportElement(XmlNode xmlNode)
        {
            if (xmlNode is XmlElement)
            {
                var xmlElement = xmlNode as XmlElement;

                if (xmlElement.Name == "paragraph" || xmlElement.Name == "p")
                {
                    var element = new Paragraph();

                    element.Subelements = ImportElements(xmlElement.ChildNodes);

                    return element;
                }
                if (xmlElement.Name == "division" || xmlElement.Name == "d")
                {
                    var element = new Division();

                    element.Subelements = ImportElements(xmlElement.ChildNodes);

                    return element;
                }
                if (xmlElement.Name == "heading1" || xmlElement.Name == "h1" || xmlElement.Name == "heading2" || xmlElement.Name == "h2" || xmlElement.Name == "heading3" || xmlElement.Name == "h3" || xmlElement.Name == "heading4" || xmlElement.Name == "h4" || xmlElement.Name == "heading5" || xmlElement.Name == "h5" || xmlElement.Name == "heading6" || xmlElement.Name == "h6" || xmlElement.Name == "heading7" || xmlElement.Name == "h7" || xmlElement.Name == "heading8" || xmlElement.Name == "h8" || xmlElement.Name == "heading9" || xmlElement.Name == "h9" || xmlElement.Name == "heading10" || xmlElement.Name == "h10")
                {
                    var element = new Heading();

                    if (xmlElement.Name.StartsWith("heading"))
                    {
                        element.Level = int.Parse(xmlElement.Name.Substring("heading".Length));
                    }
                    else if (xmlElement.Name.StartsWith("h"))
                    {
                        element.Level = int.Parse(xmlElement.Name.Substring("h".Length));
                    }

                    element.Subelements = ImportElements(xmlElement.ChildNodes);

                    return element;
                }
                if (xmlElement.Name == "unordered-list" || xmlElement.Name == "ul")
                {
                    var element = new UnorderedList();

                    element.Subelements = ImportElements(xmlElement.ChildNodes);

                    return element;
                }
                if (xmlElement.Name == "ordered-list" || xmlElement.Name == "ol")
                {
                    var element = new OrderedList();

                    element.Subelements = ImportElements(xmlElement.ChildNodes);

                    return element;
                }
                if (xmlElement.Name == "list-item" || xmlElement.Name == "li")
                {
                    var element = new ListItem();

                    element.Subelements = ImportElements(xmlElement.ChildNodes);

                    return element;
                }
                if (xmlElement.Name == "italic" || xmlElement.Name == "i")
                {
                    var element = new Italic();

                    element.Subelements = ImportElements(xmlElement.ChildNodes);

                    return element;
                }
                if (xmlElement.Name == "bold" || xmlElement.Name == "b")
                {
                    var element = new Bold();

                    element.Subelements = ImportElements(xmlElement.ChildNodes);

                    return element;
                }
                if (xmlElement.Name == "line-break" || xmlElement.Name == "lb")
                {
                    var element = new LineBreak();

                    return element;
                }
                if (xmlElement.Name == "image")
                {
                    var element = new Image();

                    if (xmlElement.HasAttribute("location"))
                    {
                        element.Location = xmlElement.GetAttribute("location");
                    }

                    return element;
                }
                if (xmlElement.Name == "figure" || xmlElement.Name == "f")
                {
                    var element = new Figure();

                    element.Subelements = ImportElements(xmlElement.ChildNodes);

                    return element;
                }
                if (xmlElement.Name == "description")
                {
                    var element = new Description();

                    element.Subelements = ImportElements(xmlElement.ChildNodes);

                    return element;
                }
                if (xmlElement.Name == "address")
                {
                    var element = new Address();

                    element.Subelements = ImportElements(xmlElement.ChildNodes);

                    return element;
                }
                if (xmlElement.Name == "page-break" || xmlElement.Name == "pb")
                {
                    var element = new PageBreak();

                    return element;
                }
            }
            else if (xmlNode is XmlText)
            {
                var xmlText = xmlNode as XmlText;

                var textElement = new TextElement();

                textElement.Text = xmlText.InnerText;

                return textElement;
            }

            return null;
        }
    }
}
