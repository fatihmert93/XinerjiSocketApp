using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XinerjiSocketApp.Infrastructure.Abstract;
using XinerjiSocketApp.Infrastructure.DataAccess.Abstract.Repository;
using XinerjiSocketApp.Infrastructure.DataAccess.Dapper;
using XinerjiSocketApp.Infrastructure.DataAccess.Dapper.MsSql;
using XinerjiSocketApp.Infrastructure.DataAccess.Dapper.Repositories;
using XinerjiSocketApp.Infrastructure.Hubs;
using XinerjiSocketApp.Infrastructure.IOC;
using XinerjiSocketApp.Service;

namespace XinerjiSocketApp.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSignalR();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyHeader()
                        .AllowAnyMethod().AllowCredentials();
                });
            });

            services.AddScoped<IConnectionFactory>(serviceProvider =>
            {
                var connectionString = "Server=.;Database=XinerjiSocketApp;Trusted_Connection=True;";
                return new SqlConnectionFactory(connectionString);
            });

            services.AddScoped(typeof(DapperGenericRepository<>), typeof(SqlGenericRepository<>));

            services.AddScoped<ICovidRepository, CovidRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<ICovidService, CovidService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMessageService, MessageService>();


            ServiceLocator.SetLocatorProvider(serviceProvider: services.BuildServiceProvider());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHub<CovidHub>("/CovidHub");

                endpoints.MapHub<UserChatHub>("/UserChatHub");
            });
        }
    }
}
