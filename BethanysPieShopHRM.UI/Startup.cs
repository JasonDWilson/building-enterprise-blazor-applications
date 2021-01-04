using BethanysPieShopHRM.UI.Data;
using BethanysPieShopHRM.UI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;

namespace BethanysPieShopHRM.UI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) { Configuration = configuration; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapBlazorHub();
                    endpoints.MapFallbackToPage("/_Host");
                });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor()
                .AddCircuitOptions(
                    options =>
                    {
                        options.DetailedErrors = true;
                    });

            services.AddProtectedBrowserStorage();

            // Helper Registrations
            services.AddScoped<IExpenseApprovalService, ManagerApprovalService>();
            services.AddScoped<IEmailService, EmailService>();

            // Http Services
            var pieShopUri = new Uri("https://localhost:44340/");
            var recruitingUri = new Uri("https://localhost:5001/");
            void RegisterTypedClient<TClient, TImplementation>(Uri apiBaseUrl)
                where TClient : class
                where TImplementation : class, TClient
            => services.AddHttpClient<TClient, TImplementation>(client => client.BaseAddress = apiBaseUrl);
            RegisterTypedClient<IEmployeeDataService, EmployeeDataService>(pieShopUri);
            RegisterTypedClient<ICountryDataService, CountryDataService>(pieShopUri);
            RegisterTypedClient<IJobCategoryDataService, JobCategoryDataService>(pieShopUri);
            RegisterTypedClient<ITaskDataService, TaskDataService>(pieShopUri);
            RegisterTypedClient<ISurveyDataService, SurveyDataService>(pieShopUri);
            //RegisterTypedClient<ICurrencyDataService, CurrencyDataService>(pieShopUri);
            RegisterTypedClient<IExpenseDataService, ExpenseDataService>(pieShopUri);
            RegisterTypedClient<IJobDataService, JobsDataService>(recruitingUri);


        }
    }
}
