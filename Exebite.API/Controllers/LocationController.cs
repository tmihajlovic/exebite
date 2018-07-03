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
    [Route("api/location")]
    [Authorize]
    public class LocationController : ControllerBase
    {
        private readonly ILocationCommandRepository _commandRepository;
        private readonly ILocationQueryRepository _queryRepository;
        private readonly IMapper _mapper;

        public LocationController(
            ILocationCommandRepository commandRepository,
            ILocationQueryRepository queryRepository,
            IMapper mapper)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get() =>
            _queryRepository.Query(new LocationQueryModel())
                                    .Map(x => (IActionResult)Ok(_mapper.Map<IEnumerable<LocationModel>>(x.Items)))
                                    .Reduce(InternalServerError);

        [HttpGet("{id}")]
        public IActionResult Get(int id) =>
             _queryRepository.Query(new LocationQueryModel { Id = id })
                                     .Map(x => (IActionResult)Ok(_mapper.Map<IEnumerable<RestaurantModel>>(x.Items)))
                                     .Reduce(_ => (IActionResult)NotFound(), error => error is RecordNotFound)
                                     .Reduce(InternalServerError);

        [HttpPost]
        public IActionResult Post([FromBody]CreateLocationModel model) =>
            _commandRepository.Insert(_mapper.Map<LocationInsertModel>(model))
                                      .Map(x => (IActionResult)Ok(x))
                                      .Reduce(_ => (IActionResult)BadRequest(), error => error is ArgumentNotSet)
                                      .Reduce(InternalServerError);

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateLocationModel model) =>
            _commandRepository.Update(id, _mapper.Map<LocationUpdateModel>(model))
                                      .Map(x => (IActionResult)Ok(x))
                                      .Reduce(_ => (IActionResult)NotFound(), error => error is RecordNotFound)
                                      .Reduce(_ => (IActionResult)BadRequest(), error => error is ArgumentNotSet)
                                      .Reduce(InternalServerError);

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _commandRepository.Delete(id)
                              .Map(_ => (IActionResult)NoContent())
                              .Reduce(_ => (IActionResult)NotFound(), error => error is RecordNotFound)
                              .Reduce(InternalServerError);

        [HttpGet("Query")]
        public IActionResult Query(LocationQueryModel query) =>
            _queryRepository.Query(_mapper.Map<LocationQueryModel>(query))
                .Map(x => (IActionResult)Ok(_mapper.Map<IEnumerable<LocationModel>>(x.Items)))
                .Reduce(_ => (IActionResult)BadRequest(), error => error is ArgumentNotSet)
                .Reduce(InternalServerError);
    }
}
