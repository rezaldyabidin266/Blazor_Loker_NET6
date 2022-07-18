using BlazorLoker2022.Resource.Biodata;
using BlazorLoker2022.Resource.Loker;
using BlazorLoker2022.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace BlazorLoker2022.Pages.Login
{
    public class PelamarEditHistoryKerjaBase : ComponentBase
    {
        [Parameter]
        public int idLoker { get; set; }

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
        protected PelamarKriteria infoLoker = new PelamarKriteria();
        protected List<string> kriteria = new List<string>();
        protected bool UploadVisible { get; set; } = false;
        protected string? urlFoto { get; set; }
        protected string? urlCv { get; set; }
        protected string? token;
        public EditContext? HistoryKerjaContext { get; set; }
        protected PelamarHistoryKerja pelamarHistoryKerjaClass = new PelamarHistoryKerja();
        protected PelamarAddHistoryKerja pelamarUpdateHistoryKerjaClass = new PelamarAddHistoryKerja();
        protected List<PelamarHistoryKerja> pelamarHistoryKerjaList = new List<PelamarHistoryKerja>();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

        }
        protected override void OnInitialized()
        {
            HistoryKerjaContext = new EditContext(pelamarHistoryKerjaClass);
        }
        protected override async Task OnInitializedAsync()
        {
            await getHistoryKerjaId();
        }
        protected async Task getHistoryKerjaId()
        {
            try
            {
                pelamarHistoryKerjaClass = await servicePelamarBiodata.getHistoryKerja(idLoker);
                await Js.InvokeVoidAsync("console.log", pelamarHistoryKerjaClass);
                pelamarUpdateHistoryKerjaClass.id = pelamarHistoryKerjaClass.id;
                pelamarUpdateHistoryKerjaClass.tempatKerja = pelamarHistoryKerjaClass.tempatKerja;
                pelamarUpdateHistoryKerjaClass.posisi = pelamarHistoryKerjaClass.posisi;
                pelamarUpdateHistoryKerjaClass.tugas = pelamarHistoryKerjaClass.tugas;
                pelamarUpdateHistoryKerjaClass.salaryTerakhir = pelamarHistoryKerjaClass.salaryTerakhir;
                pelamarUpdateHistoryKerjaClass.tglAwal = pelamarHistoryKerjaClass.tglAwal;
                pelamarUpdateHistoryKerjaClass.tglAkhir = pelamarHistoryKerjaClass.tglAkhir;
   
            }
            catch (Exception ex)
            {
                Js.InvokeVoidAsync("console.log", ex.Message);
            }
        }

        protected async void kirimEdit()
        {
            if (HistoryKerjaContext.Validate())
            {
                try
                {
                    var data = await servicePelamarBiodata.updateHistorykerja(pelamarUpdateHistoryKerjaClass);
                    navigationManager.NavigateTo("PelamarEdit");
                    await Js.InvokeVoidAsync("notifDev", "Berhasil Edit Pengalaman", "success", 3000);
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
