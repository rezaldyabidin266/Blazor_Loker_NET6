using BlazorLoker2022.Resource.Biodata;
using BlazorLoker2022.Resource.Loker;
using BlazorLoker2022.Service;
using DevExpress.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace BlazorLoker2022.Pages.Biodata
{
    public class PelamarUploadBase : ComponentBase
    {
        [Parameter]
        public int idLoker { get; set; }

        [Inject]
        protected ServicePelamarLoker servicePelamarLoker { get; set; }
        [Inject]
        protected ServicePelamarBiodata servicePelamaBiodata { get; set; }

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
        protected PelamarAddHistoryKerja pelamarAddHistoryKerjaClass = new PelamarAddHistoryKerja();
        protected List<PelamarHistoryKerja> pelamarHistoryKerjaList = new List<PelamarHistoryKerja>();

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
            await getInfoLoker();
            await getAllHistoryKerja();
            token = await LocalStorage.GetItemAsync<string>("token");
            urlFoto = servicePelamaBiodata.uploadFoto();
            urlCv = servicePelamaBiodata.uploadCv();
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
        protected string Url(string url)
        {
            return navigationManager.ToAbsoluteUri(url).AbsoluteUri;
        }
        protected async void ErrorHandling(FileUploadErrorEventArgs e)
        {

            string errorMessage = e.RequestInfo.ResponseText;
            await Js.InvokeVoidAsync("notifDev", errorMessage, "error", 3000);
            await InvokeAsync(StateHasChanged);

        }

        protected async void SettingHeaderFoto(FileUploadStartEventArgs args)
        {

            args.RequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        protected async void SettingHeaderCv(FileUploadStartEventArgs args)
        {

            args.RequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        protected void SelectedFilesChangedAwal(IEnumerable<UploadFileInfo> files)
        {

            UploadVisible = files.ToList().Count > 0;
            InvokeAsync(StateHasChanged);
        }

        protected async void UploadSuksesFoto(FileUploadEventArgs e)
        {

            var message = "File " + e.FileInfo.Name + " Berhasil Di Upload";
            await Js.InvokeVoidAsync("notifDev", message, "success", 3000);
            await InvokeAsync(StateHasChanged);
        }
        protected async void UploadSuksesCv(FileUploadEventArgs e)
        {

            var message = "File " + e.FileInfo.Name + " Berhasil Di Upload";
            await Js.InvokeVoidAsync("notifDev", message, "success", 3000);
            await InvokeAsync(StateHasChanged);
        }

        protected async void kirimHistory()
        {
            if (HistoryKerjaContext.Validate())
            {
                try
                {
                    var data = await servicePelamaBiodata.addHistoryKerja(pelamarAddHistoryKerjaClass);                    
                    await Js.InvokeVoidAsync("notifDev", "Berhasil Tambah Pengalaman", "success", 3000);
                    pelamarHistoryKerjaList = await servicePelamaBiodata.getAllHistoryKerja();
                    await InvokeAsync(StateHasChanged);

                }
                catch (Exception ex)
                {
                    await Js.InvokeVoidAsync("notifDev", ex.Message, "error", 3000);
                }
            }
        }

        protected async Task getAllHistoryKerja()
        {
            try
            {
                pelamarHistoryKerjaList = await servicePelamaBiodata.getAllHistoryKerja();
            }
            catch (Exception ex)
            {

                await Js.InvokeVoidAsync("notifDev", ex.Message, "error", 3000);
            }
        }

    }
}
