using Contract.Common.Behaviors;
using Contract.Common.Interfaces;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace InventoryManagement.Application.UnitTests.Behaviours
{
    public class LoggingBehaviourTests
    {

        private readonly ILogger<DummyRequest> _logger = Substitute.For<ILogger<DummyRequest>>();
        private readonly IUser _user = Substitute.For<IUser>();
      
        private readonly LoggingProcessor<DummyRequest> _sut;

        public LoggingBehaviourTests()
        {
            _sut = new LoggingProcessor<DummyRequest>(_logger, _user);
        }

        [Fact]
        public async Task Process_WithUserId_LogsRequestWithUserName()
        {
            
            var request = new DummyRequest();
            _user.UserId.Returns(Guid.NewGuid());
            _user.UserName.Returns("dummy_name");

            await _sut.Process(request, CancellationToken.None);


            _logger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString()!.Contains("Request")),
                Arg.Any<Exception>(),
                Arg.Any<Func<object, Exception?, string>>());
        }

        public class DummyRequest;



    }
}
