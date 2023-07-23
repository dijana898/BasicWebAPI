using BasicWebAPI.DbContexts;
using BasicWebAPI.Interface;
using BasicWebAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BasicWebAPI.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly DataContext _context; 
        public ContactRepository(DataContext context) 
        {
            _context = context;
        }

        public Contact GetContactById(int id)
        {
            return _context.Contacts.Where(p => p.ContactId == id).FirstOrDefault();
        }

        public Contact GetContactByName(string name)
        {
            return _context.Contacts.Where(p => p.ContactName == name).FirstOrDefault();
        }

        public ICollection<Contact> GetContacts() 
        {
            return _context.Contacts.OrderBy(p => p.ContactId).ToList();
        }

        public Boolean ContactExists(int id) 
        {
            return _context.Contacts.Any(p => p.ContactId == id);
        }

        public bool CreateContact(Contact contact)
        {
            _context.Add(contact);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateContact(Contact contact)
        {
            _context.Update(contact);
            return Save();
        }

        public bool DeleteContact(Contact contact)
        {
            _context.Remove(contact);
            return Save();
        }
    }
}
