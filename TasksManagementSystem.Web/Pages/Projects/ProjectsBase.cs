using Microsoft.AspNetCore.Components;
using TaskManagementSystem.Models.DTOs.ProjectDTOs;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Pages.Projects
{
    public class ProjectsBase : ComponentBase
    {
        public IEnumerable<ProjectDTO> ProjectsList { get; set; }
        public string TitleToAdd { get; set; }
        public string DescriptionToAdd { get; set; }
        public string ErrorMessage { get; set; }

        [Inject]
        public IProjectService _projectService { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
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
