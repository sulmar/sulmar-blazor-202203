using Bogus;
using Shopper.Domain.Models;

namespace Shopper.Infrastructure.Fakers;

public class CustomerFaker : Faker<Customer>
{
    public CustomerFaker()
    {
        StrictMode(true);
        RuleFor(p => p.Id, f => f.IndexFaker);
        RuleFor(p => p.FirstName, f => f.Person.FirstName);
        RuleFor(p => p.LastName, f => f.Person.LastName);
        RuleFor(p => p.Gender, f=> (Gender) f.Person.Gender);
        RuleFor(p => p.Salary, f => f.Random.Number(100, 1000).OrNull(f, 0.2f));
        RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.2f).OrNull(f, 0.1f));

        Ignore(p => p.Password);
        Ignore(p => p.ConfirmPassword);

    }

}

