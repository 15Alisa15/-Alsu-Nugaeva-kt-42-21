using Nugaeva_Alsu_OZKT_42_21.Database.Models;
using Nugaeva_Alsu_OZKT_42_21.Filters.SubjectsFilters;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Nugaeva_Alsu_OZKT_42_21.Interfaces
{
    public interface ISubjectService
    {
        Task<Subject[]> GetSubjectsByNameAsync(SubjectNameFilter filter, CancellationToken cancellationToken);
        Task<Subject[]> GetSubjectsByIsDeletedAsync(SubjectIsDeletedFilter filter, CancellationToken cancellationToken);
    }

    public class SubjectService : ISubjectService
    {
        private readonly NugaevaDbContext _dbContext;
        public SubjectService(NugaevaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Subject[]> GetSubjectsByNameAsync(SubjectNameFilter filter, CancellationToken cancellationToken = default)
        {
            var subjects = _dbContext.Set<Subject>().Where(w => w.SubjectName == filter.SubjectName).Where(w => w.IsDeleted == filter.SubjectIsDeleted).ToArrayAsync(cancellationToken);
            return subjects;
        }
        public Task<Subject[]> GetSubjectsByIsDeletedAsync(SubjectIsDeletedFilter filter, CancellationToken cancellationToken = default)
        {
            var subjects = _dbContext.Set<Subject>().Where(w => w.IsDeleted == filter.SubjectIsDeleted).ToArrayAsync(cancellationToken);
            return subjects;
        }
    }

}


