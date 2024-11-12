using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nugaeva_Alsu_OZKT_42_21.Database.Models;
using Nugaeva_Alsu_OZKT_42_21.Filters.StudentFilters;
using Nugaeva_Alsu_OZKT_42_21.Interfaces.StudentsInterfaces;

using Xunit;

namespace Nugaeva_Alsu_OZKT_42_21.Tests
{
    public class StudentServiceTests
    {
        private readonly NugaevaDbContext _dbContext;
        private readonly IStudentService _studentService;

        public StudentServiceTests()
        {
            var options = new DbContextOptionsBuilder<NugaevaDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _dbContext = new NugaevaDbContext(options);
            _studentService = new StudentService(_dbContext);
        }

        [Fact]
        public async Task GetStudentsByFIOAsync_ShouldReturnStudentsByFIO()
        {
            // Подготовка
            var student1 = new Student { FirstName = "Иван", LastName = "Иванов", MiddleName = "Иванович" };
            var student2 = new Student { FirstName = "Петр", LastName = "Петров", MiddleName = "Петрович" };
            var student3 = new Student { FirstName = "Иван", LastName = "Сидоров", MiddleName = "Александрович" };
            var student4 = new Student { FirstName = "Владимир", LastName = "Владимиров", MiddleName = "Владимирович" };

            _dbContext.Students.Add(student1);
            _dbContext.Students.Add(student2);
            _dbContext.Students.Add(student3);
            _dbContext.Students.Add(student4);

            await _dbContext.SaveChangesAsync();

            // Действие
            var filter = new StudentFIOFilter { FirstName = "Иван", LastName = "Иванов", MiddleName = "Иванович", StudentIsDeleted = false };

            var result = await _studentService.GetStudentsByFIOAsync(filter, CancellationToken.None);

            // Утверждение
            Assert.Equal(2, result.Count());
         
        }

        [Fact]
        public async Task GetStudentsByIsDeletedAsync_ShouldReturnStudentsByIsDeleted()
        {
            // Подготовка
            var student1 = new Student { FirstName = "Иван", LastName = "Иванов", MiddleName = "", IsDeleted = true };
            var student2 = new Student { FirstName = "Петр", LastName = "Петров", MiddleName = "", IsDeleted = false };
            var student3 = new Student { FirstName = "Анна", LastName = "Аннаева", MiddleName = "", IsDeleted = true };

            _dbContext.Students.Add(student1);
            _dbContext.Students.Add(student2);
            _dbContext.Students.Add(student3);

            await _dbContext.SaveChangesAsync();

            // Действие
            var filter = new StudentIsDeletedFilter { StudentIsDeleted = true };
            var result = await _studentService.GetStudentsByIsDeletedAsync(filter, CancellationToken.None);

            // Утверждение
            Assert.Equal(2, result.Count());
            Assert.True(result[0].IsDeleted);
            Assert.True(result[1].IsDeleted);
        }

        [Fact]
        public async Task GetStudentsByGroupIdAsync_ShouldReturnStudentsFromGroup()
        {
            // Подготовка
            var group = new Group { GroupId = 1, GroupName = "КТ-42-21" };
            var student1 = new Student { FirstName = "Иван", LastName = "Иванов", MiddleName = "Иванович", GroupId = 1, IsDeleted = false };
            var student2 = new Student { FirstName = "Петр", LastName = "Петров", MiddleName = "Петрович", GroupId = 1, IsDeleted = false };
            var student3 = new Student { FirstName = "Анна", LastName = "Аннаева", MiddleName = "Петровна", GroupId = 2, IsDeleted = false };

            _dbContext.Groups.Add(group);
            _dbContext.Students.Add(student1);
            _dbContext.Students.Add(student2);
            _dbContext.Students.Add(student3);

            await _dbContext.SaveChangesAsync();

            // Действие
            var filter = new StudentGroupIdFilter { GroupId = 1 };
            var result = await _studentService.GetStudentsByGroupIdAsync(filter, CancellationToken.None);

            // Утверждение
            Assert.Equal(2, result.Count()); // Ожидаем 2 студентских лица
            Assert.Equal(1, result[0].GroupId);
            Assert.Equal("Иван", result[0].FirstName);
            Assert.Equal("Иванов", result[0].LastName);
            Assert.Equal("Петр", result[1].FirstName);
            Assert.Equal("Петров", result[1].LastName);
        }
    }
}