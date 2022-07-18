using BlazorLoker2022.Service.Pelamar;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLoker2022.Pages.Login
{
    public class PelamarResetPasswordBase : ComponentBase
    {
        [Inject]
        protected ServicePelamarLogin servicePelamarLogin { get; set; }

        [Inject]
        protected NavigationManager navigationManager { get; set; }

        [Inject]
        protected IJSRuntime? Js { get; set; }

        [Inject]
        protected Blazored.LocalStorage.ILocalStorageService LocalStorage { get; set; }
        protected string? otp;
        protected string? msg;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

        }
        protected override void OnInitialized()
        {
        }
        protected override async Task OnInitializedAsync()
        {

        }
        
        protected async Task resetPassword()
        {
            try
            {
                msg = await servicePelamarLogin.resetPassword(otp);
                await Js.InvokeVoidAsync("notifDev", msg, "success", 3000);
                navigationManager.NavigateTo("/PelamarBuatPasswordBaru");
            }
            catch (Exception ex)
            {
                await Js.InvokeVoidAsync("notifDev", ex.Message, "error", 3000);
            }
        }
    }
}
