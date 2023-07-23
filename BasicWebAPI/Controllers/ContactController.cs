using AutoMapper;
using BasicWebAPI.Dto;
using BasicWebAPI.Interface;
using BasicWebAPI.Models;
using BasicWebAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BasicWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public ContactController(IContactRepository contactRepository, ICompanyRepository companyRepository, ICountryRepository countryRepository, IMapper mapper) 
        {
            _contactRepository = contactRepository;
            _companyRepository = companyRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Contact>))]

        public IActionResult GetContacts() 
        {
            var contacts = _mapper.Map<List<ContactDto>>(_contactRepository.GetContacts());

            if(!ModelState.IsValid) 
                return BadRequest (ModelState); 
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Contact))]
        [ProducesResponseType(400)]
        public IActionResult GetContact(int id)
        {
            if (!_contactRepository.ContactExists(id))
                return NotFound();

            var contact = _mapper.Map<ContactDto>(_contactRepository.GetContactById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(contact);
        }

        
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateContact([FromQuery] int companyId, [FromQuery] int countryId, [FromBody] ContactDto contactCreate)
        {
            if (contactCreate == null)
                return BadRequest(ModelState);

            var contact = _contactRepository.GetContacts()
                .Where(c => c.ContactName.Trim().ToUpper() == contactCreate.ContactName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (contact != null)
            {
                ModelState.AddModelError("", "Contact already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contactMap = _mapper.Map<Contact>(contactCreate);

            contactMap.Company = _companyRepository.GetCompanyById(companyId);
            contactMap.Country = _countryRepository.GetCountrytById(countryId);

            if (!_contactRepository.CreateContact(contactMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{contactId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int contactId, [FromBody] ContactDto updatedContact)
        {
            if (updatedContact == null)
                return BadRequest(ModelState);

            if (contactId != updatedContact.ContactId)
                return BadRequest(ModelState);

            if (!_contactRepository.ContactExists(contactId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var contactMap = _mapper.Map<Contact>(updatedContact);

            if (!_contactRepository.UpdateContact(contactMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteContact(int id)
        {
            if (!_contactRepository.ContactExists(id))
            {
                return NotFound();
            }

            var contactToDelete = _contactRepository.GetContactById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_contactRepository.DeleteContact(contactToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }

    }
}
