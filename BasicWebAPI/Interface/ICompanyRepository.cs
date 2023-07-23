using BasicWebAPI.Models;

namespace BasicWebAPI.Interface
{
    public interface ICompanyRepository
    {
        ICollection<Company> GetCompanies();

        Company GetCompanyById(int id);
        Company GetCompanyByName(string name);
        bool CompanyExists(int companyId);
        bool CreateCompany(Company company);
        bool UpdateCompany(Company company);
        bool DeleteCompany(Company company);
        bool Save();
    }
}
