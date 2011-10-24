using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using Elysium.Core.Plugins;

namespace Elysium.Core
{
    public class Composer
    {
        public static readonly Composer Instance = new Composer();

        private readonly CompositionContainer _container;

        private readonly AggregateCatalog _catalog;

        public Composer()
        {
            _catalog = new AggregateCatalog();
            _container = new CompositionContainer(_catalog);
            _container.ComposeParts(this);
        }

        public void RegisterAssembly(Assembly assembly)
        {
            _catalog.Catalogs.Add(new AssemblyCatalog(assembly));
        }

        public delegate void CollectionChangedEventHandler(IEnumerable deletedValues, IEnumerable addedValues);

        [ImportMany(typeof(IGadget), AllowRecomposition = true)]
        public IEnumerable<IGadget> Gadgets
        {
            get { return _gadgets; }
            set
            {
                var deletedValues = _gadgets.Intersect(_gadgets.Intersect(value));
                var addedValues = value.Intersect(_gadgets.Intersect(value));
                _gadgets = value;
                if (GadgetsRecomposed != null)
                    GadgetsRecomposed(deletedValues, addedValues);
            }
        }

        private IEnumerable<IGadget> _gadgets;

        public event CollectionChangedEventHandler GadgetsRecomposed;

        [ImportMany(typeof(IEmbeddedApplication), AllowRecomposition = true)]
        public IEnumerable<IEmbeddedApplication> EmbeddedApplications
        {
            get { return _embeddedApplications; }
            set
            {
                var deletedValues = _embeddedApplications.Intersect(_embeddedApplications.Intersect(value));
                var addedValues = value.Intersect(_embeddedApplications.Intersect(value));
                _embeddedApplications = value;
                if (EmbeddedApplicationsRecomposed != null)
                    EmbeddedApplicationsRecomposed(deletedValues, addedValues);
            }
        }

        private IEnumerable<IEmbeddedApplication> _embeddedApplications;

        public event CollectionChangedEventHandler EmbeddedApplicationsRecomposed;

        [ImportMany(typeof(IApplication), AllowRecomposition = true)]
        public IEnumerable<IApplication> Applications
        {
            get { return _applications; }
            set
            {
                var deletedValues = _applications.Intersect(_applications.Intersect(value));
                var addedValues = value.Intersect(_applications.Intersect(value));
                _applications = value;
                if (AplicationsRecomposed != null)
                    AplicationsRecomposed(deletedValues, addedValues);
            }
        }

        private IEnumerable<IApplication> _applications;

        public event CollectionChangedEventHandler AplicationsRecomposed;


    }
} ;