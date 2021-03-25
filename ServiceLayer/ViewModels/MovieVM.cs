using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.ViewModels
{
    public class MovieVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public List<OrderVM> Orders { get; set; }

        public MovieVM(Movie movie, bool loadConnection = false)
        {
            ID = movie.ID;
            Name = movie.Name;
            Duration = movie.Duration;
            if (loadConnection)
            {
                Orders = new List<OrderVM>();
                foreach (var item in movie.Orders)
                {
                    Orders.Add(new OrderVM(item));
                }
            }
        }
    }
}
