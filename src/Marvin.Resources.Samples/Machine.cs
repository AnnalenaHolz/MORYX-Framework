﻿using System.Runtime.Serialization;
using Marvin.AbstractionLayer.Resources;
using Marvin.Serialization;

namespace Marvin.Resources.Samples
{
    [ResourceRegistration(nameof(Machine))]
    public class Machine : Resource
    {
        [DataMember, EditorVisible]
        public string CurrentState { get; set; }
    }
}