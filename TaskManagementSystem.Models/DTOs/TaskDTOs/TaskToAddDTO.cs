using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models.DTOs.TaskDTOs
{
    public class TaskToAddDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
    }
}
