using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    public interface ITicketService
    {
        Task BookTicket(TicketDTO ticket);
        Task<IEnumerable<Place>> GetBookedPlaces(Guid perfomanceId);

    }
    public class TicketService : ITicketService
    {
        private readonly ApplicationContext _dbContext;

        public TicketService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task BookTicket(TicketDTO ticket)
        {
            ArgumentNullException.ThrowIfNull(nameof(ticket));
            if (ticket.PerfomanceId == default)
            {
                throw new ArgumentException();
            }
            if (await _dbContext.Tickets.AnyAsync(x => x.PerfomanceId == ticket.PerfomanceId && x.Row == ticket.Row && x.Place == ticket.Place))
            {
                throw new ArgumentException("Place already booked", nameof(ticket));
            }
            Ticket newTicket = new Ticket()
            {
                Email = ticket.Email,
                Row = ticket.Row,
                Place = ticket.Place,
                Price = ticket.Price,
                Name = ticket.Name,
                PerfomanceId = ticket.PerfomanceId,
            };
            await _dbContext.Tickets.AddAsync(newTicket);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Place>> GetBookedPlaces(Guid perfomanceId)
        {
            return await _dbContext.Tickets.Where(x => x.PerfomanceId == perfomanceId).Select(x => new Place(x.Row, x.Place)).ToArrayAsync();
        }
    }
}
