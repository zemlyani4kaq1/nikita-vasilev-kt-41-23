using Microsoft.EntityFrameworkCore;
using NikitaVasilevKt_41_23.Database;
using NikitaVasilevKt_41_23.Filters;
using NikitaVasilevKt_41_23.Models;

namespace NikitaVasilevKt_41_23.Interfaces
{
    public interface IDisciplineService
    {
        Task<Discipline[]> GetDisciplinesAsync(DisciplineFilter filter, CancellationToken cancellationToken = default);
        Task<Discipline> AddDisciplineAsync(Discipline discipline, CancellationToken cancellationToken = default);
        Task<Discipline?> UpdateDisciplineAsync(Discipline discipline, CancellationToken cancellationToken = default);
        Task<bool> DeleteDisciplineAsync(int disciplineId, CancellationToken cancellationToken = default);
    }

    public class DisciplineService : IDisciplineService
    {
        private readonly StudentDbContext _dbContext;

        public DisciplineService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Discipline[]> GetDisciplinesAsync(DisciplineFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Set<Discipline>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Direction))
                query = query.Where(d => d.Direction == filter.Direction);

            if (filter.IsDeleted.HasValue)
                query = query.Where(d => d.IsDeleted == filter.IsDeleted.Value);

            return query.ToArrayAsync(cancellationToken);
        }

        public async Task<Discipline> AddDisciplineAsync(Discipline discipline, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<Discipline>().Add(discipline);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return discipline;
        }

        public async Task<Discipline?> UpdateDisciplineAsync(Discipline discipline, CancellationToken cancellationToken = default)
        {
            var existing = await _dbContext.Set<Discipline>().FirstOrDefaultAsync(d => d.DisciplineId == discipline.DisciplineId, cancellationToken);
            if (existing == null) return null;

            existing.DisciplineName = discipline.DisciplineName;
            existing.Direction = discipline.Direction;
            existing.IsDeleted = discipline.IsDeleted;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return existing;
        }

        public async Task<bool> DeleteDisciplineAsync(int disciplineId, CancellationToken cancellationToken = default)
        {
            var existing = await _dbContext.Set<Discipline>().FirstOrDefaultAsync(d => d.DisciplineId == disciplineId, cancellationToken);
            if (existing == null) return false;

            existing.IsDeleted = true;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}