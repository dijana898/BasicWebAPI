using BasicWebAPI.DbContexts;
using BasicWebAPI.Interface;
using BasicWebAPI.Models;
using System.ComponentModel.Design;

namespace BasicWebAPI.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext _context;

        public CompanyRepository(DataContext context)
        {
            _context = context;
        }
        public bool CompanyExists(int companyId)
        {
            return _context.Companies.Any(p => p.CompanyId == companyId);
        }

        public bool CreateCompany(Company company)
        {
            _context.Add(company);
            return Save();
        }

        public bool DeleteCompany(Company company)
        {
            _context.Remove(company);
            return Save();
        }

        public ICollection<Company> GetCompanies()
        {
            return _context.Companies.OrderBy(p => p.CompanyId).ToList();
        }

        public Company GetCompanyById(int id)
        {
            return _context.Companies.Where(p => p.CompanyId == id).FirstOrDefault();
        }

        public Company GetCompanyByName(string name)
        {
            return _context.Companies.Where(p => p.CompanyName == name).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCompany(Company company)
        {
            _context.Update(company);
            return Save();
        }
    }
}
