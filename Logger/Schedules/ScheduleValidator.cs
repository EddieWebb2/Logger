using System;
using Logger.Infrastructure;
using Logger.Types;

namespace Logger.Schedules
{
    public class ScheduleValidator
    {
        private readonly IScheduler _schedule;

        public ScheduleValidator(IScheduler schedule)
        {
            _schedule = schedule;
        }

        public void Validate()
        {
            try
            {
                CheckFrequencyIsNotNone();
            }
            catch (Exception ex)
            {
                //Log.Error(ex, "Invalid Schedule Type");
                throw;
            }
        }

        private void CheckFrequencyIsNotNone()
        {
            if (_schedule.AlertFrequency == AlertFrequencies.None)
            {
                throw new NotSupportedException("Schedules cannot be initialized with a frequency of None");
            }
        }
    }
}
