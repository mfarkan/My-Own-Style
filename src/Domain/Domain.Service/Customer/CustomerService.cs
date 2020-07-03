using AutoMapper;
using Core.Enumarations;
using Domain.DataLayer.Business;
using Domain.Model.Customer;
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
        private readonly IMapper _mapper;
        public CustomerService(IBusinessRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<CustomerResponseDTO> GetCustomerAsync(Guid Id)
        {
            var customerInstance = await _repository.GetByIdAsync<Domain.Model.Customer.Customer>(Id);
            var result = _mapper.Map<Domain.Model.Customer.Customer, CustomerResponseDTO>(customerInstance);
            return result;
        }

        public async Task<List<CustomerResponseDTO>> GetCustomersAsync(int page, int pageSize)
        {
            var skipSize = pageSize * (page - 1);
            var customerList = await _repository.QueryWithoutTracking<Domain.Model.Customer.Customer>()
                .Skip(skipSize).Take(pageSize).ToListAsync();
            var resultList = _mapper.Map<List<Domain.Model.Customer.Customer>, List<CustomerResponseDTO>>(customerList);
            return resultList;
        }

        public async Task<List<CustomerResponseDTO>> GetCustomersWithFilter(string customerName, string customerAddress, string customerTelephone, string customerEmailAddress, CustomerType? customerType, int page = 1, int pageSize = 10)
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
            var customerList = await query.ToListAsync();
            var resultList = _mapper.Map<List<Domain.Model.Customer.Customer>, List<CustomerResponseDTO>>(customerList);
            return resultList;

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
                CustomerCompanyType = request.CustomerType,
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
            customer.CustomerCompanyType = request.CustomerType;
            customer.CustomerName = request.CustomerName;
            customer.CustomerTelephoneNumber = request.CustomerTelephoneNumber;
            _repository.Update(customer);
            await _repository.CommitAsync();
            return customer.Id;
        }
    }
}
