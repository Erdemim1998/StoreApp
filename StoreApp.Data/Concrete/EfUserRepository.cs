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

        public IQueryable<AppUser> Users => _context.Users;

        public IQueryable<AppRole> Roles => _context.Roles;

        public IQueryable<IdentityUserRole<string>> UserRoles => _context.UserRoles;

        public async void CreateUser(AppUser user)
        {
            await _context.Users.AddAsync(user);
            _context.SaveChanges();
        }

        public async void CreateRole(AppRole role)
        {
            await _context.Roles.AddAsync(role);
            _context.SaveChanges();
        }

        public async void CreateUserRole(IdentityUserRole<string> userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
            _context.SaveChanges();
        }
    }
}
