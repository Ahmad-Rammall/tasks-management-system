using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TaskManagementSystem.Models.DTOs.ProjectDTOs;
using TaskManagementSystem.Models.DTOs.TaskDTOs;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Services
{
    public class TaskService : ITaskService
    {
        private readonly HttpClient _httpClient;
        public TaskService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<TaskDTO>> GetProjectTasks(int projectId)
        {
            try
            {
                var response = await _httpClient.GetAsync
                    ($"/api/Task/{projectId}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        return Enumerable.Empty<TaskDTO>();
                    }

                    var jsonString = await response.Content.ReadAsStringAsync();
                    var tasks = JsonSerializer.Deserialize<IEnumerable<TaskDTO>>(jsonString);
                    return tasks;
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

        public async Task<TaskRequestDTO> SendRequest(int taskid)
        {
            throw new NotImplementedException();
        }
    }
}
