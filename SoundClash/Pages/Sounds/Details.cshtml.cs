using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SoundClash.Authorization;
using SoundClash.Data;
using SoundClash.Models;
using SoundClash.Models.View;

namespace SoundClash.Pages.Sounds
{
    [AllowAnonymous]
    public class DetailsModel : DI_BasePageModel
    {
        public DetailsModel(ApplicationDbContext context,
                           IAuthorizationService authorizationService,
                           UserManager<ApplicationUser> userManager)
        : base(context, authorizationService, userManager)
        {

        }

        public int Id { get; set; }

        [BindProperty]
        public SoundDetails SoundDetails { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Id = id;

            /// Load only necessary fields
            SoundDetails = await Context.Sound.Where(s => s.Id == Id)?.Select(s => new SoundDetails
            {
                Name = s.Name,
                Uploader = s.Uploader.UserName ?? "SoundClash",
                Type = s.Type,
                FileLocation = s.FileLocation
            }).FirstOrDefaultAsync();

            if (SoundDetails == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
