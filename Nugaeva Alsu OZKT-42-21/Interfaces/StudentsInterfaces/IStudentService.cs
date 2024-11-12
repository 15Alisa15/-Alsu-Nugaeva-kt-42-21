using Nugaeva_Alsu_OZKT_42_21.Filters.StudentFilters;
using Nugaeva_Alsu_OZKT_42_21.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Nugaeva_Alsu_OZKT_42_21.Database.Configurations;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nugaeva_Alsu_OZKT_42_21.Interfaces.StudentsInterfaces;
using System.Collections.Generic;

namespace Nugaeva_Alsu_OZKT_42_21.Interfaces.StudentsInterfaces
{
    public interface IStudentService
    {
        public Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken);
        public Task<Student[]> GetStudentsByFIOAsync(StudentFIOFilter filter, CancellationToken cancellationToken);
        public Task<Student[]> GetStudentsByIsDeletedAsync(StudentIsDeletedFilter filter, CancellationToken cancellationToken);
        public Task<Student[]> GetStudentsByGroupIdAsync(StudentGroupIdFilter filter, CancellationToken cancellationToken);
        
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
            var students = await _dbContext.Set<Student>()
                .Where(w => w.Group.GroupName == filter.GroupName)
                .Where(w => w.IsDeleted == filter.StudentIsDeleted)
                .ToListAsync(cancellationToken);

            return students.ToArray();
        }

        public async Task<Student[]> GetStudentsByGroupIdAsync(StudentGroupIdFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _dbContext.Students
                .Include(s => s.Group)
                .Where(s => s.Group.GroupId == filter.GroupId && !s.IsDeleted)
                .ToArrayAsync(cancellationToken);

            return students;
        }
    

    public async Task<Student[]> GetStudentsByFIOAsync(StudentFIOFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _dbContext.Set<Student>()
                .Where(w => w.FirstName == filter.FirstName &&
                             w.LastName == filter.LastName &&
                             w.MiddleName == filter.MiddleName)
                .Where(w => w.IsDeleted == filter.StudentIsDeleted)
                .ToListAsync(cancellationToken);

            return students.ToArray();
        }

        public async Task<Student[]> GetStudentsByIsDeletedAsync(StudentIsDeletedFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _dbContext.Set<Student>()
                .Where(w => w.IsDeleted == filter.StudentIsDeleted)
                .ToListAsync(cancellationToken);

            return students.ToArray();
        }
    }
}