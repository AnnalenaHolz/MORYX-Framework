﻿using System;
using Marvin.AbstractionLayer.Resources;

namespace Marvin.AbstractionLayer.Drivers
{
    /// <summary>
    /// Interface for all device drivers
    /// </summary>
    public interface IDriver : IResource
    {
        /// <summary>
        /// Current state of the device
        /// </summary>
        IDriverState CurrentState { get; }

        /// <summary>
        /// Event raised when the device state changed
        /// </summary>
        event EventHandler<IDriverState> StateChanged;
    }
}