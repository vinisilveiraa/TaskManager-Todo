namespace ToDoApi.DTOs.Profile
{
    public class ProfileStatsDto
    {
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int PendingTasks { get; set; }
        public int CompletedThisWeek { get; set; }
        public int CompletedThisMonth { get; set; }
        public double CompletionRate { get; set; }
        public List<DailyTaskStatsDto> MonthlyActivity { get; set; } = new();
        public List<DailyTaskStatsDto> WeeklyActivity { get; set; } = new();
    }
}
