using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SoundClash.Authorization;
using SoundClash.CustomAttributes;
using SoundClash.Data;
using SoundClash.Models;
using SoundClash.Models.View;

namespace SoundClash.Pages.Sounds
{
    public class EditModel : DI_BasePageModel
    {
        private readonly ILogger<EditModel> _logger;

        public EditModel(ApplicationDbContext context,
                           IAuthorizationService authorizationService,
                           UserManager<ApplicationUser> userManager,
                           ILogger<EditModel> logger)
        : base(context, authorizationService, userManager)
        {
            _logger = logger;
        }

        public int Id { get; set; }

        [BindProperty]
        public SoundEdit SoundEdit { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Id = id;

            // Check whether user is authorized to update
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                      User, Id,
                                                      UserOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            /// FROM Sound WHERE ID==id SELECT Name && Type
            SoundEdit = await Context.Sound.Where(s => s.Id == Id)?.Select(s => new SoundEdit
            {
                Name = s.Name,
                Type = s.Type,
                FileLocation = s.FileLocation
            }).FirstOrDefaultAsync();

            if (SoundEdit == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            // Additional Server Side Validation
            if (!ModelState.IsValid)
                return Page();

            Id = id;

            // Check whether user is authorized to update
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                      User, Id,
                                                      UserOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Sound SoundToUpdate = await Context.Sound.FindAsync(Id);

            if (SoundToUpdate == null)
            {
                return NotFound();
            }

            /// If we get a value in our FormFile ...
            if (SoundEdit.FormFile != null)
            {
                /// Save File to storage ...
                await SaveFile();
            } 

            /// Write to Entity Model
            if (await TryUpdateModelAsync<Sound>(
                SoundToUpdate,
                "soundedit",
                s => s.Name,
                s => s.Type))
            {
                try
                {
                    await Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoundExists(SoundToUpdate.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                _logger.LogInformation("Sound updated.");

                return RedirectToPage($"./Index");
            }

            var validationErrors = ModelState.Values.Where(E => E.Errors.Count > 0)
                .SelectMany(E => E.Errors)
                .Select(E => E.ErrorMessage)
                .ToList();

            _logger.LogError("Update failed!\n" + String.Join(',', validationErrors));

            return Page();
        }

        private bool SoundExists(int id)
        {
            return Context.Sound.Any(e => e.Id == id);
        }

        /// <summary>
        /// Saves uploaded file to disk
        /// </summary>
        /// <returns></returns>
        public async Task SaveFile()
        {
            // Routine to save uploaded File
            if (SoundEdit.FormFile.Length > 0)
            {
                using FileStream stream = System.IO.File.Create("wwwroot/" + SoundEdit.FileLocation);
                await SoundEdit.FormFile.CopyToAsync(stream);
            }
        }
    }
}
