using System;
using System.Net;
using System.Net.Http;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Logger.Tests.ControllerTests
{
    [Collection("ControllerCollection")]
    public class ScheduleTests
    {
        private readonly ITestOutputHelper _output;
        private TestFixture _fixture;

        public ScheduleTests(TestFixture fixture, ITestOutputHelper output)
        {
            this._fixture = fixture;
            this._output = output;
        }


        [Fact]
        public void When_calling_schedule_output_result_200()
        {
            var url = string.Format("api/");
            var client = new HttpClient
            {
                BaseAddress = new Uri(_fixture.config.LoggerServiceEndpoint)
            };
            
            var response = client.GetAsync(url).Result;

            var body = response.Content.ReadAsStringAsync().Result;
            _output.WriteLine(body);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}
