using AutoMapper;
using BasicWebAPI.Controllers;
using BasicWebAPI.Dto;
using BasicWebAPI.Interface;
using BasicWebAPI.Models;
using BasicWebAPI.Repository;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Tests.Controller
{
    public class ContactControllerTests
    {
        private readonly IContactRepository _contactRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public ContactControllerTests()
        {
            _contactRepository = A.Fake<IContactRepository>();
            _companyRepository = A.Fake<ICompanyRepository>();
            _countryRepository = A.Fake<ICountryRepository>();
            _mapper = A.Fake<IMapper>(); 
        }

        [Fact]
        public void ContactController_GetContacts_ReturnOK()
        {
            //Arrange
            var contacts = A.Fake<ICollection<ContactDto>>();
            var contactList = A.Fake<List<ContactDto>>();
            A.CallTo(() => _mapper.Map<List<ContactDto>>(contacts)).Returns(contactList);
            var controller = new ContactController(_contactRepository, _companyRepository, _countryRepository, _mapper);

            //Act
            var result = controller.GetContacts();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

       
    }
}
