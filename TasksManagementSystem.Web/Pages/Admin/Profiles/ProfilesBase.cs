﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Threading.Tasks;
using TaskManagementSystem.Models.DTOs.AuthDTOs;
using TaskManagementSystem.Models.DTOs.UserDTOs;
using TasksManagementSystem.Web.Components.DeleteConfirmationModal;
using TasksManagementSystem.Web.Components.ProfileModal;
using TasksManagementSystem.Web.Components.ProfileUpdateModal;
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
        public int SelectedProfileId { get; set; }
        public UserDTO SelectedUser { get; set; }
        [Inject]public IProfileService _profileService { get; set; }
        [Inject] NavigationManager navigationManager { get; set; }
        [Inject] public IAuthService _authService {  get; set; }
        [Inject] public IJSRuntime jSRuntime { get; set; }

        protected bool IsAdmin { get; set; } = true;
        public DeleteModalBase? deleteModal { get; set; }
        public ProfileModalBase? AddProfileModal { get; set; }
        public ProfileUpdateModalBase? UpdateProfileModal { get; set; }

        public void UpdateSelectedProfileId(int newProfileId)
        {
            SelectedProfileId = newProfileId;
        }
        public void UpdateSelectedUser(UserDTO user)
        {
            SelectedUser = user;
        }
        public void OpenDeleteModal()
        {
            deleteModal?.ShowModal();
        }
        public void OpenAddModal()
        {
            AddProfileModal?.ShowModal();
        }
        public void OpenUpdateModal()
        {
            UpdateProfileModal?.ShowModal();
            Console.WriteLine(SelectedUser.Id);
        }
        public async Task DeleteEmployee()
        {
            try
            {
                await _profileService.DeleteUser(SelectedProfileId);
                navigationManager.NavigateTo(navigationManager.Uri, forceLoad: true);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
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

        protected async Task OnModalChange(UserRegisterDTO userRegisterDTO)
        {
            FullName = userRegisterDTO.FullName;
            Username = userRegisterDTO.Username;
            Password = userRegisterDTO.Password;
            Console.WriteLine(Username + " : " + FullName + " : " + Password);
        }

        public async Task AddEmployee()
        {
            try
            {
                Console.WriteLine(Username + " : " + FullName + " : " + Password);
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
        public async Task UpdateEmployee()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Password))
                {
                    UserUpdateWithoutPassDTO userDto = new UserUpdateWithoutPassDTO
                    {
                        FullName = SelectedUser.FullName,
                        Username = SelectedUser.Username,
                        IsDeleted = SelectedUser.IsDeleted,
                    };
                    await _profileService.UpdateEmployeeWP(SelectedUser.Id, userDto);
                    navigationManager.NavigateTo(navigationManager.Uri, forceLoad: true);
                }
                else
                {
                    Console.WriteLine(SelectedUser.Id);
                    UserUpdateDTO userDto = new UserUpdateDTO
                    {
                        FullName = SelectedUser.FullName,
                        Username = SelectedUser.Username,
                        IsDeleted = SelectedUser.IsDeleted,
                        Password = Password
                    };
                    await _profileService.UpdateEmployee(SelectedUser.Id, userDto);
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
