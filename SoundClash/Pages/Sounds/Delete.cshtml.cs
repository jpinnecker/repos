using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SoundClash.Authorization;
using SoundClash.Data;
using SoundClash.Models;
using SoundClash.Models.View;

namespace SoundClash.Pages.Sounds
{
    public class DeleteModel : DI_BasePageModel
    {
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(ApplicationDbContext context,
                           IAuthorizationService authorizationService,
                           UserManager<ApplicationUser> userManager,
                           ILogger<DeleteModel> logger)
        : base(context, authorizationService, userManager)
        {
            _logger = logger;
        }

        public int Id { get; set; }

        [BindProperty]
        public SoundDetails SoundDetails { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Id = id;

            // Check whether user is authorized to delete
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                      User, Id,
                                                      UserOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            SoundDetails = await Context.Sound.Where(s => s.Id == Id)?.Select(s => new SoundDetails
            {
                Name = s.Name,
                Uploader = s.Uploader.UserName,
                Type = s.Type,
                FileLocation = s.FileLocation
            }).FirstOrDefaultAsync();

            if (SoundDetails == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Id = id;

            // Check whether user is authorized to delete
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                      User, Id,
                                                      UserOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Sound sound = await Context.Sound.FindAsync(Id);

            if (sound == null)
            {
                return NotFound();
            }

            try
            {
                System.IO.File.Delete("wwwroot/" + sound.FileLocation);
                Context.Sound.Remove(sound);
                await Context.SaveChangesAsync();
            } catch (Exception e)
            {
                _logger.LogWarning("Deletion failed.\n" + e.Message);
                ModelState.AddModelError(string.Empty, "Deletion failed.");
                return Page();
            }

            _logger.LogInformation("Sound deleted.");
            return RedirectToPage("./Index");
        }
    }
}
