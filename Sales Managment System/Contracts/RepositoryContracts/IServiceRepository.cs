using Sales_Managment_System.Models;

namespace Sales_Managment_System.Contracts;

public interface IServiceRepository
{
    public IEnumerable<Service> getAll();
    public Service getById(Guid guid);
}