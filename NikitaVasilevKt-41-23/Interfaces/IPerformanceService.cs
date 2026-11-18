using Microsoft.EntityFrameworkCore;
using NikitaVasilevKt_41_23.Database;
using NikitaVasilevKt_41_23.Filters;
using NikitaVasilevKt_41_23.Models;

namespace NikitaVasilevKt_41_23.Interfaces
{
    public interface IPerformanceService
    {
        Task<Grade[]> GetStudentGradesAsync(int studentId, CancellationToken cancellationToken = default);
        Task<double> GetAverageGradeByGroupAndDisciplineAsync(int groupId, int disciplineId, CancellationToken cancellationToken = default);
        Task<double> GetAverageGradeByYearAsync(int admissionYear, CancellationToken cancellationToken = default);
        Task<Grade> AddGradeAsync(Grade grade, CancellationToken cancellationToken = default);
        Task<Grade?> UpdateGradeAsync(Grade grade, CancellationToken cancellationToken = default);
    }

    public class PerformanceService : IPerformanceService
    {
        private readonly StudentDbContext _dbContext;

        public PerformanceService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Оценки у конкретного студента
        public Task<Grade[]> GetStudentGradesAsync(int studentId, CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<Grade>()
                .Where(g => g.StudentId == studentId)
                .ToArrayAsync(cancellationToken);
        }

        // Средний балл по предмету в группе
        public async Task<double> GetAverageGradeByGroupAndDisciplineAsync(int groupId, int disciplineId, CancellationToken cancellationToken = default)
        {
            var grades = await _dbContext.Set<Grade>()
                .Where(g => g.Student.GroupId == groupId && g.DisciplineId == disciplineId)
                .Select(g => g.Value)
                .ToListAsync(cancellationToken);

            return grades.Count > 0 ? grades.Average() : 0.0;
        }

        // Средний балл по году
        public async Task<double> GetAverageGradeByYearAsync(int admissionYear, CancellationToken cancellationToken = default)
        {
            var grades = await _dbContext.Set<Grade>()
                .Where(g => g.Student.Group.AdmissionYear == admissionYear)
                .Select(g => g.Value)
                .ToListAsync(cancellationToken);

            return grades.Count > 0 ? grades.Average() : 0.0;
        }

        // Добавление оценки конкретному студенту
        public async Task<Grade> AddGradeAsync(Grade grade, CancellationToken cancellationToken = default)
        {
            grade.Student = null!;
            grade.Discipline = null!;
            _dbContext.Set<Grade>().Add(grade);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return grade;
        }

        // Изменение оценки у студента
        public async Task<Grade?> UpdateGradeAsync(Grade grade, CancellationToken cancellationToken = default)
        {
            var existing = await _dbContext.Set<Grade>().FirstOrDefaultAsync(g => g.GradeId == grade.GradeId, cancellationToken);
            if (existing == null) return null;

            existing.Value = grade.Value;
            existing.StudentId = grade.StudentId;
            existing.DisciplineId = grade.DisciplineId;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return existing;
        }
    }
}