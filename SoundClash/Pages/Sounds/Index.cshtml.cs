using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SoundClash.Data;
using SoundClash.Models;
using SoundClash.Models.View;

namespace SoundClash.Pages.Sounds
{
    public enum Sort
    {
        Null, // don't sort by default
        Name_ASC,
        Name_DESC,
        Uploader_ASC,
        Uploader_DESC,
        Type_ASC,
        Type_DESC
    }

    [AllowAnonymous]
    public class IndexModel : DI_BasePageModel
    {
        private readonly IConfiguration _config;

        public IndexModel(ApplicationDbContext context,
                           IAuthorizationService authorizationService,
                           UserManager<ApplicationUser> userManager,
                           IConfiguration config)
        : base(context, authorizationService, userManager)
        {
            _config = config;
        }

        /// <summary>
        /// View Model of the Index Page
        /// </summary>
        public PaginatedList<SoundIndex> SoundIndex { get; set; }

        /// Filter properties
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public SoundType SoundType { get; set; }

        /// Sorting properties
/*
        public string CurrentFilter { get; set; }
        */

        public Sort Name = Sort.Name_ASC;
        public Sort Uploader = Sort.Uploader_ASC;
        public Sort Type = Sort.Type_ASC;
        public Sort CurrentSort;

        public async Task<IActionResult> OnGetAsync(Sort sortOrder, int? pageIndex,
                                                    SoundType soundType, string searchString)
        {
            /// Build our database query
            IQueryable<SoundIndex> SoundIndexQuery = Context.Sound.Select(s => new SoundIndex
            {
                Id = s.Id,
                Name = s.Name,
                Uploader = s.Uploader.UserName ?? "SoundClash",
                Type = s.Type,
                FileLocation = s.FileLocation
            });

            /// Remember old filter
            if (!string.IsNullOrEmpty(searchString))
                SearchString = searchString;
            
            if (soundType != SoundType.All)
                SoundType = soundType;

            /// Apply filter
            if (!string.IsNullOrEmpty(SearchString))
                SoundIndexQuery = SoundIndexQuery
                    .Where(s => s.Name.Contains(SearchString) || s.Uploader.Contains(SearchString));

            if (SoundType != SoundType.All)
                SoundIndexQuery = SoundIndexQuery
                    .Where(s => s.Type == SoundType);

            /// Remember old sorting
            CurrentSort = sortOrder;

            /// Apply sorting
            switch (sortOrder)
            {
                case Sort.Name_ASC:
                    Name = Sort.Name_DESC;
                    SoundIndexQuery = SoundIndexQuery.OrderBy(s => s.Name);
                    break;

                case Sort.Name_DESC:
                    Name = Sort.Name_ASC;
                    SoundIndexQuery = SoundIndexQuery.OrderByDescending(s => s.Name);
                    break;

                case Sort.Uploader_ASC:
                    Uploader = Sort.Uploader_DESC;
                    SoundIndexQuery = SoundIndexQuery.OrderBy(s => s.Uploader);
                    break;

                case Sort.Uploader_DESC:
                    Uploader = Sort.Uploader_ASC;
                    SoundIndexQuery = SoundIndexQuery.OrderByDescending(s => s.Uploader);
                    break;

                case Sort.Type_ASC:
                    Type = Sort.Type_DESC;
                    SoundIndexQuery = SoundIndexQuery.OrderBy(s => s.Type);
                    break;

                case Sort.Type_DESC:
                    Type = Sort.Type_ASC;
                    SoundIndexQuery = SoundIndexQuery.OrderByDescending(s => s.Type);
                    break;

                default:
                    break;
            }

            /// Fire query
            try
            {
                SoundIndex = await PaginatedList<SoundIndex>.CreateAsync(SoundIndexQuery, pageIndex ?? 1,
                    int.Parse(_config["SoundIndexPageSize"]));
            } catch (ArgumentException)
            {
                //log error here
                return NotFound();
            }

            return Page();
        }
    }
}
