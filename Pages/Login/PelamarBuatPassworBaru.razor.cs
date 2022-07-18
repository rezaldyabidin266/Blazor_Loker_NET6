using BlazorLoker2022.Resource.Login;
using BlazorLoker2022.Service.Pelamar;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLoker2022.Pages.Login
{
    public class PelamarBuatPassworBaruBase : ComponentBase
    {
        [Inject]
        protected ServicePelamarLogin servicePelamarLogin { get; set; }

        [Inject]
        protected NavigationManager navigationManager { get; set; }

        [Inject]
        protected IJSRuntime? Js { get; set; }

        [Inject]
        protected Blazored.LocalStorage.ILocalStorageService LocalStorage { get; set; }
        protected string? pwBaru;
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

        protected async Task kirimPwBaru()
        {
            try
            {
                PelamarLoginClass pelamarLoginClass = new PelamarLoginClass()
                {
                    ipAddress ="null",
                    browser = "null",
                    latitude = "null",
                    longitude = "null",
                    remarks = "null",
                    email = email,
                    password = pwBaru
                };
                await Js.InvokeVoidAsync("console.log", pelamarLoginClass);
                msg = await servicePelamarLogin.buatPasswordBaru(pelamarLoginClass);
                await Js.InvokeVoidAsync("notifDev", msg, "success", 3000);
                navigationManager.NavigateTo("/PelamarLogin");
            }
            catch (Exception ex)
            {
                await Js.InvokeVoidAsync("notifDev", ex.Message, "error", 3000);
            }
        }
    }
}
