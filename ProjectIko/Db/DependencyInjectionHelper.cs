using ProjectIko.Db.Interface;
using ProjectIko.Db.Repository;

namespace ProjectIko.Db
{
    public static class DependencyInjectionHelper
    {
        public static void RegisterPGDataLayerServices(this IServiceCollection services)
        {
            services.AddTransient<IIkoRepository, IkoRepository>();
        }
    }
}
