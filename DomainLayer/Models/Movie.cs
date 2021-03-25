using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainLayer.Models
{
    [Index(nameof(Name))]
    public class Movie : IEntity<int>
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Enter name", AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required]
        public int Duration { get; set; }
        public virtual ICollection<Order> Orders { get; set; }


        [NotMapped]
        public int Key => ID;
        [NotMapped]
        public bool LoadedConections { get; set; }
        [NotMapped]
        public string Index => Name;

        public void UpdateEntity(IEntity<int> newEntity)
        {
            if (newEntity is Movie movie)
            {
                this.ID = movie.ID;
                this.Name = movie.Name;
                this.Duration = movie.Duration;
                this.Orders = new List<Order>(movie.Orders);
            }
            else
            {
                throw new InvalidOperationException("The updated and the updating entities must be from the same type");
            }
        }

        public Movie()
        {
            Orders = new List<Order>();
        }
    }
}
