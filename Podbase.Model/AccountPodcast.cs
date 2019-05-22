using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Podbase.Model
{
    public class AccountPodcast
    {

        public int UserId { get; set; }
        [Key]
        public int PodcastId { get; set; }

        public ObservableCollection<Podcast> PodcastsPerAccount { get; set; } = new ObservableCollection<Podcast>();
    }
}
