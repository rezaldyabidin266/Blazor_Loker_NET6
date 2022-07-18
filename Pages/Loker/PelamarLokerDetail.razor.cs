using BlazorLoker2022.Resource.Loker;
using BlazorLoker2022.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLoker2022.Pages.Loker
{
    public class PelamarLokerDetailBase : ComponentBase
    {
        [Parameter]
        public int idLoker { get; set; }

        [Inject]
        protected ServicePelamarLoker servicePelamarLoker { get; set; }

        [Inject]
        protected NavigationManager navigationManager { get; set; }

        [Inject]
        protected IJSRuntime? Js { get; set; }

        [Inject]
        protected Blazored.LocalStorage.ILocalStorageService LocalStorage { get; set; }
        protected string? backgroundApi;
        protected PelamarKriteria infoLoker = new PelamarKriteria();
        protected List<string> kriteria = new List<string>();
        protected string? token;
        protected string? ilustrasiApi;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

        }
        protected override async void OnInitialized()
        {
            
        }
        protected override async Task OnInitializedAsync()
        {
            await getInfoLoker();
            await getGambarIlustrasi();
            await getGambarBackground();

            token = await LocalStorage.GetItemAsStringAsync("token");
        }

        protected async Task getInfoLoker()
        {
            try
            {
                infoLoker = await servicePelamarLoker.getInfoLoker(idLoker);
                await Js.InvokeVoidAsync("console.log", infoLoker);
                kriteria = infoLoker.kriteria;
            }
            catch (Exception ex)
            {
                Js.InvokeVoidAsync("console.log", ex.Message);
            }
        }

        protected async Task getGambarIlustrasi()
        {
            try
            {
                byte[] byteImg = await servicePelamarLoker.GetGambarIlustrasiSatuList(idLoker);
                var base64 = Convert.ToBase64String(byteImg);
                ilustrasiApi = "data:image/png;base64," + base64;
            }
            catch (Exception ex)
            {

                await Js.InvokeVoidAsync("console.log", ex.Message);
            }
        }
        protected async Task getGambarBackground()
        {
            try
            {
                byte[] byteImg = await servicePelamarLoker.GetGambarBackground(idLoker);
                var base64 = Convert.ToBase64String(byteImg);
                backgroundApi = "data:image/png;base64," + base64;
            }
            catch (Exception ex)
            {
                await Js.InvokeVoidAsync("console.log", ex.Message);
            }
        }
        
        protected async Task goQuiz()
        {
            if (token != null) {
                navigationManager.NavigateTo($"/Quiz/{idLoker}");
            }
            else
            {
                navigationManager.NavigateTo($"/register");
                await Js.InvokeVoidAsync("notifDev", "Anda harus login terlebih dahulu" , "error", 3000);
            }
         
        }
    }
}
