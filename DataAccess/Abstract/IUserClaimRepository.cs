using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserClaimRepository : IEntityRepository<UserClaim>, IAsyncEntityRepository<UserClaim>
    {
        Task<IEnumerable<SelectionItem>> GetUserClaimSelectedList(int userId);
        Task<IEnumerable<UserClaim>> BulkInsert(int userId, IEnumerable<UserClaim> userClaims);
    }
}