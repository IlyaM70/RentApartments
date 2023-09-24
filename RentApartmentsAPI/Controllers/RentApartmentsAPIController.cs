using Microsoft.AspNetCore.Mvc;
using RentApartmentsAPI.Data;
using RentApartmentsAPI.Models;
using RentApartmentsAPI.Models.Dto;

namespace RentApartmentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentApartmentsAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ApartmentDTO> GetApartments()
        {
            return ApartmnetStore.apartmentList;
        }
    }
}
