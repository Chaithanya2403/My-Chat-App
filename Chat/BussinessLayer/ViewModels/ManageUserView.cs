using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.ViewModels
{
    public class ManageUserView
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserroleName { get; set; }
        public string UserState { get; set; }
        public string UserDistrict { get; set; }
        public string UserTaluk { get; set; }
        public string UserGramapanchayath { get; set; }
        public string UserVillage { get; set; }
        public string UserId { get; set; }
        public string OperationUnit { get; set; }
    
    }
}
