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
        //public async Task<IEnumerable<ProjectDTO>> GetEmployeeProjects(int employeeId)
        //{
        //    try
        //    {
        //        string jwtToken = await LocalStorageManager.GetFromLocalStorage(_jsRuntime, "jwtToken");

        //        // Create a new HttpRequestMessage
        //        var request = new HttpRequestMessage(HttpMethod.Get, $"/api/Project/{employeeId}");

        //        // Add the Authorization header with the JWT token
        //        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

        //        // Send the request using HttpClient
        //        var response = await _httpClient.SendAsync(request);

        //        // Check if the response is successful
        //        if (response.IsSuccessStatusCode)
        //        {
        //            if (response.StatusCode == HttpStatusCode.NotFound)
        //            {
        //                return Enumerable.Empty<ProjectDTO>();
        //            }

        //            // Read the response content as a string
        //            var jsonString = await response.Content.ReadAsStringAsync();
        //            Console.WriteLine(jsonString);

        //            // Deserialize the JSON string into IEnumerable<ProjectDTO>
        //            var projects = JsonSerializer.Deserialize<IEnumerable<ProjectDTO>>(jsonString);
        //            foreach (var p in projects)
        //            {
        //                Console.WriteLine("Title : " + p.Title);

        //            }


        //            return projects;
        //        }
        //        else
        //        {
        //            // If response is not successful, read the error message from the response content
        //            var message = await response.Content.ReadAsStringAsync();
        //            throw new Exception($"Http Status : {response.StatusCode} - Message : {message}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public async Task<IEnumerable<ProjectDTO>> GetEmployeeProjects(int employeeId)
        {
            try
            {
                string jwtToken = await LocalStorageManager.GetFromLocalStorage(_jsRuntime, "jwtToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                var projects = await _httpClient.GetFromJsonAsync<IEnumerable<ProjectDTO>>($"api/Project/{employeeId}");
                foreach (var p in projects)
                {
                    Console.WriteLine("Title : " + p.Title);

                }
                return projects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
