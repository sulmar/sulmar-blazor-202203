using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Shopper.Domain.Models;
using Shopper.Domain.Services;

namespace Shopper.BlazorWebAssembly.Pages.Customers
{
    public partial class CustomerList 
    {
        private int totalCount;

        [Inject]
        public ICustomerService CustomerService { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            totalCount = await CustomerService.GetCount();

            StateHasChanged();
        }

        private async ValueTask<ItemsProviderResult<Customer>> LoadCustomers(ItemsProviderRequest request)
        {
            // GET api/customers?startIndex={0}&count={1}
            var items = await CustomerService.GetAsync();

            var _customers = items.Skip(request.StartIndex).Take(request.Count).ToList();

            return new ItemsProviderResult<Customer>(_customers, totalCount);


        }

    }
}
