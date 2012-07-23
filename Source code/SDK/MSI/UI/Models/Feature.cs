namespace Elysium.SDK.MSI.UI.Models
{
    public struct Feature
    {
        private readonly string _name;
        private readonly bool _allowAbsent;

        public Feature(string name, bool allowAbsent)
        {
            _name = name;
            _allowAbsent = allowAbsent;
        }

        public string Name
        {
            get { return _name; }
        }

        public bool AllowAbsent
        {
            get { return _allowAbsent; }
        }
    }
} ;