using Microsoft.EntityFrameworkCore;
using NikitaVasilevKt_41_23.Database;
using NikitaVasilevKt_41_23.Filters;
using NikitaVasilevKt_41_23.Models;

namespace NikitaVasilevKt_41_23.Interfaces
{
    public interface IStudentService
    {
        Task<Student[]> GetStudentsAsync(StudentFilter filter, CancellationToken cancellationToken = default);
        Task<Student> AddStudentAsync(Student student, CancellationToken cancellationToken = default);
        Task<Student?> UpdateStudentAsync(Student student, CancellationToken cancellationToken = default);
        Task<bool> DeleteStudentAsync(int studentId, CancellationToken cancellationToken = default);
    }

    public class StudentService : IStudentService
    {
        private readonly StudentDbContext _dbContext;

        public StudentService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Student[]> GetStudentsAsync(StudentFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Set<Student>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.GroupName))
                query = query.Where(s => s.Group.GroupName == filter.GroupName);

            if (!string.IsNullOrWhiteSpace(filter.FirstName))
                query = query.Where(s => s.FirstName == filter.FirstName);

            if (!string.IsNullOrWhiteSpace(filter.LastName))
                query = query.Where(s => s.LastName == filter.LastName);

            if (!string.IsNullOrWhiteSpace(filter.MiddleName))
                query = query.Where(s => s.MiddleName == filter.MiddleName);

            if (filter.IsDeleted.HasValue)
                query = query.Where(s => s.IsDeleted == filter.IsDeleted.Value);

            return query.ToArrayAsync(cancellationToken);
        }

        public async Task<Student> AddStudentAsync(Student student, CancellationToken cancellationToken = default)
        {
            student.Group = null!; // Чтобы EF Core привязывал по GroupId, не создавая дубликат группы
            _dbContext.Set<Student>().Add(student);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return student;
        }

        public async Task<Student?> UpdateStudentAsync(Student student, CancellationToken cancellationToken = default)
        {
            var existing = await _dbContext.Set<Student>().FirstOrDefaultAsync(s => s.StudentId == student.StudentId, cancellationToken);
            if (existing == null) return null;

            existing.FirstName = student.FirstName;
            existing.LastName = student.LastName;
            existing.MiddleName = student.MiddleName;
            existing.GroupId = student.GroupId;
            existing.IsDeleted = student.IsDeleted;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return existing;
        }

        public async Task<bool> DeleteStudentAsync(int studentId, CancellationToken cancellationToken = default)
        {
            var existing = await _dbContext.Set<Student>().FirstOrDefaultAsync(s => s.StudentId == studentId, cancellationToken);
            if (existing == null) return false;

            existing.IsDeleted = true;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}