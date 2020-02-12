using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.ViewModels
{
    public class MessageDetailView
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string MsgDate { get; set; }
    }
}
