using AutoMapper;
using BasicWebAPI.Dto;
using BasicWebAPI.Interface;
using BasicWebAPI.Models;
using BasicWebAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasicWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        public CompanyController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Company>))]

        public IActionResult GetCompanies()
        {
            var companies = _mapper.Map<List<CompanyDto>>(_companyRepository.GetCompanies());
            //var companies = _companyRepository.GetCompanies();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(companies);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Company))]
        [ProducesResponseType(400)]
        public IActionResult GetCompany(int id)
        {
            if (!_companyRepository.CompanyExists(id))
                return NotFound();

            var company = _mapper.Map<CompanyDto>(_companyRepository.GetCompanyById(id));

            //var company = _companyRepository.GetCompanyById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(company);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCompany([FromBody] CompanyDto companyCreate)
        {
            if (companyCreate == null)
                return BadRequest(ModelState);

            var company = _companyRepository.GetCompanies()
                .Where(c => c.CompanyName.Trim().ToUpper() == companyCreate.CompanyName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (company != null)
            {
                ModelState.AddModelError("", "Company already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var companyMap = _mapper.Map<Company>(companyCreate);

            if (!_companyRepository.CreateCompany(companyMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCompany(int id, [FromBody] CompanyDto updatedCompany)
        {
            if (updatedCompany == null)
                return BadRequest(ModelState);

            if (id != updatedCompany.CompanyId)
                return BadRequest(ModelState);

            if (!_companyRepository.CompanyExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var companyMap = _mapper.Map<Company>(updatedCompany);

            if (!_companyRepository.UpdateCompany(companyMap))
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
        public IActionResult DeleteCompany(int id)
        {
            if (!_companyRepository.CompanyExists(id))
            {
                return NotFound();
            }

            var companyToDelete = _companyRepository.GetCompanyById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_companyRepository.DeleteCompany(companyToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }

    }
}
