using BasicWebAPI.DbContexts;
using BasicWebAPI.Interface;
using BasicWebAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BasicWebAPI.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;
        public CountryRepository(DataContext context) 
        {
            _context = context;
        }  
        public bool CountryExists(int countryId)
        {
            return _context.Contacts.Any(p => p.ContactId == countryId);
        }

        public bool CreateCountry(Country country)
        {
            _context.Add(country);
            return Save();
        }

        public bool DeleteCountry(Country country)
        {
            _context.Remove(country);
            return Save();
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Countries.OrderBy(p => p.CountryId).ToList();
        }

        public Country GetCountryByName(string name)
        {
            return _context.Countries.Where(p => p.CountryName == name).FirstOrDefault();
        }

        public Country GetCountrytById(int id)
        {
            return _context.Countries.Where(p => p.CountryId == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCountry(Country country)
        {
            _context.Update(country);
            return Save();
        }
    }
}
