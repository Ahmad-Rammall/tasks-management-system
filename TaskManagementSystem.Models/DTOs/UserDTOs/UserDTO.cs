using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models.DTOs.UserDTOs
{
    [Serializable]

    public class UserDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string RoleName { get; set; }
    }
}
