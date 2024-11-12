using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nugaeva_Alsu_OZKT_42_21.Database.Models;

using Nugaeva_Alsu_OZKT_42_21.Filters.SubjectsFilters;
using Nugaeva_Alsu_OZKT_42_21.Interfaces;
using Xunit;

namespace Nugaeva_Alsu_OZKT_42_21.Tests
{
    public class SubjectServiceTests
    {
        private readonly NugaevaDbContext _dbContext;
        private readonly ISubjectService _subjectService;

        public SubjectServiceTests()
        {
            var options = new DbContextOptionsBuilder<NugaevaDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _dbContext = new NugaevaDbContext(options);
            _subjectService = new SubjectService(_dbContext);
        }

        [Fact]
        public async Task GetSubjectsByNameAsync_ShouldReturnSubjectsByName()
        {
            // Подготовка
            var subject1 = new Subject { SubjectName = "Математика", IsDeleted = false };
            var subject2 = new Subject { SubjectName = "Русский язык", IsDeleted = false };
            var subject3 = new Subject { SubjectName = "Английский язык", IsDeleted = true };

            _dbContext.Subjects.Add(subject1);
            _dbContext.Subjects.Add(subject2);
            _dbContext.Subjects.Add(subject3);

            await _dbContext.SaveChangesAsync();

            // Действие
            var filter = new SubjectNameFilter { SubjectName = "Математика", SubjectIsDeleted = false };
            var result = await _subjectService.GetSubjectsByNameAsync(filter, CancellationToken.None);

            // Утверждение
            Assert.Single(result);
            Assert.Equal("Математика", result[0].SubjectName);
            Assert.False(result[0].IsDeleted);
        }
    }
}

        

       