using System;
using System.Collections.Generic;
using System.Text;

namespace Podbase.Model
{
    public class Podcast
    {
        public int PodcastId { get; set; }
        public string Name { get; set; }
        public string Creator { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
    }
}
