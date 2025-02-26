using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ServiceColectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection servicesCollection,
            ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(servicesCollection);
            }

            return ServiceTool.Create(servicesCollection);
        }

    }
}
