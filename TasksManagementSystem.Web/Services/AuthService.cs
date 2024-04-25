using System.Net.Http.Json;
using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
