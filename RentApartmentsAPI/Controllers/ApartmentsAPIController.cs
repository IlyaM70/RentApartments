using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentApartmentsAPI.Data;
using RentApartmentsAPI.Models;
using RentApartmentsAPI.Models.Dto;

namespace RentApartmentsAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/ApartmentsAPI")]
    [ApiController]
    public class ApartmentsAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ApartmentsAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet("GetApartments")]
        public async Task<ActionResult<IEnumerable<ApartmentDTO>>> GetApartments()
        {
            IEnumerable<Apartment> apartmentList = await _db.Apartments.ToListAsync();
            return Ok(_mapper.Map<List<ApartmentDTO>>(apartmentList));
        }

        [HttpGet("GetApartment/{id:int}", Name = "GetApartment")]
        //[ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApartmentDTO>> GetApartment(int id)
        {
            if (id == 0)
            {
               return BadRequest();
            }

            Apartment? apartment = await _db.Apartments.FirstOrDefaultAsync(x => x.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ApartmentDTO>(apartment));
        }

        [HttpPost("CreateApartment")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApartmentDTO>> CreateApartment([FromBody]ApartmentDTO apartmentDTO)
        {

            if (apartmentDTO == null)
            {
                return BadRequest(apartmentDTO);
            }
            if (apartmentDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Apartment apartment = _mapper.Map<Apartment>(apartmentDTO);

            await _db.Apartments.AddAsync(apartment);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetApartment",new {id=apartmentDTO.Id}, apartmentDTO);
        }

        [HttpDelete("DeleteApartment/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteApartment(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var apartment = await _db.Apartments.FirstOrDefaultAsync(x => x.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }
            _db.Apartments.Remove(apartment);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("UpdateApartment/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateApartment(int id, [FromBody]ApartmentDTO apartmentDTO)
        {
            if (apartmentDTO==null||id!=apartmentDTO.Id)
            {
                return BadRequest();
            }

            Apartment apartment = _mapper.Map<Apartment>(apartmentDTO);

            _db.Apartments.Update(apartment);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("UpdatePartialApartment/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialApartment(int id,
            [FromBody] JsonPatchDocument<ApartmentDTO> patchDTO )
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var apartment = await _db.Apartments.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }

            ApartmentDTO apartmentDTO = _mapper.Map<ApartmentDTO>(apartment);


            patchDTO.ApplyTo(apartmentDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Apartment apartmentToUpdate = _mapper.Map<Apartment>(apartmentDTO);

            _db.Apartments.Update(apartmentToUpdate);
            await _db.SaveChangesAsync();


            return NoContent();
        }

    }
}
