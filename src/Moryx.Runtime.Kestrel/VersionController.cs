﻿// Copyright (c) 2021, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moryx.Communication.Endpoints;

namespace Moryx.Runtime.Kestrel
{
    [ApiController, Route("endpoints")]
    [Produces("application/json")]
    internal class VersionController : Controller
    {
        public EndpointCollector Collector { get; set; }

        [HttpGet]
        public Endpoint[] AllEndpoints()
        {
            return Collector.AllEndpoints;
        }

        [HttpGet("service/{service}")]
        public Endpoint[] FilteredEndpoints(string service)
        {
            return Collector.AllEndpoints.Where(e => e.Service == service).ToArray();
        }

        [Obsolete("Will be removed or returns array in the next major")]
        [HttpGet("endpoint/{endpoint}")]
        public Endpoint GetEndpointConfig(string endpoint)
        {
            return Collector.AllEndpoints.FirstOrDefault(e => e.Path == endpoint);
        }
    }
}
