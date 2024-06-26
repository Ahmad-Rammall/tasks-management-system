﻿using Microsoft.JSInterop;
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

        public async Task<TaskRequestDTO> AcceptRequest(int requestId)
        {
            try
            {
                string token = await LocalStorageManager.GetFromLocalStorage(_jsRuntime, "jwtToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.DeleteAsync($"api/Task/acceptRequest/{requestId}");
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
                throw ex;
            }
        }

        public async Task<TaskDTO> AddTaskToEmployee(TaskToAddDTO taskToAddDTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<TaskToAddDTO>("api/Task", taskToAddDTO);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        return default(TaskDTO);

                    return await response.Content.ReadFromJsonAsync<TaskDTO>();
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

        public async Task<IEnumerable<TaskRequestDTO>> GetAllRequests()
        {
            try
            {
                string jwtToken = await LocalStorageManager.GetFromLocalStorage(_jsRuntime, "jwtToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                var requests = await _httpClient.GetFromJsonAsync<IEnumerable<TaskRequestDTO>>($"api/Task/requests");
                return requests;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<TaskDTO>> GetEmployeeTasks(int projectId, int employeeId)
        {
            try
            {
                string jwtToken = await LocalStorageManager.GetFromLocalStorage(_jsRuntime, "jwtToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                var tasks = await _httpClient.GetFromJsonAsync<IEnumerable<TaskDTO>>($"api/Task/{projectId}/{employeeId}");
                return tasks;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        public async Task<TaskRequestDTO> RejectRequest(int requestId)
        {
            try
            {
                string token = await LocalStorageManager.GetFromLocalStorage(_jsRuntime, "jwtToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PutAsJsonAsync($"api/task/rejectRequest/", requestId);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
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
