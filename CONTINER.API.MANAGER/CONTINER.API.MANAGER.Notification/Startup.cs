using CONTINER.API.MANAGER.Cross.RabbitMQ.RabbitMQ;
using CONTINER.API.MANAGER.Cross.RabbitMQ.RabbitMQ.Bus;
using CONTINER.API.MANAGER.Notification.RabbitMQ.EventHandlers;
using CONTINER.API.MANAGER.Notification.RabbitMQ.Events;
using CONTINER.API.MANAGER.Notification.Repository;
using CONTINER.API.MANAGER.Notification.Repository.Data;
using CONTINER.API.MANAGER.Notification.Service;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CONTINER.API.MANAGER.Notification
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
                options.UseMySQL(Configuration["mariadb:cn"]);
            });

            services.AddScoped<IServiceMail, ServiceMail>();
            services.AddScoped<IRepositoryMail, RepositoryMail>();
            services.AddScoped<IContextDatabase, ContextDatabase>();

            /*Start RabbitMQ*/
            services.AddMediatR(typeof(Startup));
            services.AddRabbitMQ();
            services.AddTransient<MailEventHandler>();
            services.AddTransient<IEventHandler<MailCreatedEvent>, MailEventHandler>();
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
            eventBus.Subscribe<MailCreatedEvent, MailEventHandler>();

        }
    }
}
