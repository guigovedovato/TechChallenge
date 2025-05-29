using FCG.Domain.Library;
using FCG.Infrastructure.Data.Context;
using FCG.Infrastructure.Log;

namespace FCG.Infrastructure.Data.Repository;

public class LibraryRepository(ApplicationDbContext _context, BaseLogger _logger) : Repository<LibraryModel>(_context, _logger), ILibraryRepository
{
}
