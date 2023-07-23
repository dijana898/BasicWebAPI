using BasicWebAPI.Models;

namespace BasicWebAPI.Interface
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();

        Country GetCountrytById(int id);
        Country GetCountryByName(string name);
        bool CountryExists(int countryId);
        bool CreateCountry(Country country);
        bool UpdateCountry(Country country);
        bool DeleteCountry(Country country);
        bool Save();
    }
}
