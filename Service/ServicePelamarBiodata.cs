using BlazorLoker2022.Resource.Biodata;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace BlazorLoker2022.Service
{
    public class ServicePelamarBiodata
    {
        private readonly HttpClient _httpClient;
        private const string Controller = "Biodata/";

        public ServicePelamarBiodata(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public string uploadFoto()
        {
            return _httpClient.BaseAddress.AbsoluteUri + Controller + "upload-foto";
        }
        public string uploadCv()
        {
            return _httpClient.BaseAddress.AbsoluteUri + Controller + "upload-cv";
        }
        public async Task<List<PelamarHistoryKerja>> addHistoryKerja(PelamarAddHistoryKerja addHistoryKerja)
        {
            var respond = await _httpClient.PostAsJsonAsync(Controller + $"add-history-kerja", addHistoryKerja);
            return respond.IsSuccessStatusCode
              ? JsonConvert.DeserializeObject<List<PelamarHistoryKerja>>(respond.Content.ReadAsStringAsync().Result)
              : throw new Exception(await respond.Content.ReadAsStringAsync());
        }
        public async Task<List<PelamarHistoryKerja>> getAllHistoryKerja()
        {
            var respond = await _httpClient.GetAsync(Controller + $"all-history-kerja");
            return respond.IsSuccessStatusCode
              ? JsonConvert.DeserializeObject<List<PelamarHistoryKerja>>(respond.Content.ReadAsStringAsync().Result)
              : throw new Exception(await respond.Content.ReadAsStringAsync());
        }
        public async Task<PelamarHistoryKerja> getHistoryKerja(int historyId)
        {
            var respond = await _httpClient.GetAsync(Controller + $"history-kerja?historyId={historyId}");
            return respond.IsSuccessStatusCode
              ? JsonConvert.DeserializeObject<PelamarHistoryKerja>(respond.Content.ReadAsStringAsync().Result)
              : throw new Exception(await respond.Content.ReadAsStringAsync());
        }
        public async Task<byte[]> downloadFoto()
        {
            var respond = await _httpClient.GetAsync(Controller + $"download-foto");
            return await respond.Content.ReadAsByteArrayAsync();
        }
        public async Task<byte[]> downloadCv()
        {
            var respond = await _httpClient.GetAsync(Controller + $"download-cv");
            return await respond.Content.ReadAsByteArrayAsync();
        }
        public async Task<string> updateHistorykerja(PelamarAddHistoryKerja addHistoryKerja)
        {

            var respond = await _httpClient.PutAsJsonAsync(Controller + $"update-history-kerja", addHistoryKerja);
            return respond.IsSuccessStatusCode
             ? await respond.Content.ReadAsStringAsync()
             : throw new Exception(await respond.Content.ReadAsStringAsync());
        }
        
        public async Task<string> deleteHistoryKerja(int historyId)
        {
            var respond = await _httpClient.DeleteAsync(Controller + $"delete-history-kerja?historyId={historyId}");
            return respond.IsSuccessStatusCode
             ? await respond.Content.ReadAsStringAsync()
             : throw new Exception(await respond.Content.ReadAsStringAsync());
        }
    }
}
