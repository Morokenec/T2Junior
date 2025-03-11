namespace T2JuniorAPI.Entities
{
    public class InitiativeStatus : BaseCommonProperties
    {
        public string Name { get; set; }
        public virtual ICollection<Initiative> Initiatives { get; set; }
    }
}
