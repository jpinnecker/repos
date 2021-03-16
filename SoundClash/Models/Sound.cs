using Microsoft.AspNetCore.Http;
using SoundClash.CustomAttributes;
using SoundClash.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoundClash.Models
{
    /// <summary>
    /// Entity Model for our Sound
    /// </summary>
    public class Sound : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [SoundType]
        public SoundType Type { get; set; }

        [Required]
        public string FileLocation { get; set; }

        public ApplicationUser Uploader { get; set; }

        //public List<ApplicationUser> FavouringUsers { get; set; }

        /// <summary>
        /// Alternate way of validating, being kept as fallback solution.
        /// Deprecated since SoundTypeAttribute is working perfectly fine.
        /// Now uses SoundTypeAttribute to validate.
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>Result of Validation</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Don't accept SoundType.All
            if (!SoundTypeAttribute.ValidateSoundType(Type))
            {
                yield return new ValidationResult(
                    Constants.SoundTypeError,
                    new[] { nameof(Type) });
            }
        }
    }
}
