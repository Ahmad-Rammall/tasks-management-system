using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TaskManagementSystem.Models.DTOs.ProjectDTOs;
using TasksManagementSystem.Web.Helpers;
using TasksManagementSystem.Web.Pages.Projects;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Components.ProjectComponent
{
    public class ProjectBase : ProjectsBase
    {
        [Parameter]
        public int ProjectID { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string Description { get; set; }

        [Parameter]
        public bool IsAdmin { get; set; }

        [Parameter]
        public bool ShowDeleteModal { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Inject]
        public IProjectService _projectService { get; set; }
        public async Task HandleClick()
        {
            var roleId = await LocalStorageManager.GetFromLocalStorage(JSRuntime, "userRole");
            if(int.Parse(roleId) == 2)
                navigationManager.NavigateTo($"/TasksPage/{ProjectID}");
            else
                navigationManager.NavigateTo($"/TasksPageAdmin/{ProjectID}");
        }
        public async Task DeleteProject()
        {
            var response = await _projectService.DeleteProject(ProjectID);
            if (response != null)
            {
                ShowDeleteModal = false;
                navigationManager.NavigateTo(navigationManager.Uri, forceLoad:true);
            }
        }

        public async Task UpdateProject()
        {
            try
            {
                ProjectToAddDTO projectToAddDTO = new ProjectToAddDTO
                {
                    Title = Title,
                    Description = Description
                };
                Console.WriteLine(projectToAddDTO.Title);
                var response = await _projectService.UpdateProject(ProjectID, projectToAddDTO);
                if (response != null)
                {
                    navigationManager.NavigateTo(navigationManager.Uri, forceLoad: true);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
