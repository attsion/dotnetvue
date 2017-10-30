
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EEQG.Data
{
    public static class StoreExtensions
    {
        public static void AddAppStores(this IServiceCollection service,bool addPeopleBase=true)
        {
            if (addPeopleBase)
            {
                service.AddScoped<PeopleStore>();
            }

            service.AddScoped<ClassifyDevStore>();
            service.AddScoped<ClassifyStore>();
            service.AddScoped<PeopleScalesStore>();
        }
    }
}
