﻿using AbpBlazorServerDemo.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace AbpBlazorServerDemo
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreMvcModule)
    )]
    public class AbpBlazorServerDemoModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddServerSideBlazor();
            context.Services.AddSingleton<WeatherForecastService>();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var env = context.GetEnvironment();
            var app = context.GetApplicationBuilder();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseConfiguredEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}