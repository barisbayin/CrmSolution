using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Personnel : IEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string IdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullAddress { get; set; } = null;
        public string AddressLine { get; set; } = null;
        public int? CountryId { get; set; } = null;
        public int? CityId { get; set; } = null;
        public int? CountyId { get; set; } = null;
        public int? NeighbourhoodId { get; set; } = null;
        public string ZipCode { get; set; } = null;
        public GenderInformation? genderInformation { get; set; } = null;
        public string ImagePath { get; set; } = null;
        public Department Department { get; set; } = null;
        public bool IsUser { get; set; } = false;
    }
}
