using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        ///GET api/tickets/
        [HttpGet]
        public async Task<IEnumerable<Place>> GetPlaces([FromQuery] Guid perfomanceId)
        {
            return await _ticketService.GetBookedPlaces(perfomanceId);
        }

        ///POST api/tickets
        [HttpPost]
        public async Task BookTicket([FromBody] TicketDTO ticket)
        {
            await _ticketService.BookTicket(ticket);
        }
    }
}
