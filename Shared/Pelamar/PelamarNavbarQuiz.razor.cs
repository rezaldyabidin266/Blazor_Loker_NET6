using BlazorAgentVs2022.Utility;
using BlazorLoker2022.Resource.Login;
using BlazorLoker2022.Service.Pelamar;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace BlazorLoker2022.Shared.Pelamar
{
    public class PelamarNavbarQuizBase : ComponentBase
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
        protected PelamarGetClass getPelamarData = new PelamarGetClass();

        protected string? token;
        protected string? unauthorized;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
        }
        protected override void OnInitialized()
        {

        }
        protected override async Task OnInitializedAsync()
        {
            unauthorized = await LocalStorage.GetItemAsync<string>("statusCode");
            await getPelamar();
            token = await LocalStorage.GetItemAsync<string>("token");
      
        }
        protected async Task getPelamar()
        {
            try
            {
                getPelamarData = await servicePelamarLogin.getPelamar();
                await Js.InvokeVoidAsync("console.log", getPelamarData);
            }
            catch (Exception ex)
            {
                Js.InvokeVoidAsync("console.log", ex.Message);
            }
        }
        protected void goLogout()
        {
            ((MyAuthenticationProvider)authenticationStateProvider).MarkUserAsLogout();
            navigationManager.NavigateTo("/pelamarLogin");
        }
    }
}
