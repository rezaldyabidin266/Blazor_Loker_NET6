using BlazorLoker2022.Resource.Login;
using BlazorLoker2022.Resource.Quiz;
using BlazorLoker2022.Service;
using BlazorLoker2022.Service.Pelamar;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLoker2022.Pages.Loker
{
    public class PelamarHistoryApplyBase: ComponentBase
    {
        [Inject]
        protected ServicePelamarQuiz servicePelamarQuiz { get; set; }
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

        protected List<PelamarQuizHistoryApply> historyApply = new List<PelamarQuizHistoryApply>();
        protected PelamarGetClass getPelamarData = new PelamarGetClass();
        protected string? foto;
        protected string? cv;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            //await Js.InvokeVoidAsync("mouse_login");
        }
        protected override void OnInitialized()
        {

        }
        protected override async Task OnInitializedAsync()
        {
            var unauthorized = await LocalStorage.GetItemAsync<string>("statusCode");
            if (unauthorized == "Unauthorized")
            {
                navigationManager.NavigateTo("/pelamarLogin");
            }

            await getHistoryApply();
            await getPelamar();
            await dowloandFoto();
            await dowloandCv();
        }
        protected async Task getHistoryApply()
        {
            try
            {
                historyApply = await servicePelamarQuiz.getHistoryApply();
                await Js.InvokeVoidAsync("console.log", historyApply);
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
                await Js.InvokeVoidAsync("console.log", getPelamarData);
            }
            catch (Exception ex)
            {
                Js.InvokeVoidAsync("console.log", ex.Message);
            }
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
                foto = "image/desktop/imageError.png";
                await Js.InvokeVoidAsync("console.log", ex.Message);
            }
        }
        protected async Task goEdit(int idLoker)
        {
            navigationManager.NavigateTo($"/QuizEdit/{idLoker}");
        }
        protected async Task dowloandCv()
        {
            try
            {
                byte[] byteImg = await servicePelamarBiodata.downloadCv();
                var base64 = Convert.ToBase64String(byteImg);
                cv = "data:application/pdf;base64," + base64;
                await Js.InvokeVoidAsync("console.log", cv);
            }
            catch (Exception ex)
            {

                await Js.InvokeVoidAsync("console.log", ex.Message);
            }
        }

    }
}
