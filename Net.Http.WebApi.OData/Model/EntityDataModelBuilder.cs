﻿// -----------------------------------------------------------------------
// <copyright file="EntityDataModelBuilder.cs" company="Project Contributors">
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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// A class which builds the <see cref="EntityDataModel"/>.
    /// </summary>
    public sealed class EntityDataModelBuilder
    {
        private readonly Dictionary<string, EdmComplexType> entitySets;

        /// <summary>
        /// Initialises a new instance of the <see cref="EntityDataModelBuilder"/> class.
        /// </summary>
        /// <remarks>Uses <see cref="StringComparer"/>.OrdinalIgnoreCase for the entity set name comparer.</remarks>
        public EntityDataModelBuilder()
            : this(StringComparer.OrdinalIgnoreCase)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="EntityDataModelBuilder"/> class.
        /// </summary>
        /// <param name="entitySetNameComparer">The equality comparer to use for the entity set name.</param>
        public EntityDataModelBuilder(IEqualityComparer<string> entitySetNameComparer)
        {
            this.entitySets = new Dictionary<string, EdmComplexType>(entitySetNameComparer);
        }

        /// <summary>
        /// Builds the Entity Data Model containing the collections registered.
        /// </summary>
        /// <returns>The Entity Data Model.</returns>
        public EntityDataModel BuildModel()
        {
            EntityDataModel.Current = new EntityDataModel(this.entitySets);

            return EntityDataModel.Current;
        }

        /// <summary>
        /// Registers an Entity Set of the specified type to the Entity Data Model with the specified name.
        /// </summary>
        /// <typeparam name="T">The type exposed by the collection.</typeparam>
        /// <param name="entitySetName">Name of the Entity Set.</param>
        /// <param name="entityKeyExpression">The entity key expression.</param>
        public void RegisterEntitySet<T>(string entitySetName, Expression<Func<T, object>> entityKeyExpression)
        {
            var edmType = (EdmComplexType)EdmTypeCache.Map.GetOrAdd(
                typeof(T),
                t => EdmTypeResolver(t, entityKeyExpression.GetMemberInfo()));

            this.entitySets.Add(entitySetName, edmType);
        }

        private static EdmType EdmTypeResolver(Type clrType, MemberInfo entityKeyMember)
        {
            if (clrType.IsEnum)
            {
                throw new NotSupportedException("Enums are not supported in OData 3.0");
            }

            var clrTypeProperties = clrType.GetProperties().OrderBy(p => p.Name);

            var edmProperties = new List<EdmProperty>();
            var edmComplexType = new EdmComplexType(
                clrType,
                entityKeyMember.Name,
                edmProperties);

            edmProperties.AddRange(
                clrTypeProperties.Select(
                    p => new EdmProperty(
                        p.Name,
                        EdmTypeCache.Map.GetOrAdd(p.PropertyType, t => EdmTypeResolver(t, null)),
                        edmComplexType)));

            return edmComplexType;
        }
    }
}