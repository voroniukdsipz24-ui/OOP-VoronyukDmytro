using lek23.Models;

namespace lek23.Interfaces
{
    public interface IOrderRepository
    {
        void Save(Order order);
        Order GetById(int id);
    }
}
