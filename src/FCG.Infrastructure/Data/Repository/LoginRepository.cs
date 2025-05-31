using FCG.Domain.Login;
using FCG.Infrastructure.Data.Context;
using FCG.Infrastructure.Log;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Data.Repository;

public class LoginRepository(ApplicationDbContext _context, BaseLogger _logger) : Repository<LoginModel>(_context, _logger), ILoginRepository
{
    public async Task<LoginModel?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        try
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Email.Equals(email), cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving {typeof(LoginModel)} with email {email}: {ex.Message}");
            throw;
        }
    }
}
