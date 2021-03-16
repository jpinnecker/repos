using Microsoft.AspNetCore.Http;
using SoundClash.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SoundClash.CustomAttributes
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        public string[] AllowedExtensions = { Constants.DefaultAllowedExtension };

        public string GetErrorMessage() =>
            Constants.FileExtensionError + $" Please use: {string.Join(", ", AllowedExtensions)}";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;
            if (!ValidateFileExtension(((IFormFile)value)?.FileName))
                return new ValidationResult(GetErrorMessage());

            return ValidationResult.Success;
        }

        /// <summary>
        /// Returns true if fileName has been validated.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool ValidateFileExtension(string fileName)
        {
            string fileExtension = Path.GetExtension(fileName)?.ToLowerInvariant();

            return AllowedExtensions.Contains(fileExtension);
        }
    }
}
