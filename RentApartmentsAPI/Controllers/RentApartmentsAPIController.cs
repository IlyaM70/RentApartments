using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public ActionResult<IEnumerable<ApartmentDTO>> GetApartments()
        {
            return Ok(ApartmnetStore.apartmentList);
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

            var apartment = ApartmnetStore.apartmentList.FirstOrDefault(x => x.Id == id);
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
            if (ApartmnetStore.apartmentList
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
            apartmentDTO.Id =
                ApartmnetStore.apartmentList.OrderByDescending(x=>x.Id).FirstOrDefault().Id + 1;
            ApartmnetStore.apartmentList.Add(apartmentDTO);

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
            var apartment = ApartmnetStore.apartmentList.FirstOrDefault(x => x.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }
            ApartmnetStore.apartmentList.Remove(apartment);
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
            var apartment = ApartmnetStore.apartmentList.FirstOrDefault(x => x.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }
            apartment.Name = apartmentDTO.Name;
            apartment.Sqft = apartmentDTO.Sqft;
            apartment.Occupancy = apartmentDTO.Occupancy;

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
            var apartment = ApartmnetStore.apartmentList.FirstOrDefault(x => x.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }
            patchDTO.ApplyTo(apartment, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }

    }
}
