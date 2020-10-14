using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using SDCode.Web.Classes;
using SDCode.Web.Classes.Database;
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
            services.AddScoped<IImageIndexesGetter, ImageIndexesGetter>();
            services.AddScoped<IStimuliImageDataUrlGetter, StimuliImageDataUrlGetter>();
            services.AddScoped<IPhaseSetsGetter, PhaseSetsGetter>();
            services.AddScoped<INextImageGetter, NextImageGetter>();
            services.AddScoped<IImageCongruencyGetter, ImageCongruencyGetter>();
            services.AddScoped<ITestNameGetter, TestNameGetter>();
            services.AddScoped<IImageContextGetter, ImageContextGetter>();
            services.AddScoped<IProgressGetter, ProgressGetter>();
            services.AddScoped<IStanfordRepository, StanfordRepository>();
            services.AddScoped<IResponseFeedbackGetter, ResponseFeedbackGetter>();
            services.AddScoped<ICollectionRandomizer, CollectionRandomizer>();
            services.AddScoped<ITestResponsesRepository, TestResponsesRepository>();
            services.AddScoped<ISessionMetaRepository, SessionMetaRepository>();
            services.AddScoped<ICommaDelimitedIntegersCollector, CommaDelimitedIntegersCollector>();
            services.AddScoped<IDataTypeDescriptionGetter, DataTypeDescriptionGetter>();
            services.AddScoped<IModelTypeCsvFilenameGetter, ModelTypeCsvFilenameGetter>();
            services.AddScoped<IStimuliImageUrlGetter, StimuliImageUrlGetter>();
            services.AddScoped<ITestStartTimeGetter, TestStartTimeGetter>();
            services.AddScoped<IReturningUserPhaseDataGetter, ReturningUserPhaseDataGetter>();
            services.AddScoped<IEncodingFinishedChecker, EncodingFinishedChecker>();
            services.AddScoped<IParticipantEnrollmentVerifier, ParticipantEnrollmentVerifier>();
            services.AddScoped<IConfidencesDescriptionGetter, ConfidencesDescriptionGetter>();
            services.AddScoped<IJudgementsDescriptionGetter, JudgementsDescriptionGetter>();
            services.AddScoped<ITestInstructionsViewModelGetter, TestInstructionsViewModelGetter>();
            services.AddScoped<IConfidencesDescriptionsGetter, ConfidencesDescriptionsGetter>();
            services.AddScoped<ISleepQuestionsRepository, SleepQuestionsRepository>();
            services.AddScoped<IConsentRepository, ConsentRepository>();
            services.AddScoped<IPSQIRepository, PSQIRepository>();
            services.AddScoped<IEpworthRepository, EpworthRepository>();
            services.AddScoped<IDemographicsRepository, DemographicsRepository>();
            services.AddScoped<IObscuredImagesRepository, ObscuredImagesRepository>();
            services.AddScoped<INeglectedImagesRepository, NeglectedImagesRepository>();
            services.AddScoped<SQLiteDBContext, SQLiteDBContext>();
            services.AddScoped<IEmailSender, SendGridEmailSender>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<SQLiteDBContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SQLiteDBContext dbContext)
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
            dbContext.Database.Migrate();
        }
    }
}
