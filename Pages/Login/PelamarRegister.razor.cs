using BlazorLoker2022.Resource.Login;
using BlazorLoker2022.Service.Pelamar;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace BlazorLoker2022.Pages.Login
{
    public class PelamarRegisterBase : ComponentBase
    {
        [Inject]
        protected ServicePelamarLogin servicePelamarLogin { get; set; }

        [Inject]
        protected NavigationManager navigationManager { get; set; }

        [Inject]
        protected IJSRuntime? Js { get; set; }

        [Inject]
        protected Blazored.LocalStorage.ILocalStorageService LocalStorage { get; set; }

        [Inject]
        protected AuthenticationStateProvider authenticationStateProvider { get; set; }

        protected string token;
        protected string browserUser;
        protected int nomorTelepon;
        public EditContext? RegisterPelamar { get; set; }

        protected PelamarRegisterClass registerPelamarClass = new PelamarRegisterClass();
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            //await Js.InvokeVoidAsync("mouse_login");
        }
        protected override void OnInitialized()
        {
            RegisterPelamar = new EditContext(registerPelamarClass);
        }
        protected override async Task OnInitializedAsync()
        {
            registerPelamarClass.tglLahir = DateTime.Now;
            browserUser = await Js.InvokeAsync<string>("myBrowser");
            await Js.InvokeVoidAsync("getLocation");
        }
        protected void goLogin()
        {
            navigationManager.NavigateTo("/pelamarLogin");
        }
        protected async Task postRegister()
        {
            if (RegisterPelamar.Validate())
            {
                try
                {
                    registerPelamarClass = await servicePelamarLogin.registerPelamar(registerPelamarClass);
                    await Js.InvokeVoidAsync("console.log", registerPelamarClass);
                    await Js.InvokeVoidAsync("notifDev", "Silahkan Login", "success", 3000);
                    navigationManager.NavigateTo("/pelamarLogin");
                }
                catch (Exception ex)
                {
                    await Js.InvokeVoidAsync("notifDev", ex.Message, "error", 3000);
                }
            }
        }
        protected void goLoker()
        {
            navigationManager.NavigateTo("/");
        }
    }
}
