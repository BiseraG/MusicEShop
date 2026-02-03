using Microsoft.EntityFrameworkCore;
using MusicEShop.Domain.DomainModels;

namespace MusicEShop.Repository
{
    public static class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            context.Database.Migrate();

            if (context.Artists.Any())
                return;

            var artists = new[]
            {
                new Artist { Id = Guid.Parse("11111111-1111-1111-1111-111111111101"), Name = "The Beatles", Country = "UK", Genre = "Rock" },
                new Artist { Id = Guid.Parse("11111111-1111-1111-1111-111111111102"), Name = "Pink Floyd", Country = "UK", Genre = "Progressive Rock" },
                new Artist { Id = Guid.Parse("11111111-1111-1111-1111-111111111103"), Name = "Led Zeppelin", Country = "UK", Genre = "Rock" },
                new Artist { Id = Guid.Parse("11111111-1111-1111-1111-111111111104"), Name = "Fleetwood Mac", Country = "UK/US", Genre = "Rock" },
                new Artist { Id = Guid.Parse("11111111-1111-1111-1111-111111111105"), Name = "Daft Punk", Country = "France", Genre = "Electronic" },
            };
            context.Artists.AddRange(artists);

            var albums = new[]
            {
                new Album { Id = Guid.Parse("22222222-2222-2222-2222-222222222201"), Title = "Abbey Road", Genre = "Rock", ReleaseDate = new DateTime(1969, 9, 26), Price = 24.99m, ArtistId = artists[0].Id },
                new Album { Id = Guid.Parse("22222222-2222-2222-2222-222222222202"), Title = "Sgt. Pepper's Lonely Hearts Club Band", Genre = "Rock", ReleaseDate = new DateTime(1967, 6, 1), Price = 22.99m, ArtistId = artists[0].Id },
                new Album { Id = Guid.Parse("22222222-2222-2222-2222-222222222203"), Title = "The Dark Side of the Moon", Genre = "Progressive Rock", ReleaseDate = new DateTime(1973, 3, 1), Price = 19.99m, ArtistId = artists[1].Id },
                new Album { Id = Guid.Parse("22222222-2222-2222-2222-222222222204"), Title = "The Wall", Genre = "Progressive Rock", ReleaseDate = new DateTime(1979, 11, 30), Price = 21.99m, ArtistId = artists[1].Id },
                new Album { Id = Guid.Parse("22222222-2222-2222-2222-222222222205"), Title = "Led Zeppelin IV", Genre = "Rock", ReleaseDate = new DateTime(1971, 11, 8), Price = 23.99m, ArtistId = artists[2].Id },
                new Album { Id = Guid.Parse("22222222-2222-2222-2222-222222222206"), Title = "Rumours", Genre = "Rock", ReleaseDate = new DateTime(1977, 2, 4), Price = 18.99m, ArtistId = artists[3].Id },
                new Album { Id = Guid.Parse("22222222-2222-2222-2222-222222222207"), Title = "Random Access Memories", Genre = "Electronic", ReleaseDate = new DateTime(2013, 5, 17), Price = 26.99m, ArtistId = artists[4].Id },
            };
            context.Albums.AddRange(albums);

