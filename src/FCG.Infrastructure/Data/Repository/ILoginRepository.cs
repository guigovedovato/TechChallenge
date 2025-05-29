using FCG.Domain.Authentication;

namespace FCG.Infrastructure.Data.Repository;

public interface ILoginRepository : IRepository<LoginModel>
{
    Task<LoginModel?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}
