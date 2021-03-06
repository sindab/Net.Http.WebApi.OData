﻿namespace Net.Http.WebApi.OData.Tests
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Hosting;
    using Net.Http.WebApi.OData.Model;
    using Xunit;

    public class HttpRequestMessageExtensionsTests
    {
        [Fact]
        public void ResolveODataContextUri_ReturnsNull_IfMetadataIsNone()
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=none");

            var contextUri = httpRequestMessage.ResolveODataContextUri();

            Assert.Null(contextUri);
        }

        [Fact]
        public void ResolveODataContextUri_ReturnsUri_IfMetadataIsFull()
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=full");

            var contextUri = httpRequestMessage.ResolveODataContextUri();

            Assert.Equal("http://services.odata.org/OData/$metadata", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataContextUri_ReturnsUri_IfMetadataIsMinimal()
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=minimal");

            var contextUri = httpRequestMessage.ResolveODataContextUri();

            Assert.Equal("http://services.odata.org/OData/$metadata", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySet_AndEntityKey_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products('Milk')"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=none");

            var contextUri = httpRequestMessage.ResolveODataContextUri(EntityDataModel.Current.EntitySets["Products"], "Milk");

            Assert.Null(contextUri);
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySet_AndEntityKey_ReturnsUri_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products('Milk')"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=full");

            var contextUri = httpRequestMessage.ResolveODataContextUri(EntityDataModel.Current.EntitySets["Products"], "Milk");

            Assert.Equal("http://services.odata.org/OData/$metadata#Products/$entity", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySet_AndEntityKey_ReturnsUri_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products('Milk')"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=minimal");

            var contextUri = httpRequestMessage.ResolveODataContextUri(EntityDataModel.Current.EntitySets["Products"], "Milk");

            Assert.Equal("http://services.odata.org/OData/$metadata#Products/$entity", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySet_AndIntEntityKey_AndProperty_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Orders(12345)/Name"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=none");

            var contextUri = httpRequestMessage.ResolveODataContextUri(EntityDataModel.Current.EntitySets["Orders"], 12345, "Name");

            Assert.Null(contextUri);
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySet_AndIntEntityKey_AndProperty_ReturnsUri_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Orders(12345)/Name"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=full");

            var contextUri = httpRequestMessage.ResolveODataContextUri(EntityDataModel.Current.EntitySets["Orders"], 12345, "Name");

            Assert.Equal("http://services.odata.org/OData/$metadata#Orders(12345)/Name", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySet_AndIntEntityKey_AndProperty_ReturnsUri_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Orders(12345)/Name"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=minimal");

            var contextUri = httpRequestMessage.ResolveODataContextUri(EntityDataModel.Current.EntitySets["Orders"], 12345, "Name");

            Assert.Equal("http://services.odata.org/OData/$metadata#Orders(12345)/Name", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySet_AndStringEntityKey_AndProperty_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products('Milk')/Name"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=none");

            var contextUri = httpRequestMessage.ResolveODataContextUri(EntityDataModel.Current.EntitySets["Products"], "Milk", "Name");

            Assert.Null(contextUri);
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySet_AndStringEntityKey_AndProperty_ReturnsUri_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products('Milk')/Name"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=full");

            var contextUri = httpRequestMessage.ResolveODataContextUri(EntityDataModel.Current.EntitySets["Products"], "Milk", "Name");

            Assert.Equal("http://services.odata.org/OData/$metadata#Products('Milk')/Name", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySet_AndStringEntityKey_AndProperty_ReturnsUri_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products('Milk')/Name"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=minimal");

            var contextUri = httpRequestMessage.ResolveODataContextUri(EntityDataModel.Current.EntitySets["Products"], "Milk", "Name");

            Assert.Equal("http://services.odata.org/OData/$metadata#Products('Milk')/Name", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySet_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products/"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=none");

            var contextUri = httpRequestMessage.ResolveODataContextUri(EntityDataModel.Current.EntitySets["Products"]);

            Assert.Null(contextUri);
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySet_ReturnsUri_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products/"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=full");

            var contextUri = httpRequestMessage.ResolveODataContextUri(EntityDataModel.Current.EntitySets["Products"]);

            Assert.Equal("http://services.odata.org/OData/$metadata#Products", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySet_ReturnsUri_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products/"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=minimal");

            var contextUri = httpRequestMessage.ResolveODataContextUri(EntityDataModel.Current.EntitySets["Products"]);

            Assert.Equal("http://services.odata.org/OData/$metadata#Products", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataEntityUri_WithIntEntityKey()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Orders"));

            var contextUri = httpRequestMessage.ResolveODataEntityUri(EntityDataModel.Current.EntitySets["Orders"], 12345);

            Assert.Equal("http://services.odata.org/OData/Orders(12345)", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataEntityUri_WithStringEntityKey()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products"));

            var contextUri = httpRequestMessage.ResolveODataEntityUri(EntityDataModel.Current.EntitySets["Products"], "Milk");

            Assert.Equal("http://services.odata.org/OData/Products('Milk')", contextUri.ToString());
        }

        public class CreateODataErrorResponse_WithHttpStatusCode_AndMessage
        {
            private readonly HttpResponseMessage httpResponseMessage;

            public CreateODataErrorResponse_WithHttpStatusCode_AndMessage()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products?$select=Foo"));
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                this.httpResponseMessage = httpRequestMessage.CreateODataErrorResponse(HttpStatusCode.BadRequest, "The type 'NorthwindModel.Product' does not contain a property named 'Foo'.");
            }

            [Fact]
            public void TheContentIsSet()
            {
                Assert.IsType<ObjectContent<ODataErrorContent>>(this.httpResponseMessage.Content);
                Assert.IsType<ODataErrorContent>(((ObjectContent<ODataErrorContent>)this.httpResponseMessage.Content).Value);

                var errorContent = (ODataErrorContent)((ObjectContent<ODataErrorContent>)this.httpResponseMessage.Content).Value;

                Assert.NotNull(errorContent.Error);
                Assert.Equal("", errorContent.Error.Code);
                Assert.Equal("The type 'NorthwindModel.Product' does not contain a property named 'Foo'.", errorContent.Error.Message);
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(this.httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal("4.0", this.httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }

            [Fact]
            public void TheStatusCodeIsBadRequest()
            {
                Assert.Equal(HttpStatusCode.BadRequest, this.httpResponseMessage.StatusCode);
            }
        }

        public class CreateODataErrorResponse_WithHttpStatusCode_Code_AndMessage
        {
            private readonly HttpResponseMessage httpResponseMessage;

            public CreateODataErrorResponse_WithHttpStatusCode_Code_AndMessage()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products?$select=Foo"));
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                this.httpResponseMessage = httpRequestMessage.CreateODataErrorResponse(HttpStatusCode.BadRequest, "400", "The type 'NorthwindModel.Product' does not contain a property named 'Foo'.");
            }

            [Fact]
            public void TheContentIsSet()
            {
                Assert.IsType<ObjectContent<ODataErrorContent>>(this.httpResponseMessage.Content);
                Assert.IsType<ODataErrorContent>(((ObjectContent<ODataErrorContent>)this.httpResponseMessage.Content).Value);

                var errorContent = (ODataErrorContent)((ObjectContent<ODataErrorContent>)this.httpResponseMessage.Content).Value;

                Assert.NotNull(errorContent.Error);
                Assert.Equal("400", errorContent.Error.Code);
                Assert.Equal("The type 'NorthwindModel.Product' does not contain a property named 'Foo'.", errorContent.Error.Message);
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(this.httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal("4.0", this.httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }

            [Fact]
            public void TheStatusCodeIsBadRequest()
            {
                Assert.Equal(HttpStatusCode.BadRequest, this.httpResponseMessage.StatusCode);
            }
        }

        public class CreateODataErrorResponse_WithODataException
        {
            private readonly HttpResponseMessage httpResponseMessage;

            public CreateODataErrorResponse_WithODataException()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products?$select=Foo"));
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                this.httpResponseMessage = httpRequestMessage.CreateODataErrorResponse(new ODataException(HttpStatusCode.BadRequest, "The type 'NorthwindModel.Product' does not contain a property named 'Foo'."));
            }

            [Fact]
            public void TheContentIsSet()
            {
                Assert.IsType<ObjectContent<ODataErrorContent>>(this.httpResponseMessage.Content);
                Assert.IsType<ODataErrorContent>(((ObjectContent<ODataErrorContent>)this.httpResponseMessage.Content).Value);

                var errorContent = (ODataErrorContent)((ObjectContent<ODataErrorContent>)this.httpResponseMessage.Content).Value;

                Assert.NotNull(errorContent.Error);
                Assert.Equal("BadRequest", errorContent.Error.Code);
                Assert.Equal("The type 'NorthwindModel.Product' does not contain a property named 'Foo'.", errorContent.Error.Message);
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(this.httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal("4.0", this.httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }

            [Fact]
            public void TheStatusCodeIsBadRequest()
            {
                Assert.Equal(HttpStatusCode.BadRequest, this.httpResponseMessage.StatusCode);
            }
        }

        public class CreateODataResponse_String_WithNonNullValue
        {
            private readonly HttpResponseMessage httpResponseMessage;

            public CreateODataResponse_String_WithNonNullValue()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products('Milk')/Code/$value"));

                this.httpResponseMessage = httpRequestMessage.CreateODataResponse("MLK");
            }

            [Fact]
            public void TheContentIsSet()
            {
                Assert.Equal("MLK", ((StringContent)this.httpResponseMessage.Content).ReadAsStringAsync().Result);
            }

            [Fact]
            public void TheContentIsStringContent()
            {
                Assert.IsType<StringContent>(this.httpResponseMessage.Content);
            }

            [Fact]
            public void TheContentTypeIsTextPlain()
            {
                Assert.Equal("text/plain", this.httpResponseMessage.Content.Headers.ContentType.MediaType);
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(this.httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal("4.0", this.httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }

            [Fact]
            public void TheMetadataLevelContentTypeParameterIsNotSet()
            {
                var metadataParameter = this.httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

                Assert.Null(metadataParameter);
            }

            [Fact]
            public void TheStatusCodeIsOk()
            {
                Assert.Equal(HttpStatusCode.OK, this.httpResponseMessage.StatusCode);
            }
        }

        public class CreateODataResponse_String_WithNullValue
        {
            private readonly HttpResponseMessage httpResponseMessage;

            public CreateODataResponse_String_WithNullValue()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products('Milk')/Code/$value"));

                this.httpResponseMessage = httpRequestMessage.CreateODataResponse(default(string));
            }

            [Fact]
            public void TheContentIsNull()
            {
                Assert.Null(this.httpResponseMessage.Content);
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(this.httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal("4.0", this.httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }

            [Fact]
            public void TheStatusCodeIsNoContent()
            {
                Assert.Equal(HttpStatusCode.NoContent, this.httpResponseMessage.StatusCode);
            }
        }

        public class CreateODataResponse_T_WithAcceptHeaderContainingODataMetadataFull
        {
            private readonly HttpResponseMessage httpResponseMessage;

            public CreateODataResponse_T_WithAcceptHeaderContainingODataMetadataFull()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products?$filter=Price eq 21.39M"));
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=full");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                this.httpResponseMessage = httpRequestMessage.CreateODataResponse(HttpStatusCode.OK, new ODataResponseContent(null, new object[0]));
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(this.httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal("4.0", this.httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }

            [Fact]
            public void TheMetadataLevelContentTypeParameterIsSet()
            {
                var metadataParameter = this.httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

                Assert.NotNull(metadataParameter);
                Assert.Equal("full", metadataParameter.Value);
            }
        }

        public class CreateODataResponse_T_WithAcceptHeaderContainingODataMetadataMinimal
        {
            private readonly HttpResponseMessage httpResponseMessage;

            public CreateODataResponse_T_WithAcceptHeaderContainingODataMetadataMinimal()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products?$filter=Price eq 21.39M"));
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=minimal");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                this.httpResponseMessage = httpRequestMessage.CreateODataResponse(HttpStatusCode.OK, new ODataResponseContent(null, new object[0]));
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(this.httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal("4.0", this.httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }

            [Fact]
            public void TheMetadataLevelContentTypeParameterIsSet()
            {
                var metadataParameter = this.httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

                Assert.NotNull(metadataParameter);
                Assert.Equal("minimal", metadataParameter.Value);
            }
        }

        public class CreateODataResponse_T_WithAcceptHeaderContainingODataMetadataNone
        {
            private readonly HttpResponseMessage httpResponseMessage;

            public CreateODataResponse_T_WithAcceptHeaderContainingODataMetadataNone()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products?$filter=Price eq 21.39M"));
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=none");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                this.httpResponseMessage = httpRequestMessage.CreateODataResponse(HttpStatusCode.OK, new ODataResponseContent(null, new object[0]));
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(this.httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal("4.0", this.httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }

            [Fact]
            public void TheMetadataLevelContentTypeParameterIsSet()
            {
                var metadataParameter = this.httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

                Assert.NotNull(metadataParameter);
                Assert.Equal("none", metadataParameter.Value);
            }
        }

        public class CreateODataResponse_T_WithoutMetadataLevelSpecifiedInRequest
        {
            private readonly HttpResponseMessage httpResponseMessage;

            public CreateODataResponse_T_WithoutMetadataLevelSpecifiedInRequest()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products?$filter=Price eq 21.39M"));
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                this.httpResponseMessage = httpRequestMessage.CreateODataResponse(HttpStatusCode.OK, new ODataResponseContent(null, new object[0]));
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(this.httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal("4.0", this.httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }

            [Fact]
            public void TheMetadataLevelContentTypeParameterIsSet()
            {
                var metadataParameter = this.httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

                Assert.NotNull(metadataParameter);
                Assert.Equal("minimal", metadataParameter.Value);
            }
        }

        public class ReadODataRequestOptions
        {
            [Fact]
            public void CachesInRequestProperties()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products"));
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=minimal");

                var requestOptions1 = httpRequestMessage.ReadODataRequestOptions();
                var requestOptions2 = httpRequestMessage.ReadODataRequestOptions();

                Assert.Same(requestOptions1, requestOptions2);

                Assert.Same(requestOptions1, httpRequestMessage.Properties[typeof(ODataRequestOptions).FullName]);
            }
        }

        public class ReadODataRequestOptions_WithAcceptHeaderContaininAnInvalidODataMetadataValue
        {
            [Fact]
            public void AnHttpResponseExceptionIsThrown()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products"));
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=all");

                var exception = Assert.Throws<HttpResponseException>(() => httpRequestMessage.ReadODataRequestOptions());

                Assert.Equal(HttpStatusCode.BadRequest, exception.Response.StatusCode);
                Assert.Equal(Messages.ODataMetadataValueInvalid, ((HttpError)((ObjectContent<HttpError>)exception.Response.Content).Value).Message);
            }
        }

        public class ReadODataRequestOptions_WithAcceptHeaderContainingODataMetadataFull
        {
            [Fact]
            public void TheMetadataLevelIsSetToVerbose()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products"));
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=full");

                var requestOptions = httpRequestMessage.ReadODataRequestOptions();

                Assert.Equal(ODataMetadataLevel.Full, requestOptions.MetadataLevel);
            }
        }

        public class ReadODataRequestOptions_WithAcceptHeaderContainingODataMetadataMinimal
        {
            [Fact]
            public void TheMetadataLevelIsSetToMinimal()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products"));
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=minimal");

                var requestOptions = httpRequestMessage.ReadODataRequestOptions();

                Assert.Equal(ODataMetadataLevel.Minimal, requestOptions.MetadataLevel);
            }
        }

        public class ReadODataRequestOptions_WithAcceptHeaderContainingODataMetadataNone
        {
            [Fact]
            public void TheMetadataLevelIsSetToNone()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products"));
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=none");

                var requestOptions = httpRequestMessage.ReadODataRequestOptions();

                Assert.Equal(ODataMetadataLevel.None, requestOptions.MetadataLevel);
            }
        }

        public class ReadODataRequestOptions_WithODataIsolationHeaderContainingSnapshot
        {
            [Fact]
            public void TheIsolationLevelIsSet()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products"));
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataIsolation, "Snapshot");

                var requestOptions = httpRequestMessage.ReadODataRequestOptions();

                Assert.Equal(ODataIsolationLevel.Snapshot, requestOptions.IsolationLevel);
            }
        }

        public class ReadODataRequestOptions_WithODataIsolationHeaderNotContainingSnapshot
        {
            [Fact]
            public void AnHttpResponseExceptionIsThrownWhenReadingTheIsolationLevel()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products"));
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataIsolation, "ReadCommitted");

                var exception = Assert.Throws<HttpResponseException>(() => httpRequestMessage.ReadODataRequestOptions());

                Assert.Equal(HttpStatusCode.BadRequest, exception.Response.StatusCode);
                Assert.Equal(Messages.UnsupportedIsolationLevel, ((HttpError)((ObjectContent<HttpError>)exception.Response.Content).Value).Message);
            }
        }
    }
}