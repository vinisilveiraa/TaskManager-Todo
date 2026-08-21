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
            var daysInMonth = DateTime.DaysInMonth(
                today.Year,
                today.Month
            );

            var completedThisWeek = tasks.Count(x => x.Completed_At > startOfWeek);
            var completedThisMonth = tasks.Count(x => x.Completed_At > startOfMonth);

            var completionRate = totalTasks == 0 ? 0 : (double)completedTasks / totalTasks * 100;


            // Enumerable.Range faz uma sequencia de numeros de (start, limit)
            // AddDays soma dias na data
            var weeklyStats = Enumerable.Range(0, 7)
                .Select(i =>
                {
                    var date = startOfWeek.AddDays(i);

                    return new DailyTaskStatsDto
                    {
                        Date = date,
                        completed = tasks.Count(x =>
                        x.IsCompleted && x.Completed_At.HasValue && x.Completed_At.Value.Date == date
                        )
                    };
                }).ToList();

            var montlyStats = Enumerable.Range(0, daysInMonth)
                .Select(i =>
            {
                var date = startOfMonth.AddDays(i);

                return new DailyTaskStatsDto
                {
                    Date = date,
                    completed = tasks.Count(x =>
                    x.IsCompleted && x.Completed_At.HasValue && x.Completed_At.Value.Date == date
                    )
                };
            }).ToList();

            return new ProfileStatsDto
            {
                TotalTasks = totalTasks,
                CompletedTasks = completedTasks,
                PendingTasks = pendingTasks,
                CompletedThisMonth = completedThisMonth,
                CompletedThisWeek = completedThisWeek,
                CompletionRate = completionRate,

                MonthlyActivity = montlyStats,
                WeeklyActivity = weeklyStats
            };

        }
    }
}
