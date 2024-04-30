using Microsoft.AspNetCore.Components;
using TaskManagementSystem.Models.DTOs.UserDTOs;
using TasksManagementSystem.Web.Services.Interfaces;
using TasksManagementSystem.Web.Components.DeleteConfirmationModal;

namespace TasksManagementSystem.Web.Components.ProfileComponent
{
    public class ProfileComponentBase : ComponentBase
    {
        [Parameter] public UserDTO Employee { get; set; }
        [Parameter] public bool IsDeleted { get; set; }
        [Parameter] public EventCallback<int> SetSelectedProfileId { get; set; }
        [Parameter] public Action OpenDeleteModal { get; set; }
        public string ErrorMessage { get; set; }
        public string Password { get; set; }
        public bool ShowUpdateModal { get; set; } = false;
        [Inject] public IProfileService _profileService { get; set; }
        [Inject] NavigationManager navigationManager { get; set; }

        public void ShowDeleteModal()
        {
            SetSelectedProfileId.InvokeAsync(Employee.Id);
            OpenDeleteModal?.Invoke();
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
