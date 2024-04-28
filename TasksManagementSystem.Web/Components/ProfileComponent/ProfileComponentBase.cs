using Microsoft.AspNetCore.Components;
using TaskManagementSystem.Models.DTOs.UserDTOs;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Components.ProfileComponent
{
    public class ProfileComponentBase : ComponentBase
    {
        [Parameter]
        public UserDTO Employee { get; set; }

        [Parameter]
        public bool IsDeleted { get; set; }
        public string ErrorMessage { get; set; }
        public string Password { get; set; }

        public bool ShowDeleteModal { get; set; } = false;
        public bool ShowUpdateModal { get; set; } = false;

        [Inject]
        public IProfileService _profileService { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }
        public async Task DeleteEmployee()
        {
            try
            {
                await _profileService.DeleteUser(Employee.Id);
                navigationManager.NavigateTo(navigationManager.Uri, forceLoad: true);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        public async Task UpdateEmployee()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Password))
                {
                    UserUpdateWithoutPassDTO userDto = new UserUpdateWithoutPassDTO
                    {
                        FullName = Employee.FullName,
                        Username = Employee.Username,
                        IsDeleted = Employee.IsDeleted,
                    };
                    await _profileService.UpdateEmployeeWP(Employee.Id, userDto);
                    navigationManager.NavigateTo(navigationManager.Uri, forceLoad: true);
                }
                else
                {
                    Console.WriteLine(Employee.Id);
                    UserUpdateDTO userDto = new UserUpdateDTO
                    {
                        FullName = Employee.FullName,
                        Username = Employee.Username,
                        IsDeleted = Employee.IsDeleted,
                        Password = Password
                    };
                    await _profileService.UpdateEmployee(Employee.Id, userDto);
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