            var tracks = new[]
            {
                new Track { Id = Guid.Parse("33333333-3333-3333-3333-333333333301"), Title = "Come Together", Duration = 259, Price = 1.29m, AlbumId = albums[0].Id },
                new Track { Id = Guid.Parse("33333333-3333-3333-3333-333333333302"), Title = "Something", Duration = 182, Price = 1.29m, AlbumId = albums[0].Id },
                new Track { Id = Guid.Parse("33333333-3333-3333-3333-333333333303"), Title = "Here Comes the Sun", Duration = 185, Price = 1.29m, AlbumId = albums[0].Id },
                new Track { Id = Guid.Parse("33333333-3333-3333-3333-333333333304"), Title = "With a Little Help from My Friends", Duration = 164, Price = 1.29m, AlbumId = albums[1].Id },
                new Track { Id = Guid.Parse("33333333-3333-3333-3333-333333333305"), Title = "Lucy in the Sky with Diamonds", Duration = 208, Price = 1.29m, AlbumId = albums[1].Id },
                new Track { Id = Guid.Parse("33333333-3333-3333-3333-333333333306"), Title = "Speak to Me / Breathe", Duration = 234, Price = 1.29m, AlbumId = albums[2].Id },
                new Track { Id = Guid.Parse("33333333-3333-3333-3333-333333333307"), Title = "Time", Duration = 413, Price = 1.29m, AlbumId = albums[2].Id },
                new Track { Id = Guid.Parse("33333333-3333-3333-3333-333333333308"), Title = "Money", Duration = 382, Price = 1.29m, AlbumId = albums[2].Id },
                new Track { Id = Guid.Parse("33333333-3333-3333-3333-333333333309"), Title = "Another Brick in the Wall (Part 2)", Duration = 240, Price = 1.29m, AlbumId = albums[3].Id },
                new Track { Id = Guid.Parse("33333333-3333-3333-3333-333333333310"), Title = "Comfortably Numb", Duration = 383, Price = 1.29m, AlbumId = albums[3].Id },
                new Track { Id = Guid.Parse("33333333-3333-3333-3333-333333333311"), Title = "Stairway to Heaven", Duration = 482, Price = 1.29m, AlbumId = albums[4].Id },
                new Track { Id = Guid.Parse("33333333-3333-3333-3333-333333333312"), Title = "Black Dog", Duration = 296, Price = 1.29m, AlbumId = albums[4].Id },
                new Track { Id = Guid.Parse("33333333-3333-3333-3333-333333333313"), Title = "Go Your Own Way", Duration = 218, Price = 1.29m, AlbumId = albums[5].Id },
                new Track { Id = Guid.Parse("33333333-3333-3333-3333-333333333314"), Title = "Dreams", Duration = 257, Price = 1.29m, AlbumId = albums[5].Id },
                new Track { Id = Guid.Parse("33333333-3333-3333-3333-333333333315"), Title = "Get Lucky", Duration = 369, Price = 1.29m, AlbumId = albums[6].Id },
                new Track { Id = Guid.Parse("33333333-3333-3333-3333-333333333316"), Title = "Instant Crush", Duration = 337, Price = 1.29m, AlbumId = albums[6].Id },
            };
            context.Tracks.AddRange(tracks);

            var artistTracks = new[]
            {
                new ArtistTrack { Id = Guid.NewGuid(), ArtistId = artists[0].Id, TrackId = tracks[0].Id },
                new ArtistTrack { Id = Guid.NewGuid(), ArtistId = artists[0].Id, TrackId = tracks[1].Id },
                new ArtistTrack { Id = Guid.NewGuid(), ArtistId = artists[0].Id, TrackId = tracks[2].Id },
                new ArtistTrack { Id = Guid.NewGuid(), ArtistId = artists[0].Id, TrackId = tracks[3].Id },
                new ArtistTrack { Id = Guid.NewGuid(), ArtistId = artists[0].Id, TrackId = tracks[4].Id },
                new ArtistTrack { Id = Guid.NewGuid(), ArtistId = artists[1].Id, TrackId = tracks[5].Id },
                new ArtistTrack { Id = Guid.NewGuid(), ArtistId = artists[1].Id, TrackId = tracks[6].Id },
                new ArtistTrack { Id = Guid.NewGuid(), ArtistId = artists[1].Id, TrackId = tracks[7].Id },
                new ArtistTrack { Id = Guid.NewGuid(), ArtistId = artists[1].Id, TrackId = tracks[8].Id },
                new ArtistTrack { Id = Guid.NewGuid(), ArtistId = artists[1].Id, TrackId = tracks[9].Id },
                new ArtistTrack { Id = Guid.NewGuid(), ArtistId = artists[2].Id, TrackId = tracks[10].Id },
                new ArtistTrack { Id = Guid.NewGuid(), ArtistId = artists[2].Id, TrackId = tracks[11].Id },
                new ArtistTrack { Id = Guid.NewGuid(), ArtistId = artists[3].Id, TrackId = tracks[12].Id },
                new ArtistTrack { Id = Guid.NewGuid(), ArtistId = artists[3].Id, TrackId = tracks[13].Id },
                new ArtistTrack { Id = Guid.NewGuid(), ArtistId = artists[4].Id, TrackId = tracks[14].Id },
                new ArtistTrack { Id = Guid.NewGuid(), ArtistId = artists[4].Id, TrackId = tracks[15].Id },
            };
            context.ArtistTracks.AddRange(artistTracks);

            context.SaveChanges();
        }
    }
}
