namespace T2JuniorAPI.DTOs.Events
{
    public class UpdateEventDTO
    {
        public required Guid IdEvent { get; set; }
        public required Guid IdDirection { get; set; }
        public required string Name { get; set; }
        public required string Place { get; set; }
        public required DateTime StartDatetime { get; set; }
        public required DateTime EndDatetime { get; set; }
        public required int NumberParticpants { get; set; }
        public int? FactParticpants { get; set; }
        public int Raiting { get; set; }
    }
}
