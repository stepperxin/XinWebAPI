using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XinWebAPI.Data.PlayGround;
using XinWebAPI.Models.DTO.PlayGround;
using XinWebAPI.Models.PlayGround;

namespace XinWebAPI.Services.PlayGround
{
    public class CustomerService : ICustomerService
    {
        private readonly PlayGroundDBContext _studentPortalDbContext;
        private readonly IMapper _mapper;
        public CustomerService(PlayGroundDBContext studentPortalDbContext, IMapper mapper)
        {
            this._studentPortalDbContext = studentPortalDbContext;
            this._mapper = mapper;
        }

        public async Task<List<Customer>> getAllCustomersAsync()
        {
            var customer = await _studentPortalDbContext.Customer.ToListAsync();
            return customer;
        }

        public async Task<Customer> addCustomer(CustomerDTO customerDTO)
        {
            if (IsValid(customerDTO))
            {
                Customer customer = _mapper.Map<Customer>(customerDTO);
                _studentPortalDbContext.Customer.Add(customer);
                await _studentPortalDbContext.SaveChangesAsync();
                return customer;
            }
            else return null;
        }

        public async Task<Customer> getCustomer(int id)
        {
            var customer = await _studentPortalDbContext.Customer.Where(x => x.Id == id).FirstOrDefaultAsync();
            return customer;
        }

        public async Task<Customer> updateCustomer(int id, CustomerDTO customerDTO)
        {
            if (IsValid(customerDTO))
            {
                var customer = await _studentPortalDbContext.Customer.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (customer != null)
                {
                    customer.Name = customerDTO.Name;
                    customer.Industry = customerDTO.Industry;
                    await _studentPortalDbContext.SaveChangesAsync();
                }
                return customer;
            }
            else return null;
        }

        public async Task<Customer> deleteCustomer(int id)
        {
            var result = await _studentPortalDbContext.Customer.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (result != null)
            {
                _studentPortalDbContext.Customer.Remove(result);
                await _studentPortalDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public bool IsValid(CustomerDTO customerDTO)
        {
            if (customerDTO == null)
                throw new ArgumentNullException(nameof(customerDTO));
            if (customerDTO.Name == null || customerDTO.Name == string.Empty)
                throw new Exception("Name is required.");
            if (customerDTO.Name.Length > 200)
                throw new Exception("Name can be 200 chars long.");
            if (customerDTO.Industry == null || customerDTO.Industry == string.Empty)
                throw new Exception("Name is required.");
            if (customerDTO.Industry.Length > 200)
                throw new Exception("Name can be 200 chars long.");
            return true;
        }

    }
        
}
