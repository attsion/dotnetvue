using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using EEQG.Data;
namespace EEQG.ServerData
{
    public static class ServerStoreExtensions
    {
        public static void AddAppServerStores(this IServiceCollection service)
        {
            service.AddAppStores(false);
            service.AddScoped<PeopleServerStrore>();
            service.AddScoped<TransFileServerStore>();
        }
    }
}
