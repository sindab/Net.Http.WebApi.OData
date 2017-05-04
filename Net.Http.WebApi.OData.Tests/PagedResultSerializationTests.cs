namespace Net.Http.WebApi.OData.Tests
{
    using System.Dynamic;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Xunit;

    public class PagedResultSerializationTests
    {
        [Fact]
        public void JsonSerializationWithClassContent()
        {
            var item = new Thing();
            item.Name = "Coffee";
            item.Total = 2.55M;

            var pagedResult = new PagedResult<Thing>(new[] { item }, count: 12);

            var jsonResult = JsonConvert.SerializeObject(pagedResult);

            Assert.Equal("{\"@odata.count\":12,\"value\":[{\"Name\":\"Coffee\",\"Total\":2.55}]}", jsonResult);
        }

        [Fact]
        public void JsonSerializationWithDynamicContent()
        {
            dynamic item = new ExpandoObject();
            item.Id = 14225;
            item.Name = "Fred";

            var pagedResult = new PagedResult<dynamic>(new[] { item }, count: 12);

            var jsonResult = JsonConvert.SerializeObject(pagedResult);

            Assert.Equal("{\"@odata.count\":12,\"value\":[{\"Id\":14225,\"Name\":\"Fred\"}]}", jsonResult);
        }

        [Fact]
        public void JsonSerializationWithSimpleContent()
        {
            var pagedResult = new PagedResult<int>(new[] { 1, 2, 3 }, count: 5);

            var jsonResult = JsonConvert.SerializeObject(pagedResult);

            Assert.Equal("{\"@odata.count\":5,\"value\":[1,2,3]}", jsonResult);
        }

        [DataContract]
        public class Thing
        {
            [DataMember]
            public string Name
            {
                get;
                set;
            }

            [DataMember]
            public decimal Total
            {
                get;
                set;
            }
        }
    }
}