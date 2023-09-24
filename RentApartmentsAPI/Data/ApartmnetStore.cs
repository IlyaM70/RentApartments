using RentApartmentsAPI.Models.Dto;

namespace RentApartmentsAPI.Data
{
    public static class ApartmnetStore
    {
        public static List<ApartmentDTO> apartmentList = new List<ApartmentDTO>() {
            new ApartmentDTO{Id=1, Name= "Ocean View"},
            new ApartmentDTO{Id=2, Name = "River Side"}
        };
    }
}
