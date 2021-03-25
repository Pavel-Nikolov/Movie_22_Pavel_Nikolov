using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models
{
    [Index(nameof(Name))]
    public class Order : IEntity<int>
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Enter name", AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be positive")]        
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Enter movie")]
        public virtual Movie Movie { get; set; }
        public virtual ICollection<User> Users { get; set; }


        [NotMapped]
        public int Key => ID;
        [NotMapped]
        public bool LoadedConections { get; set; }
        [NotMapped]
        public string Index => Name;

        public void UpdateEntity(IEntity<int> newEntity)
        {
            if (newEntity is Order order)
            {
                ID = order.ID;
                Name = order.Name;                
                Price = order.Price;
                Movie = order.Movie;
                Users = new List<User>(order.Users);
            }
            else
            {
                throw new InvalidOperationException("The updated and the updating entities must be from the same type");
            }
        }

        public Order()
        {
            Users = new List<User>();
        }
    }
}