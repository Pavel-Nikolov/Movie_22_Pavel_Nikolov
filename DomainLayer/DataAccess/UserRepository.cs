using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.DataAccess
{
    public class UserRepository : Repository<User, int>
    {
        public UserRepository(Context context):base(context, context.Users)
        {

        }

        protected override IEnumerable<User> LoadCollection(DbSet<User> dbSet)
        {
            return dbSet.Include(x => x.Orders);
        }

        protected override void LoadEntity(User entity)
        {
            context.Entry<User>(entity).Collection(x => x.Orders).Load();
        }

        protected override void MapConnections(User user)
        {
            if (user.Orders == null)
            {
                return;
            }

            List<Order> orders = new List<Order>(user.Orders);
            Order orderFromDb;

            for (int i = 0; i < orders.Count; i++)
            {
                orderFromDb = context.Orders.Find(orders[i].ID);
                if (orderFromDb != null)
                {
                    orders[i] = orderFromDb;
                }
            }

            user.Orders = orders;
        }
    }
}
