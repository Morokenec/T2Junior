namespace T2JuniorAPI.DTOs.Walls
{
    public class WallDTO
    {
        public Guid Id { get; set; }
        public Guid IdType { get; set; }
        public Guid IdOwner { get; set; }
        public string TypeName { get; set; }
        public string OwnerName { get; set; }

    }
}
