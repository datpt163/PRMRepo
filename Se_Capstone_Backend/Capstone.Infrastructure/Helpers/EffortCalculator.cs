using System;

namespace Capstone.Domain.Helpers
{
    public static class EffortCalculator
    {
        public static decimal CalculateSPI(decimal earnedValue, decimal plannedValue)
        {
            if (plannedValue == 0) return 0;
            return earnedValue / plannedValue;
        }

        public static decimal CalculateSV(decimal earnedValue, decimal plannedValue)
        {
            return earnedValue - plannedValue;
        }

        public static decimal CalculateCPI(decimal earnedValue, decimal actualCost)
        {
            if (actualCost == 0) return 0;
            return earnedValue / actualCost;
        }

        public static decimal CalculateCV(decimal earnedValue, decimal actualCost)
        {
            return earnedValue - actualCost;
        }

        public static decimal CalculateEAC(decimal budgetAtCompletion, decimal costPerformanceIndex)
        {
            if (costPerformanceIndex == 0) return 0;
            return budgetAtCompletion / costPerformanceIndex;
        }

        public static decimal CalculateBAC(decimal totalBudget)
        {
            return totalBudget;
        }

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

        public static decimal CalculateCompletionRate(int totalEstimatedTime, int totalActualTime)
        {
            if (totalEstimatedTime > 0)
            {
                return (decimal)totalActualTime / totalEstimatedTime * 100;
            }
            return 0;
        }
    }
}
