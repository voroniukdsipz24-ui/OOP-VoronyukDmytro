using System;
using lek23.Interfaces;
using lek23.Models;

namespace lek23.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public decimal CalculateDiscount(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            if (order.Amount <= 0)
                throw new ArgumentException("Сума повинна бути більшою за нуль");

            if (order.IsVipCustomer)
                return order.Amount * 0.15m;

            return order.Amount * 0.05m;
        }

        public void CreateOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            if (order.Amount <= 0)
                throw new ArgumentException("Некоректна сума");

            _repository.Save(order);
        }

        public Order GetOrder(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Некоректний ID");

            return _repository.GetById(id);
        }
    }
}
