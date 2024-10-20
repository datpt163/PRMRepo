using System;

namespace Capstone.Domain.Helpers
{
    public static class EffortCalculator
    {
        public static double CalculateEffort(DateTime startTime, DateTime endTime, bool includeWeekend = false)
        {
            if (endTime < startTime)
            {
                throw new ArgumentException("End time cannot be earlier than start time.");
            }

            double totalHours = 0;

            for (var date = startTime.Date; date <= endTime.Date; date = date.AddDays(1))
            {
                if (!includeWeekend && (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday))
                {
                    continue;
                }

                if (date == startTime.Date)
                {
                    totalHours += (date.AddDays(1) - startTime).TotalHours;
                }
                else if (date == endTime.Date)
                {
                    totalHours += (endTime - date).TotalHours;
                }
                else
                {
                    totalHours += 8;
                }
            }

            return totalHours;
        }

        public static int CalculateWorkdays(DateTime startDate, DateTime endDate, bool includeWeekend = false)
        {
            int workdays = 0;

            for (var date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
            {
                if (!includeWeekend && (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday))
                {
                    continue;
                }
                workdays++;
            }

            return workdays;
        }

    }
}
