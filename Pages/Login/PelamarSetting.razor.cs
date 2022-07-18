using BlazorLoker2022.Resource.Pelamar.Login;
using BlazorLoker2022.Service;
using BlazorLoker2022.Service.Pelamar;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLoker2022.Pages.Login
{
    public class PelamarSettingBase : ComponentBase
    {
        [Inject]
        protected ServicePelamarLogin servicePelamarLogin { get; set; }
        [Inject]
        protected ServicePelamarBiodata servicePelamarBiodata { get; set; }

        [Inject]
        protected NavigationManager navigationManager { get; set; }

        [Inject]
        protected ServicePelamarQuiz servicePelamarQuiz { get; set; }

        [Inject]
        protected IJSRuntime? Js { get; set; }

        [Inject]
        protected Blazored.LocalStorage.ILocalStorageService LocalStorage { get; set; }

        protected string? passwordLama;
        protected string? passwordBaru;
        protected string? passwordConfirm;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

        }
        
        protected override async void OnInitialized()
        {

        }
        
        protected override async Task OnInitializedAsync()
        {

        }
        
        protected async Task kirimUbahPassword()
        {
            if (passwordBaru == passwordConfirm)
            {
                try
                {
                    var ubahPw = new PelamarUbahPassword
                    {
                        passwordLama = passwordLama,
                        passwordBaru = passwordBaru,
                    };
                    string msg = await servicePelamarLogin.ubahPassword(ubahPw);
                    await Js.InvokeVoidAsync("notifDev", msg, "success", 3000);
                }
                catch (Exception ex)
                {
                    await Js.InvokeVoidAsync("notifDev", ex.Message, "error", 3000);
                }
            }
            else
            {
                await Js.InvokeVoidAsync("notifDev", "Kata sandi unverifikasi", "error", 3000);
            }
        }
        
    }
}
