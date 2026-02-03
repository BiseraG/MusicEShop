using MusicEShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEShop.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<MusicEShopUser> GetAll();
        MusicEShopUser Get(string? id);
        void Insert(MusicEShopUser entity);
        void Update(MusicEShopUser entity);
        void Delete(MusicEShopUser entity);
    }
}
