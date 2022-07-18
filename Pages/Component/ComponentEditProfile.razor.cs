using BlazorLoker2022.Resource.Login;
using BlazorLoker2022.Service;
using BlazorLoker2022.Service.Pelamar;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace BlazorLoker2022.Pages.Component
{
    public class ComponentEditProfileBase: ComponentBase
    {
        [Inject]
        protected ServicePelamarLogin servicePelamarLogin { get; set; }
        [Inject]
        protected ServicePelamarBiodata servicePelamarBiodata { get; set; }

        [Inject]
        protected NavigationManager navigationManager { get; set; }

        [Inject]
        protected IJSRuntime? Js { get; set; }

        [Inject]
        protected Blazored.LocalStorage.ILocalStorageService LocalStorage { get; set; }
        protected PelamarGetClass updatePelamar = new PelamarGetClass();
        protected PelamarGetClass getPelamarData = new PelamarGetClass();
        public EditContext? updatePelamarContext { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

        }
        protected override async void OnInitialized()
        {

            updatePelamarContext = new EditContext(getPelamarData);
            var unauthorized = await LocalStorage.GetItemAsync<string>("statusCode");
            if (unauthorized == "Unauthorized")
            {
                navigationManager.NavigateTo("/pelamarLogin");
            }

        }
        protected override async Task OnInitializedAsync()
        {
            await getPelamar();
        }
        protected async Task getPelamar()
        {
            try
            {
                getPelamarData = await servicePelamarLogin.getPelamar();
            }
            catch (Exception ex)
            {
                Js.InvokeVoidAsync("console.log", ex.Message);
            }
        }
        protected async void kirimUpdatePelamar()
        {
            if (updatePelamarContext.Validate())
            {
                try
                {
                    getPelamarData.password = "*****";
                    var data = await servicePelamarLogin.updatePelamar(getPelamarData);
                    await Js.InvokeVoidAsync("notifDev", "Berhasil Update", "success", 3000);
                    await InvokeAsync(StateHasChanged);
                }
                catch (Exception ex)
                {
                    await Js.InvokeVoidAsync("notifDev", ex.Message, "error", 3000);
                }
            }
        }
        
    }
}
