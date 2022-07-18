using BlazorLoker2022.Resource.Pelamar.Login;
using System.Net.Http.Json;

namespace BlazorLoker2022.Service.Pelamar
{
    public class ServicePelamarTest
    {
        private readonly HttpClient _httpClient;
        private const string Controller = "Login/";

        public ServicePelamarTest(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PelamarKalimatMotavasi>> pelamarMotivasi()
        {
            return await _httpClient.GetFromJsonAsync<List<PelamarKalimatMotavasi>>(Controller + "list-kalimat-motivasi");
        }
    }
}
