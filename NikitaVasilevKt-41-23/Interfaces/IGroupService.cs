using Microsoft.EntityFrameworkCore;
using NikitaVasilevKt_41_23.Database;
using NikitaVasilevKt_41_23.Filters;
using NikitaVasilevKt_41_23.Models;

namespace NikitaVasilevKt_41_23.Interfaces
{
    public interface IGroupService
    {
        Task<Group[]> GetGroupsAsync(GroupFilter filter, CancellationToken cancellationToken = default);
        Task<Group> AddGroupAsync(Group group, CancellationToken cancellationToken = default);
        Task<Group?> UpdateGroupAsync(Group group, CancellationToken cancellationToken = default);
        Task<bool> DeleteGroupAsync(int groupId, CancellationToken cancellationToken = default);
    }

    public class GroupService : IGroupService
    {
        private readonly StudentDbContext _dbContext;

        public GroupService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Group[]> GetGroupsAsync(GroupFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Set<Group>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Speciality))
                query = query.Where(g => g.Speciality == filter.Speciality);

            if (filter.AdmissionYear.HasValue)
                query = query.Where(g => g.AdmissionYear == filter.AdmissionYear.Value);

            if (filter.IsDeleted.HasValue)
                query = query.Where(g => g.IsDeleted == filter.IsDeleted.Value);

            return query.ToArrayAsync(cancellationToken);
        }

        public async Task<Group> AddGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<Group>().Add(group);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return group;
        }

        public async Task<Group?> UpdateGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            var existing = await _dbContext.Set<Group>().FirstOrDefaultAsync(g => g.GroupId == group.GroupId, cancellationToken);
            if (existing == null) return null;

            existing.GroupName = group.GroupName;
            existing.Speciality = group.Speciality;
            existing.AdmissionYear = group.AdmissionYear;
            existing.IsDeleted = group.IsDeleted;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return existing;
        }

        public async Task<bool> DeleteGroupAsync(int groupId, CancellationToken cancellationToken = default)
        {
            var existing = await _dbContext.Set<Group>().FirstOrDefaultAsync(g => g.GroupId == groupId, cancellationToken);
            if (existing == null) return false;

            // Мягкое удаление группы и привязанных к ней студентов
            existing.IsDeleted = true;
            var students = await _dbContext.Set<Student>().Where(s => s.GroupId == groupId).ToListAsync(cancellationToken);
            foreach (var student in students)
            {
                student.IsDeleted = true;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}