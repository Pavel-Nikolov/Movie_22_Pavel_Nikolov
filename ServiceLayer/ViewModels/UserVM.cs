using DomainLayer.Models;
using System.Collections.Generic;

namespace ServiceLayer.ViewModels
{
    public class UserVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public List<OrderVM> Orders { get; set; }

        public UserVM(User user, bool loadConnection = false)
        {
            ID = user.ID;
            Name = user.Name;
            Age = user.Age;
            if (loadConnection)
            {
                Orders = new List<OrderVM>();
                foreach (var item in user.Orders)
                {
                    Orders.Add(new OrderVM(item));
                }
            }
        }
    }
}