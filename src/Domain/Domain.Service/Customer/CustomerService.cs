using Core.Enumarations;
using Domain.DataLayer.Business;
using Domain.Model.Customer;
using Domain.Service.Model.Customer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly IBusinessRepository _repository;
        public CustomerService(IBusinessRepository repository)
        {
            _repository = repository;
        }
        public async Task<Domain.Model.Customer.Customer> GetCustomerAsync(Guid Id)
        {
            var result = await _repository.GetByIdAsync<Domain.Model.Customer.Customer>(Id);
            return result;
        }

        public async Task<List<Domain.Model.Customer.Customer>> GetCustomersAsync()
        {
            var resultList = await _repository.GetAllAsync<Domain.Model.Customer.Customer>();
            return resultList;
        }
        
        public async Task<List<Domain.Model.Customer.Customer>> GetCustomersWithFilter(string customerName, string customerAddress, string customerTelephone, string customerEmailAddress, CustomerType? customerType, int page = 1, int pageSize = 10)
        {
            // asNotracking eklenebilir.
            var query = _repository.QueryWithoutTracking<Domain.Model.Customer.Customer>().Where(q => q.Status == StatusType.Active);

            if (!string.IsNullOrEmpty(customerName))
                query.Where(q => q.CustomerName.Contains(customerName));

            if (!string.IsNullOrEmpty(customerAddress))
                query.Where(q => q.CustomerAddress.Contains(customerAddress));

            if (!string.IsNullOrEmpty(customerTelephone))
                query.Where(q => q.CustomerTelephoneNumber.Contains(customerTelephone));

            if (!string.IsNullOrEmpty(customerEmailAddress))
                query.Where(q => q.CustomerEmailAddress.Contains(customerEmailAddress));

            if (customerType.HasValue)
                query.Where(q => q.CustomerCompanyType == customerType.Value);

            query.Skip(page).Take(pageSize);
            var resultList = await query.ToListAsync();
            return resultList;

        }
    }
}
