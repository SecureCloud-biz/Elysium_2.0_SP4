using System;
using System.Collections.Generic;

namespace Elysium.Platform.ViewModels
{
    internal static class Locator
    {
        internal static Main Main
        {
            get { return _main ?? (_main = new Main()); }
        }

        private static Main _main;

        internal static Collections.ObservableDictionary<Guid, Gadget> Gadgets
        {
            get
            {
                if (_gadgets == null)
                {
                    _gadgets = new Collections.ObservableDictionary<Guid, Gadget>();
                    foreach (var item in Settings.Default.Gadgets)
                    {
                        _gadgets.Add(item.Key, new Gadget(item.Value));
                    }
                    Settings.Default.Gadgets.CollectionChanged +=
                        (s, e) =>
                            {
                                foreach (KeyValuePair<Guid, Models.Gadget> item in e.OldItems)
                                {
                                    _gadgets.Remove(item.Key);
                                }
                                foreach (KeyValuePair<Guid, Models.Gadget> item in e.NewItems)
                                {
                                    _gadgets.Add(item.Key, new Gadget(item.Value));
                                }
                            };
                }
                return _gadgets;
            }
        }

        private static Collections.ObservableDictionary<Guid, Gadget> _gadgets;

        internal static Collections.ObservableDictionary<Guid, Application> Applications
        {
            get
            {
                if (_applications == null)
                {
                    _applications = new Collections.ObservableDictionary<Guid, Application>();
                    foreach (var item in Settings.Default.Applications)
                    {
                        _applications.Add(item.Key, new Application(item.Value));
                    }
                    Settings.Default.Applications.CollectionChanged +=
                        (s, e) =>
                            {
                                foreach (KeyValuePair<Guid, Models.Application> item in e.OldItems)
                                {
                                    _applications.Remove(item.Key);
                                }
                                foreach (KeyValuePair<Guid, Models.Application> item in e.NewItems)
                                {
                                    _applications.Add(item.Key, new Application(item.Value));
                                }
                            };
                }
                return _applications;
            }
        }

        private static Collections.ObservableDictionary<Guid, Application> _applications;
    }
} ;