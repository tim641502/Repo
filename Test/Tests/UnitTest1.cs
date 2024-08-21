using System.Net;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using WessexWaterAssessment.Server.Controllers;

namespace Test.Tests
{
    public class UnitTest1
    {
        // Mock the httpClient used by ToDoClient and return a ToDoController
        private ToDoController GetController(HttpStatusCode code, string httpClientResponse)
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = code,
                    Content = new StringContent(httpClientResponse)
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/ToDos");

            var client = new ToDoClient(httpClient);
            var repo = new ToDoRepository(client);
            return new ToDoController(new Mock<ILogger<ToDoController>>().Object, repo);
        }

        [Fact]
        public async Task ToDoSummary_SucceedsAsync()
        {
            // sample data that contains three toDos- 1 complete, 2 uncomplete
            string filePath = "../../../Data/SampleGetAllToDos.json";
            string allToDos = File.ReadAllText(filePath);

            var response = await GetController(HttpStatusCode.OK, allToDos).GetAsync();

            Assert.True(response.Success);

            var data = response.Data;

            Assert.Equal(3, data.TotalTodos);

            Assert.Equal(1, data.CompletedTodos);

            Assert.Equal(2, data.UncompletedTodos);
        }

        [Fact]
        public async Task ToDoSummary_FailsAsync()
        {
            // mock up a BadRequest result from the httpClient and ensure error is handled correctly
            var response = await GetController(HttpStatusCode.BadRequest, "").GetAsync();

            Assert.True(!response.Success);

            Assert.NotNull(response.Message);
        }
    }
}