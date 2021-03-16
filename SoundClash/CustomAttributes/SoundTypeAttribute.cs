using SoundClash.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoundClash.CustomAttributes
{
    /// <summary>
    /// Custom ValidationAttribute for ASP validation
    /// </summary>
    public class SoundTypeAttribute : ValidationAttribute
    {
        public string GetErrorMessage() =>
            Constants.SoundTypeError;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!ValidateSoundType((SoundType)value))
                return new ValidationResult(GetErrorMessage());

            return ValidationResult.Success;
        }

        /// <summary>Returns true if validation is successful.</summary>
        public static bool ValidateSoundType(SoundType Type)
        {
            return Type != SoundType.All;
        }
    }
}
