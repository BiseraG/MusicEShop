﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEShop.Domain.DomainModels
{
    public class PlaylistTrack
    {
        [Key]
        public Guid Id { get; set; }
        public Guid PlaylistId { get; set; }
        public Playlist? Playlist { get; set; }

        public Guid TrackId { get; set; }
        public Track? Track { get; set; }
    }
}
