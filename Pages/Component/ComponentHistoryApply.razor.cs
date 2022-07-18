using BlazorLoker2022.Resource.Quiz;
using BlazorLoker2022.Service;
using BlazorLoker2022.Service.Pelamar;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLoker2022.Pages.Component
{
    public class ComponentHistoryApplyBase : ComponentBase
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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            //await Js.InvokeVoidAsync("mouse_login");
        }
        protected override void OnInitialized()
        {

        }
        protected override async Task OnInitializedAsync()
        {
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
        protected async Task goEdit(int idLoker)
        {
            navigationManager.NavigateTo($"/QuizEdit/{idLoker}");
        }        
    }
}
