using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using BussinessLayer;
//using SRICHIDACADEMY.Models;
//using BussinessLayer.ViewModels;

namespace SRICHIDACADEMY.Hubs
{
    public class MyHub : Hub
    {
        static List<UserDetail> ConnectedUsers = new List<UserDetail>();

        static List<MessageDetail> CurrentMessage = new List<MessageDetail>();

        public static string emailIDLoaded = "";
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;
            Sdo_ChatEntities dc = new Sdo_ChatEntities();
            var ctx = new Sdo_ChatEntities();
            var userDetail =
                 (from m in ctx.UserProfiles
                  where m.UserName == userName
                  select new { m.UserName, m.UserImage, m.FolderId, m.Email }).FirstOrDefault();
            if (userDetail != null)
            {
                if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
                {
                    //ConnectedUsers.Add(new UserDetail { ConnectionId = id, UserName = userName, UserImage = userDetail.UserImage });

                    var item = dc.UserDetails.FirstOrDefault(x => x.UserName == userName);
                    if (item != null)
                    {
                        dc.UserDetails.Remove(item);
                        dc.SaveChanges();

                        // Disconnect
                        Clients.All.onUserDisconnectedExisting(item.ConnectionId, item.UserName, item.UserImage, item.FolderId);
                    }

                    var Users = dc.UserDetails.ToList();
                    if (Users.Where(x => x.UserName == userName).ToList().Count == 0)
                    {
                        var userdetails = new UserDetail
                        {
                            ConnectionId = id,
                            UserName = userName,
                            UserImage = userDetail.UserImage,
                            FolderId = userDetail.FolderId

                        };
                        dc.UserDetails.Add(userdetails);
                        dc.SaveChanges();

                        // send to caller
                        var connectedUsers = dc.UserDetails.ToList();
                        var CurrentMessage = dc.ChatMessageDetails.ToList();

                        // send to caller
                        Clients.Caller.onConnected(id, userName, connectedUsers, CurrentMessage);

                    }

                    // send to all except caller client
                    Clients.AllExcept(id).onNewUserConnected(id, userName, userDetail.UserImage);

                }

            }
        }



        public void SendMessageToAll(string userName, string message, string image)
        {
            // store last 100 messages in cache
            AddMessageinCache(userName, message);

            // Broad cast message
            Clients.All.messageReceived(userName, message, image);
        }

        public void SendPrivateMessage(string toUserId, string message, string status)
        {

            string fromUserId = Context.ConnectionId;
            using (Sdo_ChatEntities dc = new Sdo_ChatEntities())
            {
                var toUser = dc.UserDetails.FirstOrDefault(x => x.ConnectionId == toUserId);
                var fromUser = dc.UserDetails.FirstOrDefault(x => x.ConnectionId == fromUserId);

                if (toUser != null && fromUser != null)
                {
                    if (status == "Click")
                        AddPrivateMessageinCache(toUser.UserName, fromUser.UserName, message);

                    // send to 
                    Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.UserName, message);

                    // send to caller user
                    Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, message);
                }
            }

        }

        private void AddPrivateMessageinCache(string chatToUser, string userName, string message)
        {
            using (Sdo_ChatEntities dc = new Sdo_ChatEntities())
            {
                // Save master
                var master = dc.ChatPrivateMessageMasters.ToList().Where(a => a.UserName.Equals(userName)).ToList();
                if (master.Count == 0)
                {
                    var result = new ChatPrivateMessageMaster
                    {
                        UserName = userName
                    };
                    dc.ChatPrivateMessageMasters.Add(result);
                    dc.SaveChanges();
                }

                // Save details

                var fldrone = new Sdo_ChatEntities();
                var folderDetailone =
                     (from m in fldrone.UserProfiles
                      where m.UserName == userName
                      select new { m.FolderId }).FirstOrDefault();

                if (folderDetailone != null)
                {

                    var resultDetails = new ChatPrivateMessageDetail
                    {
                        MasterEmailID = userName,
                        ChatToEmailID = chatToUser,
                        Message = message,
                        FolderId = folderDetailone.FolderId,
                        MsgTime = Convert.ToString(DateTime.Now.ToString("HH:mm:ss")),
                        MsgDate = Convert.ToString(DateTime.Now)
                    };
                    dc.ChatPrivateMessageDetails.Add(resultDetails);
                    dc.SaveChanges();
                }
            }
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                ConnectedUsers.Remove(item);

                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.UserName);

            }

            return base.OnDisconnected(true);
        }


        //endregion

        //region private Messages

        private void AddMessageinCache(string userName, string message)
        {
            //CurrentMessage.Add(new MessageDetail { UserName = userName, Message = message });

            //if (CurrentMessage.Count > 100)
            //    CurrentMessage.RemoveAt(0);

            var fldr = new Sdo_ChatEntities();
            var folderDetail =
                 (from m in fldr.UserProfiles
                  where m.UserName == userName
                  select new { m.FolderId }).FirstOrDefault();

            if (folderDetail != null)
            {
                using (Sdo_ChatEntities dc = new Sdo_ChatEntities())
                {
                    var messageDetail = new ChatMessageDetail
                    {
                        UserName = userName,
                        Message = message,
                        EmailID = emailIDLoaded,
                        FolderId = folderDetail.FolderId,
                        MsgTime = Convert.ToString(DateTime.Now.ToString("HH:mm:ss")),
                        MsgDate = Convert.ToString(DateTime.Now)

                    };
                    dc.ChatMessageDetails.Add(messageDetail);
                    dc.SaveChanges();
                }
            }

        }

        public void removeMessages(string userName, string status)
        {
            using (Sdo_ChatEntities dc = new Sdo_ChatEntities())
            {
                var fromUser = dc.UserDetails.FirstOrDefault(x => x.UserName == userName);

                if (fromUser != null)
                {
                    if (status == "btnClear")
                        DeleteMessages();

                }
            }


        }

        private void DeleteMessages()
        {
            Sdo_ChatEntities db = new Sdo_ChatEntities();

            var all = from c in db.ChatMessageDetails select c;
            db.ChatMessageDetails.RemoveRange(all);
            db.SaveChanges();
        }



        //endregion
    }
}

