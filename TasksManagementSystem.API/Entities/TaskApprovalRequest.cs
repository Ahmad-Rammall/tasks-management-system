﻿using System.ComponentModel.DataAnnotations.Schema;

namespace TasksManagementSystem.API.Entities
{
    public class TaskApprovalRequest
    {
        public int Id { get; set; }

        [ForeignKey("TaskProp")]
        public int TaskId { get; set; }
        public string ExtraWork { get; set; }
        public bool IsApproved { get; set; }
        public Task TaskProp { get; set; }
    }
}
