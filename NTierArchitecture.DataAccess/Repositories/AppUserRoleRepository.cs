using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.Entities.Models;
using NTierArchitecture.Entities.Repositories;

namespace NTierArchitecture.DataAccess.Repositories;

internal sealed class AppUserRoleRepository : Repository<AppUserRole>, IAppUserRoleRepository
{
    public AppUserRoleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}


