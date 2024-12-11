using XinWebAPI.Models.DTO.PlayGround;
using XinWebAPI.Models.PlayGround;

namespace XinWebAPI.Services.PlayGround
{
    public interface ICustomerService
    {
        Task<List<Customer>> getAllCustomersAsync();

        Task<Customer> addCustomer(CustomerDTO customerDTO);

        Task<Customer> getCustomer(int id);

        Task<Customer> updateCustomer(int id, CustomerDTO customerDTO);

        Task<Customer> deleteCustomer(int id);

        bool IsValid(CustomerDTO customerDTO);

    }
}
