namespace ValidationStudy.Validators
{
    using FluentValidation;

    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(address => address.Number).NotNull().NotEmpty();
            RuleFor(address => address.Complement).NotNull().NotEmpty();
        }
    }
}
