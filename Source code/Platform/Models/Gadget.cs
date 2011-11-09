using System;
using System.Xml;
using System.Xml.Serialization;

namespace Elysium.Platform.Models
{
    [XmlRoot]
    internal sealed class Gadget
    {
        [XmlAttribute]
        internal Guid ID { get; set; }

        [XmlAttribute]
        internal string Assembly { get; set; }

        [XmlAttribute]
        internal string Type { get; set; }

        [XmlAttribute]
        internal string Page { get; set; }

        [XmlAttribute]
        internal string Group { get; set; }

        [XmlAttribute]
        internal int Column { get; set; }

        [XmlAttribute]
        internal int ColumnSpan { get; set; }

        [XmlAttribute]
        internal int Row { get; set; }
        
        [XmlAttribute]
        internal bool IsExpanded { get; set; }

        [XmlAttribute]
        internal bool IsVisible { get; set; }
    }
} ;