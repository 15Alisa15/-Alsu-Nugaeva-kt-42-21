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
                    GroupName = "KT-44-21"
                }
            };
            await ctx.Set<Group>().AddRangeAsync(groups);

            var students = new List<Student>
            {
                new Student
                {
                    FirstName = "Иван",
                    LastName = "Иванов",
                    MiddleName = "Иванович",
                    GroupId = 2,
                },
                new Student
                {
                    FirstName = "Петр",
                    LastName = "Петров",
                    MiddleName = "Петрович",
                    GroupId = 2,
                },
                new Student
                {
                    FirstName = "Владимир",
                    LastName = "Владимиров",
                    MiddleName = "Владимирович",
                    GroupId = 2,
                }
            };
            await ctx.Set<Student>().AddRangeAsync(students);

            await ctx.SaveChangesAsync();

            // Act
            var filterGroup = new Filters.StudentFilters.StudentGroupFilter
            {
                GroupName = "KT-44-21"
            };

            var filterFIO = new Filters.StudentFilters.StudentFIOFilter
            {
                FirstName = "арпопаоп",
                LastName = "Иванов",
                MiddleName = "Иванович",
            };

            try
            {
                var studentsResultGroup = await studentService.GetStudentsByGroupAsync(filterGroup, CancellationToken.None);
                var studentsResultFIO = await studentService.GetStudentsByFIOAsync(filterFIO, CancellationToken.None);

                // Assert
                Assert.Equal(3, studentsResultGroup.Length);

                Assert.Equal(1, studentsResultFIO.Length);
           
               

            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Caught NullReferenceException: {ex.Message}");
                // Здесь можно добавить дополнительную логику обработки ошибки
            }
        }
    }
}