using Microsoft.AspNetCore.Identity;
using StoreApp.Data.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.Abstract
{
    public interface IUserRepository
    {
        IQueryable<AppUser> Users { get; }

        IQueryable<AppRole> Roles { get; }

        IQueryable<IdentityUserRole<string>> UserRoles { get; }

        void CreateUser(AppUser user);

        void CreateRole(AppRole role);

        void CreateUserRole(IdentityUserRole<string> userRole);
    }
}
