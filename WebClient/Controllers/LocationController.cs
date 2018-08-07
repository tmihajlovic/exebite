using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exebite.DtoModels;

namespace WebClient.Controllers
{
    public class LocationController : Controller
    {
        private readonly TempContext _context;

        public LocationController(TempContext context)
        {
            _context = context;
        }

        // GET: Location
        public async Task<IActionResult> Index()
        {
            //return View(await _context.LocationDto.ToListAsync());
            return View(new List<LocationDto>());
        }

        // GET: Location/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationDto = await _context.LocationDto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locationDto == null)
            {
                return NotFound();
            }

            return View(locationDto);
        }

        // GET: Location/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address")] CreateLocationDto locationDto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(locationDto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(locationDto);
        }

        // GET: Location/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationDto = await _context.LocationDto.FindAsync(id);
            if (locationDto == null)
            {
                return NotFound();
            }
            return View(locationDto);
        }

        // POST: Location/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address")] LocationDto locationDto)
        {
            if (id != locationDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locationDto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationDtoExists(locationDto.Id))
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
            return View(locationDto);
        }

        // GET: Location/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationDto = await _context.LocationDto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locationDto == null)
            {
                return NotFound();
            }

            return View(locationDto);
        }

        // POST: Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var locationDto = await _context.LocationDto.FindAsync(id);
            _context.LocationDto.Remove(locationDto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationDtoExists(int id)
        {
            return _context.LocationDto.Any(e => e.Id == id);
        }
    }
}
