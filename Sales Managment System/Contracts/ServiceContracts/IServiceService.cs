using Sales_Managment_System.Models;

namespace Sales_Managment_System.Contracts;

public interface IServiceService
{
    public Service GetService(Guid guid);


}