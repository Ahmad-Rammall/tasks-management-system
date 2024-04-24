﻿using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using TaskManagementSystem.Models.DTOs.TaskDTOs;
using TasksManagementSystem.API.Data;
using TasksManagementSystem.API.Entities;
using TasksManagementSystem.API.Repositories.Interfaces;

namespace TasksManagementSystem.API.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;
        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }
        private async Task<bool> ProjectExists(int projectId)
        {
            return await _context.Projects.AnyAsync(p => p.Id == projectId);
        }
        private async Task<bool> EmployeeExists(int employeeId)
        {
            var employee = await _context.Users.FindAsync(employeeId);
            if (employee == null || employee.isDeleted)
                return false;

            return true;
        }
        public Task<TaskApprovalRequest> AcceptRequest(int taskId)
        {
            throw new NotImplementedException();
        }

        public async Task<TaskEntity> AddTaskToEmployee(TaskToAddDTO taskToAddDTO)
        {
            if(! await EmployeeExists(taskToAddDTO.UserId) || !await ProjectExists(taskToAddDTO.ProjectId))
            {
                return null;
            }

            var task = new TaskEntity
            {
                ProjectId = taskToAddDTO.ProjectId,
                UserId = taskToAddDTO.UserId,
                Title = taskToAddDTO.Title,
                Description = taskToAddDTO.Description,
                IsCompleted = false
            };

            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<IEnumerable<TaskEntity>> GetProjectTasks(int projectId)
        {
            if(! await ProjectExists(projectId))
            {
                return null;
            }

            return await _context.Tasks.Where(task => task.ProjectId == projectId).ToListAsync();
        }

        public Task<TaskApprovalRequest> RejectRequest(int taskId)
        {
            throw new NotImplementedException();
        }

        public Task<TaskApprovalRequest> SendApprovalRequest(int taskId)
        {
            throw new NotImplementedException();
        }
    }
}
