using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoundClash.Models
{
    public static class Constants
    {
        public const string SoundTypeError = "Please choose a fitting Type.";
        public const string FileError = "Please choose a File.";
        public const string FileSizeError = "File size is too large.";
        public const string FileExtensionError = "File Extension not permitted.";
        public const string FileSignatureError = "Faulty File Signature.";
        public const string DefaultAllowedExtension = ".mp3";
        public const uint FileSizeMaxInMiB = 2;
        public const uint BytesPerMiB = 1048576;

        public static uint GetFileSizeMaxInByte() { return FileSizeMaxInMiB * BytesPerMiB; }
    }
}
