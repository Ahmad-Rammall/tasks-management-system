using Microsoft.AspNetCore.Components;
using TaskManagementSystem.Models.DTOs.UserDTOs;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Pages.Profiles
{
    public class ProfilesBase : ComponentBase
    {
        public IEnumerable<UserDTO> EmployeesList { get; set; }

        [Inject]
        public IProfileService _profileService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                EmployeesList = await _profileService.GetAllEmployees();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
