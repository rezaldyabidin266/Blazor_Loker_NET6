using Blazored.LocalStorage;
using BlazorLoker2022.Resource.Login;
using BlazorLoker2022.Resource.Pelamar.Login;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace BlazorLoker2022.Service.Pelamar
{
    public class ServicePelamarLogin
    {
        private readonly HttpClient _httpClient;
        private const string Controller = "Login/";
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorage;
        public ServicePelamarLogin(HttpClient httpClient, IJSRuntime jsruntime, NavigationManager navigationManager, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _jsRuntime = jsruntime;
            _navigationManager = navigationManager;
            _localStorage = localStorage;
        }
        public async Task<List<PelamarKalimatMotavasi>> pelamarMotivasi()
        {
            return await _httpClient.GetFromJsonAsync<List<PelamarKalimatMotavasi>>(Controller + "list-kalimat-motivasi");
        }

        public async Task<string> loginPelamar(PelamarLoginClass pelamarLogin)
        {
            var respond = await _httpClient.PostAsJsonAsync(Controller, pelamarLogin);
            if (respond.IsSuccessStatusCode)
            {
                return await respond.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception(await respond.Content.ReadAsStringAsync());
            }
        }
        public async Task<PelamarRegisterClass> registerPelamar(PelamarRegisterClass pelamarRegister)
        {
            var respond = await _httpClient.PostAsJsonAsync(Controller + $"register-pelamar", pelamarRegister);
            return respond.IsSuccessStatusCode
              ? JsonConvert.DeserializeObject<PelamarRegisterClass>(respond.Content.ReadAsStringAsync().Result)
              : throw new Exception(await respond.Content.ReadAsStringAsync());
        }
        public async Task<PelamarGetClass> getPelamar()
        {
            var respond = await _httpClient.GetAsync(Controller + $"get-pelamar");
            if (respond.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsStringAsync("statusCode", respond.StatusCode.ToString());
                return JsonConvert.DeserializeObject<PelamarGetClass>(respond.Content.ReadAsStringAsync().Result);
            }
            else
            {
                if (respond.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await _localStorage.SetItemAsStringAsync("statusCode", respond.StatusCode.ToString());
                    throw new Exception(await respond.Content.ReadAsStringAsync());
                  
                }
                else
                {
                    await _localStorage.SetItemAsStringAsync("statusCode", respond.StatusCode.ToString());
                    throw new Exception(await respond.Content.ReadAsStringAsync());
                }
            }
        }
        public async Task<PelamarGetClass> updatePelamar(PelamarGetClass updatePelamar)
        {
            var respond = await _httpClient.PutAsJsonAsync(Controller + $"update-pelamar",updatePelamar);
            return respond.IsSuccessStatusCode
             ? JsonConvert.DeserializeObject<PelamarGetClass>(respond.Content.ReadAsStringAsync().Result)
             : throw new Exception(await respond.Content.ReadAsStringAsync());
        }

        public async Task<string> resetPassword(string otp)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("otp", otp);
            var respond = await _httpClient.PutAsJsonAsync(Controller + $"reset-password", "");
            return respond.IsSuccessStatusCode
             ? await respond.Content.ReadAsStringAsync()
             : throw new Exception(await respond.Content.ReadAsStringAsync());
        }
        public async Task<string> requestOtpResetPassword(string email)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("email", email);
            var respond = await _httpClient.GetAsync(Controller + $"request-otp-reset-password");
            return respond.IsSuccessStatusCode
              ? await respond.Content.ReadAsStringAsync()
              : throw new Exception(await respond.Content.ReadAsStringAsync());
        }
        public async Task<string> ubahPassword(PelamarUbahPassword ubahPassword)
        {
            var respond = await _httpClient.PostAsJsonAsync(Controller + $"ubah-password", ubahPassword);
            return respond.IsSuccessStatusCode
              ? await respond.Content.ReadAsStringAsync()
              : throw new Exception(await respond.Content.ReadAsStringAsync());        
        }
        public async Task<string> buatPasswordBaru(PelamarLoginClass sandiBaru)
        {
            var respond = await _httpClient.PostAsJsonAsync(Controller + $"buat-password-baru", sandiBaru);
            return respond.IsSuccessStatusCode
              ? await respond.Content.ReadAsStringAsync()
              : throw new Exception(await respond.Content.ReadAsStringAsync());
        }
    }
}
