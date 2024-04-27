using Microsoft.AspNetCore.Components;
using TaskManagementSystem.Models.DTOs.ProjectDTOs;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Pages.Projects
{
    public class ProjectsBase : ComponentBase
    {
        public IEnumerable<ProjectDTO> ProjectsList {  get; set; }

        [Inject]
        public IProjectService _projectService { get; set; }
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
    }
}
