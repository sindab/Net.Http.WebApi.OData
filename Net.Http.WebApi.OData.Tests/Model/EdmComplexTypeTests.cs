﻿namespace Net.Http.WebApi.OData.Tests.Model
{
    using System;
    using NorthwindModel;
    using OData.Model;
    using Xunit;

    public class EdmComplexTypeTests
    {
        [Fact]
        public void Constructor_SetsProperties()
        {
            var type = typeof(Customer);
            var properties = new EdmProperty[0];

            var edmComplexType = new EdmComplexType(type.FullName, type, properties);

            Assert.Same(type, edmComplexType.ClrType);
            Assert.Equal(type.FullName, edmComplexType.Name);
            Assert.Same(properties, properties);
        }

        [Fact]
        public void Constructor_ThrowsArgumentException_ForEmptyName()
        {
            var type = typeof(Customer);
            var properties = new EdmProperty[0];

            Assert.Throws<ArgumentException>(() => new EdmComplexType("", type, properties));
        }

        [Fact]
        public void Constructor_ThrowsArgumentException_ForNullName()
        {
            var type = typeof(Customer);
            var properties = new EdmProperty[0];

            Assert.Throws<ArgumentException>(() => new EdmComplexType(null, type, properties));
        }

        [Fact]
        public void Constructor_ThrowsArgumentNullException_ForNullProperties()
        {
            var type = typeof(Customer);

            Assert.Throws<ArgumentNullException>(() => new EdmComplexType(type.FullName, type, null));
        }

        [Fact]
        public void Constructor_ThrowsArgumentNullException_ForNullType()
        {
            var type = typeof(Customer);
            var properties = new EdmProperty[0];

            var exception = Assert.Throws<ArgumentNullException>(() => new EdmComplexType(type.FullName, null, properties));
        }

        [Fact]
        public void Equality_False_IfOtherNotEdmType()
        {
            var type = typeof(Customer);
            var properties = new EdmProperty[0];

            var edmComplexType1 = new EdmComplexType(type.FullName, type, properties);

            Assert.False(edmComplexType1.Equals("Customer"));
        }

        [Fact]
        public void Equality_False_IfOtherNull()
        {
            var type = typeof(Customer);
            var properties = new EdmProperty[0];

            var edmComplexType1 = new EdmComplexType(type.FullName, type, properties);

            Assert.False(edmComplexType1.Equals(null));
        }

        [Fact]
        public void Equality_True_IfNameAndTypeAreSame()
        {
            var type = typeof(Customer);
            var properties = new EdmProperty[0];

            var edmComplexType1 = new EdmComplexType(type.FullName, type, properties);
            var edmComplexType2 = new EdmComplexType(type.FullName, type, properties);

            Assert.True(edmComplexType1.Equals(edmComplexType2));
        }

        [Fact]
        public void Equality_True_IfReferenceIsSame()
        {
            var type = typeof(Customer);
            var properties = new EdmProperty[0];

            var edmComplexType1 = new EdmComplexType(type.FullName, type, properties);
            var edmComplexType2 = edmComplexType1;

            Assert.True(edmComplexType1.Equals(edmComplexType2));
        }

        [Fact]
        public void GetProperty_ReturnsProperty()
        {
            TestHelper.EnsureEDM();

            var edmComplexType = EntityDataModel.Current.Collections["Customers"];

            var edmProperty = edmComplexType.GetProperty("CompanyName");

            Assert.NotNull(edmProperty);
            Assert.Equal("CompanyName", edmProperty.Name);
        }

        [Fact]
        public void GetProperty_ThrowsArgumentExceptionIfPropertyNameNotFound()
        {
            TestHelper.EnsureEDM();

            var edmComplexType = EntityDataModel.Current.Collections["Customers"];

            var exception = Assert.Throws<ArgumentException>(() => edmComplexType.GetProperty("Name"));

            Assert.Equal("The type 'NorthwindModel.Customer' does not contain a property named 'Name'", exception.Message);
        }

        [Fact]
        public void ToString_ReturnsName()
        {
            var type = typeof(Customer);
            var properties = new EdmProperty[0];

            var edmComplexType = new EdmComplexType(type.FullName, type, properties);

            Assert.Equal(edmComplexType.ToString(), edmComplexType.Name);
        }
    }
}