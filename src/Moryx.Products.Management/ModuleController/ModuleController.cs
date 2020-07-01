// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.AbstractionLayer;
using Moryx.AbstractionLayer.Products;
using Moryx.Container;
using Moryx.Model;
using Moryx.Products.Management.Importers;
using Moryx.Products.Management.Modification;
using Moryx.Products.Model;
using Moryx.Runtime.Container;
using Moryx.Runtime.Modules;
using Moryx.Tools.Wcf;

namespace Moryx.Products.Management
{
    /// <summary>
    /// The main controller of all product modules.
    /// </summary>
    [ServerModule(ModuleName)]
    public class ModuleController : ServerModuleFacadeControllerBase<ModuleConfig>, IFacadeContainer<IProductManagement>
    {
        internal const string ModuleName = "ProductManager";
        /// <summary>
        /// Name of this module
        /// </summary>
        public override string Name => ModuleName;

        /// <summary>
        /// Generic component to access every data model
        /// </summary>
        [Named(ProductsConstants.Namespace)]
        public IUnitOfWorkFactory ProductsModel { get; set; }

        private IConfiguredServiceHost _host;

        #region State transition

        /// <summary>
        /// Code executed on start up and after service was stopped and should be started again
        /// </summary>
        protected override void OnInitialize()
        {
            Container.SetInstance(ProductsModel, ProductsConstants.Namespace);
            Container.SetInstance(ConfigManager);

            // Load all product plugins
            Container.LoadComponents<IProductStorage>();
            Container.LoadComponents<IProductImporter>();

            // Load strategies
            Container.LoadComponents<IProductTypeStrategy>();
            Container.LoadComponents<IProductInstanceStrategy>();
            Container.LoadComponents<IProductLinkStrategy>();
            Container.LoadComponents<IProductRecipeStrategy>();
            Container.LoadComponents<IPropertyMapper>();
        }

        /// <summary>
        /// Code executed after OnInitialize
        /// </summary>
        protected override void OnStart()
        {
            // Start Manager
            Container.Resolve<IProductStorage>().Start();
            Container.Resolve<IProductManager>().Start();

            // Start all plugins
            _host = Container.Resolve<IConfiguredHostFactory>().CreateHost<IProductInteraction>(Config.InteractionHost);
            _host.Start();

            // Activate facades
            ActivateFacade(_productManagement);
        }

        /// <summary>
        /// Code executed when service is stopped
        /// </summary>
        protected override void OnStop()
        {
            // Deactivate facades
            DeactivateFacade(_productManagement);

            _host.Stop();
            _host = null;
        }
        #endregion

        #region FacadeContainer
        private readonly ProductManagementFacade _productManagement = new ProductManagementFacade();
        IProductManagement IFacadeContainer<IProductManagement>.Facade
        {
            get { return _productManagement; }
        }

        #endregion
    }

}
