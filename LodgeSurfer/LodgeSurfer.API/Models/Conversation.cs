using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LodgeSurfer.API.Models
{
    public class Conversation
    {
        public int ConversationId { get; set; }
        public int UserOneId { get; set; }
        public int UserTwoId { get; set; }

        [ForeignKey("UserOneId")]
        [InverseProperty("Conversations")]
        public User UserOne { get; set; }

        [ForeignKey("UserTwoId")]
        public User UserTwo { get; set; }


        public virtual ICollection<Message> Messages { get; set; }

    }
}