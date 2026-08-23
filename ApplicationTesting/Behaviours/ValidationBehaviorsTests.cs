using Contract.Common.Behaviors;
using Contract.Features.Transactions.Orders.Commands.CreateOrder;
using Contract.Features.Transactions.Orders.DTOs;
using Contract.Features.Transactions.Orders.Mappers;
using Castle.Core.Logging;
using FluentValidation;
using FluentValidation.Results;
using InventoryManagement.Tests.Common.Factories.Orders;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.Common;
using NSubstitute;

using Xunit;

namespace InventoryManagement.Application.UnitTests.Behaviours
{
    public class ValidationBehaviorsTests
    {

        private readonly ValidationBehavior<CreateOrderCommand, Result<OrderDto>> _validatoinBehvaiour;
        private readonly RequestHandlerDelegate<Result<OrderDto>> _requestHandlerDelegate;
        private readonly IValidator<CreateOrderCommand> _mocValidator;
        private readonly ILogger<ValidationBehavior<CreateOrderCommand, Result<OrderDto>>> _mocLogger;
         public ValidationBehaviorsTests() {

            _mocLogger = NSubstitute.Substitute.For<ILogger<ValidationBehavior<CreateOrderCommand, Result<OrderDto>>>>();
            _mocValidator = NSubstitute.Substitute.For<IValidator<CreateOrderCommand>>();  
            _requestHandlerDelegate  = NSubstitute.Substitute.For<RequestHandlerDelegate<Result<OrderDto>>>();
            _validatoinBehvaiour = new([_mocValidator], _mocLogger);
        }

        [Fact]
        public async Task Handle_Should_Return_Success_When_Validation_Passes()
        {
            var command = OrderFactory.CreateValidPurchaseOrderCommand();
            var orderDto = OrderFactory.CreatePurchase().ToDto();
             
            _mocValidator
    .ValidateAsync(Arg.Any<ValidationContext<CreateOrderCommand>>(), Arg.Any<CancellationToken>())
    .Returns(new ValidationResult());

            _requestHandlerDelegate
    .Invoke(Arg.Any<CancellationToken>())
    .Returns(Task.FromResult<Result<OrderDto>>(orderDto));
            var result = await _validatoinBehvaiour.Handle(command, 
                _requestHandlerDelegate, default);
            
            Assert.True(result.IsSuccess);
            Assert.Equal( orderDto , result.Value  );
        }

        [Fact]
        public async Task InvokeValidationBehavior_WhenValidatorResultIsNotValid_ShouldReturnListOfErrors()
        {
             var createWorkOrderCommand = OrderFactory.CreateValidPurchaseOrderCommand();

            List<ValidationFailure> validationFailures = [new(propertyName: 
                "property1", errorMessage: "property1 is invalid")];

            _mocValidator
      .ValidateAsync(Arg.Any<ValidationContext<CreateOrderCommand>>(), Arg.Any<CancellationToken>())
      .Returns(new ValidationResult(validationFailures));
            var result = await _validatoinBehvaiour.Handle(createWorkOrderCommand,
                _requestHandlerDelegate, default);

             Assert.True(result.IsError);
            Assert.Equal("Validation.property1", result.TopError.Code);
              Assert.Equal("property1 is invalid", result.TopError.Description);
        }


        [Fact]
        public async Task InvokeValidationBehavior_WhenNoValidator_ShouldInvokeNextBehavior()
        {
             var createWorkOrderCommand = OrderFactory.CreateValidPurchaseOrderCommand();
            
            var validationBehavior = new ValidationBehavior<
                CreateOrderCommand, Result<OrderDto>>(new List<IValidator<CreateOrderCommand>>(),_mocLogger);


            var workOrderResponse = OrderFactory.CreatePurchase().ToDto();

            _requestHandlerDelegate.Invoke(default).Returns(workOrderResponse);

            var result = await validationBehavior.Handle(createWorkOrderCommand, 
               _requestHandlerDelegate, default);

            Assert.True(result.IsSuccess);
            Assert.Equal(workOrderResponse, result.Value);
        }

    }

}
