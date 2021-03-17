using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Savorboard.CAP.InMemoryMessageQueue;
using TestApi.Commands;
using TestApi.Interfaces;
using TestApi.Services;

namespace TestApi
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
            services.AddHttpClient();
            services.AddMediatR(typeof(Startup));
            services.AddSwaggerGen();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingCommand<,>));

            services.AddCap(options =>
            {
                options.FailedRetryCount = 3;
                options.FailedRetryInterval = 60;
                options.UseInMemoryStorage();
                options.UseInMemoryMessageQueue();
                options.UseDashboard();
            });


            services.AddHostedService<QueueService>();
            services.AddHostedService<CapMonitorService>();
            services.AddSingleton<IEventQueue,EventQueue>();
            services.AddSingleton<CapPublishService>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}