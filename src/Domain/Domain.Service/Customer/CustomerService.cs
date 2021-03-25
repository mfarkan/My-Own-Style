using Domain.Service.Model.Customer;

namespace Domain.Service.Customer
{
    public class CustomerService : ICustomerService
    {
        //private readonly IBusinessRepository _repository;
        //public CustomerService(IBusinessRepository repository)
        //{
        //    _repository = repository;
        //}
        //public async Task<List<Domain.Model.Customer.Customer>> GetAllCustomerAsync()
        //{
        //    return await _repository.GetAllAsync<Domain.Model.Customer.Customer>();
        //}
        //public async Task<Domain.Model.Customer.Customer> GetCustomerAsync(Guid Id)
        //{
        //    return await _repository.GetByIdAsync<Domain.Model.Customer.Customer>(Id);
        //}

        //public async Task<List<Domain.Model.Customer.Customer>> GetCustomersAsync(int page, int pageSize)
        //{
        //    var skipSize = pageSize * (page - 1);
        //    var customerList = await _repository.QueryWithoutTracking<Domain.Model.Customer.Customer>()
        //        .Skip(skipSize).Take(pageSize).Where(q => q.Status == StatusType.Active).ToListAsync();
        //    return customerList ?? new List<Domain.Model.Customer.Customer>();
        //}

        //public async Task<List<Domain.Model.Customer.Customer>> GetCustomersWithFilter(CustomerFilterRequestDTO filterRequestDTO)
        //{
        //    var query = _repository.QueryWithoutTracking<Domain.Model.Customer.Customer>().Where(q => q.Status == StatusType.Active
        //    && q.Institution.Id == filterRequestDTO.InstitutionId);

        //    if (!string.IsNullOrEmpty(filterRequestDTO.CustomerName))
        //        query = query.Where(q => q.CustomerName.Contains(filterRequestDTO.CustomerName));

        //    if (!string.IsNullOrEmpty(filterRequestDTO.CustomerAddress))
        //        query = query.Where(q => q.CustomerAddress.Contains(filterRequestDTO.CustomerAddress));

        //    if (!string.IsNullOrEmpty(filterRequestDTO.CustomerPhoneNumber))
        //        query = query.Where(q => q.CustomerTelephoneNumber.Contains(filterRequestDTO.CustomerPhoneNumber));

        //    if (!string.IsNullOrEmpty(filterRequestDTO.CustomerEmail))
        //        query = query.Where(q => q.CustomerEmailAddress.Contains(filterRequestDTO.CustomerEmail));

        //    if (filterRequestDTO.CustomerType.HasValue)
        //        query = query.Where(q => q.CustomerCompanyType == filterRequestDTO.CustomerType.Value);

        //    query = query.Skip(filterRequestDTO.Start * filterRequestDTO.Length).Take(filterRequestDTO.Length);
        //    var customerList = await query.ToListAsync();
        //    return customerList ?? new List<Domain.Model.Customer.Customer>();

        //}

        //public async Task PassivateCustomer(Guid Id)
        //{
        //    await _repository.PassivateEntityAsync<Domain.Model.Customer.Customer>(Id);
        //    await Task.CompletedTask;
        //}
        //public async Task<Guid> CreateNewCustomer(CustomerRequestDTO request)
        //{
        //    var institution = await _repository.GetByIdAsync<Domain.Model.Institution.Institution>(request.InstitutionId);
        //    if (institution == null)
        //        return Guid.Empty;

        //    Domain.Model.Customer.Customer newCustomer = new Domain.Model.Customer.Customer
        //    {
        //        CustomerAddress = request.CustomerAddress,
        //        CustomerCompanyType = request.CustomerCompanyType,
        //        CustomerEmailAddress = request.CustomerEmailAddress,
        //        CustomerDescription = request.CustomerDescription,
        //        CustomerName = request.CustomerName,
        //        CustomerTelephoneNumber = request.CustomerTelephoneNumber,
        //        Institution = institution
        //    };
        //    _repository.Add(newCustomer);
        //    await _repository.CommitAsync();
        //    return newCustomer.Id;
        //}
        //public async Task<Guid> UpdateCustomer(Guid Id, CustomerRequestDTO request)
        //{
        //    var customer = await _repository.GetByIdAsync<Domain.Model.Customer.Customer>(Id);
        //    var institution = await _repository.GetByIdAsync<Domain.Model.Institution.Institution>(request.InstitutionId);
        //    if (customer == null || institution == null)
        //        return Guid.Empty;

        //    customer.CustomerAddress = request.CustomerAddress;
        //    customer.CustomerEmailAddress = request.CustomerEmailAddress;
        //    customer.CustomerDescription = request.CustomerDescription;
        //    customer.CustomerCompanyType = request.CustomerCompanyType;
        //    customer.CustomerName = request.CustomerName;
        //    customer.Institution = institution;
        //    customer.CustomerTelephoneNumber = request.CustomerTelephoneNumber;
        //    _repository.Update(customer);
        //    await _repository.CommitAsync();
        //    return customer.Id;
        //}
        //public async Task<Domain.Model.Customer.Customer> GetCustomerExpensesAsync(Guid Id)
        //{
        //    var customerData = await _repository.QueryWithoutTracking<Domain.Model.Customer.Customer>()
        //        .Include(q => q.Expenses).FirstOrDefaultAsync(q => q.Status == StatusType.Active && q.Id == Id);
        //    return customerData;
        //}
    }
}
