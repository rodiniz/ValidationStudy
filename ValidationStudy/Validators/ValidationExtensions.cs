namespace ValidationStudy.Validators
{
    using System.Linq;
    using FluentValidation;
    using System.Collections.Generic;
    
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, IList<Address>> MustHaveOneDefaultAdress<T>(this IRuleBuilder<T, IList<Address>> ruleBuilder)
        {
            return ruleBuilder.Must(list => list.Count(x=> x.Default) ==1 ).WithMessage("Adress list must have one default adress and only one");
        }
    }
}
