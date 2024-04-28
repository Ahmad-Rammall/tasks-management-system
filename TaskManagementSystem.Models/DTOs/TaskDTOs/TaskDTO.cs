using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models.DTOs.TaskDTOs
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
