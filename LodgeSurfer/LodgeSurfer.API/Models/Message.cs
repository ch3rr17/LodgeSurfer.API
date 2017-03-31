using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LodgeSurfer.API.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public int ConversationId { get; set; }
        public int UserId { get; set; }
        public string MessageText { get; set; }
        public DateTime MessageTime { get; set; }

        //Foreign Keys
        public virtual Conversation Conversation { get; set; }
        public virtual User User { get; set; }
    }
}