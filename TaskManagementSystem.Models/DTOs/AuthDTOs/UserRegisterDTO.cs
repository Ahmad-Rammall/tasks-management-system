using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TaskManagementSystem.Models.DTOs.AuthDTOs
{
    public class UserRegisterDTO
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
