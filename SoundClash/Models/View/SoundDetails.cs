using SoundClash.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoundClash.Models.View
{
    /// <summary>
    /// View Model of Sound for Details Page
    /// </summary>
    public class SoundDetails
    {
        public string Name { get; set; }
        public string Uploader { get; set; }
        public SoundType Type { get; set; }
        /// <summary>
        /// Holds Sound File
        /// </summary>
        public string FileLocation { get; set; }
    }
}
