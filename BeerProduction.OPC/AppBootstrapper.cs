// Copyright (c) Converter Systems LLC. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Windows;
using Microsoft.Extensions.Logging;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;

namespace BeerProduction.OPC
{
    /// <summary>
    /// Runs a bootstrapping sequence that initializes the Prism services.
    /// </summary>
    public class AppBootstrapper : UnityBootstrapper, IDisposable
    {
        readonly ILoggerFactory loggerFactory;

        public AppBootstrapper(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            // Disposing the container will dispose all the shared component parts.
            if (this.Container != null)
            {
                this.Container.Dispose();
                this.Container = null;
            }
        }

        /// <inheritdoc/>
        protected override void ConfigureModuleCatalog()
        {
            ModuleCatalog catalog = (ModuleCatalog)this.ModuleCatalog;
            catalog.AddModule(typeof(OpcStart));
        }
    }
}