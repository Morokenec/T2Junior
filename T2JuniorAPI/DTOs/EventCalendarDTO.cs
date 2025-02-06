namespace T2JuniorAPI.DTOs
{
    public class EventCalendarDTO
    {
        public Guid IdEvent { get; set; }

        public required string Name { get; set; }

        public required DateTime StartDatetime { get; set; }

        public required DateTime EndDatetime { get; set; }

    }
}
