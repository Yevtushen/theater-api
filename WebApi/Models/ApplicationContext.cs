using Microsoft.EntityFrameworkCore;


namespace WebApi.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> o) : base(o) { }

        public DbSet<Perfomance> Perfomances => Set<Perfomance>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Ticket> Tickets => Set<Ticket>();

    }
}
