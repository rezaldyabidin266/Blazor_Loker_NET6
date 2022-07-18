using BlazorLoker2022.Resource.Biodata;
using BlazorLoker2022.Service;
using BlazorLoker2022.Service.Pelamar;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLoker2022.Pages.Component
{
    public class CardHistoryKerjaBase : ComponentBase
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
        
        protected List<PelamarHistoryKerja> pelamarHistoryKerjaList = new List<PelamarHistoryKerja>();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

        }
        protected override void OnInitialized()
        {
        }
        protected override async Task OnInitializedAsync()
        {
            await getAllHistoryKerja();
        }
        protected async Task getAllHistoryKerja()
        {
            try
            {
                pelamarHistoryKerjaList = await servicePelamarBiodata.getAllHistoryKerja();
            }
            catch (Exception ex)
            {

                await Js.InvokeVoidAsync("notifDev", ex.Message, "error", 3000);
            }
        }

        protected void goEdit(int id)
        {
            navigationManager.NavigateTo($"/PelamarEditHistory/{id}");
        }

        protected async Task deleteHistory(int historyId)
        {
            try
            {
                var msg = await servicePelamarBiodata.deleteHistoryKerja(historyId);
                await Js.InvokeVoidAsync("notifDev", msg, "success", 3000);
                pelamarHistoryKerjaList = await servicePelamarBiodata.getAllHistoryKerja();
                await InvokeAsync(StateHasChanged);
            }
            catch (Exception ex)
            {
                await Js.InvokeVoidAsync("notifDev", ex.Message, "error", 3000);
            }
        }
    }
}
