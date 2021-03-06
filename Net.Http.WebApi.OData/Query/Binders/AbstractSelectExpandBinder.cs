﻿// -----------------------------------------------------------------------
// <copyright file="AbstractSelectExpandBinder.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Query.Binders
{
    using Model;

    /// <summary>
    /// A base class for binding the $select and $expand query options.
    /// </summary>
    public abstract class AbstractSelectExpandBinder
    {
        /// <summary>
        /// Binds the $select and $expand properties from the OData Query.
        /// </summary>
        /// <param name="selectExpandQueryOption">The select/expand query option.</param>
        public void Bind(SelectExpandQueryOption selectExpandQueryOption)
        {
            if (selectExpandQueryOption == null)
            {
                return;
            }

            for (int i = 0; i < selectExpandQueryOption.Properties.Count; i++)
            {
                var property = selectExpandQueryOption.Properties[i];

                this.Bind(property);
            }
        }

        /// <summary>
        /// Binds the specified <see cref="EdmProperty"/>.
        /// </summary>
        /// <param name="edmProperty">The <see cref="EdmProperty"/> to bind.</param>
        protected abstract void Bind(EdmProperty edmProperty);
    }
}