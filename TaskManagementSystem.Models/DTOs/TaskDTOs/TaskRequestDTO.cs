using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models.DTOs.TaskDTOs
{
    public class TaskRequestDTO
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public bool IsApproved { get; set; }
    }
}
