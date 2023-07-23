using BasicWebAPI.Models;

namespace BasicWebAPI.Dto
{
    public class ContactDto
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public Company Company { get; set; }
        public Country Country { get; set; }

    }
}
