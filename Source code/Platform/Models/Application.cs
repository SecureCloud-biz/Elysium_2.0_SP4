using System;
using System.Xml.Serialization;

namespace Elysium.Platform.Models
{
    [XmlRoot]
    public sealed class Application
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
        public bool IsAttached { get; set; }

        [XmlAttribute]
        public bool IsVisible { get; set; }
    }
} ;