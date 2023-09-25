using RentApartmentsAPI.Models;

namespace RentApartmentsAPI.Repositories.RepositoryInterfaces
{
    public interface IApartmentRepository: IRepository<Apartment>
    {
        Task<Apartment> UpdateAsync(Apartment entity);
    }
}
