﻿using System;

namespace Marvin.AbstractionLayer.Drivers.Rfid
{
    /// <summary>
    /// Base class for rfid tag events
    /// </summary>
    public class RfidTag
    {
        /// <summary>
        /// The Epc/UII that was read
        /// </summary>
        public string Epc { get; set; }

        /// <summary>
        /// Simmilar to the Epc but unique
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// Identification of the antenna that reported the event. Only unique within a driver.
        /// </summary>
        public int AntennaId { get; set; }

        /// <summary>
        /// Signal strength during read
        /// </summary>
        public int SignalStrength { get; set; }

        /// <summary>
        /// The time when the tag was readed
        /// </summary>
        public DateTime TimeStamp { get; set; }
    }
}