using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models.DTOs.AuthDTOs
{
    public class UserLoginDTO
    {
        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
