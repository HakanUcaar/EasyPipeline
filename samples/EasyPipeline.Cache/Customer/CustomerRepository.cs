namespace EasyPipeline.Cache;

public class CustomerRepository
{
    public Customer GetCustomerById(int id)
    {
        return new Customer(id, "John", "Doe", "123456789", "USA");
    }
}
