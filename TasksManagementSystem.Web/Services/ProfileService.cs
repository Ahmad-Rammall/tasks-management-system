using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TaskManagementSystem.Models.DTOs.ProjectDTOs;
using TaskManagementSystem.Models.DTOs.TaskDTOs;
using TaskManagementSystem.Models.DTOs.UserDTOs;
using TasksManagementSystem.Web.Helpers;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Services
{
    public class ProfileService : IProfileService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public ProfileService(HttpClient httpClient, IJSRuntime jSRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jSRuntime;
        }
        public async Task<UserDTO> AddEmployee(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                string token = await LocalStorageManager.GetFromLocalStorage(_jsRuntime, "jwtToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PostAsJsonAsync<UserRegisterDTO>($"api/Profile", userRegisterDTO);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        return default(UserDTO);

                    return await response.Content.ReadFromJsonAsync<UserDTO>();
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

        public async Task<UserDTO> DeleteUser(int userId)
        {
            try
            {
                string token = await LocalStorageManager.GetFromLocalStorage(_jsRuntime, "jwtToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.DeleteAsync($"api/Profile/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        return default(UserDTO);

                    return await response.Content.ReadFromJsonAsync<UserDTO>();
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

        public async Task<IEnumerable<UserDTO>> GetAllEmployees()
        {
            try
            {
                string jwtToken = await LocalStorageManager.GetFromLocalStorage(_jsRuntime, "jwtToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                var profiles = await _httpClient.GetFromJsonAsync<IEnumerable<UserDTO>>($"api/Profile");
                return profiles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserDTO> UpdateEmployee(int employeeId, UserUpdateDTO userUpdateDTO)
        {
            try
            {
                string token = await LocalStorageManager.GetFromLocalStorage(_jsRuntime, "jwtToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PutAsJsonAsync<UserUpdateDTO>($"api/Profile/{employeeId}", userUpdateDTO);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        return default(UserDTO);

                    return await response.Content.ReadFromJsonAsync<UserDTO>();
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

        public async Task<UserDTO> UpdateEmployeeWP(int employeeId, UserUpdateWithoutPassDTO userUpdateDTO)
        {
            try
            {
                string token = await LocalStorageManager.GetFromLocalStorage(_jsRuntime, "jwtToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PutAsJsonAsync<UserUpdateWithoutPassDTO>($"api/Profile/updateWP/{employeeId}", userUpdateDTO);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        return default(UserDTO);

                    return await response.Content.ReadFromJsonAsync<UserDTO>();
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
