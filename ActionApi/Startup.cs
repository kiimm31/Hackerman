using Action.Domain;
using ActionApi.Commands;
using ActionApi.Extensions;
using ActionApi.Helpers;
using ActionApi.Interfaces;
using ActionApi.Services;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuartzJob;
using QuartzLibrary.Standard.QuartzJob;
using Savorboard.CAP.InMemoryMessageQueue;
using Serilog;

namespace ActionApi
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
            services.AddHttpClient<IHttpHelper>();
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
        
            services.AddMvc();
            services.AddDistributedMemoryCache();
            services.AddAutoMapper(typeof(Startup));
            services.AddHostedService<QueueService>();
            services.AddHostedService<CapMonitorService>();
            services.AddSingleton<IEventQueue, EventQueue>();
            services.AddSingleton<CapPublishService>();
            services.AddTransient<IHttpHelper, HttpHelper>();
            services.AddDbContext<ActionContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(nameof(ActionApi)))
                , ServiceLifetime.Transient);

            services.AddHealthChecks();
            services.AddHangFire("ActionApi", Configuration);

            services.AddQuartz();
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
            
            app.UseHangfireDashboard();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            app.UseSwagger();
            app.UseSerilogRequestLogging();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHangFire();
        }
    }
}