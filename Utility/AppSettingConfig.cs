using System.Net.Http.Headers;
using BlazorLoker2022.Service;
using BlazorLoker2022.Service.Pelamar;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;


namespace BlazorAgentVs2022.Utility
{
    public static class AppSettingConfig
    {
        public static IServiceCollection AddAppSettingServiceAsync(
            this IServiceCollection services,
            string apiEndPoint_Pelamar)
        {
            services.AddScoped<AuthenticationStateProvider, MyAuthenticationProvider>();
            services.AddScoped<TokenControl>();
            services.AddHttpClient<ServicePelamarTest>(x => { x.BaseAddress = new Uri(apiEndPoint_Pelamar); })
                .AddHttpMessageHandler<TokenControl>();
            services.AddHttpClient<ServicePelamarLogin>(x => { x.BaseAddress = new Uri(apiEndPoint_Pelamar); })
                 .AddHttpMessageHandler<TokenControl>();
            //services.AddHttpClient<ServicePelamarMotivations>(x => { x.BaseAddress = new Uri(apiEndPoint_Pelamar); })
            //   .AddHttpMessageHandler<TokenControl>();
            services.AddHttpClient<ServicePelamarLoker>(x => { x.BaseAddress = new Uri(apiEndPoint_Pelamar); })
                .AddHttpMessageHandler<TokenControl>();
            services.AddHttpClient<ServicePelamarQuiz>(x => { x.BaseAddress = new Uri(apiEndPoint_Pelamar); })
                 .AddHttpMessageHandler<TokenControl>();
            services.AddHttpClient<ServicePelamarBiodata>(x => { x.BaseAddress = new Uri(apiEndPoint_Pelamar); })
             .AddHttpMessageHandler<TokenControl>();
            services.AddTransient<HttpRequestMessage>();

            //ApiPublic
            //services.AddHttpClient<ServiceApiPublic>(x => { x.BaseAddress = new Uri(apiEndPoint); });
            return services;

        }
    }
}
