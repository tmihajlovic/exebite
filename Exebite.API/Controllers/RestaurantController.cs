using System.Collections.Generic;
using AutoMapper;
using Either;
using Exebite.API.Models;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/restaurant")]
    [Authorize]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantQueryRepository _queryRepository;
        private readonly IRestaurantCommandRepository _commandRepository;
        private readonly IMapper _mapper;

        public RestaurantController(IRestaurantQueryRepository queryRepository, IRestaurantCommandRepository commandRepository, IMapper mapper)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int page, int size) =>
            _queryRepository.Query(new RestaurantQueryModel(page, size))
                            .Map(x => (IActionResult)Ok(_mapper.Map<IEnumerable<RestaurantModel>>(x)))
                            .Reduce(x => StatusCode(500, x));

        [HttpGet("{id}")]
        public IActionResult Get(int id) =>
                        _queryRepository.Query(new RestaurantQueryModel { Id = id })
                            .Map(x => (IActionResult)Ok(_mapper.Map<IEnumerable<RestaurantModel>>(x)))
                            .Reduce(_ => NotFound());

        [HttpPost]
        public IActionResult Post([FromBody]string name) =>
            _commandRepository.Insert(new RestaurantInsertModel { Name = name })
                              .Map(x => (IActionResult)Ok(x))
                              .Reduce(_ => BadRequest());

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]string name) =>
            _commandRepository.Update(id, new RestaurantUpdateModel { Name = name })
                              .Map(x => (IActionResult)Ok(_mapper.Map<RestaurantModel>(x)))
                              .Reduce(_ => NotFound());

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _commandRepository.Delete(id)
                .Map(_ => (IActionResult)NoContent())
                .Reduce(x => StatusCode(500, x));

        [HttpGet("Query")]
        public IActionResult Query(RestaurantQueryDto query) =>
            _queryRepository.Query(_mapper.Map<RestaurantQueryModel>(query))
                            .Map(x => (IActionResult)Ok(_mapper.Map<IEnumerable<RestaurantModel>>(x)))
                            .Reduce(x => StatusCode(500, x));
    }
}
