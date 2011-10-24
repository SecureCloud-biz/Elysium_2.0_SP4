using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using Elysium.Core.Plugins;
using Elysium.Core.Properties;

namespace Elysium.Core
{
    public class Composer
    {
        public static readonly Composer Instance = new Composer();

        #region Private members

        private readonly CompositionContainer _container;

        private readonly AggregateCatalog _catalog;

        #endregion

        #region Constructors

        public Composer()
        {
            _catalog = new AggregateCatalog();
            _container = new CompositionContainer(_catalog);
            _container.ComposeParts(this);
        }

        #endregion

        #region Assemblies registration and loading

        public enum RegistrationStatus
        {
            Registered,
            Unregistered
        }

        public delegate void AssemblyRegistrationEventHandler(Assembly assembly);

        public event AssemblyRegistrationEventHandler AssemblyRegistered;

        public event AssemblyRegistrationEventHandler AssemblyUnregistered;

        public delegate RegistrationStatus AssemblyStatusEventHandler(Assembly assembly);

        public event AssemblyStatusEventHandler AssemblyStatus;

        public void RegisterAssembly(Assembly assembly)
        {
            if (AssemblyStatus != null && AssemblyStatus(assembly) == RegistrationStatus.Registered)
                throw new ArgumentException(Resources.AssemblyAlreadyRegistered, "assembly");
            if (AssemblyRegistered != null)
                AssemblyRegistered(assembly);
        }

        public void UnregisterAssembly(Assembly assembly)
        {
            if (AssemblyStatus != null && AssemblyStatus(assembly) == RegistrationStatus.Unregistered)
                throw new ArgumentException(Resources.AssemblyNotRegistered, "assembly");
            if (AssemblyUnregistered != null)
                AssemblyUnregistered(assembly);
        }

        public void LoadAssembly(Assembly assembly)
        {
            _catalog.Catalogs.Add(new AssemblyCatalog(assembly));
        }

        public void UnloadAssembly(Assembly assembly)
        {
            _catalog.Catalogs.Remove(_catalog.Catalogs.OfType<AssemblyCatalog>().Where(x => x.Assembly.Equals(assembly)).First());
        }

        #endregion

        #region Public members

        public delegate void CollectionChangedEventHandler(object sender, IEnumerable deletedValues, IEnumerable addedValues);

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
                    GadgetsRecomposed(this, deletedValues, addedValues);
            }
        }

        private IEnumerable<IGadget> _gadgets = Enumerable.Empty<IGadget>();

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
                    EmbeddedApplicationsRecomposed(this, deletedValues, addedValues);
            }
        }

        private IEnumerable<IEmbeddedApplication> _embeddedApplications = Enumerable.Empty<IEmbeddedApplication>();

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
                    AplicationsRecomposed(this, deletedValues, addedValues);
            }
        }

        private IEnumerable<IApplication> _applications = Enumerable.Empty<IApplication>();

        public event CollectionChangedEventHandler AplicationsRecomposed;

        #endregion
    }
} ;