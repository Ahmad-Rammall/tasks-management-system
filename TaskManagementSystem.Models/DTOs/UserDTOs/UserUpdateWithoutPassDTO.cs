﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models.DTOs.UserDTOs
{
    public class UserUpdateWithoutPassDTO
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
