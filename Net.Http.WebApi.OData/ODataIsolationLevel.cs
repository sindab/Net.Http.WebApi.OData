﻿// -----------------------------------------------------------------------
// <copyright file="ODataIsolationLevel.cs" company="Project Contributors">
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
    /// <summary>
    /// The OData isolation levels.
    /// </summary>
    public enum ODataIsolationLevel
    {
        /// <summary>
        /// No isolation level is specified in the request.
        /// </summary>
        None = 0,

        /// <summary>
        /// Snapshot isolation level is specified in the request.
        /// </summary>
        Snapshot
    }
}