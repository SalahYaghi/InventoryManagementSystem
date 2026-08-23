using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubcutaneousTests.Common
{
    [CollectionDefinition(CollectionName)]
    public class WebAppFactoryCollection : ICollectionFixture<WebAppFactory>
    {
        public const string CollectionName = "WebAppFactory Collection";
    }
}
