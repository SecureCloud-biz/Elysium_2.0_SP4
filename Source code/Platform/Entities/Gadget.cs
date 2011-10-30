using System;
using System.Diagnostics.Contracts;
using System.Xml.Serialization;
using Elysium.Platform.Properties;
using Elysium.Theme.WPF.ViewModels;

namespace Elysium.Platform.Entities
{
    [XmlRoot]
    public sealed class Gadget : ViewModelBase
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
        public string Group
        {
            get { return _group; }
            set
            {
                _group = value;
                OnPropertyChanged("Group");
            }
        }

        private string _group;

        [XmlAttribute]
        public int Column
        {
            get { return _column; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(GadgetErrors.ColumnValueMustBeGreaterThanOrEqualToZero, "value");
                }
                Contract.EndContractBlock();

                _column = value;
                OnPropertyChanged("Column");
            }
        }

        private int _column;

        [XmlAttribute]
        public int ColumnSpan
        {
            get { return _columnSpan; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(GadgetErrors.ColumnSpanValueMustBeGreaterThanZero, "value");
                }
                if (value > 2)
                {
                    throw new ArgumentException(GadgetErrors.ColumnSpanValueMustBeLessThanOrEqualToTwo, "value");
                }
                Contract.EndContractBlock();

                _columnSpan = value;
                OnPropertyChanged("ColumnSpan");
            }
        }

        private int _columnSpan;

        [XmlAttribute]
        public int Row
        {
            get { return _row; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(GadgetErrors.RowValueMustBeGreaterThanOrEqualToZero, "value");
                }
                Contract.EndContractBlock();

                _row = value;
                OnPropertyChanged("Row");
            }
        }

        private int _row;

        [XmlAttribute]
        public bool IsExpandable
        {
            get { return _isExpandable; }
            set
            {
                _isExpandable = value;
                OnPropertyChanged("IsExpandable");
            }
        }

        private bool _isExpandable;

        [XmlAttribute]
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                OnPropertyChanged("IsExpanded");
            }
        }

        private bool _isExpanded;

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