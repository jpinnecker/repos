using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoundClash.Models.View
{
    public class ClashIndex
    {
        public List<SoundElement> Elements { get; set; } = new List<SoundElement>();
    }

    public class SoundElement
    {
        public Sound Sound { get; set; } = new Sound();
        public List<SoundSelect> searchList { get; set; } = new List<SoundSelect>(0);    }
}
