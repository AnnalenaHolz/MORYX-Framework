// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

namespace Moryx.Workplans
{
    /// <summary>
    /// Context the workplan is executed on
    /// </summary>
    public interface IWorkplanContext
    {
        /// <summary>
        /// Check if a step was disabled
        /// </summary>
        bool IsDisabled(IWorkplanStep step);
    }
}
