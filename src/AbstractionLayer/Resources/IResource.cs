// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

namespace Marvin.AbstractionLayer.Resources
{
    /// <summary>
    /// Basic interface of a Resource.
    /// </summary>
    public interface IResource
    {
        /// <summary>
        /// Id of the resource
        /// </summary>
        long Id { get; }

        /// <summary>
        /// Name of this resource instance
        /// </summary>
        string Name { get; }
    }
}
