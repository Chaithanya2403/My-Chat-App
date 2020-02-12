using BussinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SRICHIDACADEMY.Controllers
{
    public class PrivateChatController : ApiController
    {
        public List<ChatPrivateMessageDetail> PostPrivateChatHistory(ChatHistoryView users)
        {
            Sdo_ChatEntities db = new Sdo_ChatEntities();

            var fromuserid = Convert.ToString(users.fromusers);

            var touserid = Convert.ToString(users.tousers);

            //var username = db.UserProfiles.Where(c => c.FolderId == fromuserid).FirstOrDefault();

            //var chathistory = db.ChatPrivateMessageDetails.Where(c => c.MasterEmailID == username.UserName || c.ChatToEmailID == username.UserName).ToList();

            var fromusername = db.UserProfiles.Where(c => c.Email == fromuserid).Select(c => c.UserName).FirstOrDefault();

            var tousername = db.UserProfiles.Where(c => c.Email == fromuserid).Select(c => c.UserName).FirstOrDefault();

            var chathistory = db.ChatPrivateMessageDetails.Where(c => c.MasterEmailID == fromusername || c.ChatToEmailID == tousername).ToList();

            return chathistory;

        }
    }
}
