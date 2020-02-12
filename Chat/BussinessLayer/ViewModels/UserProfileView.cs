using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BussinessLayer.ViewModels
{
    public class UserProfileView
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserImage { get; set; }
        public string Mobile { get; set; }
        public Nullable<int> Role { get; set; }
        public Nullable<int> FolderId { get; set; }

    }
}
