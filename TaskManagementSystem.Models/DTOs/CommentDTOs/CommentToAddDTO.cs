﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models.DTOs.CommentDTOs
{
    public class CommentToAddDTO
    {
        public string Content { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
    }
}