using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicEShop.Domain.DomainModels;
using MusicEShop.Service.Interface;

namespace MusicEShop.Web.Controllers
{
    [Authorize]
    public class AlbumsController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly IArtistService _artistService;
        private readonly ICartService _cartService;

        public AlbumsController(IAlbumService albumService, IArtistService artistService, ICartService cartService)
        {
            _albumService = albumService;
            _artistService = artistService;
            _cartService = cartService;
        }
        public IActionResult Index()
        {
            var albums = _albumService.GetAllAlbums();
            foreach (var album in albums)
            {
                album.Artist = _artistService.GetArtistById(album.ArtistId);
            }

            return View(albums);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = _albumService.GetAlbumById(id);

            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_artistService.GetAllArtists(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Genre,Details,ReleaseDate,CoverImage,Price,ArtistId,Id")] Album album)
        {
            if (ModelState.IsValid)
            {
                album.Id = Guid.NewGuid();

          
                _albumService.CreateAlbum(album);

                return RedirectToAction(nameof(Index));
            }

            ViewData["ArtistId"] = new SelectList(_artistService.GetAllArtists(), "Id", "Id", album.ArtistId);
            return View(album);
        }

        public IActionResult  Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = _albumService.GetAlbumById(id);
            if (album == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_artistService.GetAllArtists(), "Id", "Name", album.ArtistId);
            return View(album);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Title,Genre,Details,ReleaseDate,Price,CoverImage,ArtistId,Id")] Album album)
        {
            if (id != album.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _albumService.UpdateAlbum(album);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_artistService.GetAllArtists(), "Id", "Name", album.ArtistId);
            return View(album);
        }

        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = _albumService.GetAlbumById(id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var album = _albumService.GetAlbumById(id);
            if (album != null)
            {
                _albumService.DeleteAlbum(id);
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult ListAlbumsByArtist(Guid artistId)
        {
            var albums = _albumService.GetAlbumsByArtist(artistId);

            if (albums == null || !albums.Any())
            {
                return NotFound("No albums found for this artist.");
            }

            return View(albums);
        }

        public IActionResult AddToCart(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = _albumService.GetAlbumById(id);

            CartItem ps = new CartItem();

            ps.Quantity = 1;

            if (album != null)
            {
                ps.AlbumId = album.Id;
            }

            return View(ps);
        }

        [HttpPost]
        public IActionResult AddToCartConfirmed(CartItem model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _cartService.AddToShoppingConfirmed(model, userId);



            return View("Index", _albumService.GetAllAlbums());
        }


        private bool AlbumExists(Guid id)
        {
            return _albumService.GetAllAlbums().Any(e => e.Id == id);
        }
    }
}
