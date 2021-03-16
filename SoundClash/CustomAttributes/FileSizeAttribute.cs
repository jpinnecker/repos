using Microsoft.AspNetCore.Http;
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
    public class FileSizeAttribute : ValidationAttribute
    {
        public string GetErrorMessage() =>
            Constants.FileSizeError + $" Cannot be larger than: {Constants.FileSizeMaxInMiB}MB";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return ValidationResult.Success;
            }
            else if (value is IFormFile formFile)
            {
                if (!ValidateFileSize(formFile.Length))
                    return new ValidationResult(GetErrorMessage());
            } 
            else if (value is byte[] byteFile)
            {
                if (!ValidateFileSize(byteFile.Length))
                    return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        /// <summary>Returns true if validation is successful.</summary>
        public static bool ValidateFileSize(long FileSizeInByte)
        {
            return FileSizeInByte < Constants.GetFileSizeMaxInByte();
        }
    }
}
