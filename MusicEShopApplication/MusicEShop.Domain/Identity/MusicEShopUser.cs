using Microsoft.AspNetCore.Identity;
using MusicEShop.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEShop.Domain.Identity
{
    public class MusicEShopUser : IdentityUser
    {
        public string name { get; set; } = string.Empty;
        // One-to-Many: User → Playlists
        public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
        public virtual Cart Cart { get; set; }
        public virtual ICollection<Order>? Order { get; set; }
    }
}
