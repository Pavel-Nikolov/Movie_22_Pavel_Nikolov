using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer.DataAccess
{
    public class MovieRepository : Repository<Movie, int>
    {        
        public MovieRepository(Context context) : base(context, context.Movies)
        {

        }

        protected override IEnumerable<Movie> LoadCollection(DbSet<Movie> dbSet)
        {
            return dbSet.Include(x => x.Orders);
        }

        protected override void LoadEntity(Movie entity)
        {
            context.Entry<Movie>(entity).Collection(e => e.Orders).Load();
        }

        protected override void MapConnections(Movie brand)
        {
            if (brand.Orders == null)
            {
                return;
            }
            List<Order> orders = context.Orders.ToList();
            Order orderFromDb;
            for (int i = 0; i < orders.Count; i++)
            {
                orderFromDb = context.Orders.Find(orders[i].ID);
                if (orderFromDb != null)
                {
                    orders[i] = orderFromDb;
                }
            }
            brand.Orders = orders;
        }

    }
}
