using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.DataAccess
{
    public class OrderRepository : Repository<Order, int>
    {
        public OrderRepository(Context context):base(context, context.Orders)
        {

        }

        protected override IEnumerable<Order> LoadCollection(DbSet<Order> dbSet)
        {
            return dbSet.Include(x => x.Movie).Include(x => x.Users);
        }

        protected override void LoadEntity(Order entity)
        {
            context.Entry<Order>(entity).Reference(x => x.Movie).Load();
            context.Entry<Order>(entity).Collection(x => x.Users).Load();
        }

        protected override void MapConnections(Order entity)
        {
            Movie movieFromDb = context.Movies.Find(entity.Movie.ID);
            if (movieFromDb != null)
            {
                entity.Movie = movieFromDb;
            }

            if (entity.Users == null)
            {
                return;
            }

            List<User> users = new List<User>(entity.Users);
            User userFromDb;
            for (int i = 0; i < users.Count; i++)
            {
                userFromDb = context.Users.Find(users[i].ID);
                if (userFromDb != null)
                {
                    users[i] = userFromDb;
                }
            }
            entity.Users = users;
        }
    }
}
