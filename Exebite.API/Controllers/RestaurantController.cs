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
    public class RestaurantController : ControllerBase
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
                            .Map(x => (IActionResult)Ok(_mapper.Map<IEnumerable<RestaurantDto>>(x.Items)))
                            .Reduce(InternalServerError);

        [HttpGet("{id}")]
        public IActionResult Get(int id) =>
            _queryRepository.Query(new RestaurantQueryModel { Id = id })
                            .Map(x => (IActionResult)Ok(_mapper.Map<IEnumerable<RestaurantDto>>(x.Items)))
                            .Reduce(_ => (IActionResult)BadRequest(), error => error is ArgumentNotSet)
                            .Reduce(InternalServerError);

        [HttpPost]
        public IActionResult Post([FromBody]RestaurantInsertModelDto restaurant) =>
            _commandRepository.Insert(_mapper.Map<RestaurantInsertModel>(restaurant))
                              .Map(x => Created(new { id = x }))
                              .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                              .Reduce(InternalServerError);

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]RestaurantUpdateModelDto restaurant) =>
            _commandRepository.Update(id, _mapper.Map<RestaurantUpdateModel>(restaurant))
                              .Map(x => AllOk(new { updated = x }))
                              .Reduce(_ => NotFound(), error => error is RecordNotFound)
                              .Reduce(InternalServerError);

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _commandRepository.Delete(id)
                              .Map(_ => (IActionResult)NoContent())
                              .Reduce(_ => (IActionResult)NotFound(), error => error is RecordNotFound)
                              .Reduce(InternalServerError);

        [HttpGet("Query")]
        public IActionResult Query([FromQuery]RestaurantQueryDto query) =>
            _queryRepository.Query(_mapper.Map<RestaurantQueryModel>(query))
                            .Map(x => (IActionResult)Ok(_mapper.Map<IEnumerable<RestaurantDto>>(x.Items)))
                            .Reduce(_ => (IActionResult)BadRequest(), error => error is ArgumentNotSet)
                            .Reduce(InternalServerError);

        private IActionResult Created<T>(T content) =>
            StatusCode(StatusCodes.Status201Created, content);

        private IActionResult AllOk<T>(T content) =>
            StatusCode(StatusCodes.Status200OK, content);
    }
}
