using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoundClash.Models;
using System;
using System.Linq;

namespace SoundClash.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>());
            SeedSoundDB(context);
        }

        //Initialise Sound DB with example Sounds
        public static void SeedSoundDB(ApplicationDbContext dbContext)
        {
            // Look for any sounds.
            if (dbContext.Sound.Any())
            {
                return;   // DB has been seeded
            }

            dbContext.Sound.AddRange(
                new Sound
                {
                    Name = "TomTom",
                    Type = SoundType.Drums,
                    FileLocation = "resources/TomTom.mp3"
                },
                new Sound
                {
                    Name = "TomTom2",
                    Type = SoundType.Drums,
                    FileLocation = "resources/TomTom2.mp3"
                },
                new Sound
                {
                    Name = "SnareDrum",
                    Type = SoundType.Drums,
                    FileLocation = "resources/SnareDrum.mp3"
                },
                new Sound
                {
                    Name = "SnareDrum2",
                    Type = SoundType.Drums,
                    FileLocation = "resources/SnareDrum2.mp3"
                },
                new Sound
                {
                    Name = "RideCymbal",
                    Type = SoundType.Drums,
                    FileLocation = "resources/RideCymbal.mp3"
                },
                new Sound
                {
                    Name = "RideCymbal2",
                    Type = SoundType.Drums,
                    FileLocation = "resources/RideCymbal2.mp3"
                },
                new Sound
                {
                    Name = "KickDrum",
                    Type = SoundType.Drums,
                    FileLocation = "resources/KickDrum.mp3"
                },
                new Sound
                {
                    Name = "HiHatOpen",
                    Type = SoundType.Drums,
                    FileLocation = "resources/HiHatOpen.mp3"
                },
                new Sound
                {
                    Name = "HiHatClosed",
                    Type = SoundType.Drums,
                    FileLocation = "resources/HiHatClosed.mp3"
                },
                new Sound
                {
                    Name = "FloorTom",
                    Type = SoundType.Drums,
                    FileLocation = "resources/FloorTom.mp3"
                },
                new Sound
                {
                    Name = "CrashCymbalBrushed",
                    Type = SoundType.Drums,
                    FileLocation = "resources/CrashCymbalBrushed.mp3"
                },
                new Sound
                {
                    Name = "CrashCymbal",
                    Type = SoundType.Drums,
                    FileLocation = "resources/CrashCymbal.mp3"
                },
                new Sound
                {
                    Name = "CrashCymbal2",
                    Type = SoundType.Drums,
                    FileLocation = "resources/CrashCymbal2.mp3"
                }
            );
            dbContext.SaveChanges();
        }
    }
}