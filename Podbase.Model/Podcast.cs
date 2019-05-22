using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Podbase.Model
{
    public class Podcast
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PodcastId { get; set; }
        public int UserId { get; set; }

        public string Name { get; set; }
        public string Creator { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }

    }
}
