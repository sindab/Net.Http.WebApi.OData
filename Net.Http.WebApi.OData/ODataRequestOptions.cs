﻿// -----------------------------------------------------------------------
// <copyright file="ODataRequestOptions.cs" company="Project Contributors">
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
    using System;
    using System.Net.Http;

    /// <summary>
    /// Contains OData options for the request.
    /// </summary>
    public sealed class ODataRequestOptions
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ODataRequestOptions"/> class.
        /// </summary>
        /// <param name="request">The current http request message.</param>
        internal ODataRequestOptions(HttpRequestMessage request)
        {
            this.DataServiceUri = request.RequestUri.ResolveODataServiceUri();
            this.IsolationLevel = request.ReadIsolationLevel();
            this.MetadataLevel = request.ReadMetadataLevel();
        }

        /// <summary>
        /// Gets the root URI of the OData Service.
        /// </summary>
        public Uri DataServiceUri
        {
            get;
        }

        /// <summary>
        /// Gets the OData-Isolation requested by the client, or None if not otherwise specified.
        /// </summary>
        public ODataIsolationLevel IsolationLevel
        {
            get;
        }

        /// <summary>
        /// Gets the odata.metadata level specified in the ACCEPT header by the client, or Minimal if not otherwise specified.
        /// </summary>
        public ODataMetadataLevel MetadataLevel
        {
            get;
        }
    }
}