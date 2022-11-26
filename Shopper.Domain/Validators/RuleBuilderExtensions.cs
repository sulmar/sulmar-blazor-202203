using FluentValidation;

namespace Shopper.Domain.Validators
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, TProperty?> MyRule<T, TProperty>(this IRuleBuilder<T, TProperty?> ruleBuilder)
        {            
            return ruleBuilder.NotNull();
        }

    }
}
