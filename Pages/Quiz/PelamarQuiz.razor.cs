using BlazorLoker2022.Resource.Loker;
using BlazorLoker2022.Resource.Quiz;
using BlazorLoker2022.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace BlazorLoker2022.Pages.Quiz
{
    public class PelamarQuizBase : ComponentBase
    {
        [Parameter]
        public int idLoker { get; set; }

        [Inject]
        protected ServicePelamarQuiz servicePelamarQuiz { get; set; }
        [Inject]
        protected ServicePelamarLoker servicePelamarLoker { get; set; }

        [Inject]
        protected NavigationManager navigationManager { get; set; }

        [Inject]
        protected IJSRuntime? Js { get; set; }

        [Inject]
        protected Blazored.LocalStorage.ILocalStorageService LocalStorage { get; set; }

        protected PelamarQuizApply quizApply = new PelamarQuizApply();
        protected List<FormQuizz> quiz = new List<FormQuizz>();
        protected PelamarKriteria infoLoker = new PelamarKriteria();
        protected List<string> kriteria = new List<string>();
        protected List<FormIsianJawabanDetailResponse> jawabanDetailList = new List<FormIsianJawabanDetailResponse>();
        protected FormIsianJawabanDetailResponse jawabanDetail = new FormIsianJawabanDetailResponse();
        protected List<PelamarQuizJawaban> jawabanApply = new List<PelamarQuizJawaban>();
        protected PelamarQuizJawaban? jawab;
        protected DateTime DateToday = DateTime.Today;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

        }
        protected override void OnInitialized()
        {

        }
        protected override async Task OnInitializedAsync()
        {
            await getInfoLoker();
            await getQuiz();
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

        protected async Task getQuiz()
        {
            try
            {
                quizApply = await servicePelamarQuiz.getQuizApply(idLoker);
                quiz = quizApply.formQuizz;
            }
            catch (Exception ex)
            {
                Js.InvokeVoidAsync("console.log", ex.Message);
            }
        }

        protected async void kirimApply()
        {
            try
            {
                foreach (var item in quiz)
                {
                    jawab = new PelamarQuizJawaban
                    {
                        detailJawabanId = item.detailJawabanId,
                        jawaban = (string)item.jawaban
                    };

                    jawabanApply.Add(jawab);

                }
         
                var message = await servicePelamarQuiz.kirimJawabanApply(quizApply.headerId, jawabanApply);
                await Js.InvokeVoidAsync("notifDev", "Berhasil Kirim Jawaban", "success", 3000);
                navigationManager.NavigateTo($"/Upload/{idLoker}");

            }
            catch (Exception ex)
            {
                await Js.InvokeVoidAsync("notifDev", ex.Message, "error", 3000);
            }
        }
        
        protected async void goBack()
        {
            await Js.InvokeVoidAsync("history.back");
        }
    }
}


