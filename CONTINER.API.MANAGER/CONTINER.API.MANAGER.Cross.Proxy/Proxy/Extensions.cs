using System;
using System.Collections.Generic;
using System.Text;

namespace CONTINER.API.MANAGER.Cross.Proxy.Proxy
{
    public static class Extensions
    {
        public static IServiceCollection AddProxyHttp(this IServiceCollection services)
        {
            services.AddSingleton<IHttpClient, CustomHttpClient>();
            return services;
        }
    }
}
