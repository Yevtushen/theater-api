using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    public interface IPerfomanceService
    {
        Task<IEnumerable<Perfomance>> GetPerfomances(DateTimeOffset? dateTime, string? name);
        Task<Perfomance?> GetPerfomanceById(Guid id);
        Task SavePerfomance(Guid? id, PerfomanceDTO perfomance);
        Task CreatePerfomance(PerfomanceDTO perfomance);
        Task DeletePerfomance(Guid id);
    }

    public class PerfomanceService : IPerfomanceService
    {
        private readonly ApplicationContext _dbContext;
        public PerfomanceService(ApplicationContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task CreatePerfomance(PerfomanceDTO perfomance)
        {
            ArgumentNullException.ThrowIfNull(nameof(perfomance));
            if (await _dbContext.Perfomances.AnyAsync(x => x.DateAndTime == perfomance.DateAndTime)) 
            {
                throw new ArgumentException("Already have perfomance in that time", nameof(perfomance));
            }
            Perfomance newPerfomance = new Perfomance()
            {
                Name = perfomance.Name,
                Description = perfomance.Description,
                DateAndTime = perfomance.DateAndTime,
            };
            await _dbContext.Perfomances.AddAsync(newPerfomance);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePerfomance(Guid id)
        {
            await _dbContext.Perfomances.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<Perfomance?> GetPerfomanceById(Guid id)
        {
            return await _dbContext.Perfomances.FindAsync(id);
        }

        public async Task<IEnumerable<Perfomance>> GetPerfomances(DateTimeOffset? dateTime, string? name)
        {
            var q = _dbContext.Perfomances.AsQueryable();
            if (dateTime != null)
            {
                q = q.Where(q => q.DateAndTime >= dateTime);
            }
            if (name != null)
            {
                q = q.Where(q => q.Name == name);
            }
            return await q.ToArrayAsync();
        }

        public async Task SavePerfomance(Guid? id, PerfomanceDTO perfomance)
        {
            var dbPerfomance = id is null
                ? (await _dbContext.Perfomances.AddAsync(new Perfomance())).Entity
                : await GetPerfomanceById(id.Value) ?? throw new InvalidOperationException();

            dbPerfomance.Name = perfomance.Name;
            dbPerfomance.DateAndTime = perfomance.DateAndTime;
            dbPerfomance.Description = perfomance.Description;
        }
    }
}
