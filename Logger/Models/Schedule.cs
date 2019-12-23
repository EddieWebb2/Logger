namespace Logger.Models
{
    public class Schedule: BaseEntity
    {
        public string Name { get; set; }
        public AlertFrequency ScheduleType { get; set; }
    }
}
