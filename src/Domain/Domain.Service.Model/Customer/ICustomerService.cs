using Core.Enumarations;
using Domain.Model.Customer;
using Domain.Service.Model.Customer.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Model.Customer
{
    public interface ICustomerService
    {
        Task<List<Domain.Model.Customer.Customer>> GetCustomersAsync(int page, int pageSize);
        Task<Domain.Model.Customer.Customer> GetCustomerAsync(Guid Id);
        Task<List<Domain.Model.Customer.Customer>> GetCustomersWithFilter(string customerName, string customerAddress,
            string customerTelephone, string customerEmailAddress, CustomerType? customerType, int page = 1, int pageSize = 10);

        Task PassivateCustomer(Guid Id);
        Task<Guid> CreateNewCustomer(CustomerRequestDTO request);
        Task<Guid> UpdateCustomer(Guid Id, CustomerRequestDTO request);
    }
}
