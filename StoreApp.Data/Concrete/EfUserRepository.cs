using Microsoft.AspNetCore.Identity;
using StoreApp.Data.Abstract;

namespace StoreApp.Data.Concrete
{
    public class EfUserRepository : IUserRepository
    {
        private readonly StoreContext _context;

        public EfUserRepository(StoreContext context)
        {
            _context = context;
        }

        public IQueryable<IdentityUserRole<string>> UserRoles => _context.UserRoles;

        public async void CreateUserRole(IdentityUserRole<string> userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
            _context.SaveChanges();
        }
    }
}
