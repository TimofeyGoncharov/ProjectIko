using ProjectIko.Db.Interface;
using ProjectIko.Models;

namespace ProjectIko.Db.Repository
{
    public class IkoRepository : Repository<Model>, IIkoRepository
    {
        private readonly AppContext _context;
        private readonly ILogger _logger;

        public IkoRepository
            (
            AppContext context
            , ILogger<IkoRepository> logger
            ) : base(logger, context)
        {
            _context = context;
            _logger = logger;
        }
    }
}
