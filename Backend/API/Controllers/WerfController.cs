using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using MediatR;
using Domain.Queries;
using Domain.Commands;
using AutoMapper;
using API.Filters;
using API.Dto;
using Microsoft.AspNetCore.Authorization;
using Domain.Responses;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [TypeFilter(typeof(CustomExceptionFilter))]
    public class WerfController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public WerfController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET: api/Werven
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WerfDto>>> GetWerven() 
            => _mapper.Map<List<WerfDto>>(await _mediator.Send(new GetWervenListQuery()));

        // GET: api/Werven/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<WerfDto>> Get(int id) 
            => _mapper.Map<WerfDto>(await _mediator.Send(new GetWerfByIdQuery(id)));

        // POST api/Werven
        [HttpPost]
        public async Task<Dictionary<int, WerfWerknemerInsertedResponse>> Post([FromBody] InsertWerfCommandDto value)
            => await _mediator.Send(new InsertWerfCommand(value.Naam, value.StartDatum, value.EindDatum, value.Status, _mapper.Map<List<Werknemer>>(value.Werknemers)));

        // DELETE api/werven/5
        [HttpDelete("{id}"), Authorize]
        public async Task<Unit> Delete(int id)
            => await _mediator.Send(new DeleteWerfCommand(id));
    }
}
