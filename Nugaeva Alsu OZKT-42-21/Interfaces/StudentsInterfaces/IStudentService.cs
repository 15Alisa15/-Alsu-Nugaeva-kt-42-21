
using Nugaeva_Alsu_OZKT_42_21.Filters.StudentFilters;
using Nugaeva_Alsu_OZKT_42_21.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Nugaeva_Alsu_OZKT_42_21.Database.Configurations;
using System.Collections.Generic;

namespace Nugaeva_Alsu_OZKT_42_21.Interfaces.StudentsInterfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken);
    }

    public class StudentService : IStudentService
    {
        private readonly NugaevaDbContext _dbContext;
        public StudentService(NugaevaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Student>> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken = default)
        {
            var group = await _dbContext.Groups
      .FirstOrDefaultAsync(g => g.GroupName == filter.GroupName, cancellationToken);


            if (group == null)
                return Enumerable.Empty<Student>();

            return await _dbContext.Students
                .Where(s => s.GroupId == group.GroupId)
                .ToListAsync(cancellationToken);
        }
    }
}