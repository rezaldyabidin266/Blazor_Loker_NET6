using BlazorLoker2022.Resource.Quiz;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace BlazorLoker2022.Service
{
    public class ServicePelamarQuiz
    {
        private readonly HttpClient _httpClient;
        private const string Controller = "Quiz/";

        public ServicePelamarQuiz(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PelamarQuizApply> getQuizApply(int idLoker)
        {
            var respond = await _httpClient.GetAsync(Controller + $"apply?lokerId={idLoker}");
            return respond.IsSuccessStatusCode
              ? JsonConvert.DeserializeObject<PelamarQuizApply>(respond.Content.ReadAsStringAsync().Result)
              : throw new Exception(await respond.Content.ReadAsStringAsync());
        }
        public async Task<List<PelamarQuizGetJawaban>> kirimJawabanApply(int headerId,List<PelamarQuizJawaban> jawabanApply)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("headerId", headerId.ToString());            
            var respond = await _httpClient.PostAsJsonAsync(Controller + $"jawaban", jawabanApply);
            return respond.IsSuccessStatusCode
              ? JsonConvert.DeserializeObject<List<PelamarQuizGetJawaban>>(respond.Content.ReadAsStringAsync().Result)
              : throw new Exception(await respond.Content.ReadAsStringAsync());
        }
        public async Task<List<PelamarQuizHistoryApply>> getHistoryApply()
        {
            var respond = await _httpClient.GetAsync(Controller + $"history-apply");
            return respond.IsSuccessStatusCode
              ? JsonConvert.DeserializeObject<List<PelamarQuizHistoryApply>>(respond.Content.ReadAsStringAsync().Result)
              : throw new Exception(await respond.Content.ReadAsStringAsync());
        }
    }
}
