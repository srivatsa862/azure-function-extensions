using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace SasS.Azure.Functions.Extensions.Tests
{
    public class HttpRequestExtensionsTests
    {
        [Fact]
        public async Task ReadBodyAsStringAsync_Empty_Body_Test()
        {
            //Arrange
            var httpRequest = CreateMockRequest(null).Object;

            //Act
            var result = await HttpRequestExtensions.ReadBodyAsStringAsync(httpRequest);

            //Assert
            Assert.True(string.IsNullOrEmpty(result));
        }

        private static Mock<HttpRequest> CreateMockRequest(object body)
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);

            sw.Write(body);
            sw.Flush();

            ms.Position = 0;

            var mockRequest = new Mock<HttpRequest>();
            mockRequest.Setup(x => x.Body).Returns(ms);

            return mockRequest;
        }

        private static Mock<HttpRequest> CreateMockJsonRequest(object body)
        {
            return CreateMockRequest(JsonConvert.SerializeObject(body));
        }
    }
}
