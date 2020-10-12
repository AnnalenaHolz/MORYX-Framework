// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.ComponentModel;
using System.Runtime.Serialization;
using Moryx.AbstractionLayer.Resources;
using Moryx.Serialization;

namespace Moryx.Resources.Samples
{
    [ResourceRegistration]
    public class Machine : Resource
    {
        [DataMember, EntrySerialize]
        public string CurrentState { get; set; }
    }
}
