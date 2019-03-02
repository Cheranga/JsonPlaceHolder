using JsonPlaceHolder.Services;
using JsonPlaceHolder.Services.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace JsonPlaceHolder
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
            RegisterConfigs(services);
            RegisterServices(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        private void RegisterConfigs(IServiceCollection services)
        {
            services.Configure<PostServiceConfig>(Configuration.GetSection("PostServiceConfig"));
            services.AddSingleton(provider =>
            {
                var configValue = provider.GetRequiredService<IOptions<PostServiceConfig>>().Value;
                return configValue;
            });
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddHttpClient<IPostsService, PostsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}