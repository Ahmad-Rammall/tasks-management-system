using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TaskManagementSystem.Models.DTOs.ProjectDTOs;
using TasksManagementSystem.Web.Helpers;
using TasksManagementSystem.Web.Pages.Admin.Projects;
using TasksManagementSystem.Web.Services.Interfaces;
using TasksManagementSystem.Web.Store.User;

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
        public bool ShowDeleteModal { get; set; } = false;

        [Parameter]
        public bool ShowUpdateModal { get; set; } = false;

        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Inject]
        public IState<UserState> UserState { get; set; }

        [Inject]
        public IProjectService _projectService { get; set; }
        [Inject] private IAuthService _authService { get; set; }
        [Inject] private IJSRuntime jSRuntime { get; set; }
        public async Task HandleClick()
        {
            Methods methods = new Methods(_authService, jSRuntime);
            bool isAdmin = await methods.IsUserAdmin();
            if(isAdmin)
                navigationManager.NavigateTo($"/TasksPageAdmin/{ProjectID}");
            else
                navigationManager.NavigateTo($"/TasksPage/{ProjectID}");
        }
        public async Task DeleteProject()
        {
            var response = await _projectService.DeleteProject(ProjectID);
            if (response != null)
            {
                ShowDeleteModal = false;
                navigationManager.NavigateTo(navigationManager.Uri, forceLoad: true);
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
