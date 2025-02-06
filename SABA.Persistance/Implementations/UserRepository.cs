using PashaBank.Repository.Implementations;
using SABA.Core.Abstractions;
using SABA.Core.Models.UserModel;
using SABA.Persistance.DataContext;
using System.Linq.Expressions;

namespace SABA.Persistance.Implementations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context) { }


        public async Task<User> FindUserAsync(Expression<Func<User, bool>> predicate)
        {
            var result = await this.FindAsync(predicate);
            return result.FirstOrDefault();
        }

    }
}
