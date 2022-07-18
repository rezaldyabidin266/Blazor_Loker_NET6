using BlazorLoker2022.Resource.Loker;
using BlazorLoker2022.Resource.Pelamar.Login;
using BlazorLoker2022.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLoker2022.Pages.Loker
{
    public class PelamarLokerBase : ComponentBase
    {
        [Inject]
        protected ServicePelamarLoker servicePelamarLoker { get; set; }
 
        [Inject]
        protected NavigationManager navigationManager { get; set; }

        [Inject]
        protected IJSRuntime? Js { get; set; }

        [Inject]
        protected Blazored.LocalStorage.ILocalStorageService LocalStorage { get; set; }

        protected List<PelamarLokerOpen> lokerOpen = new List<PelamarLokerOpen>();
        protected List<byte[]> byteImgList = new List<byte[]>();
        protected List<string> baseImgList = new List<string>();
        protected string? ilustrasiApi;
        protected int idKalimat;
        protected Dictionary<int, string> imageIlustrasi = new Dictionary<int, string>();
        protected List<PelamarKalimatMotavasi> listMotivasi = new List<PelamarKalimatMotavasi>();
        protected string? kalimatMotivasi;
        protected string? gambarMotivasi;
        protected bool spinning = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
  
        }
        protected override void OnInitialized()
        {

        }
        protected override async Task OnInitializedAsync()
        {
            await getLokerOpen();
            await getGambarIlustrasiList();
            await GetListKalimatMotivasi();
            await GetGambarMotivasi();
        }

        protected async Task getLokerOpen()
        {
            try
            {
                //lokerOpen = await servicePelamarLoker.getLokeropen();
                spinning = true;
                lokerOpen = await Task.Run(() => servicePelamarLoker.getLokeropen());
                spinning = false;

                await Js.InvokeVoidAsync("console.log", lokerOpen);
            }
            catch (Exception ex)
            {
                Js.InvokeVoidAsync("console.log", ex.Message);
            }

        }
        
        protected async Task getGambarIlustrasiList()
        {
            try
            {
                foreach (var item in lokerOpen.OrderBy(x => x.idLoker))
                {
                    byte[] byteImg = await servicePelamarLoker.GetGambarIlustrasiSatuList(item.idLoker);
                    var base64 = Convert.ToBase64String(byteImg);
                    ilustrasiApi = "data:image/png;base64," + base64;
                    imageIlustrasi.Add(item.idLoker, ilustrasiApi);
                }
            }
            catch (Exception ex)
            {

                await Js.InvokeVoidAsync("console.log", ex.Message);
            }
        }

        protected async Task GetListKalimatMotivasi()
        {
            try
            {
                try
                {
                    listMotivasi = await servicePelamarLoker.pelamarMotivasi();
                    var lastId = listMotivasi.LastOrDefault();
                    Random rnd = new Random();
                    int idRandom = rnd.Next(0, lastId.id);
                    foreach (var x in listMotivasi)
                    {
                        if (x.id == idRandom)
                        {
                            kalimatMotivasi = x.kalimat;
                        }

                    }
                }
                catch (Exception ex)
                {
     
                    await Js.InvokeVoidAsync("notifDev", ex.Message, "error", 3000);
                }
            }
            catch (Exception ex)
            {
                await Js.InvokeVoidAsync("notifDev", ex.Message, "error", 3000);
            }
        }

        protected async Task refreshMotivasi()
        {
            try
            {
                try
                {
                    listMotivasi = await servicePelamarLoker.pelamarMotivasi();
                    var lastId = listMotivasi.LastOrDefault();
                    Random rnd = new Random();
                    int idRandom = rnd.Next(0, lastId.id);
                    foreach (var x in listMotivasi)
                    {
                        if (x.id == idRandom)
                        {
                            kalimatMotivasi = x.kalimat;
                        }

                    }
                    byte[] fotoByte = await servicePelamarLoker.getGambarMotivasi();
                    var foto = Convert.ToBase64String(fotoByte);
                    gambarMotivasi = "data:image/png;base64," + foto;
                }
                catch (Exception ex)
                {

                    await Js.InvokeVoidAsync("notifDev", ex.Message, "error", 3000);
                }
            }
            catch (Exception ex)
            {
                await Js.InvokeVoidAsync("notifDev", ex.Message, "error", 3000);
            }
        }

        protected async Task GetGambarMotivasi()
        {
            try
            {
                byte[] fotoByte = await servicePelamarLoker.getGambarMotivasi();
                var foto = Convert.ToBase64String(fotoByte);
                gambarMotivasi = "data:image/png;base64," + foto;
            }
            catch (Exception ex)
            {
                await Js.InvokeVoidAsync("notifDev", ex.Message, "error", 3000);
            }
        }

        protected void goLokerDetail(int lokerId)
        {
            navigationManager.NavigateTo($"lokerDetail/{lokerId}");
        }

        protected async void shareLink(int idLoker)
        {
            var link = $"https://loker.esbrasilonline.com/lokerDetail/{idLoker}";
            var msg = await Js.InvokeAsync<string>("copyText", link);
            await Js.InvokeVoidAsync("notifDev", msg, "success", 3000);
        }
    }
}
