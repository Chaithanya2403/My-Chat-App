using BussinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SRICHIDACADEMY.Controllers
{
    public class ChatHistoryController : ApiController
    {

        Sdo_ChatEntities db = new Sdo_ChatEntities();
        //[ResponseType(typeof())]
        public IQueryable<ChatMessageDetail> GetChatHistory()
        {
            return db.ChatMessageDetails;
        }


    }
}
