using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Zup.AdministracaoClientes.API.Configurations;
using Zup.AdministracaoClientes.API.Types;
using Zup.AdministracaoClientes.Data.Context;
using Zup.AdministracaoClientes.Data.Types;
using Zup.AdministracaoClientes.Domain.Types;
using Zup.AdministracaoClientes.Infra.CrossCutting.IoC;
using Zup.AdministracaoClientes.Infra.CrossCutting.Swagger;

namespace Zup.AdministracaoClientes.API
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
            ApplicationSettingsType _applicationSettings = Configuration.GetSection(ApplicationSettingsType.KEY)
                                                                        .Get<ApplicationSettingsType>();
            
            // In Memory
            if (_applicationSettings.TestInMemoryDatabase)
            {
                services.AddDbContext<AdministracaoClientesContext>(opt =>
                    opt.UseInMemoryDatabase(AdministracaoClientesContext.DATABASE_NAME));
            }
            else
                services.AddDbContext<AdministracaoClientesContext>();

            services
                .AddControllers(options =>
                {
                    // To configure Api versioning.
                    options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                    options.UseCentralRoutePrefix(new RouteAttribute("api/v{version:apiVersion}"));

                    // To configure controller routes in kebab-case.
                    options.Conventions.Add(new RouteTokenTransformerConvention(
                        new SlugifyParameterTransformer()));
                })

                .ConfigureApiBehaviorOptions(options =>
                {
                    // To show ModelState.IsValid errors.
                    options.SuppressModelStateInvalidFilter = true;
                })

                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.UseCamelCasing(true);
                });


            /*
             * Gabriel Vicente:
             * In case you need to use API versioning, you should add the [ApiVersion("x.0")] attribute to the
             * specific method. In case you need 2 method versions in the same controller, you also need to
             * add the two attributes (of the two versions) to the controller. E.g.:
             *
             *  [ApiVersion("1.0")]
             *  [ApiVersion("2.0")]
             *  [Authorize, Route("[controller]"), ApiController]
             *  public class TestController : ControllerBase
             *
             * Ref: https://www.talkingdotnet.com/support-multiple-versions-of-asp-net-core-web-api/
             */

            RegisterOptions(services);

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(_applicationSettings.ApiVersion ?? 1, 0);
                options.UseApiBehavior = false;
            });

            services.AddSwaggerConfiguration();

            NativeInjector.RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseAuthentication();

            app.UseSwaggerConfiguration();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        private void RegisterOptions(IServiceCollection services)
        {
            services.Configure<ApplicationSettingsType>(Configuration.GetSection(ApplicationSettingsType.KEY));
            services.Configure<CPFBlacklistType>(Configuration.GetSection(CPFBlacklistType.KEY));
            services.Configure<ConnectionStringsType>(Configuration.GetSection(ConnectionStringsType.KEY));
        }
    }
}
