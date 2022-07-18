using BlazorAgentVs2022.Utility;
using BlazorLoker2022.Resource.Login;
using BlazorLoker2022.Service.Pelamar;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace BlazorLoker2022.Pages.Pelamar
{
    public class PelamarLoginBase : ComponentBase
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
        public EditContext? LoginPelamar { get; set; }
        public EditContext? RegisterPelamar { get; set; }

        protected PelamarLoginClass pelamarLoginClass = new PelamarLoginClass();
        protected PelamarRegisterClass registerPelamarClass = new PelamarRegisterClass();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            //await Js.InvokeVoidAsync("mouse_login");
        }
        protected override void OnInitialized()
        {
            LoginPelamar = new EditContext(pelamarLoginClass);
            RegisterPelamar = new EditContext(registerPelamarClass);
        }
        protected override async Task OnInitializedAsync()
        {
            registerPelamarClass.tglLahir = DateTime.Now;
            browserUser = await Js.InvokeAsync<string>("myBrowser");
            await Js.InvokeVoidAsync("getLocation");
         
        }

        protected async Task postLogin()
        {
            if (LoginPelamar.Validate())
            {
                try
                {
                    var koordinat = await LocalStorage.GetItemAsStringAsync("Koordinat");
                    PelamarLocation koordinatResult = JsonConvert.DeserializeObject<PelamarLocation>(koordinat);
                    pelamarLoginClass.browser = browserUser;
                    pelamarLoginClass.longitude = koordinatResult.Longitude;
                    pelamarLoginClass.latitude = koordinatResult.Latitude;
                    pelamarLoginClass.remarks = koordinatResult.Remarks;
                    pelamarLoginClass.ipAddress = "0";
                    token = await servicePelamarLogin.loginPelamar(pelamarLoginClass);
                    await LocalStorage.SetItemAsync("token", token);
                    ((MyAuthenticationProvider)authenticationStateProvider).MarkUserAsAuthenticated(token);
                    navigationManager.NavigateTo("/");
                }
                catch (Exception ex)
                {
                    await Js.InvokeVoidAsync("notifDev", ex.Message, "error", 3000);
                }
            }
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
                }
                catch (Exception ex)
                {
                    await Js.InvokeVoidAsync("notifDev", ex.Message, "error", 3000);
                }
            }
        }
        
        protected void goRegister()
        {
            navigationManager.NavigateTo("/register");
        }
        protected void goLoker()
        {
            navigationManager.NavigateTo("/");
        }
    }
}

