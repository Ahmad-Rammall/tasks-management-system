using Microsoft.EntityFrameworkCore;
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
        private async Task<bool> TaskExists(int taskId)
        {
            return await _context.Tasks.AnyAsync(t => t.Id == taskId);
        }
        private async Task<TaskApprovalRequest> GetRequest(int requestId)
        {
            return await _context.TaskApprovalRequests.FindAsync(requestId);
        }
        private async Task<TaskEntity> GetTask(int taskId)
        {
            return await _context.Tasks.FindAsync(taskId);
        }
        private async Task<bool> EmployeeExists(int employeeId)
        {
            var employee = await _context.Users.FindAsync(employeeId);
            if (employee == null || employee.isDeleted)
                return false;

            return true;
        }
        public async Task<TaskApprovalRequest> AcceptRequest(int requestId)
        {
            var request = await GetRequest(requestId);
            if(request == null)
            {
                return null;
            }

            var task = await GetTask(request.TaskId);
            if (task == null)
                return null;

            task.IsCompleted = true;

            _context.Entry(task).State = EntityState.Modified;
            _context.TaskApprovalRequests.Remove(request);
            await _context.SaveChangesAsync();

            return request;
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

        public async Task<TaskApprovalRequest> RejectRequest(int requestId)
        {
            var request = await GetRequest(requestId);
            if (request == null)
            {
                return null;
            }

            _context.TaskApprovalRequests.Remove(request);
            await _context.SaveChangesAsync();

            return request;
        }

        public async Task<TaskApprovalRequest> SendApprovalRequest(int taskId)
        {
            if(! await TaskExists(taskId))
            {
                return null;
            }

            var newRequest = new TaskApprovalRequest
            {
                TaskId = taskId,
                IsApproved = false,
            };

            await _context.TaskApprovalRequests.AddAsync(newRequest);
            await _context.SaveChangesAsync();

            return newRequest;

        }
    }
}
