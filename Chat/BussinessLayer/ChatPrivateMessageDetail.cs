//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BussinessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChatPrivateMessageDetail
    {
        public int ID { get; set; }
        public string MasterEmailID { get; set; }
        public string ChatToEmailID { get; set; }
        public string Message { get; set; }
        public Nullable<int> FolderId { get; set; }
        public string MsgTime { get; set; }
        public string MsgDate { get; set; }
    }
}