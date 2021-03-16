using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoundClash.Models;
using SoundClash.Models.View;

namespace SoundClash.Pages.Clash
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public ClashIndex ClashIndex { get; set; }

        public void OnGet(ClashIndex clashIndex)
        {
            ClashIndex = clashIndex;
        }

        public void OnPostAdd()
        {
            ClashIndex.Elements.Add(new SoundElement());
        }
    }
}
