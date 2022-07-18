using BlazorLoker2022.Resource.Loker;
using BlazorLoker2022.Resource.Quiz;
using BlazorLoker2022.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLoker2022.Pages.Quiz
{
    public class PelamarQuizEditBase : ComponentBase
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
        protected List<FormIsianJawabanDetailResponse> finals = new List<FormIsianJawabanDetailResponse>();
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
            var newObject = new FormIsianJawabanDetailResponse()
            {
                keterangan = "haloooo",
            };
            finals.Add(newObject);
        }

        protected async Task getInfoLoker()
        {
            try
            {
                infoLoker = await servicePelamarLoker.getInfoLoker(idLoker);                
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
                await Js.InvokeVoidAsync("console.log", quiz);
            }
            catch (Exception ex)
            {
                Js.InvokeVoidAsync("console.log", ex.Message);
            }
        }
        protected async void kirimEdit()
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
                await Js.InvokeVoidAsync("console.log", jawabanApply);
                var message = await servicePelamarQuiz.kirimJawabanApply(quizApply.headerId, jawabanApply);
                await Js.InvokeVoidAsync("notifDev", "Berhasil Edit Jawaban", "success", 3000);

            }
            catch (Exception ex)
            {
                await Js.InvokeVoidAsync("notifDev", ex.Message, "error", 3000);
            }
        }
    }
}
