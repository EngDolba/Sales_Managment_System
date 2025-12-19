using Sales_Managment_System.Contracts;
using Sales_Managment_System.Models;

namespace Sales_Managment_System.Services;

public class ServiceService(
    IServiceRepository repository) : IServiceService
{
    public Service GetService(Guid guid)
    {
        return repository.getById(guid);
    }
}