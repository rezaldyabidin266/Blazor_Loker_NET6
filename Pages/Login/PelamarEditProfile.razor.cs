using BlazorLoker2022.Resource.Login;
using BlazorLoker2022.Resource.Pelamar.Login;
using BlazorLoker2022.Resource.Quiz;
using BlazorLoker2022.Service;
using BlazorLoker2022.Service.Pelamar;
using DevExpress.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace BlazorLoker2022.Pages.Login
{
    public class PelamarEditProfileBase : ComponentBase
    {

        [Inject]
        protected ServicePelamarLogin servicePelamarLogin { get; set; }
        [Inject]
        protected ServicePelamarBiodata servicePelamarBiodata { get; set; }

        [Inject]
        protected NavigationManager navigationManager { get; set; }

        [Inject]
        protected ServicePelamarQuiz servicePelamarQuiz { get; set; }

        [Inject]
        protected IJSRuntime? Js { get; set; }

        [Inject]
        protected Blazored.LocalStorage.ILocalStorageService LocalStorage { get; set; }
        protected PelamarGetClass getPelamarData = new PelamarGetClass();
        protected PelamarGetClass updatePelamar = new PelamarGetClass();
        protected List<PelamarQuizHistoryApply> historyApply = new List<PelamarQuizHistoryApply>();
        public EditContext? updatePelamarContext { get; set; }
        protected string? foto;
        protected string? cv;
        protected bool UploadVisible { get; set; } = false;
        protected string? urlFoto { get; set; }
        protected string? urlCv { get; set; }
        protected string? token;

        protected string? passwordLama;
        protected string? passwordBaru;
        protected string? passwordConfirm;
        protected string? umur;
        protected int ActiveTabIndex { get; set; } = 0;
        protected Boolean spinner { get; set; } = false;
        protected Boolean spinnerCv { get; set; } = false;        
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
            await dowloandFoto();
            await dowloandCv();
            token = await LocalStorage.GetItemAsync<string>("token");
            urlFoto = servicePelamarBiodata.uploadFoto();
            urlCv = servicePelamarBiodata.uploadCv();
            await getHistoryApply();
        }
        protected async Task getHistoryApply()
        {
            try
            {
                historyApply = await servicePelamarQuiz.getHistoryApply();
            }
            catch (Exception ex)
            {
                Js.InvokeVoidAsync("console.log", ex.Message);
            }
        }
        protected async Task getPelamar()
        {
            try
            {
                getPelamarData = await servicePelamarLogin.getPelamar();
                umur = HitungUmur(getPelamarData.tglLahir);
            }
            catch (Exception ex)
            {
                Js.InvokeVoidAsync("console.log", ex.Message);
            }
        }

        public string HitungUmur(DateTime tanggal)
        {
            int y, m, d;

            d = DateTime.Now.Day - tanggal.Day;
            m = DateTime.Now.Month - tanggal.Month;
            y = DateTime.Now.Year - tanggal.Year;

            if (Math.Sign(d) == -1)
            {
                d = 30 - Math.Abs(d);
                m -= 1;
            }

            if (Math.Sign(m) == -1)
            {
                m = 12 - Math.Abs(m);
                y -= 1;
            }

            return $"Umur {y} Tahun";
        }


        protected async Task dowloandFoto()
        {
            try
            {
                byte[] byteImg = await servicePelamarBiodata.downloadFoto();
                var base64 = Convert.ToBase64String(byteImg);
                foto = "data:image/png;base64," + base64;
            }
            catch (Exception ex)
            {
                await Js.InvokeVoidAsync("console.log", ex.Message);
                foto = "image/desktop/imageError.png";
            }
        }
        protected async Task dowloandCv()
        {
            try
            {
                byte[] byteImg = await servicePelamarBiodata.downloadCv();
                var base64 = Convert.ToBase64String(byteImg);
                cv = "data:application/pdf;base64," + base64;
            }
            catch (Exception ex)
            {
                await Js.InvokeVoidAsync("console.log", ex.Message);
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

    
        protected void SelectedFilesChangedAwal(IEnumerable<UploadFileInfo> files)
        {

            UploadVisible = files.ToList().Count > 0;
            InvokeAsync(StateHasChanged);
        }
        
        protected async void progressUploadFoto(FileUploadEventArgs e)
        {
            var progress = e.FileInfo.LoadedBytes / e.FileInfo.Size * 100;
            spinner = true;
            await InvokeAsync(StateHasChanged);
        }

        protected async void UploadSuksesFoto(FileUploadEventArgs e)
        {
            await Js.InvokeVoidAsync("HideProgressPanel");
            spinner = false;
  
            var message = "File " + e.FileInfo.Name + " Berhasil Di Upload";
            await Js.InvokeVoidAsync("notifDev", message, "success", 3000);
            await dowloandFoto();
            await InvokeAsync(StateHasChanged);
        }
        protected async void OnUploadStartedFoto(FileUploadEventArgs e)
        {
            await Js.InvokeVoidAsync("HideProgressPanel");
        }

        protected async void SettingHeaderCv(FileUploadStartEventArgs args)
        {

            args.RequestHeaders.Add("Authorization", $"Bearer {token}");
        }
        protected async void progressUploadCv(FileUploadEventArgs e)
        {
            var progress = e.FileInfo.LoadedBytes / e.FileInfo.Size * 100;
            spinnerCv = true;
            await InvokeAsync(StateHasChanged);
        }
        protected async void UploadSuksesCv(FileUploadEventArgs e)
        {
            await Js.InvokeVoidAsync("HideProgressPanel");
            spinnerCv = false;
            
            var message = "File " + e.FileInfo.Name + " Berhasil Di Upload";
            await Js.InvokeVoidAsync("notifDev", message, "success", 3000);
            await InvokeAsync(StateHasChanged);
        }
        protected async void OnUploadStartedCv(FileUploadEventArgs e)
        {
            await Js.InvokeVoidAsync("HideProgressPanel");
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
        protected async Task kirimUbahPassword()
        {
            if (passwordBaru == passwordConfirm)
            {
                try
                {
                    var ubahPw = new PelamarUbahPassword
                    {
                        passwordLama = passwordLama,
                        passwordBaru = passwordBaru,
                    };
                    string msg = await servicePelamarLogin.ubahPassword(ubahPw);
                    await Js.InvokeVoidAsync("notifDev", msg, "success", 3000);
                }
                catch (Exception ex)
                {
                    await Js.InvokeVoidAsync("notifDev", ex.Message, "error", 3000);
                }
            }
            else
            {
                await Js.InvokeVoidAsync("notifDev", "Kata sandi unverifikasi", "error", 3000);
            }
        }
    }
}
