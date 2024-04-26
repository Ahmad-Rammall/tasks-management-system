using System.Text.Json;
using TaskManagementSystem.Models.DTOs.ProjectDTOs;
using TaskManagementSystem.Models.DTOs.TaskDTOs;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Services
{
    public class ProjectService : IProjectService
    {
        private readonly HttpClient _httpClient;
        public ProjectService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<ProjectDTO>> GetEmployeeProjects(int employeeId)
        {
            try
            {
                var response = await _httpClient.GetAsync
                    ($"/api/Project/{employeeId}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        return Enumerable.Empty<ProjectDTO>();
                    }

                    var jsonString = await response.Content.ReadAsStringAsync();
                    var projects = JsonSerializer.Deserialize<IEnumerable<ProjectDTO>>(jsonString);
                    return projects;
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
