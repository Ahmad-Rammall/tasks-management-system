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
        public Task<UserDTO> AddEmployee(UserRegisterDTO userRegisterDTO)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> DeleteUser(int userId)
        {
            throw new NotImplementedException();
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

        public Task<UserDTO> UpdateEmployee(int employeeId, UserUpdateDTO userUpdateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
