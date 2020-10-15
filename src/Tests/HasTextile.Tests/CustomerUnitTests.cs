using Domain.Model.Customer;
using Domain.Model.Income;
using Domain.Model.Institution;
using Domain.Service.Model.Customer;
using Domain.Service.Model.Customer.Model;
using GenFu;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasTextile.Tests
{
    [TestFixture(Author = "Murat Fatih ARKAN", Description = "This is a test for customer services without real context and repository")]
    public class CustomerUnitTests
    {
        private List<Guid> Guids = new List<Guid> { new Guid("D7093A7E-6953-4FA6-BE65-235BA8ADC583") };
        private Institution GetInstitution()
        {
            GenFu.GenFu.Configure<Institution>()
                .Fill(q => q.Name, "MFARKAN COMPANY")
                .Fill(q => q.EmailAddress).AsEmailAddressForDomain("mfarkan.com")
                .Fill(q => q.Status, Core.Enumarations.StatusType.Active)
                .Fill(q => q.Code, "MF")
                .Fill(q => q.Id, new Guid("D724DE61-D72A-4963-87CF-A9CED8D88FA3"))
                .Fill(q => q.CreatedAt).AsPastDate()
                .Fill(q => q.PhoneNumber).AsPhoneNumber();
            return GenFu.GenFu.New<Institution>();
        }
        private List<Expenses> GetExpenses()
        {
            GenFu.GenFu.Configure<Expenses>()
                .Fill(q => q.Amount).WithinRange(1, 5000)
                .Fill(q => q.CurrencyType, Core.Enumarations.CurrencyType.TRY)
                .Fill(q => q.Description).AsMusicGenreDescription()
                .Fill(q => q.DocumentNumber)
                .Fill(q => q.Expiry)
                .Fill(q => q.CreatedAt).AsPastDate()
                .Fill(q => q.Institution, this.GetInstitution())
                .Fill(q => q.ExpiryDate)
                .Fill(q => q.Status, Core.Enumarations.StatusType.Active)
                .Fill(q => q.Type);
            return GenFu.GenFu.ListOf<Expenses>(2);
        }
        private Customer GetCustomer()
        {
            GenFu.GenFu.Configure<Customer>()
            .Fill(q => q.CreatedAt).AsPastDate()
            .Fill(q => q.CustomerAddress).AsAddress()
            .Fill(q => q.CustomerCompanyType)
            .Fill(q => q.Institution, this.GetInstitution())
            .Fill(q => q.CustomerDescription).AsMusicGenreDescription()
            .Fill(q => q.CustomerEmailAddress).AsEmailAddressForDomain("mfarkan.com")
            .Fill(q => q.CustomerName).AsMusicArtistName()
            .Fill(q => q.CustomerTelephoneNumber).AsPhoneNumber()
            .Fill(q => q.Status)
            .Fill(q => q.Id, new Guid("D7093A7E-6953-4FA6-BE65-235BA8ADC583"));
            return GenFu.GenFu.New<Customer>();
        }
        private List<Customer> GetCustomers()
        {
            GenFu.GenFu.Configure<Customer>()
                .Fill(q => q.CreatedAt).AsPastDate()
                .Fill(q => q.CustomerAddress).AsAddress()
                .Fill(q => q.CustomerCompanyType)
                .Fill(q => q.Institution, this.GetInstitution())
                .Fill(q => q.CustomerDescription).AsMusicGenreDescription()
                .Fill(q => q.CustomerEmailAddress).AsEmailAddressForDomain("mfarkan.com")
                .Fill(q => q.CustomerName).AsMusicArtistName()
                .Fill(q => q.CustomerTelephoneNumber).AsPhoneNumber()
                .Fill(q => q.Status)
                .Fill(q => q.Id).WithRandom(Guids);
            return GenFu.GenFu.ListOf<Customer>(2);
        }
        private CustomerFilterRequestDTO GetFilterRequestDTO()
        {
            GenFu.GenFu.Configure<CustomerFilterRequestDTO>()
                .Fill(q => q.InstitutionId, new Guid("D724DE61-D72A-4963-87CF-A9CED8D88FA3"))
                .Fill(q => q.CustomerEmail).AsEmailAddressForDomain("mfarkan.com")
                .Fill(q => q.CustomerName).AsMusicArtistName()
                .Fill(q => q.CustomerPhoneNumber).AsPhoneNumber()
                .Fill(q => q.Start, 0)
                .Fill(q => q.Length, 10)
                .Fill(q => q.CustomerType);
            return GenFu.GenFu.New<CustomerFilterRequestDTO>();
        }
        private ICustomerService _customerService;
        [SetUp]
        public void SetUp()
        {
            List<Customer> customerList = this.GetCustomers();
            var fakeCustomer = this.GetCustomer();
            var customerFilterRequestDTO = this.GetFilterRequestDTO();
            var moqCustomerService = new Mock<ICustomerService>();
            moqCustomerService.Setup(q => q.GetCustomerAsync(new Guid("D7093A7E-6953-4FA6-BE65-235BA8ADC583"))).ReturnsAsync(fakeCustomer);
            moqCustomerService.Setup(q => q.GetCustomerAsync(new Guid("8C4DA32F-EE15-4807-AF6D-15D3349CF6BB"))).ReturnsAsync(default(Customer));
            moqCustomerService.Setup(q => q.GetCustomerAsync(new Guid("E2156639-E8DC-46E0-B8CE-10907F01B428"))).ReturnsAsync(default(Customer));
            moqCustomerService.Setup(q => q.CreateNewCustomer(It.IsAny<CustomerRequestDTO>())).ReturnsAsync(new Guid("D7093A7E-6953-4FA6-BE65-235BA8ADC583"));
            moqCustomerService.Setup(q => q.GetAllCustomerAsync()).ReturnsAsync(customerList);
            moqCustomerService.Setup(q => q.GetCustomerExpensesAsync(new Guid("D7093A7E-6953-4FA6-BE65-235BA8ADC583"))).ReturnsAsync(fakeCustomer);
            moqCustomerService.Setup(q => q.GetCustomersWithFilter(customerFilterRequestDTO)).ReturnsAsync(customerList);
            moqCustomerService.Setup(q => q.UpdateCustomer(It.IsAny<Guid>(), It.IsAny<CustomerRequestDTO>())).ReturnsAsync(new Guid("D7093A7E-6953-4FA6-BE65-235BA8ADC583"));
            moqCustomerService.Setup(q => q.PassivateCustomer(new Guid("D7093A7E-6953-4FA6-BE65-235BA8ADC583"))).Returns(Task.CompletedTask);

            _customerService = moqCustomerService.Object;
        }
        [TestCase(true, "D7093A7E-6953-4FA6-BE65-235BA8ADC583")]
        [TestCase(false, "8C4DA32F-EE15-4807-AF6D-15D3349CF6BB")]
        [TestCase(false, "E2156639-E8DC-46E0-B8CE-10907F01B428")]
        [TestCase(false, "389EE5D3-09C2-4492-A623-281D173D0C71")]
        [TestCase(false, "E556DC5B-685D-4E2C-BD45-3FF3C5A459F7")]
        public async Task Customer_GetById_Empty_Result(bool expected, Guid Id)
        {
            var customer = await _customerService.GetCustomerAsync(Id);
            Assert.AreEqual(expected, customer != null);
            if (customer != null)
                Assert.IsNotNull(customer.CustomerEmailAddress, "All customer must have an email address");
        }
        [TestCase(true, 2)]
        [TestCase(false, 3)]
        [TestCase(false, 5)]
        public async Task Customer_CustomerList_Result(bool expectedResult, int expectedCount)
        {
            var customerList = await _customerService.GetAllCustomerAsync();

            Assert.IsNotNull(customerList, "Customer list must not be empty.");
            Assert.AreEqual(expectedResult, customerList.Count == 2);


        }
    }
}
