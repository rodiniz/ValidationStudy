namespace ValidationStudy.Validators
{
    using FluentValidation;
    using System.Linq;

    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.Id).NotEmpty();
            RuleFor(customer => customer.Name).NotNull().NotEmpty();
            RuleFor(customer => customer.Surname).NotNull().NotEmpty();

            RuleFor(x => x.Address)
              .Must(x => x.Count > 0);

            RuleForEach(x => x.Address)             
              .SetValidator(new AddressValidator()).When(c=> c.Address.Any());

        }
    }
}
