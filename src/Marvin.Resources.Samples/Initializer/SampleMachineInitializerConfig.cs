﻿using System.ComponentModel;
using Marvin.AbstractionLayer.Resources;

namespace Marvin.Resources.Samples.Initializer
{
    public class SampleMachineInitializerConfig : ResourceInitializerConfig
    {
        [ReadOnly(true)]
        public override string PluginName
        {
            get { return nameof(SampleMachineInitializer); }
            set { }
        }

        [DisplayName("Machine Name"), Description("Defines the name of the machine.")]
        [DefaultValue("Sample")]
        public string MachineName { get; set; }
    }
}