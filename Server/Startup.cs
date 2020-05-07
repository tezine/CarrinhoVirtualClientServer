#region Imports
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Server.Codes;
using Server.Controllers;
#endregion

namespace Server {
    public class Startup {
        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            // requires using Microsoft.Extensions.Options
            //doc aqui https://docs.microsoft.com/pt-br/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-3.1&tabs=visual-studio
            services.Configure<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));
            services.AddSingleton<IBlazorAppDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
            services.AddCors(options => {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder => {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            SetupJwt(services);
            services.AddMvc(options => {
                options.Filters.Add(typeof(SGlobalExceptionHandler));
                options.EnableEndpointRouting = false;
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                //options.JsonSerializerOptions.PropertyNamingPolicy = null;
            }); ;//adicionado para suportar webapi
            services.AddSwaggerDocument(config => {
                config.PostProcess = document => {
                    document.Info.Version = "v1";
                    document.Info.Title = "Carrinho Virtual API";
                    document.Info.Description = "A simple ASP.NET Core web API";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact {
                        Name = "Bruno Tezine",
                        Email = string.Empty,
                        Url = "https://test.com"
                    };
                    document.Info.License = new NSwag.OpenApiLicense {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    };
                };
            });
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression();
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            SetupApiVersioning(services);
            services.AddControllers();
            services.AddHttpClient();
            AddServerSingletons(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(MyAllowSpecificOrigins);
            app.UseResponseCompression();
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
            SDefines.ConnectionString = Configuration["ConnectionStrings:SampleConnection"];
        }

        #region SetupJwt
        private void SetupJwt(IServiceCollection services) {
            services.AddAuthentication()
                //.AddCookie(cfg => cfg.SlidingExpiration = true)
                .AddJwtBearer(cfg => {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = false;
                    cfg.TokenValidationParameters = new TokenValidationParameters() {
                        ValidIssuer = SDefines.TokenIssuer,
                        ValidAudience = SDefines.TokenAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SDefines.Token))
                    };
                });
        }
        #endregion

        #region SetupApiVersioning
        private void SetupApiVersioning(IServiceCollection services) {
            services.AddApiVersioning(
                o => {
                    o.AssumeDefaultVersionWhenUnspecified = true;
                    o.DefaultApiVersion = new ApiVersion(1, 0);
                });
        }
        #endregion

        #region AddServerSingletons
        private void AddServerSingletons(IServiceCollection services) {
            //services.AddSingleton<WeatherForecastService>();
            services.AddSingleton<SUsersService>();
            services.AddSingleton<SAdminUsersService>();
            services.AddSingleton<SProductsService>();
            services.AddSingleton<SSearchService>();
            services.AddSingleton<SCompaniesService>();
            services.AddSingleton<SUsersCompaniesService>();
            services.AddSingleton<SCompaniesAdminUsersService>();
        }
        #endregion
    }
}
