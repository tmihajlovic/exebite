using Exebite.Common;
using Exebite.DtoModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Services;

namespace WebClient.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationService _service;

        public LocationController(ILocationService service)
        {
            _service = service;
        }

        // GET: Location
        public async Task<IActionResult> Index()
        {
            var queryDto = new LocationQueryDto()
            {
                Page = 1,
                Size = QueryConstants.MaxElements - 1
            };
            var res = await _service.QueryAsync(queryDto).ConfigureAwait(false);
            return View(res.Items);
        }

        // GET: Location/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationDto = await _service.QueryAsync(new LocationQueryDto { Id = id, Page = 1, Size = 1 }).ConfigureAwait(false);
            if (locationDto.Total == 0)
            {
                return NotFound();
            }

            return View(locationDto.Items.First());
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
        public async Task<IActionResult> Create([Bind("Name,Address")] LocationDto locationDto)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(new CreateLocationDto { Name = locationDto.Name, Address = locationDto.Address }).ConfigureAwait(false);
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

            var locationDto = await _service.QueryAsync(new LocationQueryDto { Id = id, Page = 1, Size = 1 }).ConfigureAwait(false);
            if (locationDto.Total == 0)
            {
                return NotFound();
            }
            return View(locationDto.Items.First());
        }

        // POST: Location/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Address")] LocationDto locationDto)
        {
            if (id != locationDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(id, new UpdateLocationDto { Name = locationDto.Name, Address = locationDto.Address }).ConfigureAwait(false);
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

            var locationDto = await _service.QueryAsync(new LocationQueryDto { Id = id, Page = 1, Size = 1 }).ConfigureAwait(false);
            if (locationDto.Total == 0)
            {
                return NotFound();
            }

            return View(locationDto.Items.First());
        }

        // POST: Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteByIdAsync(id).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        private bool LocationDtoExists(int id)
        {
            return _service.QueryAsync(new LocationQueryDto { Id = id, Page = 1, Size = 1 }).Result.Total != 0;
        }
    }
}