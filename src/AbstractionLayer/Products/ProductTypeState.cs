﻿namespace Marvin.AbstractionLayer
{
    /// <summary>
    /// Current state of a product
    /// </summary>
    public enum ProductTypeState
    {
        /// <summary>
        /// Object was created, but not reviewed or released for production
        /// </summary>
        Created,

        /// <summary>
        /// Product is released and may be manufactured
        /// </summary>
        Released,

        /// <summary>
        /// Product refers to an old version, that must no longer be manufactured
        /// </summary>
        Deprecated,
    }
}