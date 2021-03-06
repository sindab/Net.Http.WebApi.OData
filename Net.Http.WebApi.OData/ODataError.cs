﻿// -----------------------------------------------------------------------
// <copyright file="ODataError.cs" company="Project Contributors">
// Copyright 2012 - 2017 Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace Net.Http.WebApi.OData
{
    using System.Runtime.Serialization;

    [DataContract]
    internal sealed class ODataError
    {
        [Newtonsoft.Json.JsonProperty("code", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore, Order = 0)]
        public string Code
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("message", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore, Order = 1)]
        public string Message
        {
            get;
            set;
        }
    }
}