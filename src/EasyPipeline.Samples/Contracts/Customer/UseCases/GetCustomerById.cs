using EasyPipeline.Samples.Contracts;
using EasyPipeline.Samples.Repositories;

namespace EasyPipeline.Samples.UseCases;

public record GetCustomerByIdQuery(int Id);

public class GetCustomerById(PipelineExecuter<GetCustomerByIdQuery, Customer> pipelineExecuter, CustomerRepository customerRepository)
{
    public async Task<Customer> Handle(GetCustomerByIdQuery query)
    {        
        return await pipelineExecuter.Run(query,(query) =>
        {
            Console.WriteLine("Method execute");
            return customerRepository.GetCustomerById(query.Id); 
        });
    }
}
