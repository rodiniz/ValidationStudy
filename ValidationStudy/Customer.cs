using System;
using System.Collections.Generic;

namespace ValidationStudy
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Discount { get; set; }
        public List<Address> Address { get; set; }
        public string Surname { get; set; }
    }
}
