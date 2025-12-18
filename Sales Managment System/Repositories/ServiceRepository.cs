using Sales_Managment_System.Contracts;
using Sales_Managment_System.Models;

namespace Sales_Managment_System.Services;

public class ServiceRepository(AppDbContext context) : IServiceRepository
{
    public IEnumerable<Service> getAll()
    {
        return context.services.ToList();
    }

    public Service? getById(Guid guid)
    {
        return context.services.Find(guid);
    }

    
}