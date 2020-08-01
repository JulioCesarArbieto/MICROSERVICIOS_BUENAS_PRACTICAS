using CONTINER.API.MANAGER.Cross.Jwt.Jwt;
using CONTINER.API.MANAGER.Cross.Proxy.Proxy;
using CONTINER.API.MANAGER.Cross.RabbitMQ.RabbitMQ;
using CONTINER.API.MANAGER.Deposit.RabbitMQ.CommandHandlers;
using CONTINER.API.MANAGER.Deposit.RabbitMQ.Commands;
using CONTINER.API.MANAGER.Deposit.Repository;
using CONTINER.API.MANAGER.Deposit.Repository.Data;
using CONTINER.API.MANAGER.Deposit.Service;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CONTINER.API.MANAGER.Deposit
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<ContextDatabase>(
             options =>
             {
                 options.UseNpgsql(Configuration["postgres:cn"]);

             });

            services.AddScoped<IServiceTransaction, ServiceTransaction>();
            services.AddScoped<IServiceAccount, ServiceAccount>();
            services.AddScoped<IRepositoryTransaction, RepositoryTransaction>();
            services.AddScoped<IContextDatabase, ContextDatabase>();

            /*Start RabbitMQ*/
            services.AddMediatR(typeof(Startup));
            services.AddRabbitMQ();
            services.AddTransient<IRequestHandler<DepositCreateCommand, bool>, DepositCommandHandler>();
            services.AddTransient<IRequestHandler<MailCreateCommand, bool>, MailCommandHandler>();
            /*End RabbitMQ*/

            //services.Configure<JwtOptions>(Configuration.GetSection("jwt"));
            //services.AddProxyHttp();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
