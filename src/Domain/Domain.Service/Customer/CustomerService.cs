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
        public CustomerService(IBusinessRepository repository)
        {
            //_mapper = mapper;
            _repository = repository;
        }
        public async Task<List<Domain.Model.Customer.Customer>> GetAllCustomerAsync()
        {
            var customerList = await _repository.GetAllAsync<Domain.Model.Customer.Customer>();
            return customerList;
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
                .Skip(skipSize).Take(pageSize).Where(q => q.Status == StatusType.Active).ToListAsync();
            return customerList ?? new List<Domain.Model.Customer.Customer>();
        }

        public async Task<List<Domain.Model.Customer.Customer>> GetCustomersWithFilter(CustomerFilterRequestDTO filterRequestDTO)
        {
            var query = _repository.QueryWithoutTracking<Domain.Model.Customer.Customer>().Where(q => q.Status == StatusType.Active);

            if (!string.IsNullOrEmpty(filterRequestDTO.CustomerName))
                query = query.Where(q => q.CustomerName.Contains(filterRequestDTO.CustomerName));

            if (!string.IsNullOrEmpty(filterRequestDTO.CustomerAddress))
                query = query.Where(q => q.CustomerAddress.Contains(filterRequestDTO.CustomerAddress));

            if (!string.IsNullOrEmpty(filterRequestDTO.CustomerPhoneNumber))
                query = query.Where(q => q.CustomerTelephoneNumber.Contains(filterRequestDTO.CustomerPhoneNumber));

            if (!string.IsNullOrEmpty(filterRequestDTO.CustomerEmail))
                query = query.Where(q => q.CustomerEmailAddress.Contains(filterRequestDTO.CustomerEmail));

            if (filterRequestDTO.CustomerType.HasValue)
                query = query.Where(q => q.CustomerCompanyType == filterRequestDTO.CustomerType.Value);

            query = query.Skip(filterRequestDTO.Start * filterRequestDTO.Length).Take(filterRequestDTO.Length);
            var customerList = await query.ToListAsync();
            return customerList ?? new List<Domain.Model.Customer.Customer>();

        }

        public async Task PassivateCustomer(Guid Id)
        {
            var customer = await _repository.GetByIdAsync<Domain.Model.Customer.Customer>(Id);
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
            var customer = await _repository.GetByIdAsync<Domain.Model.Customer.Customer>(Id);
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
        public async Task<Domain.Model.Customer.Customer> GetCustomerExpensesAsync(Guid Id)
        {
            var customerData = await _repository.QueryWithoutTracking<Domain.Model.Customer.Customer>()
                .Include(q => q.Expenses).FirstOrDefaultAsync(q => q.Status == StatusType.Active && q.Id == Id);
            return customerData;
        }
    }
}
