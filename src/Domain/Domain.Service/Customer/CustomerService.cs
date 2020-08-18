using Core.Enumarations;
using Domain.DataLayer.Business;
using Domain.Service.Model.Customer;
using Domain.Service.Model.Customer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Service.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly IBusinessRepository _repository;
        //private readonly IMapper _mapper;
        public CustomerService(IBusinessRepository repository)
        {
            //_mapper = mapper;
            _repository = repository;
        }
        public async Task<Domain.Model.Customer.Customer> GetCustomerAsync(Guid Id)
        {
            var customerInstance = await _repository.GetByIdAsync<Domain.Model.Customer.Customer>(Id);
            return customerInstance;
        }

        public async Task<List<Domain.Model.Customer.Customer>> GetCustomersAsync(int page, int pageSize)
        {
            var skipSize = pageSize * (page - 1);
            var customerList = await _repository.QueryWithoutTracking<Domain.Model.Customer.Customer>()
                .Skip(skipSize).Take(pageSize).ToListAsync();
            return customerList ?? new List<Domain.Model.Customer.Customer>();
        }

        public async Task<List<Domain.Model.Customer.Customer>> GetCustomersWithFilter(string customerName, string customerAddress, string customerTelephone, string customerEmailAddress, CustomerType? customerType, int page = 1, int pageSize = 10)
        {
            var query = _repository.QueryWithoutTracking<Domain.Model.Customer.Customer>().Where(q => q.Status == StatusType.Active);

            if (!string.IsNullOrEmpty(customerName))
                query = query.Where(q => q.CustomerName.Contains(customerName));

            if (!string.IsNullOrEmpty(customerAddress))
                query = query.Where(q => q.CustomerAddress.Contains(customerAddress));

            if (!string.IsNullOrEmpty(customerTelephone))
                query = query.Where(q => q.CustomerTelephoneNumber.Contains(customerTelephone));

            if (!string.IsNullOrEmpty(customerEmailAddress))
                query = query.Where(q => q.CustomerEmailAddress.Contains(customerEmailAddress));

            if (customerType.HasValue)
                query = query.Where(q => q.CustomerCompanyType == customerType.Value);

            query = query.Skip(page).Take(pageSize);
            var customerList = await query.ToListAsync();
            return customerList ?? new List<Domain.Model.Customer.Customer>();

        }

        public async Task PassivateCustomer(Guid Id)
        {
            var customer = await _repository.Query<Domain.Model.Customer.Customer>().Where(q => q.Id == Id).FirstOrDefaultAsync();
            if (customer == null)
                return;
            customer.Delete();
            _repository.Update(customer);
            await _repository.CommitAsync();
        }
        public async Task<Guid> CreateNewCustomer(CustomerRequestDTO request)
        {
            Domain.Model.Customer.Customer newCustomer = new Domain.Model.Customer.Customer
            {
                CustomerAddress = request.CustomerAddress,
                CustomerCompanyType = request.CustomerCompanyType,
                CustomerEmailAddress = request.CustomerEmailAddress,
                CustomerDescription = request.CustomerDescription,
                CustomerName = request.CustomerName,
                CustomerTelephoneNumber = request.CustomerTelephoneNumber
            };
            _repository.Add(newCustomer);
            await _repository.CommitAsync();
            return newCustomer.Id;
        }
        public async Task<Guid> UpdateCustomer(Guid Id, CustomerRequestDTO request)
        {
            var customer = await _repository.Query<Domain.Model.Customer.Customer>().Where(q => q.Id == Id).FirstOrDefaultAsync();
            if (customer == null)
                return Guid.Empty;

            customer.CustomerAddress = request.CustomerAddress;
            customer.CustomerEmailAddress = request.CustomerEmailAddress;
            customer.CustomerDescription = request.CustomerDescription;
            customer.CustomerCompanyType = request.CustomerCompanyType;
            customer.CustomerName = request.CustomerName;
            customer.CustomerTelephoneNumber = request.CustomerTelephoneNumber;
            _repository.Update(customer);
            await _repository.CommitAsync();
            return customer.Id;
        }
    }
}
