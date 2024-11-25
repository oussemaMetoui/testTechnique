namespace Domain.ValueObjects
{
    public class DateRange
    {
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        public DateRange(DateTime startDate, DateTime? endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException("StartDate must be before EndDate.");

            StartDate = startDate;
            EndDate = endDate ?? DateTime.MaxValue;
        }

        public bool OverlapsWith(DateRange other)
        {
            return StartDate < other.EndDate && EndDate > other.StartDate;
        }

        public int GetDurationInDays() => EndDate.HasValue ? (EndDate.Value - StartDate).Days : 0;

        public override bool Equals(object? obj)
        {
            if (obj is not DateRange other) return false;
            return StartDate == other.StartDate && EndDate == other.EndDate;
        }
    }
}