//-->>>>> ***** Receive Request From Client [  Connect  ] *****
//        public void Connect(string userName, string password)
//        {
//            var id = Context.ConnectionId;
//            string userGroup = "";
//            //Manage Hub Class
//            //if freeflag==0 ==> Busy
//            //if freeflag==1 ==> Free

//            //if tpflag==0 ==> User
//            //if tpflag==1 ==> Admin


//            var ctx = new Sdo_ChatEntities();

//            var userInfo =
//                 (from m in ctx.UserProfiles
//                  where m.UserName == userName && m.Password == password
//                  select new { m.Id, m.UserName, m.Role }).FirstOrDefault();

//            try
//            {
//                //You can check if user or admin did not login before by below line which is an if condition
//                //if (UsersList.Count(x => x.ConnectionId == id) == 0)

//                //Here you check if there is no userGroup which is same DepID --> this is User otherwise this is Admin
//                //userGroup = DepID


//                if ((int)userInfo.Role == 0)
//                {
//                    //now we encounter ordinary user which needs userGroup and at this step, 
//                    //system assigns the first of free Admin among UsersList
//                    var strg = (from s in ConnectedUsers
//                                where (s.tpflag == "1")
//                                    && (s.freeflag == "1")
//                                select s).First();
//                    userGroup = strg.UserGroup;

//                    //Admin becomes busy so we assign zero to freeflag which is shown admin is busy
//                    strg.freeflag = "0";

//                    //now add USER to UsersList
//                    ConnectedUsers.Add(new UserInfo
//                    {
//                        ConnectionId = id,
//                        Id = userInfo.Id,
//                        UserName = userName,
//                        UserGroup = userGroup,
//                        freeflag = "0",
//                        tpflag = "0",
//                    });
//                    //whether it is Admin or User now both of them has userGroup and I Join this user or admin to specific group 
//                    Groups.Add(Context.ConnectionId, userGroup);
//                    Clients.Caller.onConnected(id, userName, userInfo.Id, userGroup);
//                }
//                else
//                {
//                    //If user has admin code so admin code is same userGroup
//                    //now add ADMIN to UsersList
//                    ConnectedUsers.Add(new UserInfo
//                    {
//                        ConnectionId = id,
//                        Role = userInfo.Id,
//                        UserName = userName,
//                        UserGroup = userInfo.Role.ToString(),
//                        freeflag = "1",
//                        tpflag = "1"
//                    });
//                    //whether it is Admin or User now both of them has userGroup and I Join this user or admin to specific group 
//                    Groups.Add(Context.ConnectionId, userInfo.Role.ToString());
//                    Clients.Caller.onConnected(id, userName, userInfo.Id, userInfo.Role.ToString());

//                }
//            }

//            catch
//            {
//                string msg = "All Administrators are busy, please be patient and try again";
//                //***** Return to Client *****
//                Clients.Caller.NoExistAdmin();
//            }
//        }
//        // <<<<<-- ***** Return to Client [  NoExist  ] *****

//        //--group ***** Receive Request From Client [  SendMessageToGroup  ] *****
//        public void SendMessageToGroup(string userName, string message)
//        {
//            if (ConnectedUsers.Count != 0)
//            {
//                var strg = (from s in ConnectedUsers where (s.UserName == userName) select s).First();
//                CurrentMessage.Add(new MessageInfo { UserName = userName, Message = message, UserGroup = strg.UserGroup });
//                string strgroup = strg.UserGroup;
//                // If you want to Broadcast message to all UsersList use below line
//                // Clients.All.getMessages(userName, message);

//                //If you want to establish peer to peer connection use below line 
//                //so message will be send just for user and admin who are in same group
//                //***** Return to Client *****
//                Clients.Group(strgroup).getMessages(userName, message);
//            }
//        }
//        // <<<<<-- ***** Return to Client [  getMessages  ] *****

//        //--group ***** Receive Request From Client ***** 
//        //{ Whenever User close session then OnDisconneced will be occurs }
//        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
//        {

//            var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
//            if (item != null)
//            {
//                ConnectedUsers.Remove(item);

//                var id = Context.ConnectionId;

//                if (item.tpflag == "0")
//                {
//                    //user logged off == user
//                    try
//                    {
//                        var stradmin = (from s in ConnectedUsers
//                                        where
//                                            (s.UserGroup == item.UserGroup) && (s.tpflag == "1")
//                                        select s).First();
//                        //become free
//                        stradmin.freeflag = "1";
//                    }
//                    catch
//                    {
//                        //***** Return to Client *****
//                        Clients.Caller.NoExistAdmin();
//                    }
//                }

//                //save conversation to dat abase
//            }

//            return base.OnDisconnected(true);
//        }
//    }
//}
