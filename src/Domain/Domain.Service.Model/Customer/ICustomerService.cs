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
        Task<List<CustomerResponseDTO>> GetCustomersAsync(int page, int pageSize);
        Task<CustomerResponseDTO> GetCustomerAsync(Guid Id);
        Task<List<CustomerResponseDTO>> GetCustomersWithFilter(string customerName, string customerAddress,
            string customerTelephone, string customerEmailAddress, CustomerType? customerType, int page = 1, int pageSize = 10);

        Task PassivateCustomer(Guid Id);
        Task<Guid> CreateNewCustomer(CustomerRequestDTO request);
        Task<Guid> UpdateCustomer(Guid Id, CustomerRequestDTO request);
    }
}
