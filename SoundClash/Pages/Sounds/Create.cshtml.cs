using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SoundClash.Authorization;
using SoundClash.Data;
using SoundClash.Models;
using SoundClash.Models.View;

namespace SoundClash.Pages.Sounds
{
    public class CreateModel : DI_BasePageModel
    {
        private readonly IConfiguration _config;

        public CreateModel(ApplicationDbContext context,
                           IAuthorizationService authorizationService,
                           UserManager<ApplicationUser> userManager,
                           IConfiguration config)
        : base(context, authorizationService, userManager)
        {
            _config = config;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SoundCreate SoundCreate { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            // Additional Server Side Validation
            if (!ModelState.IsValid)
                return Page();

            var sound = Context.Add(new Sound());
            // Save uploaded file to the correct property
            await SaveFile();
            sound.CurrentValues.SetValues(SoundCreate);

            // Link Sound to Owner
            sound.Reference(s => s.Uploader).CurrentValue = await UserManager.GetUserAsync(User);

            // If Entity Requirements aren't met return
            if (!TryValidateModel(sound.Entity))
            {
                return Page();
            }

            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        /// <summary>
        /// Saves uploaded file to disk
        /// </summary>
        /// <returns></returns>
        public async Task SaveFile()
        {
            // Routine to save uploaded File
            if (SoundCreate.FormFile.Length > 0)
            {
                SoundCreate.FileLocation = Path.Combine(_config["StoragePath"],
                    Path.GetRandomFileName()) + ".mp3";

                using FileStream stream = System.IO.File.Create("wwwroot" + SoundCreate.FileLocation);
                await SoundCreate.FormFile.CopyToAsync(stream);
            }
        }
    }
}
