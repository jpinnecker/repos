using Microsoft.AspNetCore.Http;
using SoundClash.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SoundClash.Models.View
{
    /// <summary>
    /// View Model of Sound for Create Page using IFormFile.
    /// Has custom Attribute 'FileSize'.
    /// </summary>
    public class SoundEdit
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [SoundType]
        public SoundType Type { get; set; }

        [FileSize]
        [FileExtension]
        [FileSignature]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }

        public string FileLocation { get; set; }
    }
}
