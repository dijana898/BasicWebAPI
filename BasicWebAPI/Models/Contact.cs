﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicWebAPI.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        //public int CompanyId { get; set; }
        //public int CountryId { get; set; }
        public Company Company { get; set; }
        public Country Country { get; set; }

    }
}
