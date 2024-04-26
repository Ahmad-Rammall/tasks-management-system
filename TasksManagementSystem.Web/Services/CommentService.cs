using System.Net.Http.Headers;
using System.Net.Http;
using TaskManagementSystem.Models.DTOs.CommentDTOs;
using TaskManagementSystem.Models.DTOs.ProjectDTOs;
using TasksManagementSystem.Web.Helpers;
using TasksManagementSystem.Web.Services.Interfaces;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TaskManagementSystem.Models.DTOs.TaskDTOs;

namespace TasksManagementSystem.Web.Services
{
    public class CommentService : ICommentService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jSRuntime;

        public CommentService(HttpClient httpClient, IJSRuntime jSRuntime)
        {
            _httpClient = httpClient;
            _jSRuntime = jSRuntime;
        }
        public async Task<CommentDTO> AddComment(CommentToAddDTO commentToAddDTO)
        {
            try
            {
                string jwtToken = await LocalStorageManager.GetFromLocalStorage(_jSRuntime, "jwtToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                var response = await _httpClient.PostAsJsonAsync<CommentToAddDTO>("api/Comment", commentToAddDTO);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        return default(CommentDTO);

                    return await response.Content.ReadFromJsonAsync<CommentDTO>();
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

        public async Task<IEnumerable<CommentDTO>> GetAllTaskComments(int taskId)
        {
            try
            {
                string jwtToken = await LocalStorageManager.GetFromLocalStorage(_jSRuntime, "jwtToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                var comments = await _httpClient.GetFromJsonAsync<IEnumerable<CommentDTO>>($"api/Comment/{taskId}");
                return comments;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
