using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Exebite.DtoModels;
using WebClient.Services;
using Exebite.Common;
using Microsoft.EntityFrameworkCore;

namespace WebClient.Controllers
{
    public class FoodController : Controller
    {
        private readonly IFoodService _service;

        public FoodController(IFoodService service)
        {
            _service = service;
        }

        // GET: Food
        public async Task<IActionResult> Index()
        {
            var queryDto = new FoodQueryModelDto()
            {
                Page = 1,
                Size = QueryConstants.MaxElements - 1
            };
            var res = await _service.QueryAsync(queryDto).ConfigureAwait(false);
            return View(res.Items);
        }

        // GET: Food/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodDto = await _service
                .QueryAsync(new FoodQueryModelDto { Id = id, Page = 1, Size = 1 })
                .ConfigureAwait(false);
            if (foodDto.Total == 0)
            {
                return NotFound();
            }

            return View(foodDto.Items.First());
        }

        // GET: Food/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Food/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Price,RestaurantId,Description,IsInactive")] FoodDto foodDto)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(new CreateFoodDto
                {
                    Description = foodDto.Description,
                    IsInactive = foodDto.IsInactive,
                    Name = foodDto.Name,
                    Price = foodDto.Price,
                    RestaurantId = foodDto.RestaurantId,
                    Type = foodDto.Type
                }).ConfigureAwait(false);
                return RedirectToAction(nameof(Index));
            }
            return View(foodDto);
        }

        // GET: Food/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodDto = await _service
                .QueryAsync(new FoodQueryModelDto { Id = id, Page = 1, Size = 1 })
                .ConfigureAwait(false);
            if (foodDto.Total == 0)
            {
                return NotFound();
            }

            return View(foodDto.Items.First());
        }

        // POST: Food/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,Price,RestaurantId,Description,IsInactive")] FoodDto foodDto)
        {
            if (id != foodDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(id, new UpdateFoodDto
                    {
                        Description = foodDto.Description,
                        IsInactive = foodDto.IsInactive,
                        Name = foodDto.Name,
                        Price = foodDto.Price,
                        RestaurantId = foodDto.RestaurantId,
                        Type = foodDto.Type
                    }).ConfigureAwait(false);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodDtoExists(foodDto.Id))
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
            return View(foodDto);
        }

        // GET: Food/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodDto = await _service
                .QueryAsync(new FoodQueryModelDto { Id = id, Page = 1, Size = 1 })
                .ConfigureAwait(false);

            if (foodDto == null)
            {
                return NotFound();
            }

            return View(foodDto);
        }

        // POST: Food/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool FoodDtoExists(int id)
        {
            return _service
                .QueryAsync(new FoodQueryModelDto { Id = id, Page = 1, Size = 1 })
                .Result.Total != 0;
        }
    }
}
