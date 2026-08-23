using Contract.Common.Behaviors;
using FluentValidation;
using MediatR.Pipeline;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Contract
{
    public static class DependencyInjection
    {

        private static IServiceCollection AddMediatRService(this IServiceCollection services) {

            var assembly = Assembly.GetExecutingAssembly();
            
            services.AddMediatR(options => {
                options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                options.AddOpenRequestPostProcessor(typeof(LoggingPostProcessor<,>));
                options.AddOpenRequestPreProcessor(typeof(LoggingProcessor<>));

                options.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
                options.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
                options.AddOpenBehavior(typeof(ValidationBehavior<,>));
                options.AddOpenBehavior(typeof(CachingBehavior<,>));
               
            });
            
            services.AddValidatorsFromAssembly(assembly);

            services.AddTransient(typeof(IRequestPreProcessor<>), typeof(LoggingProcessor<>));
            services.AddTransient(typeof(IRequestPostProcessor<,>), typeof(LoggingPostProcessor<,>));

 

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddMediatRService();

            return services;
        }
    }
}

