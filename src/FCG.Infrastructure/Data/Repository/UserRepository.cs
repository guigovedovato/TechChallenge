using FCG.Domain.Profile;
using FCG.Infrastructure.Data.Context;
using FCG.Infrastructure.Log;

namespace FCG.Infrastructure.Data.Repository;

public class UserRepository(ApplicationDbContext _context, BaseLogger _logger) : Repository<UserModel>(_context, _logger), IUserRepository
{
}
