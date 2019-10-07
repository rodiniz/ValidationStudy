using AutoFixture;
using System.Collections.Generic;
using System.Linq;
using ValidationStudy;
using ValidationStudy.Validators;
using Xunit;

namespace ValidationTests
{
    public class CustomerValidatorTests
    {
        Fixture fixture;
        CustomerValidator validator;
        public CustomerValidatorTests()
        {
            this.fixture = new Fixture();
            this.validator = new CustomerValidator();
        }
        [Fact]
        public void CustomerShouldBeValid()
        {
            // Arrange
            Customer customer = fixture.Create<Customer>();

            // Act
            var result = validator.Validate(customer);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void CustomerNameShouldNotBeEmpty()
        {
            // Arrange
            Customer customer = fixture.Build<Customer>()
                .With(c => c.Name, string.Empty)
                .Create();

            // Act
            var result = validator.Validate(customer);

            // Assert
            Assert.False(result.IsValid);
            Assert.True(result.Errors[0].PropertyName == "Name");
        }

        [Fact]
        public void CustomerAdressMustBeFilled()
        {
            // Arrange
            Customer customer = fixture.Build<Customer>()
                .With(c => c.Address, new List<Address>())
                .Create();

            // Act
            var result = validator.Validate(customer);

            // Assert
            Assert.False(result.IsValid);
            Assert.True(result.Errors.First().PropertyName == "Address");
        }

        [Fact]
        public void CustomerAdressWithNoNumber()
        {
            // Arrange           
            var adresses = fixture.Build<Address>()              
                .Without(c=> c.Number) 
                .CreateMany(1).ToList();

          
            Customer customer = fixture.Build<Customer>()
                .With(c => c.Address, adresses)
                .Create();

            // Act
            var result = validator.Validate(customer);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains("Number", result.Errors.First().PropertyName);
        }
    }
}
