using System;
using System.Xml.Serialization;
using Elysium.Theme.WPF.ViewModels;

namespace Elysium.Platform.Entities
{
    [XmlRoot]
    public sealed class Application : ViewModelBase
    {
        [XmlAttribute]
        public string Page
        {
            get { return _page; }
            set
            {
                _page = value;
                OnPropertyChanged("Page");
            }
        }

        private string _page;

        [XmlAttribute]
        public bool IsAttachable
        {
            get { return _isAttachable; }
            set
            {
                _isAttachable = value;
                OnPropertyChanged("IsAttachable");
            }
        }

        private bool _isAttachable;

        [XmlAttribute]
        public bool IsAttached
        {
            get { return _isAttached; }
            set
            {
                _isAttached = value;
                OnPropertyChanged("IsAttached");
            }
        }

        private bool _isAttached;

        [XmlAttribute]
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }

        private bool _isVisible;
    }
} ;