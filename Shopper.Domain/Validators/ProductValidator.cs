using FluentValidation;
using Shopper.Domain.Models;
using Shopper.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.Domain.Validators
{
    // dotnet add package FluentValidation
    public class ProductValidator : AbstractValidator<Product>
    {
      //  private readonly IProductRepository productRepository;

        public ProductValidator()
        {
       //     this.productRepository = productRepository;
      
            RuleFor(p => p.Name).NotNull().MaximumLength(50);
            RuleFor(p => p.Description).NotNull().MaximumLength(50);
            RuleFor(p => p.Discount).GreaterThan(0).When(p => p.HasDiscount);
            RuleFor(p => p.Supplier).Test();
        }     
    }

    public static class Ext
    {
        public static IRuleBuilderOptions<T, TProperty?> Test<T, TProperty>(this IRuleBuilder<T, TProperty?> ruleBuilder)
        {            
            return ruleBuilder.NotNull();
        }
    }
}
