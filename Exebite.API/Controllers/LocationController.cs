using System.Collections.Generic;
using AutoMapper;
using Either;
using Exebite.API.Models;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/location")]
    [Authorize]
    public class LocationController : Controller
    {
        private readonly ILocationCommandRepository _locationCommandRepository;
        private readonly ILocationQueryRepository _locationQueryRepository;
        private readonly IMapper _mapper;

        public LocationController(
            ILocationCommandRepository locationCommandRepository,
            ILocationQueryRepository locationQueryRepository,
            IMapper mapper)
        {
            _locationCommandRepository = locationCommandRepository;
            _locationQueryRepository = locationQueryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get() =>
            _locationQueryRepository.Query(new LocationQueryModel())
                                    .Map(x => (IActionResult)Ok(_mapper.Map<IEnumerable<LocationModel>>(x.Items)))
                                    .Reduce(InternalServerError);

        [HttpGet("{id}")]
        public IActionResult Get(int id) =>
             _locationQueryRepository.Query(new LocationQueryModel { Id = id })
                                     .Map(x => (IActionResult)Ok(_mapper.Map<IEnumerable<RestaurantModel>>(x.Items)))
                                     .Reduce(_ => (IActionResult)NotFound(), error => error is RecordNotFound)
                                     .Reduce(InternalServerError);

        [HttpPost]
        public IActionResult Post([FromBody]CreateLocationModel model) =>
            _locationCommandRepository.Insert(_mapper.Map<LocationInsertModel>(model))
                                      .Map(x => (IActionResult)Ok(x))
                                      .Reduce(_ => (IActionResult)BadRequest(), error => error is ArgumentNotSet)
                                      .Reduce(InternalServerError);

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateLocationModel model) =>
            _locationCommandRepository.Update(id, _mapper.Map<LocationUpdateModel>(model))
                                      .Map(x => (IActionResult)Ok(x))
                                      .Reduce(_ => (IActionResult)NotFound(), error => error is RecordNotFound)
                                      .Reduce(_ => (IActionResult)BadRequest(), error => error is ArgumentNotSet)
                                      .Reduce(InternalServerError);

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _locationCommandRepository.Delete(id)
                              .Map(_ => (IActionResult)NoContent())
                              .Reduce(_ => (IActionResult)NotFound(), error => error is RecordNotFound)
                              .Reduce(InternalServerError);

        [HttpGet("Query")]
        public IActionResult Query(LocationQueryModel query) =>
            _locationQueryRepository.Query(_mapper.Map<LocationQueryModel>(query))
                .Map(x => (IActionResult)Ok(_mapper.Map<IEnumerable<LocationModel>>(x.Items)))
                .Reduce(_ => (IActionResult)BadRequest(), error => error is ArgumentNotSet)
                .Reduce(InternalServerError);

        private IActionResult InternalServerError(Error error) =>
                StatusCode(StatusCodes.Status500InternalServerError, error);
    }
}
