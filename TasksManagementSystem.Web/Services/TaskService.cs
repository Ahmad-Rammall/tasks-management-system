using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TaskManagementSystem.Models.DTOs.ProjectDTOs;
using TaskManagementSystem.Models.DTOs.TaskDTOs;
using TasksManagementSystem.Web.Helpers;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Services
{
    public class TaskService : ITaskService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        public TaskService(HttpClient httpClient, IJSRuntime jSRuntime)
        {
            _jsRuntime = jSRuntime;
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<TaskDTO>> GetProjectTasks(int projectId)
        {
            try
            {
                string jwtToken = await LocalStorageManager.GetFromLocalStorage(_jsRuntime, "jwtToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                var tasks = await _httpClient.GetFromJsonAsync<IEnumerable<TaskDTO>>($"api/Task/{projectId}");
                return tasks;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TaskRequestDTO> SendRequest(int taskid)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<int>("api/Task/sendRequest", taskid);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        return default(TaskRequestDTO);

                    return await response.Content.ReadFromJsonAsync<TaskRequestDTO>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status : {response.StatusCode} - Message : {message}");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
