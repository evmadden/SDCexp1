using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddScoped<ICsvFile<ConsentModel, ConsentModel.Map>, CsvFile<ConsentModel, ConsentModel.Map>>();
            services.AddScoped<ICsvFile<DemographicsModel, DemographicsModel.Map>, CsvFile<DemographicsModel, DemographicsModel.Map>>();
            services.AddScoped<ICsvFile<PSQIModel, PSQIModel.Map>, CsvFile<PSQIModel, PSQIModel.Map>>();
            services.AddScoped<ICsvFile<EpworthModel, EpworthModel.Map>, CsvFile<EpworthModel, EpworthModel.Map>>();
            services.AddScoped<ICsvFile<StanfordModel, StanfordModel.Map>, CsvFile<StanfordModel, StanfordModel.Map>>();
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
            services.AddScoped<IRepository<ConsentModel>, Repository<ConsentModel, ConsentModel.Map>>();
            services.AddScoped<IRepository<DemographicsModel>, Repository<DemographicsModel, DemographicsModel.Map>>();
            services.AddScoped<IRepository<PSQIModel>, Repository<PSQIModel, PSQIModel.Map>>();
            services.AddScoped<IRepository<EpworthModel>, Repository<EpworthModel, EpworthModel.Map>>();
            services.AddScoped<ISessionMetaRepository, SessionMetaRepository>();
            services.AddScoped<ICommaDelimitedIntegersCollector, CommaDelimitedIntegersCollector>();
            services.AddScoped<IDataTypeDescriptionGetter, DataTypeDescriptionGetter>();
            services.AddScoped<IModelTypeCsvFilenameGetter, ModelTypeCsvFilenameGetter>();
            services.AddScoped<IStimuliImageUrlGetter, StimuliImageUrlGetter>();
            services.AddScoped<ITestStartTimeGetter, TestStartTimeGetter>();
            services.AddScoped<IReturningUserPhaseDataGetter, ReturningUserPhaseDataGetter>();
            services.AddScoped<IEncodingFinishedChecker, EncodingFinishedChecker>();
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
