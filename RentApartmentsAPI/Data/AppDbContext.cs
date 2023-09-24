using Microsoft.EntityFrameworkCore;
using RentApartmentsAPI.Models;

namespace RentApartmentsAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<Apartment> Apartments { get; set; }
    }
}
