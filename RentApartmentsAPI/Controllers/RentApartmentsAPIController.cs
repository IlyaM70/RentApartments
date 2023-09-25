using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentApartmentsAPI.Data;
using RentApartmentsAPI.Models;
using RentApartmentsAPI.Models.Dto;

namespace RentApartmentsAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/RentApartmentsAPI")]
    [ApiController]
    public class RentApartmentsAPIController : ControllerBase
    {
        private readonly AppDbContext _db;

        public RentApartmentsAPIController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ApartmentDTO>> GetApartments()
        {
            return Ok(_db.Apartments.ToList());
        }

        [HttpGet("{id:int}", Name = "GetApartment")]
        //[ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ApartmentDTO> GetApartment(int id)
        {
            if (id == 0)
            {
               return BadRequest();
            }

            var apartment = _db.Apartments.FirstOrDefault(x => x.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return Ok(apartment);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ApartmentDTO> CreateApartment([FromBody]ApartmentDTO apartmentDTO)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //check if name unique
            if (_db.Apartments
                .FirstOrDefault(x => x.Name.ToLower() == apartmentDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("NameNotUnique", "Apartment already exist!");
                return BadRequest(ModelState);
            }


            if (apartmentDTO == null)
            {
                return BadRequest(apartmentDTO);
            }
            if (apartmentDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Apartment apartment = new()
            {
                Id = apartmentDTO.Id,
                Name = apartmentDTO.Name,
                Occupancy = apartmentDTO.Occupancy,
                ImageUrl = apartmentDTO.ImageUrl,
                Rate = apartmentDTO.Rate,
                Sqft = apartmentDTO.Sqft,
                Description = apartmentDTO.Description,
                Amenity = apartmentDTO.Amenity,
            };

            _db.Apartments.Add(apartment);
            _db.SaveChanges();

            return CreatedAtRoute("GetApartment",new {id=apartmentDTO.Id}, apartmentDTO);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteApartment(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var apartment = _db.Apartments.FirstOrDefault(x => x.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }
            _db.Apartments.Remove(apartment);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateApartment(int id, [FromBody]ApartmentDTO apartmentDTO)
        {
            if (apartmentDTO==null||id!=apartmentDTO.Id)
            {
                return BadRequest();
            }

            Apartment apartment = new()
            {
                Id = apartmentDTO.Id,
                Name = apartmentDTO.Name,
                Occupancy = apartmentDTO.Occupancy,
                ImageUrl = apartmentDTO.ImageUrl,
                Rate = apartmentDTO.Rate,
                Sqft = apartmentDTO.Sqft,
                Description = apartmentDTO.Description,
                Amenity = apartmentDTO.Amenity,
            };

            _db.Apartments.Update(apartment);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialApartment(int id,
            [FromBody] JsonPatchDocument<ApartmentDTO> patchDTO )
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var apartment = _db.Apartments.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }

            ApartmentDTO apartmentDTO = new()
            {
                Id = apartment.Id,
                Name = apartment.Name,
                Occupancy = apartment.Occupancy,
                ImageUrl = apartment.ImageUrl,
                Rate = apartment.Rate,
                Sqft = apartment.Sqft,
                Description = apartment.Description,
                Amenity = apartment.Amenity,
            };




            patchDTO.ApplyTo(apartmentDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Apartment apartmentToUpdate = new()
            {
                Id = apartmentDTO.Id,
                Name = apartmentDTO.Name,
                Occupancy = apartmentDTO.Occupancy,
                ImageUrl = apartmentDTO.ImageUrl,
                Rate = apartmentDTO.Rate,
                Sqft = apartmentDTO.Sqft,
                Description = apartmentDTO.Description,
                Amenity = apartmentDTO.Amenity
            };

            _db.Apartments.Update(apartmentToUpdate);
            _db.SaveChanges();


            return NoContent();
        }

    }
}
