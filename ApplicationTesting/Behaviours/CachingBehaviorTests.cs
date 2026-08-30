using Application.Common.Interfaces;
using Contract.Common.Behaviors;
using Contract.Common.Interfaces;
using Contract.Features.Transactions.Orders.Commands.CreateOrder;
using Contract.Features.Transactions.Orders.DTOs;
using Inventory.Domain.Common.Results;
using Inventory.Domain.Common.Results.Abstractions;
using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace InventoryManagement.Application.UnitTests.Behaviours
{
    public class CachingBehaviorTests
    {

        private HybridCache _mockCache;
        private readonly ILogger<CachingBehavior<CacheQuery,
            Result<string>>> _mocLogger;


        private readonly CachingBehavior<CacheQuery, Result<string>> _cachingBehavior;
 
        private readonly IRedisHealthState _healthState;

        public CachingBehaviorTests() {
            _healthState = Substitute.For<IRedisHealthState>();
            _healthState.IsRedisAvailable.Returns(true);

            _mocLogger = NSubstitute.Substitute.For<
                ILogger<CachingBehavior<CacheQuery, Result<string>>>>();   
             _mockCache = NSubstitute.Substitute.For<HybridCache>();
             _cachingBehavior = new CachingBehavior<CacheQuery, Result<string>>(_mockCache, _mocLogger, _healthState); 
        }


        [Fact]
        public async Task Handle_WhenNotCachedQuery_ShouldSkipCacheAndReturnResult() {
            

            var nonCachedRequest = new NoCacheQuery();

            CachingBehavior<NoCacheQuery, string> cachingBehavior = new CachingBehavior<NoCacheQuery, string>(_mockCache, Substitute.For<
                ILogger<CachingBehavior<NoCacheQuery, string>>>(), _healthState);
           
            
            
            var result = await cachingBehavior.Handle(nonCachedRequest , _ => Task.FromResult("Ok") ,CancellationToken.None);

            Assert.Equal( "Ok", result);

            await _mockCache.DidNotReceive().SetAsync(Arg.Any<string>(), Arg.Any<string>(),
              Arg.Any<HybridCacheEntryOptions>(), Arg.Any<IEnumerable<string>>(), Arg.Any<CancellationToken>());

        }
        
        [Fact]
        public async Task Handle_WhenCachedQueryAndResultIsSuccess_ShouldCacheResult()
        {
            var request = new CacheQuery();
            var response = (Result<string>)("cached result");

            _mockCache.GetOrCreateAsync<Result<string>>(request.CacheKey,
                 Arg.Any<Func<CancellationToken, ValueTask<Result<string>>>>(),
                 Arg.Any<HybridCacheEntryOptions>(),
                 Arg.Any<IEnumerable<string>>(),
                Arg.Any<CancellationToken>()).Returns(response);

            var next = Substitute.For<RequestHandlerDelegate<Result<string>>>();

            var result = await _cachingBehavior.Handle( request , next , CancellationToken.None);

            Assert.Equal(response, result);

            await next.DidNotReceive().Invoke(Arg.Any<CancellationToken>());

            await _mockCache.DidNotReceive().SetAsync(Arg.Any<string>(), Arg.Any<Result<string>>(),
              Arg.Any<HybridCacheEntryOptions>(), Arg.Any<IEnumerable<string>>(), Arg.Any<CancellationToken>());

        }

        [Fact]
        public async Task Handle_WhenCachedQueryAndResultIsError_ShouldNotCacheResult() {

            var request = new CacheQuery();

            var response = (Result<string>)Error.Validation("validation.error" , "error");

            var result = await _cachingBehavior.Handle(request, _ => Task.FromResult(response), CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal(response, result);

            await _mockCache.DidNotReceive().SetAsync(
                Arg.Any<string>(),
                Arg.Any<Result<string>>(),
                Arg.Any<HybridCacheEntryOptions>(),
                Arg.Any<IEnumerable<string>>(),
                Arg.Any<CancellationToken>());


        }

        public class NoCacheQuery;
        public class CacheQuery : ICachedQuery
        {
            public string CacheKey => "test-key";
            
        }
    }
}
