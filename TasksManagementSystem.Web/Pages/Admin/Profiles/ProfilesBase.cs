﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TaskManagementSystem.Models.DTOs.UserDTOs;
using TasksManagementSystem.Web.Helpers;
using TasksManagementSystem.Web.Services;
using TasksManagementSystem.Web.Services.Interfaces;

namespace TasksManagementSystem.Web.Pages.Admin.Profiles
{
    public class ProfilesBase : JwtVerificationComponent
    {
        public IEnumerable<UserDTO> EmployeesList { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ErrorMessage { get; set; }
        public bool ShowDeleteModal { get; set; } = false;

        [Inject]
        public IProfileService _profileService { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }
        [Inject] public IAuthService _authService {  get; set; }
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
                    EmployeesList = await _profileService.GetAllEmployees();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddEmployee()
        {
            try
            {
                UserRegisterDTO userRegisterDTO = new UserRegisterDTO
                {
                    Username = Username,
                    FullName = FullName,
                    Password = Password
                };

                await _profileService.AddEmployee(userRegisterDTO);
                navigationManager.NavigateTo(navigationManager.Uri, forceLoad: true);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
