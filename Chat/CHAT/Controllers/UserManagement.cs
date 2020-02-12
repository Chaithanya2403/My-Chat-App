using BussinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRICHIDACADEMY.Controllers
{
    public class UserManagment
    {
        DataProvider dataProvider = new DataProvider();
        public Boolean IsValidUser(string username, string password)
        {
            if (dataProvider.GetUsers().Any(c => c.UserName == username && c.Password == password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string GetUserRoleByUserName(string userName)
        {
            var userRole = (from usr in dataProvider.GetUsers()
                            join rol in dataProvider.GetRoles() on usr.Role equals rol.Id
                            where usr.UserName == userName
                            select rol.RoleName).First();
            return userRole.ToString();

        }
        public int GetUserIdByUserName(string userName)
        {
            int id = (from usr in dataProvider.GetUsers()
                      join userid in dataProvider.GetUsers() on usr.Id equals userid.Id
                      where usr.UserName == userName
                      select userid.Id).First();
            return id;
        }
    }
}