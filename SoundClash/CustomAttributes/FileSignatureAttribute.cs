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
    public class FileSignatureAttribute : ValidationAttribute
    {
        public string GetErrorMessage() =>
            Constants.FileSignatureError;

        public string[] AllowedExtensions = { Constants.DefaultAllowedExtension };

        /// <summary>
        /// For File Signatures look here:
        /// https://en.wikipedia.org/wiki/List_of_file_signatures
        /// </summary>
        private static readonly Dictionary<string, List<byte[]>> _fileSignature =
            new Dictionary<string, List<byte[]>>
        {
            /// File Signatures for MP3's
            { ".mp3", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xFB },
                    new byte[] { 0xFF, 0xF3 },
                    new byte[] { 0xFF, 0xF2 },
                    new byte[] { 0x49, 0x44, 0x43}
                }
            }
        };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;
            if (!ValidateFileSignature((IFormFile)value))
                return new ValidationResult(GetErrorMessage());

            return ValidationResult.Success;
        }

        /// <summary>
        /// Returns true if fileName has been validated.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool ValidateFileSignature(IFormFile file)
        {
            using (BinaryReader reader = new BinaryReader(file.OpenReadStream()))
            {
                List<byte[]> signatures = new List<byte[]>();
                // For every extension look up allowed byte signatures
                Array.ForEach(AllowedExtensions, x => signatures.AddRange(_fileSignature[x]));
                // Read as many Bytes as maximal needed
                byte[] headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));
                // Return true if there is a fitting sequence
                return signatures.Any(signature =>
                    headerBytes.Take(signature.Length).SequenceEqual(signature));
            }
        }
    }
}
