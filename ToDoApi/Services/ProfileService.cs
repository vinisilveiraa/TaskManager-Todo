using ToDoApi.DTOs.Profile;
using ToDoApi.Repositories;

namespace ToDoApi.Services
{
    public class ProfileService
    {
        private readonly ItemRepository _repository;
        public ProfileService(ItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProfileStatsDto> GetStatsAsync(int userId)
        {
            var tasks = await _repository.GetByUserIdAsync(userId, null, null);

            var totalTasks = tasks.Count;

            var completedTasks = tasks.Count(x => x.IsCompleted);
            var pendingTasks = tasks.Count(x => !x.IsCompleted);

            var today = DateTime.UtcNow.Date;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            var startOfMonth = new DateTime(
                today.Year,
                today.Month,
                1
            );

            var completedThisWeek = tasks.Count(x => x.Completed_At > startOfWeek);
            var completedThisMonth = tasks.Count(x => x.Completed_At > startOfMonth);

            var completionRate = totalTasks == 0 ? 0 : (double)completedTasks / totalTasks * 100;

            return new ProfileStatsDto
            {
                TotalTasks = totalTasks,
                CompletedTasks = completedTasks,
                PendingTasks = pendingTasks,
                CompletedThisMonth = completedThisMonth,
                CompletedThisWeek = completedThisWeek,
                CompletionRate = completionRate
            };

        }
    }
}
