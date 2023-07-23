using BasicWebAPI.DbContexts;
using BasicWebAPI.Models;

namespace BasicWebAPI
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }

        public void SeedDataContext()
        {
            if (!dataContext.Contacts.Any())
            {
                var contacts = new List<Contact>()
                {
                    new Contact()
                    {
                        //ContactId = 1,
                        ContactName = "Contact1",

                        Company = new Company()
                        {
                            //CompanyId = 1,
                            CompanyName = "Company1"
                        },

                        Country = new Country()
                        {
                            //CountryId = 1,
                            CountryName = "Germany"
                        }
                    },
                    new Contact()
                    {
                        //ContactId = 2,
                        ContactName = "Contact2",

                        Company = new Company()
                        {
                            //CompanyId = 2,
                            CompanyName = "Company2"
                        },

                        Country = new Country()
                        {
                            //CountryId = 1,
                            CountryName = "Germany"
                        }
                    },
                    new Contact()
                    {
                        //ContactId = 3,
                        ContactName = "Contact3",

                        Company = new Company()
                        {
                            //CompanyId = 2,
                            CompanyName = "Company2"
                        },

                        Country = new Country()
                        {
                            //CountryId = 2,
                            CountryName = "UK"
                        }
                    },
                    new Contact()
                    {
                        //ContactId = 4,
                        ContactName = "Contact4",

                        Company = new Company()
                        {
                            //CompanyId = 1,
                            CompanyName = "Company1"
                        },

                        Country = new Country()
                        {
                            //CountryId = 2,
                            CountryName = "UK"
                        }
                    }
                };
                dataContext.Contacts.AddRange(contacts);
                dataContext.SaveChanges();
            }
        }
    }
}
