using Microsoft.AspNetCore.Identity;
using SoundClash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoundClash.Data
{
    public class ApplicationUser : IdentityUser
    {
        public List<Sound> OwnSounds { get; set; }
        //public List<Sound> FavouriteSounds { get; set; }
    }
}
