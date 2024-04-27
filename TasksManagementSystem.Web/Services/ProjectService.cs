using System.Net.Http.Headers;
using System.Net;
using System.Text.Json;
using TaskManagementSystem.Models.DTOs.ProjectDTOs;
using TaskManagementSystem.Models.DTOs.TaskDTOs;
using TasksManagementSystem.Web.Services.Interfaces;
using Microsoft.JSInterop;
using TasksManagementSystem.Web.Helpers;
using System.Reflection;
using System.Net.Http.Json;

namespace TasksManagementSystem.Web.Services
{
    public class ProjectService : IProjectService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public ProjectService(HttpClient httpClient, IJSRuntime jSRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jSRuntime;
        }
        public async Task<IEnumerable<ProjectDTO>> GetEmployeeProjects(int employeeId)
        {
            try
            {
                string jwtToken = await LocalStorageManager.GetFromLocalStorage(_jsRuntime, "jwtToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                var projects = await _httpClient.GetFromJsonAsync<IEnumerable<ProjectDTO>>($"api/Project/{employeeId}");
                return projects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<ProjectDTO>> GetAllProjects()
        {
            try
            {
                string jwtToken = await LocalStorageManager.GetFromLocalStorage(_jsRuntime, "jwtToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                var projects = await _httpClient.GetFromJsonAsync<IEnumerable<ProjectDTO>>($"api/Project");
                return projects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProjectDTO> DeleteProject(int projectId)
        {
            try
            {
                string token = await LocalStorageManager.GetFromLocalStorage(_jsRuntime, "jwtToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.DeleteAsync($"api/Project/{projectId}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        return default(ProjectDTO);

                    return await response.Content.ReadFromJsonAsync<ProjectDTO>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status : {response.StatusCode} - Message : {message}");
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
