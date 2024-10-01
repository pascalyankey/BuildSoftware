using API.Dto;
using API.Filters;
using AutoMapper;
using Domain.Commands;
using Domain.Models;
using Domain.Queries;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [TypeFilter(typeof(CustomExceptionFilter))]
    public class WerknemerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public WerknemerController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET: api/Werknemers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WerknemerDto>>> GetWerknemers() 
            => _mapper.Map<List<WerknemerDto>>(await _mediator.Send(new GetWerknemerListQuery()));

        // GET: api/Werknemers/Naam
        [HttpGet("{werfNaam}")]
        public async Task<ActionResult<IEnumerable<WerknemerDto>>> GetWerknemersByWerfNaam(string werfNaam) 
            => _mapper.Map<List<WerknemerDto>>(await _mediator.Send(new GetWerknemerListByWerfNaamQuery(werfNaam)));

        // POST: api/Werknemers
        [HttpPost]
        public async Task<Dictionary<int, WerfWerknemerInsertedResponse>> Post([FromBody] List<InsertWerknemerCommandDto> value) =>
            await _mediator.Send(new InsertWerknemersCommand(_mapper.Map<List<Werknemer>>(value)));


        // DELETE: api/Werknemers/5
        [HttpDelete("{id}")]
        public async Task<Unit> Delete(int id)
            => await _mediator.Send(new DeleteWerknemerCommand(id));

        // POST: api/Werknemers
        [HttpPost("Login")]
        public async Task<ActionResult<WerknemerToken>> Login([FromBody] LoginCommandDto value) =>
            await _mediator.Send(new LoginCommand(value.Emailadres, value.Wachtwoord));
    }
}
