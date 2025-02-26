using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Core.CrossCuttingConserns.Caching;
using Core.CrossCuttingConserns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolvers
{
    public class CoreModule:ICoreModule
    {
        public void Load(IServiceCollection servicesCollection)
        {
            servicesCollection.AddMemoryCache();
           servicesCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
           servicesCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
           servicesCollection.AddSingleton<Stopwatch>();
        }
    }
}
