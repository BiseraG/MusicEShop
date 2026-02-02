using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicEShop.Domain.Identity;
using MusicEShop.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEShop.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<MusicEShopUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<MusicEShopUser>();
        }

        public void Delete(MusicEShopUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public MusicEShopUser? Get(string? id)
        {
            return entities
                .Include(z => z.Cart)
                .ThenInclude(c => c.CartItems!)
                .ThenInclude(ci => ci.Track)
                .Include(z => z.Cart)
                .ThenInclude(c => c.CartItems!)
                .ThenInclude(ci => ci.Album)
                .SingleOrDefault(s => s.Id == id);
        }

        public IEnumerable<MusicEShopUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(MusicEShopUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(MusicEShopUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }
    }
}
