using Microsoft.AspNetCore.Components;
using Shopper.Domain.Models;
using Shopper.Domain.Services;

namespace Shopper.BlazorWebAssembly.Pages.Customers
{
    public partial class CustomerList 
    {
        private IEnumerable<Customer> customers;

        [Inject]
        public ICustomerService CustomerService { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            customers = await CustomerService.GetAsync();

            StateHasChanged();
        }

    }
}
