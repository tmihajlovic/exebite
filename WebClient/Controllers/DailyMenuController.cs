using System.Linq;
using System.Threading.Tasks;
using Exebite.Common;
using Exebite.DtoModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebClient.Services;

namespace WebClient.Controllers
{
    public class DailyMenuController : Controller
    {
        private readonly IDailyMenuService _service;

        public DailyMenuController(IDailyMenuService service)
        {
            _service = service;
        }

        // GET: DailyMenuDtoes
        public async Task<IActionResult> Index()
        {
            var queryDto = new DailyMenuQueryDto()
            {
                Page = 1,
                Size = QueryConstants.MaxElements - 1
            };
            var res = await _service.QueryAsync(queryDto).ConfigureAwait(false);
            return View(res.Items);
        }

        // GET: DailyMenuDtoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyMenuDto = await _service.QueryAsync(new DailyMenuQueryDto { Id = id, Page = 1, Size = 1 }).ConfigureAwait(false);
            if (dailyMenuDto == null)
            {
                return NotFound();
            }

            return View(dailyMenuDto.Items.First());
        }

        // GET: DailyMenuDtoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DailyMenuDtoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RestaurantId")] DailyMenuDto model)
        {
            if (ModelState.IsValid)
            {
                // TODO: this should be change to use CreateMenuDto because Foods is missing
                await _service.CreateAsync(new CreateDailyMenuDto { RestaurantId = model.RestaurantId }).ConfigureAwait(false);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: DailyMenuDtoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyMenuDto = await _service.QueryAsync(new DailyMenuQueryDto { Id = id, Page = 1, Size = 1 }).ConfigureAwait(false);
            if (dailyMenuDto.Total == 0)
            {
                return NotFound();
            }
            return View(dailyMenuDto.Items.First());
        }

        // POST: DailyMenuDtoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RestaurantId")] DailyMenuDto model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(id, new UpdateDailyMenuDto { RestaurantId = model.RestaurantId }).ConfigureAwait(false);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailyMenuDtoExists(model.Id))
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
            return View(model);
        }

        // GET: DailyMenuDtoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyMenuDto = await _service.QueryAsync(new DailyMenuQueryDto { Id = id, Page = 1, Size = 1 }).ConfigureAwait(false);
            if (dailyMenuDto.Total == 0)
            {
                return NotFound();
            }

            return View(dailyMenuDto.Items.First());
        }

        // POST: DailyMenuDtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteByIdAsync(id).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        private bool DailyMenuDtoExists(int id)
        {
            return _service.QueryAsync(new DailyMenuQueryDto { Id = id, Page = 1, Size = 1 }).Result.Total != 0;
        }
    }
}
