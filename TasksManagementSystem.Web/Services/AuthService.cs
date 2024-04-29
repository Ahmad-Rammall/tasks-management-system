using Microsoft.JSInterop;
using System.Net.Http.Json;
using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TasksManagementSystem.Web.Helpers;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jSRuntime;
        public AuthService(HttpClient httpClient, IJSRuntime jSRuntime)
        {
            _httpClient = httpClient;
            _jSRuntime = jSRuntime;
        }

        public async Task<bool> IsUserAdmin(string token)
        {
            string jwtToken = await LocalStorageManager.GetFromLocalStorage(_jSRuntime, "jwtToken");

            var response = await _httpClient.PostAsJsonAsync<string>
                    ("/api/Auth", token);
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return false;
                }

                return await response.Content.ReadFromJsonAsync<bool>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http Status : {response.StatusCode} - Message : {message}");
            }
        }

        public async Task<LoginResponseDTO> LoginUser(UserLoginDTO userLoginDTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<UserLoginDTO>
                    ("/api/Auth/login", userLoginDTO);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        return default(LoginResponseDTO);
                    }

                    return await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status : {response.StatusCode} - Message : {message}");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserRegisterDTO> RegisterUser(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<UserRegisterDTO>
                    ("/api/Auth/register", userRegisterDTO);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        return default(UserRegisterDTO);
                    }

                    return await response.Content.ReadFromJsonAsync<UserRegisterDTO>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status : {response.StatusCode} - Message : {message}");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
