using System.Linq;
using System.Threading.Tasks;
using Exebite.Common;
using Exebite.DtoModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebClient.Services;

namespace WebClient.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        // GET: Customer
        public async Task<IActionResult> Index()
        {
            var queryDto = new CustomerQueryDto()
            {
                Page = 1,
                Size = QueryConstants.MaxElements - 1
            };
            var res = await _service.QueryAsync(queryDto).ConfigureAwait(false);
            return View(res.Items);
        }

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerDto = await _service.QueryAsync(new CustomerQueryDto { Id = id, Page = 1, Size = 1 }).ConfigureAwait(false);
            if (customerDto == null)
            {
                return NotFound();
            }

            return View(customerDto.Items.First());
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Balance,LocationId,GoogleUserId,RoleId")] CustomerDto model)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(new CreateCustomerDto
                {
                    Name = model.Name,
                    Balance = model.Balance,
                    GoogleUserId = model.GoogleUserId,
                    LocationId = model.LocationId
                }).ConfigureAwait(false);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerDto = await _service.QueryAsync(new CustomerQueryDto { Id = id, Page = 1, Size = 1 }).ConfigureAwait(false);
            if (customerDto.Total == 0)
            {
                return NotFound();
            }
            return View(customerDto.Items.First());
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Balance,LocationId,GoogleUserId,RoleId")] CustomerDto model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(id, new UpdateCustomerDto
                    {
                        Name = model.Name,
                        Balance = model.Balance,
                        GoogleUserId = model.GoogleUserId,
                        LocationId = model.LocationId ?? 0,
                        RoleId = model.RoleId ?? 0
                    }).ConfigureAwait(false);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerDtoExists(model.Id))
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

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerDto = await _service.QueryAsync(new CustomerQueryDto { Id = id, Page = 1, Size = 1 }).ConfigureAwait(false);
            if (customerDto.Total == 0)
            {
                return NotFound();
            }

            return View(customerDto);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteByIdAsync(id).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerDtoExists(int id)
        {
            return _service.QueryAsync(new CustomerQueryDto { Id = id, Page = 1, Size = 1 }).Result.Total != 0;
        }
    }
}
