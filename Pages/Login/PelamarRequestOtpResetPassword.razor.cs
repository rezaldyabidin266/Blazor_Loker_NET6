using BlazorLoker2022.Service.Pelamar;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLoker2022.Pages.Login
{
    public class PelamarRequestOtpResetPasswordBase : ComponentBase
    {
        [Inject]
        protected ServicePelamarLogin servicePelamarLogin { get; set; }

        [Inject]
        protected NavigationManager navigationManager { get; set; }

        [Inject]
        protected IJSRuntime? Js { get; set; }

        [Inject]
        protected Blazored.LocalStorage.ILocalStorageService LocalStorage { get; set; }
        protected string? email;
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

        protected async Task requestOtp()
        {
            try
            {
                msg = await servicePelamarLogin.requestOtpResetPassword(email);
                await Js.InvokeVoidAsync("notifDev", msg, "success", 3000);
                navigationManager.NavigateTo("/PelamarResetPassword");
            }
            catch (Exception ex)
            {
                await Js.InvokeVoidAsync("notifDev", ex.Message, "error", 3000);
            }
        }

    }
}
