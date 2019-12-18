using System;
using System.Collections.Generic;
using Logger.Api;
using Logger.Api.Controllers;
using Logger.Infrastructure;
using Microsoft.Owin.Hosting;
using Xunit;

namespace Logger.Tests
{
    public class TestFixture
    {
        private ApiHost _api;
        private IDisposable _host;
        public ILoggerConfiguration config { get; private set; }
        private readonly List<IController> _controllers;
        
        public TestFixture()
        {
            config = new LoggerConfiguration();
            
            _controllers = new List<IController>()
            {
                // Add any new controllers here for the test fixture
                new ScheduleController(config)
            };

            _api = new ApiHost(config, _controllers);

            _host = WebApp.Start(config.LoggerServiceEndpoint, app =>
            {
                _api.Configure(app);
                foreach (var controller in _controllers)
                {
                    controller.Configure(app);
                }
            });
        }

        public void Dispose()
        {
            _host.Dispose();
        }

        [CollectionDefinition("ControllerCollection")]
        public class ControllerCollection : ICollectionFixture<TestFixture>
        {
        }
    }
}
