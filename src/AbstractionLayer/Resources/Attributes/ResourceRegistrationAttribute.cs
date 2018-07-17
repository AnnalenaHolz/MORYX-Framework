﻿using System;
using Marvin.Container;

namespace Marvin.AbstractionLayer.Resources
{
    /// <summary>
    /// Simplified plugin registration attribute for resources
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ResourceRegistrationAttribute : PluginAttribute
    {
        /// <summary>
        /// ReadOnly Name of component.
        /// For resources the name is the FullName of the type
        /// </summary>
        public new string Name => base.Name;

        /// <summary>
        /// Generic registration with lifecylce <see cref="LifeCycle.Transient"/>
        /// </summary>
        public ResourceRegistrationAttribute() 
            : base(LifeCycle.Transient, typeof(IResource))
        {
        }

        /// <summary>
        /// Constructor of custom type with lifecycle <see cref="LifeCycle.Singleton"/>
        /// </summary>
        public ResourceRegistrationAttribute(Type customRegistration)
            : base(LifeCycle.Singleton, typeof(IResource), customRegistration)
        {
        }
    }
}