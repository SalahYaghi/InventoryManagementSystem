using Contract.Common.Behaviors;
using Contract.Common.Interfaces;
using Castle.Core.Logging;
using Domain.Identity.Users;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace InventoryManagement.Application.UnitTests.Behaviours
{
    public class PerformanceBehaviourTests
    {

        private readonly PerformanceBehaviour<TestRequest, TestRequest> 
            _performanceBehaviour;
        private readonly ILogger<TestRequest> _logger;
        private readonly IUser _user; 

        public PerformanceBehaviourTests() { 

            _user = NSubstitute.Substitute.For<IUser>();
            _logger = NSubstitute.Substitute.For<ILogger<TestRequest>>();
            _performanceBehaviour = new PerformanceBehaviour<TestRequest, TestRequest>(
                _logger, _user );

        }

        [Fact]
        public async Task Handle_WhenRequestTakesLessThan500Ms_ShouldNotLogWarning()
        {

         var request = new TestRequest { Title = "Test" };   
         var response = new TestRequest { Title = "Test", Result = "Success" };


            var result =  await _performanceBehaviour.Handle(request
          ,(_) => Task.FromResult(response) ,  CancellationToken.None);


            Assert.Equal(result, response);

            _logger.DidNotReceive().Log(
                LogLevel.Warning,
                Arg.Any<EventId>(),
                Arg.Any<object>(),
                Arg.Any<Exception>(),
                Arg.Any<Func<object, Exception?, string>>());

        }

        [Fact]
        public async Task Handle_WhenRequestTakesMoreThan500Ms_ShouldLogWarning()
        {
            var cancellationToken = CancellationToken.None;
            var request = new TestRequest { Title = "Test" };
            var response = new TestRequest { Title = "Test", Result = "Success" };


            var result = await _performanceBehaviour.Handle(request
          , async (_) => {
              await Task.Delay(600, cancellationToken); 
              return await Task.FromResult(response);
              }, cancellationToken);


            Assert.Equal(result, response);

            _logger.Received(1).Log(
                LogLevel.Warning , 
                Arg.Any<EventId>() ,
                Arg.Is<object>(o => o.ToString()!.Contains("Long Running Request"))    ,
                null ,
                Arg.Any<Func<object, Exception?, string>>());
        }

        [Fact]
        public async Task Handle_ShouldAlwaysReturnResponseFromNext()
        {
            // Arrange
            var request = new TestRequest { Title = "Test" };
            var expectedResponse = new TestRequest { Result = "Success" };
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await _performanceBehaviour.Handle(request, (_) => Task.FromResult(expectedResponse), cancellationToken);

            // Assert
            Assert.Equal(expectedResponse, result);
        }
     
        [Fact]
        public async Task Handle_WhenNextThrowsException_ShouldNotCatchException()
        {
            var request = new TestRequest { Title = "Test" };
            var cancellationToken = CancellationToken.None;
            var expectedException = new InvalidOperationException("Test exception");

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => _performanceBehaviour.Handle(request, (_) =>
                throw expectedException, cancellationToken));

            Assert.Equal(expectedException, exception);
        }

        // Don't forget to add more tests for edge cases, such as when the user is null or when the logger throws an exception.







        public class TestRequest {
        
            public string Title { get; set; } = string.Empty;
            public string Result { get; set; } = string.Empty;

        }
    }
}
