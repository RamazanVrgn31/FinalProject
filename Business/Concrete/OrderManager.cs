using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class OrderManager:IOrderService
    {
        private IOrderService _orderService;

        public OrderManager(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public List<Order> GetAll()
        {
             return  _orderService.GetAll();
        }

        public void Add(Order order)
        {
            _orderService.Add(order);
        }

        public void Update(Order order)
        {
            _orderService.Update(order);
        }

        public void Delete(Order order)
        {
            _orderService.Delete(order);

        }
    }
}
