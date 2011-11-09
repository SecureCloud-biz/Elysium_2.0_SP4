using System;
using System.Xml.Serialization;

namespace Elysium.Platform.Models
{
    [XmlRoot]
    internal sealed class Application
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
        internal bool IsAttached { get; set; }

        [XmlAttribute]
        internal bool IsVisible { get; set; }
    }
} ;