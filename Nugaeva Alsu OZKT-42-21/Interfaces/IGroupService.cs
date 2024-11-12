using Nugaeva_Alsu_OZKT_42_21.Filters.GroupFilters;
using System.Threading.Tasks;
using System.Threading;
using Nugaeva_Alsu_OZKT_42_21.Database.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Nugaeva_Alsu_OZKT_42_21.Interfaces
{
    public interface IGroupService
    {
        Task<Group[]> GetGroupsByIsDeletedAsync(GroupIsDeletedFilter filter, CancellationToken cancellationToken);
    }

    public class GroupService : IGroupService
    {
        private readonly NugaevaDbContext _dbContext;
        public GroupService(NugaevaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Group[]> GetGroupsByIsDeletedAsync(GroupIsDeletedFilter filter, CancellationToken cancellationToken = default)
        {
            var students = _dbContext.Set<Group>().Where(w => w.IsDeleted == filter.GroupIsDeleted).ToArrayAsync(cancellationToken);
            return students;
        }
    }

}