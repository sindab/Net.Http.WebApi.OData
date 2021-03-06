﻿// -----------------------------------------------------------------------
// <copyright file="EntityDataModel.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// A class which represents the Entity Data Model.
    /// </summary>
    public sealed class EntityDataModel
    {
        internal EntityDataModel(IReadOnlyDictionary<string, EntitySet> entitySets)
        {
            this.EntitySets = entitySets;
        }

        /// <summary>
        /// Gets the current Entity Data Model.
        /// </summary>
        /// <remarks>
        /// Will be null until <see cref="EntityDataModelBuilder" />.BuildModel() has been called.
        /// </remarks>
        public static EntityDataModel Current
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the Entity Sets defined in the Entity Data Model.
        /// </summary>
        public IReadOnlyDictionary<string, EntitySet> EntitySets
        {
            get;
        }
    }
}