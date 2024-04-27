using Microsoft.AspNetCore.Components;
using TaskManagementSystem.Models.DTOs.UserDTOs;

namespace TasksManagementSystem.Web.Pages.Profiles
{
    public class ProfilesBase : ComponentBase
    {
        public IEnumerable<UserDTO> EmployeesList { get; set; }
    }
}
