using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SoundClash.Data;
using SoundClash.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoundClash.Hubs
{
    /// <summary>
    /// Needs to allow anonymous as I haven't found a way to populate Context.User
    /// </summary>
    [AllowAnonymous]
    public class ClashHub : Hub
    {
        private readonly ApplicationDbContext _dbContext;
        public ClashHub(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SoundSearch(string soundName)
        {
            // WHERE Name contains elementName SELECT Name TAKE 5
            List<SoundSelect> resultList = await _dbContext.Sound.Where(s => s.Name.Contains(soundName)).Take(5)
                                          .Select(s => new SoundSelect
                                          { 
                                              SoundName = s.Name,
                                              FileLocation = s.FileLocation
                                          }).ToListAsync();

            await Clients.Caller.SendAsync("ReceiveSoundList", resultList);
        }

    }
}
