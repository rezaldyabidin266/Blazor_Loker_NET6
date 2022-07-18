using BlazorLoker2022.Resource.Loker;
using BlazorLoker2022.Resource.Pelamar.Login;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace BlazorLoker2022.Service
{
    public class ServicePelamarLoker
    {
        private readonly HttpClient _httpClient;
        private const string Controller = "Loker/";

        public ServicePelamarLoker(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PelamarLokerOpen>> getLokeropen()
        {
            var respond = await _httpClient.GetAsync(Controller + $"loker-open");
            return respond.IsSuccessStatusCode
              ? JsonConvert.DeserializeObject<List<PelamarLokerOpen>>(respond.Content.ReadAsStringAsync().Result)
              : throw new Exception(await respond.Content.ReadAsStringAsync());
        }
        public async Task<PelamarKriteria> getInfoLoker(int idLoker)
        {
            var respond = await _httpClient.GetAsync(Controller + $"info-loker?lokerId={idLoker}");
            return respond.IsSuccessStatusCode
              ? JsonConvert.DeserializeObject<PelamarKriteria>(respond.Content.ReadAsStringAsync().Result)
              : throw new Exception(await respond.Content.ReadAsStringAsync());
        }
        public async Task<byte[]> GetGambarIlustrasi(int lokerId)
        {
            var respond = await _httpClient.GetAsync(Controller + $"download-image-ilustrasi?lokerId={lokerId}");

            return respond.IsSuccessStatusCode
              ? await respond.Content.ReadAsByteArrayAsync()
              : throw new Exception(await respond.Content.ReadAsStringAsync());
        }
        public async Task<byte[]> GetGambarIlustrasiSatuList(int lokerId)
        {
            var respond = await _httpClient.GetAsync(Controller + $"download-image-ilustrasi?lokerId={lokerId}");
            return await respond.Content.ReadAsByteArrayAsync();
        }
        public async Task<List<byte[]>> GetGambarIlustrasiList(int lokerId)
        {
            var respond = await _httpClient.GetAsync(Controller + $"download-image-ilustrasi?lokerId={lokerId}");
            var listGambar = new List<byte[]>();
            listGambar.Add(await respond.Content.ReadAsByteArrayAsync());

            return listGambar;
        }
        public async Task<byte[]> GetGambarBackground(int lokerId)
        {
            var respond = await _httpClient.GetAsync(Controller + $"download-image-background?lokerId={lokerId}");

            return respond.IsSuccessStatusCode
              ? await respond.Content.ReadAsByteArrayAsync()
              : throw new Exception(await respond.Content.ReadAsStringAsync());
        }

        public async Task<List<PelamarKalimatMotavasi>> pelamarMotivasi()
        {
            return await _httpClient.GetFromJsonAsync<List<PelamarKalimatMotavasi>>($"Login/list-kalimat-motivasi");
        }
        public async Task<byte[]> getGambarMotivasi()
        {
            return await _httpClient.GetByteArrayAsync($"Login/gambar-motivasi");
        }


    }
}
