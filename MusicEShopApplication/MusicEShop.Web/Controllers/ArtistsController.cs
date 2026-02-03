using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ArtistsController : Controller
    {
        private readonly IArtistService _artistService;


        public ArtistsController(IArtistService artistService)
        {
            this._artistService = artistService;
        }

        // GET: Artists
        public IActionResult Index()
        {
            return View(_artistService.GetAllArtists());
        }

        // GET: Artists/Details/5
        public IActionResult Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = _artistService.GetArtistById(id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // GET: Artists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Country,Genre,ArtistImage,Id")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                artist.Id = Guid.NewGuid();

                

                _artistService.CreateArtist(artist);
                return RedirectToAction(nameof(Index));
            }

            return View(artist);
        }


        // GET: Artists/Edit/5
        public IActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = _artistService.GetArtistById(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,Country,Genre,ArtistImage,Id")] Artist artist)
        { 
            if (id != artist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _artistService.UpdateArtist(artist);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistExists(artist.Id))
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
            return View(artist);
        }

        // GET: Artists/Delete/5
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = _artistService.GetArtistById(id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var artist = _artistService.GetArtistById(id);
            if (artist != null)
            {
                _artistService.DeleteArtist(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ArtistExists(Guid id)
        {
            return _artistService.GetAllArtists().Any(e => e.Id == id);
        }
    }
}
