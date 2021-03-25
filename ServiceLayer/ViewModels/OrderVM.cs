using DomainLayer.Models;
using System.Collections.Generic;

namespace ServiceLayer.ViewModels
{
    public class OrderVM
    {
        public int ID { get; set; }
        public string Name { get; set; }        
        public decimal Price { get; set; }
        public MovieVM Movie { get; set; }
        public List<UserVM> Users { get; set; }

        public OrderVM(Order order, bool loadConnection = false)
        {
            ID = order.ID;
            Name = order.Name;
            
            Price = order.Price;

            if (loadConnection)
            {
                order.Movie = order.Movie;
                Movie = new MovieVM(order.Movie);

                Users = new List<UserVM>();
                foreach (var item in order.Users)
                {
                    Users.Add(new UserVM(item));
                }
            }
        }
    }
}