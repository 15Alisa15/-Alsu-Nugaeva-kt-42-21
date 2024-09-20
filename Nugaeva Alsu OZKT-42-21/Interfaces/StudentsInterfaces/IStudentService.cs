using Nugaeva_Alsu_OZKT_42_21.Database;
using Nugaeva_Alsu_OZKT_42_21.Filters.StudentFilters;
using Nugaeva_Alsu_OZKT_42_21.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Nugaeva_Alsu_OZKT_42_21.Interfaces.StudentsInterfaces
{
    public interface IStudentService
    {
        public Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken);
    }

    public class StudentService : IStudentService
    {
        private readonly NugaevaDbContext _dbContext;
        public StudentService(NugaevaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _dbContext.Students
                .Where(s => s.Group.GroupName == filter.GroupName)
                .ToListAsync(cancellationToken);

            return students.ToArray();
        }
    }
}