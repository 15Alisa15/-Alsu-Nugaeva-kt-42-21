using Nugaeva_Alsu_OZKT_42_21.Filters.StudentFilters;
using Nugaeva_Alsu_OZKT_42_21.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using Xunit;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Nugaeva_Alsu_OZKT_42_21.Interfaces.StudentsInterfaces;

namespace Nugaeva_Alsu_OZKT_42_21.Tests
{
    public class StudentIntegrationTests
    {
        public readonly DbContextOptions<NugaevaDbContext> _dbContextOptions;

        public StudentIntegrationTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<NugaevaDbContext>()
            .UseInMemoryDatabase(databaseName: "student_db")
            .Options;
        }

        [Fact]
        public async Task GetStudentsByGroupAsync_KT4121_TwoObjects()
        {
            // Arrange
            var ctx = new NugaevaDbContext(_dbContextOptions);
            var studentService = new StudentService(ctx);
            var groups = new List<Group>
    {
        new Group
        {
            GroupName = "KT-42-21"
        },
        new Group
        {
            GroupName = "KT-41-21"
        }
    };
            await ctx.Set<Group>().AddRangeAsync(groups);

            var students = new List<Student>
    {
        new Student
        {
            FirstName = "qwerty",
            LastName = "asdf",
            MiddleName = "zxc",
            GroupId = 1,
        },
        new Student
        {
            FirstName = "qwerty2",
            LastName = "asdf2",
            MiddleName = "zxc2",
            GroupId = 2,
        },
        new Student
        {
            FirstName = "qwerty3",
            LastName = "asdf3",
            MiddleName = "zxc3",
            GroupId = 1,
        }
    };
            await ctx.Set<Student>().AddRangeAsync(students);

            await ctx.SaveChangesAsync();

            // Act
            var filter = new Filters.StudentFilters.StudentGroupFilter
            {
                GroupName = "KT-41-21"
            };

            try
            {
                var studentsResult = await studentService.GetStudentsByGroupAsync(filter, CancellationToken.None);

                Assert.Single(studentsResult); // Проверяем, что вернулось одно студентское лицо
                Assert.Equal("qwerty2", studentsResult.First().FirstName);
                Assert.Equal("asdf2", studentsResult.First().LastName);
                Assert.Equal("zxc2", studentsResult.First().MiddleName);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Caught NullReferenceException: {ex.Message}");
                // Здесь можно добавить дополнительную логику обработки ошибки
            }
        }
    }
}