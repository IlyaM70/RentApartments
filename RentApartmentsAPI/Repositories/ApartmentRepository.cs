using RentApartmentsAPI.Data;
using RentApartmentsAPI.Models;
using RentApartmentsAPI.Repositories.RepositoryInterfaces;

namespace RentApartmentsAPI.Repositories
{
    public class ApartmentRepository : Repository<Apartment>, IApartmentRepository
    {
        private readonly AppDbContext _db;
        public ApartmentRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }


        public async Task<Apartment> UpdateAsync(Apartment entity)
        {
            entity.UpdateDate = DateTime.Now;
            _db.Apartments.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
