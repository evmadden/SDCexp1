using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SDCode.Web.Classes;
using SDCode.Web.Models;

namespace SDCode.Web
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
            services.Configure<Config>(Configuration.GetSection("Config"));
            services.AddControllersWithViews(options=>{
                options.Filters.Add<ParticipantVerifyFilter>();
            }).AddRazorRuntimeCompilation();
            //services.AddScoped<ICsvFile<HolidayModel, HolidayMap>, CsvFile<HolidayModel, HolidayMap>>(); // todo mlh remove
            services.AddScoped<ICsvFile<ConsentModel, ConsentMap>, CsvFile<ConsentModel, ConsentMap>>();
            services.AddScoped<ICsvFile<DemographicsModel, DemographicsMap>, CsvFile<DemographicsModel, DemographicsMap>>();
            services.AddScoped<ICsvFile<PSQIModel, PSQIMap>, CsvFile<PSQIModel, PSQIMap>>();
            services.AddScoped<ICsvFile<EpworthModel, EpworthMap>, CsvFile<EpworthModel, EpworthMap>>();
            services.AddScoped<ICsvFile<StanfordModel, StanfordMap>, CsvFile<StanfordModel, StanfordMap>>();
            services.AddScoped<IImageIndexesGetter, ImageIndexesGetter>();
            services.AddScoped<IStimuliImageDataUrlGetter, StimuliImageDataUrlGetter>();
            services.AddScoped<ICsvFile<PhaseSetsModel, PhaseSetsModel.Map>, CsvFile<PhaseSetsModel, PhaseSetsModel.Map>>();
            services.AddScoped<IPhaseSetsGetter, PhaseSetsGetter>();
            services.AddScoped<INextImageGetter, NextImageGetter>();
            services.AddScoped<IImageCongruencyGetter, ImageCongruencyGetter>();
            services.AddScoped<ICsvFile<ResponseDataModel, ResponseDataModel.Map>, CsvFile<ResponseDataModel, ResponseDataModel.Map>>();
            services.AddScoped<ITestNameGetter, TestNameGetter>();
            services.AddScoped<IImageContextGetter, ImageContextGetter>();
            services.AddScoped<IResponseDataCsvFileGetter, ResponseDataCsvFileGetter>();
            services.AddScoped<IProgressGetter, ProgressGetter>();
            services.AddScoped<IStanfordRepository, StanfordRepository>();
            services.AddScoped<IResponseFeedbackGetter, ResponseFeedbackGetter>();
            services.AddScoped<ICollectionRandomizer, CollectionRandomizer>();
            services.AddScoped<ICsvFile<SessionMetaModel, SessionMetaModel.Map>, CsvFile<SessionMetaModel, SessionMetaModel.Map>>();
            services.AddScoped<ITestResponsesRepository, TestResponsesRepository>();
            services.AddScoped<IRepository<ConsentModel>, Repository<ConsentModel, ConsentMap>>();
            services.AddScoped<IRepository<DemographicsModel>, Repository<DemographicsModel, DemographicsMap>>();
            services.AddScoped<IRepository<PSQIModel>, Repository<PSQIModel, PSQIMap>>();
            services.AddScoped<IRepository<EpworthModel>, Repository<EpworthModel, EpworthMap>>();
            services.AddScoped<ISessionMetaRepository, SessionMetaRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
