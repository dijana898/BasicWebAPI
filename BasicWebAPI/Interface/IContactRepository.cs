using BasicWebAPI.Models;

namespace BasicWebAPI.Interface
{
    public interface IContactRepository
    {
        ICollection<Contact> GetContacts();
        Contact GetContactById(int id);
        Contact GetContactByName(string name);
        bool ContactExists(int contactId);
        bool CreateContact(Contact contact);
        bool UpdateContact(Contact contact);
        bool DeleteContact(Contact contact);
        bool Save();
    }
}
