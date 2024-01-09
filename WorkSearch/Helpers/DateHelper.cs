namespace WorkSearch.Helpers
{
    public static class DateHelper
    {
        public static int GetAge(DateOnly date)
        {
            var diff = DateTime.Now - date.ToDateTime(TimeOnly.Parse("10:00 PM"));
            var yearsDiff = (new DateTime(1, 1, 1) + diff).Year - 1;
            return yearsDiff;
        }

        public static int GetAge(DateTime date)
        {
            var diff = DateTime.Now - date;
            var yearsDiff = (new DateTime(1, 1, 1) + diff).Year - 1;
            return yearsDiff;
        }
    }
}
