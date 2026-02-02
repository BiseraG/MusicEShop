using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicEShop.Domain.DomainModels;
using MusicEShop.Repository;
using MusicEShop.Service.Interface;

namespace MusicEShop.Web.Controllers
{
    [Authorize]
    public class TracksController : Controller
    {
        private readonly ITrackService _trackService;
        private readonly IAlbumService _albumService;
        private readonly ICartService _cartService;

        public TracksController(ITrackService trackService, IAlbumService albumService, ICartService cartService)
        {
            this._trackService = trackService;
            _albumService = albumService;
            _cartService = cartService;
        }

        public IActionResult Index()
        {

            var tracks=_trackService.GetAllTracks();
            
            
            return View(tracks);
        }

        public IActionResult Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = _trackService.GetTrackById(id);
            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        public IActionResult Create()
        {
            ViewData["AlbumId"] = new SelectList(_albumService.GetAllAlbums(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Duration,Price,AlbumId,Id")] Track track)
        {
            if (ModelState.IsValid)
            {
                track.Id = Guid.NewGuid();
                track.Album=_albumService.GetAlbumById(track.AlbumId);
                _trackService.CreateTrack(track);
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = new SelectList(_albumService.GetAllAlbums(), "Id", "Title", track.AlbumId);
            return View(track);
        }

        public IActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = _trackService.GetTrackById(id);
            if (track == null)
            {
                return NotFound();
            }
            ViewData["AlbumId"] = new SelectList(_albumService.GetAllAlbums(), "Id", "Title", track.AlbumId);
            return View(track);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Title,Duration,Price,AlbumId,Id")] Track track)
        {
            if (id != track.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _trackService.UpdateTrack(track);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackExists(track.Id))
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
            ViewData["AlbumId"] = new SelectList(_albumService.GetAllAlbums(), "Id", "Title", track.AlbumId);
            return View(track);
        }

        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = _trackService.GetTrackById(id);
            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var track = _trackService.GetTrackById(id);
            if (track != null)
            {
                _trackService.DeleteTrack(id);  
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult AddToCart(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = _trackService.GetTrackById(id);

            CartItem ps = new CartItem();

            ps.Quantity = 1;

            if (_trackService != null)
            {
                ps.TrackId = track.Id;
            }

            return View(ps);
        }

        [HttpPost]
        public IActionResult AddToCartConfirmed(CartItem model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _cartService.AddToShoppingConfirmed(model, userId);



            return RedirectToAction(nameof(Index));
        }
        private bool TrackExists(Guid id)
        {
            return _trackService.GetAllTracks().Any(e => e.Id == id);
        }
    }
}
