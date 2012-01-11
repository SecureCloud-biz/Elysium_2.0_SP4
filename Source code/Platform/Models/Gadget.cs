using System;
using System.Xml;
using System.Xml.Serialization;

namespace Elysium.Platform.Models
{
    [XmlRoot]
    public sealed class Gadget
    {
        [XmlAttribute]
        public Guid ID { get; set; }

        [XmlAttribute]
        public string Assembly { get; set; }

        [XmlAttribute]
        public string Type { get; set; }

        [XmlAttribute]
        public string Page { get; set; }

        [XmlAttribute]
        public string Group { get; set; }

        [XmlAttribute]
        public int Column { get; set; }

        [XmlAttribute]
        public int ColumnSpan { get; set; }

        [XmlAttribute]
        public int Row { get; set; }
        
        [XmlAttribute]
        public bool IsExpanded { get; set; }

        [XmlAttribute]
        public bool IsVisible { get; set; }
    }
} ;