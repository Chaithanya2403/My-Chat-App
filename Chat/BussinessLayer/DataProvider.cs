using BussinessLayer;
using BussinessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class DataProvider
    {
        Sdo_ChatEntities dbconnection = new Sdo_ChatEntities();
        public List<UserProfileView> GetUsers()
        {
            var userlist = dbconnection.UserProfiles.Select(c => new UserProfileView
            {
                Id = c.Id,
                FullName = c.FullName,
                Email = c.Email,
                Password = c.Password,
                ConfirmPassword = c.ConfirmPassword,
                UserName = c.UserName,
                UserImage = c.UserImage,
                //Mobile = c.Mobile,
                Role = c.Role,
                FolderId = c.FolderId
            }).ToList();
            return userlist;
        }
        public List<UserRoleView> GetRoles()
        {
            var roleslist = dbconnection.UserRoles.Select(c => new UserRoleView
            {
                Id = c.Id,
                RoleName = c.RoleName
            }).ToList();
            return roleslist;
        }
        //public List<UserDetail> GetUserDetails()
        //{
        //    var userdetailslist = dbconnection.UserDetails.Select(c => new UserDetailView
        //        {
        //            Id = c.Id,
        //            ConnectionId = c.ConnectionId,
        //            UserName = c.UserName
        //        }).ToList();
        //    return userdetailslist;
        //}
        //public List<MessageDetail> GetMessageDetails()
        //{
        //    var msgdetaillist = dbconnection.MessageDetails.Select(c => new MessageDetailView
        //        {
        //            Id = c.Id,
        //            UserName = c.UserName,
        //            Message = c.Message,
        //            StartTime = c.StartTime,
        //            EndTime = c.EndTime,
        //            MsgDate = c.MsgDate
        //        }).ToList();
        //    return msgdetaillist;
        //}
    }
}
