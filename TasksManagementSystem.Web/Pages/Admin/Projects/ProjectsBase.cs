using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TaskManagementSystem.Models.DTOs.ProjectDTOs;
using TasksManagementSystem.Web.Helpers;
using TasksManagementSystem.Web.Services;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Pages.Admin.Projects
{
    public class ProjectsBase : JwtVerificationComponent
    {
        public IEnumerable<ProjectDTO> ProjectsList { get; set; }
        public string TitleToAdd { get; set; }
        public string DescriptionToAdd { get; set; }
        public string ErrorMessage { get; set; }

        [Inject]
        public IProjectService _projectService { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }
        [Inject] public IAuthService _authService { get; set; }
        [Inject] public IJSRuntime jSRuntime { get; set; }

        protected bool IsAdmin { get; set; } = true;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                await base.OnInitializedAsync();
                if (IsNavigated) return;

                Methods methods = new Methods(_authService, jSRuntime);
                IsAdmin = await methods.IsUserAdmin();

                if (IsAdmin)
                    ProjectsList = await _projectService.GetAllProjects();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task AddProject()
        {
            try
            {
                ProjectToAddDTO projectToAddDTO = new ProjectToAddDTO
                {
                    Title = TitleToAdd,
                    Description = DescriptionToAdd
                };
                var response = await _projectService.AddProject(projectToAddDTO);
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
