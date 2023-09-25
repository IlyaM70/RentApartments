using AutoMapper;
using RentApartmentsAPI.Models;
using RentApartmentsAPI.Models.Dto;

namespace RentApartmentsAPI
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<Apartment, ApartmentDTO>();
            CreateMap<ApartmentDTO, Apartment>();
        }
    }
}
