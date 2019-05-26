using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Podbase.Model
{
    [Table("Friends")]
    public class Friend
    {
        public int UserId { get; set; }
        public int FriendId { get; set; }
    }
}
