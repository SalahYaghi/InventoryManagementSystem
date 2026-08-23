using Contract.Common.Behaviors;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using static InventoryManagement.Application.UnitTests.Behaviours.LoggingBehaviourTests;
using Xunit;

namespace InventoryManagement.Application.UnitTests.Behaviours
{
    public class UnhandeledExceptionsBehaviourTests
    {

        private readonly ILogger<DummyRequest> _logger =
            Substitute.For<ILogger<DummyRequest>>();
        private readonly UnhandledExceptionBehaviour<DummyRequest, string> _sut;

        public UnhandeledExceptionsBehaviourTests()
        {
            _sut = new UnhandledExceptionBehaviour<DummyRequest, string>(_logger);
        }

        [Fact]
        public async Task Handle_WhenNoException_InvokesNextAndReturnsResult()
        {
            var request = new DummyRequest();
            var next = Substitute.For<RequestHandlerDelegate<string>>();

            next.Invoke(CancellationToken.None).Returns("Success");

           var result =  (await _sut.Handle(request, next, CancellationToken.None));

            result.Equals("Success");

        }

        [Fact]
        public async Task Handle_WhenExceptionThrown_LogsErrorAndRethrows() {

            var request = new DummyRequest();

            var next = Substitute.For<RequestHandlerDelegate<string>>();
            
            var exception = new Exception("Test exception");
            
            next.Invoke(CancellationToken.None).Throws(exception);

             var act = async () =>
                await _sut.Handle(request, next, CancellationToken.None);

             var thrown = await Assert.ThrowsAsync<Exception>(act);

            Assert.Equal(thrown,exception);

            _logger.Received(1).Log(
                LogLevel.Error,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString()!.Contains("Unhandled Exception")),
                exception,
                Arg.Any<Func<object, Exception?, string>>());


        }

    }    
}
