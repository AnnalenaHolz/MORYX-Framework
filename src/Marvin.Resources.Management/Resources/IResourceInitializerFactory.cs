﻿using Marvin.AbstractionLayer.Resources;
using Marvin.Container;

namespace Marvin.Resources.Management
{
    /// <summary>
    /// Factory to create <see cref="IResourceInitializer"/>
    /// </summary>
    [PluginFactory(typeof(IConfigBasedComponentSelector))]
    internal interface IResourceInitializerFactory
    {
        /// <summary>
        /// Creates an <see cref="IResourceInitializer"/> with the given config
        /// </summary>
        IResourceInitializer Create(ResourceInitializerConfig config);
    }
}