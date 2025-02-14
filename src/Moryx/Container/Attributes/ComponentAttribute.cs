// Copyright (c) 2023, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;

namespace Moryx.Container
{
    /// <summary>
    /// Registration attribute to decorate components of a module.
    /// </summary>
    public class ComponentAttribute : RegistrationAttribute
    {
        /// <summary>
        /// Constructor with life cycle
        /// </summary>
        /// <param name="lifeStyle">Life style of component</param>
        /// <param name="services">Implemented service</param>
        public ComponentAttribute(LifeCycle lifeStyle, params Type[] services) 
            : base(lifeStyle, services)
        {
        }

        /// <summary>
        /// Flag that this plugin shall not be intercepted
        /// </summary>
        public bool DontIntercept { get; set; }    
    }
}
