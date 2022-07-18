using BlazorLoker2022.Resource.Biodata;
using BlazorLoker2022.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace BlazorLoker2022.Pages.Login
{
    public class PelamarTambahHistoryKerjaBase : ComponentBase
    {
        [Inject]
        protected ServicePelamarLoker servicePelamarLoker { get; set; }
        [Inject]
        protected ServicePelamarBiodata servicePelamarBiodata { get; set; }

        [Inject]
        protected NavigationManager navigationManager { get; set; }

        [Inject]
        protected IJSRuntime? Js { get; set; }

        [Inject]
        protected Blazored.LocalStorage.ILocalStorageService LocalStorage { get; set; }
        public EditContext? HistoryKerjaContext { get; set; }
        protected PelamarAddHistoryKerja pelamarAddHistoryKerjaClass = new PelamarAddHistoryKerja();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

        }
        protected override void OnInitialized()
        {
            HistoryKerjaContext = new EditContext(pelamarAddHistoryKerjaClass);
            pelamarAddHistoryKerjaClass.tglAwal = DateTime.Now;
            pelamarAddHistoryKerjaClass.tglAkhir = DateTime.Now;
        }
        protected override async Task OnInitializedAsync()
        {

        }
        protected async void kirimHistory()
        {
            if (HistoryKerjaContext.Validate())
            {
                try
                {
                    var data = await servicePelamarBiodata.addHistoryKerja(pelamarAddHistoryKerjaClass);
                    await Js.InvokeVoidAsync("notifDev", "Berhasil Tambah Pengalaman", "success", 3000);
                    navigationManager.NavigateTo("PelamarEdit");
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
