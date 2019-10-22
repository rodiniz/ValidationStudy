namespace ValidationTests
{
    using AutoFixture;
    using System.Collections.Generic;
    using System.Linq;
    using ValidationStudy;
    using ValidationStudy.Validators;
    using Xunit;

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
            var adresses = fixture.Build<Address>()
              .With(c => c.Default,true)
              .CreateMany(1).ToList();
         
            Customer customer = fixture.Build<Customer>()
                .With(c => c.Address, adresses)
                .Create();
            // Act
            var result = validator.Validate(customer);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void CustomerNameShouldNotBeEmpty()
        {
            // Arrange
            var adresses = fixture.Build<Address>()
             .With(c => c.Default, true)
             .CreateMany(1).ToList();

            Customer customer = fixture.Build<Customer>()
                .With(c => c.Address, adresses)
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
                .With(c => c.Default,true)
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

        [Fact]
        public void CustomerMustHaveOneDefaultAdress()
        {
            // Arrange
            var adresses = fixture.Build<Address>()
              .With(c => c.Default, false)
              .CreateMany(1).ToList();

            Customer customer = fixture.Build<Customer>()
                .With(c => c.Address, adresses)
                .Create();
            // Act
            var result = validator.Validate(customer);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Adress list must have one default adress and only one", result.Errors.First().ErrorMessage);
        }
    }
}
