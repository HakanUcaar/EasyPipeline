using EasyPipeline.Samples.Contracts;

namespace EasyPipeline.Samples.Repositories;

public class CustomerRepository
{
    public Customer GetCustomerById(int id)
    {
        return new Customer(id, "John", "Doe", "123456789", "USA");
    }
}
