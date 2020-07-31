using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CONTINER.API.MANAGER.Cross.RabbitMQ.RabbitMQ;
using CONTINER.API.MANAGER.Cross.RabbitMQ.RabbitMQ.Bus;
using CONTINER.API.MANAGER.History.RabbitMQ.EventHandlers;
using CONTINER.API.MANAGER.History.RabbitMQ.Events;
using CONTINER.API.MANAGER.History.Repository;
using CONTINER.API.MANAGER.History.Service;


namespace CONTINER.API.MANAGER.History
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
            services.AddScoped<IServiceHistory, ServiceHistory>();
            services.AddScoped<IRepositoryHistory, RepositoryHistory>();

            /*Start RabbitMQ*/
            services.AddMediatR(typeof(Startup));
            services.AddRabbitMQ();
            services.AddTransient<DepositEventHandler>();
            services.AddTransient<WithdrawalEventHandler>();
            services.AddTransient<IEventHandler<DepositCreatedEvent>, DepositEventHandler>();
            services.AddTransient<IEventHandler<WithdrawalCreatedEvent>, WithdrawalEventHandler>();
            /*End RabbitMQ*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<DepositCreatedEvent, DepositEventHandler>();
            eventBus.Subscribe<WithdrawalCreatedEvent, WithdrawalEventHandler>();
        }
    }
}
